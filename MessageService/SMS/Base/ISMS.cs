using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.SMS.Base
{
    public interface ISMS
    {
        string Send(SendType type);

        string GetURL();

        string GetParamStr();

        byte[] GetData();
    }
}
