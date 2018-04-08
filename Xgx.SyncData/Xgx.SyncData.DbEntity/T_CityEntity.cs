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
    [Table("T_City")]
    public class T_CityEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_CityEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        [ExplicitKey]
        public String city_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String city1_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String city2_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String city3_name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String city_code { get; set; }


        #endregion

    }
}