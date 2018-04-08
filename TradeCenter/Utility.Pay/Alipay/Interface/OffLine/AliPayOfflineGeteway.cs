using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.Interface.Offline.CreateAndPay;
using JIT.Utility.Pay.Alipay.Interface.Offline.QRCodePre;
using System.Xml;
using JIT.Utility.Pay.Alipay.Interface.OffLine.Base;
using JIT.Utility.Pay.Alipay.Interface.OffLine.CreateAndPay;

namespace JIT.Utility.Pay.Alipay.Interface.Offline
{
    public static class AliPayOffLineGeteway
    {
        static AliPayOffLineGeteway()
        {
            Geteway = "https://mapi.alipay.com/gateway.do?";
        }

        public static readonly string Geteway;

        public static OffLineQRCodePreResponseParameters OfflineQRPay(OfflineQRCodePreRequest pRequest)
        {
            BaseOfflineResponse baseResponse = new BaseOfflineResponse();
            OffLineQRCodePreResponseParameters response = new OffLineQRCodePreResponseParameters();
            var str = BaseGeteway.GetResponseStr(pRequest, Geteway);
            var dic = GetDictionary(str);
            baseResponse.Load(dic);
            if (baseResponse.IsSuccess == "F")
                throw new Exception("请求Offline接口失败:" + baseResponse.Error);
            var paraDic = GetDictionary(baseResponse.Response);
            response.Load(paraDic);
            return response;
        }

        public static CreateAndPayResponseParameters OfflineCreateAndPay(CreateAndPayRequest pRequest)
        {
            BaseOfflineResponse baseResponse = new BaseOfflineResponse();
            CreateAndPayResponseParameters response = new CreateAndPayResponseParameters();
            var str = BaseGeteway.GetResponseStr(pRequest, Geteway);
            var dic = GetDictionary(str);
            baseResponse.Load(dic);
            if (baseResponse.IsSuccess == "F")
                throw new Exception("请求Offline接口失败:" + baseResponse.Error);
            var paraDic = GetDictionary(baseResponse.Response);
            response.Load(paraDic);
            return response;
        }

        private static Dictionary<string, string> GetDictionary(string pXmlStr)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pXmlStr);
            var node = doc.DocumentElement;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (XmlNode item in node.ChildNodes)
            {
                dic[item.Name] = item.InnerXml;
            }
            return dic;
        }
    }
}
