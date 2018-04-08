using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.Checker;
using JIT.Utility.Log;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.DataStructure;
using JIT.Utility.ETCL.Base;

namespace JIT.Utility.ETCL.Checker
{
    /// <summary>
    /// 默认重复项检查器
    /// </summary>
    public class DefaultDuplicateChecker : IChecker
    {
        /// <summary>
        /// 数据库表缓存
        /// </summary>
        Dictionary<string, object[]> dictDBTableCache = new Dictionary<string, object[]>();
        /// <summary>
        /// 表、列 中的值缓存
        /// </summary>
        Dictionary<string, object[]> dictDBTableColumnValueCache = new Dictionary<string,object[]>();
        /// <summary>
        /// 当前处理请求的用户信息
        /// </summary>
        private BasicUserInfo _userInfo; 
        /// <summary>
        /// 当前检查所需的ETCL表信息
        /// </summary>
        private ETCLTable _etclTable;
        /// <summary>
        /// 当前检查所需的列信息
        /// </summary>
        private ETCLColumn _etclColumn;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="pETCLTable">ETCL表</param>
        /// <param name="pETCLColumn">ETCL列</param>
        public DefaultDuplicateChecker(BasicUserInfo pUserInfo, ETCLTable pETCLTable, ETCLColumn pETCLColumn)
        {
            this._userInfo = pUserInfo;
            this._etclTable = pETCLTable;
            this._etclColumn = pETCLColumn;
        }

        /// <summary>
        /// 执行检查
        /// </summary>
        /// <param name="pPropertyValue">待检查的数据值</param>
        /// <param name="pPropertyText">此数据的属性名称（列描述）</param>
        /// <param name="pRowData">当前数据项的信息</param>
        /// <param name="oResult">检查结果</param>
        /// <returns>TRUE：通过检查，FALSE：未通过检查。</returns>
        public bool Process(object pPropertyValue, string pPropertyText, IETCLDataItem pRowData, ref IETCLResultItem oResult)
        {
            //int rowIndex =pRowData _etclTable.Entities.IndexOf((CommonExcelDataItem)pRowData);
            bool isPass = true;
            if (pPropertyValue == null || string.IsNullOrWhiteSpace(pPropertyValue.ToString()))//空值不检查是否重复
                return isPass;
            int currentIndex = 0;
            List<int> listDuplicateItemIndex =new List<int>();            
            if (isPass)
            {
                if (pPropertyValue != null && this._etclColumn.ValueCountCache.ContainsKey(pPropertyValue) && this._etclColumn.ValueCountCache[pPropertyValue] > 1)
                {
                    isPass = false;
                    listDuplicateItemIndex.Add(currentIndex - 1);
                }
            }

            string duplicatedRowIndex = string.Empty;
            //检测数据库中是否已存在此值
            if (isPass)
            {
                object[] oAllEntities;
                if (dictDBTableCache.ContainsKey(this._etclTable.TableName))
                {
                    oAllEntities = dictDBTableCache[this._etclTable.TableName];
                }
                else
                {
                    oAllEntities = Reflection.DynamicReflectionHandler.QueryAll(this._etclTable, new object[] { _userInfo });
                    dictDBTableCache.Add(this._etclTable.TableName, oAllEntities);
                }
                    object[] cellValues = new object[oAllEntities.Length]; 
                    if (dictDBTableColumnValueCache.ContainsKey(this._etclTable.TableName + "|" + this._etclColumn.ColumnName))
                    {
                        cellValues = dictDBTableColumnValueCache[this._etclTable.TableName + "|" + this._etclColumn.ColumnName];
                    }
                    else
                    {
                        cellValues = new object[oAllEntities.Length];
                        for (int i = 0; i < oAllEntities.Length; i++)
                        {
                            object entityItem = oAllEntities[i];
                            cellValues[i] = Reflection.DynamicReflectionHandler.GetProperty(entityItem, this._etclColumn.ColumnName);
                        }
                        dictDBTableColumnValueCache.Add(this._etclTable.TableName + "|" + this._etclColumn.ColumnName, cellValues);
                    }
                    if (pPropertyValue != null && cellValues.Contains(pPropertyValue))
                    {
                        duplicatedRowIndex = "-1";
                        isPass = false;
                    }                
            }

            if (!isPass)
            {//此单元格的值与其它单元格有重复,记录错误信息.
                oResult.OPType = OperationType.DataRepeatly;
                if (duplicatedRowIndex == string.Empty)
                {
                    foreach (var item in listDuplicateItemIndex)
                    {
                        duplicatedRowIndex += (item + 2).ToString() + ",";//序号应从2开始（第一行是列头，item为0序号应为1）
                    }
                    if (duplicatedRowIndex.Length > 0)
                        duplicatedRowIndex = duplicatedRowIndex.Substring(0, duplicatedRowIndex.Length - 1);
                }
                if (duplicatedRowIndex == "-1")
                {
                    oResult.Message = string.Format("[检测到重复项(在数据库表中).列:【{0}】].", pPropertyText);
                }
                else
                {
                    oResult.Message = string.Format("[检测到重复项(在当前工作表中).列:【{0}】,行号:【{1}】].", pPropertyText, duplicatedRowIndex);
                }
            }
            return isPass;
        }
    }
}
