using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay.Alipay.Interface.Wap.Response
{
    public class AliPayWapQueryOrderResponse
    {
        public bool IsSucess { get; set; }
        public TokenResponse TokenResponse { get; set; }
        public string RedirectURL { get; set; }
        public string Message { get; set; }
    }
}
