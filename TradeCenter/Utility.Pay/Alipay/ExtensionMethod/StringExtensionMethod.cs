using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Pay.Alipay.Interface.Wap.Response;

namespace JIT.Utility.Pay.Alipay.ExtensionMethod
{
    public static class StringExtensionMethod
    {
        public static string Fmt(this string value, params object[] pObjs)
        {
            return string.Format(value, pObjs);
        }
    }
}
