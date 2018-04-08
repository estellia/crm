using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.CreativityWarehouse
{
    /// <summary>
    /// 互动（游戏或促销）
    /// </summary>
    public class T_CTW_LEventInteractionData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid InteractionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid TemplateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid ThemeId { get; set; }

        /// <summary>
        /// 1.吸粉   2.促销
        /// </summary>
        public int InteractionType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid DrawMethodId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LeventId { get; set; }

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
        public string DrawMethodName { get; set; }

        public string EventName { get; set; }
        public string Title { get; set; }
        public string ActivityName { get; set; }

        public string ThemeName { get; set; }
        #endregion
    }
}
