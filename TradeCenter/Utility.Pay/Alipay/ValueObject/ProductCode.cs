using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.ValueObject
{
    public enum ProductCodes
    {
        /// <summary>
        /// 声波支付
        /// </summary>
        SOUNDWAVE_PAY_OFFLINE,//声波支付
        /// <summary>
        /// 指纹支付
        /// </summary>
        FINGERPRINT_FAST_PAY,//指纹支付
        /// <summary>
        /// 条码支付
        /// </summary>
        BARCODE_PAY_OFFLINE,//条码支付
        /// <summary>
        /// 二维码
        /// </summary>
        QR_CODE_OFFLINE
    }
}
