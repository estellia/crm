/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/9/22 14:19:09
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
    [Table("T_User_Role")]
    public class T_User_RoleEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_User_RoleEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        [ExplicitKey]
        public String user_role_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String role_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String unit_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? default_flag { get; set; }


        #endregion

    }
}