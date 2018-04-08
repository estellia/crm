/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/10 17:28:00
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
using System.Text;

using JIT.Utility.Report.Export.SummaryFormatter;
using JIT.Utility.Report.Export.SummaryFormatter.Excel;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Report.Analysis
{
    /// <summary>
    /// 通用的摘要 
    /// </summary>
    public class CommonSummary:ISummary
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CommonSummary()
        {
            this.Formatter = new CommonExcelSummaryFormatter();
            this.CalculationType = SummaryCalculationTypes.CalculateSelf;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 需要进行计算的列的ID
        /// </summary>
        protected List<string> NeedCalculationColumnIDs { get; set; }

        /// <summary>
        /// 摘要计算器
        /// <remarks>
        /// <para>计算器的方法声明：</para>
        /// <para>第一个参数：需要进行计算的报表列</para>
        /// <para>第二个参数：报表列的所有数据项</para>
        /// <para>返回值：计算后的摘要内容</para>
        /// </remarks>
        /// </summary>
        public Func<AnalysisColumn,object[], string> Calculator;

        /// <summary>
        /// 充满魔力的报表摘要计算器,他可以满足任何需求...(provided by jack.chen,a great magician,he is god.)
        /// </summary>
        public Func<AnalysisColumn,Column, ReportDataRetriever, string> MagicCalculator;
        #endregion

        #region 添加需要进行摘要计算的报表列
        /// <summary>
        /// 添加需要进行摘要计算的报表列
        /// </summary>
        /// <param name="pColumnID">报表列的ID</param>
        public void Add(string pColumnID)
        {
            //
            if (string.IsNullOrEmpty(pColumnID))
                throw new ArgumentNullException("pColumnID");
            //
            if (this.NeedCalculationColumnIDs == null)
                this.NeedCalculationColumnIDs = new List<string>();
            bool isRepeat = false;
            foreach (var id in this.NeedCalculationColumnIDs)
            {
                if (id == pColumnID)
                {
                    isRepeat = true;
                    break;
                }
            }
            if (!isRepeat)
                this.NeedCalculationColumnIDs.Add(pColumnID);
        }

        /// <summary>
        /// 添加需要进行摘要计算的报表列
        /// </summary>
        /// <param name="pColumn">报表列</param>
        public void Add(AnalysisColumn pColumn)
        {
            if (pColumn != null)
            {
                this.Add(pColumn.ColumnID);
            }
        }
        #endregion

        #region 清除
        /// <summary>
        /// 清除
        /// </summary>
        public void Clear()
        {
            this.NeedCalculationColumnIDs.Clear();
        }
        #endregion

        #region ISummary 成员

        /// <summary>
        /// 摘要的文本
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 摘要显示的顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 摘要的格式化器
        /// <remarks>
        /// <para>此格式化器用于在将报表数据导出时，服务器端对报表摘要进行格式化,以实现所见即所得的导出</para>
        /// </remarks>
        /// </summary>
        public ISummaryFormatter Formatter { get; set; }

        /// <summary>
        /// 判断报表列是否需要进行摘要计算
        /// </summary>
        /// <param name="pColumn">报表列</param>
        /// <returns></returns>
        public bool IsNeedCalculate(AnalysisColumn pColumn)
        {
            if (pColumn == null || string.IsNullOrEmpty(pColumn.ColumnID))
                return false;
            //
            if (this.NeedCalculationColumnIDs == null || this.NeedCalculationColumnIDs.Count <= 0)
                return false;
            else
            {
                foreach (var id in this.NeedCalculationColumnIDs)
                {
                    if (id == pColumn.ColumnID)
                        return true;
                }
            }
            //
            return false;
        }

        /// <summary>
        /// 计算摘要
        /// </summary>
        /// <param name="pColumn">报表列</param>
        /// <param name="pDataItems">
        /// <remarks>
        /// <para>列的数据项</para>
        /// <para>1.如果列是维度列,则对象是为KeyValuePair集合,其中Key为维度数据项的ID，Value为维度数据项的文本</para>
        /// <para>2.如果是度量列，则对象为度量值</para>
        /// <para>3.如果是自定义计算列，则对象为计算后的值</para>
        /// </remarks>
        /// </param>
        /// <returns></returns>
        public string CalculateSummary(AnalysisColumn pColumn, object[] pDataItems)
        {
            return this.Calculator(pColumn, pDataItems);
        }

        #region 1.01版本扩展
        /// <summary>
        /// 摘要计算方式
        /// <remarks>
        /// <para>1.01版本扩展</para>
        /// </remarks>
        /// </summary>
        public SummaryCalculationTypes CalculationType { get; set; }

        /// <summary>
        /// 计算摘要
        /// <remarks>
        /// <para>1.01版本扩展</para>
        /// </remarks>
        /// </summary>
        /// <param name="pColumn">当前进行计算的报表列</param>
        /// <param name="pGridColumn">当前进行计算的(ExtJS)表格列</param>
        /// <param name="pDataRetriever">报表数据获取器</param>
        /// <returns></returns>
        public string CalculateSummary(AnalysisColumn pColumn, Column pGridColumn, ReportDataRetriever pDataRetriever)
        {
            return this.MagicCalculator(pColumn,pGridColumn, pDataRetriever);
        }
        #endregion

        #endregion

    }
}
