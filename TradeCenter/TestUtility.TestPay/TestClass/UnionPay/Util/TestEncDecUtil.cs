/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/23 10:10:07
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
using JIT.Utility.Pay.UnionPay.Util;

namespace JIT.TestUtility.TestPay.TestClass.UnionPay.Util
{
    /// <summary>
    /// TestEncDecUtil  
    /// </summary>
    [TestFixture]
    public class TestEncDecUtil
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestEncDecUtil()
        {
        }
        #endregion

        [Test]
        public void TestEncrypt()
        {
            string s1 = EncDecUtil.GetEncryptKey("654321");
            string rightEncryptedS1 = "HyWvyTPmlX2M/XJruMxKMIldV42XG2eCoV5o8Gi6CgifcIlKky3a8+tVnof8pnz34+HvKt9tk2C0wa2LEy9Vz0cXiBn2DTh/G8hka9QKdyjER+YtZYRqfU/qpJnw+xvcWvPo9HZXykWAYHuCPjnbPoKLex6xh6Xh0g2ES10Gxhc=";
            var encryptedS1 = EncDecUtil.Base64Encrypt(EncDecUtil.RSAEncrypt("D:/cer/630056832596.pfx", "123456", s1));
            Assert.IsTrue(encryptedS1 != null);
            Assert.IsTrue(encryptedS1 == rightEncryptedS1);
        }

        [Test]
        public void Test3DESKeyEncrypt()
        {
            string key = EncDecUtil.GetEncryptKey("654321");
            string encryptedKey = EncDecUtil.Base64Encrypt(EncDecUtil.RSAEncrypt("D:/cer/700000000000001.pfx", "000000", key));
            Assert.IsTrue(encryptedKey != null);
        }

        [Test]
        public void TestTripleDesEncrypt()
        {
            string s1 = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><upbp application=\"MGw.Req\" version =\"1.0.0\" sendTime =\"20131223155031\" sendSeqId =\"12345888888000\"><frontUrl>http://www.163.com</frontUrl><merchantOrderDesc></merchantOrderDesc><misc></misc><gwType>01</gwType><transTimeout>ssssss</transTimeout><backUrl>http://192.168.1.156:8080/Test/Notify</backUrl><merchantOrderCurrency>156</merchantOrderCurrency><merchantOrderAmt>1</merchantOrderAmt><merchantOrderDesc>呵呵</merchantOrderDesc><merchantId>630056832596</merchantId><merchantOrderTime>20131223155031</merchantOrderTime><merchantOrderId>20131223155031</merchantOrderId><merchantUserId></merchantUserId><mobileNum>15388157741</mobileNum><cardNum></cardNum></upbp>";
            string key = EncDecUtil.GetEncryptKey("654321");
            string rightEncryptedS1 = "Dau3eXih4xg1yrTZhvQ9RCd/nMVK4dNRiTWXjAh9OT0uyUEroFMYrAmndlfsulEQsqjwZu3W7AVpqiDQdsOJmiKD83+xtm1Zks5CUhvzPirQH2yzyTVnycXWkGUzYE2NZzoo4BtTPWmK1BMtXc8Edm2VvT0LpghUYGji+D2B2cJKuNlFrBXb/VU2hOK3xF7VFnLYax79Vdtdnj9HUR4Jnk5i1bCLWzQT+l07WkLcpTkKY3ud7NrP/lqKXScLWoCXR9glH24OLIb8BHmNcwMKNEVbWgJHWmsxdJHG0BKhi7pOzKabx5LbqWl6UWGaun9Ll7NZYVTwYekA6yynWk5L+OCOFcM32pCzRY8cMM+nV0zakyyyKJ3MUS6sXMFXfXUOFnLYax79VdsKUt1osFs7Yf/yCpnfAOcMxfOxWmaThaGIZhkxOILqZMuBj6eduJF7tc+RUaOClbVPI/rtai13xrk5BjIjNam72WMD0Qsffu8bmf1lvDMuVoovUujpjGx13fesDllvf5X8BHmNcwMKNBD8j+EOIidzPqf0D8gxU1arGJzIkdTGxn9Z9BzE68q0TyP67Wotd8a+o9HAhfJAwGi5rLQTaH26/AR5jXMDCjRFW1oCR1prMbp6Y1t3I1YT/QGIquncWYxggdTCBg7jIT6n9A/IMVNWvc9EfUBtNPv8BHmNcwMKNCUwZThqh4f9tz8WDOJGDc/69FiKnWivNn1UwC4pOEzmWAwqYmShn73bDfvoTQ1Mcuwx1m51Qv6d7jaq9dMk1TTRr2q/xF+7oKmSrko/d9qp7DHWbnVC/p2PTW+ta0IdUKITi57cBHPbGm2hsCCCWGkocaY7ZvTsMkB5fUWRlHPjxYQQtmSCXpRp+d8eGu4LOixT8QQOUv3zDIggDEqf900a3FMkTQngKby0H5cpi1yZBK/VZOjeGKhpVm+NHIcoeg==";
            var encryptedS1 =  EncDecUtil.Base64Encrypt(EncDecUtil.TripleDESEncrypt(key, s1));
            Assert.IsTrue(encryptedS1 != null);
            Assert.IsTrue(encryptedS1 == rightEncryptedS1);
        }
    }
}
