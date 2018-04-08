/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/10/19 19:59:45
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
using Dapper.Contrib.Extensions;

namespace Xgx.SyncData.DbEntity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    [Table("t_customer_user")]
    public partial class t_customer_userEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public t_customer_userEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public String customer_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ExplicitKey]
		public String customer_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cu_account { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cu_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cu_pwd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cu_expired_date { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? sys_modify_stamp { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? cu_status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cu_status_desc { get; set; }


        #endregion

    }
}