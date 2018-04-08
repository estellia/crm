using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using cPos.WebServices;
using cPos.Components;

namespace cPos.Service
{
    /// <summary>
    /// 获取管理平台信息
    /// </summary>
    public class cLoggingManager
    {
        /// <summary>
        /// 根据客户标识，获取客户信息
        /// </summary>
        /// <param name="Customer_Id"></param>
        /// <returns></returns>
        public LoggingManager GetLoggingManager(string Customer_Id)
        {
            cPos.WebServices.AuthManagerWebServices.AuthService AuthWebService = new cPos.WebServices.AuthManagerWebServices.AuthService();
            AuthWebService.Url = System.Configuration.ConfigurationManager.AppSettings["sso_url"] + "/authservice.asmx";
            string str = AuthWebService.GetCustomerDBConnectionString(Customer_Id);
            
            cPos.Model.LoggingManager myLoggingManager = (cPos.Model.LoggingManager)cXMLService.Deserialize(str, typeof(cPos.Model.LoggingManager));
            myLoggingManager.Customer_Id = Customer_Id;
            return myLoggingManager;
        }

    }
}
