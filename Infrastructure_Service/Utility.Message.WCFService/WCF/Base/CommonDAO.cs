using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.DataAccess;
using JIT.Utility;

namespace JIT.Utility.Message.WCF.Base
{
    public class CommonDAO:BaseDAO<BasicUserInfo>
    {
        public CommonDAO(BasicUserInfo userinfo)
            : base(userinfo, ConnectStringManager.Default)
        { }
    }
}
