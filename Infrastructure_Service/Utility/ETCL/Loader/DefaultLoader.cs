using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.Utility.Log;
using JIT.Utility.ETCL.Interface;
using JIT.Utility.ETCL.DataStructure;

namespace JIT.Utility.ETCL.Loader
{
    /// <summary>
    /// 默认的加载器（将数据保存到目标数据库）
    /// </summary>
    public class DefaultLoader : ILoader
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public DefaultLoader()
        {
        }

        /// <summary>
        /// 执行加载
        /// </summary>
        /// <param name="pDataRow">ETCL数据项</param>
        /// <param name="pUserInfo">用户信息</param>
        /// <param name="oResult">抽取结果详情</param>
        /// <param name="pTran">数据库事务</param>
        /// <returns>TRUE：抽取成功，FALSE：抽取失败。</returns>
        public bool Process(IETCLDataItem[] pDataRow, BasicUserInfo pUserInfo, out IETCLResultItem[] oResult, IDbTransaction pTran)
        {
            bool isPass = true;
            List<ETCLCommonResultItem> lstDuplicateCell = new List<ETCLCommonResultItem>();
            CommonExcelDataItem[] excelDataItems = (CommonExcelDataItem[])pDataRow;
            foreach (var entityItem in excelDataItems)
            {
                try
                {
                    object oQueryResult = Reflection.DynamicReflectionHandler.CreateEntity(entityItem.Table, new object[] { pUserInfo }, entityItem.Entity, pTran);
                    
                }
                catch (Exception ex)
                {
                    Loggers.Exception(new ExceptionLogInfo(ex));
                    isPass = false;
                    ETCLCommonResultItem result = new ETCLCommonResultItem();
                    result.OPType = OperationType.CreateEntity;
                    result.TableIndex = entityItem.Table.Index.Value;
                    result.RowIndex = entityItem.Index.Value;
                    result.Message = string.Format("[实体创建失败(添加到数据库表).].");
                    lstDuplicateCell.Add(result);
                }

            }
            oResult = lstDuplicateCell.ToArray();
            return isPass;
        }
    }
}
