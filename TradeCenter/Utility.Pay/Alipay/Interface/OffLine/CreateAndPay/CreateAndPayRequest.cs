using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Util;
using JIT.Utility.Pay.Alipay.Interface.Wap;
using JIT.Utility.Pay.Alipay.Interface.Base;
using JIT.Utility.Pay.Alipay.Interface.Offline.Base;
using JIT.Utility.Pay.Alipay.ValueObject;
using JIT.Utility.Pay.Alipay.Channel;

namespace JIT.Utility.Pay.Alipay.Interface.Offline.CreateAndPay
{
    public class CreateAndPayRequest : BaseOfflineRequest
    {
        #region 构造函数
        public CreateAndPayRequest()
            : base()
        {
            ProductCode = ProductCodes.SOUNDWAVE_PAY_OFFLINE.ToString();
        }

        public CreateAndPayRequest(AliPayChannel pChannel)
            : base(pChannel)
        {
            ProductCode = ProductCodes.SOUNDWAVE_PAY_OFFLINE.ToString();
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
        /// 动态 ID 类型：可空
        ///<para>soundwave：声波</para>
        ///<para>qrcode：二维码</para>
        ///<para>barcode：条码</para>
        /// </summary>
        public string DynamicIDType
        {
            get { return this.GetPara("dynamic_id_type"); }
            set { this.SetPara("dynamic_id_type", value); }
        }
        /// <summary>
        /// 动态 ID。可空
        /// </summary>
        public string DynamicID
        {
            get { return this.GetPara("dynamic_id"); }
            set { this.SetPara("dynamic_id", value); }
        }
        #endregion

        #region 方法
        public override void SetSelfService()
        {
            this.Service = "alipay.acquire.createandpay";
        }

        public override bool IsSelfValid()
        {
            return !string.IsNullOrEmpty(this.ProductCode);
        }
        #endregion


    }
}
