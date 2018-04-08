using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Admin.Model.Right;
using cPos.Admin.Component;

namespace cPos.Admin.Service.Interfaces
{
    public interface IRightService
    {
        #region 应用系统
        /// <summary>
        /// 获取所有的应用系统列表
        /// </summary>
        /// <returns></returns>
        IList<AppInfo> GetAllAppList();

        /// <summary>
        /// 获取所有的行业版本列表
        /// </summary>
        /// <returns></returns>
        IList<VersionInfo> GetAllVersionList();

        /// <summary>
        /// 获取对于客户可视的应用系统列表
        /// </summary>
        /// <returns></returns>
        IList<AppInfo> GetCustomerVisibleAppList();
        #endregion

        #region 菜单
        /// <summary>
        /// 查询某个客户的菜单列表
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        IList<MenuInfo> GetAllMenuListByCustomer(string customerID);

        /// <summary>
        /// 获取应用系统下的所有菜单列表
        /// </summary>
        /// <param name="appID">应用系统ID</param>
        /// <returns></returns>
        IList<MenuInfo> GetAllMenuListByAppID(string appID);

        /// <summary>
        /// 获取应用系统下的所有菜单列表
        /// </summary>
        /// <param name="appID">应用系统编码</param>
        /// <returns></returns>
        IList<MenuInfo> GetAllMenuListByAppCode(string appCode);

        /// <summary>
        /// 获取应用系统下的第一层菜单列表
        /// </summary>
        /// <param name="appID">应用系统ID</param>
        /// <returns></returns>
        IList<MenuInfo> GetFirstLevelMenuListByAppID(string appID);

        /// <summary>
        /// 获取应用系统下的第一层菜单列表
        /// </summary>
        /// <param name="appID">应用系统ID</param>
        /// <returns></returns>
        IList<MenuInfo> GetFirstLevelMenuListByAppIDAndVersion(string appID, string vocaVerMappingID);

        /// <summary>
        /// 根据行业版本获取菜单列表
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="vocaVerMappingID"></param>
        /// <returns></returns>
        IList<MenuInfo> GetMenuListByAppIDAndVersion(string appID, string vocaVerMappingID);

        /// <summary>
        /// 获取菜单的下一层子菜单
        /// </summary>
        /// <param name="menuID">菜单ID</param>
        /// <returns></returns>
        IList<MenuInfo> GetSubMenuListByMenuID(string menuID);

        /// <summary>
        /// 获取菜单的下一层子菜单
        /// </summary>
        /// <param name="menuID">菜单ID</param>
        /// <returns></returns>
        IList<MenuInfo> GetSubMenuListByAppVersionAndMenuID(string menuID, string vocaVerMappingID, string appID);

        /// <summary>
        /// 查询某个用户在某个应用系统下的可见菜单列表
        /// </summary>
        /// <param name="appCode">应用系统编码</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        IList<MenuInfo> GetMenuListByAppCodeAndUserID(string appCode, string userID);

        /// <summary>
        /// 查询某个用户在某个应用系统下的第一级可见菜单列表
        /// </summary>
        /// <param name="appCode">应用系统编码</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        IList<MenuInfo> GetFirstLevelMenuListByAppCodeAndUserID(string appCode, string userID);

        /// <summary>
        /// 查询某个用户可见的某个菜单的下级子菜单列表
        /// </summary>
        /// <param name="appCode"></param>
        /// <param name="userID"></param>
        /// <param name="menuID"></param>
        /// <returns></returns>
        IList<MenuInfo> GetSubMenuListByUserIDAndMenuID(string userID, string menuID);

        /// <summary>
        /// 检验菜单的编码是否已经存在
        /// </summary>
        /// <param name="menuID">如果是校验一个已经存在的菜单,则传入该菜单的ID,否则为空</param>
        /// <param name="menuCode">菜单的编码</param>
        /// <returns>如果已经存在,返回true,否则,返回false</returns>
        bool ExistMenuCode(string menuID, string menuCode);

        /// <summary>
        /// 添加一个菜单
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="menu">菜单</param>
        void InsertMenu(LoggingSessionInfo loggingSession, MenuInfo menu);

        /// <summary>
        /// 修改一个菜单
        /// </summary>
        /// <param name="loggingSession">当前用户的登录信息</param>
        /// <param name="menu">菜单</param>
        void ModifyMenu(LoggingSessionInfo loggingSession, MenuInfo menu);

        /// <summary>
        /// 根据菜单ID获取菜单信息
        /// </summary>
        /// <param name="menuID">菜单ID</param>
        /// <returns></returns>
        MenuInfo GetMenuByID(string menuID);

        /// <summary>
        /// 判断能否停用一个菜单
        /// </summary>
        /// <param name="menuID">菜单ID</param>
        bool CanDisableMenu(string menuID);

        /// <summary>
        /// 设置行业版本菜单状态
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="vocaVerMappingID"></param>
        /// <param name="appID"></param>
        /// <param name="menuID"></param>
        /// <param name="status"></param>
        string SetVersionMenuStatus(LoggingSessionInfo loggingSessionInfo, string menuID, string vocaVerMappingID, string appID, string status);

        /// <summary>
        /// 同步行业版本菜单
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="vocaVerMappingID"></param>
        /// <param name="appID"></param>
        /// <param name="menuID"></param>
        /// <param name="status"></param>
        string SyncCustomerVersionMenu(cPos.Model.LoggingSessionInfo loggingSessionInfo, string vocaVerMappingID, string appID);
        /// <summary>
        /// 根据ID获取行业版本对应的菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MenuInfo GetVocationVersionMenuMappingByID(Guid id);
        /// <summary>
        /// 设菜单操作权限
        /// </summary>
        /// <param name="isCanAccess">0=不可操作；1=可操作</param>
        /// <param name="id">表主键</param>
        void UpdateIsCanAccess(int isCanAccess, Guid id);

        #endregion
    }
}
