using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Interface.Offline.Base;
using JIT.Utility.Pay.Alipay.ValueObject;
using JIT.Utility.Pay.Alipay.Channel;

namespace JIT.Utility.Pay.Alipay.Interface.Offline.QRCodePre
{
    /// <summary>
    /// Offline预订单请求
    /// </summary>
    public class OfflineQRCodePreRequest : BaseOfflineRequest
    {
        #region 构造函数
        public OfflineQRCodePreRequest()
            : base()
        {
            ItBPay = "1d";
            ProductCode = ProductCodes.QR_CODE_OFFLINE.ToString();
        }
        public OfflineQRCodePreRequest(AliPayChannel pChannel)
            : base(pChannel)
        {
            ItBPay = "1d";
            ProductCode = ProductCodes.QR_CODE_OFFLINE.ToString();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 用来区分是哪种业务类型的下单。目前支持如下 3 类：不可空
        ///<para> SOUNDWAVE_PAY_OFFLINE：声波支付</para>
        ///<para> FINGERPRINT_FAST_PAY：指纹支付</para>
        ///<para> BARCODE_PAY_OFFLINE：条码支付</para>
        /// </summary>
        public string ProductCode
        {
            get { return this.GetPara("product_code"); }
            set { this.SetPara("product_code", value); }
        }
        /// <summary>
        /// 多渠道收单的渠道类型.用来标识是那种收单渠道来源。不可空,构造函数中赋值
        #endregion

        #region 接口方法
        public override void SetSelfService()
        {
            this.Service = "alipay.acquire.precreate";
        }

        public override bool IsSelfValid()
        {
            return !string.IsNullOrEmpty(this.ProductCode)
                && !string.IsNullOrEmpty(Currency) && !string.IsNullOrEmpty(ItBPay);
        }
        #endregion
    }
}
