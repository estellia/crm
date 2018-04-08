using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.AppVersion.DataAccess.Base
{
    public class CommonDAO : JIT.Utility.DataAccess.BaseDAO<JIT.Utility.BasicUserInfo>
    {
        public CommonDAO(JIT.Utility.BasicUserInfo pUserInfo)
            : base(pUserInfo, CommonConectStringManager.GetInstance(pUserInfo))
        { }
    }
}
