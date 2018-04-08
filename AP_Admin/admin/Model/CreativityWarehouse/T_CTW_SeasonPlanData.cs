using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.CreativityWarehouse
{
    /// <summary>
    /// 首页计划活动管理
    /// </summary>
    public class T_CTW_SeasonPlanData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid SeasonPlanId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PlanDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PlanName { get; set; }

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


        #endregion
    }
}
