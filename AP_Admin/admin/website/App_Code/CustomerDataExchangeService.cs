using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using cPos.Admin.Component;
using cPos.Admin.Model.Customer;
using cPos.Admin.Service.Interfaces;
using cPos.Admin.Service.Implements;
using cPos.Admin.DataCrypt;
using cPos.Admin.AP;
using cPos.Admin.Service;

/// <summary>
/// Summary description for CustomerDataExchangeService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CustomerDataExchangeService : System.Web.Services.WebService
{

    public CustomerDataExchangeService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    private void log(LogLevel level, string functionName, string messageType, string message)
    {
        LogManager.Log(level, SystemName.AP, ModuleName.CUSTOMER_DATA_EXCHANGE_SERVICE, functionName, messageType, message);
    }

    private ICustomerService GetCustomerService()
    {
        return (ICustomerService)BusinessServiceProxyLocator.GetService(typeof(ICustomerService));
    }

    [WebMethod]
    public bool SynUser(string customerID, int typeID, string userInfo)
    {
        this.log(LogLevel.DEBUG, FunctionName.WS_SYN_USER, MessageType.PARAMATER, "customerID: " + customerID);
        this.log(LogLevel.DEBUG, FunctionName.WS_SYN_USER, MessageType.PARAMATER, "typeID: " + typeID.ToString());
        this.log(LogLevel.DEBUG, FunctionName.WS_SYN_USER, MessageType.PARAMATER, "userInfo: " + userInfo);

        //验证参数
        bool ret = ArgumentHelper.Validate("customerID", customerID, false);
        if (!ret)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_SYN_USER, "customerID", "参数不正确");
            this.log(LogLevel.INFO, FunctionName.WS_SYN_USER, MessageType.RESULT, ret.ToString());
            return ret;
        }

        //获取连接
        CustomerConnectInfo cConnect = this.GetCustomerService().GetCustomerConnectByID(customerID);
        if (cConnect == null)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_SYN_USER, "CustomerConnect", "未找到");
            this.log(LogLevel.INFO, FunctionName.WS_SYN_USER, MessageType.RESULT, "false");
            return false;
        }

        if (typeID < 1 || typeID > 5)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_SYN_USER, "typeID", "参数不正确");
            this.log(LogLevel.INFO, FunctionName.WS_SYN_USER, MessageType.RESULT, "false");
            return false;
        }

        //解密
        cPos.Admin.Service.BaseService base_service = this.GetCustomerService() as cPos.Admin.Service.BaseService;
        string s = base_service.DecryptStringByCustomer(customerID, userInfo);
        this.log(LogLevel.DEBUG, FunctionName.WS_SYN_USER, "DecryptCustomerUser", s);

        CustomerUserInfo user = (CustomerUserInfo)XMLGenerator.Deserialize(typeof(CustomerUserInfo), s);
        user.Customer.ID = customerID;
        switch (typeID)
        {
            case 1://新建
                ret = this.GetCustomerService().InsertCustomerUserFromCP(user);
                break;
            case 2://修改
                ret = this.GetCustomerService().ModifyCustomerUserFromCP(user);
                break;
            case 3://启用
                ret = this.GetCustomerService().EnableCustomerUserFromCP(user);
                break;
            case 4://停用
                ret = this.GetCustomerService().DisableCustomerUserFromCP(user);
                break;
            case 5://修改密码
                ret = this.GetCustomerService().ModifyCustomerUserPasswordFromCP(user);
                break;
            default:
                ret = false;
                break;
        }
        this.log(LogLevel.INFO, FunctionName.WS_SYN_USER, MessageType.RESULT, ret.ToString());
        return ret;
    }

    [WebMethod]
    public bool SynUnit(string customerID, int typeID, string unitInfo)
    {
        this.log(LogLevel.DEBUG, FunctionName.WS_SYS_UNIT, MessageType.PARAMATER, "customerID: " + customerID);
        this.log(LogLevel.DEBUG, FunctionName.WS_SYS_UNIT, MessageType.PARAMATER, "typeID: " + typeID.ToString());
        this.log(LogLevel.DEBUG, FunctionName.WS_SYS_UNIT, MessageType.PARAMATER, "unitInfo: " + unitInfo);

        //验证参数
        bool ret = ArgumentHelper.Validate("customerID", customerID, false);
        if (!ret)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_SYS_UNIT, "customerID", "参数不正确");
            this.log(LogLevel.INFO, FunctionName.WS_SYS_UNIT, MessageType.RESULT, ret.ToString());
            return ret;
        }

        CustomerConnectInfo cConnect = this.GetCustomerService().GetCustomerConnectByID(customerID);
        if (cConnect == null)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_SYS_UNIT, "CustomerConnect", "未找到");
            this.log(LogLevel.INFO, FunctionName.WS_SYS_UNIT, MessageType.RESULT, "false");
            return false;
        }

        if (typeID < 1 || typeID > 4)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_SYS_UNIT, "typeID", "参数不正确");
            this.log(LogLevel.INFO, FunctionName.WS_SYS_UNIT, MessageType.RESULT, "false");
            return false;
        }

        //解密
        cPos.Admin.Service.BaseService base_service = this.GetCustomerService() as cPos.Admin.Service.BaseService;
        string s = base_service.DecryptStringByCustomer(customerID, unitInfo);
        this.log(LogLevel.DEBUG, FunctionName.WS_SYS_UNIT, "DecryptCustomerUnit", s);

        CustomerShopInfo shop = (CustomerShopInfo)XMLGenerator.Deserialize(typeof(CustomerShopInfo), s);
        shop.Customer.ID = customerID;
        switch (typeID)
        {
            case 1://新建
                ret = this.GetCustomerService().InsertCustomerShopFromCP(shop);
                break;
            case 2://修改
                ret = this.GetCustomerService().ModifyCustomerShopFromCP(shop);
                break;
            case 3://启用
                ret = this.GetCustomerService().EnableCustomerShopFromCP(shop);
                break;
            case 4://停用
                ret = this.GetCustomerService().DisableCustomerShopFromCP(shop);
                break;
            default:
                ret = false;
                break;
        }
        this.log(LogLevel.INFO, FunctionName.WS_SYS_UNIT, MessageType.RESULT, ret.ToString());
        return ret;
    }

    [WebMethod]
    public bool SynTerminal(string customerID, int typeID, string terminalInfo)
    {
        this.log(LogLevel.DEBUG, FunctionName.WS_SYS_TERMINAL, MessageType.PARAMATER, "customerID: " + customerID);
        this.log(LogLevel.DEBUG, FunctionName.WS_SYS_TERMINAL, MessageType.PARAMATER, "typeID: " + typeID.ToString());
        this.log(LogLevel.DEBUG, FunctionName.WS_SYS_TERMINAL, MessageType.PARAMATER, "terminalInfo: " + terminalInfo);

        //验证参数
        bool ret = ArgumentHelper.Validate("customerID", customerID, false);
        if (!ret)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_SYS_TERMINAL, "customerID", "参数不正确");
            this.log(LogLevel.INFO, FunctionName.WS_SYS_TERMINAL, MessageType.RESULT, ret.ToString());
            return ret;
        }

        CustomerConnectInfo cConnect = this.GetCustomerService().GetCustomerConnectByID(customerID);
        if (cConnect == null)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_SYS_TERMINAL, "CustomerConnect", "未找到");
            this.log(LogLevel.INFO, FunctionName.WS_SYS_TERMINAL, MessageType.RESULT, "false");
            return false;
        }

        if (typeID < 1 || typeID > 2)
        {
            this.log(LogLevel.ERROR, FunctionName.WS_SYS_TERMINAL, "typeID", "参数不正确");
            this.log(LogLevel.INFO, FunctionName.WS_SYS_TERMINAL, MessageType.RESULT, "false");
            return false;
        }

        //解密
        cPos.Admin.Service.BaseService base_service = this.GetCustomerService() as cPos.Admin.Service.BaseService;
        string s = base_service.DecryptStringByCustomer(customerID, terminalInfo);
        this.log(LogLevel.DEBUG, FunctionName.WS_SYS_TERMINAL, "DecryptCustomerTerminal", s);

        CustomerTerminalInfo terminal = (CustomerTerminalInfo)XMLGenerator.Deserialize(typeof(CustomerTerminalInfo), s);
        terminal.Customer.ID = customerID;
        switch (typeID)
        {
            case 1://新建
                ret = this.GetCustomerService().InsertCustomerTerminalFromCP(terminal);
                break;
            case 2://修改
                ret = this.GetCustomerService().ModifyCustomerTerminalFromCP(terminal);
                break;
            default:
                ret = false;
                break;
        }
        this.log(LogLevel.INFO, FunctionName.WS_SYS_TERMINAL, MessageType.RESULT, ret.ToString());
        return ret;
    }

    [WebMethod]
    public bool CanAddUser(string customerID)
    {
        this.log(LogLevel.DEBUG, FunctionName.WS_CAN_ADD_USER, MessageType.PARAMATER, "customerID: " + customerID);
        bool ret = this.GetCustomerService().CanCreateUser(customerID);
        this.log(LogLevel.DEBUG, FunctionName.WS_CAN_ADD_USER, MessageType.RESULT, ret.ToString());
        return ret;
    }

    [WebMethod]
    public bool CanAddShop(string customerID)
    {
        this.log(LogLevel.DEBUG, FunctionName.WS_CAN_ADD_UNIT, MessageType.PARAMATER, "customerID: " + customerID);
        bool ret = this.GetCustomerService().CanCreateShop(customerID);
        this.log(LogLevel.DEBUG, FunctionName.WS_CAN_ADD_UNIT, MessageType.RESULT, ret.ToString());
        return ret;
    }

    [WebMethod]
    public bool CanAddTerminal(string customerID)
    {
        this.log(LogLevel.DEBUG, FunctionName.WS_CAN_ADD_TERMINAL, MessageType.PARAMATER, "customerID: " + customerID);
        bool ret = this.GetCustomerService().CanCreateTerminal(customerID);
        this.log(LogLevel.DEBUG, FunctionName.WS_CAN_ADD_TERMINAL, MessageType.RESULT, ret.ToString());
        return ret;
    }
}
