using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Interface.Base;

namespace JIT.Utility.Pay.Alipay.Interface.OffLine.Base
{
    public class BaseOfflineResponse : BaseResponse
    {
        #region 构造函数
        public BaseOfflineResponse()
            : base()
        { }
        #endregion

        #region 属性
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public string IsSuccess
        {
            get { return this.GetDataPara("is_success"); }
            set { this.SetDataPara("is_success", value); }
        }
        /// <summary>
        /// 签名方式
        /// </summary>
        public string SignType
        {
            get { return this.GetDataPara("sign_type"); }
            set { this.SetDataPara("sign_type", value); }
        }
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign
        {
            get { return this.GetDataPara("sign"); }
            set { this.SetDataPara("sign", value); }
        }
        /// <summary>
        /// 错误
        /// </summary>
        public string Error
        {
            get { return this.GetDataPara("error"); }
            set { this.SetDataPara("error", value); }
        }
        /// <summary>
        /// 请求
        /// </summary>
        public string Request
        {
            get { return this.GetDataPara("request"); }
            set { this.SetDataPara("request", value); }
        }
        /// <summary>
        /// 响应
        /// </summary>
        public string Response
        {
            get { return this.GetDataPara("response"); }
            set { this.SetDataPara("response", value); }
        }
        #endregion
        public override void Load(Dictionary<string, string> Paras)
        {
            this.DataParas = Paras;
        }
    }
}
