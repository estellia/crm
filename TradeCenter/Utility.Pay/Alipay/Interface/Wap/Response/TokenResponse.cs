using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Interface.Base;

namespace JIT.Utility.Pay.Alipay.Interface.Wap.Response
{
    public class TokenResponse : BaseResponse
    {
        /// <summary>
        /// 商户签约的支付宝账号对应的支付宝唯一用户号。以 2088 开头的 16 位纯数字组成，不可空
        /// </summary>
        public string Partner
        {
            get { return this.GetDataPara("partner"); }
            set { this.DataParas["partner"] = value; }
        }

        /// <summary>
        /// 签名,可空
        /// </summary>
        public string Sign
        {
            get
            {
                return this.GetDataPara("sign");
            }
            set { this.DataParas["sign"] = value; }
        }

        /// <summary>
        /// 获取签名方式，不可空
        /// </summary>
        public string SecID
        {
            get { return this.GetDataPara("sec_id"); }
            set { this.DataParas["sec_id"] = value; }
        }

        /// <summary>
        /// 执行授权网关，不可空
        /// </summary>
        public string Service
        {
            get { return this.GetDataPara("service"); }
            set { this.DataParas["service"] = value; }
        }

        /// <summary>
        /// 版本，不可空
        /// </summary>
        public string Version
        {
            get { return this.GetDataPara("v"); }
            set { this.DataParas["v"] = value; }
        }

        /// <summary>
        /// 返回的业务参数，可空
        /// </summary>
        public string ResData
        {
            get { return this.GetDataPara("res_data"); }
            set { this.DataParas["res_data"] = value; }
        }

        /// <summary>
        /// 请求失败后才会返回该值。包含请求失败的错误码和错误原因。
        /// </summary>
        public string ResError
        {
            get { return this.GetDataPara("res_error"); }
            set { this.DataParas["res_error"] = value; }
        }

        public string Token
        {
            get { return this.GetDataPara("request_token"); }
            set { this.DataParas["request_token"] = value; }
        }


        public override void Load(Dictionary<string, string> Paras)
        {
            DataParas = Paras;
        }
    }
}
