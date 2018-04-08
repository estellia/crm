using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Components.SqlMappers;
using cPos.Model.Advertise;

namespace cPos.Service
{
    /// <summary>
    /// 广告订单与门店关系
    /// </summary>
    public class AdvertiseOrderUnitService
    {

        #region
        /// <summary>
        /// 设置广告订单与门店关系
        /// </summary>
        /// <param name="str">字符窜</param>
        /// <param name="customer_id">客户标识</param>
        /// <returns></returns>
        public bool SetAdvertiseOrderUnitInfoXML(string str, string customer_id)
        {
            bool bReturn = false;

            try
            {

                //反序列化
                IList<AdvertiseOrderUnitInfo> advertiseOrderUnitInfoList = (IList<AdvertiseOrderUnitInfo>)cXMLService.Deserialize(str, typeof(List<cPos.Model.Advertise.AdvertiseOrderUnitInfo>));
                foreach (AdvertiseOrderUnitInfo advertiseOrderadvertiseInfoInfo in advertiseOrderUnitInfoList)
                {
                    advertiseOrderadvertiseInfoInfo.customer_id = customer_id;
                }

                //获取连接数据库信息
                LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(customer_id);

                bReturn = SetAdvertiseOrderUnitInfoList(loggingManager, advertiseOrderUnitInfoList, true);

                return bReturn;

            }
            catch (Exception ex)
            {
                bReturn = false;
                throw (ex);
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 批量处理广告订单与门店关系
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="advertiseOrderAdvertiseInfoList">关系集合</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <returns></returns>
        public bool SetAdvertiseOrderUnitInfoList(LoggingManager loggingManager
            , IList<AdvertiseOrderUnitInfo> advertiseOrderUnitInfoList, bool IsTrans)
        {
            if (IsTrans) cSqlMapper.Instance(loggingManager).BeginTransaction();
            try
            {
                IList<AdvertiseOrderInfo> advertiseOrderInfolist = new List<AdvertiseOrderInfo>();

                if (advertiseOrderUnitInfoList != null)
                {
                    #region 获取广告播放订单主信息集合
                    foreach (AdvertiseOrderUnitInfo aoa in advertiseOrderUnitInfoList)
                    {
                        AdvertiseOrderInfo advertiseOrderInfo = new AdvertiseOrderInfo();
                        if (advertiseOrderInfolist == null || advertiseOrderInfolist.Count == 0)
                        {
                            advertiseOrderInfo.order_id = aoa.order_id;
                            advertiseOrderInfo.customer_id = aoa.customer_id;
                            advertiseOrderInfolist.Add(advertiseOrderInfo);
                        }
                        else
                        {
                            bool bSelect = true;
                            foreach (AdvertiseOrderInfo aoi in advertiseOrderInfolist)
                            {
                                if (aoi.customer_id.Equals(aoa.customer_id) && aoi.order_id.Equals(aoa.order_id))
                                {
                                    bSelect = false;
                                    break;
                                }
                            }
                            if (bSelect)
                            {
                                advertiseOrderInfo.order_id = aoa.order_id;
                                advertiseOrderInfo.customer_id = aoa.customer_id;
                                advertiseOrderInfolist.Add(advertiseOrderInfo);
                            }
                        }
                    }
                    #endregion

                    #region 分别处理订单信息
                    if (advertiseOrderInfolist != null)
                    {
                        foreach (AdvertiseOrderInfo aoi in advertiseOrderInfolist)
                        {
                            IList<AdvertiseOrderUnitInfo> aoaList1 = new List<AdvertiseOrderUnitInfo>();
                            foreach (AdvertiseOrderUnitInfo aoa in advertiseOrderUnitInfoList)
                            {
                                if (aoi.customer_id.Equals(aoa.customer_id) && aoi.order_id.Equals(aoa.order_id))
                                {
                                    aoaList1.Add(aoa);
                                }
                            }
                            aoi.advertiseOrderUnitInfoList = aoaList1;
                            bool b = SetAdvertiseOrderUnitByOrder(loggingManager, aoi);
                        }
                    }
                    #endregion
                }
                if (IsTrans) cSqlMapper.Instance(loggingManager).CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                if (IsTrans) cSqlMapper.Instance(loggingManager).RollBackTransaction();
                throw (ex);
            }
        }
        /// <summary>
        /// 设置广告订单与门店关系信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="advertiseOrderInfo">广告订单主信息</param>
        /// <returns></returns>
        public bool SetAdvertiseOrderUnitByOrder(LoggingManager loggingManager, AdvertiseOrderInfo advertiseOrderInfo)
        {
            //处理广告订单与广告关系
            cSqlMapper.Instance(loggingManager).Update("AdvertiseOrder.HandleUnit", advertiseOrderInfo);
            return true;
        }
        #endregion
    }
}
