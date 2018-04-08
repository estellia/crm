using adminProject.ApplicationInterface.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Request
{
    public class PanicbuyingEventRP : IAPIRequestParameter
    {
        public string EventId { get; set; }

        public string EventName { get; set; }
        public string EventCode { get; set; }

        public string ImageId { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public void Validate()
        { }
    }
}