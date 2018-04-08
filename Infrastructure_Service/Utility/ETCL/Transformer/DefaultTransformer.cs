using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.Utility.Log;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Transformer
{
    /// <summary>
    /// 默认数据-实体转换器（采用反射的方式）
    /// </summary>
    public class DefaultTransformer : ITransformer
    {
        /// <summary>
        /// ETCL工作簿信息
        /// </summary>
        private ETCLWorkSheetInfo _workSheetInfo;
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="pWorksheetInfo">ETCL工作簿信息</param>
        public DefaultTransformer(ETCLWorkSheetInfo pWorksheetInfo)
        {
            this._workSheetInfo = pWorksheetInfo;
        }

        /// <summary>
        /// 执行转换，并进行文本-键转换
        /// </summary>
        /// <param name="pDataSource">从数据源取出的直接数据</param>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="oData">转换后的数据</param>
        /// <param name="oResult">抽取结果详情</param>
        /// <returns>TRUE：抽取成功，FALSE：抽取失败。</returns>
        public bool Process(DataTable pDataSource, BasicUserInfo pUserInfo, out IETCLDataItem[] oData, out IETCLResultItem[] oCheckResult)
        {
            bool isPass = true;
            List<ETCLCommonResultItem> lstCheckResult = new List<ETCLCommonResultItem>();
            ////将原始数据按表拆分到各个DataTable
            List<CommonExcelDataItem> lstDataItem = new List<CommonExcelDataItem>();
            if (pDataSource != null)
            {
                bool isAllNullOrEmpty = true;
                //移除全为null或string.Empty的记录
                for (int i = 0; i < pDataSource.Rows.Count; i++)
                {
                    isAllNullOrEmpty = true;
                    DataRow dr = pDataSource.Rows[i];
                    for (int j = 0; j < pDataSource.Columns.Count; j++)
                    {
                        if (dr[j] != DBNull.Value && dr[j].ToString().Trim() != string.Empty)
                        {
                            isAllNullOrEmpty = false;
                            break;
                        }
                    }
                    if (isAllNullOrEmpty)
                    {
                        pDataSource.Rows.RemoveAt(i);
                        i--;
                    }
                }
                pDataSource.AcceptChanges();

                foreach (var tableItem in this._workSheetInfo.Tables)
                {
                    if (tableItem.ReferenceOnly == 1)
                    {
                        continue;
                    }
                    tableItem.Data = pDataSource.Copy();
                    tableItem.Data.TableName = tableItem.TableName;
                    Dictionary<string, string> columnTextNameMapping = new Dictionary<string, string>();
                    foreach (var columnItem in tableItem.Columns)
                    {
                        columnTextNameMapping.Add(columnItem.ColumnText, columnItem.ColumnName);
                    }
                    //去掉多余列
                    foreach (DataColumn columnItem in pDataSource.Columns)
                    {
                        if (columnTextNameMapping.ContainsKey(columnItem.ColumnName))
                        {//文本改成列名
                            tableItem.Data.Columns[columnItem.ColumnName].ColumnName = columnTextNameMapping[columnItem.ColumnName];
                        }
                        else
                        {
                            tableItem.Data.Columns.Remove(columnItem.ColumnName);
                        }
                    }
                }
            }
            else
            {
                oData = null;
                oCheckResult = null;
                return true;
            }
            string assemblyPath;
            if (System.Environment.CurrentDirectory == AppDomain.CurrentDomain.BaseDirectory)
            {//Winform
                assemblyPath = AppDomain.CurrentDomain.BaseDirectory;
            }
            else
            {//Web
                assemblyPath = AppDomain.CurrentDomain.BaseDirectory + "Bin/";
            }
            //转换到Entity
            Dictionary<string, object[]> dictDBTableCache = new Dictionary<string, object[]>();
            foreach (var tableItem in this._workSheetInfo.Tables)
            {
                if (tableItem.Data == null || tableItem.ReferenceOnly == 1)
                    continue;
                List<CommonExcelDataItem> lstEntity = new List<CommonExcelDataItem>();
                Dictionary<string, Dictionary<object, object>> dictCachedReference = new Dictionary<string, Dictionary<object, object>>();
                foreach (DataRow tableRowItem in tableItem.Data.Rows)
                {
                    CommonExcelDataItem oInstance = new CommonExcelDataItem();
                    lstDataItem.Add(oInstance);
                    oInstance.Table = tableItem;
                    oInstance.Index = lstEntity.Count;
                    if (string.IsNullOrEmpty(tableItem.EntityName))
                    {//使用默认规则创建Entity（TableName+“Entity”）
                        oInstance.Entity = Reflection.DynamicReflectionHandler.CreateInstance(assemblyPath + "TenantPlatform.Entity.dll", "JIT.TenantPlatform.Entity." + tableItem.TableName + "Entity", null);
                    }
                    else
                    {
                        oInstance.Entity = Reflection.DynamicReflectionHandler.CreateInstance(assemblyPath + tableItem.EntityAssemblyName, tableItem.EntityName, null);
                    }
                    //依次填充属性
                    foreach (ETCLColumn columnItem in tableItem.Columns)
                    {//值->键转换处理。
                        if (!tableItem.Data.Columns.Contains(columnItem.ColumnName))
                            continue;
                        object oCellValue = tableRowItem[columnItem.ColumnName];
                        columnItem.ValueCache.Add(oCellValue);
                        object oReferencedID = null;
                        if (!string.IsNullOrWhiteSpace(columnItem.ReferenceTableName) && !string.IsNullOrWhiteSpace(columnItem.ReferenceColumnName) && !string.IsNullOrWhiteSpace(columnItem.ReferenceTextColumnName))
                        {
                            //每个单元格;
                            if (oCellValue == null || string.IsNullOrWhiteSpace(oCellValue.ToString()))//空值不检查依赖项
                                continue;
                            bool bExists = false;
                            Dictionary<object, object> dictValuesOfCurrentColumn;
                            //a.缓存中查找
                            if (dictCachedReference.ContainsKey(columnItem.ColumnName))
                            {//缓存中查找
                                dictValuesOfCurrentColumn = dictCachedReference[columnItem.ColumnName];
                                if (dictValuesOfCurrentColumn.ContainsKey(oCellValue))
                                {
                                    bExists = true;
                                    oReferencedID = dictValuesOfCurrentColumn[oCellValue];
                                }
                            }
                            else
                            {
                                dictValuesOfCurrentColumn = new Dictionary<object, object>();
                                dictCachedReference.Add(columnItem.ColumnName, dictValuesOfCurrentColumn);
                            }

                            //b.数据库中查找
                            if (!bExists)
                            {//通过反射从数据库中读取此依赖表信息
                                ETCLTable referenceTable = this._workSheetInfo.GetTableByName(columnItem.ReferenceTableName);
                                object[] oTempEntities;
                                if (dictDBTableCache.ContainsKey(columnItem.ReferenceTableName))
                                {
                                    oTempEntities = dictDBTableCache[columnItem.ReferenceTableName];
                                }
                                else
                                {
                                    oTempEntities = Reflection.DynamicReflectionHandler.QueryAll(referenceTable, new object[] { pUserInfo });
                                    dictDBTableCache.Add(columnItem.ReferenceTableName, oTempEntities);
                                }
                                foreach (var entityItem in oTempEntities)
                                {
                                    bool bMached = true;
                                    if (!string.IsNullOrWhiteSpace(columnItem.ReferenceAdditionalCondition))
                                    {//附加条件(如，从Options中查数据时，需要OptionName和OptionValue多个字段)
                                        string[] conditions = columnItem.ReferenceAdditionalCondition.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        foreach (var conditionItem in conditions)
                                        {
                                            string[] conditionNameValue = conditionItem.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                                            if (conditionNameValue == null || conditionNameValue.Length != 2)
                                                continue;
                                            object oValue = Reflection.DynamicReflectionHandler.GetProperty(entityItem, conditionNameValue[0]);
                                            if (oValue == null || oValue.ToString() != conditionNameValue[1])
                                            {
                                                bMached = false;
                                                break;
                                            }
                                        }
                                        if (!bMached)
                                            continue;
                                    }

                                    object oTempValue = Reflection.DynamicReflectionHandler.GetProperty(entityItem, columnItem.ReferenceTextColumnName);
                                    if (oTempValue != null && oTempValue.Equals(oCellValue))
                                    {
                                        oReferencedID = Reflection.DynamicReflectionHandler.GetProperty(entityItem, columnItem.ReferenceColumnName);
                                        dictValuesOfCurrentColumn.Add(oCellValue, oReferencedID);
                                        bExists = true;
                                        break;
                                    }
                                }
                            }
                            if (bExists)//有效的依赖项
                            {
                                if (!Reflection.DynamicReflectionHandler.SetProperty(oInstance.Entity, columnItem.ColumnName, oReferencedID))
                                    throw new ETCLException(400, string.Format("反射方法设置依赖项属性失败,表:{0} 列:{1} 关联:{2}", tableItem.TableName, columnItem.ColumnName, oReferencedID));
                            }
                            else
                            {
                                isPass = false;
                                ETCLCommonResultItem resultItem = new ETCLCommonResultItem();
                                resultItem.ColumnOrder = tableItem.Columns.IndexOf(columnItem);
                                resultItem.Message = string.Format("[数据项:【{0}】]的数据在关联表中不存在.", columnItem.ColumnText);
                                resultItem.OPType = OperationType.ForeignKeyDependence;
                                resultItem.ResultCode = 102;
                                resultItem.RowIndex = tableItem.Data.Rows.IndexOf(tableRowItem);
                                resultItem.TableIndex = this._workSheetInfo.Tables.IndexOf(tableItem);
                                resultItem.WorksheetIndex = this._workSheetInfo.Index.Value;// etclInfo.WorkSheets.IndexOf(worksheetItem);
                                lstCheckResult.Add(resultItem);
                            }
                        }
                        else
                        {
                            if (oCellValue != null)
                            {
                                if (columnItem.ValueCountCache.ContainsKey(oCellValue))
                                    columnItem.ValueCountCache[oCellValue]++;
                                else
                                    columnItem.ValueCountCache.Add(oCellValue, 1);
                            }
                            if (!Reflection.DynamicReflectionHandler.SetProperty(oInstance.Entity, columnItem.ColumnName, oCellValue))
                                throw new ETCLException(400, string.Format("反射方法设置属性失败,表:{0} 列:{1} 值:{2}", tableItem.TableName, columnItem.ColumnName, oCellValue));
                        }
                    }
                    lstEntity.Add(oInstance);
                }
                tableItem.Entities = lstEntity;
            }
            oCheckResult = lstCheckResult.ToArray();
            oData = lstDataItem.ToArray();
            return isPass;
        }

    }
}
