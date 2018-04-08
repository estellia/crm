using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.Pay
{
    public interface IGateway
    {
        T CallAPI<T>(string pUrl, string pContent);
    }
}
