using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using cPos.Admin.Component;
using cPos.Admin.Model.Customer;
using cPos.Admin.Service.Interfaces;
using cPos.Admin.DataCrypt;
using cPos.Admin.Model.Right;
using System.Threading;
using System.Text;
using cPos.Model;
using cPos.Admin.Service;
using System.Collections;
using cPos.Admin.Service.Implements;
using cPos.Admin.Model.User;
using Jayrock.Json.Conversion;

/// <summary>
/// Summary description for MenuService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class BasicService : System.Web.Services.WebService
{
    public BasicService()
    {

    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    /// <summary>
    /// 通过条码获取商品信息
    /// </summary>
    [WebMethod]
    public string GetItemInfoByBarcode(string barcode)
    {
        cPos.Model.LoggingSessionInfo loggingSessionInfo = new cPos.Model.LoggingSessionInfo();
        var itemService = new ItemService();
        var skuService = new SkuService();
        ItemInfo item = new ItemInfo();
        SkuInfo sku = skuService.GetSkuByBarcode(barcode);
        item.SkuInfoByBarcode = sku;
        if (sku != null)
        {
            item.ItemInfoByBarcode = itemService.GetItemInfoById(loggingSessionInfo, sku.item_id);
        }
        return XMLGenerator.Serialize(item);
    }

    /// <summary>
    /// 获取用户信息与客户关系信息
    /// </summary>
    [WebMethod]
    public string GetUserUnitRelations(string userId, string type)
    {
        cPos.Model.LoggingSessionInfo loggingSessionInfo = new cPos.Model.LoggingSessionInfo();
        var customerService = new CustomerService();

        CustomerUserInfo user = new CustomerUserInfo();
        user = customerService.GetCustomerUserInfoByMobileUser(userId);

        string content = string.Empty;
        Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
        Jayrock.Json.Conversion.JsonConvert.Export(user, writer);
        content = writer.ToString();
        return content;
    }

    /// <summary>
    /// 申请客户及门店
    /// </summary>
    [WebMethod]
    public string ApplyCustomerAndUnit(string userId, string customerInfo, string unitInfo)
    {
        //userId = "AB90F0708E324BBAB016DEE062022430";
        //customerInfo = "{\"status\":0,\"customerList\":[{\"iD\":\"9057BC8D9A8E474680631DAF143342A9\",\"code\":\"mob_cus_1\",\"name\":\"mob_cus_1_name\",\"status\":0,\"creater\":{},\"createTime\":\"0001-01-01T00:00:00.0000000+08:00\",\"lastEditor\":{},\"lastEditTime\":\"0001-01-01T00:00:00.0000000+08:00\",\"lastSystemModifyStamp\":\"0001-01-01T00:00:00.0000000+08:00\"}],\"creater\":{},\"createTime\":\"0001-01-01T00:00:00.0000000+08:00\",\"lastEditor\":{},\"lastEditTime\":\"0001-01-01T00:00:00.0000000+08:00\",\"lastSystemModifyStamp\":\"0001-01-01T00:00:00.0000000+08:00\"}";
        //unitInfo = "{\"customer\":{\"status\":0,\"creater\":{},\"createTime\":\"0001-01-01T00:00:00.0000000+08:00\",\"lastEditor\":{},\"lastEditTime\":\"0001-01-01T00:00:00.0000000+08:00\",\"lastSystemModifyStamp\":\"0001-01-01T00:00:00.0000000+08:00\"},\"status\":0,\"customerShopList\":[{\"customer\":{\"status\":0,\"creater\":{},\"createTime\":\"0001-01-01T00:00:00.0000000+08:00\",\"lastEditor\":{},\"lastEditTime\":\"0001-01-01T00:00:00.0000000+08:00\",\"lastSystemModifyStamp\":\"0001-01-01T00:00:00.0000000+08:00\"},\"iD\":\"A8CCA2FFCA7844629AD7E71ED0083469\",\"code\":\"mob_unit_1\",\"name\":\"mob_unit_1_name\",\"status\":0,\"customer_id\":\"9057BC8D9A8E474680631DAF143342A9\",\"lastSystemModifyStamp\":\"0001-01-01T00:00:00.0000000+08:00\"}],\"lastSystemModifyStamp\":\"0001-01-01T00:00:00.0000000+08:00\"}";

        cPos.Model.LoggingSessionInfo loggingSessionInfo = new cPos.Model.LoggingSessionInfo();
        var customerService = new CustomerService();

        cPos.Admin.Model.Customer.CustomerInfo customer = null;
        cPos.Admin.Model.Customer.CustomerInfo unitCustomer = null;
        if (customerInfo != null && customerInfo.Length > 0)
        {
            customer = JsonConvert.Import<cPos.Admin.Model.Customer.CustomerInfo>(customerInfo);
        }
        if (unitInfo != null && unitInfo.Length > 0)
        {
            unitCustomer = JsonConvert.Import<cPos.Admin.Model.Customer.CustomerInfo>(unitInfo);
        }

        string error = string.Empty;
        // 处理客户/门店
        if ((customer != null && customer.CustomerList != null) ||
            unitCustomer != null && unitCustomer.CustomerShopList != null)
        {
            customerService.SetMobileCustomerInfoBatch(customer.CustomerList, unitCustomer.CustomerShopList, out error);
        }
        return error;
    }
}
