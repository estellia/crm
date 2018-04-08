using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.CreativityWarehouse
{
    /// <summary>
    /// 营销活动类型
    /// </summary>
    public class SysMarketingGroupTypeData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid ActivityGroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        public string ActivityGroupCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

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
