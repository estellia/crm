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
    /// 调价单sku
    /// </summary>
    public class AdjustmentOrderDetailSkuService:BaseService
    {
        #region 根据订单标识获取调价单SKU明细信息
        /// <summary>
        /// 根据调价单标识获取调价单SKU明细
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IList<AdjustmentOrderDetailSkuInfo> GetAdjustmentOrderDetailSkuListByOrderId(LoggingSessionInfo loggingSessionInfo, string orderId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                return cSqlMapper.Instance().QueryForList<AdjustmentOrderDetailSkuInfo>("AdjustmentOrderDetailSku.SelectByOrderId", _ht);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
        #region 保存调价单SKU明细
        /// <summary>
        /// 保存添加单SKU明细
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="adjustmentOrderInfo"></param>
        /// <returns></returns>
        public bool SetAdjustmentOrderDetailSkuInfo(LoggingSessionInfo loggingSessionInfo, AdjustmentOrderInfo adjustmentOrderInfo)
        {
            try
            {
                if (adjustmentOrderInfo.AdjustmentOrderDetailSkuList != null)
                {
                    foreach (AdjustmentOrderDetailSkuInfo adjustmentOrderDetailSkuInfo in adjustmentOrderInfo.AdjustmentOrderDetailSkuList)
                    {
                        if (adjustmentOrderDetailSkuInfo.order_detail_sku_id == null || adjustmentOrderDetailSkuInfo.order_detail_sku_id.Equals(""))
                        {
                            adjustmentOrderDetailSkuInfo.order_detail_sku_id = NewGuid();
                        }
                    }
                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("AdjustmentOrderDetailSku.InsertOrUpdate", adjustmentOrderInfo);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region 下载调价单sku明细
        /// <summary>
        /// 下载调价单sku明细
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IList<AdjustmentOrderDetailSkuInfo> GetAdjustmentOrderDetailSkuListByOrderIdUnDownload(LoggingSessionInfo loggingSessionInfo, string orderId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("OrderId", orderId);
                return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<AdjustmentOrderDetailSkuInfo>("AdjustmentOrderDetailSku.SelectByOrderIdUnDownload", _ht);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
    }
}
