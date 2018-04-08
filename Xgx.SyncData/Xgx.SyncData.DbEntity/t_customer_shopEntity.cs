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
    [Table("t_customer_shop")]
    public partial class t_customer_shopEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public t_customer_shopEntity()
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
		public String customer_shop_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_name_en { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_name_short { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_province { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_city { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_country { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_post_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_contact { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_tel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_fax { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? cs_status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String cs_status_desc { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? sys_modify_stamp { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String longitude { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String dimension { get; set; }

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


        #endregion

    }
}