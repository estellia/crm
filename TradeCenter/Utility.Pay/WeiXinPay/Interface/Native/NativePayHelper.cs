using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.WeiXinPay.Util;
using System.Web.Security;
using System.Web;

namespace JIT.Utility.Pay.WeiXinPay.Interface.Native
{
    public class NativePayHelper : WeiXinPayBaseRequest
    {
        #region 构造函数
        public NativePayHelper()
            : base()
        {
            this.SignMethod = "SHA1";
        }

        public NativePayHelper(WeiXinPayChannel pChannel)
            : base(pChannel)
        { }
        #endregion

        #region 属性
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
        /// <summary>
        /// Native支付时,微信支付平台调用本地服务，返回给微信支付平台的XML内容
        /// </summary>
        /// <returns></returns>
        public string GetParametersXMLStr(string pRetCode = "0", string pRetErrorMessage = "")
        {
            string msg;
            if (!IsValid(out msg))
                throw new WeiXinPayException(msg);
            Dictionary<string, object> tempDic = new Dictionary<string, object>();
            tempDic.Add("AppId", AppId);
            tempDic.Add("Package", this.Package);
            tempDic.Add("TimeStamp", this.TimeStamp);
            tempDic.Add("RetCode", pRetCode);
            tempDic.Add("RetErrMsg", pRetErrorMessage);
            tempDic.Add("NonceStr", this.NonceStr);
            tempDic["appkey"] = Channel.PaySignKey;
            var nosign = CommonUtil.GetParametersStr(tempDic);
            tempDic.Add("AppSignature", CommonUtil.Sha1(nosign));
            tempDic.Add("SignMethod", SignMethod);
            tempDic.Remove("appkey");
            return CommonUtil.ArrayToXml(tempDic);
        }

        /// <summary>
        /// 生成NativeURL
        /// </summary>
        /// <param name="pProductid">商品标识</param>
        /// <returns></returns>
        public string GenerateNativeUrl(string pProductid)
        {
            if (Channel == null)
                throw new WeiXinPayException("Channel未设置");
            var temp = Paras.Where(t => t.Key.ToLower() == "appid" || t.Key.ToLower() == "noncestr"
                || t.Key.ToLower() == "timestamp" || t.Key.ToLower() == "productid").Select(t => t);
            var dic = new Dictionary<string, object> { };
            foreach (var item in temp)
            {
                dic[item.Key.ToLower()] = item.Value.ToString();
            }
            dic["appkey"] = Channel.PaySignKey;
            dic["productid"] = pProductid;
            var nosign = CommonUtil.GetParametersStr(dic);
            dic["sign"] = CommonUtil.Sha1(nosign);
            temp = dic.Where(t => t.Key != "appkey").Select(t => t);
            return string.Format("weixin://wxpay/bizpayurl?{0}", CommonUtil.GetParametersStr(temp));
        }

        //重写父类的GetContent方法,禁止其它调用
        public override string GetContent()
        {
            throw new Exception("Native不支持调用此方法");
        }

        #endregion

    }
}
