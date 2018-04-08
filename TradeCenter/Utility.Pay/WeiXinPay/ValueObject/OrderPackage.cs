/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/3/10 10:55:59
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

namespace JIT.Utility.Pay.WeiXinPay.ValueObject
{
    /// <summary>
    /// 订单包裹 
    /// </summary>
    public class OrderPackage
    {
        private Dictionary<string, string> _innerParams = new Dictionary<string, string>();
        public OrderPackage()
        {
            this._innerParams.Add("bank_type","WX");    //银行通道类型，由于这里是使用的微信公众号支付，因此这个字段固定为WX，注意大写。参数取值："WX"
            this._innerParams.Add("fee_type", "1");       //现金支付币种,取值：1（人民币）,默认值是1，暂只支持1。
            this._innerParams.Add("input_charset", "UTF-8");       //传入参数字符编码。取值范围："GBK"、"UTF-8"。默认："GBK"
            this._innerParams.Add("notify_url", "http://www.qq.com");       //通知URL,在支付完成后,接收微信通知支付结果的URL,需给绝对路径,255 字符内, 格式如:http://wap.tenpay.com/tenpay.asp。取值范围：255 字节以内。
        }

        /// <summary>
        /// 商户号,即注册时分配的partnerId。
        /// </summary>
        public string PartnerID
        {
            get { return this._innerParams["partner"]; }
            set { this._innerParams["partner"] = value; }
        }

        /// <summary>
        /// 商户KEY
        /// </summary>
        public string PartnerKey { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNO
        {
            get { return this._innerParams["out_trade_no"]; }
            set { this._innerParams["out_trade_no"] = value; }
        }

        /// <summary>
        /// 商品描述。参数长度：128 字节以下。
        /// </summary>
        public string GoodsDescription
        {
            get { return this._innerParams["body"]; }
            set { this._innerParams["body"] = value; }
        }

        /// <summary>
        /// 总金额
        /// </summary>
        public string TotalAmount
        {
            get { return this._innerParams["total_fee"]; }
            set { this._innerParams["total_fee"] = value; }
        }

        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIP
        {
            get { return this._innerParams["spbill_create_ip"]; }
            set { this._innerParams["spbill_create_ip"] = value; }
        }

        /// <summary>
        /// 生成符合微信支付要求的订单Package内容
        /// </summary>
        /// <returns></returns>
        public string GeneratePackageContent()
        {
            var items =this._innerParams.OrderBy(item => item.Key).ToArray();
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            foreach (var item in items)
            {
                sb.AppendFormat("&{0}={1}",item.Key,item.Value);
                sb2.AppendFormat("&{0}={1}",item.Key,HttpUtility.UrlEncode(item.Value));
            }
            sb.Remove(0, 1);
            sb2.Remove(0, 1);
            sb.AppendFormat("&key={0}", this.PartnerKey);
            var s1 = sb.ToString();
            var signTemp = JIT.Utility.MD5Helper.Encryption(s1);
            signTemp = signTemp.ToUpper();

            return sb2.AppendFormat("&sign={0}", signTemp).ToString(); ;
        }
    }
}
