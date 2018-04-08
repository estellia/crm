/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/9/29 14:23:59
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
    [Table("T_Item_Category")]
    public partial class T_Item_CategoryEntity  
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Item_CategoryEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        [ExplicitKey]
        public String item_category_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_category_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_category_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String pyzjm { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String parent_id { get; set; }

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
		public String CustomerID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? DisplayIndex { get; set; }


        #endregion

    }
}