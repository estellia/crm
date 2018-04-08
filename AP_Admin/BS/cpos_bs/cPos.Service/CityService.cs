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


namespace cPos.Service
{
    /// <summary>
    /// 城市服务
    /// </summary>
    public class CityService
    {
        #region 获取城市信息
        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public IList<CityInfo> GetCityInfoList(LoggingSessionInfo loggingSessionInfo)
        {
            Hashtable _ht = new Hashtable();
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<CityInfo>("City.Select", _ht);
        }
        /// <summary>
        /// 获取单个城市信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="city_id"></param>
        /// <returns></returns>
        public CityInfo GetCityById(LoggingSessionInfo loggingSessionInfo, string city_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("CityId", city_id);
            return (CityInfo)cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject("City.SelectById", _ht);
        }

        public CityInfo GetCityById(LoggingManager loggingManager, string city_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("CityId", city_id);
            return (CityInfo)cSqlMapper.Instance(loggingManager).QueryForObject("City.SelectById", _ht);
        }
        #endregion

        #region 获取省
        /// <summary>
        /// 获取省
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public IList<CityInfo> GetProvinceList(LoggingSessionInfo loggingSessionInfo)
        {
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<CityInfo>("City.SelectProvince", "");
        }
        #endregion

        #region 获取市
        /// <summary>
        /// 根据省获取市
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="city_code">省号码</param>
        /// <returns></returns>
        public IList<CityInfo> GetCityListByProvince(LoggingSessionInfo loggingSessionInfo,string city_code)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("CityCode", city_code);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<CityInfo>("City.SelectCity", _ht);
        }
        #endregion

        #region 获取地区
        /// <summary>
        /// 根据市获取地区
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="city_code">市号码</param>
        /// <returns></returns>
        public IList<CityInfo> GetAreaListByCity(LoggingSessionInfo loggingSessionInfo, string city_code)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("CityCode", city_code);
            return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<CityInfo>("City.SelectArea", _ht);
        }
        #endregion
    }
}
