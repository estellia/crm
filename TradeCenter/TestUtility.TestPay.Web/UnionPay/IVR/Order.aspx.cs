using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Pay.UnionPay.Interface;
using JIT.Utility.Pay.UnionPay.ValueObject;
using JIT.Utility.Pay.UnionPay.Interface.IVR;
using JIT.Utility.Pay.UnionPay.Interface.IVR.Request;
using JIT.Utility.Pay.UnionPay.Interface.IVR.ValueObject;

namespace JIT.TestUtility.TestPay.Web.UnionPay.IVR
{
    public partial class Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnOrder.Click += new EventHandler(btnOrder_Click);
            this.btnQueryOrder.Click += new EventHandler(btnQueryOrder_Click);
            this.txtMobileNO.Text = ConfigurationManager.AppSettings["DefaultMobileNO"];
        }

        private void btnQueryOrder_Click(object sender, EventArgs e)
        {
            //创建支付通道
            UnionPayChannel channel = new UnionPayChannel()
            {
                CertificateFilePassword = ConfigurationManager.AppSettings["IVREncryptCertificateFilePassword"]
                ,
                CertificateFilePath = ConfigurationManager.AppSettings["IVREncryptCertificateFilePath"]
                ,
                MerchantID = ConfigurationManager.AppSettings["IVRMerchantID"]
                ,
                PacketEncryptKey = "654321"
            };
            //查询订单
            QueryOrderRequest req = new QueryOrderRequest();
            req.SendTime = DateTime.Now;
            req.SendSeqID = Guid.NewGuid().ToString("N");
            req.TransType = IVRTransTypes.PreAuthorization;
            req.MerchantID = channel.MerchantID;
            req.MerchantOrderID = this.Session["MerchantOrderID"] as string;
            req.MerchantOrderTime = this.Session["MerchantOrderTime"] as DateTime?;
            //
            try
            {
                var rsp = IVRGateway.QueryOrder(channel, req);
                this.txtOrderInfo.Text = rsp.ToJSON();
            }
            catch (Exception ex)
            {
                this.txtOrderInfo.Text = "执行失败：" + Environment.NewLine + ex.Message;
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            //创建支付通道
            UnionPayChannel channel = new UnionPayChannel()
            {
                CertificateFilePassword = ConfigurationManager.AppSettings["IVREncryptCertificateFilePassword"]
                ,
                CertificateFilePath = ConfigurationManager.AppSettings["IVREncryptCertificateFilePath"]
                ,
                MerchantID = ConfigurationManager.AppSettings["IVRMerchantID"]
                ,
                PacketEncryptKey = "654321"
            };
            //下订单
            PreOrderRequest req = new PreOrderRequest();
            req.SendTime = DateTime.Now;
            req.SendSeqID = Guid.NewGuid().ToString("N");
            req.FrontUrl = ConfigurationManager.AppSettings["IVRFrontUrl"];    //商户平台的页面,用户支付完毕后跳转的页面
            req.MerchantOrderDesc = "呵呵";
            req.Misc = string.Empty;
            req.Mode = IVRModes.Callback;
            req.TransTimeout = DateTime.Now.AddHours(1);
            req.BackUrl = ConfigurationManager.AppSettings["IVRBackUrl"];      //当用户支付完成后,支付平台回调的交易通知的页面
            req.MerchantOrderCurrency = Currencys.RMB;
            req.MerchantOrderAmt = 1;
            req.MerchantID = channel.MerchantID;
            req.MerchantOrderTime = DateTime.Now.AddMinutes(-5);
            req.MerchantOrderID = Guid.NewGuid().ToString("N").Substring(0,30);
            req.MerchantUserID = string.Empty;
            req.MobileNum = this.txtMobileNO.Text;      //消费者的手机号,支付前置会将验证码发送到该手机
            req.CarNum = string.Empty; 
            //记录支付请求日志
            Loggers.Debug(new DebugLogInfo() { Message = string.Format("[IVR]PreOrder Request={0}", req.GetContent()) });
            //支付平台成功接收
            var rsp = IVRGateway.PreOrder(channel, req);
            //重定向到支付页面
            if (rsp.IsSuccess)
            {
                //
                this.Session["MerchantOrderID"] = rsp.MerchantOrderID;
                this.Session["MerchantOrderTime"] = rsp.MerchantOrderTime;
                //
                this.ltGotoPay.Text = "订单创建成功,请等待支付平台呼入.";
                this.ltGotoPay.Mode = LiteralMode.PassThrough;
            }
        }
    }
}