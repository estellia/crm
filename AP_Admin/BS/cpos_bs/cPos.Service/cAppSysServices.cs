using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Components.SqlMappers;

namespace cPos.Service
{
    /// <summary>
    /// 应用系统
    /// </summary>
    public class cAppSysServices : BaseService
    {
        /// <summary>
        /// 设置应用系统
        /// </summary>
        /// <param name="strXML"></param>
        /// <param name="Customer_Id"></param>
        /// <returns></returns>
        public bool SetAppSysInfo(string strXML, string Customer_Id)
        {
            bool bReturn = true;
            try
            {

                //反序列化
                IList<AppSysModel> appSysInfoList = (IList<cPos.Model.AppSysModel>)cXMLService.Deserialize(strXML, typeof(List<cPos.Model.AppSysModel>));
                //转成hash
                var args = new Hashtable
                       {
                           {"AppSysModels", appSysInfoList}
                       };
                //获取连接数据库信息
                LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(Customer_Id);
     
                //提交
                cSqlMapper.Instance(loggingManager).Update("AppSys.InsertAppSys", args);
            }
            catch (Exception ex)
            {
                bReturn = false;
                throw (ex);
            }
            return bReturn;
        }

        #region 获取所有的应用系统列表
        /// <summary>
        /// 获取所有的应用系统列表
        /// </summary>
        /// <param name="loggingSessionInfo">当前登录用户的Session信息</param>
        /// <returns></returns>
        public IList<AppSysModel> GetAllAppSyses(LoggingSessionInfo loggingSessionInfo)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSessionInfo.CurrentLanguageKindId);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<AppSysModel>("AppSys.SelectAllAppSys", hashTable);
        }

        #endregion


        #region 获取某个应用系统下的所有角色
        /// <summary>
        /// 获取某个应用系统下的所有角色
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="appSysId">应用系统Id</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns></returns>
        public RoleModel GetRolesByAppSysId(LoggingSessionInfo loggingSession, string appSysId, int maxRowCount, int startRowIndex)
        {
            RoleModel roleInfo = new RoleModel();
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("ApplicationId", appSysId);
            hashTable.Add("StartRow", startRowIndex);
            hashTable.Add("EndRow", startRowIndex + maxRowCount);
            hashTable.Add("MaxRowCount", maxRowCount);
            hashTable.Add("CustomerId", loggingSession.CurrentLoggingManager.Customer_Id);
            int iCount = cSqlMapper.Instance().QueryForObject<int>("Role.SelectByApplicationIdCount", hashTable);
            IList<RoleModel> roleInfoList = new List<RoleModel>();
            roleInfoList= cSqlMapper.Instance().QueryForList<RoleModel>("Role.SelectByApplicationId", hashTable);
            roleInfo.ICount = iCount;
            roleInfo.RoleInfoList = roleInfoList;
            return roleInfo;
        }

        #endregion

        #region 删除角色
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="loggingSession"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public string DeleteRoleById(LoggingSessionInfo loggingSession, string roleId)
        {
            string strResult = string.Empty;
            RoleModel role = new AppSysService().GetRoleById(loggingSession, roleId);
            if (role == null)
            {
                return strResult = "角色不存在";
            }
            //不能删除系统保留的角色
            if (role.Is_Sys == 1)
            {
                return strResult = "不能删除系统保留的角色";
            }

            //如果已经给角色设置了用户,就不能再删除角色了
            int count = cSqlMapper.Instance().QueryForObject<int>("UserRole.CountUserByRoleId", roleId);
            if (count > 0)
            {
                return strResult = "给角色设置了用户,就不能再删除角色了";
            }
            //如果Bill的操作中引用了角色,也不能再删除角色了
            count = cSqlMapper.Instance().QueryForObject<int>("BillActionRole.CountByRoleId", roleId);
            if (count > 0)
            {
                return strResult = "表单的操作中引用了角色,也不能再删除角色了";
            }

            cSqlMapper.Instance().BeginTransaction();
            try
            {
                //删除角色
                cSqlMapper.Instance().Update("Role.DeleteById", roleId);
              
                //删除角色的菜单
                cSqlMapper.Instance().Update("RoleMenu.DeleteByRoleId", roleId);
                cSqlMapper.Instance().CommitTransaction();
                return strResult = "删除成功";
            }
            catch (Exception ex)
            {
                cSqlMapper.Instance().RollBackTransaction();
                throw ex;
            }
        }
        #endregion

        #region 菜单
        /// <summary>
        /// 获取某个应用系统下的所有菜单
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="appCode">应用系统编码</param>
        /// <returns></returns>
        public IList<MenuModel> GetAllMenusByAppSysCode(LoggingSessionInfo loggingSessionInfo,string appCode)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("ApplicationCode", appCode);

            hashTable.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<MenuModel>("Menu.GetAllMenusByAppSysId", hashTable);
        }



        /// <summary>
        /// 获取某个用户在某个应用系统下的所有菜单
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="appCode">应用系统编码</param>
        /// <param name="appCode">用户ID</param>
        /// <returns></returns>
        public IList<MenuModel> GetAllMenusByAppAndUser(LoggingSessionInfo loggingSessionInfo, string appCode)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSessionInfo.CurrentLanguageKindId);
            hashTable.Add("ApplicationCode", appCode);
            hashTable.Add("UserId", loggingSessionInfo.CurrentUser.User_Id);
            hashTable.Add("RoleId", this.GetBasicRoleId(loggingSessionInfo.CurrentUserRole.RoleId));
            hashTable.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);

            return cSqlMapper.Instance().QueryForList<MenuModel>("Menu.GetAllMenusByRoleAndApp", hashTable);
        }
        /// <summary>
        /// 获取所有相关的菜单根据系统号码
        /// </summary>
        /// <param name="appCode">系统号码</param>
        /// <returns></returns>
        public IList<MenuModel> GetAllMenusByAppSysCode(string appCode)
        {
            return cSqlMapper.Instance().QueryForList<MenuModel>("Menu.GetAllMenusByAppSysId", appCode);
        }

        

        /// <summary>
        /// 获取某个应用系统下的第一层菜单
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="appSysId">应用系统Id</param>
        /// <returns></returns>
        public IList<MenuModel> GetMainMenusByAppSysId(LoggingSessionInfo loggingSession, string appSysId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("ApplicationId", appSysId);
            hashTable.Add("CustomerId", loggingSession.CurrentLoggingManager.Customer_Id);

            return cSqlMapper.Instance().QueryForList<MenuModel>("Menu.SelectMainMenusByApplicationId", hashTable);
        }

        /// <summary>
        /// 获取菜单的子菜单
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="menuId">父菜单Id</param>
        /// <returns></returns>
        public IList<MenuModel> GetSubMenus(LoggingSessionInfo loggingSession, string menuId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("MenuId", menuId);

            return cSqlMapper.Instance().QueryForList<MenuModel>("Menu.SelectSubMenus", hashTable);
        }

        /// <summary>
        /// 获取某个角色在某个应用系统下的第一层菜单
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="appCode">应用系统号码</param>
        /// <returns></returns>
        public IList<MenuModel> GetMainMenusByRoleAndApp(LoggingSessionInfo loggingSession, string appCode)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("ApplicationCode", appCode);
            hashTable.Add("RoleId", this.GetBasicRoleId(loggingSession.CurrentUserRole.RoleId));

            return cSqlMapper.Instance().QueryForList<MenuModel>("Menu.GetMainMenusByRoleAndApp", hashTable);
        }

        /// <summary>
        /// 获取某个角色在菜单的子菜单
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="menuId">父菜单Id</param>
        /// <param name="roleId">角色标识</param>
        /// <returns></returns>
        public IList<MenuModel> GetSubMenusByRole(LoggingSessionInfo loggingSession, string roleId, string menuId)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            hashTable.Add("MenuId", menuId);
            hashTable.Add("RoleId", this.GetBasicRoleId(loggingSession.CurrentUserRole.RoleId));

            return cSqlMapper.Instance().QueryForList<MenuModel>("Menu.SelectSubMenusByRoleId", hashTable);
        }
        #endregion

    }
}
