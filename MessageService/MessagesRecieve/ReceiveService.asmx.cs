using System;
using System.Web.Services;
using JIT.Utility;
using JIT.Utility.Log;
using JIT.Utility.SMS.DataAccess;
using JIT.Utility.SMS.Entity;

namespace MessagesReceive
{
    /// <summary>
    /// 短信接收
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ReceiveService : WebService
    {

        [WebMethod]
        public string Recieve(string phoneNO, string message, string sign)
        {
            try
            {
                var dao = new SMSSendDAO(new BasicUserInfo());
                dao.Create(new SMSSendEntity
                {
                    MobileNO = phoneNO,
                    SMSContent = message,
                    Status = 0,//未发送
                    Sign = sign,
                    SendTime = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                Loggers.Exception( new BasicUserInfo(), ex);
                return "F";
            }
            return "T";
        }
    }
}
