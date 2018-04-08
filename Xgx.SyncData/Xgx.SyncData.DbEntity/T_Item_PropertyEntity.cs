/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/10/28 14:25:19
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
    [Table("T_Item_Property")]
    public partial class T_Item_PropertyEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Item_PropertyEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		[ExplicitKey]
		public String item_property_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String prop_value { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String status { get; set; }

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