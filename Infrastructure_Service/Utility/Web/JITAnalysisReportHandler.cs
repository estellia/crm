/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/16 14:13:30
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Locale;
using JIT.Utility.Report;
using JIT.Utility.Report.Analysis;

namespace JIT.Utility.Web
{
    /// <summary>
    /// JIT的分析报表的处理者
    /// </summary>
    public abstract class JITAnalysisReportHandler<TUserInfo> : JITAjaxHandler<TUserInfo> where TUserInfo : BasicUserInfo
    {
        #region 属性集
        /// <summary>
        /// 当前的报表请求操作
        /// </summary>
        private Operations? CurrentOperation { get; set; }

        /// <summary>
        /// 是否缓存分析报表的输出
        /// </summary>
        protected bool IsCacheAnalysisReportOutput { get; set; }
        #endregion

        #region 实现对分析报表的Ajax请求的处理
        /// <summary>
        /// 实现对分析报表的Ajax请求的处理
        /// </summary>
        /// <param name="pContext">HTTP上下文</param>
        protected override void ProcessAjaxRequest(HttpContext pContext)
        {
            this.InitParams(pContext);
            if (this.CurrentOperation.HasValue == false)
            {
                throw new ArgumentException("请求的QueryString中必须带有op项,以表明请求的是何操作.");
            }
            //
            var report = this.GetReportInstance();
            AnalysisReportOutput output = null;
            switch (this.CurrentOperation.Value)
            {
                case Operations.Query:
                    #region 查询
                    {
                        var query = this.GetReportQuery();
                        if (query != null)
                        {
                            output = report.ProcessQuery(query.WhereCondtions, query.OrderBys, query.Language.HasValue?query.Language.Value: Languages.zh_CN);
                        }
                        else
                        {
                            output = report.ProcessQuery(null, null, Languages.zh_CN);
                        }
                    }
                    #endregion
                    break;
                case Operations.Goto:
                    #region 跳转
                    {
                        var para = this.DeserializeJSONContent<AnalysisReportClientPara>();
                        if (para == null || string.IsNullOrWhiteSpace(para.SectionID))
                        {
                            throw new Exception("缺少跳转到哪个钻取剖面参数.");
                        }
                        output = report.ProcessGoto(para.SectionID);
                    }
                    #endregion
                    break;
                case Operations.DrillIn:
                    #region 钻取
                    {
                        var para = this.DeserializeJSONContent<AnalysisReportClientPara>();
                        if (para == null || string.IsNullOrWhiteSpace(para.DimColumnID) || string.IsNullOrWhiteSpace(para.DimValue))
                        {
                            throw new Exception("缺少钻取的参数.");
                        }
                        output = report.ProcessDrillIn(para.DimColumnID, para.DimValue,para.DimText,null);
                    }
                    #endregion
                    break;
                case Operations.ChangePivot:
                    #region 改变数据透视
                    {
                        var para = this.DeserializeJSONContent<AnalysisReportClientPara>();
                        if (para == null || string.IsNullOrWhiteSpace(para.PivotChangedColumnID) ||para.IsPivoted.HasValue ==false)
                        {
                            throw new Exception("改变数据透视缺少参数.");
                        }
                        output = report.ProcessPivotChanged(para.PivotChangedColumnID, para.IsPivoted.Value);
                    }
                    #endregion
                    break;
                case Operations.ChangeCRConversion:
                    #region 改变行列转换
                    {
                        var para = this.DeserializeJSONContent<AnalysisReportClientPara>();
                        if (para == null || string.IsNullOrWhiteSpace(para.CRConvertionChangedColumnID) || para.IsCRConverted.HasValue == false)
                        {
                            throw new Exception("改变行列转换缺少参数.");
                        }
                        output = report.ProcessCRConversionChanged(para.CRConvertionChangedColumnID, para.IsCRConverted.Value);
                    }
                    #endregion
                    break;
                case Operations.ExportToExcel:
                    #region 导出到Excel
                    {
                        if (this.IsCacheAnalysisReportOutput)
                        {
                            var sessionID = this.GetCachingSessionID();
                            output = this.CurrentContext.Session[sessionID] as AnalysisReportOutput;
                            if (output == null)
                            {
                                output = report.ProcessQueryAgain();
                            }
                        }
                        else
                        {
                            output = report.ProcessQueryAgain();
                        }

                        this.CurrentContext.Response.Clear();
                        this.CurrentContext.Response.ContentType = "application/vnd.ms-excel";
                        this.CurrentContext.Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}.xls",DateTime.Now.ToSerialString()));
                        var excel = output.WriteXLS();
                        using (var stream = excel.SaveToStream())
                        {
                            var bytes =stream.ToArray();
                            this.CurrentContext.Response.OutputStream.Write(bytes, 0, bytes.Length);
                        }
                        this.CurrentContext.Response.End();
                    }
                    #endregion
                    break;
                case Operations.CRExchange:
                    #region 行列互换
                    {
                        output = report.ProcessCRExchange();
                    }
                    #endregion
                    break;
                case Operations.ViewDetail:
                    #region 查看明细
                    {
                        var drillings = this.DeserializeJSONContent<MultiDrilling>();
                        StringBuilder text = new StringBuilder();
                        text.Append(drillings.DimText);
                        List<IWhereCondition> wheres = new List<IWhereCondition>();
                        if (drillings.DrillingItems != null)
                        {
                            //text.Append("[");
                            //bool hasAppend = false;
                            //
                            var columns = report.CurrentColumns;
                            foreach (var item in drillings.DrillingItems)
                            {
                                var col = columns.FindBy(item.DimColumnID);
                                var dimCol = col as DimColumn;
                                wheres.Add(new EqualsCondition() { FieldName = dimCol.DataColumn.PropertyName, Value = item.DimValue });
                                text.AppendFormat("【{0}】", item.DimText);
                            }
                            //text.Append("]");
                        }
                        output = report.ProcessDrillIn(drillings.DimColumnID, drillings.DimValue, text.ToString(), wheres.ToArray());
                    }
                    #endregion
                    break;
                default:
                    throw new NotImplementedException(string.Format("未实现的操作:{0}.",this.CurrentOperation.Value));
            }
            //缓存分析报表的输出
            if (this.IsCacheAnalysisReportOutput)
            {
                var sessionID = this.GetCachingSessionID();
                this.CurrentContext.Session[sessionID] = output;
            }
            //输出
            this.ResponseAnalysisReportOutput(output);
        }
        #endregion

