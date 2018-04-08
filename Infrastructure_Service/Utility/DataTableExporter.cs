using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Cells;
using System.Data;
using System.Drawing;
using JIT.Utility.Report;
using System.Collections;
using JIT.Utility.Report.Analysis;

namespace JIT.Utility
{
    /// <summary>
    /// 将Datatable导出到XLS
    /// </summary>
    public class DataTableExporter
    { 

        /// <summary>
        /// 将DataTable写入到Excel中
        /// </summary>
        /// <param name="pData">源数据</param>
        /// <param name="pDataStartRowIndex">数据开始行索引</param>
        /// <returns>Workbook对象（可直接调用它的Save方法进行保存文件）</returns>
        public static Workbook WriteXLS(DataTable pData, int pDataStartRowIndex)
        {
            Aspose.Cells.License lic = new License();
            lic.SetLicense("Aspose.Total.lic");
            Workbook wb = new Workbook();
            wb.Worksheets.Clear();
            wb.Worksheets.Add();
            string worksheetName = pData.TableName;
            worksheetName = worksheetName.Replace(":", "");
            worksheetName = worksheetName.Replace("\\", "");
            worksheetName = worksheetName.Replace("/", "");
            worksheetName = worksheetName.Replace("?", "");
            worksheetName = worksheetName.Replace("*", "");
            worksheetName = worksheetName.Replace("[", "");
            worksheetName = worksheetName.Replace("]", "");
            wb.Worksheets[0].Name = worksheetName;
            var ws = wb.Worksheets[0];
            //写入标题
            WriteTitle(ws, pData);

            //写入内容
            WriteDataRow(ws, pData, pDataStartRowIndex, 1);
            return wb;
        }

        /// <summary>
        /// 将DataTable写入到WorkSheet
        /// </summary>
        /// <param name="pSheet">worksheet</param>
        /// <param name="pData">源数据</param>
        /// <param name="pDataStartRowIndex">数据开始行索引</param>
        /// <returns>Workbook对象（可直接调用它的Save方法进行保存文件）</returns>
        private static void WriteXLS(Worksheet pSheet,DataTable pData, int pDataStartRowIndex)
        {
            string worksheetName = pData.TableName;
            worksheetName = worksheetName.Replace(":", "");
            worksheetName = worksheetName.Replace("\\", "");
            worksheetName = worksheetName.Replace("/", "");
            worksheetName = worksheetName.Replace("?", "");
            worksheetName = worksheetName.Replace("*", "");
            worksheetName = worksheetName.Replace("[", "");
            worksheetName = worksheetName.Replace("]", "");
            pSheet.Name = worksheetName;
            //写入标题
            WriteTitle(pSheet, pData);
            //写入内容
            WriteDataRow(pSheet, pData, pDataStartRowIndex, 1);
        }

        /// <summary>
        /// 将DataTable写入到Excel中
        /// </summary>
        /// <param name="pData">源数据</param>
        /// <param name="pDataStartRowIndex">数据开始行索引</param>
        /// <returns>Workbook对象（可直接调用它的Save方法进行保存文件）</returns>
        public static Workbook WriteXLS(DataSet pData, int pDataStartRowIndex)
        {
            Aspose.Cells.License lic = new License();
            lic.SetLicense("Aspose.Total.lic");
            Workbook wb = new Workbook();
            wb.Worksheets.Clear();
            for (int i = 0; i < pData.Tables.Count; i++)
            {
                Worksheet sheet = wb.Worksheets.Add(pData.Tables[i].TableName);
                WriteXLS(sheet, pData.Tables[i], pDataStartRowIndex);
            }
            return wb;
        }

        private static void SetBorder(Style style)
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
        /// <summary>
        /// 写入工作簿的列标题
        /// </summary>
        /// <param name="pWorksheet">工作簿对象</param>
        /// <param name="pData">数据表</param>
        private static void WriteTitle(Worksheet pWorksheet, DataTable pData)
        {
            int shownColumnCount = 0;
            for (int i = 0; i < pData.Columns.Count; i++)
            {
                DataColumn dcItem = pData.Columns[i];
                pWorksheet.Cells[0, shownColumnCount].PutValue(dcItem.ColumnName);
                Range r = pWorksheet.Cells.CreateRange(0, shownColumnCount, 1, 1);
                DrawTitleStyle(pWorksheet.Workbook, r);
                shownColumnCount++;
            }           
        }  
        /// <summary>
        /// 行DataTable中的数据写入到工作簿中
        /// </summary>
        /// <param name="pWorksheet">工作簿对象</param>
        /// <param name="pData">数据表</param>
        private static void WriteDataRow(Worksheet pWorksheet, DataTable pData,int pStartRowIndex,int pWriteRowIndex)
        {
            for (int i = pStartRowIndex; i < pData.Rows.Count; i++)
            {
                for (int j = 0; j < pData.Columns.Count; j++)
                {
                    Cell contentCell = pWorksheet.Cells[i + pWriteRowIndex, j];
                    Style style = contentCell.GetStyle();
                    switch (pData.Columns[j].DataType.ToString().ToLower())
                    {
                        case "system.int32":
                            if (pData.Rows[i][j] != DBNull.Value)
                            {
                                int tempIntValue;
                                if (int.TryParse(pData.Rows[i][j].ToString(), out tempIntValue))
                                    contentCell.PutValue(tempIntValue);
                            }
                            break;
                        case "system.double":
                        case "system.decimal":
                        case "system.float":
                        case "system.single":
                            if (pData.Rows[i][j] != DBNull.Value)
                            {
                                double tempDoubleValue;
                                if (double.TryParse(pData.Rows[i][j].ToString(), out tempDoubleValue))
                                    contentCell.PutValue(tempDoubleValue);
                            }
                            break; 
                        default:
                            contentCell.PutValue(pData.Rows[i][j].ToString());
                            break;
                    }
                    style.HorizontalAlignment = TextAlignmentType.Center;
                }
            }
        }
          
        /// <summary>
        /// 标题样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="pRange"></param>
        private static void DrawTitleStyle(Workbook workbook, Range pRange)
        {
            Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()];
            Color c = Color.Gray;
            style.ForegroundColor = c;
            style.Pattern = BackgroundType.Solid;
            style.Font.Color = Color.White;
            style.Font.IsBold = true;
            //s.Borders.DiagonalColor = Color.Red;
            //s.Borders.DiagonalStyle= CellBorderType.Thick;
            //s.Borders.SetColor(Color.Red);
            //s.Borders.SetStyle(CellBorderType.Thick);
            //s.SetBorder(BorderType.Horizontal | BorderType.Vertical, CellBorderType.Dashed , Color.Red);
            //s.SetBorder(BorderType.LeftBorder | BorderType.RightBorder | BorderType.BottomBorder | BorderType.TopBorder, CellBorderType.Thick, Color.Red);
            style.HorizontalAlignment = TextAlignmentType.Center;
            //s.ShrinkToFit = true; 

            SetBorder(style);

            StyleFlag styleFlag = new StyleFlag();
            //Specify all attributes
            styleFlag.All  = true;
            styleFlag.Borders = true;
            pRange.ApplyStyle(style, styleFlag);            
        }

    }
}
