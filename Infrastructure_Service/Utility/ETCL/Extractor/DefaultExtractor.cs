using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Aspose.Cells;
using JIT.Utility.Log;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Extractor
{
    /// <summary>
    /// 默认抽取器
    /// </summary>
    public class DefaultExtractor : IExtractor
    {

        /// <summary>
        /// 将XLS抽取到DataTable
        /// </summary>
        /// <param name="pDataSource">数据源（XLS的文件路径）</param>
        /// <param name="oData">抽取出的直接数据（DataTable）</param>
        /// <param name="oResult">抽取结果详情</param>
        /// <returns>TRUE：抽取成功，FALSE：抽取失败。</returns>
        public bool Process(object pDataSource, out object oData, out IETCLResultItem[] oResult)
        {
            bool bReturn = true;
            List<ETCLCommonResultItem> lstErrorInfo = new List<ETCLCommonResultItem>();
            List<ETCLCommonResultItem> pcheckResult = new List<ETCLCommonResultItem>();
            Worksheet worksheetItem = (Worksheet)pDataSource;
            int maxRowCount = worksheetItem.Cells.MaxRow;
            int maxColCount = worksheetItem.Cells.MaxColumn;
            List<string> columnNames = new List<string>();
            if (maxRowCount > 0 && maxColCount > 0)
            {
                //1.检查Excel工作表的第一行,如果第一行全为空或string.Empty,则表示没有数据/数据是已经被处理过的
                bool bypassed = true;
                for (int i = 0; i < maxColCount + 1; i++)
                {
                    if (worksheetItem.Cells[0, i].Value != null && (!string.IsNullOrEmpty(worksheetItem.Cells[0, i].StringValue)))
                    {
                        bypassed = false;
                        columnNames.Add(worksheetItem.Cells[0, i].StringValue.Trim().ToUpper());
                    }
                }
                if (bypassed)
                {                    
                    oData = null;
                    oResult = null;
                    return true;
                }
                //检查列名是否重复
                for (int i = 0; i < columnNames.Count; i++)
                {
                    for (int j = i + 1; j < columnNames.Count; j++)
                    {
                        if (columnNames[i] == columnNames[j])
                        {
                            bReturn = false;
                            ETCLCommonResultItem resultItem = new ETCLCommonResultItem();
                            resultItem.ColumnOrder = -1;
                            resultItem.Message = string.Format("工作表[{0}]中第{1}列的列名[{3}]与第{2}列的列名重复." , worksheetItem.Name, i + 1, j + 1, columnNames[i]);
                            resultItem.OPType = OperationType.Extract;
                            resultItem.ResultCode = 105;
                            resultItem.RowIndex = -1;
                            resultItem.TableIndex = -1;
                            resultItem.WorksheetIndex = worksheetItem.Index;
                            lstErrorInfo.Add(resultItem);
                        }
                    }
                }
                if (lstErrorInfo.Count == 0)
                {
                    //记录原始数据
                    DataTable dt = worksheetItem.Cells.ExportDataTableAsString(0, 0, maxRowCount + 1, maxColCount + 1, true);
                    oData = dt;
                }
                else
                {
                    oData = null;
                }
            }
            else
            {
                oData = null;
            }
            oResult = lstErrorInfo.ToArray();
            return bReturn;
        }

    }
}
