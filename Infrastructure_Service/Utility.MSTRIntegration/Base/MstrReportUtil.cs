using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.DataAccess;
using JIT.Utility.MSTRIntegration.BLL;
using JIT.Utility.MSTRIntegration.Entity;
using JIT.Utility.DataAccess.Query;
using System.Web.SessionState;
using System.Web;
using System.Configuration;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.MSTRIntegration.Base
{
    /// <summary>
    /// Mstr报表相关工具类
    /// </summary>
    public class MstrReportUtil
    {
        /// <summary>
        /// 当前登录会话标识
        /// </summary>
        private int _mstrIntegrationSessionID;
        /// <summary>
        /// 报表标识
        /// </summary>
        private string _mstrReportGuid;

        /// <summary>
        /// 当前用户信息
        /// </summary>
        private ReportUserInfo _currentUserInfo;

        /// <summary>
        /// 数据库访问助手
        /// </summary>
        private ISQLHelper _sqlHelper;

        #region 构造器 
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="pReportGuid">报表标识</param>
        /// <param name="pReportUserInfo">用户信息</param>
        /// <param name="pSQLHelper">数据库操作类</param>
        public MstrReportUtil(int pMstrIntegrationSessionID, string pReportGuid, ReportUserInfo pReportUserInfo)
        {
            this._mstrIntegrationSessionID = pMstrIntegrationSessionID;
            this._mstrReportGuid = pReportGuid;
            this._currentUserInfo = pReportUserInfo;
            this._sqlHelper = new JIT.Utility.DataAccess.DefaultSQLHelper(ConfigurationManager.AppSettings["MstrIntegrationConn"]); ;
        }
        #endregion 构造器

        /// <summary>
        /// 获取Mstr格式的Url
        /// </summary>
        /// <param name="pQueryParameter">提问回答对象</param>
        /// <param name="pDataRigthHierachy">用于处理数据权限的提问回答</param>
        /// <returns>Mstr格式的Url</returns>
        public string GetMstrUrl(PromptAnswerItem[] pQueryParameter, DataRigthHierachy pDataRigthHierachy)
        {
            //if (this._httpSessionState["MstrSSO_SessionID"] == null)
            //    throw new Exception("会话超时，请重新登录。");
            #region Mstr项目、报表等信息
              //项目信息
            MSTRProjectEntity mstrProjectQueryEntity = new MSTRProjectEntity();
            //mstrProjectQueryEntity.ProjectID = reportInfo.ProjectID;
            mstrProjectQueryEntity.ClientID = this._currentUserInfo.ClientID;//一个客户仅允许一个项目
            MSTRProjectBLL mstrProjectBLL = new MSTRProjectBLL(this._currentUserInfo, this._sqlHelper);
            MSTRProjectEntity[] mstrProjectEntities = mstrProjectBLL.QueryByEntity(mstrProjectQueryEntity, null);
            if (mstrProjectEntities == null || mstrProjectEntities.Length == 0)
            {
                return null;
                throw new Exception("未找到客户标识为[" + this._currentUserInfo.ClientID + "]的报表项目。");
            }
            var projectInfo = mstrProjectEntities[0];

            //获取报表信息
            MSTRReportEntity mstrReportQueryEntity = new MSTRReportEntity();
            if (this._mstrReportGuid == null)
            {//用于刷新的报表
                mstrReportQueryEntity.ProjectID = projectInfo.ProjectID;
                mstrReportQueryEntity.ReportType = 99;
                //mstrReportQueryEntity.ReportName = "KeepSession";
            }
            else
            {
                mstrReportQueryEntity.ProjectID = projectInfo.ProjectID;
                mstrReportQueryEntity.ReportGUID = this._mstrReportGuid.ToString();
            }
            MSTRReportBLL mstrReportBLL = new MSTRReportBLL(this._currentUserInfo, this._sqlHelper);
            MSTRReportEntity[] mstrReportEntities = mstrReportBLL.QueryByEntity(mstrReportQueryEntity, null);
            if (mstrReportEntities == null || mstrReportEntities.Length == 0)
                throw new Exception("未找到标识为[" + this._mstrReportGuid + "]的报表。");
            var reportInfo = mstrReportEntities[0];

            //取出当前报表所有Prompt            
            MSTRReportPromptEntity mstrReportPromptQueryEntity = new MSTRReportPromptEntity();
            mstrReportPromptQueryEntity.ReportID = reportInfo.ReportID;
            MSTRReportPromptBLL mstrReportPromptBLL = new MSTRReportPromptBLL(this._currentUserInfo, this._sqlHelper);
            MSTRReportPromptEntity[] mstrReportPromptEntities = mstrReportPromptBLL.QueryByEntity(mstrReportPromptQueryEntity, null);
            int mappintCount = (mstrReportPromptEntities == null ? 0 : mstrReportPromptEntities.Length);
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { ClientID = this._currentUserInfo.ClientID, UserID = this._currentUserInfo.UserID, Message = string.Format("报表[{0}]所关联的提问数量为[{1}]", reportInfo.ReportName, mappintCount) });
          
            #endregion

            #region 依次生成Mstr所需格式的PromptAnswer
            string mstrPromptAnswerUrl = string.Empty;
            if (mstrReportPromptEntities != null && mstrReportPromptEntities.Length > 0)
            {
                //取出每个Prompt的详情
                int[] promptIds = new int[mstrReportPromptEntities.Length];
                for (int i = 0; i < mstrReportPromptEntities.Length; i++)
                {
                    promptIds[i] = mstrReportPromptEntities[i].PromptID.Value;
                }
                IWhereCondition[] whereCondition = new IWhereCondition[1];
                JIT.Utility.DataAccess.Query.InCondition<int> promptIDsCondition = new JIT.Utility.DataAccess.Query.InCondition<int>();
                promptIDsCondition.FieldName = "PromptID";
                promptIDsCondition.Values = promptIds;
                whereCondition[0] = promptIDsCondition;
                MSTRPromptBLL mstrPromptBLL = new MSTRPromptBLL(this._currentUserInfo, this._sqlHelper);
                MSTRPromptEntity[] mstrPromptEntities = mstrPromptBLL.Query(whereCondition, null);

                //依次拼接Prompt
                //记录各个类型的提问答案
                Dictionary<PromptAnswerType, string> dictPromptAnswers = new Dictionary<PromptAnswerType, string>();
                dictPromptAnswers.Add(PromptAnswerType.ElementsPromptAnswer, string.Empty);
                dictPromptAnswers.Add(PromptAnswerType.ObjectsPromptAnswer, string.Empty);
                dictPromptAnswers.Add(PromptAnswerType.ValuePromptAnswer, string.Empty);
                
                //合并提问回答
                List<PromptAnswerItem> lstAnswers = new List<PromptAnswerItem>();
                lstAnswers.AddRange(pQueryParameter);
                if (pDataRigthHierachy != null)
                    lstAnswers.AddRange(pDataRigthHierachy.PromptAnswerItems);
                //处理空回答
                foreach (var promptItem in mstrPromptEntities)
                {
                    var answeredItem = lstAnswers.ToArray().Where(answer => answer.PromptCode == promptItem.PromptCode).ToArray();
                    if (answeredItem.Length == 0)
                    {//未回答项,准备空回答
                        PromptAnswerItem emptyPromptAnswerItem = new PromptAnswerItem();
                        emptyPromptAnswerItem.PromptCode = promptItem.PromptCode;
                        emptyPromptAnswerItem.PromptType = (PromptAnswerType)promptItem.PromptType;
                        emptyPromptAnswerItem.QueryCondition = new string[0] { };
                        lstAnswers.Add(emptyPromptAnswerItem);
                    }
                }

                //List<int> lstAnsweredPromptId=new List<int>();//已提供回答的提问项标识
                foreach (var answerItem in lstAnswers)
                {
                    //根据代码查找Prompt
                    var promptItems = mstrPromptEntities.Where(item => item.PromptCode == answerItem.PromptCode).ToArray();
                    if (promptItems == null || promptItems.Length == 0)
                        throw new Exception("配置表中未找到代码为[" + answerItem.PromptCode + "]的报表提问。");
                    if (promptItems.Length > 1)
                        throw new Exception("配置表中代码为[" + answerItem.PromptCode + "]的报表提问出现多次（" + promptItems.Length.ToString() + "次）。");

                    var promptItem = promptItems[0];
                    answerItem.PromptType = (PromptAnswerType)promptItem.PromptType;
                    //拼接Mstr格式的URL
                    string promptAnswerString = GetPromptAnswerString(answerItem.PromptType, promptItem.PromptGUID, answerItem.QueryCondition);
                    switch (answerItem.PromptType)
                    {
                        case PromptAnswerType.ElementsPromptAnswer:
                            dictPromptAnswers[PromptAnswerType.ElementsPromptAnswer] += promptItem.PromptGUID + ";" + promptAnswerString + ",";
                            break;
                        case PromptAnswerType.ValuePromptAnswer:
                            dictPromptAnswers[PromptAnswerType.ValuePromptAnswer] += promptAnswerString + "^";
                            break;
                        case PromptAnswerType.ObjectsPromptAnswer:
                            dictPromptAnswers[PromptAnswerType.ObjectsPromptAnswer] += promptAnswerString + "^";
                            break;
                        default:
                            throw new Exception("未知的提问类型");
                    }
                }

                if (dictPromptAnswers[PromptAnswerType.ElementsPromptAnswer] != string.Empty)
                {
                    if (dictPromptAnswers[PromptAnswerType.ElementsPromptAnswer].EndsWith(","))
                        dictPromptAnswers[PromptAnswerType.ElementsPromptAnswer] = dictPromptAnswers[PromptAnswerType.ElementsPromptAnswer].Substring(0, dictPromptAnswers[PromptAnswerType.ElementsPromptAnswer].Length - 1);
                    mstrPromptAnswerUrl += "&elementsPromptAnswers=" + dictPromptAnswers[PromptAnswerType.ElementsPromptAnswer];
                }
                if (dictPromptAnswers[PromptAnswerType.ObjectsPromptAnswer] != string.Empty)
                {
                    if (dictPromptAnswers[PromptAnswerType.ObjectsPromptAnswer].EndsWith("^"))
                        dictPromptAnswers[PromptAnswerType.ObjectsPromptAnswer] = dictPromptAnswers[PromptAnswerType.ObjectsPromptAnswer].Substring(0, dictPromptAnswers[PromptAnswerType.ObjectsPromptAnswer].Length - 1);
                    mstrPromptAnswerUrl += "&objectsPromptAnswers=" + dictPromptAnswers[PromptAnswerType.ObjectsPromptAnswer];
                }
                if (dictPromptAnswers[PromptAnswerType.ValuePromptAnswer] != string.Empty)
                {
                    if (dictPromptAnswers[PromptAnswerType.ValuePromptAnswer].EndsWith("^"))
                        dictPromptAnswers[PromptAnswerType.ValuePromptAnswer] = dictPromptAnswers[PromptAnswerType.ValuePromptAnswer].Substring(0, dictPromptAnswers[PromptAnswerType.ValuePromptAnswer].Length - 1);
                    mstrPromptAnswerUrl += "&valuePromptAnswers=" + dictPromptAnswers[PromptAnswerType.ValuePromptAnswer];
                }
            }
            #endregion 依次生成Mstr所需格式的PromptAnswer
            string reportUrl;
            switch ((ReportType)reportInfo.ReportType.Value)
            {
                case ReportType.Report:
                    reportUrl = string.Format("{0}&port={1}&evt=4001&src=mstrWeb.4001&reportID={2}&ipclientid={4}&ipsessionid={5}&reportViewMode={6}{3}", projectInfo.WebServerBaseUrl, projectInfo.IServerPort, reportInfo.ReportGUID, mstrPromptAnswerUrl, this._currentUserInfo.ClientID, this._mstrIntegrationSessionID, reportInfo.ReportViewMode);
                    //reportUrl = string.Format("{0}&port={1}&evt=4001&src=mstrWeb.4001&reportID={2}&reportViewMode={6}", projectInfo.WebServerBaseUrl, projectInfo.IServerPort, reportInfo.ReportGUID, mstrPromptAnswerUrl, this._currentUserInfo.ClientID, (int)this._httpSessionState["MstrSSO_SessionID"], reportInfo.ReportViewMode);
                    break;
                case ReportType.Document:
                    reportUrl = string.Format("{0}&port={1}&evt=2048001&src=mstrWeb.2048001&documentID={2}&ipclientid={4}&ipsessionid={5}&reportViewMode={6}{3}", projectInfo.WebServerBaseUrl, projectInfo.IServerPort, reportInfo.ReportGUID, mstrPromptAnswerUrl, this._currentUserInfo.ClientID, this._mstrIntegrationSessionID, reportInfo.ReportViewMode);
                    //reportUrl = string.Format("{0}&port={1}&evt=2048001&src=mstrWeb.2048001&documentID={2}&reportViewMode={6}", projectInfo.WebServerBaseUrl, projectInfo.IServerPort, reportInfo.ReportGUID, mstrPromptAnswerUrl, this._currentUserInfo.ClientID, (int)this._httpSessionState["MstrSSO_SessionID"], reportInfo.ReportViewMode);
                    break;
                case ReportType.KeepSesson:
                    reportUrl = string.Format("{0}&port={1}&evt=4001&src=mstrWeb.4001&reportID={2}&ipclientid={4}&ipsessionid={5}&reportViewMode={6}{3}", projectInfo.WebServerBaseUrl, projectInfo.IServerPort, reportInfo.ReportGUID, mstrPromptAnswerUrl, this._currentUserInfo.ClientID, this._mstrIntegrationSessionID, reportInfo.ReportViewMode);
                    break;
                default:
                    throw new Exception("无效的报表类型.");
                    break;
            }            
            return reportUrl;
        }

        /// <summary>
        /// 根据提问及回答信息生成Mstr格式的字符串
        /// </summary>
        /// <param name="pPromptAnswerType">提问类型</param>
        /// <param name="pPromptGuid">提问标识</param>
        /// <param name="pQueryCondition">回答结果</param>
        /// <returns>Mstr格式的提问回答字符串</returns>
        private string GetPromptAnswerString(PromptAnswerType pPromptAnswerType, string pPromptGuid, string[] pQueryCondition)
        {
            WriteDebugLog("pPromptAnswerType:【"+pPromptAnswerType.ToJSON()+"】,"+"pPromptGuid:【"+pPromptGuid.ToJSON()+"】,pQueryCondition:【"+pQueryCondition.ToJSON()+"】.");
            return GetPromptAnswer(pPromptAnswerType, pPromptGuid).GetAnswerExpression(pQueryCondition);
        }

        /// <summary>
        /// 根据类型获取提问回答处理类
        /// </summary>
        /// <param name="pPromptAnswerType">类型</param>
        /// <param name="pPromptGuid">提问标识</param>
        /// <returns>提问回答处理类</returns>
        private IPromptAnswer GetPromptAnswer(PromptAnswerType pPromptAnswerType, string pPromptGuid)
        {
            switch (pPromptAnswerType)
            {
                case PromptAnswerType.ElementsPromptAnswer:
                    return new ElementsPromptAnswer(pPromptGuid);
                    break;
                case PromptAnswerType.ValuePromptAnswer:
                    return new ValuePromptAnswer(pPromptGuid);
                    break;
                case PromptAnswerType.ObjectsPromptAnswer:
                    return new ObjectsPromptAnswer(pPromptGuid);
                    break;
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="pMessage">日志信息</param>
        private void WriteDebugLog(string pMessage)
        {
            JIT.Utility.Log.Loggers.Debug(new JIT.Utility.Log.DebugLogInfo() { UserID = this._currentUserInfo.UserID, ClientID = this._currentUserInfo.ClientID, Message = pMessage });
        }
    }
}
