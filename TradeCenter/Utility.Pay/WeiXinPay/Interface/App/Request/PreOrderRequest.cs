/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/7 17:26:33
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
using System.Web;

using JIT.Utility.ExtensionMethod;
using JIT.Utility.Pay.WeiXinPay.ValueObject;
using JIT.Utility.Pay.WeiXinPay.Util;

namespace JIT.Utility.Pay.WeiXinPay.Interface.App.Request
{
    /// <summary>
    /// 预订单请求 
    /// </summary>
    public class PreOrderRequest
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PreOrderRequest()
        {
            this.Currency = Currencys.RMB;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 订单编号,长度不超过32个字符
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string OrderNO { get; set; }

        /// <summary>
        /// 商家对用户的唯一标识,如果用微信SSO，此处建议填写授权用户的openid
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 商品描述，长度不超过128个字符
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string GoodsDescription { get; set; }

        /// <summary>
        /// 订单金额,单位为分
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public int OrderAmount { get; set; }

        /// <summary>
        /// 附加数据，长度不超过128字节
        /// </summary>
        public string Attach { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public Currencys Currency { get; set; }

        /// <summary>
        /// 订单生成的机器IP，指用户浏览器端IP，不是商户服务器IP，格式为IPV4 整型。取值范围：15 字节以内。
        /// <remarks>
        /// <para>必填参数</para>
        /// </remarks>
        /// </summary>
        public string ClientIP { get; set; }

        private string _timeStart;
        /// <summary>
        /// 交易起始时间,长度不超过14个字符
        /// </summary>
        public DateTime? TimeStart
        {
            get 
            {
                if (!string.IsNullOrWhiteSpace(this._timeStart))
                {
                    return DateTime.ParseExact(this._timeStart, "yyyyMMddHHmmss", null);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    this._timeStart = value.Value.ToString("yyyyMMddHHmmss");
                }
                else
                {
                    this._timeStart = string.Empty;
                }
            }
        }

        private string _timeExpire;
        /// <summary>
        /// 交易结束时间,也是订单失效时间
        /// </summary>
        public DateTime? TimeExpire
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this._timeExpire))
                {
                    return DateTime.ParseExact(this._timeExpire, "yyyyMMddHHmmss", null);
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    this._timeExpire = value.Value.ToString("yyyyMMddHHmmss");
                }
                else
                {
                    this._timeExpire = string.Empty;
                }
            }
        }

        /// <summary>
        /// 物流费用,单位为分
        /// </summary>
        public int? TransportAmount { get; set; }

        /// <summary>
        /// 商品费用，单位为分
        /// </summary>
        public int? ProductAmount { get; set; }

        /// <summary>
        /// 商品标记，优惠券时可能用到
        /// </summary>
        public string GoodsTag { get; set; }
        #endregion

        #region 生成订单Package
        /// <summary>
        /// 生成订单Package
        /// </summary>
        /// <param name="pChannel"></param>
        /// <returns></returns>
        protected string GenerateOrderPackage(WeiXinPayChannel pChannel)
        {
            //组织参数字典
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("bank_type", "WX");
            if (!string.IsNullOrWhiteSpace(this.GoodsDescription))
                parameters.Add("body", this.GoodsDescription);
            else
                throw new WeiXinPayException(string.Format("缺少必填参数GoodsDescription."));
            if (!string.IsNullOrWhiteSpace(this.Attach))
                parameters.Add("attach", this.Attach);
            if (!string.IsNullOrWhiteSpace(pChannel.ParnterID))
                parameters.Add("partner", pChannel.ParnterID);
            else
                throw new WeiXinPayException(string.Format("缺少必填参数pChannel.ParnterID."));
            if (!string.IsNullOrWhiteSpace(this.OrderNO))
                parameters.Add("out_trade_no", this.OrderNO);
            else
                throw new WeiXinPayException(string.Format("缺少必填参数OrderNO."));
            if(this.OrderNO.Length>16)
                throw new WeiXinPayException(string.Format("OrderNO的长度不能超过16个字符."));
            if (this.OrderAmount > 0)
                parameters.Add("total_fee", this.OrderAmount.ToString());
            else
                throw new WeiXinPayException(string.Format("订单金额必须大于0."));
            parameters.Add("fee_type", this.Currency.GetCode());
            if (!string.IsNullOrWhiteSpace(pChannel.NotifyToTradeCenterURL))    //总是先通知到交易中心
                parameters.Add("notify_url", pChannel.NotifyToTradeCenterURL);
            else
                throw new WeiXinPayException(string.Format("缺少必填参数pChannel.NotifyToTradeCenterURL."));
            if (!string.IsNullOrWhiteSpace(this.ClientIP))
                parameters.Add("spbill_create_ip", this.ClientIP);
            else
                throw new WeiXinPayException(string.Format("缺少必填参数ClientIP."));
            if (this.TimeStart.HasValue)
                parameters.Add("time_start", this._timeStart);
            if (this.TimeExpire.HasValue)
                parameters.Add("time_expire", this._timeExpire);
            if (this.TransportAmount.HasValue && this.TransportAmount.Value > 0)
            {
                parameters.Add("transport_fee", this.TransportAmount.Value.ToString());
            }
            if (this.ProductAmount.HasValue && this.ProductAmount.Value > 0)
            {
                parameters.Add("product_fee", this.ProductAmount.Value.ToString());
            }
            if (!string.IsNullOrWhiteSpace(this.GoodsTag))
                parameters.Add("goods_tag", this.GoodsTag);
            parameters.Add("input_charset", "UTF-8");
            return CommonUtil.GenerateOrderPackage(parameters, pChannel);
        }
        #endregion

        #region 生成预订单内容
        /// <summary>
        /// 生成预订单内容
        /// </summary>
        /// <param name="pChannel"></param>
        /// <returns></returns>
        public InnerPreOrderRequestInfo GetContent(WeiXinPayChannel pChannel)
        {
            //组织参数
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            var packageContent = this.GenerateOrderPackage(pChannel);
            parameters.Add("package", packageContent);
            var nonceStr = CommonUtil.GenerateNoncestr();
            parameters.Add("noncestr", nonceStr);
            var timeStamp = CommonUtil.GetCurrentTimeStamp();
            parameters.Add("timestamp", timeStamp.ToString());
            if (!string.IsNullOrWhiteSpace(this.UserID))
            {
                parameters.Add("traceid", this.UserID);
            }
            //生成预订单内容
            return CommonUtil.GeneratePreOrderContent(parameters, pChannel);
        }
        #endregion
    }
}
