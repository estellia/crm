using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using cPos.Admin.Component;
using cPos.Admin.Service.Interfaces;

/// <summary>
/// Summary description for PageBase
/// </summary>
public class PageBase : System.Web.UI.Page
{
	public PageBase()
	{
        this.InfoBox = new InfoBox(this);
	}

    public InfoBox InfoBox { get; private set; }

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

    protected override void OnLoad(EventArgs e)
    {
        if (this.Session[cPos.Admin.Component.SessionManager.KEY_LOGGING_SESSION] == null)
        {
            this.Response.Redirect("~/login/login.aspx");
        }

        base.OnLoad(e);
    }

    protected IUserService GetUserService()
    {
        return (IUserService)BusinessServiceProxyLocator.GetService(typeof(IUserService));
    }

    protected ICustomerService GetCustomerService()
    {
        return (ICustomerService)BusinessServiceProxyLocator.GetService(typeof(ICustomerService));
    }


    protected IDexLogService GetDexLogService()
    {
        return (IDexLogService)BusinessServiceProxyLocator.GetService(typeof(IDexLogService));
    }


    protected IRightService GetRightService()
    {
        return (IRightService)BusinessServiceProxyLocator.GetService(typeof(IRightService));
    }

    protected IOrderService GetOrderService()
    {
        return (IOrderService)BusinessServiceProxyLocator.GetService(typeof(IOrderService));
    }
    protected IUnitService GetUnitService()
    {
        return (IUnitService)BusinessServiceProxyLocator.GetService(typeof(IUnitService));
    }

    protected LoggingSessionInfo LoggingSession
    {
        get 
        {
            return SessionManager.CurrentLoggingSession;
        }
    }

    #region LoggingSessionInfo 登录信息类集合
    public cPos.Model.LoggingSessionInfo loggingSessionInfo
    {
        get { return (cPos.Model.LoggingSessionInfo)this.Session["loggingSessionInfo"]; }
        set { this.Session["loggingSessionInfo"] = value; }
    }
    #endregion
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
        _page.ClientScript.RegisterStartupScript(this.GetType(),"show_pop_" + DateTime.Now.Ticks ,
            string.Format("infobox.showPop('{0}','{1}');", (msg??"").Replace("'",@"\'").Replace("\r",@"\r").Replace("\n",@"\n"), "info")
            ,true );
    }

    public void ShowPopError(string msg)
    {
        _page.ClientScript.RegisterStartupScript(this.GetType(), "show_pop_" + DateTime.Now.Ticks,
           string.Format("infobox.showPop('{0}','{1}');", (msg ?? "").Replace("'", @"\'").Replace("\r", @"\r").Replace("\n", @"\n"), "error")
           , true);
    }
    public void ShowLoading(string msg)
    {
        _page.ClientScript.RegisterStartupScript(this.GetType(),
                  "", "$(document).ready(function(){ fnShowLoading('" + msg + "'); });",
                  true);
    }
    public void HideLoading(string msg)
    {
        _page.ClientScript.RegisterStartupScript(this.GetType(),
                  "", "$(document).ready(function(){ fnHideLoading('" + msg + "'); });",
                  true);
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