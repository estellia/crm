using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.DataAccess;

namespace JIT.Utility.Message.WCF.Base
{
    public class CommonDAO_QDY : BaseDAO<BasicUserInfo>
    {
        public CommonDAO_QDY(BasicUserInfo userinfo)
            : base(userinfo, ConnectStringManager_QDY.Default)
        { }
    }
}
