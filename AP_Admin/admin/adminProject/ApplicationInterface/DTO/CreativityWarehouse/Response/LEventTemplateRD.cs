using adminProject.ApplicationInterface.DTO.Base;
using cPos.Admin.Model.CreativityWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response
{
    public class LEventTemplateRD : IAPIResponseData
    {

        public int TotalCount { get; set; }
        public int TotalPageCount { get; set; }
        public IList<T_CTW_LEventTemplateData> LEventTemplateList { get; set; }

        public List<LEventTemplateInfo> LEventTemplateDropDownList { get; set; }
        public LEventTemplateInfo LEventTemplateInfo { get; set; }

    }

    public class LEventTemplateInfo
    {
        public string TemplateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ActivityGroupId { get; set; }

        public string ImageId { get; set; }
        public string ImageUrl { get; set; }
    }

}