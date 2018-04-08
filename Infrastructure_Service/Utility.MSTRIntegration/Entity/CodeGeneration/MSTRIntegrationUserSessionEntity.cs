/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/13 13:32:19
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
    /// 实体： 租户平台的用户登录网站后,插入一条这样的记录.MSTR的认证模块根据记录中的信息来创建/重新加载 MSTR的Session. 
    /// </summary>
    public partial class MSTRIntegrationUserSessionEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MSTRIntegrationUserSessionEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自增主键
		/// </summary>
		public int? SessionID { get; set; }

		/// <summary>
		/// 客户ID
		/// </summary>
		public string ClientID { get; set; }

		/// <summary>
		/// 用户ID
		/// </summary>
		public string UserID { get; set; }

		/// <summary>
		/// 集成平台网站的Session ID
		/// </summary>
		public string WebSessionID { get; set; }

		/// <summary>
		/// 网站用户的IP
		/// </summary>
		public string IP { get; set; }

		/// <summary>
		/// 是否检查IP,如果检查IP则用户登录平台时的IP和访问报表的IP必须是一致的.目的是防止用户将报表的URL给其他人,造成非法访问.
		/// </summary>
		public int? IsCheckIP { get; set; }

		/// <summary>
		/// 区域性标识.更改区域标识会影响到语言等设置.这里的区域性标识是Windows系统下的,通常中文为2052,英文(美国)为1033
		/// </summary>
		public int? LCID { get; set; }

		/// <summary>
		/// 租户平台网站是否更改了MSTR集成会话的一些设置.通常是更改了网站的语言设置.
		/// </summary>
		public int? IsChange { get; set; }

		/// <summary>
		/// 会话所属的MSTR IServer的名称.
		/// </summary>
		public string MSTRIServerName { get; set; }

		/// <summary>
		/// 会话所属的MSTR IServer的端口号，如果IServer采用的是默认端口，则值为0
		/// </summary>
		public int? MSTRIServerPort { get; set; }

		/// <summary>
		/// 会话所属的MSTR用户的用户名
		/// </summary>
		public string MSTRUserName { get; set; }

		/// <summary>
		/// 会话所属的MSTR用户的密码
		/// </summary>
		public string MSTRUserPassword { get; set; }

		/// <summary>
		/// 会话所属的MSTR项目的项目名称
		/// </summary>
		public string MSTRProjectName { get; set; }

		/// <summary>
		/// MSTR Session的ID
		/// </summary>
		public string MSTRSessionID { get; set; }

		/// <summary>
		/// MSTR Session的状态信息,可以利用这些信息还原MSTR Session对象.
		/// </summary>
		public string MSTRSessionState { get; set; }

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