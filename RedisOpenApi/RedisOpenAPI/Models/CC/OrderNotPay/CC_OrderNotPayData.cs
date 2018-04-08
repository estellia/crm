using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC.OrderNotPay
{
    public class CC_OrderNotPayData
    {

        public CC_DataInfo first { get; set; }
        public CC_DataInfo orderProductPrice { get; set; }
        public CC_DataInfo orderProductName { get; set; }
        public CC_DataInfo orderAddress { get; set; }
        public CC_DataInfo orderName { get; set; }
        public CC_DataInfo remark { get; set; }
    }
}
