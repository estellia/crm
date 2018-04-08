using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

using cPos.Admin.Component;
using cPos.Admin.Model.Customer;
using cPos.Admin.Service.Interfaces;
using cPos.Admin.DataCrypt;
using cPos.Admin.ExchangeModel.SSO;

using cPos.Admin.Service;
using cPos.Admin.Service.Implements;
using cPos.Admin.SSO;

/// <summary>
/// Summary description for AuthService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AuthService : System.Web.Services.WebService
{
    public AuthService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    private ICustomerService GetCustomerService()
    {
        return (ICustomerService)BusinessServiceProxyLocator.GetService(typeof(ICustomerService));
    }

    private void log(LogLevel level, string functionName, string messageType, string message)
    {
        LogManager.Log(level, SystemName.SSO, ModuleName.AUTHENTICATION_SERVICE, functionName, messageType, message);
    }

    [WebMethod]
    public string GetLoginUserInfo(string token)
    {
        this.log(LogLevel.DEBUG, FunctionName.WS_GET_LOGIN_USER_INFO, MessageType.PARAMATER, string.Format("token:{0}", token));
        
        //验证参数
        bool ret = ArgumentHelper.Validate("token", token, false);
        if (!ret)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_GET_LOGIN_USER_INFO, "token", "参数不正确");
            this.log(LogLevel.INFO, FunctionName.WS_GET_LOGIN_USER_INFO, MessageType.RESULT, "");
            return "";
        }

        //取token的登录用户ID
        string cu_id = this.GetCustomerService().GetLoginCustomerUserIDByToken(token);
        if (string.IsNullOrEmpty(cu_id))
        {
            this.log(LogLevel.ERROR, FunctionName.WS_GET_LOGIN_USER_INFO, "CustomerUserID", "未找到");
            this.log(LogLevel.INFO, FunctionName.WS_GET_LOGIN_USER_INFO, MessageType.RESULT, "");
            return "";
        }
        this.log(LogLevel.DEBUG, FunctionName.WS_GET_LOGIN_USER_INFO, "CustomerUserID", cu_id);

        //取登录用户
        CustomerUserInfo cUser = this.GetCustomerService().GetCustomerUserByID(cu_id);
        if (cUser == null)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_GET_LOGIN_USER_INFO, "CustomerUser", "未找到");
            this.log(LogLevel.INFO, FunctionName.WS_GET_LOGIN_USER_INFO, MessageType.RESULT, "");
            return "";
        }

        LoggingUserInfo user = new LoggingUserInfo();
        user.CustomerID = cUser.Customer.ID;
        user.CustomerCode = cUser.Customer.Code;
        user.CustomerName = cUser.Customer.Name;
        user.UserID = cUser.ID;
        user.UserName = cUser.Name;
        CustomerConnectInfo cConnect = this.GetCustomerService().GetCustomerConnectByID(cUser.Customer.ID);
        if (cConnect != null)
        {
            user.ConnectionString = cConnect.DBConnectionString;
        }
        string s = XMLGenerator.Serialize(user);
        this.log(LogLevel.DEBUG, FunctionName.WS_GET_LOGIN_USER_INFO, "DecryptResult", s);

        //加密
        cPos.Admin.Service.BaseService base_service = this.GetCustomerService() as cPos.Admin.Service.BaseService;
        string output = base_service.EncryptStringByKeyFile(cConnect.KeyFile, s);
        this.log(LogLevel.DEBUG, FunctionName.WS_GET_LOGIN_USER_INFO, MessageType.RESULT, output);

        return output;
    }

    [WebMethod]
    public string GetCustomerDBConnectionString(string customerID)
    {
        this.log(LogLevel.DEBUG, FunctionName.WS_GET_CUSTOMER_DB_CONNECTION_STRING, 
            MessageType.PARAMATER, string.Format("customerID:{0}", customerID));

        //验证参数
        bool ret = ArgumentHelper.Validate("customerID", customerID, false);
        if (!ret)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_GET_LOGIN_USER_INFO, "customerID", "参数不正确");
            this.log(LogLevel.INFO, FunctionName.WS_GET_LOGIN_USER_INFO, MessageType.RESULT, "");
            return "";
        }

        //取连接
        CustomerConnectInfo cConnect = this.GetCustomerService().GetCustomerConnectByID(customerID);
        if (cConnect == null)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_GET_LOGIN_USER_INFO, "CustomerConnect", "未找到");
            this.log(LogLevel.INFO, FunctionName.WS_GET_LOGIN_USER_INFO, MessageType.RESULT, "");
            return "";
        }

        //生成XML
        LoggingUserInfo user = new LoggingUserInfo();
        user.CustomerID = customerID;
        user.CustomerCode = cConnect.Customer.Code;
        user.CustomerName = cConnect.Customer.Name;
        user.ConnectionString = cConnect.DBConnectionString;
        string s = XMLGenerator.Serialize(user);
        this.log(LogLevel.DEBUG, FunctionName.WS_GET_LOGIN_USER_INFO, "DecryptResult", s);

        //加密
        cPos.Admin.Service.BaseService base_service = this.GetCustomerService() as cPos.Admin.Service.BaseService;
        string output = base_service.EncryptStringByKeyFile(cConnect.KeyFile, s);
        this.log(LogLevel.DEBUG, FunctionName.WS_GET_LOGIN_USER_INFO, MessageType.RESULT, output);

        return output;
    }

    [WebMethod]
    public string GetCustomerInfo(string customer_code)
    {
        CustomerInfo customerInfo = new CustomerInfo();
        customerInfo = new cPos.Admin.Service.Implements.InitialService().GetCustomerInfoByCode(customer_code);
        string s = XMLGenerator.Serialize(customerInfo);
        return s;
    }
}
