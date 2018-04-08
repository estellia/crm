using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.ExtensionMethod
{
    public static class DictionaryExtensionMethods
    {
        public static string GetXMLString(this Dictionary<string, string> pData)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in pData)
            {
                sb.AppendFormat("<{0}>{1}</{0}>", item.Key, item.Value);
            }
            return sb.ToString();
        }
    }
}
