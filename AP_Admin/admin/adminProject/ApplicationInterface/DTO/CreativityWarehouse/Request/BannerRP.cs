using adminProject.ApplicationInterface.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Request
{
    public class BannerRP : IAPIRequestParameter
    {
        public string AdId { get; set; }
        public string ActivityGroupId { get; set; }
        public string TemplateId { get; set; }
        public string BannerImageId { get; set; }
        public string BannerUrl { get; set; }
        public int DisplayIndex { get; set; }
        public string BannerName { get; set; }
        /// <summary>
        /// 10=待上架 20=待发布 30=已发布 40=已下架 
        /// </summary>
        public int Status { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public void Validate()
        { }
    }
}