using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Common;
using cPos.Model.User;

namespace cPos.ExchangeService
{
    public class DexAuthService : IDexAuthService
    {
        #region D001-申请用户凭证接口
        /// <summary>
        /// D001-申请用户凭证接口
        /// </summary>
        /// <param name="apply_user_id">申请人ID</param>
        /// <param name="apply_user_pwd">申请人密码(暂时不用填)</param>
        /// <param name="user_id">用户ID</param>
        /// <param name="user_code">用户代码</param>
        /// <param name="customer_id">用户所属客户ID</param>
        /// <param name="customer_code">用户所属客户代码</param>
        /// <param name="user_password">用户密码</param>
        /// <param name="user_role_info_list">用户与门店的关系</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable ApplyUserCertificate(string apply_user_id, string apply_user_pwd,
            string user_id, string user_code, string customer_id, string customer_code, string user_password,
            IList<UserRoleInfo> user_role_info_list)
        {
            var service = new Dex.Services.AuthService();
            return service.ApplyUserCertificate(AppType.BS, apply_user_id, apply_user_pwd,
                user_id, user_code, customer_id, customer_code, user_password, user_role_info_list);
        }
        #endregion

        #region 更新用户凭证接口
        /// <summary>
        /// 更新用户凭证接口
        /// </summary>
        /// <param name="apply_user_id">申请人ID</param>
        /// <param name="apply_user_pwd">申请人密码(暂时不用填)</param>
        /// <param name="user_id">用户ID</param>
        /// <param name="user_pwd_new">修改人新密码</param>
        /// <param name="user_code_new">用户新代码</param>
        /// <param name="user_name_new">用户新名称</param>
        /// <returns>status, error_code, error_desc</returns>
        public Hashtable UpdateUserCertificate(string apply_user_id, string apply_user_pwd,
            string user_id, string user_pwd_new, string user_code_new, string user_name_new,
            IList<UserRoleInfo> user_role_info_list)
        {
            var service = new Dex.Services.AuthService();
            return service.UpdateUserCertificate(AppType.BS, apply_user_id, apply_user_pwd,
                user_id, user_pwd_new, user_code_new, user_name_new, user_role_info_list);
        }
        #endregion
    }
}
