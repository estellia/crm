using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using cPos.Dex.ContractModel;

namespace cPos.Dex.WcfService
{
    [ServiceContract(Namespace = Common.Config.NS)]
    public interface IBizLogService
    {
        //// GetLogs
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    UriTemplate = "getlogs?user_id={userId}&user_pwd={userPwd}&start_row={startRow}&rows_count={rowsCount}")]
        //GetLogsContract GetLogsJson(string userId, string userPwd,
        //    long startRow, long rowsCount, LogQueryInfo queryInfo);

        //// GetLogsCount
        //[OperationContract]
        //[WebInvoke(Method = "POST",
        //    UriTemplate = "getlogscount?user_id={userId}&user_pwd={userPwd}")]
        //GetCountContract GetLogsCountJson(string userId, string userPwd,
        //    LogQueryInfo queryInfo);

        //// GetLog
        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    UriTemplate = "getlog?user_id={userId}&user_pwd={userPwd}&log_id={logId}")]
        //GetLogContract GetLogJson(string userId, string userPwd,
        //    string logId);
    }
}
