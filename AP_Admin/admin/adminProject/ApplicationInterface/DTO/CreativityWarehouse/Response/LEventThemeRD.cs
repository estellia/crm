using adminProject.ApplicationInterface.DTO.Base;
using cPos.Admin.Model.CreativityWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response
{
    public class LEventThemeRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int TotalPageCount { get; set; }
        public IList<T_CTW_LEventThemeData> LEventThemeList { get; set; }

        public LEventThemeInfo LEventThemeInfo { get; set; }
    }

    public class LEventThemeInfo
    {
        public string ThemeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ThemeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageId { get; set; } 

        public string ImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string H5Url { get; set; }

    }
}