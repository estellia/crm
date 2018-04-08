/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/27 16:58:31
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
using JIT.Utility.Pay.UnionPay.Interface.IVR.Request;

namespace JIT.TestUtility.TestPay.TestClass.UnionPay.Interface.IVR.Request
{
    /// <summary>
    /// TestTransactionNotificationRequest  
    /// </summary>
    [TestFixture]
    public class TestTransactionNotificationRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestTransactionNotificationRequest()
        {
        }
        #endregion

        [Test]
        public void TestProperties()
        {
            string s1 = "MjY3MDk5NjMzMjE4|Aa/qpl2e+s6+kfKQY0H9rYQTbA6n0o1k/JlBTQezC81hXdIXjZLohIF1lOGTtC6q1luGeXrSHHxusCZdg5gHCz79gEQkdq4eHWN6aZPryu/DbvNv7jXVxnors+ZqFqJE2zeWp0Lr5Fu9S3zXl0w7unAxfz766Kltruy+4MXYKr4=|oIUNRpv1nzvfQQunMn70rG260iepVSeX8UmvVFxewcSjdiFPEoqP1LcMevlQBj+6wk8yKWtH8V3A51/JVgA4mWQoqYtaSrsne5wUN0wvb8ejbjgSiyu00wg3EDHSW+enaHNg399iPu+DQ929XVGyHvDYrpXqv3eZwROP+LKZ/2QR5zGZGqPYxiZu6N1rAyK4ZqZvabWLC6rlDLnmA23lXXL4eGhRixbKSmXuTJ7AsIsQbVEi5mkIMM5LIncQDVUqmYzLPNQHESv07WoWjzAb01dox1//kxzVj1k90p7uPaczkhjort48xdoasbqeixSMVA49oa52qgFkDPUTbxioOVdox1//kxzV47Uzt3uucC0ocl1rZ3TPEV0L7rqTMTY2m+KOz89IscKqR7zrrDujO+qPccNnwwSG0Os8Aeb0DoeVhiyjpWW4lrErBXrLWx/HdbHN9FH31ayW2PFVxfFzysochNP2S/1ONE1bx6WsJ2VaUoGP4ptUTzRr7QOQKKfDBUQ/uOZk3QD5MYUy/JQsgDfv2YNd17NV1zESRTLwDE5sxg+BeGVAytstx6xPeBveg0PdvV1Rsh6p8/RQBVIuVxlC4kYCUfMHjiqueqWn4ovuFnT3e1qzZJ10PfG44mwdJrLSNhTPc+fc32ObyDWV4pcRqHqTOVHC8wI895/+IwpvSnVFU1KXnyay0jYUz3PndSO7nNl7cLrZMDmrTpmgJyhv/Pz3Jma7XZEYHjzXrVUUb3uOJmyEgtLSb86Nxext5aZLdb2GG9vgDCZaa9izZZkHmP5I9erDrlU+4B4wiH0=";
            var req1 = new TransactionNotificationRequest();
            req1.Load(s1);
        }
    }
}
