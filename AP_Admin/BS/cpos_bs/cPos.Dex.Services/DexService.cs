using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using System.Collections;

namespace cPos.Dex.Services
{
    public class DexService
    {
        /// <summary>
        /// GetInterfaces
        /// </summary>
        /// <param name="apply_user_id"></param>
        /// <param name="apply_user_pwd"></param>
        /// <param name="user_id"></param>
        /// <param name="user_code"></param>
        /// <param name="unit_code"></param>
        /// <param name="user_password"></param>
        /// <returns></returns>
        public IList<InterfaceInfo> GetInterfaces(Hashtable ht)
        {
            IList<InterfaceInfo> list = new List<InterfaceInfo>();

            list = SqlMapper.Instance().QueryForList<InterfaceInfo>("InterfaceInfo.GetInterfaces", ht);

            return list;
        }
    }
}
