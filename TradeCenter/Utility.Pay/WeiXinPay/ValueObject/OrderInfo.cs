/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/7 18:46:56
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

namespace JIT.Utility.Pay.WeiXinPay.ValueObject
{
    /// <summary>
    /// 订单信息 
    /// </summary>
    public class OrderInfo
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public OrderInfo()
        {
        }
        #endregion

        /// <summary>
        /// 商品描述。参数长度：128 字节以下。
        /// </summary>
        public string GoodsDescription { get; set; }

        /// <summary>
        /// 订单编号,长度不超过32个字符
        /// </summary>
        public string OrderNO { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public int OrderAmount { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public Currencys Currency { get; set; }

        /// <summary>
        /// 附加数据，长度不超过128字节
        /// </summary>
        public string Attach { get; set; }

        /// <summary>
        /// 订单生成的机器IP，指用户浏览器端IP，不是商户服务器IP，格式为IPV4 整型。取值范围：15 字节以内。
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
    }
}
