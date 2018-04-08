using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.CreativityWarehouse
{
    /// <summary>
    /// 秒杀/团购/抢购/热销
    /// </summary>
    public class T_CTW_PanicbuyingEventData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EventName { get; set; }
        public string EventCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ImageId { get; set; }

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


        #endregion
    }
}
