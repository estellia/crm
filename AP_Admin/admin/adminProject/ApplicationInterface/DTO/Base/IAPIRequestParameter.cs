using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.Base
{
    /// <summary>
    /// API请求参数接口
    /// </summary>
    public interface IAPIRequestParameter
    {
        /// <summary>
        /// 验证API请求参数是否合法
        /// </summary>
        void Validate();
    }
}