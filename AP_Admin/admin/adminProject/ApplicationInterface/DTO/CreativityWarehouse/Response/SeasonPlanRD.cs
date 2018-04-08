using adminProject.ApplicationInterface.DTO.Base;
using cPos.Admin.Model.CreativityWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response
{
    public class SeasonPlanRD : IAPIResponseData
    {
        public List<SeasonPlanInfo> SeasonPlanList { get; set; }
    }

    public class SeasonPlanInfo {
        public Guid SeasonPlanId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlanDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlanName { get; set; }
    }
}