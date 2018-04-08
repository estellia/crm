using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using JIT.Utility.DataAccess;

namespace JIT.Utility.Message.WCF.Base
{
    public class ConnectStringManager_QDY : IConnectionStringManager
    {
        private ConnectStringManager_QDY()
        { }
        public string GetConnectionStringBy(JIT.Utility.BasicUserInfo pUserInfo)
        {
            return ConfigurationManager.AppSettings["qdyconn"];
        }

        public static ConnectStringManager_QDY _default;
        public static ConnectStringManager_QDY Default
        {
            get
            {
                if (_default != null)
                    return _default;
                else return _default = new ConnectStringManager_QDY();
            }
        }
    }
}
