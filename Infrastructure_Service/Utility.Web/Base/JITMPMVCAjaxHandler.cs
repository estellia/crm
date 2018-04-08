/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/1/19 13:19:58
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
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.IO;
using System.Threading;

using JIT.Utility;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Web;

namespace JIT.Utility.Web.Base
{
    /// <summary>
    /// JIT的MVC模式的Ajax请求处理者 
    /// </summary>
    public abstract class JITMPMVCAjaxHandler:JITMVCAjaxHandler<BasicUserInfo>
    {
        /// <summary>
        /// 当前的基础信息
        /// </summary>
        //protected override BasicUserInfo CurrentUserInfo
        //{
        //    get { return SessionManager.CurrentUserInfo; }
        //}

        /// <summary>
        /// 认证
        /// </summary>
        protected override void Authenticate()
        {
            //do nothing
        }
    }
}
