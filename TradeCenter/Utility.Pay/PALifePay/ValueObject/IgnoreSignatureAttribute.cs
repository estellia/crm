using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.PALifePay.ValueObject
{
    /// <summary>
    /// 签名时忽略该特性的字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreSignatureAttribute : Attribute
    {
    }
}
