using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Service.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// 验证用户名和密码
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="ht">验证通过后返回的数据(UserID:用户ID;UserName:用户姓名)</param>
        /// <returns>-1:用户不存在;-2:用户被停用;-3:密码不正确;1:成功;</returns>
        int ValidateUser(string account, string password, out Hashtable ht);

        /// <summary>
        /// 获取一个登录帐号的密码
        /// </summary>
        /// <param name="userID">登录帐号的ID</param>
        /// <returns></returns>
        string GetUserPassword(string userID);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="oldPwd">旧密码(加过密的)</param>
        /// <param name="newPwd">新密码(加过密的)</param>
        /// <returns>1:用户不存在;2:用户被停用;3:旧密码不正确;4:修改成功;5:修改失败</returns>
        int ModifyUserPassword(string userID, string oldPwd, string newPwd);

        /// <summary>
        /// 校验密码是否有效
        /// </summary>
        /// <param name="oldPwd">旧密码(加过密的)</param>
        /// <param name="newPwd">新密码(加过密的)</param>
        /// <returns></returns>
        bool ValidateUserPassword(string oldPwd, string newPwd);
    }
}
