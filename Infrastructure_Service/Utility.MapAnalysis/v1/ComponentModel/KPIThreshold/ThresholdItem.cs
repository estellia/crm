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
    public class ThresholdItem
    {
        public ThresholdItem()
        {
            size = "20";
            alpha = "1";
            color = string.Empty;
        }
        /// <summary>
        /// 分段开始值 用于渲染（type=1 有效）
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string start { get; set; }

        /// <summary>
        /// 分段开始显示值 用于显示（type=1 有效）
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string startlabel { get; set; }

        /// <summary>
        /// 分段结束值 用于渲染（type=1 有效）
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string end { get; set; }

        /// <summary>
        /// 分段结束显示值 用于显示
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string endlabel { get; set; }

        /// <summary>
        /// 分段颜色 如0xFF0000
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string color { get; set; }

        /// <summary>
        /// 气泡大小 气泡“SB”时需要
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string size { get; set; }

        /// <summary>
        /// 透明度(0.00-1.00，不适用图片渲染)
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string alpha { get; set; }

        /// <summary>
        /// 图片 点
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling= NullValueHandling.Ignore)]
        public string image { get; set; }


        /// <summary>
        /// 单值渲染值 用于渲染（type=2 有效）
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string text { get; set; }
        /// <summary>
        /// 单值渲染显示值 用于显示（type=2 有效）
        /// </summary>
        [DataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string textlabel { get; set; }
    }
}