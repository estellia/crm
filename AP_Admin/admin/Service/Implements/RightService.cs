using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Admin.Model.Right;
using cPos.Admin.Component;
using cPos.Admin.Component.SqlMappers;

namespace cPos.Admin.Service.Implements
{

    public class RightService : BaseService, Interfaces.IRightService
    {
        #region 应用系统

        public IList<AppInfo> GetAllAppList()
        {
            return MSSqlMapper.Instance().QueryForList<AppInfo>("Right.App.GetAllAppList", null);
        }

        public IList<AppInfo> GetCustomerVisibleAppList()
        {
            return MSSqlMapper.Instance().QueryForList<AppInfo>("Right.App.GetCustomerVisibleAppList", null);
        }
        #endregion

        #region 行业版本

        public IList<VersionInfo> GetAllVersionList()
        {
            return MSSqlMapper.Instance().QueryForList<VersionInfo>("Right.App.GetAllVersionList", null);
        }

        //public IList<AppInfo> GetCustomerVisibleAppList()
        //{
        //    return MSSqlMapper.Instance().QueryForList<AppInfo>("Right.App.GetCustomerVisibleAppList", null);
        //}
        #endregion

        #region 菜单

        public IList<MenuInfo> GetAllMenuListByCustomer(string customerID)
        {
            return MSSqlMapper.Instance().QueryForList<MenuInfo>("Right.Menu.GetAllMenuListByCustomer", customerID);
        }

        public IList<MenuInfo> GetAllMenuListByAppID(string appID)
        {
            return MSSqlMapper.Instance().QueryForList<MenuInfo>("Right.Menu.GetAllMenuListByAppID", appID);
        }

        public IList<MenuInfo> GetAllMenuListByAppCode(string appID)
        {
            return MSSqlMapper.Instance().QueryForList<MenuInfo>("Right.Menu.GetAllMenuListByAppCode", appID);
        }

        public IList<MenuInfo> GetFirstLevelMenuListByAppID(string appID)
        {
            return MSSqlMapper.Instance().QueryForList<MenuInfo>("Right.Menu.GetFirstLevelMenuListByAppID", appID);
        }

        /// <summary>
        /// 根据行业版本和AppID获取菜单列表
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="vocaVerMappingID"></param>
        /// <returns></returns>
        public IList<MenuInfo> GetFirstLevelMenuListByAppIDAndVersion(string appID, string vocaVerMappingID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("AppID", appID);
            ht.Add("VocaVerMappingID", vocaVerMappingID);
            return MSSqlMapper.Instance().QueryForList<MenuInfo>("Right.Menu.GetFirstLevelMenuListByAppIDAndVersion", ht);
        }

        /// <summary>
        /// 根据行业版本和AppID获取菜单列表
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="vocaVerMappingID"></param>
        /// <returns></returns>
        public IList<MenuInfo> GetMenuListByAppIDAndVersion(string appID, string vocaVerMappingID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("AppID", appID);
            ht.Add("VocaVerMappingID", vocaVerMappingID);
            return MSSqlMapper.Instance().QueryForList<MenuInfo>("Right.Menu.GetMenuListByAppIDAndVersion", ht);
        }

        public IList<MenuInfo> GetSubMenuListByMenuID(string menuID)
        {
            return MSSqlMapper.Instance().QueryForList<MenuInfo>("Right.Menu.GetSubMenuListByMenuID", menuID);
        }

        /// <summary>
        /// 根据行业版本和上级菜单获取子菜单列表
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public IList<MenuInfo> GetSubMenuListByAppVersionAndMenuID(string menuID, string vocaVerMappingID, string appID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("MenuID", menuID);
            ht.Add("VocaVerMappingID", vocaVerMappingID);
            ht.Add("AppID", appID);
            return MSSqlMapper.Instance().QueryForList<MenuInfo>("Right.Menu.GetSubMenuListByAppVersionMenuID", ht);
        }

        public IList<MenuInfo> GetMenuListByAppCodeAndUserID(string appCode, string userID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("AppCode", appCode);
            ht.Add("UserID", userID);
            return MSSqlMapper.Instance().QueryForList<MenuInfo>("Right.Menu.GetMenuListByAppCodeAndUserID", ht);
        }

