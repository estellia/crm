/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/7 15:18:04
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
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;

using JIT.Utility.DataAccess.Query;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Locale;
using JIT.Utility.Report.Analysis;
using JIT.Utility.Report.FactData;
using JIT.Utility.Report.Utils;
using JIT.Utility.Report.DataFilter;
using JIT.Utility.Web;
using JIT.Utility.Web.ComponentModel.ExtJS;
using JIT.Utility.Web.ComponentModel.ExtJS.Data;
using JIT.Utility.Web.ComponentModel.ExtJS.Menu;
using JIT.Utility.Web.ComponentModel.ExtJS.Form;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;
using JIT.Utility.Web.ComponentModel.ExtJS.Data.Field;

namespace JIT.Utility.Report
{
    /// <summary>
    /// 分析报表的基类 
    /// </summary>
    public abstract class BaseAnalysisReport:IAnalysisReport
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaseAnalysisReport()
        {
            this.LoadingMessage = "数据加载中,请稍后...";
            this.ReportID = Guid.NewGuid().ToText();
            this.IsDimColumnFirst = true;
        }
        #endregion

        #region 属性集

        #region 私有的&受保护的
        /// <summary>
        /// 当前的事实数据模型
        /// </summary>
        protected FactDataModel CurrentFactDataModel { get; set; }

        /// <summary>
        /// 当前的分析模型
        /// </summary>
        protected AnalysisModel CurrentAnalysisMode { get; set; }

        /// <summary>
        /// 当前的过滤条件
        /// </summary>
        protected IWhereCondition[] CurrentWhereConditions { get; set; }

        /// <summary>
        /// 当前的排序条件
        /// </summary>
        protected OrderBy[] CurrentOrderBys { get; set; }

        /// <summary>
        /// 当前的语言
        /// </summary>
        protected Languages CurrentLanguage { get; set; }

        /// <summary>
        /// 报表数据集中列与报表列的映射关系
        /// </summary>
        private Dictionary<string,AnalysisColumn> ColumnMappings { get; set; }

        /// <summary>
        /// 分析报表表格面板的ID
        /// </summary>
        protected string ReportGridPanelID { get; set; }

        /// <summary>
        /// 钻取导航控件的ID
        /// </summary>
        protected string NaviID { get; set; }

        /// <summary>
        /// 由钻取带来的筛选条件
        /// </summary>
        protected Dictionary<string,IWhereCondition[]> ConditionsByDrilling { get; set; }

        /// <summary>
        /// 报表查询的JS函数
        /// </summary>
        private JSFunction JSFunctionOfQuery { get; set; }

        /// <summary>
        /// 报表的跳转到指定钻取剖面的JS函数
        /// </summary>
        private JSFunction JSFunctionOfGoto { get; set; }

        /// <summary>
        /// 报表钻取的JS函数
        /// </summary>
        private JSFunction JSFunctionOfDrilling { get; set; }

        /// <summary>
        /// 改变报表中的数据透视列的JS函数
        /// </summary>
        private JSFunction JSFunctionOfChangePivot { get; set; }

        /// <summary>
        /// 改变行列转换的JS函数
        /// </summary>
        private JSFunction JSFunctionOfChangeCRConversion { get; set; }

        /// <summary>
        /// 行列互换的JS函数
        /// </summary>
        private JSFunction JSFunctionOfCRExchange { get; set; }

        /// <summary>
        /// 导出数据到Excel的JS函数
        /// </summary>
        private JSFunction JSFunctionOfExportToExcel { get; set; }

        /// <summary>
        /// 查看明细的JS函数
        /// </summary>
        private JSFunction JSFunctionOfViewDetail { get; set; }

        /// <summary>
        /// 多维度的钻取
        /// </summary>
        private JSFunction JSFunctionOfMultiDimDrill { get; set; }
        #endregion

        #region 公共的

        /// <summary>
        /// 获得报表的查询条件的JS函数
        /// </summary>
        public JSFunction JSFunctionOfCollectQuerConditons { get; set; }

        /// <summary>
        /// 报表的后台处理程序的URL
        /// </summary>
        public string AjaxHandlerUrl { get; set; }

        /// <summary>
        /// Ajax请求时，加载中的提示信息
        /// </summary>
        public string LoadingMessage { get; set; }

        /// <summary>
        /// 是否维度列要放在报表的首位
        /// <remarks>
        /// <para>默认为true</para>
        /// <para>如果为true,则其他列的DisplayOrder将乘以100,通过这种方式让维度列在表格的前部显示</para>
        /// </remarks>
        /// </summary>
        public bool IsDimColumnFirst { get; set; }
        #endregion

        #endregion

        #region IAnalysisReport 成员

        /// <summary>
        /// 分析报表实例的ID，该ID全局唯一
        /// </summary>
        public string ReportID { get; private set; }

        /// <summary>
        /// 当前的报表列
        /// </summary>
        public AnalysisColumnList CurrentColumns
        {
            get { return this.GetCurrentColumns(); }
        }

        /// <summary>
        /// 分析报表结果数据的过滤器
        /// </summary>
        public List<IDataFilter> ResultDataFilters { get; set; }

        /// <summary>
        /// 处理分析报表的查询
        /// </summary>
        /// <param name="pWheres">报表数据的过滤条件</param>
        /// <param name="pOrderBys">报表数据的排序条件</param>
        /// <param name="pLanguage">用户所选择的语言</param>
        /// <param name="pSaveCurrentCondtions">是否保存当前的条件</param>
        /// <returns>分析报表的输出</returns>
        public AnalysisReportOutput ProcessQuery(IWhereCondition[] pWheres, OrderBy[] pOrderBys, Languages pLanguage)
        {
            return this.ProcessQuery(pWheres, pOrderBys, pLanguage, true);
        }

