/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/26 14:18:17
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

namespace Utility.Sync.WCFService.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class SyncLogEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SyncLogEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 日志ID
		/// </summary>
		public Guid? LogID { get; set; }

		/// <summary>
		/// 来源ID[商品/商家/其他]
		/// </summary>
		public String SourceItemID { get; set; }

		/// <summary>
		/// 来源类型,1：收藏；2：取消收藏
		/// </summary>
		public Int32? SourceType { get; set; }

		/// <summary>
		/// 客户ID
		/// </summary>
		public String ClientID { get; set; }

		/// <summary>
		/// 会员ID
		/// </summary>
		public String MemberID { get; set; }

		/// <summary>
		/// 描述,可以放需要传输的参数
		/// </summary>
		public String Description { get; set; }

		/// <summary>
		/// 是否已同步，1：已同步；2：未同步
		/// </summary>
		public Int32? IsNotSync { get; set; }

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