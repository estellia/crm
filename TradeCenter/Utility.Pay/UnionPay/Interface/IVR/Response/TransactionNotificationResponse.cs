/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 18:33:12
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
using System.Text;

namespace JIT.Utility.Pay.UnionPay.Interface.IVR.Response
{
    /// <summary>
    /// 交易通知响应
    /// <remarks>
    /// <para>商户平台 -> 支付前置</para>
    /// </remarks> 
    /// </summary>
    public class TransactionNotificationResponse
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TransactionNotificationResponse()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 是否处理成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 响应码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        #endregion

        /// <summary>
        /// 获取响应内容
        /// </summary>
        /// <returns></returns>
        public string GetContent()
        {
            if (this.IsSuccess)
            {
                return string.Format("1|0000|{0}", this.Description);
            }
            else
            {
                return string.Format("0|{0}|{1}", this.Code, this.Description);
            }
        }

        /// <summary>
        /// 交易通知处理成功
        /// </summary>
        public static readonly TransactionNotificationResponse OK = new TransactionNotificationResponse() { IsSuccess = true, Description = "OK" };

        /// <summary>
        /// 交易通知处理失败
        /// </summary>
        public static readonly TransactionNotificationResponse FAILED = new TransactionNotificationResponse() { IsSuccess = false, Code = "5000", Description = "Server Error" };
    }
}
