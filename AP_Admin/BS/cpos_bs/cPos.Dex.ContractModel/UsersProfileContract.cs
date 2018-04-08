using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Dex.ContractModel
{
    [DataContract(Namespace = Common.Config.NS, Name = "data")]
    public class UsersProfileContract : BaseContract
    {
        [DataMember(Name = "menus")]
        public IList<Menu> menus { get; set; }

        [DataMember(Name = "roles")]
        public IList<Role> roles { get; set; }

        [DataMember(Name = "role_menus")]
        public IList<RoleMenu> role_menus { get; set; }

        [DataMember(Name = "users")]
        public IList<User> users { get; set; }

        [DataMember(Name = "user_roles")]
        public IList<UserRole> user_roles { get; set; }
    }
}