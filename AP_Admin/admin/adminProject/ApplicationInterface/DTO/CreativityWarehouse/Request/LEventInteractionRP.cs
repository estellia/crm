using adminProject.ApplicationInterface.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Request
{

    public class LEventInteractionRP : IAPIRequestParameter
    {
        public string InteractionId { get; set;}
        public string TemplateId { get; set; }
        public string ThemeId { get; set;}
        public int InteractionType { get; set; }
        public string DrawMethodId { get; set; }
        public string LeventId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public void Validate()
        { }
    }
}