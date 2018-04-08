/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/10 18:24:56
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
using JIT.Utility.Pay.WeiXinPay.Interface;
using JIT.Utility.Pay.WeiXinPay.Util;

namespace JIT.TestUtility.TestPay.TestClass.WeiXinPay.Util
{
    /// <summary>
    /// TestCommonUtil  
    /// </summary>
    [TestFixture]
    public class TestCommonUtil
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestCommonUtil()
        {
        }
        #endregion

        [Test]
        public void TestGenerateOrderPackage()
        {
            Dictionary<string, string> ps = new Dictionary<string, string>();
            ps.Add("bank_type", "WX");
            ps.Add("body", "test");
            ps.Add("fee_type", "1");
            ps.Add("input_charset", "UTF-8");
            ps.Add("notify_url", "htttp://www.baidu.com");
            ps.Add("out_trade_no", "VxscC3lGoO5mBI63");
            ps.Add("partner", "1900000109");
            ps.Add("spbill_create_ip", "127.0.0.1");
            ps.Add("total_fee", "1");

            WeiXinPayChannel channel = new WeiXinPayChannel();
            channel.ParnterKey = "8934e7d15453e97507ef794cf7b0519d";
            var packageContent = CommonUtil.GenerateOrderPackage(ps, channel);
            Assert.IsTrue(packageContent == "bank_type=WX&body=test&fee_type=1&input_charset=UTF-8&notify_url=htttp%3a%2f%2fwww.baidu.com&out_trade_no=VxscC3lGoO5mBI63&partner=1900000109&spbill_create_ip=127.0.0.1&total_fee=1&sign=819255571F172EFB3994B99F2A7E1D85");
        }

        [Test]
        public void TestGeneratePreOrderContent()
        {
            Dictionary<string, string> ps = new Dictionary<string, string>();
            ps.Add("timestamp", CommonUtil.GetCurrentTimeStamp().ToString());
            ps.Add("package", "bank_type=WX&body=test&fee_type=1&input_charset=UTF-8&notify_url=htttp%3a%2f%2fwww.baidu.com&out_trade_no=VxscC3lGoO5mBI63&partner=1900000109&spbill_create_ip=127.0.0.1&total_fee=1&sign=819255571F172EFB3994B99F2A7E1D85");
            ps.Add("noncestr", CommonUtil.GenerateNoncestr());
            ps.Add("traceid", "test");

            WeiXinPayChannel channel = new WeiXinPayChannel();
            channel.AppID = "wxd930ea5d5a258f4f";
            channel.PaySignKey = "L8LrMqqeGRxST5reouB0K66CaYAWpqhAVsq7ggKkxHCOastWksvuX1uvmvQclxaHoYd3ElNBrNO2DHnnzgfVG9Qs473M3DTOZug5er46FhuGofumV8H2FVR9qkjSlC5K";
            channel.ParnterKey = "8934e7d15453e97507ef794cf7b0519d";

            var preorderContent = CommonUtil.GeneratePreOrderContent(ps, channel);
            //Assert.IsTrue(preorderContent == "41fb55f446eac92a60c4a8cff91e51af38ebf505");
        }
    }
}
