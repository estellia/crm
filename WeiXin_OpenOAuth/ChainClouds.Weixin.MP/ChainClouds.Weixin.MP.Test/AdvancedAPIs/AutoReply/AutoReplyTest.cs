using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChainClouds.Weixin.MP.AdvancedAPIs;
using ChainClouds.Weixin.MP.AdvancedAPIs.Analysis;
using ChainClouds.Weixin.MP.CommonAPIs;
using ChainClouds.Weixin.MP.Entities;
using ChainClouds.Weixin.MP.Test.CommonAPIs;

namespace ChainClouds.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    [TestClass]
    public class AutoReplyTest : CommonApiTest
    {
        [TestMethod]
        public void ArticleSummaryTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = AutoReplyApi.GetCurrentAutoreplyInfo(accessToken);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }
    }
}
