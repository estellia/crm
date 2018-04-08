/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/10/25 16:16:36
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
    [Table("T_Sku_Price")]
    public partial class T_Sku_PriceEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Sku_PriceEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
        [ExplicitKey]
		public String sku_price_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String sku_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String item_price_type_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Decimal? sku_price { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String status { get; set; }

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
		public String bat_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String if_flag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_id { get; set; }


        #endregion

    }
}