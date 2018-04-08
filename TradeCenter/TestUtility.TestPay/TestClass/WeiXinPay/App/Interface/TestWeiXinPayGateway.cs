/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/10 13:27:37
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Pay.WeiXinPay.Interface;
using JIT.Utility.Pay.WeiXinPay.Util;
using JIT.Utility.Pay.WeiXinPay.Interface.App.Request;
using JIT.Utility.Pay.WeiXinPay.Interface.App.Response;
using JIT.Utility.Pay.Platform.WeiXinPay;
using JIT.Utility.Pay.Platform.WeiXinPay.Interface.App.Request;
using JIT.Utility.Pay.WeiXinPay.Interface.Native;
using JIT.Utility.Pay.WeiXinPay;

namespace JIT.TestUtility.TestPay.TestClass.WeiXinPay.App.Interface
{
    /// <summary>
    /// 测试WeiXinPayGateway
    /// </summary>
    [TestFixture]
    public class TestWeiXinPayGateway
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestWeiXinPayGateway()
        {
        }
        #endregion

        [Test]
        public void TestPreOrder()
        {
            //WeiXinPayChannel channel = new WeiXinPayChannel();
            //channel.AppID = "wx8f74386d57405ec5";
            //channel.AppSecret = "2af3c935fc66e2087bff1064cde3a708";
            //channel.PaySignKey = "tFVyMIdj1DGCUMbahNzxTUxESkE6heBRtD2RWOfyzyh4WziirurWvBHt3WFVfQRlysh7T0MxMFHikBcScLUNrInygE4972yLyrZyFlay8tV4aKwtA3lBPNgI4qqJw46b";
            //channel.ParnterID = "1218285701";
            //channel.ParnterKey = "b158ca37b5fac76293e402e3144869fc";
            //channel.NotifyToTradeCenterURL = "http://www.qq.com";

            WeiXinPayChannel channel = new WeiXinPayChannel();
            channel.AppID = "wxd930ea5d5a258f4f";
            channel.AppSecret = "db426a9829e4b49a0dcac7b4162da6b6";
            channel.PaySignKey = "L8LrMqqeGRxST5reouB0K66CaYAWpqhAVsq7ggKkxHCOastWksvuX1uvmvQclxaHoYd3ElNBrNO2DHnnzgfVG9Qs473M3DTOZug5er46FhuGofumV8H2FVR9qkjSlC5K";
            channel.ParnterID = "1900000109";
            channel.ParnterKey = "8934e7d15453e97507ef794cf7b0519d";
            channel.NotifyToTradeCenterURL = "http://www.qq.com";
            var str = channel.ToJSON();

            PreOrderRequest req = new PreOrderRequest();
            req.ClientIP = "192.168.0.1";
            req.GoodsDescription = "测试订单";
            req.OrderAmount = 1;
            req.OrderNO = CommonUtil.GenerateNoncestr();
            req.UserID = "crestxu";

            var rsp = WeiXinPay4AppGateway.PreOrder(channel, req);
            Assert.IsTrue(rsp.IsSuccess);
        }

        [Test]
        public void TestCreatePreOrder()
        {
            WeiXinPayChannel channel = new WeiXinPayChannel();
            channel.AppID = "wxd930ea5d5a258f4f";
            channel.AppSecret = "db426a9829e4b49a0dcac7b4162da6b6";
            channel.PaySignKey = "L8LrMqqeGRxST5reouB0K66CaYAWpqhAVsq7ggKkxHCOastWksvuX1uvmvQclxaHoYd3ElNBrNO2DHnnzgfVG9Qs473M3DTOZug5er46FhuGofumV8H2FVR9qkjSlC5K";
            channel.ParnterID = "1900000109";
            channel.ParnterKey = "8934e7d15453e97507ef794cf7b0519d";
            channel.NotifyToTradeCenterURL = "http://www.qq.com";

            PreOrderRequest req = new PreOrderRequest();
            req.ClientIP = "192.168.0.1";
            req.GoodsDescription = "测试订单";
            req.OrderAmount = 1;
            req.OrderNO = "kODvVBBJ3THJ1jMB";
            req.UserID = "crestxu";

            var rsp = WeiXinPay4AppGateway.CreatePreOrder(channel, req);
            var rspContent = rsp.ToJSON();
            Assert.IsTrue(string.IsNullOrWhiteSpace(rsp.PreOrderID) == false);
        }

