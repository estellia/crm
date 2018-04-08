using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cPos.Components;
using cPos.Service;
using cPos.Model;

public partial class Common_SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (new SessionManager().loggingSessionInfo == null)
        {
            //this.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["sso_url"].ToString());
            this.Response.Redirect("../GoSso.aspx");
        } 
    }
}