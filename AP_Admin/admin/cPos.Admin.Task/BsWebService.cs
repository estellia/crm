using cPos.Admin.Model.Customer;
using cPos.Admin.Service.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Task
{
    public class BsWebService
    {
        public static string GetUrl(CustomerInfo customer)
        { 
            var customerService = new CustomerService();
            string url = customer.Connect.WsUrl.Trim();
            if (!url.EndsWith(@"/")) url += @"/";
            if (customer.Connect == null || customer.Connect.WsUrl.Trim().Length == 0)
                customer.Connect = customerService.GetCustomerConnectByID(customer.ID);
            return url;
        }

        public static MonitorLogServiceReference.MonitorLogWebServiceSoapClient
            CreateMonitorLogService(CustomerInfo customer)
        {
            var service = new MonitorLogServiceReference.MonitorLogWebServiceSoapClient();
            service.Endpoint.Address = new System.ServiceModel.EndpointAddress(
                GetUrl(customer) + "webservice/MonitorLogWebService.asmx");
            return service;
        }

        public static PosInoutServiceReference.PosInoutWebServiceSoapClient
            CreatePosInoutService(CustomerInfo customer)
        {
            var service = new PosInoutServiceReference.PosInoutWebServiceSoapClient();
            service.Endpoint.Address = new System.ServiceModel.EndpointAddress(
                GetUrl(customer) + "webservice/PosInoutWebService.asmx");
            return service;
        }

        public static AdvertiseOrderServiceReference.AdvertiseOrderWebServiceSoapClient 
            CreateAdvertiseOrderService(CustomerInfo customer)
        {
            var service = new AdvertiseOrderServiceReference.AdvertiseOrderWebServiceSoapClient();
            service.Endpoint.Address = new System.ServiceModel.EndpointAddress(
                GetUrl(customer) + "webservice/AdvertiseOrderWebService.asmx");
            return service;
        }
    }
}
