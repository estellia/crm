using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.MobileDeviceManagement.DataAccess;
using JIT.Utility.DataAccess;
using System.Configuration;
using JIT.Utility.Log;

namespace JIT.Utility.MobileDeviceManagement
{
    public class CommandRequestHandler
    {
        public string[] Process(CommandRequest pRequest)
        {
            try
            {
                DefaultSQLHelper _sqlHelper = new DefaultSQLHelper(ConfigurationManager.AppSettings["MobileLogConn"]);
                MobileCommandRecordDAO DAO = new MobileCommandRecordDAO(new Base.MobileDeviceManagementUserInfo(), _sqlHelper);
                var entitys = DAO.GetCommand(pRequest.ClientID, pRequest.UserID);
                if (entitys.Length > 0)
                {
                    if (DAO.UpdateIsDelete(_sqlHelper.CreateTransaction(), entitys) > 0)
                        return entitys.Aggregate(new List<string>() { }, (i, j) =>
                            {
                                i.Add(j.RecordID + " " + j.CommandText);
                                return i;
                            }).ToArray();
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                return new List<string>() { ex.Message }.ToArray();
            }
        }
    }
}