        public IList<MenuInfo> GetFirstLevelMenuListByAppCodeAndUserID(string appCode, string userID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("AppCode", appCode);
            ht.Add("UserID", userID);
            return MSSqlMapper.Instance().QueryForList<MenuInfo>("Right.Menu.GetFirstLevelMenuListByAppCodeAndUserID", ht);
        }

        public IList<MenuInfo> GetSubMenuListByUserIDAndMenuID(string userID, string menuID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("UserID", userID);
            ht.Add("MenuID", menuID);
            return MSSqlMapper.Instance().QueryForList<MenuInfo>("Right.Menu.GetSubMenuListByUserIDAndMenuID", ht);
        }

        public bool ExistMenuCode(string menuID, string menuCode)
        {
            Hashtable ht = new Hashtable();
            if (!string.IsNullOrEmpty(menuID))
                ht.Add("MenuID", menuID);
            ht.Add("MenuCode", menuCode);
            int ret = MSSqlMapper.Instance().QueryForObject<int>("Right.Menu.ExistMenuCode", ht);
            return ret > 0;
        }

        public void InsertMenu(LoggingSessionInfo loggingSession, MenuInfo menu)
        {
            menu.ID = this.NewGUID();
            menu.Creater.ID = loggingSession.UserID;
            menu.Creater.Name = loggingSession.UserName;
            try
            {
                MSSqlMapper.Instance().BeginTransaction();

                //添加菜单
                if (string.IsNullOrEmpty(menu.ParentMenuID))
                    MSSqlMapper.Instance().Insert("Right.Menu.InsertRootMenu", menu);
                else
                    MSSqlMapper.Instance().Insert("Right.Menu.InsertSubMenu", menu);
                //插入操作
                this.InsertBillActionLogWithoutFlow(loggingSession, Model.Bill.BillKindInfo.CODE_MENU,
                    menu.ID, Model.Bill.BillActionFlagType.Create, 1, null);
                //如果菜单所属的应用系统是管理平台,则自动插入管理平台下的系统管理员角色对应的菜单
                RoleMenuInfo role_menu = new RoleMenuInfo();
                role_menu.ID = NewGUID();
                role_menu.Menu = menu;
                role_menu.Role.Code = RoleInfo.CODE_ADMIN;
                MSSqlMapper.Instance().Insert("Right.RoleMenu.InsertForAdminRoleOfAdminPlatform", role_menu);

                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
        }

        public void ModifyMenu(LoggingSessionInfo loggingSession, MenuInfo menu)
        {
            menu.LastEditor.ID = loggingSession.UserID;
            menu.LastEditor.Name = loggingSession.UserName;
            try
            {
                MSSqlMapper.Instance().BeginTransaction();

                //修改菜单
                MSSqlMapper.Instance().Update("Right.Menu.Update", menu);
                //插入操作
                this.InsertBillActionLogWithoutFlow(loggingSession, Model.Bill.BillKindInfo.CODE_MENU,
                    menu.ID, Model.Bill.BillActionFlagType.Modify, 1, null);

                MSSqlMapper.Instance().CommitTransaction();
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
        }

        public MenuInfo GetMenuByID(string menuID)
        {
            return MSSqlMapper.Instance().QueryForObject<MenuInfo>("Right.Menu.GetMenuByID", menuID);
        }

        public bool CanDisableMenu(string menuID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("MenuID", menuID);
            ht.Add("MenuStatus", 1);
            int ret = MSSqlMapper.Instance().QueryForObject<int>("Right.Menu.CountSubMenus", ht);
            return ret == 0;
        }

        /// <summary>
        /// 设置行业版本菜单状态
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="status">修改值</param>
        /// <returns></returns>
        public string SetVersionMenuStatus(LoggingSessionInfo loggingSessionInfo, string menuID, string vocaVerMappingID, string appID, string status)
        {
            string strResult = string.Empty;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("MenuID", menuID);
                ht.Add("Status", status);
                ht.Add("CreateBy", loggingSessionInfo.UserID);
                ht.Add("VocaVerMappingID", vocaVerMappingID);
                ht.Add("AppID", appID);

                //查询是否存在
                var versionMenu = MSSqlMapper.Instance().QueryForObject<Guid>("Right.Menu.SelectVersionMenu", ht);
                if (versionMenu == new Guid("00000000-0000-0000-0000-000000000000"))
                {
                    //插入
                    MSSqlMapper.Instance().Insert("Right.Menu.InsertVersionMenu", ht);
                }
                else
                {
                    //提交
                    MSSqlMapper.Instance().Update("Right.Menu.UpdateVersionStatus", ht);
                }
                strResult = "状态修改成功.";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return strResult;
        }

        /// <summary>
        /// 同步行业版本菜单
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="status">修改值</param>
        /// <returns></returns>
        public string SyncCustomerVersionMenu(cPos.Model.LoggingSessionInfo loggingSessionInfo, string vocaVerMappingID, string appID)
        {
            string strResult = string.Empty;
            var sync_customermenu_startdate = System.Configuration.ConfigurationManager.AppSettings["sync_customermenu_startdate"];
            try
            {
                //
                var tmpLoggingSessionInfo = loggingSessionInfo;


                //tmpLoggingSessionInfo.CurrentLoggingManager.Connection_String
                var service = new CustomerService();
                var customerList = service.SelectCustomerByVersionID(vocaVerMappingID);
                var versionMenus = GetMenuListByAppIDAndVersion(appID, vocaVerMappingID).OrderBy(obj => obj.DisplayIndex).ToArray();

                foreach (var customerinfo in customerList)
                {

                    if (!string.IsNullOrEmpty(sync_customermenu_startdate) && Convert.ToDateTime(sync_customermenu_startdate) > customerinfo.CreateTime)
                    {
                        continue;
                    }
                    //if (customerinfo.ID != "eb17cc2569c74ab7888b1f403972d37d")//测试
                    //{
                    //    continue;
                    //}
                    var tmpSession = this.GetLoggingSessionInfoByCustomerId(customerinfo.ID);
                    if (tmpSession == null)
                    {
                        strResult += "商户：" + customerinfo.ID + "数据源无效；\n";
                        continue;
                    }

                    var ht_select = new Hashtable();
                    ht_select.Add("CustomerID", customerinfo.ID);
                    ht_select.Add("AppID", appID);

                    var tran = MSSqlMapper.Instance(tmpSession.CurrentLoggingManager);//访问商户库
                    var customer_menulist = tran.QueryForList<MenuInfo>("Right.Menu.GetMenuListByAppIDAndCustomerID", ht_select);//

                    try
                    {
                        tran.BeginTransaction();
                        foreach (var menuitem in versionMenus)
                        {
                            var tempmenu = customer_menulist.Where(t => t.ApplicationID == menuitem.ApplicationID && t.Code == menuitem.Code && t.CustomerID == customerinfo.ID && t.Status == 1).ToArray();

                            //test
                            //var test = customer_menulist.Where(t => t.ID == "7d021667cb7544c2b059c1c86c47cb65e9f21f7e022d4c72baf64c668b709f0e" && t.CustomerID == "e9f21f7e022d4c72baf64c668b709f0e").ToArray();
                            //if (test.Length >0)
                            //{
                            //    //continue;
                            //}

                            var _menu_id = menuitem.ID + customerinfo.ID;
                            var _parent_menu_id = menuitem.ParentMenuID != "--" ? menuitem.ParentMenuID + customerinfo.ID : menuitem.ParentMenuID;

                            Hashtable ht_menu = new Hashtable();
                            ht_menu.Add("menu_id", _menu_id);
                            ht_menu.Add("reg_app_id", menuitem.ApplicationID);
                            ht_menu.Add("menu_code", menuitem.Code);
                            ht_menu.Add("parent_menu_id", _parent_menu_id);
                            ht_menu.Add("menu_level", menuitem.Level);
                            ht_menu.Add("url_path", menuitem.URLPath);
                            ht_menu.Add("icon_path", menuitem.IconPath);
                            ht_menu.Add("display_index", menuitem.DisplayIndex);
                            ht_menu.Add("menu_name", menuitem.Name);
                            ht_menu.Add("user_flag", menuitem.CustomerVisible);
                            ht_menu.Add("menu_eng_name", menuitem.EnglishName);
                            ht_menu.Add("IsCanAccess", menuitem.IsCanAccess);//菜单是否可操作
                            ht_menu.Add("status", menuitem.Status);
                            ht_menu.Add("create_user_id", loggingSessionInfo.CurrentLoggingManager.User_Id);
                            ht_menu.Add("create_time", GetDateTime(DateTime.Now));
                            ht_menu.Add("modify_user_id", loggingSessionInfo.CurrentLoggingManager.User_Id);
                            ht_menu.Add("modify_time", GetDateTime(DateTime.Now));
                            ht_menu.Add("customer_id", customerinfo.ID);

                            //插入
                            if (tempmenu.Length == 0)
                            {
                                var tempmenuinfo = customer_menulist.Where(t => t.ID == menuitem.ID + customerinfo.ID).ToArray().FirstOrDefault();
                                //根据菜单ID更新数据
                                if (tempmenuinfo != null)
                                {
                                    if (menuitem.Code != tempmenuinfo.Code || _parent_menu_id != tempmenuinfo.ParentMenuID || menuitem.ApplicationID != tempmenuinfo.ApplicationID
                                        || menuitem.Name != tempmenuinfo.Name || menuitem.URLPath != tempmenuinfo.URLPath || menuitem.Status != tempmenuinfo.Status || menuitem.IsCanAccess != tempmenuinfo.IsCanAccess)
                                    {
                                        tran.Update("Right.Menu.UpdateCustomerMenuByMenuID", ht_menu);
                                    }
                                }
                                else
                                {
                                    tran.Insert("Right.Menu.InsertCustomerMenu", ht_menu);
                                }
                            }
                            else
                            {
                                //更新
                                if (tempmenu.Length > 1)
                                {
                                    MSSqlMapper.Instance(new cPos.Model.LoggingManager());
                                    return customerinfo.Name + "(" + customerinfo.ID + ")，找到" + tempmenu.Length + "条编码为(" + menuitem.Code + ")的菜单记录，请联系管理员修正后再进行同步。\n";
                                }

                                //相同数据不用更新
                                var tempmenuinfo = tempmenu.ToArray().FirstOrDefault();
                                if (menuitem.Code != tempmenuinfo.Code || _parent_menu_id != tempmenuinfo.ParentMenuID || menuitem.ApplicationID != tempmenuinfo.ApplicationID
                                        || menuitem.Name != tempmenuinfo.Name || menuitem.URLPath != tempmenuinfo.URLPath || menuitem.Status != tempmenuinfo.Status || menuitem.IsCanAccess != tempmenuinfo.IsCanAccess)
                                {
                                    tran.Update("Right.Menu.UpdateCustomerMenu", ht_menu);
                                }

                            }

                            //禁用其他菜单


                        }

                        tran.CommitTransaction();

                    }
                    catch (Exception ex)
                    {
                        tran.RollBackTransaction();
                        MSSqlMapper.Instance(new cPos.Model.LoggingManager());
                        throw ex;
                    }

                    MSSqlMapper.Instance(new cPos.Model.LoggingManager());

                }

                //MSSqlMapper.Instance(new cPos.Model.LoggingManager());//重新初始化为 AP 库

                strResult += "菜单更新成功.";
            }
            catch (Exception ex)
            {
                MSSqlMapper.Instance(new cPos.Model.LoggingManager());
                return ex.InnerException.Message;
                throw (ex);
            }
            return strResult;
        }

        /// <summary>
        /// 根据ID获取行业版本对应的菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MenuInfo GetVocationVersionMenuMappingByID(Guid id)
        {
            return MSSqlMapper.Instance().QueryForObject<MenuInfo>("Right.Menu.GetVocationVersionMenuMappingByID", id);
        }
        /// <summary>
        /// 设菜单操作权限
        /// </summary>
        /// <param name="isCanAccess">0=不可操作；1=可操作</param>
        /// <param name="id">表主键</param>
        public void UpdateIsCanAccess(int isCanAccess, Guid id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("IsCanAccess", isCanAccess);
            ht.Add("ID", id);
            MSSqlMapper.Instance().Update("Right.Menu.UpdateIsCanAccess", ht);
        }
        #endregion
    }
}
