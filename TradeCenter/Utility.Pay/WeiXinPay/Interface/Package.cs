using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JIT.Utility.Pay.WeiXinPay.Interface
{
    public class Package : BasicPara
    {
        #region 构造函数
        public Package()
        {
            BankType = "WX";    //银行通道类型，由于这里是使用的微信公众号支付，因此这个字段固定为WX，注意大写。参数取值："WX"
            FeeType = "1";       //现金支付币种,取值：1（人民币）,默认值是1，暂只支持1。
            InputCharset = "GBK";       //传入参数字符编码。取值范围："GBK"、"UTF-8"。默认："GBK"
        }
        #endregion

        #region 属性
        /// <summary>
        /// 银行通道类型	    是否必填:是
        /// </summary>
        private string BankType
        {
            get
            {
                return GetPara("bank_type");
            }
            set
            {
                SetPara("bank_type", value);
            }
        }
        /// <summary>
        /// 商品描述	        是否必填:是
        /// </summary>
        public string Body
        {
            get
            {
                return GetPara("body");
            }
            set
            {
                SetPara("body", value);
            }
        }
        /// <summary>
        /// 附加数据	        是否必填:否
        /// </summary>
        public string Attach
        {
            get
            {
                return GetPara("attach");
            }
            set
            {
                SetPara("attach", value);
            }
        }
        /// <summary>
        /// 商户号	            是否必填:是
        /// </summary>
        public string Partner
        {
            get
            {
                return GetPara("partner");
            }
            set
            {
                SetPara("partner", value);
            }
        }
        /// <summary>
        /// 商户订单号	        是否必填:是
        /// </summary>
        public string OutTradeNo
        {
            get
            {
                return GetPara("out_trade_no");
            }
            set
            {
                SetPara("out_trade_no", value);
            }
        }
        /// <summary>
        /// 订单总金额	        是否必填:是
        /// </summary>
        public string TotalFee
        {
            get
            {
                return GetPara("total_fee");
            }
            set
            {
                SetPara("total_fee", value);
            }
        }
        /// <summary>
        /// 货币种类
        /// </summary>
        private string FeeType
        {
            get
            {
                return GetPara("fee_type");
            }
            set
            {
                SetPara("fee_type", value);
            }
        }
        /// <summary>
        /// 通知 URL	        是否必填:是
        /// </summary>
        public string NotifyUrl
        {
            get
            {
                return GetPara("notify_url");
            }
            set
            {
                SetPara("notify_url", value);
            }
        }
        /// <summary>
        /// 订单生成的机器的IP	是否必填:是
        /// </summary>
        public string SpbillCreateIp
        {
            get
            {
                return GetPara("spbill_create_ip");
            }
            set
            {
                SetPara("spbill_create_ip", value);
            }
        }
        /// <summary>
        /// 交易起始时间	    是否必填:否
        /// </summary>
        public string TimeStart
        {
            get
            {
                return GetPara("time_start");
            }
            set
            {
                SetPara("time_start", value);
            }
        }
        /// <summary>
        /// 交易结束时间	    是否必填:否
        /// </summary>
        public string TimeExpire
        {
            get
            {
                return GetPara("time_expire");
            }
            set
            {
                SetPara("time_expire", value);
            }
        }
        /// <summary>
        /// 物流费用	        是否必填:否
        /// </summary>
        public string TransportFee
        {
            get
            {
                return GetPara("transport_fee");
            }
            set
            {
                SetPara("transport_fee", value);
            }
        }
        /// <summary>
        /// 商品费用	        是否必填:否
        /// </summary>
        public string ProductFee
        {
            get
            {
                return GetPara("product_fee");
            }
            set
            {
                SetPara("product_fee", value);
            }
        }
        /// <summary>
        /// 商品标记	        是否必填:否
        /// </summary>
        public string GoodsTag
        {
            get
            {
                return GetPara("goods_tag");
            }
            set
            {
                SetPara("goods_tag", value);
            }
        }
        /// <summary>
        /// 传入参数字符编码	是否必填:是
        /// </summary>
        private string InputCharset
        {
            get
            {
                return GetPara("input_charset");
            }
            set
            {
                SetPara("input_charset", value);
            }
        }
        #endregion

        #region 基类方法
        public override bool IsValid(out string msg)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(BankType))
                sb.AppendLine("银行通道类型:BankType为空");
            if (string.IsNullOrEmpty(Body))
                sb.AppendLine("商品描述:Body为空");
            if (string.IsNullOrEmpty(Partner))
                sb.AppendLine("商户号:Partner为空");
            if (string.IsNullOrEmpty(OutTradeNo))
                sb.AppendLine("商户订单号:OutTradeNo为空");
            if (string.IsNullOrEmpty(TotalFee))
                sb.AppendLine("订单总金额:TotalFee为空");
            if (string.IsNullOrEmpty(FeeType))
                sb.AppendLine("货币种类:FeeType为空");
            if (string.IsNullOrEmpty(NotifyUrl))
                sb.AppendLine("通知 URL:NotifyUrl为空");
            if (string.IsNullOrEmpty(SpbillCreateIp))
                sb.AppendLine("订单生成的机器的IP:SpbillCreateIp为空");
            if (string.IsNullOrEmpty(InputCharset))
                sb.AppendLine("传入参数字符编码:InputCharset为空");
            msg = sb.ToString();
            return string.IsNullOrEmpty(msg);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 生成符合微信支付要求的订单Package内容
        /// </summary>
        /// <returns></returns>
        public string GeneratePackageContent(string pPartnerKey)
        {
            string msg;
            if (!IsValid(out msg))
                throw new WeiXinPayException(msg);
            var items = this.Paras.OrderBy(item => item.Key).ToArray();
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            foreach (var item in items)
            {
                sb.AppendFormat("&{0}={1}", item.Key, item.Value);
                sb2.AppendFormat("&{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value.ToString()));
            }
            sb.Remove(0, 1);
            sb2.Remove(0, 1);
            sb.AppendFormat("&key={0}", pPartnerKey);
            var s1 = sb.ToString();
            var signTemp = JIT.Utility.MD5Helper.Encryption(s1);
            signTemp = signTemp.ToUpper();

            return sb2.AppendFormat("&sign={0}", signTemp).ToString(); ;
        }
        #endregion

    }
}
