using adminProject.ApplicationInterface.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Request
{
    public class SeasonPlanRP : IAPIRequestParameter
    {
        public string SeasonPlanId { get; set; }
        public DateTime PlanDate { get; set; }
        public string PlanName { get; set; }

        public void Validate()
        { }
    }
}