using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.Base
{
    /// <summary>
    /// 成功的响应 
    /// </summary>
    public class SuccessResponse<T> : APIResponse<T>
        where T : IAPIResponseData
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public SuccessResponse()
        {
            this.ResultCode = ERROR_CODES.SUCCESS;
            this.Message = "OK";
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pResponseData">响应数据</param>
        public SuccessResponse(T pResponseData)
        {
            this.ResultCode = ERROR_CODES.SUCCESS;
            this.Message = "OK";
            this.Data = pResponseData;
        }
        #endregion
    }
}