using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using cPos.Dex.Model;
using cPos.Dex.Components.SqlMappers;
using cPos.Dex.Common;

namespace cPos.Dex.Services
{
    public class BasicService
    {
        /// <summary>
        /// 获取版本
        /// </summary>
        public PosVersionInfo GetPosVersion(Hashtable ht)
        {
            return SqlMapper.Instance().QueryForObject<PosVersionInfo>("PosVersionInfo.GetPosVersion", ht);
        }

        /// <summary>
        /// 获取最新发布的版本
        /// </summary>
        public PosVersionInfo GetLastPubPosVersion(string version)
        {
            return SqlMapper.Instance().QueryForObject<PosVersionInfo>("PosVersionInfo.GetLastPubPosVersion", version);
        }
    }
}
