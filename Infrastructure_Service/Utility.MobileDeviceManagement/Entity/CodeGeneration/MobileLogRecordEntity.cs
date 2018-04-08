/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/6 15:37:18
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

namespace JIT.Utility.MobileDeviceManagement.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class MobileLogRecordEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MobileLogRecordEntity()
        {
        }
        #endregion     

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Int32? RecordID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ClientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UserID { get; set; }

        /// <summary>
        /// 0-异常日志1-crash日志
        /// </summary>
        public Int32? LogType { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Tag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AppCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ResultCode { get; set; }

        /// <summary>
        /// 日志发生时间
        /// </summary>
        public DateTime? LogTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

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
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }


        #endregion
    }
}