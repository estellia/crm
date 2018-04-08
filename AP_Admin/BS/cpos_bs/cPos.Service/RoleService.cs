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
    /// 角色服务
    /// </summary>
    public class RoleService : BaseService
    {
        #region 保存角色信息
        /// <summary>
        /// 设置角色保存信息（新建修改）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        public string SetRoleInfo(LoggingSessionInfo loggingSessionInfo, RoleModel roleInfo)
        {
            try {
                if (roleInfo != null)
                {
                    roleInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                    if (!IsExistRoleCode(loggingSessionInfo,roleInfo.Role_Code,roleInfo.Role_Id)){
                        return "号码重复";
                    }
                    if (roleInfo.Modify_User_id == null || roleInfo.Modify_User_id.Equals(""))
                    {
                        roleInfo.Modify_User_id = loggingSessionInfo.CurrentUser.User_Id;
                        roleInfo.Modify_Time = GetCurrentDateTime();
                    }
                    if (roleInfo.Role_Id == null || roleInfo.Role_Id.Equals(""))
                    {
                        roleInfo.Role_Id = NewGuid();
                        if (roleInfo.Create_User_Id == null || roleInfo.Create_User_Id.Equals(""))
                        {
                            roleInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
                            roleInfo.Create_Time = GetCurrentDateTime();
                        }
                        
                    }
                    if (roleInfo.RoleMenuInfoList != null)
                    {
                        foreach (RoleMenuModel roleMenuInfo in roleInfo.RoleMenuInfoList)
                        {
                            roleMenuInfo.Role_Id = roleInfo.Role_Id;
                            roleMenuInfo.Create_User_Id = roleInfo.Create_User_Id;
                            roleMenuInfo.Create_Time = roleInfo.Create_Time;
                            roleMenuInfo.Modify_User_id = roleInfo.Modify_User_id;
                            roleMenuInfo.Modify_Time = roleInfo.Modify_Time;
                            roleMenuInfo.Status = 1;
                        }
                    }
                    

                    cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("Role.InsertOrUpdate", roleInfo);
                    return "保存成功.";
                }
                else {
                    return "RoleModel对象为空";
                }

                
            }
            catch(Exception ex) {
                throw(ex);
            }
        }

        public bool SetRoleInfo(LoggingManager logggingManager, RoleModel roleInfo)
        {
            try
            {
                if (roleInfo != null)
                {
                    
                    if (roleInfo.Modify_User_id == null || roleInfo.Modify_User_id.Equals(""))
                    {
                        roleInfo.Modify_User_id = roleInfo.Create_User_Id;
                        roleInfo.Modify_Time = GetCurrentDateTime();
                    }
                    if (roleInfo.Role_Id == null || roleInfo.Role_Id.Equals(""))
                    {
                        roleInfo.Role_Id = NewGuid();
                        if (roleInfo.Create_User_Id == null || roleInfo.Create_User_Id.Equals(""))
                        {
                            roleInfo.Create_Time = GetCurrentDateTime();
                        }

                    }
                    if (roleInfo.RoleMenuInfoList != null)
                    {
                        foreach (RoleMenuModel roleMenuInfo in roleInfo.RoleMenuInfoList)
                        {
                            roleMenuInfo.Role_Id = roleInfo.Role_Id;
                            roleMenuInfo.Create_User_Id = roleInfo.Create_User_Id;
                            roleMenuInfo.Create_Time = roleInfo.Create_Time;
                            roleMenuInfo.Modify_User_id = roleInfo.Modify_User_id;
                            roleMenuInfo.Modify_Time = roleInfo.Modify_Time;
                            roleMenuInfo.Status = 1;
                        }
                    }


                    cSqlMapper.Instance(logggingManager).Update("Role.InsertOrUpdate", roleInfo);
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw (new Exception("保存角色失败" + ex.ToString()));
            }
        }

        /// <summary>
        /// 判断角色号码是否重复
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="RoleCode"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        private bool IsExistRoleCode(LoggingSessionInfo loggingSessionInfo, string RoleCode, string RoleId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("Role_Code", RoleCode);
                _ht.Add("Role_Id", RoleId);
                _ht.Add("Customer_Id", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
                int n = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject<int>("Role.IsExsitRoleCode", _ht);
                return n > 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region 根据组织，获取角色信息
        /// <summary>
        /// 根据组织，获取角色信息
        /// </summary>
        /// <param name="loggingSessionInfo">当前登录的信息</param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public IList<RoleModel> GetRoleListByUnitId(LoggingSessionInfo loggingSessionInfo, string unitId)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("UnitId", unitId);
            _ht.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<RoleModel>("Role.SelectByUnitId", _ht);

        }

        /// <summary>
        /// 查询所有的角色
        /// </summary>
        /// <param name="loggingSessionInfo">当前登录的信息</param>
        /// <returns></returns>
        public IList<RoleModel> GetAllRoles(LoggingSessionInfo loggingSessionInfo)
        {
            return cSqlMapper.Instance().QueryForList<RoleModel>("Role.SelectAllRoles", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
        }
        /// <summary>
        /// 获取客户的默认角色
        /// </summary>
        /// <param name="loggingManager"></param>
        /// <param name="customer_id"></param>
        /// <returns></returns>
        public RoleModel GetRoleDefaultByCustomerId(LoggingManager loggingManager, string customer_id)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("CustomerId", customer_id);
            return cSqlMapper.Instance(loggingManager).QueryForObject<RoleModel>("Role.SelectDefaultByCustomerId", hashTable);
        }
        #endregion
    }
}
