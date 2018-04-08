using adminProject.ApplicationInterface.DTO.Base;
using cPos.Admin.Model.CreativityWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response
{
    public class LEventsRD : IAPIResponseData
    {
        public T_CTW_LEventsData LEventsData { get; set; }

        public string BackGroundImageUrl { get; set; }
        public string BackGroundImageId { get; set; }
        public string LogoImageUrl { get; set; }
        public string LogoImageId { get; set; }
        public string NotReceiveImageUrl { get; set; }
        public string NotReceiveImageId { get; set; }
        public string ReceiveImageUrl { get; set; }
        public string ReceiveImageId { get; set; }
        public string RuleImageUrl { get; set; }
        public string RuleImageId { get; set; }
    }
}