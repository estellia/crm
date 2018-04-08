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
    /// pos小票接口
    /// </summary>
    public class PosInoutAuthService:BaseInfouAuthService
    {
        /// <summary>
        /// POS小票保存
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="inoutList">inout model 集合</param>
        /// <returns>返回真假</returns>
        public bool SetPosInoutInfo(string Customer_Id, string Unit_Id, string User_Id, IList<InoutInfo> inoutList)
        {
            string strError = string.Empty;
            try
            {
                LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
                PosInoutService posInoutService = new PosInoutService();
                foreach (InoutInfo inoutInfo in inoutList)
                {
                    inoutInfo.if_flag = "1";
                }
                bool bReturn = posInoutService.SetPosInoutInfo(loggingSessionInfo, inoutList, out strError);
                if (bReturn)
                {
                    return bReturn;
                }
                else
                {
                    throw new Exception(string.Format(strError, strError));
                }
                
            }
            catch (Exception ex) {
                strError = ex.ToString();
                throw (ex);  
            }
        }
    }
}
