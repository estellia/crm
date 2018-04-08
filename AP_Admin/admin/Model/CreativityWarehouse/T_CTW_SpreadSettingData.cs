using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.CreativityWarehouse
{
    /// <summary>
    /// 推广 
    /// </summary>
    public class T_CTW_SpreadSettingData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Share 分享   Focus 关注   Reg 注册
        /// </summary>
        public string SpreadType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PromptText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LeadPageSharePromptText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LeadPageFocusPromptText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LeadPageRegPromptText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid TemplateId { get; set; }

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
