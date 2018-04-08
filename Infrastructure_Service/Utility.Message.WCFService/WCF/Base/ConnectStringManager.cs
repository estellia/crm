using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.DataAccess;
using System.Configuration;

namespace JIT.Utility.Message.WCF.Base
{
    public class ConnectStringManager : IConnectionStringManager
    {
        private ConnectStringManager()
        { }
        public string GetConnectionStringBy(JIT.Utility.BasicUserInfo pUserInfo)
        {
            return ConfigurationManager.AppSettings["conn"];
        }

        public static ConnectStringManager _default;
        public static ConnectStringManager Default
        {
            get
            {
                if (_default != null)
                    return _default;
                else return _default = new ConnectStringManager();
            }
        }
    }
}
