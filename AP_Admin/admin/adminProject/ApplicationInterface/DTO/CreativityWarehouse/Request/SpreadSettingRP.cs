using adminProject.ApplicationInterface.DTO.Base;
using cPos.Admin.Model.CreativityWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Request
{
    public class SpreadSettingRP : IAPIRequestParameter
    {
        public string TemplateId { get; set; }
        public List<T_CTW_SpreadSettingData> SpreadSettingList { get; set; }
        public void Validate()
        { }
    }
}