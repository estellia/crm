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
    [Table("T_Unit_Relation")]
    public class T_Unit_RelationEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public T_Unit_RelationEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        [ExplicitKey]
        public String unit_relation_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String src_unit_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String dst_unit_id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? create_time { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? modify_time { get; set; }


        #endregion

    }
}