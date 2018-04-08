using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.Base
{
    public class ErrorResponse : APIResponse<EmptyResponseData>
    {
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ErrorResponse() { base.ResultCode = ERROR_CODES.DEFAULT_ERROR; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pErrorCode">错误码</param>
        /// <param name="pErrorMessage">错误信息</param>
        public ErrorResponse(int pErrorCode, string pErrorMessage)
        {
            base.ResultCode = pErrorCode;
            base.Message = pErrorMessage;
        }
    }
}