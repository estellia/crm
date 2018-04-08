/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/21 11:48:32
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

namespace JIT.Utility.MSTRIntegration.Entity
{
    /// <summary>
    /// 实体： MSTR的报表对象,主要用于集成时生成访问MSTR报表的Url 
    /// </summary>
    public partial class MSTRReportEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MSTRReportEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自增主键
		/// </summary>
		public int? ReportID { get; set; }

		/// <summary>
		/// 表MSTRProject的外键.用于表示报表属于哪个项目
		/// </summary>
		public int? ProjectID { get; set; }

		/// <summary>
		/// 报表的名称
		/// </summary>
		public string ReportName { get; set; }

		/// <summary>
		/// MSTR中报表对象的ID
		/// </summary>
		public string ReportGUID { get; set; }

		/// <summary>
		/// 报表类型（1：Report，2：Document）
		/// </summary>
		public int? ReportType { get; set; }

		/// <summary>
		/// 查看模式（1：表格，2：图形，3：表格+图形。）
		/// </summary>
		public int? ReportViewMode { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		public int? IsDelete { get; set; }

		/// <summary>
		/// 创建者的用户ID
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 最后更新者的用户ID
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }


        #endregion

    }
}