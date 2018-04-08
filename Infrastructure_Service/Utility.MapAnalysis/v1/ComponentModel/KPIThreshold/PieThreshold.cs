using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace JIT.MapAnalysis.v1.ComponentModel
{
    /// <summary>
    /// 饼图渲染阀值
    /// </summary>
    [DataContract]
    public class PieThreshold : IKPIDataThreshold
    {
        public PieThreshold()
        {
            type = "10";
        }
        /// <summary>
        /// 地图级别（1省，2市，3县，4点）
        /// </summary>
        [DataMember]
        public string level { get; set; }

        /// <summary>
        /// 渲染类型
        /// （
        /// 直接传入10即可
        ///）
        /// </summary>
        [DataMember]
        public string type { get; set; } 

        /// <summary>
        /// 分段开始值 用于渲染（type=1 有效）
        /// </summary>
        [DataMember]
        public PieThresholdItem[] threshold { get; set; }
    }
}