        #region 抽象方法
        /// <summary>
        /// 获取分析报表实例
        /// </summary>
        /// <returns></returns>
        protected abstract IAnalysisReport GetReportInstance();

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        protected abstract AnalysisReportQuery GetReportQuery();
        #endregion

        #region 虚方法
        /// <summary>
        /// 将分析报表的output向前端输出
        /// </summary>
        /// <param name="pOutput"></param>
        protected virtual void ResponseAnalysisReportOutput(AnalysisReportOutput pOutput)
        {
            var script = new StringBuilder();
            script.Append(pOutput.GenerateUpdateDataScript(0));
            this.ResponseContent(script.ToString());
        }
        #endregion

        #region 工具方法

        #region 初始化参数
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="pContext"></param>
        private void InitParams(HttpContext pContext)
        {
            string strOperation = pContext.Request.QueryString["op"];
            if (!string.IsNullOrWhiteSpace(strOperation))
            {
                Operations temp;
                if (Enum.TryParse<Operations>(strOperation, out temp))
                {
                    this.CurrentOperation = temp;
                }
            }
        }
        #endregion

        #region 获取用于缓存报表输出的Session ID
        /// <summary>
        /// 获取用于缓存报表输出的Session ID
        /// </summary>
        /// <returns></returns>
        public string GetCachingSessionID()
        {
            var report = this.GetReportInstance();
            return string.Format("{0}_{0}_{1}",this.CurrentUserInfo.ClientID,this.CurrentUserInfo.UserID,report.ReportID);
        }
        #endregion
        #endregion

        #region 内部类
        /// <summary>
        /// 分析报表的前端传过来的参数
        /// </summary>
        class AnalysisReportClientPara
        {
            #region 钻取时用
            /// <summary>
            /// 维度列ID
            /// </summary>
            public string DimColumnID { get; set; }
            /// <summary>
            /// 维度值
            /// </summary>
            public string DimValue { get; set; }
            /// <summary>
            /// 维度文本
            /// </summary>
            public string DimText { get; set; }
            #endregion

            #region 跳转时用
            /// <summary>
            /// 跳转的钻取剖面ID
            /// </summary>
            public string SectionID { get; set; }
            #endregion

            #region 数据透视改变时用
            /// <summary>
            /// 被改变的维度列ID
            /// </summary>
            public string PivotChangedColumnID { get; set; }

            /// <summary>
            /// 改变后的值
            /// </summary>
            public bool? IsPivoted { get; set; }
            #endregion

            #region 行列转换改变时用
            /// <summary>
            /// 需要进行行列转换的列ID
            /// </summary>
            public string CRConvertionChangedColumnID { get; set; }

            /// <summary>
            /// 改变后的值
            /// </summary>
            public bool? IsCRConverted { get; set; }
            #endregion
        }
        /// <summary>
        /// 多维度钻取
        /// </summary>
        class MultiDrilling
        {
            /// <summary>
            /// 维度列ID
            /// </summary>
            public string DimColumnID { get; set; }
            /// <summary>
            /// 维度值
            /// </summary>
            public string DimValue { get; set; }
            /// <summary>
            /// 维度文本
            /// </summary>
            public string DimText { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DrillingItem[] DrillingItems { get; set; }
        }

        /// <summary>
        /// 钻取项
        /// </summary>
        class DrillingItem
        {
            public DrillingItem()
            {
            }
            /// <summary>
            /// 维度列ID
            /// </summary>
            public string DimColumnID { get; set; }
            /// <summary>
            /// 维度值
            /// </summary>
            public string DimValue { get; set; }
            /// <summary>
            /// 维度文本
            /// </summary>
            public string DimText { get; set; }
        }
        #endregion
    }
}
