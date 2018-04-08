using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using JIT.Utility.MSTRIntegration.Base;
using System.Data;

namespace JIT.MSTRIntegration.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IMSTRIntegrationService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        string GetMstrReportUrl(int pMstrIntegrationSessionID, string pReportGuid, string pClientId, string pUserId, MstrPromptAnswerItem[] pPromptAnswers, MstrDataRigthPromptAnswerItem[] pDataRigthPromptAnswers);

        [OperationContract]
        string GetMstrReportExportUrl(int pMstrIntegrationSessionID, string pReportGuid, string pClientId, string pUserId, MstrPromptAnswerItem[] pPromptAnswers, MstrDataRigthPromptAnswerItem[] pDataRigthPromptAnswers,string pMessageId,int pExportType);

        [OperationContract]
        int Login(int pLanguageLCID, string pClientIP, string pClientID, string pUserID,string pWebSiteSessionId);
        
        [OperationContract]
        void Logout(string pClientId,string pUserId,int pSessionID);

        [OperationContract]
        void SwitchLanguage(string pClientId, string pUserId, int pMstrIntegrationSessionID, int pNewLanguageLCID);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        DataSet GetBaseDataBySQL(string sql);
        // TODO: 在此添加您的服务操作
    }


    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
