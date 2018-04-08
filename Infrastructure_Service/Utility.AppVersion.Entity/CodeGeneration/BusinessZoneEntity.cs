/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/17 11:16:35
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
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.Utility.AppVersion.Entity
{
    /// <summary>
    /// 实体： 商圈表 
    /// </summary>
    public partial class BusinessZoneEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public BusinessZoneEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Int32? BusinessZoneID { get; set; }

		/// <summary>
		/// 商圈编码
		/// </summary>
		public String BusinessZoneCode { get; set; }

		/// <summary>
		/// 商圈名
		/// </summary>
		public String BusinessZoneName { get; set; }

		/// <summary>
		/// 商圈类型:1-区域,2-行业,3-其它
		/// </summary>
		public Int32? Type { get; set; }

		/// <summary>
		/// 是否是叶节点
		/// </summary>
		public Boolean? IsLeaf { get; set; }

		/// <summary>
		/// 父节点ID
		/// </summary>
		public Int32? ParentID { get; set; }

		/// <summary>
		/// Web服务地址
		/// </summary>
		public String ServiceUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? IsDelete { get; set; }


        #endregion

    }
}