using adminProject.ApplicationInterface.DTO.Base;
using cPos.Admin.Model.CreativityWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response
{
    public class PanicbuyingEventRD : IAPIResponseData
    {
        public PanicbuyingEventInfos PanicbuyingEventInfoData { get; set; }

        public IList<T_CTW_PanicbuyingEventData> PanicbuyingEventList { get; set; }

        public int TotalCount { get; set; }

        public int TotalPageCount { get; set; }
    }

    public class PanicbuyingEventInfos {
        public string EventId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EventName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ImageUrl { get; set; }

        public string ImageId { get; set; }
    }
}