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
    public class AdvertiseService : BaseService
    {
        #region
        /// <summary>
        /// 设置广告主表信息
        /// </summary>
        /// <param name="str">字符窜</param>
        /// <param name="customer_id">客户标识</param>
        /// <returns></returns>
        public bool SetAdvertiseOrderXML(string str, string customer_id)
        {
            bool bReturn = false;

            try
            {

                //反序列化
                IList<AdvertiseInfo> advertiseInfoList = (IList<cPos.Model.Advertise.AdvertiseInfo>)cXMLService.Deserialize(str, typeof(List<cPos.Model.Advertise.AdvertiseInfo>));
               

                foreach (AdvertiseInfo advertiseOrderInfo in advertiseInfoList)
                {
                    advertiseOrderInfo.customer_id = customer_id;
                }

                //获取连接数据库信息
                LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(customer_id);

                bReturn = SetAdvertiseInfoList(loggingManager, advertiseInfoList, true);

                

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
        /// 批量设置广告信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="advertiseInfoList">广告信息集合</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <returns></returns>
        public bool SetAdvertiseInfoList(LoggingManager loggingManager, IList<AdvertiseInfo> advertiseInfoList, bool IsTrans)
        {
            bool bReturn = false;
            if (IsTrans) cSqlMapper.Instance(loggingManager).BeginTransaction();
            try
            {
                
                if (advertiseInfoList != null && advertiseInfoList.Count > 0)
                {
                    foreach (AdvertiseInfo advertiseInfo in advertiseInfoList)
                    {
                        bReturn = SetAdvertiseInfo(loggingManager, advertiseInfo);
                    }
                }

                if (IsTrans) cSqlMapper.Instance(loggingManager).CommitTransaction();
                return bReturn;
            }
            catch (Exception ex) {
                if (IsTrans) cSqlMapper.Instance(loggingManager).RollBackTransaction();
                throw (ex);
            }
            
        }
        /// <summary>
        /// 设置单个广告信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="advertiseInfo">广告信息</param>
        /// <returns></returns>
        public bool SetAdvertiseInfo(LoggingManager loggingManager, AdvertiseInfo advertiseInfo)
        {
            if (advertiseInfo != null)
            {
                advertiseInfo.customer_id = loggingManager.Customer_Id;
                if (advertiseInfo.create_user_id == null || advertiseInfo.create_user_id.Equals(""))
                {
                    //advertiseInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    advertiseInfo.create_time = GetCurrentDateTime();
                }
                if (advertiseInfo.modify_user_id == null || advertiseInfo.modify_user_id.Equals(""))
                {
                    //advertiseInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    advertiseInfo.modify_time = GetCurrentDateTime();
                }
                
                cSqlMapper.Instance(loggingManager).Update("Advertise.InsertOrUpdate", advertiseInfo);
            }
            return true;
        }
        #endregion

        #region 下发广告到终端

        /// <summary>
        /// 根据订单获取广告信息集合
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="order_id">订单标识</param>
        /// <returns></returns>
        public IList<AdvertiseInfo> GetAdvertiseInfoListPackaged(LoggingSessionInfo loggingSessionInfo, string order_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("OrderId", order_id);
            _ht.Add("CustomerId",loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<AdvertiseInfo>("Advertise.SelectByOrderId", _ht);
        }

        #endregion
    }
}
