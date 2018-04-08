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
    public class ProductTest : CommonApiTest
    {
        [TestMethod]
        public void AddProdectTest()
        {
            var addProductData = new AddProductData();
            var result = ProductApi.AddProduct("[appid]", addProductData);
            Console.Write(result);
            Assert.IsNotNull(result);
        }
    }
}
