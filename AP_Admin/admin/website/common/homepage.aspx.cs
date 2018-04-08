using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cPos.Admin.Component;
using cPos.Admin.Model.Customer;
using cPos.Admin.Service.Interfaces;
using cPos.Admin.DataCrypt;
using cPos.Admin.Model.Right;
using System.Threading;
using System.Text;
public partial class common_homepage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        if (SessionManager.CurrentLoggingSession == null)
        {
            this.Response.Redirect("~/login/login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                this.lblUsername.Text =SessionManager.CurrentLoggingSession.UserName;
                LoadMenuData();
            }
        }
    }

    protected IRightService GetRightService()
    {
        return (IRightService)BusinessServiceProxyLocator.GetService(typeof(IRightService));
    }

    protected void lbtnModifyPassword_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/user/modify_user_pwd.aspx");
    }
    private void LoadMenuData()
    {
        try
        { 
            var sysService = GetRightService();
            var menus = sysService.GetMenuListByAppCodeAndUserID(AppInfo.CODE_ADMIN_PLATFORM, SessionManager.CurrentLoggingSession.UserID).Where (obj=>obj.Status == 1).OrderBy(obj=>obj.DisplayIndex).ToArray ();
            var rult = menus.Where (obj=>obj.Level==1).Select(obj => new
            {
                Item = obj, 
                SubItems = menus.Where (obj1=>obj1.ParentMenuID == obj.ID).Select(obj1 => new
                {
                    Item = obj1,
                    SubItems = menus.Where(obj2 => obj2.ParentMenuID == obj1.ID).Select(obj2 => new
                    {
                        Item = obj2,
                        SubItems = new MenuInfo[0]
                    }).ToArray()
                }).ToArray()
            }).ToArray();
            hid_menu_data.Value = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(rult);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
        }
    }
    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        SessionManager.CurrentLoggingSession = null;

        //清除session
        this.Session.Abandon();

        this.Response.Redirect("~/login/login.aspx");
    }
}
