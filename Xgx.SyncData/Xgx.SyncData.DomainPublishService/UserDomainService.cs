using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xgx.SyncData.Common;
using Xgx.SyncData.Contract;
using Xgx.SyncData.DbAccess.T_User;
using Zmind.EventBus.Contract;
using OptEnum = Xgx.SyncData.Contract.OptEnum;

namespace Xgx.SyncData.DomainPublishService
{
    public class UserDomainService : IPublish
    {
        public void Deal(EventContract msg)
        {
            var bus = MqBusMgr.GetInstance();
            OptEnum operation;
            Enum.TryParse(msg.Operation.ToString(), out operation);
            var userContract = new UserContract
            {
                Operation = operation,
                UnitId = msg.Id
            };
            if (msg.Operation != Zmind.EventBus.Contract.OptEnum.Delete)
            {
                var userFacade = new T_UserFacade();
                var result = userFacade.GetUserById(msg.Id);
                if (result == null) return;
                userContract.UserCode = result.user_code;
                userContract.UserName = result.user_name;
                userContract.UserTelephone = result.user_telephone;
                userContract.CreateTime = string.IsNullOrEmpty(result.create_time) ? null : DateTime.Parse(result.create_time);
                userContract.ModifyTime = string.IsNullOrEmpty(result.modify_time) ? null : DateTime.Parse(result.modify_time);
                userContract.UnitId = result.unit_id;
                userContract.RoleCode = new List<RoleEnum>();
                var roleResult = userFacade.GetUserRoleCode(msg.Id);
                if (roleResult != null)
                {
                    foreach (var i in roleResult)
                    {
                        RoleEnum role;
                        Enum.TryParse(i, out role);
                        userContract.RoleCode.Add(role);
                    }
                }
            }
            bus.Publish<IZmindToXgx>(userContract);
        }
    }
}
