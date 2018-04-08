using adminProject.ApplicationInterface.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response
{
    public class LEventDropDownRD : IAPIResponseData
    {
        public List<LEventDrawMethodInfo> LEventDrawMethodInfoList { get; set; }
        //public List<PanicbuyingEventInfo> PanicbuyingEventInfoList { get; set; }
        //public List<LEventsInfo> LEventsInfoList { get; set; }
    }

    public class LEventDrawMethodInfo
    {
        public string DrawMethodId { get; set; }
        public int InteractionType { get; set; }

        /// <summary>
        /// 红包   大转盘    抢购/秒杀   团购   热销
        /// </summary>
        public string DrawMethodName { get; set; }

        /// <summary>
        /// DZP 大转盘   HB   红包   QN  问卷   QG  抢购/秒杀   TG  团购   RX  热销
        /// </summary>
        public string DrawMethodCode { get; set; } 
    }
    /// <summary>
    /// 促销具体活动
    /// </summary>
    //public class PanicbuyingEventInfo
    //{
    //    public string EventId { get; set; }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string EventName { get; set; }
    //    public string EventCode { get; set; }
    //}
    ///// <summary>
    ///// 吸粉具体游戏
    ///// </summary>
    //public class LEventsInfo
    //{
    //    public string EventID { get; set; }
    //    public string EventCode { get; set; }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string Title { get; set; }
    //}
}