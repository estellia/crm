using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.CreativityWarehouse
{
    /// <summary>
    /// 游戏或促销活动方式
    /// </summary>
    public class T_CTW_LEventDrawMethodData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid DrawMethodId { get; set; }

        /// <summary>
        /// 1.吸粉   2.促销
        /// </summary>
        public int InteractionType { get; set; }

        /// <summary>
        /// 红包   大转盘    抢购/秒杀   团购   热销
        /// </summary>
        public string DrawMethodName { get; set; }

        /// <summary>
        /// DZP 大转盘   HB   红包   QN  问卷   QG  抢购/秒杀   TG  团购   RX  热销
        /// </summary>
        public string DrawMethodCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DrawMethodRemark { get; set; }

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
