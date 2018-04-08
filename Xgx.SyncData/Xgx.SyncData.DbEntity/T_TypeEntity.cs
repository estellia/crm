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
    [Table("T_Type")]
    public class T_TypeEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_TypeEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        [ExplicitKey]
        public String type_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String type_code { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String type_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String type_name_en { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String type_domain { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? type_system_flag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? type_Level { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String customer_id { get; set; }


        #endregion

    }
}