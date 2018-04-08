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
    public class UserService : BaseService, cPos.Admin.Service.Interfaces.IUserService
    {
        #region IUserService Members

        public int ValidateUser(string account, string password, out Hashtable ht)
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

            UserInfo user = MSSqlMapper.Instance().QueryForObject<UserInfo>("User.User.SelectUserByAccount", account);
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
                Hashtable ht_temp = new Hashtable();
                ht_temp.Add("UserID",user.ID);
                ht_temp.Add("SessionID", Component.SessionManager.SessionID);
                ht_temp.Add("LoginIP", Component.SessionManager.CurrentIP);
                MSSqlMapper.Instance().Insert("User.User.InsertLoginLog", ht_temp);
                //返回数据
                ht.Add("UserID", user.ID);
                ht.Add("UserName", user.Name);
                ht.Add("unit_id", user.unit_id);
                return 1;
            }
            else
            {
                //密码不正确
                return -3;
            }
        }

        public string GetUserPassword(string userID)
        {
            return MSSqlMapper.Instance().QueryForObject<string>("User.User.SelectUserPwd", userID);
        }

        public int ModifyUserPassword(string userID, string oldPwd, string newPwd)
        {
            UserInfo user = MSSqlMapper.Instance().QueryForObject<UserInfo>("User.User.SelectUserByID", userID);
            if(user == null)
            {
                return 1;
            }
            if (user.Status == -1)
            {
                return 2;
            }
            if (user.Password != oldPwd)
            {
                return 3;
            }
            Hashtable ht = new Hashtable();
            ht.Add("UserID", userID);
            ht.Add("UserPwd", newPwd);
            int ret = MSSqlMapper.Instance().Update("User.User.UpdateUserPwd", ht);
            if (ret == 1)
                return 4;
            else
                return 5;
        }

        public bool ValidateUserPassword(string oldPwd, string newPwd)
        {
            if (String.IsNullOrEmpty(oldPwd))
                return false;
            if (String.IsNullOrEmpty(newPwd))
                return false;
            if (oldPwd == newPwd)
                return false;
            if (newPwd.Length < 8)
                return false;
            if (newPwd.Length != newPwd.ToCharArray().Length)
                return false;
            bool hasSymbol = false, hasNumber = false, hasSpecial = false;
            foreach (char ch in newPwd.ToUpper().ToCharArray())
            {
                if (ch >= '0' && ch <= '9')
                {
                    hasNumber = true;
                }
                else
                {
                    if (ch >= 'A' && ch <= 'Z')
                        hasSymbol = true;
                    else
                        hasSpecial = true;
                }
            }
            return hasSymbol && hasNumber && hasSpecial;
        }

        public IList<UserInfo> GetUserList(Hashtable condition)
        {
            if (!condition.ContainsKey("StartRow"))
                condition["StartRow"] = 0;
            if (!condition.ContainsKey("EndRow"))
                condition["EndRow"] = 10000;

            return MSSqlMapper.Instance().QueryForList<UserInfo>("User.User.GetUserList", condition);
        }
        #endregion
    }
}
