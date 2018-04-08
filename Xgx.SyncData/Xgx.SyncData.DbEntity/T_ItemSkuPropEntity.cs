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
    [Table("T_ItemSkuProp")]
    public partial class T_ItemSkuPropEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_ItemSkuPropEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
        [ExplicitKey]
		public String ItemSkuPropID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Item_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemSku_prop_1_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemSku_prop_2_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemSku_prop_3_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemSku_prop_4_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String ItemSku_prop_5_id { get; set; }

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
		public DateTime? create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String modify_user_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? modify_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}