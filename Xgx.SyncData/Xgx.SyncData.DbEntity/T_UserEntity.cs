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
    [Table("T_User")]
    public class T_UserEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_UserEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        [ExplicitKey]
        public String user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_gender { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_birthday { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_password { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_identity { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_telephone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_cellphone { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_postcode { get; set; }

		/// <summary>
		/// 证件类型
		/// </summary>
		public String user_id_type { get; set; }

		/// <summary>
		/// 证件号码
		/// </summary>
		public String user_id_no { get; set; }

		/// <summary>
		/// 照片Url
		/// </summary>
		public String user_pic { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String qq { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String msn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String blog { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_status_desc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String fail_date { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String user_name_en { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_id { get; set; }


        #endregion

    }
}