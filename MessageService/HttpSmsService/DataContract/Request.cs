using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.ExtensionMethod;

namespace HttpSmsService.DataContract
{
    public class Request
    {
        public string Action { get; set; }
        public object Parameters { get; set; }

        public T GetParameters<T>()
        {
            if (Parameters == null)
                return default(T);
            else
                return Parameters.ToJSON().DeserializeJSONTo<T>();
        }
    }
}