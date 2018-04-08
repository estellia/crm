using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Web;
using JIT.Utility;
using JIT.Utility.DataAccess;
using JIT.Utility.MSTRIntegration.Base;

namespace JIT.Utility.MSTRIntegration
{
    /// <summary>
    /// 报表页面基类
    /// </summary>
    public abstract class JitReportBasePage1 : JitBasePage
    {
        /// <summary>
        /// 报表标识
        /// </summary>
        protected abstract string MstrReportGuid { get; }

        /// <summary>
        /// 当前用户信息
        /// </summary>
        protected abstract BasicUserInfo CurrentUserInfo { get; }
        /// <summary>
        /// 数据库访问助手
        /// </summary>
        protected abstract ISQLHelper SqlHelper { get; } 
        /// <summary>
        /// 根据参数信息获取Mstr报表地址
        /// </summary>
        /// <param name="pReportID">报表标识</param>
        /// <param name="pQueryParameter">查询条件信息</param>
        public string GetMstrUrl(PromptAnswerItem[] pQueryParameter, DataRigthHierachy pDataRigthHierachy)
        { 
            var userInfo = new ReportUserInfo() { UserID = CurrentUserInfo.UserID, ClientID = CurrentUserInfo.ClientID }; ;
            var mstrReportUtil = new MstrReportUtil( (int)Session["MstrSSO_MstrIntegrationSessionID"],this.MstrReportGuid, userInfo);
            return mstrReportUtil.GetMstrUrl(pQueryParameter,null);
        }
    }
}
