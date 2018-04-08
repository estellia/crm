using adminProject.ApplicationInterface.DTO.Base;
using cPos.Admin.Model.CreativityWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response
{
    public class SysMarketingGroupTypeRD : IAPIResponseData
    {
        public IList<SysMarketingGroupTypeData> MarketingTypeList { get; set; }

        public List<SysMarketingGroupTypeInfo> SysMarketingGroupTypeDropDownList { get; set; }
    }

    public class SysMarketingGroupTypeInfo {
        public string ActivityGroupId { get; set; }
        public string Name { get; set; }
    }
}