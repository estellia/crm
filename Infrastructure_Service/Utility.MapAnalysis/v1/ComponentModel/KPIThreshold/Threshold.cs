using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace JIT.MapAnalysis.v1.ComponentModel
{
    /// <summary>
    /// 渲染阀值 点，面渲染(SA,SB) 
    /// </summary>
    [DataContract]
    public class Threshold : IKPIDataThreshold
    {
        public Threshold()
        {
            defaulttext = "无数据";
            defaultsize = "20";
            defaultcolor = "0x000000";
            defaultimage = string.Empty;
            defaultalpha = "1";
        }
        /// <summary>
        /// 地图级别（1省，2市，3县，4点）
        /// </summary>
        [DataMember]
        public string level { get; set; }

        /// <summary>
        /// 渲染类型
        /// （
        /// 1面分段颜色渲染，
        ///2面单值颜色渲染，
        ///3点分段颜色渲染
        ///4点单值颜色渲染，
        ///5点分段图片渲染，
        ///6点单指图片渲染
        ///）
        /// </summary>
        [DataMember]
        public string type { get; set; }

        /// <summary>
        /// 默认颜色 分类之外的值的颜色
        /// </summary>
        [DataMember]
        public string defaultcolor { get; set; }

        /// <summary>
        /// 默认显示值 分类之外的值的显示，如为空，图例不显示
        /// </summary>
        [DataMember]
        public string defaulttext { get; set; }

        /// <summary>
        /// 气泡默认大小 气泡“SB”时需要，整数，不超过40
        /// </summary>
        [DataMember]
        public string defaultsize { get; set; }

        /// <summary>
        /// 透明度(0.00-1.00，不适用图片渲染)
        /// </summary>
        [DataMember]
        public string defaultalpha { get; set; }

        /// <summary>
        /// 图片 点
        /// </summary>
        [DataMember]
        public string defaultimage { get; set; }

        /// <summary>
        /// 分段开始值 用于渲染（type=1 有效）
        /// </summary>
        [DataMember]
        public ThresholdItem[] threshold { get; set; }
    }
}