using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Pay.Negotiation.Interface.Request;
using JIT.Utility.Pay.Negotiation;
using JIT.Utility.Pay.Negotiation.Interface;
using JIT.Utility.Web;
using System.Net;

namespace JIT.TestUtility.TestPay.Web.Negotiation
{
    /// <summary>
    /// NegotionGatWay 的摘要说明
    /// </summary>
    public class NegotionGatWay : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string res = null;
            switch (context.Request["action"])
            {
                case "pay":
                    res = Pay();
                    break;
                case "BatchPay":
                    res = BatchPay();
                    break;
                case "Notity":
                    res = Notity();
                    break;

            }
            context.Response.Write(res);
        }
        private string BatchPay()
        {

            string Parameters = "{\"ChannelId\":53,\"AppOrderID\":91,\"merchantId\":\"" + DateTime.Now.ToString("yyyyMMddhhmmsss") + "\""
                          + ",\"transTime\":\"" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\""
                          + ",\"transList\":\"1,6222021208004387203,穆恒涛,10001,0,0,010102321458|2,6222021208004387203,穆恒涛,10001,0,0,010102321459\""
                          + "}";
            string str = "request={\"ClientID\":\"" + "e703dbedadd943abacf864531decdac1" + "\","
                         + "\"UserID\":\"" + "oUcanjlz0pGMW57Xm50-uiCqkPIc" + "\","
                         + "\"Token\":null,\"AppID\":1,"
                         + "\"Parameters\":" + Parameters + "}";
            var result = HttpWebClient.DoHttpRequest("http://115.29.10.228:8181/Gateway.ashx?action=BatchPay", str);

            return result;
        }
        private string Pay()
        {
            PayRequest req = new PayRequest("173665683923359");
            req.AccBankCode = "6222021001081623333";
            req.AccName = "测试";
            req.Account = "1";
            req.Amount = "1";
            req.MerchantSerial = "100" + DateTime.Now.ToString("yyyyMMddhhddss") + "021"; ;
            req.Misc = "xxxxxxxxxxxxxx";
            req.MsgExt = "xxxxxxxxxxxxx";
            req.TransDesc = "xxxxxxxxxxxxxxxxxxxxx";
            req.TransTime = DateTime.Now.ToString("yyyyMMddhhmmss");

            string Parameters = "{\"ChannelId\":93,\"AppOrderID\":88,\"merchantId\":\"" + DateTime.Now.ToString("yyyyMMddhhmmsss") + "\""
                               + ",\"transTime\":\"" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\""
                               + ",\"amount\":1,\"account\":\"6222021001081623939\",\"accName\":\"穆恒涛\",\"accBankCode\":\"10001\""
                               + "}";
            string str = "request={\"ClientID\":\"" + "e703dbedadd943abacf864531decdac1" + "\","
                         + "\"UserID\":\"" + "oUcanjlz0pGMW57Xm50-uiCqkPIc" + "\","
                         + "\"Token\":null,\"AppID\":1,"
                         + "\"Parameters\":" + Parameters + "}";
            var result = HttpWebClient.DoHttpRequest("http://112.124.68.147:8181/Gateway.ashx?action=Pay", str);
            return result;
        }
        public string Notity()
        {
            string str = @"MTczNjY1NjgzOTIzMzU5
                |F+TZrzcmMkYfn+MCZtE8n8Baheymf3wCeQlN9pkICJZ0KA/+ukQeoSdJAhTCKj+AMkarxiS4GVax
                hoEl8wh894CKSK+AH+ZHXjfav96ntSi/yqk0K/yzznyoneASEA9t/J+87L5cjxYiEMpthTQ15D+q
                pgCkoqKrFVQOS9uzlb4=
                |JPyAiMWmzppINFr5rmTaFgpQ+IXIUiJ0CPs9GQmNhgN5Nm8aNAG/lV3DUaCXu9889E8louE7AvL2
                1EJ6buAvKHHtFJ7MMpMM2bIQnoTZ1Y0iJ7oVX5jqUo/meWANdj17wUiRe/0kkxD4u9OMEtPNFiu3
                qOjuOhmoxs4VcywFQ7REyNdpd8Ocm8bOFXMsBUO0e60qMhRzsjhWtmEc6uhWyUVMWlXZ2uNlvvcN
                caEDDUnIUSqu8+MzcCeCZzIwNdqoJpIDLCGEs2RRFtS/I7I1K0qO8nUqO/GMxw6AP+AoLmQqoXhJ
                GNBeAKhg+VLKF+GGIiwl6enlFFAgvvPNFRm/dw1k0hn/22ubqslGktnqkzUxdRA+nsfy96hg+VLK
                F+GGIiwl6enlFFDJnlBYjHfw3MqaQTLL/1g/OIpHlBvd/iaqyUaS2eqTNZUBcz5t6iDzxfh5jIv8
                Z8g4iWwtiAhKtaiAKhD3dGE9BOSM/NglAv25K4qseWvABZiVjMd/wYMDXBQwvInwL8s5GhIJPnh8
                yhb9OqD8Fw3RWMntlJKB3b9GDN4t1Yv3ekQNe9OsYyemEoLcLjCHXnDLdAh41JvvoGm0x1JW+Ab6
                fEY1h/KKRAgyRZ3RFCAY5UTD+QwlBRT57OOT6tpvtqIYJtH/ezfsVrTpsK+mlvOfLepVO7zE5LwE
                +RpeYqBAQC3qVTu8xOS8pOMonfzwHB4r15IJN5fFynWYlliYE7dtSZxKcO/o9zMjAxBQfwRzMbwv
                8iQRXa/RJaVhriSkVOqxGftwD/2HB9bT9sOjz8D84ZoCVLaVu2GxGftwD/2HB6skwUp8+xXDxMxx
                blf910WAjhRcuy/yvSu99wTQBhT6NJB/gPGfM4osv99SiHmjqrEZ+3AP/YcHIjlx/zhVBDTtDn5l
                J8I/TgpMCsnuu1249DxX35ShCf5B0hmsQ86tfbEZ+3AP/YcHm+IYmvu3o//6+ufRNDvVUObvqxC/
                3Yalmv6ZgK5zmHmxGftwD/2HB3ebi3WUOhc2JvFSg2JgNEZ+lOa+nJmdSwT5Gl5ioEBA21oWtNrG
                R9RdLhAZrVBVEmluUFCsLk/bkyB8F5Jz99MRtiyF0QhA4lEgcQqXoutD4rfGr4StJev3V5MqUx/8
                WwtsT7GEHkyEauwOQWu62L+dKq61NUVE5X7eEddnOhhYt16BPXqrnhlKwPL2u97vj64Mzv8NgnVw
                glj8cB5DQXvd8BU0wwIC9dkY2tFD31idpsncCSW9UJRneS1oA4CnLUimqx34E2cPVZyiua/iVbFH
                ZHzYhX7SofDND+yZvUv4Di5GBUyJJeY=";
            //var result = HttpWebClient.DoHttpRequest("http://localhost:1693/Notify/NegotiationNotify.ashx?ChannelID=93", str);
            var result = HttpWebClient.DoHttpRequest("  http://112.124.68.147:8181/Notify/NegotiationNotify.ashx?ChannelID=93", str);
            return result;

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}