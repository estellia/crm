/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/9 13:57:27
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
    public partial class MobileCommandRecordEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public MobileCommandRecordEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? RecordID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ClientID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UserID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AppCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? AppVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CommandText { get; set; }

        /// <summary>
        /// 状态：0-初始，1-已发送，2-收到响应，3-无法执行的命令，100-已完成
        /// </summary>
        public Int32? Status { get; set; }

        /// <summary>
        /// 请求次数
        /// </summary>
        public Int32? RequestCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ResponseJson { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? ResponseCode { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public Int32? CommandResponseCount { get; set; }

        /// <summary>
        /// 有效期（秒）
        /// </summary>
        public Int32? Period { get; set; }

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