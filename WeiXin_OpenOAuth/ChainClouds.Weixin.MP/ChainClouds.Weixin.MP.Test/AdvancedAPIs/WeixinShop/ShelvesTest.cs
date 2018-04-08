using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChainClouds.Weixin.MP.AdvancedAPIs;
using ChainClouds.Weixin.MP.AdvancedAPIs.MerChant;
using ChainClouds.Weixin.MP.CommonAPIs;
using ChainClouds.Weixin.MP.Entities;
using ChainClouds.Weixin.MP.Test.CommonAPIs;
using ChainClouds.Weixin.MP.TenPayLib;

namespace ChainClouds.Weixin.MP.Test.AdvancedAPIs
{
    //需要通过微信支付审核的账户才能成功
    [TestClass]
    public class ShelvesTest : CommonApiTest
    {
        [TestMethod]
        public void AddShelvesTest()
        {
            var m1 = new M1(2, 50);
            var groupIds =new int[]{49, 50, 51, 52};
            var m2 = new M2(groupIds);
            var m3 = new M3(49, "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg");
            var imgs = new string[] { "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg", "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg", "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg", "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg" };
            var m4 = new M4(groupIds, imgs);
            var m5 = new M5(groupIds, "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg");
            var result = ShelfApi.AddShelves("accessToken", m1, m2, m3, m4, m5, "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg", "测试货架");
            Console.Write(result);
            Assert.IsNotNull(result);
        }
    }
}
