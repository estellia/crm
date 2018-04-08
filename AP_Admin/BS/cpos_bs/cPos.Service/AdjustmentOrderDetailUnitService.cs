using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Components.SqlMappers;

namespace cPos.Service
{
    /// <summary>
    /// 调价单组织
    /// </summary>
    public class AdjustmentOrderDetailUnitService:BaseService
    {
        #region 根据订单标识获取调价单UNIT明细信息
        /// <summary>
        /// 根据调价单标识获取调价单UNIT明细
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IList<AdjustmentOrderDetailUnitInfo> GetAdjustmentOrderDetailUnitListByOrderId(LoggingSessionInfo loggingSessionInfo, string orderId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                return cSqlMapper.Instance().QueryForList<AdjustmentOrderDetailUnitInfo>("AdjustmentOrderDetailUnit.SelectByOrderId", _ht);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
        #region 保存调价单商品明细
        /// <summary>
        /// 保存添加单组织明细
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="adjustmentOrderInfo"></param>
        /// <returns></returns>
        public bool SetAdjustmentOrderDetailUnitInfo(LoggingSessionInfo loggingSessionInfo, AdjustmentOrderInfo adjustmentOrderInfo)
        {
            try
            {
                if (adjustmentOrderInfo.AdjustmentOrderDetailUnitList != null)
                {
                    foreach (AdjustmentOrderDetailUnitInfo adjustmentOrderDetailUnitInfo in adjustmentOrderInfo.AdjustmentOrderDetailUnitList)
                    {
                        if (adjustmentOrderDetailUnitInfo.order_detail_unit_id == null || adjustmentOrderDetailUnitInfo.order_detail_unit_id.Equals(""))
                        {
                            adjustmentOrderDetailUnitInfo.order_detail_unit_id = NewGuid();
                        }
                    }
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("AdjustmentOrderDetailUnit.InsertOrUpdate", adjustmentOrderInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
    }
}
