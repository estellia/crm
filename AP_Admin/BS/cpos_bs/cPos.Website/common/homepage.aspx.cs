using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cPos.Components;
using cPos.Service;
using cPos.Model;

public partial class common_homepage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SessionManager sm = new SessionManager();
        if (sm.loggingSessionInfo == null)
        {
            //this.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["sso_url"].ToString());
            this.Response.Redirect("../GoSso.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                this.lblUsername.Text = string.Format("{0}，角色：{1}，登录单位：{2}",
                    sm.LoggingManager.User_Name, sm.UserRoleInfo.RoleName, sm.UserRoleInfo.UnitName);
                this.lblCurrentCustomer.Text = sm.LoggingManager.Customer_Name;
                LoadMenuData();
            }
        }
    }

    private void LoadMenuData()
    {
        try
        {
            var sysService = new cAppSysServices();

            var menus = sysService.GetAllMenusByAppAndUser(new SessionManager().loggingSessionInfo, "CP").Where(obj => obj.Status == 1).OrderBy(obj => obj.Display_Index).ToArray();
            var rult = menus.Where(obj => obj.Menu_Level == 1).Select(obj => new
            {
                Item = obj,
                SubItems = menus.Where(obj1 => obj1.Parent_Menu_Id == obj.Menu_Id).Select(obj1 => new
                {
                    Item = obj1,
                    SubItems = menus.Where(obj2 => obj2.Parent_Menu_Id == obj1.Menu_Id).Select(obj2 => new
                    {
                        Item = obj2,
                        SubItems = new MenuModel[0]
                    }).ToArray()
                }).ToArray()
            }).ToArray();

            //var menus = sysService.GetMainMenusByRoleAndApp(
            //    new SessionManager().loggingSessionInfo, "CP").Where(obj => obj.Status == 1).OrderBy(obj => obj.Display_Index);

            //var rult = menus.Select(obj => new
            //{
            //    Item = obj,
            //    SubItems = sysService.GetSubMenusByRole(new SessionManager().loggingSessionInfo, new SessionManager().loggingSessionInfo.CurrentUserRole.RoleId, obj.Menu_Id).Where(obj1 => obj1.Status == 1).OrderBy(obj1 => obj1.Display_Index).Select(obj1 => new
            //    {
            //        Item = obj1,
            //        SubItems = sysService.GetSubMenusByRole(new SessionManager().loggingSessionInfo, new SessionManager().loggingSessionInfo.CurrentUserRole.RoleId, obj1.Menu_Id).Where(obj2 => obj2.Status == 1).OrderBy(obj2 => obj2.Display_Index).Select(obj2 => new
            //        {
            //            Item = obj2,
            //            SubItems = new MenuModel[0]
            //        }).ToArray()
            //    }).ToArray()
            //}).ToArray();


            hid_menu_data.Value = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(rult);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
        }
    }


    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        new SessionManager().LoggingManager = null;

        //清除session
        this.Session.Abandon();

        this.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["sso_url"].ToString());
    }
}
