/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 14:58:10
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
using System.IO;
using System.Text;

using NUnit.Framework;
using JIT.Utility;
using JIT.Utility.Pay.UnionPay.Util;

namespace JIT.TestUtility.TestPay.TestClass.UnionPay.Util
{
    /// <summary>
    /// TestPacketUtil  
    /// </summary>
    [TestFixture]
    public class TestPacketUtil
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestPacketUtil()
        {
        }
        #endregion

        [Test]
        public void TestParseRequestPackets()
        {
            string encrypedReq1 = null;
            using (var s = ReflectionUtils.GetEmbeddedResource("JIT.TestUtility.TestPay.TestMaterial.notification_req1.txt"))
            {
                TextReader reader = new StreamReader(s);
                encrypedReq1 = reader.ReadToEnd();
            }
            string req1 = PacketUtil.ParseRequestPackets("D:/cer/5101200070003100001.cer", encrypedReq1);
        }
    }
}
