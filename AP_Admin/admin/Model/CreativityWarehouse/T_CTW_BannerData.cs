using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.CreativityWarehouse
{
    /// <summary>
    /// 首页创意仓库（KV管理）
    /// </summary>
    public class T_CTW_BannerData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid AdId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid ActivityGroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid TemplateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BannerImageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BannerUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BannerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// 10=待上架   20=已发布   30=已下架   
        /// </summary>
        public int Status { get; set; }

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

        #region 关联字段
        public string ActivityGroupName { get; set; }
        #endregion

        #endregion
    }
}