        /// <summary>
        /// 处理分析报表的查询
        /// </summary>
        /// <param name="pWheres">报表数据的过滤条件</param>
        /// <param name="pOrderBys">报表数据的排序条件</param>
        /// <param name="pLanguage">用户所选择的语言</param>
        /// <param name="pResetCondition">是否重置当前的查询条件(pWheres,pOrderBys,pLanguage)</param>
        /// <returns>分析报表的输出</returns>
        protected AnalysisReportOutput ProcessQuery(IWhereCondition[] pWheres, OrderBy[] pOrderBys, Languages pLanguage,bool pResetCondition)
        {
            AnalysisReportOutput output = new AnalysisReportOutput();
            //是否重置当前的查询条件
            if (pResetCondition)
            {
                this.CurrentWhereConditions = pWheres;
                this.CurrentOrderBys = pOrderBys;
                this.CurrentLanguage = pLanguage;
            }
            //获取数据并执行分组计算,此步骤主要是读取基础数据并进行分组计算（从而计算出度量）
            //计算的方式和策略可能会有多种,例如：
            //NO.1.从数据库中读取所有明细数据，然后在内存中进行分组计算
            //NO.2.组织分组计算的SQL语句，丢给数据库进行执行
            DataTable dataes =null;
            List<IWhereCondition> newWheres = new List<IWhereCondition>();
            if (this.ConditionsByDrilling != null && this.ConditionsByDrilling.Count > 0)
            {
                newWheres.AddRange(pWheres);
                newWheres.AddRange(this.ConditionsByDrilling.SelectMany(item => item.Value).ToArray());
                dataes = this.ProcessGroupingCalculation(newWheres.ToArray(), pOrderBys);
            }
            else
            {
                dataes = this.ProcessGroupingCalculation(pWheres, pOrderBys);
            }
            //处理自定义数据源
            var customizeDataSources = this.GetCurrentColumns().CustomizeDataSources.Select(item => item.DataSource).Distinct().ToArray();
            if (customizeDataSources != null)
            {
                var activeDims =this.GetCurrentColumns().ActiveDims;
                foreach (var cds in customizeDataSources)
                {
                    //找到使用自定义数据源的自定义数据源列
                    var columns = this.GetCurrentColumns().CustomizeDataSources.Where(item => item.DataSource == cds).ToArray();
                    var dt = cds.ProcessGroupingCalculation(newWheres.ToArray(), activeDims, columns);
                    //合并自定义数据源
                    if (dt != null)
                    {
                        foreach (var analysisCol in columns)
                        {
                            var dataCol = dt.Columns[analysisCol.ColumnID];
                            dataes.Columns.Add(new DataColumn() { ColumnName =dataCol.ColumnName, DataType =dataCol.DataType });
                            for (var i = 0; i < dataes.Rows.Count; i++)
                            {//遍历源数据表的记录,将新列的值逐个补充上
                                var dr = dataes.Rows[i];
                                StringBuilder filters = new StringBuilder();
                                bool hasAppend = false;
                                foreach (var dim in activeDims)
                                {//组织两个Table中相同记录比对的条件
                                    var dimVal = dr[dim.ColumnID];
                                    if (dimVal != DBNull.Value)
                                    {
                                        if (hasAppend)
                                        {
                                            filters.AppendFormat(" and {0}='{1}'", StringUtils.WrapperSQLServerObject(dim.ColumnID), dimVal.ToString());
                                        }
                                        else
                                        {
                                            filters.AppendFormat(" {0}='{1}'", StringUtils.WrapperSQLServerObject(dim.ColumnID), dimVal.ToString());
                                        }
                                    }
                                }
                                if (filters.Length > 0)
                                {//在目的数据库中查找维度值相同的记录
                                    var targetRows = dt.Select(filters.ToString());
                                    if (targetRows != null)
                                    {
                                        if (targetRows.Length == 1)
                                        {
                                            dr[analysisCol.ColumnID] = targetRows[0][analysisCol.ColumnID];
                                        }
                                        else if (targetRows.Length > 1)
                                        {
                                            throw new Exception(string.Format("合并自定义数据源失败.被合并数据集中符合[{0}]条件的记录有多条.",filters));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //初始化维度项的文本
            this.InitDimText(dataes, pLanguage);
            //处理分析报表
            //1.行列转换
            //2.自定义计算列
            //3.摘要
            //etc..
            this.ProcessAnalysisReport(dataes, output);
            //返回结果
            return output;
        }

        /// <summary>
        /// 执行分析报表的钻入
        /// </summary>
        /// <param name="pDrilledColumn">用户选择钻入的维度列的列ID</param>
        /// <param name="pDimValue">用户选择钻入的维度项的ID值</param>
        /// <param name="pDimValue">用户选择钻入的维度项的文本值</param>
        /// <param name="pBringFromDrilling">从用户的钻取带入的其他查询条件</param>
        /// <returns></returns>
        public AnalysisReportOutput ProcessDrillIn(string pDrilledColumnID, string pDimValue, string pDimText, IWhereCondition[] pBringFromDrilling)
        {
            AnalysisReportOutput output = new AnalysisReportOutput();
            //
            var dimColumn = this.CurrentAnalysisMode.DrillRouting.CurrentSection.Columns.Where(item => item.ColumnID == pDrilledColumnID).FirstOrDefault();
            //钻入
            this.CurrentAnalysisMode.DrillRouting.DrillIn(dimColumn,pDimValue,pDimText);
            //获取钻入后的分组计算的结果
            var dataes = this.ProcessGroupingCalculationByDrilled(this.CurrentWhereConditions, this.CurrentOrderBys, (DimColumn)dimColumn, pDimValue, pBringFromDrilling);
            //初始化维度项的文本
            this.InitDimText(dataes, this.CurrentLanguage);
            //处理分析报表
            //1.行列转换
            //2.自定义计算列
            //3.摘要
            //etc..
            this.ProcessAnalysisReport(dataes, output);
            //返回结果
            return output;
        }

        /// <summary>
        /// 执行分析报表的跳转
        /// </summary>
        /// <param name="pGotoSectionID">用户选择的跳转到的钻取剖面的ID</param>
        /// <returns></returns>
        public AnalysisReportOutput ProcessGoto(string pGotoSectionID)
        {
            //跳转
            this.CurrentAnalysisMode.DrillRouting.Goto(pGotoSectionID,this.ConditionsByDrilling);
            //
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (this.CurrentWhereConditions != null && this.CurrentWhereConditions.Length > 0)
                wheres.AddRange(this.CurrentWhereConditions);
            if (this.ConditionsByDrilling != null && this.ConditionsByDrilling.Count > 0)
                wheres.AddRange(this.ConditionsByDrilling.SelectMany(item => item.Value).ToArray());
            //
            return this.ProcessQuery(wheres.ToArray(), this.CurrentOrderBys, this.CurrentLanguage,false);
        }

        /// <summary>
        /// 处理数据透视列变更
        /// </summary>
        /// <param name="pPivotChangedColumnID">被改变数据透视状态的维度列ID</param>
        /// <param name="pIsPivoted">改变后的状态为什么</param>
        /// <returns>报表的输出</returns>
        public AnalysisReportOutput ProcessPivotChanged(string pPivotChangedColumnID, bool pIsPivoted)
        {
            var columns = this.GetCurrentColumns();
            var dimCol = columns.Where(item => item.ColumnID == pPivotChangedColumnID).FirstOrDefault();
            ((DimColumn)dimCol).IsPivoted = pIsPivoted;
            //
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if(this.CurrentWhereConditions!=null && this.CurrentWhereConditions.Length>0)
                wheres.AddRange(this.CurrentWhereConditions);
            if(this.ConditionsByDrilling!=null && this.ConditionsByDrilling.Count>0)
                wheres.AddRange(this.ConditionsByDrilling.SelectMany(item=>item.Value).ToArray());
            //
            return this.ProcessQuery(wheres.ToArray(), this.CurrentOrderBys, this.CurrentLanguage, false);
        }

        /// <summary>
        /// 处理行列转换变更
        /// </summary>
        /// <param name="pCRConversionColumnID">进行行列转换的列</param>
        /// <param name="pIsCRConverted">改变后的状态</param>
        /// <returns>报表的输出</returns>
        public AnalysisReportOutput ProcessCRConversionChanged(string pCRConversionColumnID, bool pIsCRConverted)
        {
            var columns = this.GetCurrentColumns();
            var dimCol = columns.Where(item => item.ColumnID == pCRConversionColumnID).FirstOrDefault();
            ((DimColumn)dimCol).IsCRConverted = pIsCRConverted;
            //
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (this.CurrentWhereConditions != null && this.CurrentWhereConditions.Length > 0)
                wheres.AddRange(this.CurrentWhereConditions);
            if (this.ConditionsByDrilling != null && this.ConditionsByDrilling.Count > 0)
                wheres.AddRange(this.ConditionsByDrilling.SelectMany(item => item.Value).ToArray());
            //
            return this.ProcessQuery(wheres.ToArray(), this.CurrentOrderBys, this.CurrentLanguage, false);
        }

        /// <summary>
        /// 处理行列互换
        /// </summary>
        /// <returns></returns>
        public AnalysisReportOutput ProcessCRExchange()
        {
            //行列互换
            var dims = this.GetCurrentColumns().ActiveDims;
            if (dims != null && dims.Length > 0)
            {
                foreach (var dim in dims)
                {
                    if (dim.IsCRConversionable)
                    {
                        dim.IsCRConverted = !dim.IsCRConverted;
                    }
                }
            }
            //
            return this.ProcessQueryAgain();
        }

        /// <summary>
        /// 再次执行,注意，必须要已经执行过查询才能够调用该方法
        /// </summary>
        /// <returns>报表的输出</returns>
        public AnalysisReportOutput ProcessQueryAgain()
        {
            if (this.CurrentWhereConditions == null && this.CurrentOrderBys == null)
                throw new Exception("报表还未执行过,无法重新执行.");
            //
            List<IWhereCondition> wheres = new List<IWhereCondition>();
            if (this.CurrentWhereConditions != null && this.CurrentWhereConditions.Length > 0)
                wheres.AddRange(this.CurrentWhereConditions);
            if (this.ConditionsByDrilling != null && this.ConditionsByDrilling.Count > 0)
                wheres.AddRange(this.ConditionsByDrilling.SelectMany(item => item.Value).ToArray());
            //
            return this.ProcessQuery(wheres.ToArray(), this.CurrentOrderBys, this.CurrentLanguage, false);
        }
        #endregion

        #region 抽象成员

        /// <summary>
        /// 初始化报表的模型
        /// <remarks>
        /// <para>初始化的报表模型包含：</para>
        /// <para>1.FactDataModel定义了所取的基础数据的模型</para>
        /// <para>2.AnalysisModel定义了报表的分析模型</para>
        /// </remarks>
        /// </summary>
        public abstract void InitMode();

        /// <summary>
        /// 执行分组计算
        /// <remarks>
        /// <para>分组计算的主要工作内容有：</para>
        /// <para>1.读取事实数据.</para>
        /// <para>2.对事实数据进行分组,并依次进行度量的计算</para>
        /// </remarks>
        /// </summary>
        /// <param name="pWheres">报表数据的过滤条件</param>
        /// <param name="pOrderBys">报表数据的排序条件</param>
        /// <returns>分组聚合后的数据集,数据集中的所有列的列名为对应的事实数据列的ColumnID</returns>
        protected abstract DataTable ProcessGroupingCalculation(IWhereCondition[] pWheres, OrderBy[] pOrderBys);

        /// <summary>
        /// 执行钻取后的分组计算
        /// </summary>
        /// <param name="pWheres">报表数据的过滤条件</param>
        /// <param name="pOrderBys">报表数据的排序条件</param>
        /// <param name="pDrilledColumn">用户选择钻入的维度列</param>
        /// <param name="pDimValue">用户选择钻入的维度项的值</param>
        /// <param name="pBringFromDrilling">从用户的钻取带入的其他查询条件</param>
        /// <returns>分组聚合后的数据</returns>
        protected abstract DataTable ProcessGroupingCalculationByDrilled(IWhereCondition[] pWheres, OrderBy[] pOrderBys, DimColumn pDrilledColumn, string pDimValue, IWhereCondition[] pBringFromDrilling);

        /// <summary>
        /// 报表的类型
        /// </summary>
        public abstract AnalysisReportTypes ReportType { get; }
        #endregion

        #region 虚方法

        #region 获取唯一ID
        /// <summary>
        /// 分组的序号
        /// </summary>
        private Dictionary<string, int> _groupingIndex = new Dictionary<string, int>();
        /// <summary>
        /// 获取唯一ID
        /// </summary>
        /// <param name="pObjectType">
        /// 对象的类型
        /// <remarks>
        /// <para>值有：</para>
        /// <para>1.model   用于在创建Ext.data.Model对象时设置对象的ID</para>
        /// <para>2.field   用于store中column的dataindex</para>
        /// <para>3.store   用于在创建Ext.data.Store对象时设置对象的ID</para>
        /// <para>4.grid    用于在创建Ext.grid.Panel对象时设置对象的ID</para>
        /// <para>5.label   用于在创建Ext.form.Label对象时设置对象的ID</para>
        /// <para>5.menu    用于在创建Ext.menu.Menu对象时设置对象的ID</para>
        /// </remarks>
        /// </param>
        /// <returns></returns>
        protected virtual string GetUniqueID(string pObjectType)
        {
            if (pObjectType == null)
                pObjectType = string.Empty;
            pObjectType = pObjectType.Trim().ToLower();
            var index = 1;
            if (this._groupingIndex.ContainsKey(pObjectType))
            {
                index = this._groupingIndex[pObjectType];
            }
            else
            {
                this._groupingIndex.Add(pObjectType, index);
            }
            var result = string.Format("__{0}{1}", pObjectType, index++);
            this._groupingIndex[pObjectType] = index;
            //
            return result;
        }
        #endregion

        #region 获得摘要DIV的样式
        /// <summary>
        /// 获得摘要DIV的样式
        /// </summary>
        /// <returns></returns>
        protected virtual string GetSummaryDivAttributes()
        {
            return "style=\"padding:2px 0px 1px 0px;background-color:#e0e2e7;color:#4e5f74;\"";
        }
        #endregion

        #region 生成摘要的语句
        /// <summary>
        /// 生成摘要的语句
        /// </summary>
        /// <param name="pSummaryContents">摘要的内容,如果有多个摘要则内容会有多个</param>
        /// <param name="pSummaryDivAttributes">摘要的内容的样式,实现时使用div来包容摘要内容,如果该参数不为空,则会为相应的div上添加属性,通常这用于设置div的样式</param>
        /// <returns></returns>
        protected virtual IJavascriptObject CreateSummaryRenderer(string[] pSummaryContents, string pSummaryDivAttributes = "")
        {
            JavascriptBlock script = new JavascriptBlock();
            //生成脚本
            script.Sentences = new List<string>();
            script.Sentences.Add("function (value, summaryData, dataIndex) {");
            script.Sentences.Add(string.Format("{0}var html = '';", Keyboard.TAB));
            script.Sentences.Add(string.Format("{0}html += '<div>';", Keyboard.TAB));
            if (pSummaryContents != null)
            {
                foreach (var content in pSummaryContents)
                {
                    script.Sentences.Add(string.Format("{0}html += '<div {1}>{2}</div>';",Keyboard.TAB,pSummaryDivAttributes,string.IsNullOrWhiteSpace(content)?HTML.EscapeCharacters.Space:content));
                }
            }
            script.Sentences.Add(string.Format("{0}html += '</div>';", Keyboard.TAB));
            script.Sentences.Add(string.Format("{0}return html;", Keyboard.TAB));
            script.Sentences.Add("}");
            //返回
            return script;
        }
        #endregion

        #region 生成导航
        /// <summary>
        /// 生成导航
        /// </summary>
        /// <param name="pDrillingRoute">钻取的路径</param>
        /// <param name="pOutput">分析报表的输出</param>
        /// <returns></returns>
        protected virtual ExtJSComponent CreateNavigation(AnalysisReportDrilling[] pDrillingRoute,AnalysisReportOutput pOutput)
        {
            if (string.IsNullOrWhiteSpace(this.NaviID))
            {
                this.NaviID = this.GetUniqueID("label");
            }
            Label navi = new Label();
            navi.ID = this.NaviID;
            if (pDrillingRoute != null && pDrillingRoute.Length > 0)
            {
                StringBuilder html = new StringBuilder();
                StringBuilder naviText = new StringBuilder();
                for (var i = 0; i < pDrillingRoute.Length; i++)
                {
                    if (i != 0)
                    {
                        html.AppendFormat("{0}>>{0}", HTML.EscapeCharacters.Space);
                        naviText.AppendFormat("{0}>>{0}", Keyboard.SPACE);
                    }
                    else
                    {
                        html.AppendFormat("{0}{0}", HTML.EscapeCharacters.Space);
                        naviText.AppendFormat("{0}{0}", Keyboard.SPACE);
                    }
                    string text = pDrillingRoute[i].To.SectionName;
                    string sectionID = pDrillingRoute[i].To.SectionID;
                    if (!string.IsNullOrEmpty(pDrillingRoute[i].DrilledDimText))
                    {
                        text = string.Format("{0}({1})", text, pDrillingRoute[i].DrilledDimText);
                    }
                    html.AppendFormat("<a href=\"javascript:{0}('{1}');\">{2}</a>", this.JSFunctionOfGoto.FunctionName, sectionID, text);
                    naviText.Append(text);
                }
                navi.HTML = (JavascriptBlock)html;
                pOutput.NavigationText = naviText.ToString();
            }
            //
            return navi;
        }
        #endregion
        #endregion

        #region 工具方法

        #region 受保护

        #region 获取当前的所有的报表列
        /// <summary>
        /// 获取当前的所有的报表列
        /// </summary>
        /// <returns></returns>
        protected AnalysisColumnList GetCurrentColumns()
        {
            return this.CurrentAnalysisMode.DrillRouting.CurrentSection.Columns;
        }
        #endregion

        #region 获取当前所有的摘要
        /// <summary>
        /// 获取当前所有的摘要
        /// </summary>
        /// <returns></returns>
        protected List<ISummary> GetCurrentSummaries()
        {
            return this.CurrentAnalysisMode.DrillRouting.CurrentSection.Summeries;
        }
        #endregion

        #region 获取分组明细
        /// <summary>
        /// 获取分组明细
        /// </summary>
        /// <param name="pGroupResult"></param>
        /// <returns></returns>
        protected List<GroupingItem<TElement>> GetGroupingDetails<TElement>(GroupResult[] pGroupResult)
        {
            if (pGroupResult != null)
            {
                List<GroupingItem<TElement>> result = new List<GroupingItem<TElement>>();
                //递归获取子分组
                foreach (var g in pGroupResult)
                {
                    if (g.SubGroups != null)
                    {
                        var children = this.GetGroupingDetails<TElement>(g.SubGroups.ToArray());
                        if (children != null)
                        {
                            foreach (var child in children)
                            {
                                if (child.Keys == null)
                                    child.Keys = new List<object>();
                                child.Keys.Add(g.Key);
                            }
                            result.AddRange(children);
                        }
                    }
                    else
                    {
                        GroupingItem<TElement> item = new GroupingItem<TElement>();
                        item.Keys = new List<object>();
                        item.Keys.Add(g.Key);
                        item.Details = new List<TElement>();
                        item.Details.AddRange(g.Items.Cast<TElement>());
                        //
                        result.Add(item);
                    }
                }
                //返回结果
                return result;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #endregion

        #region 私有

        #region 处理报表
        /// <summary>
        /// 处理报表
        /// </summary>
        /// <param name="pDataes">分组计算后的数据(已经执行过维度文本的获取)</param>
        /// <param name="pOutput">分析报表的输出</param>
        private void ProcessAnalysisReport(DataTable pDataes, AnalysisReportOutput pOutput)
        {
            //初始化需要输出的JS函数
            this.InitOutputJSFunctions(pOutput);
            //初始化数据集中列与报表列的映射关系
            this.InitColumnMapping();
            //初始化分析报表输出的列信息，包含Model和表格列
            this.InitOutputColumns(pOutput);
            //执行行列转换
            var dataes = this.ProcessCRConversion(pDataes, pOutput);
            //将数据表列与分析列的映射关系放在Output中
            pOutput.DataTableColumnToAnalysisColumnMappings = this.ColumnMappings;
            //计算自定义计算列
            this.CalculateCustomizeCalculation(dataes);
            //对列数据进行排序
            this.OrderColumnData(ref dataes, pOutput);
            //计算摘要
            this.CalculateSummary(dataes, pOutput);
            //生成报表数据
            this.GenerateReportStore(dataes, pOutput);
            //生成报表的表格
            this.GenerateReportGridPanel(pOutput);
            //保存报表计算完毕后的结果数据集
            pOutput.OriginalReportDataes = dataes;
            pOutput.ReportDataes = dataes;
            //生成报表的顶部工具条
            this.GenerateReportGridTopToolbar(pOutput);
            //生成报表表格的上下文菜单
            this.GenerateReportGridContextMenu(pOutput);
            //处理报表结果数据过滤器
            if (this.ResultDataFilters != null && this.ResultDataFilters.Count > 0)
            {
                pOutput.ResultDataFilters = this.ResultDataFilters.ToArray();
                pOutput.ReportDataes = dataes.Copy();
            }
        }
        #endregion

        #region 初始化报表数据集中列与报表列的映射关系
        /// <summary>
        /// 初始化报表数据集中列与报表列的映射关系
        /// </summary>
        private void InitColumnMapping()
        {
            this.ColumnMappings = new Dictionary<string,AnalysisColumn>();
            var reportColumns = this.GetCurrentColumns();
            if (reportColumns != null && reportColumns.Count > 0)
            {
                foreach (var col in reportColumns)
                {
                    switch (col.ColumnType)
                    {
                        case AnalysisColumnTypes.Dim:
                            {
                                var temp =col as DimColumn;
                                this.ColumnMappings.Add(temp.ColumnID, col);
                                this.ColumnMappings.Add(temp.GetTextColumnID(), col);
                            }
                            break;
                        case AnalysisColumnTypes.MemoryMeasure:
                        case AnalysisColumnTypes.SQLMeasure:
                        case AnalysisColumnTypes.CustomizeCalcaulate:
                        case AnalysisColumnTypes.CustomizeDataSource:
                            {
                                this.ColumnMappings.Add(col.ColumnID ,col);
                            }
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
        }
        #endregion

        #region 初始化输出的JS函数
        /// <summary>
        /// 初始化输出的JS函数
        /// </summary>
        private void InitOutputJSFunctions(AnalysisReportOutput pOutput)
        {
            //处理输出的JS函数
            if (pOutput == null)
                pOutput = new AnalysisReportOutput();
            pOutput.JSFunctions = new Dictionary<string, JSFunction>();
            if (this.JSFunctionOfCollectQuerConditons != null)
            {
                this.JSFunctionOfCollectQuerConditons.Type = JSFunctionTypes.Common;
                if(string.IsNullOrWhiteSpace(this.JSFunctionOfCollectQuerConditons.FunctionName))
                {
                    this.JSFunctionOfCollectQuerConditons.FunctionName =string.Format("__fn{0}",Guid.NewGuid().ToText());
                }
                pOutput.JSFunctions.Add("CollectQueryCondition", this.JSFunctionOfCollectQuerConditons);
            }
            //报表查询的JS函数
            if (this.JSFunctionOfQuery == null)
            {
                var ajaxUrl = this.AppendQueryString(this.AjaxHandlerUrl, "op", ((int)Operations.Query).ToString());
                this.JSFunctionOfQuery = JSFunctionFactory.CreateQuery(this.JSFunctionOfCollectQuerConditons, ajaxUrl, pLoadingMessage: this.LoadingMessage);
                this.JSFunctionOfQuery.Type = JSFunctionTypes.Common;
            }
            pOutput.JSFunctions.Add("Query", this.JSFunctionOfQuery);
            //报表跳转到指定地图剖面的JS函数
            if (this.JSFunctionOfGoto == null)
            {
                var ajaxUrl = this.AppendQueryString(this.AjaxHandlerUrl, "op", ((int)Operations.Goto).ToString());
                this.JSFunctionOfGoto = JSFunctionFactory.CreateGoto(ajaxUrl, pLoadingMessage: this.LoadingMessage);
                this.JSFunctionOfGoto.Type = JSFunctionTypes.Common;
            }
            pOutput.JSFunctions.Add("Goto", this.JSFunctionOfGoto);
            //报表钻取的JS函数
            if (this.JSFunctionOfDrilling == null)
            {
                var ajaxUrl = this.AppendQueryString(this.AjaxHandlerUrl, "op", ((int)Operations.DrillIn).ToString());
                this.JSFunctionOfDrilling = JSFunctionFactory.CreateDrilling(ajaxUrl, pLoadingMessage: this.LoadingMessage);
                this.JSFunctionOfDrilling.Type = JSFunctionTypes.Common;
            }
            pOutput.JSFunctions.Add("DrillIn", this.JSFunctionOfDrilling);
            //报表多维度钻取的JS函数
            if (this.JSFunctionOfMultiDimDrill == null)
            {
                var ajaxUrl = this.AppendQueryString(this.AjaxHandlerUrl, "op", ((int)Operations.ViewDetail).ToString());
                this.JSFunctionOfMultiDimDrill = JSFunctionFactory.CreateMultiDimDrillingFunction(ajaxUrl, pLoadingMessage: this.LoadingMessage);
                this.JSFunctionOfMultiDimDrill.Type = JSFunctionTypes.Common;
            }
            pOutput.JSFunctions.Add("MultiDimDrilling", this.JSFunctionOfMultiDimDrill);
        }
        #endregion

        #region 在URL中增加QueryString项
        /// <summary>
        /// 在URL中增加QueryString项
        /// </summary>
        /// <param name="pUrl">初始的URL</param>
        /// <param name="pKey">QueryString项的键</param>
        /// <param name="pValue">QueryString项的值</param>
        /// <returns></returns>
        private string AppendQueryString(string pUrl, string pKey, string pValue)
        {
            if (!string.IsNullOrWhiteSpace(pUrl))
            {
                if (pUrl.IndexOf("?") > 0)
                {
                    return string.Format("{0}&{1}={2}", pUrl, pKey, pValue);
                }
                else
                {
                    return string.Format("{0}?{1}={2}",pUrl,pKey,pValue);
                }
            }
            return string.Empty;
        }
        #endregion

        #region 初始化维度文本列
        /// <summary>
        /// 初始化维度文本列
        /// </summary>
        /// <param name="pDataes">分组计算后的数据集</param>
        /// <param name="pLanguage">用户所选择的语言</param>
        private void InitDimText(DataTable pDataes,Languages pLanguage)
        {
            var dims = this.GetCurrentColumns().ActiveDims;
            if (dims != null && dims.Length > 0)
            {
                foreach (var dim in dims)
                {
                    //新增一列维度文本列
                    var columnName = dim.GetTextColumnID();
                    if (!pDataes.Columns.Contains(columnName))
                    {
                        pDataes.Columns.Add(columnName, dim.GridColumnType.GetDotNetType());
                    }
                    //获取所有维度项的文本
                    var rows = pDataes.AsEnumerable();
                    var q = from m in rows
                            where m[dim.ColumnID] != null && m[dim.ColumnID] != DBNull.Value
                            select m[dim.ColumnID].ToString();
                    var ids = q.Distinct().ToArray();
                    var texts = dim.DataColumn.RelatedDim.GetTexts(ids, pLanguage);
                    //设置所有维度项的文本值
                    if (texts != null && texts.Count > 0)
                    {
                        foreach (DataRow dr in pDataes.Rows)
                        {
                            var key = dr[dim.ColumnID];
                            if (key != null && key != DBNull.Value)
                            {
                                var keyText = key.ToString();
                                var q4Text = from t in texts
                                             where t.Key == keyText
                                             select t.Value;
                                dr[dim.GetTextColumnID()] = q4Text.FirstOrDefault();
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 初始化分析报表输出的列信息，包含Model和表格列
        /// <summary>
        /// 初始化分析报表输出的列信息，包含Model和表格列
        /// </summary>
        /// <param name="pOutput">分析报表的输出</param>
        private void InitOutputColumns(AnalysisReportOutput pOutput)
        {
            if (pOutput == null)
                pOutput = new AnalysisReportOutput();
            var analysisColumns = this.GetCurrentColumns().ActiveColumns;
            if (analysisColumns != null)
            {
                var orderedColumns = analysisColumns.OrderBy(item => {
                    if ((!this.IsDimColumnFirst)|| item.ColumnType == AnalysisColumnTypes.Dim)
                    {//如果无须进行维度列排前或者列本身就是维度列
                        return item.DisplayOrder;
                    }
                    else
                    {
                        return item.DisplayOrder * 100;
                    }
                }).ToArray();
                //
                pOutput.DataModel = new Model();
                pOutput.DataModel.ClassFullName = this.GetUniqueID("model");
                pOutput.DataModel.Fields = new List<Field>();
                pOutput.Columns = new List<Column>();
                pOutput.GridColumnToAnalysisColumnMappings = new Dictionary<Column, AnalysisColumn>();
                foreach (var col in orderedColumns)
                {
                    switch (col.ColumnType)
                    {
                        case AnalysisColumnTypes.Dim:
                            {
                                //维度值列
                                pOutput.DataModel.Fields.Add(new Field() { Name = col.ColumnID, Type = col.GridColumnType.GetJavascriptType() });
                                var gridCol = new Column() { ID = this.GetGridColumnIDByDataIndex(col.ColumnID), ColumnTitle = col.ColumnTitle, IsHidden = true, ColumnType = col.GridColumnType, DataIndex = col.ColumnID, Width = 0, DisplayOrder = col.DisplayOrder };
                                pOutput.Columns.Add(gridCol);
                                pOutput.GridColumnToAnalysisColumnMappings.Add(gridCol, col);
                                //维度文本列
                                var dimTextColumnID = ((DimColumn)col).GetTextColumnID();
                                pOutput.DataModel.Fields.Add(new Field() { Name = dimTextColumnID, Type = ColumnTypes.String.GetJavascriptType() });
                                var dimTextColumn = new Column() { ID = this.GetGridColumnIDByDataIndex(dimTextColumnID), ColumnTitle = col.ColumnTitle, IsHidden = col.IsHidden, ColumnType = col.GridColumnType, DataIndex = dimTextColumnID, Width = col.ColumnWidth, DisplayOrder = col.DisplayOrder };
                                if (this.CurrentAnalysisMode.DrillRouting.CanDrillIn(col) && ((DimColumn)col).IsDrillable ==true)
                                {
                                    var dimCol = (DimColumn)col;
                                    switch (dimCol.DrillingType)
                                    {
                                        case DrillingTypes.DrillSelf:
                                            {//钻入时带入自身为筛选条件
                                                dimTextColumn.Renderer = JSFunctionFactory.CreateDimColumnRenderer(dimCol, this.JSFunctionOfDrilling.FunctionName); //维度列的呈现函数
                                            }
                                            break;
                                        case DrillingTypes.DrillAllDim:
                                            {//以查看明细的方式钻取
                                                var otherCol = this.GetCurrentColumns().ActiveDims.Where(item => item.ColumnID != col.ColumnID).ToArray();
                                                dimTextColumn.Renderer = JSFunctionFactory.CreateDimColumnRenderer(dimCol, otherCol);
                                            }
                                            break;
                                        case DrillingTypes.Customize:
                                            {//自定义方式
                                                DimColumn[] otherCol = null;
                                                if (dimCol.BringDimsToFilter != null)
                                                {
                                                    otherCol = dimCol.BringDimsToFilter.ToArray();
                                                }
                                                else
                                                {
                                                    otherCol = new DimColumn[0];
                                                }
                                                dimTextColumn.Renderer = JSFunctionFactory.CreateDimColumnRenderer(dimCol, otherCol);
                                            }
                                            break;
                                        default:
                                            throw new NotImplementedException();
                                    }
                                }
                                else
                                {
                                    dimTextColumn.ColumnType = ColumnTypes.String;
                                }
                                pOutput.Columns.Add(dimTextColumn);
                                pOutput.GridColumnToAnalysisColumnMappings.Add(dimTextColumn, col);
                            }
                            break;
                        case AnalysisColumnTypes.MemoryMeasure:
                        case AnalysisColumnTypes.SQLMeasure:
                        case AnalysisColumnTypes.CustomizeCalcaulate:
                        case AnalysisColumnTypes.CustomizeDataSource:
                            {
                                //度量列&自定义计算列
                                pOutput.DataModel.Fields.Add(new Field() { Name = col.ColumnID, Type = col.GridColumnType.GetJavascriptType() });
                                var gridCol = new Column() { ID = this.GetGridColumnIDByDataIndex(col.ColumnID), ColumnTitle = col.ColumnTitle, IsHidden = col.IsHidden, ColumnType = col.GridColumnType, DataIndex = col.ColumnID, Width = col.ColumnWidth, DisplayOrder = col.DisplayOrder };
                                if (col.GridColumnType == ColumnTypes.Percent)
                                {//如果表格列为百分比列
                                    gridCol.SetInitConfigValue("accuracy", col.GridColumnValueAccuracy);
                                }
                                if (col.Renderer != null)
                                {
                                    gridCol.Renderer = col.Renderer;
                                }
                                pOutput.Columns.Add(gridCol);
                                pOutput.GridColumnToAnalysisColumnMappings.Add(gridCol, col);
                            }
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
        }
        #endregion

        #region 根据DataIndex生成表格列的ID
        /// <summary>
        /// 根据DataIndex生成表格列的ID
        /// </summary>
        /// <param name="pDataIndex">列的数据索引</param>
        /// <returns></returns>
        private string GetGridColumnIDByDataIndex(string pDataIndex)
        {
            return string.Format("__ctl__gcl__{0}",pDataIndex);
        }
        #endregion

        #region 执行行列转换
        /// <summary>
        /// 执行行列转换
        /// <remarks>
        /// <para>1.支持多个维度进行行列转换.</para>
        /// </remarks>
        /// </summary>
        /// <param name="pDataes">执行完分组聚合运算后的结果集</param>
        /// <param name="pOutput">分析报表的输出</param>
        private DataTable ProcessCRConversion(DataTable pDataes,AnalysisReportOutput pOutput)
        {
            var crConvertedColumns = this.GetCurrentColumns().CRConvertedDims;
            var notCRConvertedColumns = this.GetCurrentColumns().NotCRConvertedDims;
            AnalysisColumn[] measures = null;
            switch (this.ReportType)
            {
                case AnalysisReportTypes.MemoryBased:
                    measures = this.GetCurrentColumns().MemoryMeasures;
                    break;
                case AnalysisReportTypes.SQLBased:
                    {
                        List<AnalysisColumn> list = new List<AnalysisColumn>();
                        list.AddRange(this.GetCurrentColumns().SQLMeasures);
                        list.AddRange(this.GetCurrentColumns().CustomizeDataSources);
                        measures = list.ToArray();
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (crConvertedColumns != null && crConvertedColumns.Length > 0)
            {
                //重新初始化表格列
                pOutput.DataModel = new Model();
                pOutput.DataModel.ClassFullName = this.GetUniqueID("model");
                pOutput.DataModel.Fields = new List<Field>();
                pOutput.Columns = new List<Column>();
                pOutput.GridColumnToAnalysisColumnMappings = new Dictionary<Column, AnalysisColumn>();
                //重新初始化报表列与数据集列的映射关系
                this.ColumnMappings = new Dictionary<string, AnalysisColumn>();
                var calColumns = this.GetCurrentColumns().CustomizeCalcaulates;
                if (calColumns != null)
                {
                    foreach (var calCol in calColumns)
                    {
                        this.ColumnMappings.Add(calCol.ColumnID ,calCol);
                        //自定义计算列
                        pOutput.DataModel.Fields.Add(new Field() { Name = calCol.ColumnID, Type = calCol.GridColumnType.GetJavascriptType() });
                        var gridCol = new Column() { ID = this.GetGridColumnIDByDataIndex(calCol.ColumnID), ColumnTitle = calCol.ColumnTitle, IsHidden = calCol.IsHidden, ColumnType = calCol.GridColumnType, DataIndex = calCol.ColumnID, Width = calCol.ColumnWidth, DisplayOrder = calCol.DisplayOrder * 100 };
                        if (!this.IsDimColumnFirst)
                        {//如果不是维度列优先,则列顺序不乘以100的系数
                            gridCol.DisplayOrder = calCol.DisplayOrder;
                        }
                        if (calCol.GridColumnType == ColumnTypes.Percent)
                        {//如果表格列类型为百分比列
                            gridCol.SetInitConfigValue("accuracy", calCol.GridColumnValueAccuracy);
                        }
                        if (calCol.Renderer != null)
                        {
                            gridCol.Renderer = calCol.Renderer;
                        }
                        pOutput.Columns.Add(gridCol);
                        pOutput.GridColumnToAnalysisColumnMappings.Add(gridCol, calCol);
                    }
                }
                //
                var rows = pDataes.AsEnumerable();
                //将数据按未进行行列转换的维度列进行分组
                List<GroupingItem<DataRow>> groupDetails = null;
                if (notCRConvertedColumns != null && notCRConvertedColumns.Length > 0)
                {
                    //组织分组条件
                    List<Func<DataRow, object>> groupings = new List<Func<DataRow, object>>();
                    foreach (var dim in notCRConvertedColumns)
                    {
                        var columnName = dim.ColumnID;
                        groupings.Add(item =>
                        {
                            return item[columnName];
                        });
                    }
                    //对数据进行分组并获取分组明细
                    var grouped = rows.GroupByMany(groupings.ToArray()).ToArray();
                    groupDetails = this.GetGroupingDetails<DataRow>(grouped);
                }
                else
                {
                    groupDetails = new List<GroupingItem<DataRow>>();
                    var groupingItem = new GroupingItem<DataRow>();
                    groupingItem.Details = new List<DataRow>();
                    groupingItem.Details.AddRange(rows);
                    groupDetails.Add(groupingItem);
                }
                DataTable dt = new DataTable();
                //处理不进行行列转换的维度列
                foreach (var col in notCRConvertedColumns)  
                {
                    //数据集中创建列
                    dt.Columns.Add(col.ColumnID, col.GridColumnType.GetDotNetType());
                    dt.Columns.Add(col.GetTextColumnID(),typeof(string));
                    //将维度值列添加入报表表格的数据模型和结果列集中
                    pOutput.DataModel.Fields.Add(new Field() { Name = col.ColumnID, Type = col.GridColumnType.GetJavascriptType() });
                    var gridCol = new Column() { ID = this.GetGridColumnIDByDataIndex(col.ColumnID), ColumnTitle = col.ColumnTitle, IsHidden = true, ColumnType = col.GridColumnType, DataIndex = col.ColumnID, Width = 0, DisplayOrder = col.DisplayOrder };
                    pOutput.Columns.Add(gridCol);
                    pOutput.GridColumnToAnalysisColumnMappings.Add(gridCol, col);
                    //将维度文本列添加入报表表格的数据模型和结果列集中
                    pOutput.DataModel.Fields.Add(new Field() { Name = col.GetTextColumnID(), Type = ColumnTypes.String.GetJavascriptType() });
                    var dimTextColumn = new Column() { ID = this.GetGridColumnIDByDataIndex(col.GetTextColumnID()), ColumnTitle = col.ColumnTitle, IsHidden = col.IsHidden, ColumnType = col.GridColumnType, DataIndex = col.GetTextColumnID(), Width = col.ColumnWidth, DisplayOrder = col.DisplayOrder  };
                    if (this.CurrentAnalysisMode.DrillRouting.CanDrillIn(col) && col.IsDrillable == true)
                    {
                        switch (col.DrillingType)
                        {
                            case DrillingTypes.DrillSelf:
                                {//钻入时带入自身为筛选条件
                                    dimTextColumn.Renderer = JSFunctionFactory.CreateDimColumnRenderer(col, this.JSFunctionOfDrilling.FunctionName); //维度列的呈现函数
                                }
                                break;
                            case DrillingTypes.DrillAllDim:
                                {//以查看明细的方式钻取
                                    var otherCol = notCRConvertedColumns.Where(item => item.ColumnID != col.ColumnID).ToArray();
                                    dimTextColumn.Renderer = JSFunctionFactory.CreateDimColumnRenderer(col, otherCol);
                                }
                                break;
                            case DrillingTypes.Customize:
                                {//自定义方式
                                    List<DimColumn> otherCol = new List<DimColumn>();
                                    if (col.BringDimsToFilter != null)
                                    {
                                        foreach (var item in col.BringDimsToFilter)
                                        {
                                            if (notCRConvertedColumns.Contains(item))
                                            {
                                                otherCol.Add(item);
                                            }
                                        }
                                    }
                                    dimTextColumn.Renderer = JSFunctionFactory.CreateDimColumnRenderer(col, otherCol.ToArray());
                                }
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }
                    else
                    {
                        dimTextColumn.ColumnType = ColumnTypes.String;
                    }
                    pOutput.Columns.Add(dimTextColumn);
                    pOutput.GridColumnToAnalysisColumnMappings.Add(dimTextColumn, col);
                    //设置数据集列与报表列的映射关系
                    this.ColumnMappings.Add(col.ColumnID,col);
                    this.ColumnMappings.Add(col.GetTextColumnID(), col);
                }
                //处理进行行列转换后的维度列和度量列
                List<dynamic> gridColumns = new List<dynamic>();    //剩下的所有的表格列
                foreach (var item in groupDetails)
                {
                    if (item.Details != null && item.Details.Count > 0)
                    {
                        var dr = dt.NewRow();
                        //处理维度列的值
                        foreach (var col in notCRConvertedColumns)
                        {
                            dr[col.ColumnID] = item.Details[0][col.ColumnID];
                            dr[col.GetTextColumnID()] = item.Details[0][col.GetTextColumnID()];
                        }
                        //处理行列转换列
                        dynamic gridColumnItem =new ExpandoObject();
                        foreach (var detail in item.Details)
                        {
                            var columnID = string.Empty;
                            Column gridColumn = null;
                            foreach (var col in crConvertedColumns)
                            {
                                var dimValue = string.Empty;
                                var dimID = string.Empty;
                                if (detail[col.ColumnID] != DBNull.Value)
                                {
                                    dimID = Convert.ToString(detail[col.ColumnID]);
                                }
                                if (detail[col.GetTextColumnID()] != DBNull.Value)
                                {
                                    dimValue = Convert.ToString(detail[col.GetTextColumnID()]);
                                }
                                columnID += "_" + col.ColumnID + "_" + dimID;
                                //生成相应的表格列的结构
                                if (gridColumn == null)
                                {
                                    gridColumn = new Column();
                                    gridColumn.ColumnTitle = dimValue;
                                    gridColumn.DisplayOrder = col.DisplayOrder;
                                    gridColumn.Tags = col.ColumnID + "_" + dimID;
                                    pOutput.GridColumnToAnalysisColumnMappings.Add(gridColumn, col);
                                    //
                                    gridColumnItem.GroupKey = columnID;             //GroupKey用于比较列是否是同一个列组
                                    gridColumnItem.GroupColumn = gridColumn;        //表格列组的具体内容
                                }
                                else
                                {
                                    //
                                    var child = new Column();
                                    gridColumn.Children = new List<Column>();
                                    gridColumn.Children.Add(child);
                                    gridColumn = child;
                                    pOutput.GridColumnToAnalysisColumnMappings.Add(child, col);
                                    //
                                    gridColumnItem.DetailColumn = gridColumn;       //列组的最底层
                                    gridColumn.ColumnTitle = dimValue;
                                    gridColumn.Tags = col.ColumnID + "_" + dimID;
                                    
                                }
                            }
                            foreach (var m in measures)
                            {
                                dynamic currentGroupColumn = null;
                                var groupKey = gridColumnItem.GroupKey;
                                //如果没有列，则补充列
                                bool isFindGroup = false;
                                bool isFindMeasureColumn = false;
                                Column measureColumn = null;
                                //查找是否已经有了列组
                                foreach (var pair in gridColumns)
                                {
                                    if (pair.GroupKey == groupKey)
                                    {
                                        isFindGroup = true;
                                        this.MergeColumnChildren(pair.GroupColumn,gridColumnItem.GroupColumn);
                                        currentGroupColumn = pair;
                                        break;
                                    }
                                }
                                if (!isFindGroup)
                                {
                                    //如果未找到已有的列组，则当前的为新的列组
                                    currentGroupColumn = new ExpandoObject();
                                    currentGroupColumn.GroupKey = gridColumnItem.GroupKey;
                                    currentGroupColumn.GroupColumn = gridColumnItem.GroupColumn;
                                    currentGroupColumn.LeafColumns = new List<Column>();
                                    gridColumns.Add(currentGroupColumn);
                                }
                                //查找列组下是否有相应度量子列
                                if (currentGroupColumn.LeafColumns != null && currentGroupColumn.LeafColumns.Count > 0)
                                {
                                    var currentTags = gridColumn.Tags.ToString() + "_" + m.ColumnID;
                                    foreach (var leaf in currentGroupColumn.LeafColumns)
                                    {
                                        if (leaf.Tags == currentTags)
                                        {
                                            isFindMeasureColumn = true;
                                            measureColumn = leaf;
                                            break;
                                        }
                                    }
                                }
                                if (!isFindMeasureColumn)
                                {
                                    //如果未找到度量列，则创建一个新的度量列并加入到列组内
                                    var measureColumnDataIndex = this.GetUniqueID("field");
                                    measureColumn = new Column();
                                    measureColumn.ID = this.GetGridColumnIDByDataIndex(measureColumnDataIndex);
                                    measureColumn.Tags = gridColumn.Tags.ToString()+"_"+m.ColumnID;    //用于判断是否同一个度量列
                                    measureColumn.ColumnTitle = m.ColumnTitle;
                                    measureColumn.ColumnType = m.GridColumnType;
                                    measureColumn.IsHidden = m.IsHidden;
                                    measureColumn.DataIndex = measureColumnDataIndex;
                                    measureColumn.Width = m.ColumnWidth;
                                    measureColumn.DisplayOrder = m.DisplayOrder;
                                    if (m.GridColumnType == ColumnTypes.Percent)
                                    {
                                        measureColumn.SetInitConfigValue("accuracy", m.GridColumnValueAccuracy);
                                    }
                                    if (m.Renderer != null)
                                    {
                                        measureColumn.Renderer = m.Renderer;
                                    }
                                    pOutput.GridColumnToAnalysisColumnMappings.Add(measureColumn, m);
                                    //
                                    currentGroupColumn.LeafColumns.Add(measureColumn);
                                    var parent = this.FindColumnByTags(currentGroupColumn.GroupColumn, gridColumn.Tags.ToString());
                                    if (parent.Children == null)
                                        parent.Children = new List<Column>();
                                    parent.Children.Add(measureColumn);
                                    //在数据集中创建相应的列
                                    dt.Columns.Add(measureColumn.DataIndex, m.GridColumnType.GetDotNetType());
                                    //在输出的数据模型增加字段
                                    pOutput.DataModel.Fields.Add(new Field() { Name = measureColumn.DataIndex, Type = m.GridColumnType.GetJavascriptType() });
                                    //设置报表列与数据集列的映射关系
                                    this.ColumnMappings.Add(measureColumn.DataIndex, m);
                                }
                                //更新度量列的值
                                dr[measureColumn.DataIndex] = detail[m.ColumnID];
                            }
                        }
                        //
                        dt.Rows.Add(dr);
                    }
                }
                //
                foreach (var col in gridColumns)
                {
                    var currentGridColumn = col.GroupColumn;
                    if (this.IsDimColumnFirst)
                    {
                        currentGridColumn.DisplayOrder = currentGridColumn.DisplayOrder * 100;
                    }
                    else
                    {
                        currentGridColumn.DisplayOrder = currentGridColumn.DisplayOrder;
                    }
                    pOutput.Columns.Add(currentGridColumn);
                }
                //对输出列进行排序
                pOutput.Columns = pOutput.Columns.OrderBy(item => item.DisplayOrder).ThenBy(item => item.ColumnTitle).ToList();
                //
                dt.AcceptChanges();
                //
                return dt;
            }
            else
            {
                return pDataes;
            }
        }
        #endregion

        #region 列的子列合并
        /// <summary>
        /// 将pColumn2的子列合并到pColumn1的子列中
        /// </summary>
        /// <param name="pColumn1"></param>
        /// <param name="pColumn2"></param>
        private void MergeColumnChildren(Column pColumn1, Column pColumn2)
        {
            if (pColumn1 != null && pColumn2 != null)
            {
                if (pColumn2.Children != null)
                {
                    if (pColumn1.Children == null)
                        pColumn1.Children = pColumn2.Children;
                    else
                    {
                        for (var i = 0; i < pColumn2.Children.Count; i++)
                        {
                            var col2 = pColumn2.Children[i];
                            bool isFind = false;
                            for (var j = 0; j < pColumn1.Children.Count; j++)
                            {
                                var col1 = pColumn1.Children[j];
                                if (col1.Tags != null && col2.Tags != null && col1.Tags.ToString() == col2.Tags.ToString())
                                {
                                    isFind = true;
                                    this.MergeColumnChildren(col1, col2);
                                    break;
                                }
                            }
                            if (!isFind)
                            {
                                pColumn1.Children.Add(col2);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 根据标签找到相应的列

        /// <summary>
        /// 根据标签找到相应的列
        /// </summary>
        /// <param name="pColumn"></param>
        /// <param name="pTags"></param>
        private Column FindColumnByTags(Column pColumn, string pTags)
        {
            if (pColumn == null)
                return null;
            //查找自身
            if (pColumn.Tags != null && pColumn.Tags.ToString() == pTags)
            {
                return pColumn;
            }
            //查找子列
            if (pColumn.Children != null && pColumn.Children.Count > 0)
            {
                foreach (var child in pColumn.Children)
                {
                    var found = this.FindColumnByTags(child, pTags);
                    if (found != null)
                        return found;
                }
            }
            //都没找到则返回null
            return null;
        }
        #endregion 

        #region 计算自定义计算列的值
        /// <summary>
        /// 计算自定义计算列的值
        /// </summary>
        private void CalculateCustomizeCalculation(DataTable pDataes)
        {
            var calColumns = this.GetCurrentColumns().CustomizeCalcaulates;
            if (calColumns != null && calColumns.Length > 0
                && pDataes !=null && pDataes.Rows.Count>0)
            {
                //对自定义计算列进行计算排序
                var orderedColumns = calColumns.OrderBy(item => item.CalculationOrder).ToArray();
                //在数据集中创建列
                foreach (var col in orderedColumns)
                {
                    var columnName = col.ColumnID;
                    if (!pDataes.Columns.Contains(columnName))
                    {
                        pDataes.Columns.Add(columnName, typeof(string));
                    }
                }
                //执行自定义计算
                CustomizeCalculationDataRetriever dataRetriever = new CustomizeCalculationDataRetriever(pDataes, this.ColumnMappings);
                foreach (var col in orderedColumns)
                {
                    for (var i = 0; i < pDataes.Rows.Count; i++)
                    {
                        var calculatedVal = col.Calculator(dataRetriever,i);
                        pDataes.Rows[i][col.ColumnID] = calculatedVal;
                    }
                }
            }
        }
        #endregion

        #region 对列数据进行排序
        /// <summary>
        /// 对列数据进行排序
        /// </summary>
        /// <param name="pDataes"></param>
        /// <param name="pOutput"></param>
        private void OrderColumnData(ref DataTable pDataes, AnalysisReportOutput pOutput)
        {
            var columns = this.GetCurrentColumns().VisiableColumns;
            if (columns != null && columns.Length > 0)
            {
                //组织排序数组
                List<string> sorters = new List<string>();
                foreach (var col in columns)
                {
                    if (col.DataOrderBy.HasValue)
                    {
                        switch (col.ColumnType)
                        {
                            case AnalysisColumnTypes.Dim:
                                {
                                    var dimCol = col as DimColumn;
                                    sorters.Add(string.Format("{0} {1}",dimCol.GetTextColumnID(), dimCol.DataOrderBy.Value== OrderByDirections.Asc?"ASC":"DESC"));
                                }
                                break;
                            default:
                                {
                                    List<string> dataColumns = new List<string>();
                                    foreach (var item in this.ColumnMappings)
                                    {
                                        if (item.Value == col)
                                            dataColumns.Add(item.Key);
                                    }
                                    if (dataColumns != null && dataColumns.Count > 0)
                                    {
                                        foreach (var item in dataColumns)
                                        {
                                            sorters.Add(string.Format("{0} {1}", item, col.DataOrderBy.Value == OrderByDirections.Asc ? "ASC" : "DESC"));
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
                //
                var sortExpression = sorters.ToArray().ToJoinString(",");
                pDataes.DefaultView.Sort = sortExpression;
                pDataes = pDataes.DefaultView.ToTable();
            }
        }
        #endregion

        #region 计算摘要
        /// <summary>
        /// 计算摘要
        /// </summary>
        /// <param name="pDataes">数据</param>
        /// <param name="pOutput">分析报表的输出</param>
        /// <returns>是否有摘要</returns>
        private void CalculateSummary(DataTable pDataes,AnalysisReportOutput pOutput)
        {
            var summaries = this.GetCurrentSummaries();
            pOutput.SummaryRenderers = new Dictionary<Column, IJavascriptObject>(); //重置摘要合计
            pOutput.ColumnSummaryValues = new Dictionary<Column, Dictionary<ISummary, string>>(); //重置列的摘要的值
            if (summaries != null && summaries.Count > 0)
            {
                var orderedSummaries = summaries.OrderBy(item => item.DisplayOrder).ToArray();
                var columnSummeryValues = new Dictionary<Column, string>();
                var rdt = new ReportDataRetriever(); ;
                rdt.Dataes = pDataes;
                rdt.ColumnSummaryValues = pOutput.ColumnSummaryValues;
                rdt.GridColumnToAnalysisColumnMappings = pOutput.GridColumnToAnalysisColumnMappings;
                
                #region 计算摘要结果
                var tableDatas = pDataes.AsEnumerable();
                Dictionary<string, string[]> results = new Dictionary<string, string[]>();
                foreach (DataColumn col in pDataes.Columns)
                {
                    var colName = col.ColumnName;
                    var gridCol = this.FindGridColumnByID(this.GetGridColumnIDByDataIndex(col.ColumnName), pOutput);
                    var reportCol = this.ColumnMappings[colName];       //DataTable列对应的报表列
                    object[] dataes = null;                             //DataTable的列数据
                    string[] contents = new string[summaries.Count];    //摘要内容,摘要的数据按数据列来组织
                    bool hasSummary = false;                            //该列是否要计算摘要
                    var summeryValues = new Dictionary<ISummary, string>();
                    for (var i = 0; i < orderedSummaries.Length; i++)
                    {
                        var summary = orderedSummaries[i];
                        //摘要计算
                        if (summary.IsNeedCalculate(reportCol))
                        {
                            switch (summary.CalculationType)
                            {
                                case SummaryCalculationTypes.CalculateSelf:
                                    {
                                        if (dataes == null)
                                            dataes = tableDatas.Select(item => item[col]).ToArray();
                                        //
                                        contents[i] = summary.CalculateSummary(reportCol, dataes);
                                    }
                                    break;
                                case SummaryCalculationTypes.CalculateFromAnyColumn:
                                    {
                                        contents[i] = summary.CalculateSummary(reportCol,gridCol, rdt);
                                    }
                                    break;
                                default:
                                    throw new NotImplementedException();
                            }
                            summeryValues.Add(summary, contents[i]);
                            hasSummary = true;
                        }
                        else
                        {
                            summeryValues.Add(summary, string.Empty);   //补空值
                        }
                    }
                    if (hasSummary)
                    {
                        results.Add(col.ColumnName, contents.ToArray());
                        //var gridCol = this.FindGridColumnByID(this.GetGridColumnIDByDataIndex(col.ColumnName), pOutput);
                        pOutput.ColumnSummaryValues.Add(gridCol, summeryValues);
                    }
                }
                #endregion 

                #region 生成SummaryRenderer
                var summaryStyles = this.GetSummaryDivAttributes();
                if (results != null && results.Count > 0)
                {
                    foreach (var result in results)
                    {
                        var currentCol = this.FindGridColumnByID(this.GetGridColumnIDByDataIndex(result.Key),pOutput);
                        if (currentCol != null)
                        {
                            var renderer = this.CreateSummaryRenderer(result.Value, summaryStyles);
                            currentCol.SummaryRenderer = renderer;
                            pOutput.SummaryRenderers.Add(currentCol, renderer);
                        }
                    }
                }
                #endregion

                #region 生成合计标头
                if (pOutput.SummaryRenderers != null && pOutput.SummaryRenderers.Count > 0)
                {
                    var firstCol = Column.FindFirstVisiableColumn(pOutput.Columns);
                    if (firstCol.SummaryRenderer != null)
                    {
                        //重新生成摘要
                        var dataIndex = firstCol.DataIndex;
                        var contents = results.Where(item => item.Key == dataIndex).Select(item => item.Value).FirstOrDefault();
                        for (var i = 0; i < contents.Length; i++)
                        {
                            contents[i] = string.Format("{0}{1}", orderedSummaries[i].Title, contents[i]);
                        }
                        //
                        var newSummaryRenderer = this.CreateSummaryRenderer(contents, summaryStyles);
                        firstCol.SummaryRenderer = newSummaryRenderer;
                        if (pOutput.SummaryRenderers.ContainsKey(firstCol))
                        {
                            pOutput.SummaryRenderers[firstCol] = newSummaryRenderer;
                        }
                    }
                    else
                    {
                        var summaryTitles = orderedSummaries.Select(item => item.Title).ToArray();
                        var titleSummaryRenderer = this.CreateSummaryRenderer(summaryTitles, summaryStyles);
                        firstCol.SummaryRenderer = titleSummaryRenderer;
                        pOutput.SummaryRenderers.Add(firstCol, titleSummaryRenderer);
                    }
                }
                #endregion 
            }
        }
        #endregion

        #region 查找指定ID的表格列
        /// <summary>
        /// 查找指定ID的表格列
        /// </summary>
        /// <param name="pGridColumnID">表格列ID</param>
        /// <returns></returns>
        private Column FindGridColumnByID(string pGridColumnID,AnalysisReportOutput pOutput)
        {
            if (pOutput.Columns != null)
            {
                Column found = null;
                foreach (var col in pOutput.Columns)
                {
                    found = col.FindByID(pGridColumnID);
                    if (found != null)
                        return found;
                }
            }
            //如果都未找到则返回null
            return null;
        }
        #endregion

        #region 生成报表的数据集
        /// <summary>
        /// 生成报表的数据集
        /// </summary>
        /// <param name="pDataes">数据</param>
        /// <param name="pOutput">分析报表的输出</param>
        private void GenerateReportStore(DataTable pDataes,AnalysisReportOutput pOutput)
        {
            pOutput.Store = new Store();
            pOutput.Store.StoreID = this.GetUniqueID("store");
            pOutput.Store.ModelClassFullName = pOutput.DataModel.ClassFullName;
            pOutput.Store.Data = pDataes;
        }
        #endregion

        #region 生成报表的表格面板
        /// <summary>
        /// 生成报表的表格面板
        /// </summary>
        /// <param name="pOutput">分析报表的输出</param>
        private void GenerateReportGridPanel(AnalysisReportOutput pOutput)
        {
            //对输出列进行排序
            pOutput.Columns = pOutput.Columns.OrderBy(item => item.DisplayOrder).ToList();
            //创建表格面板
            if (string.IsNullOrWhiteSpace(this.ReportGridPanelID))
            {
                this.ReportGridPanelID = this.GetUniqueID("grid");
            }
            pOutput.Panel = new Panel();
            pOutput.Panel.ID = this.ReportGridPanelID;
            pOutput.Panel.Columns = pOutput.Columns;
            //pOutput.Panel.Height = 400; //默认值
            //pOutput.Panel.Width = 800;  //默认值
            pOutput.Panel.Store = StoreManager.Lookup(pOutput.Store.StoreID);
            pOutput.Panel.Features = new List<IJavascriptObject>();
            pOutput.Panel.Features.Add(new JavascriptBlock("{ ftype: 'summary'}"));
        }
        #endregion

        #region 生成报表表格的顶部工具条
        /// <summary>
        /// 生成报表表格的顶部工具条
        /// </summary>
        private void GenerateReportGridTopToolbar(AnalysisReportOutput pOutput)
        {
            //
            var route = this.CurrentAnalysisMode.DrillRouting.GetDrillingRoute();
            //
            var navi = this.CreateNavigation(route, pOutput);
            pOutput.Navigation = navi;
            pOutput.Panel.TopBarItems = new List<ExtJSComponent>();
            pOutput.Panel.TopBarItems.Add(navi);
        }
        #endregion

        #region 生成报表表格的上下文菜单
        /// <summary>
        /// 生成报表表格的上下文菜单
        /// </summary>
        /// <param name="pOutput"></param>
        private void GenerateReportGridContextMenu(AnalysisReportOutput pOutput)
        {
            //初始化上下文菜单
            Menu contextMenu = new Menu();
            contextMenu.ID = this.GetUniqueID("menu");
            contextMenu.Items = new List<ExtJSComponent>();
            //改变报表中的数据透视列的JS函数
            if (this.JSFunctionOfChangePivot == null)
            {
                var ajaxUrl = this.AppendQueryString(this.AjaxHandlerUrl, "op", ((int)Operations.ChangePivot).ToString());
                this.JSFunctionOfChangePivot = JSFunctionFactory.CreateChangePivot(ajaxUrl, contextMenu.ID, pLoadingMessage: this.LoadingMessage);
                this.JSFunctionOfChangePivot.Type = JSFunctionTypes.Common;
            }
            pOutput.JSFunctions.Add("ChangePivot", this.JSFunctionOfChangePivot);
            //改变行列转换的JS函数
            if (this.JSFunctionOfChangeCRConversion == null)
            {
                var ajaxUrl = this.AppendQueryString(this.AjaxHandlerUrl, "op", ((int)Operations.ChangeCRConversion).ToString());
                this.JSFunctionOfChangeCRConversion = JSFunctionFactory.CreateChangeCRConversion(ajaxUrl, contextMenu.ID, pLoadingMessage: this.LoadingMessage);
                this.JSFunctionOfChangeCRConversion.Type = JSFunctionTypes.Common;
            }
            pOutput.JSFunctions.Add("ChangeCRConversion", this.JSFunctionOfChangeCRConversion);
            //行列互换的JS函数
            if (this.JSFunctionOfCRExchange == null)
            {
                var ajaxUrl = this.AppendQueryString(this.AjaxHandlerUrl, "op", ((int)Operations.CRExchange).ToString());
                this.JSFunctionOfCRExchange = JSFunctionFactory.CreateCRExchange(ajaxUrl, contextMenu.ID, pLoadingMessage: this.LoadingMessage);
                this.JSFunctionOfCRExchange.Type = JSFunctionTypes.Common;
            }
            pOutput.JSFunctions.Add("CRExchange", this.JSFunctionOfCRExchange);
            //导出数据到Excel的JS函数
            if (this.JSFunctionOfExportToExcel == null)
            {
                var ajaxUrl = this.AppendQueryString(this.AjaxHandlerUrl, "op", ((int)Operations.ExportToExcel).ToString());
                this.JSFunctionOfExportToExcel = JSFunctionFactory.CreateExportToExcel(ajaxUrl, pLoadingMessage: this.LoadingMessage);
                this.JSFunctionOfExportToExcel.Type = JSFunctionTypes.Common;
            }
            pOutput.JSFunctions.Add("ExportToExcel", this.JSFunctionOfExportToExcel);
            //
            var dims = this.GetCurrentColumns().Dims;
            //查看明细子菜单
            {
                //不进行行列转换的列
                var dimColumns = this.GetCurrentColumns().NotCRConvertedDims;
                if (dimColumns != null)
                {
                    DimColumn drilledCol = dimColumns.Where(item => item.IsDrillable == true).FirstOrDefault();
                    if (drilledCol != null && this.CurrentAnalysisMode.DrillRouting.CanDrillIn(drilledCol))
                    {
                        DimColumn[] otherCols = dimColumns.Where(item => item.ColumnID != drilledCol.ColumnID).ToArray();
                        //查看明细的JS函数
                        var ajaxUrl = this.AppendQueryString(this.AjaxHandlerUrl, "op", ((int)Operations.ViewDetail).ToString());
                        this.JSFunctionOfViewDetail = JSFunctionFactory.CreateViewDetail(ajaxUrl, drilledCol, otherCols, pLoadingMessage: this.LoadingMessage);
                        this.JSFunctionOfViewDetail.Type = JSFunctionTypes.Common;
                        pOutput.JSFunctions.Add("ViewDetail", this.JSFunctionOfViewDetail);
                        //查看明细子菜单
                        Item viewDetailMenuItem = new Item();
                        contextMenu.Items.Add(viewDetailMenuItem);
                        viewDetailMenuItem.Text = this.CurrentLanguage == Languages.zh_CN ? "查看明细" : "ViewDetail";
                        viewDetailMenuItem.IconUrl = AnalysisReportContextMenuIcons.ViewDetail.GetIconURL();
                        viewDetailMenuItem.Handler = this.JSFunctionOfViewDetail;
                    }
                }
            }
            //数据透视子菜单
            Item perspectiveMenuItem = new Item();
            contextMenu.Items.Add(perspectiveMenuItem);
            perspectiveMenuItem.Text = this.CurrentLanguage == Languages.zh_CN ? "数据透视" : "PivotTable";
            perspectiveMenuItem.IconUrl = AnalysisReportContextMenuIcons.Pivot.GetIconURL();
            if (dims != null && dims.Length > 0)
            {
                Menu pivotSubMenu = new Menu();
                perspectiveMenuItem.Menu = pivotSubMenu;
                pivotSubMenu.Items = new List<ExtJSComponent>();
                foreach (var dim in dims)
                {
                    //初始化菜单项
                    CheckItem item = new CheckItem();
                    item.Text = dim.ColumnTitle;
                    if (dim.IsPivotable == false)
                    {
                        item.IsDisabled = true;
                    }
                    item.IsChecked = dim.IsPivoted;
                    //创建菜单勾选改变的事件
                    JSFunction itemChanged = new JSFunction();
                    itemChanged.Params = new List<string>();
                    itemChanged.Params.Add("pSender");
                    itemChanged.Params.Add("pIsChecked");
                    itemChanged.Type = JSFunctionTypes.Variable;
                    this.JSFunctionOfChangePivot.Type = JSFunctionTypes.Common;
                    itemChanged.AddSentence(this.JSFunctionOfChangePivot.Call(dim.ColumnID.ToJSON(), "pIsChecked").ToScriptBlock(0));
                    item.CheckHandler = itemChanged;
                    //
                    pivotSubMenu.Items.Add(item);
                }
            }
            //行列转换子菜单
            Item crConversionMenu = new Item();
            contextMenu.Items.Add(crConversionMenu);
            crConversionMenu.Text = this.CurrentLanguage == Languages.zh_CN ? "行转列" : "Row to Column";
            crConversionMenu.IconUrl = AnalysisReportContextMenuIcons.RowToColumn.GetIconURL();
            if (dims != null && dims.Length > 0)
            {
                Menu crListMenu = new Menu();
                crConversionMenu.Menu = crListMenu;
                crListMenu.Items = new List<ExtJSComponent>();
                foreach (var dim in dims)
                {
                    CheckItem item = new CheckItem();
                    item.Text = dim.ColumnTitle;
                    item.IsDisabled = (!dim.IsCRConversionable);
                    item.IsChecked = dim.IsCRConverted;
                    //创建菜单勾选改变的事件
                    JSFunction itemChanged = new JSFunction();
                    itemChanged.Params = new List<string>();
                    itemChanged.Params.Add("pSender");
                    itemChanged.Params.Add("pIsChecked");
                    itemChanged.Type = JSFunctionTypes.Variable;
                    this.JSFunctionOfChangeCRConversion.Type = JSFunctionTypes.Common;
                    itemChanged.AddSentence(this.JSFunctionOfChangeCRConversion.Call(dim.ColumnID.ToJSON(), "pIsChecked").ToScriptBlock(0));
                    item.CheckHandler = itemChanged;
                    //
                    crListMenu.Items.Add(item);
                }
            }
            //行列互换
            Item crExchangeMenu = new Item();
            contextMenu.Items.Add(crExchangeMenu);
            crExchangeMenu.Text = this.CurrentLanguage == Languages.zh_CN ? "行列互换" : "C&R Exchange";
            crExchangeMenu.IconUrl = AnalysisReportContextMenuIcons.CRExchange.GetIconURL();
            JSFunction crExchange = new JSFunction();
            crExchange.Type = JSFunctionTypes.Variable;
            crExchange.AddSentence("{0}();", this.JSFunctionOfCRExchange.FunctionName);
            crExchangeMenu.Handler = crExchange;
            //导出子菜单
            Item exportMenu = new Item();
            contextMenu.Items.Add(exportMenu);
            exportMenu.Text = this.CurrentLanguage == Languages.zh_CN ? "导出" : "Export";
            exportMenu.IconUrl = AnalysisReportContextMenuIcons.Export.GetIconURL();
            JSFunction exportToExcel = new JSFunction();
            exportToExcel.Type = JSFunctionTypes.Variable;
            exportToExcel.AddSentence("{0}();", this.JSFunctionOfExportToExcel.FunctionName);
            exportMenu.Handler = exportToExcel;
            //
            pOutput.PrevContextMenu = pOutput.CurrentContextMenu;
            pOutput.CurrentContextMenu = contextMenu;
            //创建显示上下文菜单的JS函数并输出到页面
            if (pOutput.JSFunctions == null)
                pOutput.JSFunctions = new Dictionary<string, JSFunction>();
            var fnOfShowContextMenu = JSFunctionFactory.CreateShowContextMenu(contextMenu.ID);
            pOutput.JSFunctions.Add("ShowContextMenu", fnOfShowContextMenu);
        }
        #endregion

        #endregion

        #endregion
    }
}
