/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/1/2 10:55:37
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

namespace JIT.TradeCenter.Framework
{
    /// <summary>
    /// 交易响应 
    /// </summary>
    public class TradeResponse
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TradeResponse()
        {
        }
        #endregion

        /// <summary>
        /// 响应码
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 响应信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 交易响应返回的数据
        /// </summary>
        public object Datas { get; set; }
    }
}
