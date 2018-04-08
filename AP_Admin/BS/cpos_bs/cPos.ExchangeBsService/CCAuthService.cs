using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Service;
using cPos.Model.User;
using cPos.Model;

namespace cPos.ExchangeBsService
{
    /// <summary>
    /// 盘点单方法集合
    /// </summary>
    public class CCAuthService : BaseInfouAuthService
    {
        /// <summary>
        /// 批量保存盘点单：分两种，一种门店已经审批，一种门店未审批
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">门店标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="ccList">盘点单集合</param>
        /// <returns></returns>
        public bool SetCCInfoList(string Customer_Id, string Unit_Id, string User_Id, IList<CCInfo> ccList)
        {
            string strError = string.Empty;
            try
            {
                LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
                CCService ccService = new CCService();
                foreach (CCInfo ccInout in ccList) {
                    ccInout.if_flag = "1";
                }
                bool bReturn = ccService.SetCCInfoList(loggingSessionInfo, ccList, out strError);
                if (bReturn)
                {
                    return bReturn;
                }
                else
                {
                    throw new Exception(string.Format(strError, strError));
                }

            }
            catch (Exception ex)
            {
                strError = ex.ToString();
                throw (ex);
            }
        }
        /// <summary>
        /// 批量上传调整单
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">门店标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="inoutInfoList">调整单集合</param>
        /// <returns></returns>
        public bool SetAJInfoList(string Customer_Id, string Unit_Id, string User_Id, IList<InoutInfo> inoutInfoList) {
            try
            {
                LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
                AJService ajServer = new AJService();
                foreach (InoutInfo inoutInfo in inoutInfoList)
                {
                    inoutInfo.if_flag = "1";
                }
                bool bReturn = ajServer.SetAJInfoList(loggingSessionInfo, inoutInfoList);
                return bReturn;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        #region 总部盘点：CC门店在终端做好盘点单，传递到总部审批，自动生成一张调整单(审批)，下载到门店，门店审核调整单，在上传到总部，影响库存
        /// <summary>
        /// 获取总部生成的调整单（总部未审批）
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <returns></returns>
        public IList<InoutInfo> GetAJList(string Customer_Id, string Unit_Id, string User_Id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            IList<InoutInfo> inoutInfoList = new List<InoutInfo>();
            AJService ajServer = new AJService();
            inoutInfoList = ajServer.GetAJListByStatus(loggingSessionInfo, Unit_Id, "1");
            return inoutInfoList;
        }
        #endregion

        #region 修改上传下载标志
        /// <summary>
        /// 批量修改出入库单据上传标志
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="ccInfoList">盘点单单据集合</param>
        /// <param name="if_flag">上传标志1=已经上传，0=未上传</param>
        /// <returns></returns>
        public bool SetInoutIfFlag(string Customer_Id, string Unit_Id, string User_Id, IList<CCInfo> ccInfoList, string if_flag)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            //IList<CCInfo> ccInfoList = new List<CCInfo>();
            //foreach (string orderId in OrderIds) {
            //    CCInfo ccInfo = new CCInfo();
            //    ccInfo.order_id = orderId;
            //    ccInfoList.Add(ccInfo);
            //}
            CCService ccService = new CCService();
            bReturn = ccService.SetCCIfFlag(loggingSessionInfo, if_flag, ccInfoList, true);
            return bReturn;
        }
        #endregion
    }
}
