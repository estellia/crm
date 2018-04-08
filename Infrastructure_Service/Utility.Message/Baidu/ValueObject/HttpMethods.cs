/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/3 13:17:16
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

namespace JIT.Utility.Message.Baidu.ValueObject
{
    /// <summary>
    /// Http请求方法 
    /// </summary>
    public enum HttpMethods
    {
        POST
        ,
        GET
    }

    public static class HttpMethodsExtensionMethods
    {
        public static string GetDescription(this HttpMethods pCaller)
        {
            switch (pCaller)
            {
                case HttpMethods.POST:
                    return "POST";
                case HttpMethods.GET:
                    return "GET";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
