using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using cPos.Components;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using cPos.WebServices;
using cPos.Model;
using cPos.Model.User;

/// <summary>
///PageBase 的摘要说明
/// </summary>
public class PageBase:System.Web.UI.Page
{
    SessionManager sessionManage = new SessionManager();

	public PageBase()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        this.InfoBox = new InfoBox(this);
        
	}

    #region Properties
    public UserInfo UserInfo
    {
        get { return sessionManage.UserInfo; }
        set { sessionManage.UserInfo = value; }
    }
    public InfoBox InfoBox
    {
        get;
        private set;
    }
    #endregion

    #region Properties
    public UserRoleInfo UserRoleInfo
    {
        get { return sessionManage.UserRoleInfo; }
        set { sessionManage.UserRoleInfo = value; }
    }
    #endregion

    #region LoggingManager
    public LoggingManager LoggingManagerInfo
    {
        get { return sessionManage.LoggingManager; }
        set { sessionManage.LoggingManager = value; } 
    }
    #endregion

    #region LoggingSessionInfo 登录信息类集合
    public LoggingSessionInfo loggingSessionInfo
    {
        //get { return sessionManage.loggingSessionInfo; }
        //set { sessionManage.loggingSessionInfo = value; }
        get { return (LoggingSessionInfo)this.Session["loggingSessionInfo"]; }
        set { this.Session["loggingSessionInfo"] = value; }
    }
    #endregion

    #region Properties
    public string CurrentLanguageKindId
    {
        get { return sessionManage.CurrentLanguageKindId; }
        set { sessionManage.CurrentLanguageKindId = value; }
    }
    #endregion
    public void Redirect(string msg, InfoType type, string go)
    {
        //string url = string.Format("~/InfoBoxPage.aspx?info={0}&type={1}&go={2}"
        //    , System.Web.HttpContext.Current.Server.UrlEncode(msg)
        //    , (int)type
        //    , System.Web.HttpContext.Current.Server.UrlEncode(go));
        //this.Response.Redirect(url);
        string script = @"
            alert('{0}');
            location.href='{1}';
        ";
        this.ClientScript.RegisterClientScriptBlock(typeof(int), "redirect", string.Format(script, msg.Replace("\r", @"\r")
            .Replace("\n", @"\n")
            .Replace("\t", @"\t")
            .Replace("'", @"\'")
            , go), true);
    }
    
}
public class InfoBox
{
    private System.Web.UI.Page _page;
    public InfoBox(System.Web.UI.Page page)
    {
        _page = page;
    }

    public void ShowPopInfo(string msg)
    {
        _page.ClientScript.RegisterStartupScript(this.GetType(), "show_pop_" + DateTime.Now.Ticks,
            string.Format("infobox.showPop('{0}','{1}');", (msg ?? "").Replace("'", @"\'").Replace("\r", @"\r").Replace("\n", @"\n"), "info")
            , true);
    }

    public void ShowPopError(string msg)
    {
        _page.ClientScript.RegisterStartupScript(this.GetType(), "show_pop_" + DateTime.Now.Ticks,
           string.Format("infobox.showPop('{0}','{1}');", (msg ?? "").Replace("'", @"\'").Replace("\r", @"\r").Replace("\n", @"\n"), "error")
           , true);
    }
}

public enum InfoType : int
{
    Info = 1,
    Warning = 2,
    Error = 3,
}

public class PageLog
{
    private static PageLog _log;
    public static PageLog Current { get { if (_log == null) { _log = new PageLog(); } return _log; } }
    public void Write(Exception ex)
    {
        Write(string.Format("{0}", ex));
    }
    public void Write(string content)
    {
        record(string.Format("{0}\t{1}\r\n", DateTime.Now, content));
    }

    private void record(string log)
    {
        try
        {
            var path = "~/PageLog";
            path = System.Web.HttpContext.Current.Server.MapPath(path);
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            path += string.Format("/{0:yyyy-MM-dd}.txt", DateTime.Now);
            lock (this)
            {
                System.IO.File.AppendAllText(path, log);
            }
        }
        catch { }
    }
}