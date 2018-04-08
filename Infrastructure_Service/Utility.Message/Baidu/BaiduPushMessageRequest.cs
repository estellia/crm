/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/3 10:49:27
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
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using JIT.Utility.Message.Baidu.ValueObject;

namespace JIT.Utility.Message.Baidu
{
    /// <summary>
    /// Baidu云推送请求
    /// </summary>
    public class BaiduPushMessageRequest:ParameterDictionary
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BaiduPushMessageRequest()
        {
        }
        #endregion

        #region 属性集

        /// <summary>
        /// 请求的操作
        /// </summary>
        public HttpMethods HttpMethod { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// API的资源操作方法名
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public virtual string Method
        {
            get { return this.GetParam<string>("method"); }
            set { this.SetParam("method", value); }
        }

        /// <summary>
        /// API的资源操作方法名，访问令牌，明文AK，可从此值获得App的信息，配合sign中的sk做合法性身份认证
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string APIKey
        {
            get { return this.GetParam<string>("apikey"); }
            set { this.SetParam("apikey", value); }
        }

        /// <summary>
        /// API版本号，默认使用最高版本。
        /// <remarks>
        /// <para>非必填参数</para>
        /// </remarks>
        /// </summary>
        public uint? Version
        {
            get { return this.GetParam<uint?>("v"); }
            set { this.SetParam("v", value); }
        }

        /// <summary>
        /// 用户发起请求时的unix时间戳。本次请求签名的有效时间为该时间戳+10分钟。
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public uint? Timestamp
        {
            get { return this.GetParam<uint?>("timestamp"); }
            set { this.SetParam("timestamp", value); }
        }

        /// <summary>
        /// 用户指定本次请求签名的失效时间。格式为unix时间戳形式。
        /// <remarks>
        /// <para>非必填参数</para>
        /// </remarks>
        /// </summary>
        public uint? Expires
        {
            get { return this.GetParam<uint?>("timestamp"); }
            set { this.SetParam("timestamp", value); }
        }

        /// <summary>
        /// 调用参数签名值，与apikey成对出现。
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string Sign
        {
            get { return this.GetParam<string>("sign"); }
            set { this.SetParam("sign", value); }
        }
        #endregion

        /// <summary>
        /// 设置请求的时间戳为当前
        /// </summary>
        public void SetTimestampToNow()
        {
            this.Timestamp = (uint)((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
        }

        /// <summary>
        /// 设置请求的失效时间(单位:分钟)
        /// </summary>
        /// <param name="pMinutes"></param>
        public void SetExpires(int pMinutes)
        {
            if (this.Expires != null)
            {
                TimeSpan ts = new TimeSpan(0, pMinutes, 0);
                this.Expires += (uint)ts.TotalSeconds;
            }
        }
    }
}
