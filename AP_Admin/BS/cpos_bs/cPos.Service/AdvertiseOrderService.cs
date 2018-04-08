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
    /// 广告播放订单
    /// </summary>
    public class AdvertiseOrderService : BaseService
    {
        #region 设置广告播放主表信息
        /// <summary>
        /// 设置广告播放主表信息
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
                IList<AdvertiseOrderInfo> advertiseOrderInfoList = (IList<cPos.Model.Advertise.AdvertiseOrderInfo>)cXMLService.Deserialize(str, typeof(List<cPos.Model.Advertise.AdvertiseOrderInfo>));
                foreach (AdvertiseOrderInfo advertiseOrderInfo in advertiseOrderInfoList)
                {
                    advertiseOrderInfo.customer_id = customer_id;
                }
               
                //获取连接数据库信息
                LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(customer_id);

                bReturn = SetAdvertiseOrderInfoList(loggingManager, advertiseOrderInfoList, true);

                return bReturn;
               
            }
            catch (Exception ex)
            {
                throw (ex);
                return false;
            }
        }
        #endregion
        #region 保存
        /// <summary>
        /// 批量处理广告播放信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="advertiseOrderInfoList">广告订单集合</param>
        /// <param name="IsTrans">是否批处理</param>
        /// <returns></returns>
        public bool SetAdvertiseOrderInfoList(LoggingManager loggingManager, IList<AdvertiseOrderInfo> advertiseOrderInfoList, bool IsTrans)
        {
            string strDo = string.Empty;
            bool bReturn = false;
            if (IsTrans) cSqlMapper.Instance(loggingManager).BeginTransaction();
            try
            {
                
                if (advertiseOrderInfoList != null && advertiseOrderInfoList.Count > 0)
                {
                    foreach (AdvertiseOrderInfo advertiseOrderInfo in advertiseOrderInfoList)
                    {
                        bReturn = SetAdvertiseOrderInfo(loggingManager, advertiseOrderInfo);
                    }
                }
                //strError = "保存成功!"; 
                if (IsTrans) cSqlMapper.Instance(loggingManager).CommitTransaction();
                return true;
            }
            catch (Exception ex) {
                if (IsTrans) cSqlMapper.Instance(loggingManager).RollBackTransaction();
                throw (ex);
            }
        }
        /// <summary>
        /// 保存单个广告播放订单
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="advertiseOrderInfo">广告订单对象</param>
        /// <returns></returns>
        public bool SetAdvertiseOrderInfo(LoggingManager loggingManager, AdvertiseOrderInfo advertiseOrderInfo)
        {
            if (advertiseOrderInfo != null) {
                advertiseOrderInfo.customer_id = loggingManager.Customer_Id;
                if (advertiseOrderInfo.create_user_id == null || advertiseOrderInfo.create_user_id.Equals(""))
                {
                    //advertiseOrderInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    advertiseOrderInfo.create_time = GetCurrentDateTime();
                }
                if (advertiseOrderInfo.modify_user_id == null || advertiseOrderInfo.modify_user_id.Equals(""))
                {
                    //advertiseOrderInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
                    advertiseOrderInfo.modify_time = GetCurrentDateTime();
                }
                cSqlMapper.Instance(loggingManager).Update("AdvertiseOrder.InsertOrUpdate", advertiseOrderInfo);
            }
            return true;
        }

        #endregion

        #region 打包 下发到客户端
        /// <summary>
        /// 获取未打包的广告订单数量
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unit_id">门店标识</param>
        /// <returns></returns>
        public int GetAdvertiseOrderNotPackagedCount(LoggingSessionInfo loggingSessionInfo,string unit_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            _ht.Add("UnitId", unit_id);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("AdvertiseOrder.SelectUnDownloadCount", _ht);
        }
        /// <summary>
        /// 需要打包的广告订单信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="maxRowCount">当前页数量</param>
        /// <param name="startRowIndex">开始数量</param>
        /// <param name="unit_id">门店标识</param>
        /// <returns></returns>
        public IList<AdvertiseOrderInfo> GetAdvertiseOrderListPackaged(LoggingSessionInfo loggingSessionInfo, int maxRowCount, int startRowIndex,string unit_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("StartRow", startRowIndex);
            _ht.Add("EndRow", startRowIndex + maxRowCount);
            _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            _ht.Add("UnitId", unit_id);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<AdvertiseOrderInfo>("AdvertiseOrder.SelectUnDownload", _ht);
        }

        /// <summary>
        /// 设置打包批次号
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次号</param>
        /// <param name="AdvertiseOrderUnitInfoList">广告订单与门店关系集合</param>
        /// <param name="strError">错误信息返回</param>
        /// <returns></returns>
        public bool SetAdvertiseOrderBatInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, IList<AdvertiseOrderUnitInfo> AdvertiseOrderUnitInfoList, out string strError)
        {
            AdvertiseOrderInfo advertiseOrderInfo = new AdvertiseOrderInfo();
            advertiseOrderInfo.bat_no = bat_id;
            advertiseOrderInfo.advertiseOrderUnitInfoList = AdvertiseOrderUnitInfoList;
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("AdvertiseOrder.UpdateUnDownloadBatId", advertiseOrderInfo);
            strError = "Success";
            return true;
        }
        /// <summary>
        /// 更新Item表打包标识方法
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="bat_id">批次标识</param>
        /// <param name="strError">错误信息返回</param>
        /// <returns></returns>
        public bool SetAdvertiseOrderIfFlagInfo(LoggingSessionInfo loggingSessionInfo, string bat_id, out string strError)
        {
            AdvertiseOrderInfo advertiseOrderInfo = new AdvertiseOrderInfo();
            advertiseOrderInfo.bat_no = bat_id;
            //advertiseOrderInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            //advertiseOrderInfo.Modify_Time = GetCurrentDateTime();
            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("AdvertiseOrder.UpdateUnDownloadIfFlag", advertiseOrderInfo);
            strError = "Success";
            return true;
        }
        #endregion
    }
}
