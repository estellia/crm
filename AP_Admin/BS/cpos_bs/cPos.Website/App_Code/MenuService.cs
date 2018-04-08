using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using cPos.Components;
using cPos.Model;
using System.Threading;
using System.Text;
using cPos.Service;

/// <summary>
/// Summary description for MenuService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class MenuService : System.Web.Services.WebService
{
    public MenuService()
    {

    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    /// <summary>
    /// 获取一级菜单
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public IList<MenuModel> getFirstLevelMenus()
    {
        IList<MenuModel> menus =  new cAppSysServices().GetMainMenusByRoleAndApp(
            new SessionManager().loggingSessionInfo, "CP");
        return menus;
    }

    /// <summary>
    /// 获取子菜单
    /// </summary>
    /// <param name="parentMenuID"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public IList<MenuModel> getSubMenus(string parentMenuID)
    {
        IList<MenuModel> menus = new cAppSysServices().GetSubMenusByRole(
            new SessionManager().loggingSessionInfo,
            new SessionManager().loggingSessionInfo.CurrentUserRole.RoleId, parentMenuID);

        return menus;
    }

    /// <summary>
    /// 获取菜单路径
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string getMenuFullPath(string menuId)
    {
        if (menuId == null || menuId.Trim().Length == 0) return string.Empty;
        IList<MenuModel> menus = new cAppSysServices().GetAllMenusByAppSysCode("CP");
        MenuModel menu = menus.Single(e => e.Menu_Id == menuId);
        if (menu == null) return menuId;
        if (menu.Menu_Level == 1) return menuId;
        MenuModel parentMenu = menus.Single(e => e.Menu_Id == menu.Parent_Menu_Id);
        if (menu.Menu_Level == 2) return parentMenu.Menu_Id + "_" + menuId;
        MenuModel rootMenu = menus.Single(e => e.Menu_Id == parentMenu.Parent_Menu_Id);
        return rootMenu.Menu_Id + "_" + parentMenu.Menu_Id + "_" + menuId;
    }
}
