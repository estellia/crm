using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace JIT.MapAnalysis.v1.ComponentModel
{
    /// <summary>
    /// 3.	饼渲染-省，市，县
    /// </summary>
    [DataContract]
    public class KPIPieData : IKPIData
    {
        /// <summary>
        /// 区域标识
        /// </summary>
        [DataMember]
        public string id { get; set; }         

        /// <summary>
        /// 饼图大小
        /// </summary>
        [DataMember]
        public int piesize { get; set; }

        /// <summary>
        /// KPI1值
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string kpi1 { get; set; }

        /// <summary>
        /// KPI1显示文本
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string kpilabel1 { get; set; } 

        /// <summary>
        /// KPI2值
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string kpi2 { get; set; }

        /// <summary>
        /// KPI2显示文本
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string kpilabel2 { get; set; } 

        /// <summary>
        /// KPI3值
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string kpi3 { get; set; }

        /// <summary>
        /// KPI3显示文本
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string kpilabel3 { get; set; } 

        /// <summary>
        /// KPI4值
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string kpi4 { get; set; }

        /// <summary>
        /// KPI4显示文本
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string kpilabel4 { get; set; } 

        /// <summary>
        /// KPI5值
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string kpi5 { get; set; }

        /// <summary>
        /// KPI5显示文本
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string kpilabel5 { get; set; } 
    }
}