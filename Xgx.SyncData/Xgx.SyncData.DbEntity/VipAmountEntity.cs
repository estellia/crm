/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/10/28 18:36:52
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
    /// ʵ�壺  
    /// </summary>
    [Table("VipAmount")]
    public class VipAmountEntity
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipAmountEntity()
        {
        }
        #endregion     

        #region ���Լ�
		/// <summary>
		/// 
		/// </summary>
		[ExplicitKey]
		public String VipId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ExplicitKey]
		public String VipCardCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? BeginAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? InAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OutAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? EndAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? BeginReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? InReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? OutReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ImminentInvalidRAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? InvalidReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? ValidReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? TotalReturnAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String PayPassword { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsLocking { get; set; }

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