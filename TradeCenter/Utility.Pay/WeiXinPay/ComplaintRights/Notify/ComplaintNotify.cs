using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.WeiXinPay.Interface;
using JIT.Utility.Pay.WeiXinPay.Util;

namespace JIT.Utility.Pay.WeiXinPay.ComplaintRights.Notify
{
    public class ComplaintNotify : BaseNotify
    {
        #region 构造函数
        #endregion

        #region 属性
        /// <summary>
        /// 是否必填:是 字段名称：公众号 id；字段来源：商户注册具有支付权限的公众号成功后即可获得；传入方式：由商户直接传入。
        /// </summary>
        public string AppId
        {
            get { return this.GetDataPara("AppId"); }
            set { this.SetDataPara("AppId", value); }
        }
        /// <summary>
        /// 是否必填:是 字段名称：时间戳；字段来源：商户生成从 1970 年 1 月 1日 00：00：00 至今的秒数，即当前的时间；由商户生成后传入。取值范围：32 字符以下
        /// </summary>
        public string TimeStamp
        {
            get { return this.GetDataPara("TimeStamp"); }
            set { this.SetDataPara("TimeStamp", value); }
        }
        /// <summary>
        /// 是否必填:是   支付该笔订单的用户 ID，商户可通过公众号其他接口为付款用户服务。
        /// </summary>
        public string OpenId
        {
            get { return this.GetDataPara("OpenId"); }
            set { this.SetDataPara("OpenId", value); }
        }
        /// <summary>
        /// 是否必填:是   字段名称：签名；字段来源：对前面的其他字段不含appKey按照字典序排序后，使用 SHA1 算法得到的结果。由商户生成后传入。
        /// </summary>
        public string AppSignature
        {
            get { return this.GetDataPara("AppSignature"); }
            set { this.SetDataPara("AppSignature", value); }
        }
        /// <summary>
        /// 是否必填:是   通知类型 request 用户提交投诉 confirm 用户确认消除投诉 reject 用户拒绝消除投诉
        /// </summary>
        public string MsgType
        {
            get { return this.GetDataPara("MsgType"); }
            set { this.SetDataPara("MsgType", value); }
        }
        /// <summary>
        /// 是否必填:是  投诉单号
        /// </summary>
        public string FeedBackId
        {
            get { return this.GetDataPara("FeedBackId"); }
            set { this.SetDataPara("FeedBackId", value); }
        }
        /// <summary>
        /// 是否必填:否  交易订单号
        /// </summary>
        public string TransId
        {
            get { return this.GetDataPara("TransId"); }
            set { this.SetDataPara("TransId", value); }
        }
        /// <summary>
        /// 是否必填:否  用户投诉原因
        /// </summary>
        public string Reason
        {
            get { return this.GetDataPara("Reason"); }
            set { this.SetDataPara("Reason", value); }
        }
        /// <summary>
        /// 是否必填:否  用户希望解决方案
        /// </summary>
        public string Solution
        {
            get { return this.GetDataPara("Solution"); }
            set { this.SetDataPara("Solution", value); }
        }
        /// <summary>
        /// 是否必填:否  备注信息+电话
        /// </summary>
        public string ExtInfo
        {
            get { return this.GetDataPara("ExtInfo"); }
            set { this.SetDataPara("ExtInfo", value); }
        }
        /// <summary>
        /// 签名方式
        /// </summary>
        public string SignMethod
        {
            get { return this.GetDataPara("SignMethod"); }
            set { this.SetDataPara("SignMethod", value); }
        }
        #endregion

        #region 方法
        public bool CheckSign(WeiXinPayChannel pChannel)
        {
            var dic = new Dictionary<string, object>();
            dic["appid"] = this.AppId;
            dic["appkey"] = pChannel.PaySignKey;
            dic["timestamp"] = this.TimeStamp;
            dic["openid"] = this.OpenId;
            var nosignstr = CommonUtil.GetParametersStr(dic);
            var sign = CommonUtil.Sha1(nosignstr);
            return sign == this.AppSignature;
        }
        #endregion
    }
}
