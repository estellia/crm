/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/17 19:34:37
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

namespace JIT.MessageService.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class SMSSendEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SMSSendEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 
		/// </summary>
		public Int32? SMSSendID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String MobileNO { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String SMSContent { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? SendTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? RegularlySendTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SMSSendNO { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? UserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SMSStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Mtmsgid { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? ReceiveUserID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SendCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public String Sign { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Int32? SMSSource { get; set; }


        #endregion

    }
}