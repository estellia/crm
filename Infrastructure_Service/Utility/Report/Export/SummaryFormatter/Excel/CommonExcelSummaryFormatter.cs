/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/9/9 19:45:17
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

using Aspose.Cells;
using JIT.Utility.Report.Analysis;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Report.Export.SummaryFormatter.Excel
{
    /// <summary>
    /// 通用的报表摘要格式化器 
    /// </summary>
    public class CommonExcelSummaryFormatter:ISummaryFormatter
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CommonExcelSummaryFormatter()
        {
        }
        #endregion

        #region ISummaryFormatter 成员
        /// <summary>
        /// 往导出后的容器中写数据
        /// </summary>
        /// <param name="pContainer">导出后的容器,如果导出为Excel,则为WookSheet</param>
        /// <param name="pSummary">报表摘要</param>
        /// <param name="pSummaryValue">摘要值</param>
        /// <param name="pRowIndex">行</param>
        /// <param name="pColumnIndex">列</param>
        /// <param name="pIsIncludeSummaryTitle">是否在写数据的时候包含摘要标题(主要是当摘要列是第一列时,将标题和数据放在一起写)</param>
        /// <param name="pColumnType">摘要所属的表格列的列类型</param>
        public void WriteSummary(object pContainer,ISummary pSummary, object pSummaryValue, int pRowIndex, int pColumnIndex,bool pIsIncludeSummaryTitle, ColumnTypes pColumnType)
        {
            var sheet = pContainer as Worksheet;
            if (sheet != null)
            {
                //初始化
                Cell contentCell = sheet.Cells[pRowIndex, pColumnIndex];
                Style style = contentCell.GetStyle();
                style.HorizontalAlignment = TextAlignmentType.Right;
                //
                string cellVal = string.Empty;
                if (pSummaryValue != null)
                    cellVal = pSummaryValue.ToString();
                if (pIsIncludeSummaryTitle)
                {
                    cellVal = string.Format("{0}{1}",pSummary.Title,cellVal);
                }
                //
                contentCell.PutValue(cellVal);
            }
            else
            {
                throw new ArgumentNullException("pContainer", "pContainer为null或不是Aspose.Cells.Worksheet对象.");
            }
        }

        /// <summary>
        /// 往容器中写摘要标题
        /// </summary>
        /// <param name="pContainer"></param>
        /// <param name="pSummary"></param>
        /// <param name="pRowIndex"></param>
        /// <param name="pColumnIndex"></param>
        public void WriteSummaryTitle(object pContainer, ISummary pSummary, int pRowIndex, int pColumnIndex)
        {
            var sheet = pContainer as Worksheet;
            if (sheet != null)
            {
                //初始化
                Cell contentCell = sheet.Cells[pRowIndex, pColumnIndex];
                Style style = contentCell.GetStyle();
                style.HorizontalAlignment = TextAlignmentType.Left;
                contentCell.PutValue(pSummary.Title);
            }
            else
            {
                throw new ArgumentNullException("pContainer", "pContainer为null或不是Aspose.Cells.Worksheet对象.");
            }
        }
        #endregion
    }
}
