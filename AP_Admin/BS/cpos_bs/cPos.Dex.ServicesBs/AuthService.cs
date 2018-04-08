using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using cPos.Dex.Common;
using cPos.Dex.ContractModel;
using cPos.Dex.Services;

namespace cPos.Dex.ServicesBs
{
    public class AuthService
    {
        #region ChangePassword
        /// <summary>
        /// 修改密码
        /// </summary>
        public bool ChangePassword(string customerId, string unitId, string userId, string newPassword)
        {
            if (customerId == null || customerId.Trim().Length == 0)
                throw new Exception("客户ID不能为空");
            if (userId == null || userId.Trim().Length == 0)
                throw new Exception("用户ID不能为空");

            // Save
            var userAuthService = new ExchangeBsService.UserAuthService();
            return userAuthService.SetUserPassword(userId, customerId, unitId, userId, newPassword);
        }
        #endregion
    }
}
