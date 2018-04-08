using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        //Response.Cache.SetNoStore();

        this.Response.Redirect("~/Login/LoginManager.aspx"); 
    } 
}