/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/10 14:11:50
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
    /// 实体： MSTR的提示,集成中所用到的Prompt分为两种：
    ///1.报表数据权限Prompt
    ///2.集成页面中的数据过滤的Prompt 
    /// </summary>
    public partial class MSTRPromptEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MSTRPromptEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自增主键
		/// </summary>
		public int? PromptID { get; set; }

		/// <summary>
		/// 助记符,集成平台通过PromptCode来找到相应的Prompt.不同项目下的同一个Prompt的Code是一样的.
		/// </summary>
		public string PromptCode { get; set; }

		/// <summary>
		/// Prompt对象的GUID
		/// </summary>
		public string PromptGUID { get; set; }

		/// <summary>
		/// MSTR Prompt的类型：1=ElementPrompt
		/// </summary>
		public int? PromptType { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		public int? IsDelete { get; set; }

		/// <summary>
		/// 创建者的用户ID
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// 
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