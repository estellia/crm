using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Interface.Wap.Response
{
    /// <summary>
    /// XML返回
    /// </summary>
    public class RoyaltyResponse
    {
        #region 构造函数
        public RoyaltyResponse()
        {
            DataParas = new Dictionary<string, string>();
        }
        #endregion

        #region 属性
        private Dictionary<string, string> DataParas { get; set; }
        /// <summary>
        /// 是否分润成功。T：分润成功，F：分润失败。
        /// </summary>
        public string IsSuccess
        {
            get
            {
                if (this.DataParas.ContainsKey("is_success"))
                    return this.DataParas["is_success"];
                else
                    return "";
            }
            set { this.DataParas["is_success"] = value; }
        }
        /// <summary>
        /// 分润失败才有错误代码
        /// </summary>
        public string Error
        {
            get
            {
                if (this.DataParas.ContainsKey("error"))
                    return this.DataParas["error"];
                else
                    return "";
            }
            set { this.DataParas["error"] = value; }
        }
        #endregion

        #region 方法
        public void Load(Dictionary<string, string> pParas)
        {
            this.DataParas = pParas;
        }
        #endregion
    }
}
