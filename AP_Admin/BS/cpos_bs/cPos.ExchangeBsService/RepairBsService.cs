using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Service;
using cPos.Model;

namespace cPos.ExchangeBsService
{
    /// <summary>
    /// 一键报修
    /// </summary>
    public class RepairBsService : BaseInfouAuthService
    {
        #region 获取一键上传信息集合
        /// <summary>
        /// 一键上传信息保存
        /// </summary>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">组织标识</param>
        /// <param name="User_Id">用户标识</param>
        /// <param name="repairInfoList">一键报修信息集合</param>
        /// <returns></returns>
        public bool SetRepairInfo(string Customer_Id, string Unit_Id, string User_Id, IList<RepairInfo> repairInfoList)
        {
            string strError = string.Empty;
            try
            {
                LoggingSessionInfo loggingSessionInfo = GetLoggingSessionInfo(Customer_Id, User_Id, Unit_Id);
                RepairService repairService = new RepairService();

                bool bReturn = repairService.SetRepairInfo(loggingSessionInfo, repairInfoList, out strError);
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
        #endregion
    }
}
