using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Admin.Model.User;

namespace cPos.Admin.Service.Interfaces
{
    public interface IMobileUserService
    {
        /// <summary>
        /// 验证用户名和密码
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="ht">验证通过后返回的数据(UserID:用户ID;UserName:用户姓名)</param>
        /// <returns>-1:用户不存在;-2:用户被停用;-3:密码不正确;1:成功;</returns>
        int ValidateUserSales(string account, string password, out Hashtable ht);
        /// <summary>
        /// 返回用户信息
        /// </summary>
        /// <param name="user_id">用户标识</param>
        /// <returns>返回用户信息，以及用户与客户信息，用户与门店信息</returns>
        UserInfo GetUserInfoById(string user_id);
    }
}
