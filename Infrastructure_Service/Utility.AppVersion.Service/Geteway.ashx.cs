using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.Utility.Log;
using JIT.Utility.AppVersion.Framework;
using JIT.Utility.AppVersion.Service.Base;

namespace JIT.Utility.AppVersion.Service
{
    /// <summary>
    /// Geteway 的摘要说明
    /// </summary>
    public class Geteway : BaseHandler
    {
        protected override AppMgrResponse ProcessAction(string pAction, AppMgrRequest pRequest)
        {
            AppMgrResponse response = new AppMgrResponse() { ResultCode = 0, Message = "操作成功" };
            try
            {
                switch (pAction)
                {
                    case "CheckAppVersion":
                        response.Data = API.ServiceAPI.CheckAppVersion(pRequest);
                        break;
                    default:
                        throw new Exception("错误的接口:" + pAction);
                }
            }
            catch (Exception ex)
            {
                response.ResultCode = 100;
                response.Message = ex.Message;
                Loggers.Exception(new ExceptionLogInfo(ex));
            }
            return response;
        }
    }
}