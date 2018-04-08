using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.Utility.Pay.Alipay.Interface.Wap.Request;
using JIT.Utility.Pay.Alipay;
using JIT.Utility.Pay.Alipay.Util;
using JIT.Utility.Pay.Alipay.Interface.Wap;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.Channel;

namespace JIT.TestUtility.TestPay.Web.AliPay
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        AliPayChannel pChannel = new AliPayChannel();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            AliPayWapQueryTradeRequest request = new AliPayWapQueryTradeRequest(pChannel)
            {

            };
            var url = this.Label1.Text;
            Response.Redirect(url);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var total_fee = Request["total_fee"];
            var subject = Request["subject"];
            AliPayChannel channel = new AliPayChannel();
            AliPayWapTokenRequest request = new AliPayWapTokenRequest(channel)
            {
                CallBackUrl = "http://115.29.186.161:9004/AlipayWapTrade2/Call_Back.aspx",
                NotifyUrl = "http://112.124.43.61:7777/AliPay/NotifyFrm.aspx",
                OutTradeNo = Guid.NewGuid().ToString().Replace("-", ""),
                ReqID = Guid.NewGuid().ToString().Replace("-", ""),
                Subject = string.IsNullOrEmpty(subject) ? "测试" : subject,
                TotalFee = string.IsNullOrEmpty(total_fee) ? "0.03" : total_fee,
                SellerAccountName = AliPayConfig.Seller_Account_Name_Royalty,
            };
            var t = AliPayWapGeteway.GetQueryTradeResponse(request, channel);
            this.Label1.Text = t.RedirectURL;
        }
    }
}