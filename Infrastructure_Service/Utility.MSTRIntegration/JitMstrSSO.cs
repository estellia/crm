using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.MSTRIntegration.Base;
using JIT.Utility.MSTRIntegration.Entity;
using JIT.Utility.MSTRIntegration.BLL;
using JIT.Utility.DataAccess;
//using System.Web;
using JIT.Utility.Locale;
using System.Web.SessionState;
using System.Configuration;
using System.Data;

namespace JIT.Utility.MSTRIntegration
{
    /// <summary>
    /// Mstr单点登录相关处理类
    /// </summary>
    public class JitMstrSSO
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public JitMstrSSO()
        {
        }
        /// <summary>
        /// 单点登录
        /// </summary> 
        /// <param name="pLanguageLCID">Web平台当前语言(中文：2052，英文：1033。)</param>
        /// <param name="pClientIP">客户端IP地址</param>
        /// <param name="pClientID">登录用户所属客户标识</param>
        /// <param name="pUserID">登录用户标识</param>
        /// <param name="pWebSiteSessionId">Web站点的会话标识</param>
        public int Login(int pLanguageLCID, string pClientIP, string pClientID, string pUserID, string pWebSiteSessionId)
        {
            //TO-DO:登录成功后，准备记录MSTR集成组件所使用的单点登录信息。
            var sqlHelper = new JIT.Utility.DataAccess.DefaultSQLHelper(ConfigurationManager.AppSettings["MstrIntegrationConn"]);
            //1.根据客户获取报表服务器及项目相关信息
            var userInfo = new ReportUserInfo() { ClientID = pClientID, UserID = pUserID };
            MSTRProjectEntity mstrProjectQueryEntity = new MSTRProjectEntity();
            mstrProjectQueryEntity.ClientID = pClientID;
            MSTRProjectBLL mstrProjectBLL = new MSTRProjectBLL(userInfo, sqlHelper);
            MSTRProjectEntity[] mstrProjectEntities = mstrProjectBLL.QueryByEntity(mstrProjectQueryEntity, null);
            if (mstrProjectEntities == null || mstrProjectEntities.Length == 0)
            {
                JIT.Utility.Log.Loggers.Exception(new JIT.Utility.Log.ExceptionLogInfo(new Exception("未找到客户ID为[" + pClientID + "]的MSTR项目信息.")));
                return -1;
                throw new Exception("未找到客户ID为[" + pClientID + "]的MSTR项目信息.");
            }
            var mstrProjectInfo = mstrProjectEntities[0];
            //2.记录用户会话记录
            MSTRIntegrationUserSessionBLL mstrIntegrationUserSessionBLL = new MSTRIntegrationUserSessionBLL(userInfo, sqlHelper);
            MSTRIntegrationUserSessionEntity mstrIntegrationUserSessionQueryEntity = new MSTRIntegrationUserSessionEntity();
            mstrIntegrationUserSessionQueryEntity.UserID = pUserID;
            mstrIntegrationUserSessionQueryEntity.ClientID = pClientID;
            mstrIntegrationUserSessionQueryEntity.IP = pClientIP;
            mstrIntegrationUserSessionQueryEntity.IsChange = 0;
            mstrIntegrationUserSessionQueryEntity.IsCheckIP = 0;

            mstrIntegrationUserSessionQueryEntity.LCID = pLanguageLCID;
            mstrIntegrationUserSessionQueryEntity.MSTRIServerName = mstrProjectInfo.IServerName;
            mstrIntegrationUserSessionQueryEntity.MSTRIServerPort = mstrProjectInfo.IServerPort;
            mstrIntegrationUserSessionQueryEntity.MSTRProjectName = mstrProjectInfo.ProjectName;
            mstrIntegrationUserSessionQueryEntity.WebSessionID = pWebSiteSessionId;
            mstrIntegrationUserSessionQueryEntity.MSTRUserName = mstrProjectInfo.MSTRUserName;
            mstrIntegrationUserSessionQueryEntity.MSTRUserPassword = mstrProjectInfo.MSTRUserPassword;
            mstrIntegrationUserSessionBLL.Create(mstrIntegrationUserSessionQueryEntity);
            //记录单点登录表中的自增主键
            //HttpContext.Current.Session["MstrSSO_SessionID"] = mstrIntegrationUserSessionQueryEntity.SessionID.Value;
            //HttpContext.Current.Session["MstrSSO_UserID"] = pUserID;
            //HttpContext.Current.Session["MstrSSO_ClientID"] = pClientID;
            //HttpContext.Current.Session["MstrSSO_LCID"] = pLanguageLCID;
            return mstrIntegrationUserSessionQueryEntity.SessionID.Value;
        }
         
