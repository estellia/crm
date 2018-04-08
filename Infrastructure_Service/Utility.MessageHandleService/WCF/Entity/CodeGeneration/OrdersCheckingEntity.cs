/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/5 11:30:35
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

namespace JIT.Utility.Message.WCF.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class OrdersCheckingEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public OrdersCheckingEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Int32? CheckingID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? OrdersID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ClientPositionID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Reason { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ClientID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ClientDistributorID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SendStatus { get; set; }


        #endregion

    }
}