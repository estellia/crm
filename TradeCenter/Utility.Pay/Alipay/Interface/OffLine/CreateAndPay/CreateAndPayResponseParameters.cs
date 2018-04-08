using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Interface.Base;

namespace JIT.Utility.Pay.Alipay.Interface.OffLine.CreateAndPay
{
    public class CreateAndPayResponseParameters : BaseResponse
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
        /// 购买者淘宝用户号
        /// </summary>
        public string BuyerUserID
        {
            get { return this.GetDataPara("buyer_user_id"); }
            set { this.SetDataPara("buyer_user_id", value); }
        }
        /// <summary>
        /// 购买者淘宝帐号
        /// </summary>
        public string BuyerLogonID
        {
            get { return this.GetDataPara("buyer_logon_id"); }
            set { this.SetDataPara("buyer_logon_id", value); }
        }

        public bool IsSuccess
        {
            get { return this.ResultCode == "ORDER_SUCCESS_PAY_SUCCESS"; }
        }
        #endregion

        public override void Load(Dictionary<string, string> Paras)
        {
            this.DataParas = Paras;
        }
    }
}
