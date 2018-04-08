using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.CreativityWarehouse
{
    /// <summary>
    /// 风格
    /// </summary>
    public class T_CTW_LEventThemeData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid ThemeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid TemplateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ThemeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string H5Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string H5TemplateId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RCodeUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IsDelete { get; set; }
        //关联字段
        public string ImageUrl { get; set; }
        
        #endregion
    }
}
