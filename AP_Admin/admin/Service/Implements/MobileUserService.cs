using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cPos.Admin.Component.SqlMappers;
using cPos.Admin.Model.User;
using cPos.Admin.DataCrypt;

namespace cPos.Admin.Service.Implements
{
    /// <summary>
    /// 销售员管理类
    /// </summary>
    public class MobileUserService : BaseService, cPos.Admin.Service.Interfaces.IMobileUserService
    {
        #region IMobileUserService Members

        #region 验证用户
        /// <summary>
        /// 验证用户名和密码
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="ht">验证通过后返回的数据(UserID:用户ID;UserName:用户姓名)</param>
        /// <returns>-1:用户不存在;-2:用户被停用;-3:密码不正确;1:成功;</returns>
        public int ValidateUserSales(string account, string password, out Hashtable ht)
        {
            ht = new Hashtable();
            if (string.IsNullOrEmpty(account))
            {
                throw new ArgumentNullException("account");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            string encrypted_pwd = HashManager.Hash(password, HashProviderType.MD5);

            UserInfo user = MSSqlMapper.Instance().QueryForObject<UserInfo>("User.MobileUser.SelectUserByAccount", account);
            if (user == null)
            {
                //用户不存在
                return -1;
            }

            if (user.Status == -1)
            {
                //用户被停用
                return -2;
            }

            if (user.Password == encrypted_pwd)
            {
                //插入登录日志
                //Hashtable ht_temp = new Hashtable();
                //ht_temp.Add("UserID", user.ID);
                //ht_temp.Add("SessionID", Component.SessionManager.SessionID);
                //ht_temp.Add("LoginIP", Component.SessionManager.CurrentIP);
                //MSSqlMapper.Instance().Insert("User.User.InsertLoginLog", ht_temp);
                //返回数据
                ht.Add("UserID", user.ID);
                ht.Add("UserName", user.Name);
                return 1;
            }
            else
            {
                //密码不正确
                return -3;
            }
        }
        #endregion

        /// <summary>
        /// 返回用户信息
        /// </summary>
        /// <param name="user_id">用户标识</param>
        /// <returns>返回用户信息</returns>
        public UserInfo GetUserInfoById(string user_id)
        {
            UserInfo userInfo = new UserInfo();
            userInfo = MSSqlMapper.Instance().QueryForObject<UserInfo>("User.MobileUser.SelectUserByID", user_id);
            return userInfo;
        }

        #endregion
    }
}
