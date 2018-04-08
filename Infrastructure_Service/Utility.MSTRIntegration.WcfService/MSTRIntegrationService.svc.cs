using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using JIT.Utility.MSTRIntegration.Base;
using System.Configuration;
using JIT.Utility.MSTRIntegration;

namespace JIT.MSTRIntegration.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    public class MSTRIntegrationService : IMSTRIntegrationService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        /// <summary>
        /// 获取报表访问路径
        /// </summary>
        /// <param name="pMstrIntegrationSessionID">Mstr集成组件生成的会话标识</param>
        /// <param name="pReportGuid">报表标识</param>
        /// <param name="pClientId">客户标识</param>
        /// <param name="pUserId">用户标识</param>
        /// <param name="pPromptAnswers">提问回答信息</param>
        /// <param name="pDataRigthHierachy">数据权限信息</param>
        /// <returns>Mstr报表访问Url</returns>
        public string GetMstrReportUrl(int pMstrIntegrationSessionID, string pReportGuid, string pClientId, string pUserId, MstrPromptAnswerItem[] pPromptAnswers, MstrDataRigthPromptAnswerItem[] pDataRigthPromptAnswers)
        {
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { ClientID = pClientId, UserID = pUserId, Message = "接收到请求.获取报表访问路径:[" + pMstrIntegrationSessionID + "]" });
            try
            { 
                PromptAnswerItem[] promptAnswers;
                if (pPromptAnswers == null)
                    promptAnswers = new PromptAnswerItem[0] { };
                else
                    promptAnswers = new PromptAnswerItem[pPromptAnswers.Length];
                //foreach (var item in pPromptAnswers)
                for (int i = 0; i < pPromptAnswers.Length; i++)
                {
                    var item = pPromptAnswers[i];
                    PromptAnswerItem promptAnswerItem = new PromptAnswerItem();
                    promptAnswerItem.PromptCode = item.PromptCode;
                    promptAnswerItem.PromptType = 0;
                    promptAnswerItem.QueryCondition = item.QueryCondition;
                    promptAnswers[i] = promptAnswerItem;
                }
                DataRigthHierachy dataRigthHierachy = new DataRigthHierachy();
                if (pDataRigthPromptAnswers != null)
                {

                    foreach (var item in pDataRigthPromptAnswers)
                    {
                        dataRigthHierachy.Add(item.Level, item.Values);
                    }
                }
                ReportUserInfo userInfo = new ReportUserInfo();
                userInfo.ClientID = pClientId;
                userInfo.UserID = pUserId;
                MstrReportUtil mstrReportUtil = new MstrReportUtil(pMstrIntegrationSessionID, pReportGuid, userInfo);
                var mstrUrl = mstrReportUtil.GetMstrUrl(promptAnswers, dataRigthHierachy); ;
                JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { ClientID = pClientId, UserID = pUserId, Message = "处理请求完成.mstrUrl:[" + mstrUrl + "]" });
                return mstrUrl;
            }
            catch (Exception ex)
            {
                JIT.Utility.Log.Loggers.Exception(new JIT.Utility.Log.ExceptionLogInfo(ex));
            }
            return string.Empty;
        }

         /// <summary>
        /// 单点登录
        /// </summary> 
        /// <param name="pLanguageLCID">Web平台当前语言(中文：2052，英文：1033。)</param>
        /// <param name="pClientIP">客户端IP地址</param>
        /// <param name="pClientID">登录用户所属客户标识</param>
        /// <param name="pUserID">登录用户标识</param>
        /// <param name="pWebSiteSessionId">Web站点的会话标识</param>
        /// <returns>Mstr会话标识</returns>
        public int Login(int pLanguageLCID, string pClientIP, string pClientID, string pUserID, string pWebSiteSessionId)
        {
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { ClientID = pClientID, UserID = pUserID, Message = "接收到请求.Login:[" + pWebSiteSessionId + "]" });
            JitMstrSSO jitMstrSSO = new JitMstrSSO();
            var loginResult = jitMstrSSO.Login(pLanguageLCID, pClientIP, pClientID, pUserID, pWebSiteSessionId);
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { ClientID = pClientID, UserID = pUserID, Message = "登录结果:[" + loginResult + "]" });
            return loginResult;
        }

        /// <summary>
        /// 单点登录退出
        /// </summary>
        /// <param name="pClientId">客户标识</param>
        /// <param name="pUserId">用户标识</param>
        /// <param name="pMstrIntegrationSessionID">Mstr集成组件生成的会话标识</param>
        public void Logout(string pClientId, string pUserId, int pMstrIntegrationSessionID)
        {
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { ClientID = pClientId, UserID = pUserId, Message = "接收到请求.Logout:[" + pMstrIntegrationSessionID + "]" });
            JitMstrSSO jitMstrSSO = new JitMstrSSO();
            jitMstrSSO.Logout(pClientId, pUserId, pMstrIntegrationSessionID);
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { ClientID = pClientId, UserID = pUserId, Message = "登出完毕.[" + pMstrIntegrationSessionID + "]" });
        }

        /// <summary>
        /// 进行语言切换
        /// </summary>
        /// <param name="pClientId">客户标识</param>
        /// <param name="pUserId">用户标识</param>
        /// <param name="pMstrIntegrationSessionID">Mstr集成组件的会话标识</param>
        /// <param name="pNewLanguageLCID">语言标识</param>
        public void SwitchLanguage(string pClientId, string pUserId, int pMstrIntegrationSessionID, int pNewLanguageLCID)
        {
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { ClientID = pClientId, UserID = pUserId, Message = "接收到请求.SwitchLanguage:[" + pNewLanguageLCID + "]" });
            JitMstrSSO jitMstrSSO = new JitMstrSSO();
            jitMstrSSO.ContextChange(pClientId, pUserId, pMstrIntegrationSessionID, pNewLanguageLCID);
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { ClientID = pClientId, UserID = pUserId, Message = "切换语言完成.[" + pNewLanguageLCID + "]" });
        }


        public string GetMstrReportExportUrl(int pMstrIntegrationSessionID, string pReportGuid, string pClientId, string pUserId, MstrPromptAnswerItem[] pPromptAnswers, MstrDataRigthPromptAnswerItem[] pDataRigthPromptAnswers, string pMessageId, int pExportType)
        {
            string mstrExportUrl = GetMstrReportUrl(pMstrIntegrationSessionID, pReportGuid, pClientId, pUserId, pPromptAnswers, pDataRigthPromptAnswers);
            mstrExportUrl+="&msgID="+pMessageId;
            string evtString = "&evt=3062";
            string srcString = "&src=mstrWeb.3062";
            switch (pExportType)
            {
                case 2://EXCEL
                    evtString = "&evt=3067";
                    srcString = "&src=mstrWeb.3067";
                    break;
                case 1://PDF 
                default:
                    break;
            } 
            //Type:Report
            mstrExportUrl = mstrExportUrl.Replace("&evt=4001", evtString);
            mstrExportUrl = mstrExportUrl.Replace("&src=mstrWeb.4001", srcString);
            //Type:Document
            mstrExportUrl = mstrExportUrl.Replace("&evt=2048001", evtString);
            mstrExportUrl = mstrExportUrl.Replace("&src=mstrWeb.2048001", srcString);
            mstrExportUrl += "&showOptionsPage=false&fastExport=true";
            return mstrExportUrl;
        }


        public System.Data.DataSet GetBaseDataBySQL(string sql)
        {
            JitMstrSSO sso = new JitMstrSSO();
            return sso.GetBaseDataBySQL(sql);
        }
    }
}
