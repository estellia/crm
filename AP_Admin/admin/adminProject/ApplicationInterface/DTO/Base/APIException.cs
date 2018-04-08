using adminProject.ApplicationInterface.DTO.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.Base
{
    /// <summary>
    /// API调用异常 
    /// </summary>
    public class APIException : Exception
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public APIException()
        {
        }
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="pErrorMessage">错误信息</param>
        public APIException(string pErrorMessage)
            : base(pErrorMessage)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pErrorCode">错误码</param>
        /// <param name="pErrorMessage">错误信息</param>
        public APIException(int pErrorCode, string pErrorMessage)
            : base(pErrorMessage)
        {
            this.ErrorCode = pErrorCode;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pErrorCode">错误码</param>
        /// <param name="pErrorMessage">错误信息</param>
        /// <param name="pInnerException">内部异常</param>
        public APIException(int pErrorCode, string pErrorMessage, Exception pInnerException)
            : base(pErrorMessage, pInnerException)
        {
            this.ErrorCode = pErrorCode;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 所属模块
        /// </summary>
        public Modules Module { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorCode { get; set; }
        #endregion
    }
}