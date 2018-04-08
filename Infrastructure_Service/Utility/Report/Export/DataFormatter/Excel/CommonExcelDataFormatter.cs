/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/9/9 17:53:10
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
using System.Drawing;
using System.Text;

using Aspose.Cells;
using JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column;

namespace JIT.Utility.Report.Export.DataFormatter.Excel
{
    /// <summary>
    /// 通用的Excel下的数据格式化器 
    /// </summary>
    public class CommonExcelDataFormatter:IDataFormatter
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public CommonExcelDataFormatter()
        {
        }
        #endregion

        #region IDataFormatter 成员

        /// <summary>
        /// 往导出后的容器中写数据
        /// </summary>
        /// <param name="pContainer">导出后的容器,如果导出为Excel,则为WookSheet</param>
        /// <param name="pData">数据值</param>
        /// <param name="pRowIndex">行</param>
        /// <param name="pColumnIndex">列</param>
        /// <param name="pColumnType">表格列的列类型</param>
        /// <param name="pGridColumn">ExtJS的表格列</param>
        public void WriteData(object pContainer, object pData, int pRowIndex, int pColumnIndex, ColumnTypes pColumnType,JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column.Column pGridColumn)
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
                switch (pColumnType)
                {
                    case ColumnTypes.Int:
                        {
                            if (pData == null || string.IsNullOrWhiteSpace(pData.ToString()))
                            {
                                cellVal = "0";
                            }
                            else
                            {
                                cellVal = Convert.ToInt32(pData).ToString();
                            }
                            style.HorizontalAlignment = TextAlignmentType.Right;
                        }
                        break;
                    case ColumnTypes.Decimal:
                        {
                            if (pData == null || string.IsNullOrWhiteSpace(pData.ToString()))
                            {
                                cellVal = "0.00";
                            }
                            else
                            {
                                cellVal = Convert.ToDecimal(pData).ToString("f2");
                            }
                            style.HorizontalAlignment = TextAlignmentType.Right;
                        }
                        break;
                    case ColumnTypes.Boolean:
                        {
                            if (pData == null || string.IsNullOrWhiteSpace(pData.ToString()))
                            {
                                cellVal = "";
                            }
                            else
                            {
                                cellVal = Convert.ToBoolean(pData).ToString();
                            }
                            style.HorizontalAlignment = TextAlignmentType.Center;
                        }
                        break;
                    case ColumnTypes.String:
                        {
                            if (pData == null)
                            {
                                cellVal = "";
                            }
                            else
                            {
                                cellVal = pData.ToString();
                            }
                            style.HorizontalAlignment = TextAlignmentType.Left;
                        }
                        break;
                    case ColumnTypes.Date:
                        {
                            if (pData == null || string.IsNullOrWhiteSpace(pData.ToString()))
                            {
                                cellVal = "";
                            }
                            else
                            {
                                cellVal = Convert.ToDateTime(pData).ToString("yyyy-MM-dd");
                            }
                            style.HorizontalAlignment = TextAlignmentType.Center;
                        }
                        break;
                    case ColumnTypes.DateTime:
                        {
                            if (pData == null || string.IsNullOrWhiteSpace(pData.ToString()))
                            {
                                cellVal = "";
                            }
                            else
                            {
                                cellVal = Convert.ToDateTime(pData).ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            style.HorizontalAlignment = TextAlignmentType.Center;
                        }
                        break;
                    case ColumnTypes.Time:
                        {
                            if (pData == null || string.IsNullOrWhiteSpace(pData.ToString()))
                            {
                                cellVal = "";
                            }
                            else
                            {
                                cellVal = Convert.ToDateTime(pData).ToString("HH:mm:ss");
                            }
                            style.HorizontalAlignment = TextAlignmentType.Center;
                        }
                        break;
                    case ColumnTypes.Timespan:
                        {
                            if (pData == null || string.IsNullOrWhiteSpace(pData.ToString()))
                                cellVal = "";
                            else
                            {
                                cellVal = string.Empty;
                                TimeSpan tsTemp = new TimeSpan(0, 0, Convert.ToInt32(pData));
                                int iLevel = 0;
                                if (tsTemp.Days > 0)
                                    iLevel = 4;
                                else if (tsTemp.Hours > 0)
                                    iLevel = 3;
                                else if (tsTemp.Minutes > 0)
                                    iLevel = 2;
                                else
                                    iLevel = 1;
                                switch (iLevel)
                                {
                                    case 1:
                                        cellVal = tsTemp.Seconds.ToString() + "秒";
                                        break;
                                    case 2:
                                        cellVal = tsTemp.Minutes.ToString() + "分" + tsTemp.Seconds.ToString() + "秒";
                                        break;
                                    case 3:
                                        cellVal = tsTemp.Hours.ToString() + "小时" + tsTemp.Minutes.ToString() + "分" + tsTemp.Seconds.ToString() + "秒";
                                        break;
                                    case 4:
                                        cellVal = tsTemp.Days + "天" + tsTemp.Hours.ToString() + "小时" + tsTemp.Minutes.ToString() + "分" + tsTemp.Seconds.ToString() + "秒";
                                        break;
                                }
                            }
                        }
                        break;
                    case ColumnTypes.Percent:
                        {
                            if (pData == null)
                            {
                                cellVal = "";
                            }
                            else
                            {
                                var val = Convert.ToDecimal(pData);
                                var accuracy = pGridColumn.Accuracy;
                                if (accuracy.HasValue == false)
                                    accuracy = 0;
                                cellVal = (val * 100).ToString("f"+accuracy.Value.ToString()) + "%";
                            }
                        }
                        break;
                    default:
                        {
                            if (pData == null)
                            {
                                cellVal = "";
                            }
                            else
                            {
                                cellVal = pData.ToString();
                            }
                        }
                        break;
                }
                //
                contentCell.PutValue(cellVal);
            }
            else
            {
                throw new ArgumentNullException("pContainer", "pContainer为null或不是Aspose.Cells.Worksheet对象.");
            }
        }

        #endregion

        #region 设置边框样式
        /// <summary>
        /// 设置边框样式
        /// </summary>
        /// <param name="style"></param>
        protected void SetBorder(Style style)
        {
            //Setting the line style of the top border
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            //Setting the color of the top border
            style.Borders[BorderType.TopBorder].Color = Color.Black;
            //Setting the line style of the bottom border
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            //Setting the color of the bottom border
            style.Borders[BorderType.BottomBorder].Color = Color.Black;
            //Setting the line style of the left border
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            //Setting the color of the left border
            style.Borders[BorderType.LeftBorder].Color = Color.Black;
            //Setting the line style of the right border
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            //Setting the color of the right border
            style.Borders[BorderType.RightBorder].Color = Color.Black;
        }
        #endregion
    }
}
