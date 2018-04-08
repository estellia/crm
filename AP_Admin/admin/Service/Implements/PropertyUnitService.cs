using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cPos.Model;
using System.Collections;
using cPos.Admin.Component.SqlMappers;

namespace cPos.Admin.Service
{
    /// <summary>
    /// 属性与组织关系
    /// </summary>
    public class PropertyUnitService
    {
        /// <summary>
        /// 获取组织与属性关系值
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public IList<UnitPropertyInfo> GetUnitPropertyListByUnit(LoggingSessionInfo loggingSessionInfo, string unitId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("UnitId", unitId);
                return MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UnitPropertyInfo>("PropertyUnit.SelectByUnitId", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
    }
}
