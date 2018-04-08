/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/13 14:47:38
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

namespace JIT.TradeCenter.Entity
{
    /// <summary>
    /// 实体： 访问白名单 
    /// </summary>
    public partial class AppWhiteListEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AppWhiteListEntity()
        {
        }
        #endregion     


        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? AppWhiteListID { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public String IPAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AppID { get; set; }

        /// <summary>
        /// 正则表达式,匹配格式:clientid
        /// </summary>
        public String Regex { get; set; }

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