        [Test]
        public void TestCreatePreOrder2()
        {
            WeiXinPayChannel channel = new WeiXinPayChannel();
            channel.AppID = "wxd930ea5d5a258f4f";
            channel.AppSecret = "db426a9829e4b49a0dcac7b4162da6b6";
            channel.PaySignKey = "L8LrMqqeGRxST5reouB0K66CaYAWpqhAVsq7ggKkxHCOastWksvuX1uvmvQclxaHoYd3ElNBrNO2DHnnzgfVG9Qs473M3DTOZug5er46FhuGofumV8H2FVR9qkjSlC5K";
            channel.ParnterID = "1900000109";
            channel.ParnterKey = "8934e7d15453e97507ef794cf7b0519d";
            channel.NotifyToTradeCenterURL = "http://www.qq.com";

            Package package = new Package()
            {
                Body = "测试订单",
                NotifyUrl = "http://www.qq.com",
                OutTradeNo = "kODvVBBJ3THJ1jMB",
                Partner = channel.ParnterID,
                SpbillCreateIp = "192.168.0.1",
                TotalFee = "1"
            };

            WeiXinAppOrderRequest request = new WeiXinAppOrderRequest(channel);
            request.Traceid = "crestxu";
            request.SetPackage(package);

            var rsp = WeiXinPay4AppGateway.CreatePreOrder(channel, request);
            var rspContent = rsp.ToJSON();
            Console.WriteLine(rspContent);
        }

        [Test]
        public void TestNativeOrderHelper()
        {
            WeiXinPayChannel channel = new WeiXinPayChannel();
            channel.AppID = "wx8f74386d57405ec5";
            channel.AppSecret = "2af3c935fc66e2087bff1064cde3a708";
            channel.PaySignKey = "tFVyMIdj1DGCUMbahNzxTUxESkE6heBRtD2RWOfyzyh4WziirurWvBHt3WFVfQRlysh7T0MxMFHikBcScLUNrInygE4972yLyrZyFlay8tV4aKwtA3lBPNgI4qqJw46b";
            channel.ParnterID = "1218285701";
            channel.ParnterKey = "b158ca37b5fac76293e402e3144869fc";
            channel.NotifyToTradeCenterURL = "http://www.qq.com";

            Package package = new Package()
            {
                Body = "测试订单",
                NotifyUrl = "http://www.qq.com",
                OutTradeNo = "kODvVBBJ3THJ1jMB",
                Partner = channel.ParnterID,
                SpbillCreateIp = "192.168.0.1",
                TotalFee = "1"
            };

            NativePayHelper req = new NativePayHelper(channel);
            req.SetPackage(package);
            Console.WriteLine(req.GenerateNativeUrl("12343252"));
            Console.WriteLine(req.GetParametersXMLStr());
        }

        [Test]
        public void TestUpdateFeedBack()
        {
            //{"AppID":"wx8f74386d57405ec5",
            //    "AppSecret":"2af3c935fc66e2087bff1064cde3a708",
            //    "ParnterID":"1218285701",
            //    "ParnterKey":"b158ca37b5fac76293e402e3144869fc",
            //    "PaySignKey":"tFVyMIdj1DGCUMbahNzxTUxESkE6heBRtD2RWOfyzyh4WziirurWvBHt3WFVfQRlysh7T0MxMFHikBcScLUNrInygE4972yLyrZyFlay8tV4aKwtA3lBPNgI4qqJw46b",
            //    "NotifyToTradeCenterURL":"http://www.qq.com",
            //    "NotifyToBussinessSystemURL":null}
            WeiXinPayChannel channel = new WeiXinPayChannel()
            {
                AppID = "wx8f74386d57405ec5",
                AppSecret = "2af3c935fc66e2087bff1064cde3a708",
                ParnterID = "1218285701",
                ParnterKey = "b158ca37b5fac76293e402e3144869fc",
                PaySignKey = "tFVyMIdj1DGCUMbahNzxTUxESkE6heBRtD2RWOfyzyh4WziirurWvBHt3WFVfQRlysh7T0MxMFHikBcScLUNrInygE4972yLyrZyFlay8tV4aKwtA3lBPNgI4qqJw46b"
            };
            string msg;
            var res = WeiXinPayGateway.UpdateFeedBack(channel, "10279402725891408111", "oUcanju-XbWR0IJmdF_Y68Kt0szw", out msg);
            Console.WriteLine(msg);
        }
    }
}
