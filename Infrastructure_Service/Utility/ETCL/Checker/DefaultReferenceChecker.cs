using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ETCL.Checker;
using System.Data;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Checker
{
    public class DefaultReferenceChecker : IChecker
    {
        /// <summary>
        /// 引用项缓存
        /// </summary>
        private Dictionary<string, object[]> _dictCachedReference = new Dictionary<string, object[]>();
        /// <summary>
        /// 检查所需要的ETCL工作簿信息
        /// </summary>
        private ETCLWorkSheetInfo _etclWorksheet;
        /// <summary>
        /// 检查所需要的ETCL列信息
        /// </summary>
        private ETCLColumn _etclColumn;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        private BasicUserInfo _userInfo;
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="pBasicUserInfo">当前用户信息</param>
        /// <param name="pETCLWorkSheetInfo">ETCL工作簿信息</param>
        /// <param name="pETCLColumn">ETCL列信息</param>
        public DefaultReferenceChecker(BasicUserInfo pBasicUserInfo, ETCLWorkSheetInfo pETCLWorkSheetInfo, ETCLColumn pETCLColumn)
        {
            this._userInfo = pBasicUserInfo;
            this._etclWorksheet = pETCLWorkSheetInfo;
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
            bool isPass = true;
            if (pPropertyValue == null || string.IsNullOrWhiteSpace(pPropertyValue.ToString()))//空值不检查依赖项
                return true;

            //检查当前XLS中是否有此依赖表
            ETCLTable referenceTable = this._etclWorksheet.GetTableByName(this._etclColumn.ReferenceTableName);
            if (referenceTable == null)
                throw new ETCLException(200,string.Format("未找到关联表的相关定义，请修改XML配置。列名:{0},关联表:{1}", this._etclColumn.ColumnName, this._etclColumn.ReferenceTableName));

            //通过反射从数据库中读取此依赖表信息
            if (!_dictCachedReference.ContainsKey(this._etclColumn.ReferenceTableName))
            {
                object[] oTempEntities = Reflection.DynamicReflectionHandler.QueryAll(referenceTable, new object[] { this._userInfo });
                _dictCachedReference.Add(this._etclColumn.ReferenceTableName, oTempEntities);
            }
            object[] oEntities = _dictCachedReference[this._etclColumn.ReferenceTableName];
            foreach (var entityItem in oEntities)
            {
                object oTempValue = Reflection.DynamicReflectionHandler.GetProperty(entityItem, this._etclColumn.ReferenceTextColumnName);
                if (oTempValue.Equals(pPropertyValue))
                {
                    isPass = false;
                    oResult = new ETCLCommonResultItem();
                    oResult.OPType = OperationType.ForeignKeyDependence;
                    oResult.Message = string.Format("[{数据项:{0}}]的数据在关联表中不存在.", pPropertyText);
                }
            }
            return isPass;
        }
    }
}
