using adminProject.ApplicationInterface.DTO.Base;
using cPos.Admin.Model.CreativityWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response
{
    public class BannerRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int TotalPageCount { get; set; }
        public List<BannerInfo> BannerInfoList { get; set; }
    }

    public class BannerInfo
    {
        public string AdId { get; set; }

        public string ActivityGroupId { get; set; }

        public string BannerName { get; set; }

        public string ActivityGroupName { get; set; }

        /// <summary>
        /// 10=待上架   20=已发布   30=已下架   
        /// </summary>
        public int Status { get; set; }

        public int DisplayIndex { get; set; }

        public string ImageUrl { get; set; }

        public string BannerUrl { get; set; }

        public string TemplateId { get; set; }

        public string BannerImageId { get; set; }
    }
}