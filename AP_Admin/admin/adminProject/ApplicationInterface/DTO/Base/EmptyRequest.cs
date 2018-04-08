using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.Base
{
    /// <summary>
    /// 空请求
    /// <remarks>
    /// <para>请求中只包含公共参数,接口参数无数据项的</para>
    /// </remarks>
    /// </summary>
    public class EmptyRequest : APIRequest<EmptyRequestParameter>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EmptyRequest()
        {
        }
        #endregion
    }
}