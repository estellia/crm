using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.Base
{
    /// <summary>
    /// API响应 
    /// </summary>
    public class APIResponse<T>
        where T : IAPIResponseData
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public APIResponse()
        {
        }
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="pResponseData">响应数据对象</param>
        public APIResponse(T pResponseData)
        {
            this.Data = pResponseData;
        }
        #endregion

        #region 响应结果
        /// <summary>
        /// 结果码
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return this.ResultCode < 100; }
        }
        #endregion

        #region 响应数据
        /// <summary>
        /// 响应数据
        /// </summary>
        public T Data { get; set; }
        #endregion
    }
}