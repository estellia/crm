using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.DataAccess;
using JIT.Utility;

namespace Utility.Sync.WCFService.DataAccess.Base
{
    public class CommonDAL : BaseDAO<BasicUserInfo>
    {
        public CommonDAL(BasicUserInfo userinfo)
            : base(userinfo, ConnectStringManager.Default)
        { }
    }
}
