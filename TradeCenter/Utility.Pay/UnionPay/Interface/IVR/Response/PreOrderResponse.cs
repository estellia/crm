/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 13:30:37
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

using JIT.Utility.Pay.UnionPay.ExtensionMethod;
using JIT.Utility.Pay.UnionPay.ValueObject;

namespace JIT.Utility.Pay.UnionPay.Interface.IVR.Response
{
    /// <summary>
    /// 预订单响应 
    /// </summary>
    public class PreOrderResponse : BaseAPIResponse
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PreOrderResponse()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerchantName
        {
            get
            {
                return this.GetNodeTextByXPath("//merchantName");
            }
        }

        /// <summary>
        /// 商户代码
        /// </summary>
        public string MerchantID
        {
            get
            {
                return this.GetNodeTextByXPath("//merchantId");
            }
        }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string MerchantOrderID
        {
            get
            {
                return this.GetNodeTextByXPath("//merchantOrderId");
            }
        }

        private DateTime? _merchantOrderTime = null;
        /// <summary>
        /// 商户订单时间
        /// </summary>
        public DateTime? MerchantOrderTime
        {
            get
            {
                if (this._merchantOrderTime == null)
                {
                    var strVal = this.GetNodeTextByXPath("//merchantOrderTime");
                    if (!string.IsNullOrWhiteSpace(strVal))
                    {
                        this._merchantOrderTime = DateTime.ParseExact(strVal, "yyyy-MM-dd HH:mm:ss",null);
                    }
                }
                return this._merchantOrderTime;
            }
        }

        private int? _merchantOrderAmt = null;
        /// <summary>
        /// 商户订单金额
        /// </summary>
        public int? MerchantOrderAmt
        {
            get
            {
                if (this._merchantOrderAmt == null)
                {
                    string strVal = this.GetNodeTextByXPath("//merchantOrderAmt");
                    if (!string.IsNullOrWhiteSpace(strVal))
                    {
                        this._merchantOrderAmt = int.Parse(strVal);
                    }
                }
                return this._merchantOrderAmt;
            }
        }

        private Currencys? _merchantOrderCurrency = null;
        /// <summary>
        /// 订单金额币种
        /// </summary>
        public Currencys? MerchantOrderCurrency
        {
            get
            {
                if (this._merchantOrderCurrency == null)
                {
                    string strVal = this.GetNodeTextByXPath("//merchantOrderCurrency");
                    if (!string.IsNullOrWhiteSpace(strVal))
                    {
                        this._merchantOrderCurrency = (Currencys)int.Parse(strVal);
                    }
                }
                return this._merchantOrderCurrency;
            }
        }

        ///// <summary>
        ///// 重定向到银联支付页面的URL
        ///// </summary>
        //public string RedirectURL
        //{
        //    get
        //    {
        //        return this.GetNodeTextByXPath("//gwInvokeCmd");
        //    }
        //}

        /// <summary>
        /// 商户处的用户名
        /// </summary>
        public string MerchantUserID
        {
            get
            {
                return this.GetNodeTextByXPath("//merchantUserId");
            }
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNum
        {
            get
            {
                return this.GetNodeTextByXPath("//mobileNum");
            }
        }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CarNum
        {
            get
            {
                return this.GetNodeTextByXPath("//cardNum");
            }
        }

        /// <summary>
        /// 自定义保留域
        /// </summary>
        public string Misc
        {
            get
            {
                return this.GetNodeTextByXPath("//misc");
            }
        }
        #endregion
    }
}
