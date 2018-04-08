<%@ WebHandler Language="C#" Class="query" %>

using System;
using System.Web;
using System.Linq;
using System.Web.SessionState;
public class query : IHttpHandler, IRequiresSessionState
{
    cPos.Admin.Component.SessionManager sessionManage = new cPos.Admin.Component.SessionManager();
    public void ProcessRequest (HttpContext context) {
        object rult = new {Success=true,Msg="OK",Rult=(string)null };
        try
        {
            string action = context.Request.Params["action"] ?? "none";
            switch (action.ToLower())
            {
                case "searchskuinfo":
                    rult = SearchSkuInfo(context.Request.Params);
                    break;
                default:
                    rult = new { Success = false, Msg = "未知的请求 action" };
                    break;
            }
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            rult = new { Success = false, Msg = ex.Message};
        }
        
        context.Response.ContentType = "text/json";
        context.Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(rult));
    }

    public object SearchSkuInfo(System.Collections.Specialized.NameValueCollection nvc)
    {
        var skuInfolist = new cPos.Admin.Service.SkuService().GetSkuInfoByLike(loggingSessionInfo, nvc["keyword"]);
        var list = skuInfolist.Take(10).ToArray();
        return new { Success = true , Msg = "OK",Rult=list}; 
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    #region LoggingSessionInfo 登录信息类集合
    public cPos.Model.LoggingSessionInfo loggingSessionInfo
    {
        get { return (cPos.Model.LoggingSessionInfo)HttpContext.Current.Session["loggingSessionInfo"]; }
    }
    #endregion

}