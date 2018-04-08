using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using cPos.Model;
using cPos.Model.User;
//using cPos.SqlHelper;
using IBatisNet.DataMapper;
using cPos.Components.SqlMappers;
using cPos.Components;
using cPos.Service;
//using cPos.ExchangeService;


namespace cPos.Service
{
    /// <summary>
    /// 类型服务
    /// </summary>
    public class TypeService
    {
        #region 获取某个域的类型集合
        /// <summary>
        /// 获取某个域的类型集合
        /// </summary>
        /// <param name="loggingSessionInfo">登录信息</param>
        /// <param name="type_domaion">域</param>
        /// <returns></returns>
        public IList<TypeInfo> GetTypeInfoListByDomain(LoggingSessionInfo loggingSessionInfo,string type_domaion)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("type_domian", type_domaion);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<TypeInfo>("Type.SelectByDomain", _ht);
        }
        #endregion

        #region 获取单个类型信息
        /// <summary>
        /// 获取单个类型信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="type_id"></param>
        /// <returns></returns>
        public TypeInfo GetTypeInfoById(LoggingSessionInfo loggingSessionInfo, string type_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("Type_Id", type_id);
            return (TypeInfo)cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject("Type.SelectById", _ht);

        }
        #endregion
    }
}
