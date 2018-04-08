using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model.CreativityWarehouse
{
    /// <summary>
    /// 活动主表(游戏)
    /// </summary>
    public class T_CTW_LEventsData
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public string EventID { get; set; }
        public string EventCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int EventLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentEventID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 1=有   0=??有
        /// </summary>
        public int IsSubEvent { get; set; }

        /// <summary>
        /// 10=未开始   20=运行中   30=暂停   40=结束   
        /// </summary>
        public int EventStatus { get; set; }

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
        public int DrawMethodId { get; set; }

        /// <summary>
        /// 000000      从左起第一位：是否需要注册 1=是，0=否   从左起第二位：是否需要签到 1=是，0=否   从左起第三位：是否需要验证 1=是，0=否   从左起第4位：是否需要补充抽奖机会   从左起第5位：是否判断在现场 1=是，0=否   从左起第6位：未开始抽奖时，是否需要提示1=是（提示信息：写死），0=否 （暂时不处理）   从左起第7位：抽  结束后，是否需要提示1=是（提示信息：写死），0=否      （暂时不处理）   从左起第8位：是否可以多次中奖，1=是（无限次），0=否（只能中一次奖）  （暂时不处理）   从左起第8位：是否可以多次中奖，1=是（无限次），0=否（只能中一次奖）  （暂时不处理）   从左起第9位：是否需要填问券，1=是，0=否  （暂时不处理）   
        /// </summary>
        public string EventFlag { get; set; }

        
        #endregion
    }
}
