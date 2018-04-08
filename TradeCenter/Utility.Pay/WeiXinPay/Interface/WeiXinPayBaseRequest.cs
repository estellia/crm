using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.WeiXinPay.Util;
using JIT.Utility.Pay.WeiXinPay.Interface;
using System.Web.Security;
using Newtonsoft.Json;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Pay.WeiXinPay.Interface
{
    public abstract class WeiXinPayBaseRequest : BasicPara, IAPIRequest
    {
        #region 构造函数
        public WeiXinPayBaseRequest()
        {
            TimeStamp = CommonUtil.GetCurrentTimeStamp().ToString();
            NonceStr = CommonUtil.GenerateNoncestr();
        }

        public WeiXinPayBaseRequest(WeiXinPayChannel pChannel)
            : this()
        {
            this.Channel = pChannel;
        }
        #endregion

        #region Channel
        /// <summary>
        /// 通道信息，不能为空
        /// </summary>
        private WeiXinPayChannel _channel;
        [JsonIgnore]
        public WeiXinPayChannel Channel
        {
            get { return _channel; }
            set
            {
                _channel = value;
                if (value != null)
                    ConfigRequest(value);
            }
        }
        #endregion

        #region 请求参数属性
        /// <summary>
        /// 公众号 id	        是否必填:是
        /// <remarks>
        /// <para>
        /// 商户注册具有支付权限的公众号成功后即可获得；
        /// </para>
        /// </remarks>
        /// </summary>
        public string AppId
        {
            get
            {
                return GetPara("appId");
            }
            set
            {
                SetPara("appId", value);
            }
        }
        /// <summary>
        /// 时间戳	            是否必填:是
        /// <remarks>
        /// <para>
        /// 商户生成，从 1970 年 1 月 1 日 00：00：00 至今的秒数，
        /// 即当前的时间，且最终需要转换为字符串形式；
        /// </para>
        /// </remarks>
        /// </summary>
        public string TimeStamp
        {
            get
            {
                return GetPara("timeStamp");
            }
            set
            {
                SetPara("timeStamp", value);
            }
        }
        /// <summary>
        /// 随机字符串	        是否必填:是
        /// </summary>
        public string NonceStr
        {
            get
            {
                return GetPara("nonceStr");
            }
            set
            {
                SetPara("nonceStr", value);
            }
        }
        /// <summary>
        /// 订单详情扩展字符串	是否必填:是
        /// 商户将订单信息组成该字符串
        /// </summary>
        public string Package
        {
            get
            {
                return GetPara("package");
            }
            set
            {
                SetPara("package", value);
            }
        }

        /// <summary>
        /// 签名	            是否必填:是
        /// <remarks>
        /// <para>
        /// 商户将接口列表中的参数按照指定方式迚行签名，签名方式使用 signType 中标示的签名方式，
        /// 具体签名方案参见接口使用说明中签名帮劣；由商户按照规范签名后传入；
        /// </para>
        /// </remarks>
        /// </summary>
        public string AppSignature
        {
            get
            {
                return GetPara("app_signature");
            }
            set
            {
                SetPara("app_signature", value);
            }
        }
        #endregion

        #region 基方法
        public override bool IsValid(out string msg)
        {
            msg = string.Empty;
            StringBuilder sb = new StringBuilder();
            if (Channel == null)
                sb.AppendLine("未配置Channel信息:Channel为空");
            if (string.IsNullOrEmpty(this.AppId))
                sb.AppendLine("基本参数：AppId为空");
            if (this.TimeStamp == null)
                sb.AppendLine("基本参数：时间戳TimeStamp为空");
            if (string.IsNullOrEmpty(this.Package))
                sb.AppendLine("基本参数：业务数据包Package为空,请调用SetPackage方法");
            else
            {
                if (string.IsNullOrEmpty(this.AppSignature))
                    sb.AppendLine("基本参数：支付密钥PaySign为空");
            }
            msg += sb.ToString();
            return string.IsNullOrEmpty(msg);
        }
        #endregion

        #region 方法

        #region 设置Package
        public void SetPackage(Package pPackage)
        {
            this.Package = pPackage.GeneratePackageContent(Channel.ParnterKey);
            SetPaySign();
        }
        #endregion

        #region 私有 设置PaySign 设置AppID
        private void SetPaySign()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>(this.Paras);
            dic.Add("appkey", Channel.PaySignKey);
            //生成待加密的字符串
            var temp = dic.Where(t => t.Key != "sign_method" && t.Key.ToLower() != "app_signature" && t.Key != "signType").Select(t => t);
            var nosign = CommonUtil.GetParametersStr(temp);
            //加密并赋值给PaySign
            this.AppSignature = CommonUtil.Sha1(nosign);
        }

        private void ConfigRequest(WeiXinPayChannel pChannel)
        {
            AppId = pChannel.AppID;
        }
        #endregion

        #endregion

        #region 接口方法
        public virtual string GetContent()
        {
            string msg;
            if (!IsValid(out msg))
                throw new WeiXinPayException(msg) { Code = "501" };
            Dictionary<string, object> dic = new Dictionary<string, object> { };
            foreach (var item in Paras)
            {
                dic[item.Key.ToLower()] = item.Value;
            }
            return dic.ToJSON();
        }
        #endregion
    }
}
