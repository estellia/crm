/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/8 15:45:54
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
    /// 实体： 应用详情应用是指将使用交易中心中间件的系统。对于交易中心而言，应用就是客户系统 
    /// </summary>
    public partial class AppEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AppEntity()
        {
        }
        #endregion     

        #region 属性集
        /// <summary>
        /// 应用ID，自增主键
        /// </summary>
        public Int32? AppID { get; set; }

        /// <summary>
        /// 客户系统的编码
        /// </summary>
        public String AppCode { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public String AppName { get; set; }

        /// <summary>
        /// 应用描述
        /// </summary>
        public String AppDescription { get; set; }

        /// <summary>
        /// 支付通知URL
        /// </summary>
        public String NotifyUrl { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 创建者用户ID
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建者用户ID
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }


        #endregion

    }
}