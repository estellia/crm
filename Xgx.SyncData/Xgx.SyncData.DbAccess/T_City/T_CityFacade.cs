using Xgx.SyncData.DbAccess.t_unit;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess.T_City
{
    public class T_CityFacade
    {
        private readonly T_CityCMD _cmd = new T_CityCMD();
        private readonly T_CityQuery _query = new T_CityQuery();
        public void Create(T_CityEntity dbEntity)
        {
            _cmd.Create(dbEntity);
        }
        public void Update(T_CityEntity dbEntity)
        {
            _cmd.Update(dbEntity);
        }

        public void Delete(T_CityEntity dbEntity)
        {
            _cmd.Delete(dbEntity);
        }

        public string GetIdByName(string cityName1, string cityName2, string cityName3)
        {
            return _query.GetIdByName(cityName1, cityName2, cityName3);
        }
    }
}
