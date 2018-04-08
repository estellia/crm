using adminProject.ApplicationInterface.DTO.Base;
using cPos.Admin.Model.CreativityWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response
{
    public class SpreadSettingRD : IAPIResponseData
    {
        public IList<T_CTW_SpreadSettingData> SpreadSettingList { get; set; }
    }
}