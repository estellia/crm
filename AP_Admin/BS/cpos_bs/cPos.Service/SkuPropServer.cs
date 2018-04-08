using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Components.SqlMappers;

namespace cPos.Service
{
    /// <summary>
    /// sku属性
    /// </summary>
    public class SkuPropServer
    {
        /// <summary>
        /// 获取sku属性集合
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public IList<SkuPropInfo> GetSkuPropList(LoggingSessionInfo loggingSessionInfo)
        {
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<SkuPropInfo>("SkuProp.Select", "");
        }
    }
}