        ///// <summary>
        ///// Web端注销后，删除单点登录表中的记录
        ///// </summary>
        ///// <param name="pHttpSessionState">Web平台当前会话</param>
        ///// <param name="pSQLHelper">数据库操作类</param>
        //public void Logout(HttpSessionState pHttpSessionState, ISQLHelper pSQLHelper)
        //{
        //    if (pHttpSessionState["MstrSSO_Logout"] != null && (bool)pHttpSessionState["MstrSSO_Logout"])
        //        return;//已手工注销
        //    var userID = (string)pHttpSessionState["MstrSSO_UserID"];
        //    var clientID = (string)pHttpSessionState["MstrSSO_ClientID"];
        //    var userInfo = new ReportUserInfo() { ClientID = clientID, UserID = userID };
        //    MSTRIntegrationUserSessionBLL mstrIntegrationUserSessionBLL = new MSTRIntegrationUserSessionBLL(userInfo, pSQLHelper);
        //    MSTRIntegrationUserSessionEntity mstrIntegrationUserSessionQueryEntity = new MSTRIntegrationUserSessionEntity();
        //    mstrIntegrationUserSessionQueryEntity.SessionID = Convert.ToInt32(pHttpSessionState["MstrSSO_SessionID"]);
        //    mstrIntegrationUserSessionBLL.Delete(mstrIntegrationUserSessionQueryEntity);
        //    pHttpSessionState["MstrSSO_Logout"] = true;
        //}

        /// <summary>
        /// Web端注销后，删除单点登录表中的记录
        /// </summary>
        /// <param name="pHttpSessionState">Web平台当前会话</param>        
        public void Logout(string pClientId, string pUserId, int pSessionID)
        {
            var sqlHelper = new JIT.Utility.DataAccess.DefaultSQLHelper(ConfigurationManager.AppSettings["MstrIntegrationConn"]);
            var userInfo = new ReportUserInfo() { ClientID = pClientId, UserID = pUserId };
            MSTRIntegrationUserSessionBLL mstrIntegrationUserSessionBLL = new MSTRIntegrationUserSessionBLL(userInfo, sqlHelper);
            MSTRIntegrationUserSessionEntity mstrIntegrationUserSessionQueryEntity = new MSTRIntegrationUserSessionEntity();
            mstrIntegrationUserSessionQueryEntity.SessionID = pSessionID;
            mstrIntegrationUserSessionBLL.Delete(mstrIntegrationUserSessionQueryEntity);
        } 

        /// <summary>
        /// IP、是否检查IP或区域信息改变
        /// </summary>
        /// <param name="pHttpSessionState">Web平台当前会话</param>
        /// <param name="pSQLHelper">数据库操作类</param>
        /// <param name="pIP">新的IP地址（如果未改变，则传入NULL）</param>
        /// <param name="pIsCheckIP">新的 【是否检查IP】 字段值 （如果未改变，则传入NULL）</param>
        /// <param name="pLanguage">新的语言（如果未改变，则传入NULL）</param>
        public void ContextChange(string pClientId, string pUserId, int pMstrIntegrationSessionID, int pLanguageLCID)
        {
            var userInfo = new ReportUserInfo() { ClientID = pClientId, UserID = pUserId };
            var sqlHelper = new JIT.Utility.DataAccess.DefaultSQLHelper(ConfigurationManager.AppSettings["MstrIntegrationConn"]);
            MSTRIntegrationUserSessionBLL mstrIntegrationUserSessionBLL = new MSTRIntegrationUserSessionBLL(userInfo, sqlHelper);
            MSTRIntegrationUserSessionEntity mstrIntegrationUserSessionQueryEntity = mstrIntegrationUserSessionBLL.GetByID(pMstrIntegrationSessionID);
            mstrIntegrationUserSessionQueryEntity.IsChange = 1;
            mstrIntegrationUserSessionQueryEntity.LCID = pLanguageLCID;
            mstrIntegrationUserSessionBLL.Update(mstrIntegrationUserSessionQueryEntity);
        }

        /// <summary>
        /// winform程序获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetBaseDataBySQL(string sql)
        {
            var sqlHelper = new JIT.Utility.DataAccess.DefaultSQLHelper(ConfigurationManager.AppSettings["MstrIntegrationConn"]);
            return sqlHelper.ExecuteDataset(sql);
        }
    }
}
