using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.DataAccess;
using System.Configuration;
using JIT.Utility.MobileDeviceManagement.Entity;
using JIT.Utility.MobileDeviceManagement.BLL;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;

namespace JIT.Utility.MobileDeviceManagement
{
    public class LogRequestHandler
    {
        public CommonResponse Process(LogRequest[] pRequests)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                DefaultSQLHelper _sqlHelper = new DefaultSQLHelper(ConfigurationManager.AppSettings["MobileLogConn"]);
                MobileLogRecordBLL MobileLogRecordBLL = new MobileLogRecordBLL(new Base.MobileDeviceManagementUserInfo() { ClientID = pRequests[0].ClientID, UserID = pRequests[0].UserID }, _sqlHelper);
                List<MobileLogRecordEntity> entityList = new List<MobileLogRecordEntity> { };
                foreach (var pRequest in pRequests)
                {
                    MobileLogRecordEntity entity = new MobileLogRecordEntity();
                    entity.ClientID = pRequest.ClientID;
                    entity.UserID = pRequest.UserID;
                    entity.LogType = pRequest.LogType;
                    entity.LogTime = pRequest.GetLogTime();
                    entity.AppCode = pRequest.AppCode;
                    entity.ResultCode = "0";
                    entity.Message = pRequest.Message;
                    entity.CreateTime = DateTime.Now;
                    entity.IsDelete = 0;
                    entity.Tag = pRequest.Tag;
                    entityList.Add(entity);
                }

                response.ResultCode = 0;
                //存数据库
                //发送邮件
                var transaction = _sqlHelper.CreateTransaction();
                using (transaction.Connection)
                {
                    try
                    {
                        foreach (var item in entityList)
                        {
                            MobileLogRecordBLL.Create(item, transaction);
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
                if (MailHelper.MailUserRegex != null)
                {
                    var list = MailHelper.MailUsers.ToList().FindAll(t =>
                    {
                        var reg = MailHelper.MailUserRegex.IsMatch(t.AppCode + "|" + t.ClientID);
                        return reg;
                    }).ToArray();
                    MailHelper.SendMail(list, pRequests);
                }
                else
                {
                    foreach (var item in MailHelper.MailUsers)
                    {

                        if (item.Type == 0)
                            MailHelper.SendMail(item, pRequests.ToList().FindAll(t => t.AppCode == item.AppCode).ToArray());
                        else

                            MailHelper.SendMail(item, pRequests.ToList().FindAll(t => t.AppCode == item.AppCode && t.ClientID == item.ClientID).ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                response.ResultCode = 100;
                response.Message = ex.Message;
                Loggers.Exception(new ExceptionLogInfo(ex));
                return response;
            }
            return response;
        }
    }
}
