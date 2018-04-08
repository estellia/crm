/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/28 14:43:53
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
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.TradeCenter.Entity
{
    /// <summary>
    /// 实体： 各平台的请求和响应记录 
    /// </summary>
    public partial class PayRequestRecordEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public PayRequestRecordEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Int32? RecordID { get; set; }

		/// <summary>
		/// 平台,1-银联,2-支付宝,3-微信
		/// </summary>
		public Int32? Platform { get; set; }

		/// <summary>
		/// 对应的ChannelID
		/// </summary>
		public Int32? ChannelID { get; set; }

		/// <summary>
		/// 请求的Json字符串
		/// </summary>
		public String RequestJson { get; set; }

		/// <summary>
		/// 响应的Json
		/// </summary>
		public String ResponseJson { get; set; }

		/// <summary>
		/// 客户ID
		/// </summary>
		public String ClientID { get; set; }

		/// <summary>
		/// 用户ID
		/// </summary>
		public String UserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}