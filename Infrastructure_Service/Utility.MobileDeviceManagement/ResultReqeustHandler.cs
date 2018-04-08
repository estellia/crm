using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.Log;
using JIT.Utility.MobileDeviceManagement.DataAccess;
using JIT.Utility.DataAccess;
using System.Configuration;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.MobileDeviceManagement
{
    public class ResultReqeustHandler
    {
        public CommonResponse Process(CommandResponse pRequest)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                response.ResultCode = 0;
                DefaultSQLHelper _sqlHelper = new DefaultSQLHelper(ConfigurationManager.AppSettings["MobileLogConn"]);
                MobileCommandRecordDAO DAO = new MobileCommandRecordDAO(new Base.MobileDeviceManagementUserInfo(), _sqlHelper);
                var entity = DAO.GetByID(pRequest.CommandID);
                if (entity != null)
                {
                    entity.CommandResponseCount++;
                    entity.ResponseJson = pRequest.ToJSON();
                    if (pRequest.ResponseCode < 100)
                    {
                        entity.Status = 100;
                        entity.ResponseCode = 0;
                    }
                    else
                    {
                        entity.Status = 2;
                        entity.ResponseCode = 1;
                        if (entity.CommandResponseCount >= 3)
                        {
                            entity.Status = 3;
                        }
                    }
                    if (pRequest.NeedRepeat)
                    {
                        entity.LastUpdateTime = DateTime.Now;
                    }
                    DAO.Update(entity);
                }
                else
                {
                    throw new Exception("未找到命令");
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                response.Message = ex.Message;
                response.ResultCode = 200;
            }
            return response;
        }
    }
}
