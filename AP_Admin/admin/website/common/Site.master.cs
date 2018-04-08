using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cPos.Admin.Component;
using cPos.Admin.Service;
using cPos.Admin.Model;

public partial class Common_SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.CurrentLoggingSession == null)
        {
            this.Response.Redirect("~/login/login.aspx");
        }
    }
}
