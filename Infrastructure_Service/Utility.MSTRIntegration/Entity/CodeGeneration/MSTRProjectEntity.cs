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
    /// 实体： MSTR的项目,这里的信息主要用于创建MSTR的Session，同时项目的信息也用于在集成时生成访问MSTR报表的Url 
    /// </summary>
    public partial class MSTRProjectEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MSTRProjectEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自增主键
		/// </summary>
		public int? ProjectID { get; set; }

		/// <summary>
		/// 该MSTR项目属于哪个客户,通常为一个客户创建一个MSTR项目,因此,一个客户最多只有一个MSTR项目
		/// </summary>
		public string ClientID { get; set; }

		/// <summary>
		/// MSTR IServer的名称
		/// </summary>
		public string IServerName { get; set; }

		/// <summary>
		/// MSTR IServer的端口,如果是采用默认端口,则值为0
		/// </summary>
		public int? IServerPort { get; set; }

		/// <summary>
		/// 访问MSTR报表的根Url
		/// </summary>
		public string WebServerBaseUrl { get; set; }

		/// <summary>
		/// MSTR项目的名称
		/// </summary>
		public string ProjectName { get; set; }

		/// <summary>
		/// MSTR中项目的ID
		/// </summary>
		public string ProjectGUID { get; set; }

		/// <summary>
		/// 该MSTR项目所使用的MSTR用户名。这些信息用于创建MSTR的Session
		/// </summary>
		public string MSTRUserName { get; set; }

		/// <summary>
		/// MSTR用户的密码,该信息用于创建MSTR的Session
		/// </summary>
		public string MSTRUserPassword { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		public int? IsDelete { get; set; }

		/// <summary>
		/// 创建者用户ID
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 最后更新用户ID
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }


        #endregion

    }
}