using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models
{
    public class ResponseModel<T>
    {
        /// <summary>
        /// 响应编码
        /// </summary>
        public ResponseCode Code { get; set; }
        /// <summary>
        /// 响应信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 响应结果
        /// </summary>
        public T Result { get; set; }   //一个泛型化的任意类型的对象（对应上面写的T）
    }

    public class ResponseModel  //和上面的ResponseModel<T>同名，这里不含有对泛型的定义
    {
        /// <summary>
        /// 响应编码
        /// </summary>
        public ResponseCode Code { get; set; }
        /// <summary>
        /// 响应信息
        /// </summary>
        public string Message { get; set; }
    }

    public enum ResponseCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,

        /// <summary>
        /// 数据未找到
        /// </summary>
        DataNotFound,

        /// <summary>
        /// 失败
        /// </summary>
        Fail
    }
}
