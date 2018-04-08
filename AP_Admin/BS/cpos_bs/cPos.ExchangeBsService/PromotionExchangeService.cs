using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cPos.Model;
using cPos.Model.Promotion;
using cPos.Model.Exchange;
using cPos.Service;

namespace cPos.ExchangeBsService
{
    /// <summary>
    /// 促销相关服务
    /// </summary>
    public class PromotionExchangeService: BaseInfouAuthService
    {

        #region 会员

        /// <summary>
        /// 保存从终端上传过来的会员信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="userID">操作用户ID</param>
        /// <param name="unitID">门店ID</param>
        /// <param name="vips">会员信息列表</param>
        /// <returns>成功返回空串，否则返回错误描述</returns>
        public string UploadVips(string customerID, string userID, string unitID, IList<VipExchangeInfo> vips)
        {
            LoggingSessionInfo loggingSession = this.GetLoggingSessionInfo(customerID, userID, unitID);
            PromotionService p_service = new PromotionService();
            return p_service.SaveVips(loggingSession, vips);
        }

        /// <summary>
        /// 获取某个会员信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="userID">操作用户ID</param>
        /// <param name="unitID">门店ID</param>
        /// <param name="vipID">会员ID</param>
        /// <returns></returns>
        public VipInfo GetVipByID(string customerID, string userID, string unitID, string vipID)
        {
            LoggingSessionInfo loggingSession = this.GetLoggingSessionInfo(customerID, userID, unitID);
            PromotionService p_service = new PromotionService();
            return p_service.GetVipByID(loggingSession, vipID);
        }

        /// <summary>
        /// 获取某个会员信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="userID">操作用户ID</param>
        /// <param name="unitID">门店ID</param>
        /// <param name="vipNo">会员号</param>
        /// <returns></returns>
        public VipInfo GetVipByNo(string customerID, string userID, string unitID, string vipNo)
        {
            LoggingSessionInfo loggingSession = this.GetLoggingSessionInfo(customerID, userID, unitID);
            PromotionService p_service = new PromotionService();
            return p_service.GetVipByNo(loggingSession, vipNo);
        }

        /// <summary>
        /// 查询会员
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="userID">操作用户ID</param>
        /// <param name="unitID">门店ID</param>
        /// <param name="vipNo">会员号</param>
        /// <param name="vipType">会员类型</param>
        /// <param name="vipName">会员姓名</param>
        /// <param name="vipCell">会员手机</param>
        /// <param name="vipIdentityNo">身份证</param>
        /// <returns></returns>
        public IList<VipInfo> SearchVipList(string customerID, string userID, string unitID, 
            string vipNo, string vipType, string vipName, string vipCell, string vipIdentityNo)
        {
            LoggingSessionInfo loggingSession = this.GetLoggingSessionInfo(customerID, userID, unitID);
            PromotionService p_service = new PromotionService();
            Hashtable ht = new Hashtable();
            if (!string.IsNullOrEmpty(vipNo))
                ht.Add("vip_no", vipNo);
            if (!string.IsNullOrEmpty(vipType))
                ht.Add("vip_type", vipType);
            if (!string.IsNullOrEmpty(vipName))
                ht.Add("vip_name", vipName);
            if (!string.IsNullOrEmpty(vipCell))
                ht.Add("vip_cell", vipCell);
            if (!string.IsNullOrEmpty(vipIdentityNo))
                ht.Add("vip_identity_no", vipIdentityNo);
            return p_service.SelectVipListFromTerminal(loggingSession, ht);
        }

        /// <summary>
        /// 判断会员号是否已经存在
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="userID">操作用户ID</param>
        /// <param name="unitID">门店ID</param>
        /// <param name="vipID">会员ID(修改一个会员时,传入修改会员的ID)</param>
        /// <param name="vipNo">会员号</param>
        /// <returns></returns>
        public bool ExistVipNo(string customerID, string userID, string unitID, string vipID, string vipNo)
        {
            LoggingSessionInfo loggingSession = this.GetLoggingSessionInfo(customerID, userID, unitID);
            PromotionService p_service = new PromotionService();
            Hashtable ht = new Hashtable();
            ht.Add("vip_no", vipNo);
            if (!string.IsNullOrEmpty(vipID))
            {
                ht.Add("vip_id", vipID);
            }
            int count = p_service.CountVip(loggingSession, ht);
            return count > 0;
        }

        #endregion

        #region 通告
        /// <summary>
        /// 获取某个门店的通告信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="userID">操作用户ID</param>
        /// <param name="unitID">门店ID</param>
        /// <param name="startNo">上次获取的最大的通告编号</param>
        /// <returns></returns>
        public IList<AnnounceQueryInfo> SearchAnnounceList(string customerID, string userID, string unitID, int startNo)
        {
            LoggingSessionInfo loggingSession = this.GetLoggingSessionInfo(customerID, userID, unitID);
            ExchangeService e_service = new ExchangeService();
            Hashtable ht = new Hashtable();
            ht.Add("UnitID", unitID);
            ht.Add("StartNo", startNo);
            SelectObjectResultsInfo<AnnounceQueryInfo> ret = e_service.SelectDownloadAnnounceList(loggingSession, ht, 200, 0);
            return ret.Data;
        }
        #endregion
    }
}
