/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/10/28 18:36:53
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
using Dapper.Contrib.Extensions;

namespace Xgx.SyncData.DbEntity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    [Table("VipIntegral")]
    public class VipIntegralEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VipIntegralEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		[ExplicitKey]
		public String VipID { get; set; }

		/// <summary>
		/// 会员卡号
		/// </summary>
		[ExplicitKey]
		public String VipCardCode { get; set; }

		/// <summary>
		/// 开始积分
		/// </summary>
		public Int32? BeginIntegral { get; set; }

		/// <summary>
		/// 增加积分
		/// </summary>
		public Int32? InIntegral { get; set; }

		/// <summary>
		/// 消费积分
		/// </summary>
		public Int32? OutIntegral { get; set; }

		/// <summary>
		/// 最终积分
		/// </summary>
		public Int32? EndIntegral { get; set; }

		/// <summary>
		/// 累计失效积分
		/// </summary>
		public Int32? InvalidIntegral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ImminentInvalidIntegral { get; set; }

		/// <summary>
		/// 当前有效积分
		/// </summary>
		public Int32? ValidIntegral { get; set; }

		/// <summary>
		/// 累积积分
		/// </summary>
		public Int32? CumulativeIntegral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ValidNotIntegral { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}