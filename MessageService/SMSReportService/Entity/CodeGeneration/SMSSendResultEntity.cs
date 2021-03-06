/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/11/4 16:34:30
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

namespace JIT.SMSReportService.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class SMSSendResultEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SMSSendResultEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Int32? SendResultID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Command { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Spid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Mtmsgid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Rtstat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Rterrcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }


        #endregion

    }
}