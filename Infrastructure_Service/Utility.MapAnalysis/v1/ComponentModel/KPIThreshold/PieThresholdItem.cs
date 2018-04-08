using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace JIT.MapAnalysis.v1.ComponentModel
{
    /// <summary>
    /// 渲染阀值
    /// </summary>
    [DataContract]
    public class PieThresholdItem
    {  
        /// <summary>
        /// 开始颜色1 根据顺序对应kpi
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string startcolor { get; set; }

        /// <summary>
        /// 结束颜色1 根据顺序对应kpi
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string endcolor { get; set; }

        /// <summary>
        /// 图例名称
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string legendname { get; set; }
    }
}