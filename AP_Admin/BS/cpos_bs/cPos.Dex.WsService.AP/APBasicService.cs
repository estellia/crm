using cPos.Admin.Model.User;
using cPos.Dex.ContractModel;
using cPos.Model;
using Jayrock.Json.Conversion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace cPos.Dex.WsService.AP
{
    public class APBasicService
    {
        /// <summary>
        /// 通过条码获取商品信息
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public ItemInfo GetItemInfoByBarcode(string barcode)
        {
            ItemInfo item = new ItemInfo();
            var basicService = new APBasicServiceReference.BasicServiceSoapClient();
            basicService.Endpoint.Address = new System.ServiceModel.EndpointAddress(
                ConfigurationManager.AppSettings["ap_url"].ToString() + "/WebServices/BasicService.asmx");

            var str = basicService.GetItemInfoByBarcode(barcode);
            item = (ItemInfo)cXMLService.Deserialize(str, typeof(ItemInfo));
            return item;
        }

        /// <summary>
        /// 获取用户信息与客户关系信息
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public cPos.Admin.Model.Customer.CustomerUserInfo GetUserUnitRelations(string userId, string type)
        {
            var user = new cPos.Admin.Model.Customer.CustomerUserInfo();
            var basicService = new APBasicServiceReference.BasicServiceSoapClient();
            basicService.Endpoint.Address = new System.ServiceModel.EndpointAddress(
                ConfigurationManager.AppSettings["ap_url"].ToString() + "/WebServices/BasicService.asmx");

            var str = basicService.GetUserUnitRelations(userId, type);
            return JsonConvert.Import<cPos.Admin.Model.Customer.CustomerUserInfo>(str);
        }

        /// <summary>
        /// 申请客户及门店
        /// </summary>
        public string ApplyCustomerAndUnit(string userId, string customerInfo, string unitInfo)
        {
            var basicService = new APBasicServiceReference.BasicServiceSoapClient();
            basicService.Endpoint.Address = new System.ServiceModel.EndpointAddress(
                ConfigurationManager.AppSettings["ap_url"].ToString() + "/WebServices/BasicService.asmx");

            var str = basicService.ApplyCustomerAndUnit(userId, customerInfo, unitInfo);
            return str;
        }
    }
}
