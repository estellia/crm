/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/27 14:15:00
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;

using JIT.Utility.Web;
using JIT.Utility;

namespace JIT.Utility.Web.Base
{
    /// <summary>
    /// JITMPTreeHandler 
    /// </summary>
    public abstract class JITMPTreeHandler:JITTreeHandler<BasicUserInfo>
    {

        protected override Utility.BasicUserInfo CurrentUserInfo
        {
            get { return null; }
        }

        protected override void Authenticate()
        {
            //do nothing
        }
    }
}
