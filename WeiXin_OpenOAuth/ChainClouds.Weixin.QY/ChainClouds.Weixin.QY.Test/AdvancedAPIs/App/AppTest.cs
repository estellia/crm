using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChainClouds.Weixin.Exceptions;
using ChainClouds.Weixin.QY.AdvancedAPIs;
using ChainClouds.Weixin.QY.AdvancedAPIs.App;
using ChainClouds.Weixin.QY.AdvancedAPIs.MailList;
using ChainClouds.Weixin.QY.CommonAPIs;
using ChainClouds.Weixin.QY.Test.CommonApis;

namespace ChainClouds.Weixin.QY.Test.AdvancedAPIs
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class AppTest : CommonApiTest
    {
        [TestMethod]
        public void GetAppInfoTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = AppApi.GetAppInfo(accessToken, 2);

            Assert.IsNotNull(result.agentid);
            Assert.AreEqual(result.agentid, "2");
        }

        [TestMethod]
        public void SetAppTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);

            SetAppPostData date = new SetAppPostData()
                {
                    agentid = "1",
                    description = "test",
                    isreportenter = 0,
                    isreportuser = 0,
                    logo_mediaid = "1muvdK7W8cjLfNqj0hWP89-CEhZNOVsktCE1JHSTSNpzTf7cGOXyDin_ozluwNZqi",
                    name = "Test",
                    redirect_domain = "www.weiweihi.com"
                };

            var result = AppApi.SetApp(accessToken, date);

            Assert.AreEqual(result.errcode, ReturnCode_QY.请求成功);
        }
    }
}
