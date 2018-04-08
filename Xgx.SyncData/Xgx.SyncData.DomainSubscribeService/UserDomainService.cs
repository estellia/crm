using System;
using System.Collections.Generic;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.DbAccess.t_customer_user;
using Xgx.SyncData.DbAccess.T_City;
using Xgx.SyncData.DbAccess.T_Role;
using Xgx.SyncData.DbAccess.T_User;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DomainSubscribeService
{
    public class UserDomainService
    {
        public void Deal(UserContract contract)
        {
            var userDbEntity = ConvertToT_User(contract);
            var userRoleDbEntity = ConvertToT_User_Role(contract);
            var userApDbEntity = ConvertTot_customer_user(contract);
            var userFacade = new T_UserFacade();
            var userRoleFacade = new T_User_RoleFacade();
            var userApFacade = new t_customer_userFacade();
            var result = userRoleFacade.GetIdByUserId(contract.UserId);
            foreach (var i in result)
            {
                userRoleFacade.Delete(new T_User_RoleEntity {user_role_id = i});
            }
            switch (contract.Operation)
            {
                case OptEnum.Create:
                    userDbEntity.user_password = "e10adc3949ba59abbe56e057f20f883e";
                    userFacade.Create(userDbEntity);
                    userApDbEntity.cu_pwd = userDbEntity.user_password;
                    userApFacade.Create(userApDbEntity);
                    foreach (var i in userRoleDbEntity)
                    {
                        userRoleFacade.Create(i);
                    }
                    break;
                case OptEnum.Update:
                    userDbEntity.user_password = userFacade.GetUserPwd(userDbEntity.user_id);
                    userFacade.Update(userDbEntity);
                    userApDbEntity.cu_pwd = userDbEntity.user_password;
                    userApFacade.Update(userApDbEntity);
                    foreach (var i in userRoleDbEntity)
                    {
                        userRoleFacade.Create(i);
                    }
                    break;
                case OptEnum.Delete:
                    userFacade.Delete(userDbEntity);
                    userApFacade.Delete(userApDbEntity);
                    break;
            }
        }

        private T_UserEntity ConvertToT_User(UserContract contract)
        {
            var result = new T_UserEntity
            {
                user_id = contract.UserId,
                user_code = contract.UserCode,
                user_name = contract.UserName,
                user_telephone = contract.UserTelephone,
                create_time = contract.CreateTime != null ? contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                modify_time = contract.ModifyTime != null ? contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                customer_id = ConfigMgr.CustomerId,
                user_status = "1"
            };
            return result;
        }

        private List<T_User_RoleEntity> ConvertToT_User_Role(UserContract contract)
        {
            var result = new List<T_User_RoleEntity>();
            var roleFacade = new T_RoleFacade();
            foreach (var i in contract.RoleCode)
            {
                var t = new T_User_RoleEntity
                {
                    user_role_id = Guid.NewGuid().ToString("N"),
                    user_id = contract.UserId,
                    role_id = roleFacade.GetIdByCode(i.ToString(), ConfigMgr.CustomerId),
                    unit_id = contract.UnitId == ConfigMgr.XgxHeadUnitId ? ConfigMgr.HeadUnitId : contract.UnitId,
                    status = 1,
                    create_time = contract.CreateTime != null ? contract.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                    modify_time = contract.ModifyTime != null ? contract.ModifyTime.Value.ToString("yyyy-MM-dd hh:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                    create_user_id = "xgx"
                };
                result.Add(t);
            }
            return result;
        }

        private t_customer_userEntity ConvertTot_customer_user(UserContract contract)
        {
            var result = new t_customer_userEntity
            {
                customer_id = ConfigMgr.CustomerId,
                customer_user_id = contract.UserId,
                cu_account = contract.UserCode,
                cu_name = contract.UserName,
                cu_expired_date = "2030-12-30",
                sys_modify_stamp = contract.ModifyTime,
                cu_status = 1,
                cu_status_desc = "正常"
            };
            return result;
        }
    }
}
