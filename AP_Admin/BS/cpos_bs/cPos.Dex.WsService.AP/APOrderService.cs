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
    public class APOrderService
    {
        /// <summary>
        /// 保存CCOrder
        /// </summary>
        public string SetCCOrderList(string userId, string type, string orderInfo)
        {
            var orderService = new APOrderServiceReference.OrderServiceSoapClient();
            orderService.Endpoint.Address = new System.ServiceModel.EndpointAddress(
                ConfigurationManager.AppSettings["ap_url"].ToString() + "/WebServices/OrderService.asmx");

            var str = orderService.SetCCOrderList(userId, type, orderInfo);
            return str;
        }
    }
}
