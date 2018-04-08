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
using cPos.Admin.Component.SqlMappers;
using cPos.Admin.Component;

namespace cPos.Admin.Service
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
        public IList<CityInfo> GetCityInfoList(cPos.Model.LoggingSessionInfo loggingSessionInfo)
        {
            Hashtable _ht = new Hashtable();
            return MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<CityInfo>("City.Select", _ht);
        }
        /// <summary>
        /// 获取单个城市信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="city_id"></param>
        /// <returns></returns>
        public CityInfo GetCityById(cPos.Model.LoggingSessionInfo loggingSessionInfo, string city_id)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("CityId", city_id);
            return (CityInfo)MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForObject("City.SelectById", _ht);
        }
        #endregion

        #region 获取省
        /// <summary>
        /// 获取省
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public IList<CityInfo> GetProvinceList(cPos.Model.LoggingSessionInfo loggingSessionInfo)
        {
            return MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<CityInfo>("City.SelectProvince", "");
        }
        #endregion

        #region 获取市
        /// <summary>
        /// 根据省获取市
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="city_code">省号码</param>
        /// <returns></returns>
        public IList<CityInfo> GetCityListByProvince(cPos.Model.LoggingSessionInfo loggingSessionInfo,string city_code)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("CityCode", city_code);
            return MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<CityInfo>("City.SelectCity", _ht);
        }
        #endregion

        #region 获取地区
        /// <summary>
        /// 根据市获取地区
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="city_code">市号码</param>
        /// <returns></returns>
        public IList<CityInfo> GetAreaListByCity(cPos.Model.LoggingSessionInfo loggingSessionInfo, string city_code)
        {
            Hashtable _ht = new Hashtable();
            _ht.Add("CityCode", city_code);
            return MSSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<CityInfo>("City.SelectArea", _ht);
        }
        #endregion
    }
}
