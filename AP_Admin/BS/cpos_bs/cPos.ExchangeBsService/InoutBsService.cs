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
    /// 出入库单据类
    /// </summary>
    public class InoutBsService : BaseInfouAuthService
    {
        /// <summary>
        /// 批量修改出入库单据上传标志
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="InoutInfoList">出入库单据集合</param>
        /// <param name="if_flag">上传标志1=已经上传，0=未上传</param>
        /// <returns></returns>
        public bool SetInoutIfFlag(string Customer_Id, string Unit_Id, string User_Id, IList<InoutInfo> InoutInfoList, string if_flag)
        {
             LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            InoutService inoutService = new InoutService();
            bReturn = inoutService.SetInoutIfFlag(loggingSessionInfo, if_flag, InoutInfoList, true);
            return bReturn;
        }

        /// <summary>
        /// 更新单据打包标识方法
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="bat_id">批次号</param>
        /// <returns>true=成功，false=失败</returns>
        public bool SetInoutIfFlagInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        {
            LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
            bool bReturn = false;
            string strError = string.Empty;

            InoutService inoutService = new InoutService();
            bReturn = inoutService.SetInoutOrderIfFlagInfo(loggingSessionInfo, bat_id, out strError);
            return bReturn;
        }
    }
}
