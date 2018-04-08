using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC.CouponNotice
{
    public class CC_CouponNoticeData
    {
        public CC_DataInfo first { get; set; }
        public CC_DataInfo keyword1 { get; set; }
        public CC_DataInfo keyword2 { get; set; }
        public CC_DataInfo keyword3 { get; set; }
        public CC_DataInfo keyword4 { get; set; }
        public CC_DataInfo keyword5 { get; set; }
        public CC_DataInfo remark { get; set; }
    }

     //CouponsArrivalData.first = new DataInfo() { value = WXTMConfigData.FirstText, color = WXTMConfigData.FirstColour };
     //       CouponsArrivalData.keyword1 = new DataInfo() { value = CouponCode, color = WXTMConfigData.Colour1 };//券码
     //       CouponsArrivalData.keyword2 = new DataInfo() { value = CouponName, color = WXTMConfigData.Colour1 };//券名称
     //       CouponsArrivalData.keyword3 = new DataInfo() { value = CouponCount, color = WXTMConfigData.Colour1 };//可用数量
     //       CouponsArrivalData.keyword4 = new DataInfo() { value = ValidityData, color = WXTMConfigData.Colour1 };//有效期
     //       CouponsArrivalData.keyword5 = new DataInfo() { value = Scope, color = WXTMConfigData.Colour1 };//
     //       CouponsArrivalData.Remark = new DataInfo() { value = WXTMConfigData.RemarkText, color = WXTMConfigData.RemarkColour };

}
