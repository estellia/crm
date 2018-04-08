/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/17 11:16:35
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.AppVersion.Entity;

namespace JIT.Utility.AppVersion.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class AppVersionBLL
    {
        public AppVersionEntity GetVersion(string pClientID, string pAppCode, string pVersion)
        {
            var temps = this._currentDAO.GetVersion(pClientID, pAppCode);
            if (temps.Length > 0)
            {
                var list = temps.Where(t => CompareVersion(t.Version, pVersion) == 1).ToList();
                if (list.Count > 0)
                {
                    list.Sort((t1, t2) =>
                    { return CompareVersion(t1.Version, t2.Version); });
                    return list[0];
                }
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="versionNew">新版本</param>
        /// <param name="versionOld">旧版本</param>
        /// <returns></returns>
        private int CompareVersion(string versionNew, string versionOld)
        {
            var temp1 = BuQuan4(versionNew.Split('.'));
            var temp2 = BuQuan4(versionOld.Split('.'));
            if (temp1.Length != 4 || temp2.Length != 4)
                throw new Exception(string.Format("版本值不正确,V1:{0},V2:{1}", versionNew, versionOld));
            if (Convert.ToInt32(temp1[0]) > Convert.ToInt32(temp2[0]))
                return 1;
            else if (Convert.ToInt32(temp1[0]) == Convert.ToInt32(temp2[0]) && Convert.ToInt32(temp1[1]) > Convert.ToInt32(temp2[1]))
                return 1;
            else if (Convert.ToInt32(temp1[0]) == Convert.ToInt32(temp2[0]) && Convert.ToInt32(temp1[1]) == Convert.ToInt32(temp2[1]) && Convert.ToInt32(temp1[2]) > Convert.ToInt32(temp2[2]))
                return 1;
            else if (Convert.ToInt32(temp1[0]) == Convert.ToInt32(temp2[0]) && Convert.ToInt32(temp1[1]) == Convert.ToInt32(temp2[1]) && Convert.ToInt32(temp1[2]) == Convert.ToInt32(temp2[2]) && Convert.ToInt32(temp1[3]) > Convert.ToInt32(temp2[3]))
                return 1;
            else if (Convert.ToInt32(temp1[0]) == Convert.ToInt32(temp2[0]) && Convert.ToInt32(temp1[1]) == Convert.ToInt32(temp2[1]) && Convert.ToInt32(temp1[2]) == Convert.ToInt32(temp2[2]) && Convert.ToInt32(temp1[3]) == Convert.ToInt32(temp2[3]))
                return 0;
            else
                return -1;
        }

        private string[] BuQuan4(string[] array)
        {
            var list = array.ToList();
            var count = list.Count;
            if (list.Count < 4)
            {
                for (int i = 0; i < 4 - count; i++)
                {
                    list.Add("0");
                }
            }
            return list.ToArray();
        }
    }
}