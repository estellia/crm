using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace JIT.Utility.AppVersion.DataAccess.Base
{
    public class CommonConectStringManager : JIT.Utility.DataAccess.IConnectionStringManager
    {
        private static CommonConectStringManager _default;

        public static CommonConectStringManager GetInstance(Utility.BasicUserInfo pUserInfo)
        {
            if (_default != null)
                return _default;
            else
                return _default = new CommonConectStringManager();
        }

        public string GetConnectionStringBy(Utility.BasicUserInfo pUserInfo)
        {
            return ConfigurationManager.AppSettings["Conn"];
        }
    }
}
