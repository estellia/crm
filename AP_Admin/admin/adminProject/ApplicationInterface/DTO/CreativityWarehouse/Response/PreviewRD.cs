using adminProject.ApplicationInterface.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response
{
    public class PreviewRD : IAPIResponseData
    {
        public List<BannerPreview> BannerPreviewList { get; set; }
        public List<SeasonPlanPreview> SeasonPlanPreviewList { get; set; }
        public HolidaySysMarketingGroupTypePreview HolidaySysMarketingGroupTypePreview { get; set; }
        public UnitSysMarketingGroupTypePreview UnitSysMarketingGroupTypePreview { get; set; }
        public ProductSysMarketingGroupTypePreview ProductSysMarketingGroupTypePreview { get; set; }
        public List<LEventTemplatePreview> LEventTemplatePreviewList { get; set; }
    }
    /// <summary>
    /// KV
    /// </summary>
    public class BannerPreview
    {
        public string ImageUrl { get; set; }
        public int? DisplayIndex { get; set; }
    }
    /// <summary>
    /// 计划
    /// </summary>
    public class SeasonPlanPreview
    {
        public string PlanDate { get; set; }

        public string PlanName { get; set; }
    }
    /// <summary>
    /// 节日营销类型
    /// </summary>
    public class HolidaySysMarketingGroupTypePreview
    {
        public string Name { get; set; }

        public List<LEventTemplatePreview> HolidayLEventTemplatePreview { get; set; }
    }
    /// <summary>
    /// 门店营销类型
    /// </summary>
    public class UnitSysMarketingGroupTypePreview
    {
        public string Name { get; set; }
        public List<LEventTemplatePreview> UnitLEventTemplatePreview { get; set; }
    }
    /// <summary>
    /// 产品营销类型
    /// </summary>
    public class ProductSysMarketingGroupTypePreview
    {
        public string Name { get; set; }
        public List<LEventTemplatePreview> ProductLEventTemplatePreview { get; set; }
    }
    /// <summary>
    /// 主题 
    /// </summary>
    public class LEventTemplatePreview
    {
        public string TemplateName { get; set; }

        public int TemplateStatus { get; set; }
        public string ImageUrl { get; set; }
    }
}