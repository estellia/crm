/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/10/25 16:16:35
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
    [Table("T_Item")]
    public partial class T_ItemEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_ItemEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
        [ExplicitKey]
		public String item_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_category_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_name_en { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_name_short { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String pyzjm { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_remark { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String status_desc { get; set; }

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
		public String bat_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String if_flag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ifgifts { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ifoften { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ifservice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsGB { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String data_from { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? display_index { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String imageUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CustomerId { get; set; }


        #endregion

    }
}