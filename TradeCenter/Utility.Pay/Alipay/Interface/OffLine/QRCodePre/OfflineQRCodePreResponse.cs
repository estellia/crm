using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Interface.Base;

namespace JIT.Utility.Pay.Alipay.Interface.Offline.QRCodePre
{
    /// <summary>
    /// Offline预订单响应
    /// </summary>
    public class OffLineQRCodePreResponseParameters : BaseResponse
    {
        #region 属性
        /// <summary>
        /// 处理响应码。成功：SUCCESS失败：FAIL理异常：PROCESS_EXCEPTION
        /// </summary>
        public string ResultCode
        {
            get { return this.GetDataPara("result_code"); }
            set { this.SetDataPara("result_code", value); }
        }
        /// <summary>
        /// 对返回响应码进行原因说明。当响应码为SUCCESS时，本字段为空。
        /// </summary>
        public string DetailErrorCode
        {
            get { return this.GetDataPara("detail_error_code"); }
            set { this.SetDataPara("detail_error_code", value); }
        }
        /// <summary>
        /// 对详细错误码进行文字说明。
        /// </summary>
        public string DetailErrorDes
        {
            get { return this.GetDataPara("detail_error_des"); }
            set { this.SetDataPara("detail_error_des", value); }
        }
        /// <summary>
        /// 支付宝交易号
        /// </summary>
        public string TradeNo
        {
            get { return this.GetDataPara("trade_no"); }
            set { this.SetDataPara("trade_no", value); }
        }
        /// <summary>
        /// 商户传进来的自已的业务订单号
        /// </summary>
        public string OutTradeNo
        {
            get { return this.GetDataPara("out_trade_no"); }
            set { this.SetDataPara("out_trade_no", value); }
        }
        /// <summary>
        /// 凭证类型
        /// </summary>
        public string VoucherType
        {
            get { return this.GetDataPara("voucher_type"); }
            set { this.SetDataPara("voucher_type", value); }
        }
        /// <summary>
        /// 二维码码串的内容
        /// </summary>
        public string QrCode
        {
            get { return this.GetDataPara("qr_code"); }
            set { this.SetDataPara("qr_code", value); }
        }
        /// <summary>
        /// 二维码大图的URL地址
        /// </summary>
        public string BigPicUrl
        {
            get { return this.GetDataPara("big_pic_url"); }
            set { this.SetDataPara("big_pic_url", value); }
        }
        /// <summary>
        /// 二维码图片的URL地址
        /// </summary>
        public string PicUrl
        {
            get { return this.GetDataPara("pic_url"); }
            set { this.SetDataPara("pic_url", value); }
        }
        /// <summary>
        /// 二维码小图片的URL地址
        /// </summary>
        public string SmallPicUrl
        {
            get { return this.GetDataPara("small_pic_url"); }
            set { this.SetDataPara("small_pic_url", value); }
        }

        public bool IsSucess
        {
            get { return this.ResultCode == "SUCCESS"; }
        }
        #endregion

        public override void Load(Dictionary<string, string> Paras)
        {
            this.DataParas = Paras;
        }
    }
}
