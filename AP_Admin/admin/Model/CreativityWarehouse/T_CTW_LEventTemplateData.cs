using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.CreativityWarehouse
{
    /// <summary>
    /// 主题
    /// </summary>
    public class T_CTW_LEventTemplateData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid TemplateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TemplateDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid ActivityGroupId { get; set; }

        /// <summary>
        /// 10=待上架   20=待发布   30=已发布   40=已下架
        /// </summary>
        public int TemplateStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 0.不是   1.是
        /// </summary>
        public int IsLongTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int DisplayIndex { get; set; }

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
        /// <summary>
        /// 营销活动类型名称
        /// </summary>
        public string ActivityGroupName { get; set; }
        /// <summary>
        /// 风格二维码路径
        /// </summary>
        public string RCodeUrl { get; set; }
        #endregion
    }
}
