/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/17 11:16:35
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

namespace JIT.Utility.AppVersion.Entity
{
    /// <summary>
    /// 实体：  
    /// </summary>
    public partial class AppVersionEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AppVersionEntity()
        {
        }
        #endregion     

        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? AppVersionID { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public Int32? AppID { get; set; }

        /// <summary>
        /// 应用代码
        /// </summary>
        public String AppCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public String Version { get; set; }

        /// <summary>
        /// App描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String AndroidPackageUrl { get; set; }

        /// <summary>
        /// 安装包URL
        /// </summary>
        public String IOSPackageUrl { get; set; }

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