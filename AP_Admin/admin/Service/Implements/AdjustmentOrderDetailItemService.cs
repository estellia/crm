using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Admin.Component.SqlMappers;

namespace cPos.Admin.Service
{
    /// <summary>
    /// 调价单商品明细
    /// </summary>
    public class AdjustmentOrderDetailItemService : BaseService
    {
        #region 根据订单标识获取调价单商品明细信息
        /// <summary>
        /// 根据调价单标识获取调价单商品明细
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IList<AdjustmentOrderDetailItemInfo> GetAdjustmentOrderDetailItemListByOrderId(LoggingSessionInfo loggingSessionInfo, string orderId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                return MSSqlMapper.Instance().QueryForList<AdjustmentOrderDetailItemInfo>("AdjustmentOrderDetailItem.SelectByOrderId", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 保存调价单商品明细
        /// <summary>
        /// 保存添加单商品明细
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="adjustmentOrderInfo"></param>
        /// <returns></returns>
        public bool SetAdjustmentOrderDetailItemInfo(LoggingSessionInfo loggingSessionInfo, AdjustmentOrderInfo adjustmentOrderInfo)
        {
            try
            {
                if (adjustmentOrderInfo.AdjustmentOrderDetailItemList != null)
                {
                    foreach (AdjustmentOrderDetailItemInfo adjustmentOrderDetailItemInfo in adjustmentOrderInfo.AdjustmentOrderDetailItemList)
                    {
                        if (adjustmentOrderDetailItemInfo.order_detail_item_id == null || adjustmentOrderDetailItemInfo.order_detail_item_id.Equals(""))
                        {
                            adjustmentOrderDetailItemInfo.order_detail_item_id = NewGuid();
                        }
                    }
                    MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("AdjustmentOrderDetailItem.InsertOrUpdate", adjustmentOrderInfo);
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
