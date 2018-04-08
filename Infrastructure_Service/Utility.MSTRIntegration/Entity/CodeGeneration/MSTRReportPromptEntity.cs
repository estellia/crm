/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/10 14:11:51
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
    /// 实体： MSTR报表所关联的Prompt 
    /// </summary>
    public partial class MSTRReportPromptEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MSTRReportPromptEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自增主键
		/// </summary>
		public int? ReportPromptID { get; set; }

		/// <summary>
		/// 报表ID,表MSTRReport的外键
		/// </summary>
		public int? ReportID { get; set; }

		/// <summary>
		/// 提问ID，表MSTRPrompt的外键.
		/// </summary>
		public int? PromptID { get; set; }

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