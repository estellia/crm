using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.WeiXinPay.Interface;

namespace JIT.Utility.Pay.Platform.WeiXinPay.Interface.App.Request
{
    public class WeiXinAppOrderRequest : WeiXinPayBaseRequest
    {
        #region 构造函数
        public WeiXinAppOrderRequest()
            : base()
        { }
        public WeiXinAppOrderRequest(WeiXinPayChannel pChannel)
            : base(pChannel)
        {
            SignMethod = "SHA1";
        }
        #endregion

        #region 属性
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Traceid
        {
            get
            {
                return GetPara("traceid");
            }
            set
            {
                SetPara("traceid", value);
            }
        }
        /// <summary>
        /// 签名方式	        是否必填:是
        /// 按照文档中所示填入，目前仅支持 SHA1；
        /// </summary>
        public string SignMethod
        {
            get
            {
                return GetPara("sign_method");
            }
            set
            {
                SetPara("sign_method", value);
            }
        }
        #endregion

        #region 方法
        //重写基方法
        public override bool IsValid(out string msg)
        {
            msg = string.Empty;
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(this.Traceid))
                sb.AppendLine("用户ID:Traceid不能为空");
            msg += sb.ToString();
            string basemsg;
            base.IsValid(out basemsg);
            msg += basemsg;
            return string.IsNullOrEmpty(msg);
        }
        #endregion
    }
}
