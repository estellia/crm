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
    /// 班次上传
    /// </summary>
    public class ShiftBsServer : BaseInfouAuthService
    {
        /// <summary>
        /// 班次上传
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="shiftInfoList">班次集合</param>
        /// <returns></returns>
        public bool SetShiftInfoList(string Customer_Id, string Unit_Id, string User_Id, IList<ShiftInfo> shiftInfoList)
        {
            string strError = string.Empty;
            try
            {
                LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
                ShiftService shiftService = new ShiftService();
                bool bReturn = shiftService.SetShiftInfoList(loggingSessionInfo, shiftInfoList, out strError);
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
    }
}
