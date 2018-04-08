using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.Base
{
    /// <summary>
    /// 空请求参数 
    /// </summary>
    public class EmptyRequestParameter : IAPIRequestParameter
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EmptyRequestParameter()
        {
        }
        /// <summary>
        /// 店主vipID
        /// </summary>
        public string OwnerVipID { get; set; }
        #endregion

        #region IAPIRequestParameter 成员
        /// <summary>
        /// 执行验证
        /// </summary>
        public virtual void Validate()
        {
            //do nothing
        }
        #endregion
    }

    public class PageQueryRequestParameter : EmptyRequestParameter
    {
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; }
    }
}