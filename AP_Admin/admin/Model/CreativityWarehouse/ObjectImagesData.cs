using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.CreativityWarehouse
{
    /// <summary>
    /// 图片
    /// </summary>
    public class ObjectImagesData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ObjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? DisplayIndex { get; set; }

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
        public string LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BatId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? RuleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RuleContent { get; set; }


        #endregion
    }
}
