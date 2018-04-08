using adminProject.ApplicationInterface.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Request
{
    public class LEventsRP : IAPIRequestParameter
    {
        public string EventId { get; set; }

        public string EventCode { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<string> ImageIdList { get; set; }
        public void Validate()
        { }
    }
}