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
    /// ʵ�壺  
    /// </summary>
    [Table("T_Role")]
    public class T_RoleEntity
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_RoleEntity()
        {
        }
        #endregion

        #region ���Լ�
        /// <summary>
        /// 
        /// </summary>
        [ExplicitKey]
        public String role_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String def_app_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String role_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String role_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String role_eng_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? is_sys { get; set; }

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
		public String customer_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String table_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? org_level { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String type_id { get; set; }


        #endregion

    }
}