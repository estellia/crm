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
    /// 属性类
    /// </summary>
    public class PropService
    {
        #region 查询域下信息
        /// <summary>
        /// 获取某个域下的第一层
        /// </summary>
        /// <param name="loggingSessionInfo">登录Model</param>
        /// <param name="propDomain">作用域</param>
        /// <returns></returns>
        public IList<PropInfo> GetPropListFirstByDomain(LoggingSessionInfo loggingSessionInfo, string propDomain)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("PropDomain", propDomain);
                return cSqlMapper.Instance().QueryForList<PropInfo>("Prop.SelectFirst", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        /// <summary>
        /// 获取某个节点下的下一层所有节点信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录Model</param>
        /// <param name="propDomain">作用域</param>
        /// <param name="parentPropId">节点标识</param>
        /// <returns></returns>
        public IList<PropInfo> GetPropListByParentId(LoggingSessionInfo loggingSessionInfo, string propDomain, string parentPropId) {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("PropDomain", propDomain);
                _ht.Add("ParentPropId", parentPropId);
                return cSqlMapper.Instance().QueryForList<PropInfo>("Prop.SelectByParentId", _ht);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        /// <summary>
        /// 获取某个节点的详细信息
        /// </summary>
        /// <param name="loggingSessionInfo">登录Model</param>
        /// <param name="propId">节点标识</param>
        /// <returns></returns>
        public PropInfo GetPropInfoById(LoggingSessionInfo loggingSessionInfo, string propId)
        {
            try
            {
                Hashtable _ht = new Hashtable();
                _ht.Add("PropId", propId);
                return (PropInfo)cSqlMapper.Instance().QueryForObject("Prop.SelectById", _ht);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion
    }
}
