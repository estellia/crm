using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.ValueObject
{
    /// <summary>
    /// 接口类型 
    /// </summary>
    public enum APITypes
    {
        /// <summary>
        /// 产品接口
        /// </summary>
        Product
        ,
        /// <summary>
        /// 项目接口
        /// </summary>
        Project
        ,
        /// <summary>
        /// 演示接口
        /// </summary>
        Demo
    }

    /// <summary>
    /// APITypes的扩展方法类
    /// </summary>
    public static class APITypesExtensionMethods
    {
        /// <summary>
        /// 扩展方法：获取API类型的编码
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string ToCode(this APITypes pCaller)
        {
            switch (pCaller)
            {
                case APITypes.Demo:
                    return "Demo";
                case APITypes.Product:
                    return "Product";
                case APITypes.Project:
                    return "Project";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}