using adminProject.ApplicationInterface.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Request
{
    public class LEventThemeRP:IAPIRequestParameter
    {
        public string ThemeId { get; set; }
        public string TemplateId { get; set; }
        public string ThemeName { get; set; }
        public string ImageId { get; set; }
        public string H5TemplateId { get; set; }
        public string H5Url { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public void Validate()
        { }
    }

   
}