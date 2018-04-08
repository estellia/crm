using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using cPos.Admin.Component;
using cPos.Admin.Model.Customer;
using cPos.Admin.Service.Interfaces;
using cPos.Admin.DataCrypt;
using cPos.Admin.Model.Right;
using System.Threading;
using System.Text;

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

    protected IRightService GetRightService()
    {
        return (IRightService)BusinessServiceProxyLocator.GetService(typeof(IRightService));
    }

    /// <summary>
    /// 获取一级菜单
    /// </summary>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public IList<MenuInfo> getFirstLevelMenus()
    {
        IList<MenuInfo> menus = this.GetRightService().GetFirstLevelMenuListByAppCodeAndUserID(
            AppInfo.CODE_ADMIN_PLATFORM, SessionManager.CurrentLoggingSession.UserID);
        return menus;
    }

    /// <summary>
    /// 获取子菜单
    /// </summary>
    /// <param name="parentMenuID">父菜单ID</param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public IList<MenuInfo> getSubMenus(string parentMenuID)
    {
        IList<MenuInfo> menus = this.GetRightService().GetSubMenuListByUserIDAndMenuID(
            SessionManager.CurrentLoggingSession.UserID, parentMenuID);

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
        IList<MenuInfo> menus = this.GetRightService().GetAllMenuListByAppCode("AP");
        MenuInfo menu = menus.Single(e => e.ID == menuId);
        if (menu == null) return menuId;
        if (menu.Level == 1) return menuId;
        MenuInfo parentMenu = menus.Single(e => e.ID == menu.ParentMenuID);
        if (menu.Level == 2) return parentMenu.ID + "_" + menuId;
        MenuInfo rootMenu = menus.Single(e => e.ID == parentMenu.ParentMenuID);
        return rootMenu.ID + "_" + parentMenu.ID + "_" + menuId;
    }
}
