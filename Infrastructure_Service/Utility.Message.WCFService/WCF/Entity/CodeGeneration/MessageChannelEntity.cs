/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/3 17:21:10
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

namespace JIT.Utility.Message.WCF.Entity
{
    /// <summary>
    /// 实体： 消息通道 
    /// </summary>
    public partial class MessageChannelEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MessageChannelEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自增主键
		/// </summary>
		public Int32? ChannelID { get; set; }

		/// <summary>
		/// 手机端平台1=Android2=IOS
		/// </summary>
		public Int32? MobilePlatform { get; set; }

		/// <summary>
		/// 以JSON的形式存储的设置
		/// </summary>
		public String Settings { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		public Int32? IsDelete { get; set; }

		/// <summary>
		/// 创建者的用户ID
		/// </summary>
		public String CreateBy { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 最后更新者的用户ID
		/// </summary>
		public String LastUpdateBy { get; set; }

		/// <summary>
		/// 最后更新时间
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }


        #endregion

    }
}