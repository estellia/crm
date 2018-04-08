using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace JIT.MapAnalysis.v1.ComponentModel
{
    /// <summary>
    /// 2.	面渲染-省，市，县
    /// </summary>
    [DataContract]
    public class KPIFlatData : IKPIData
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public KPIFlatData()
        {
            this.id = string.Empty;
            this.kpi = string.Empty;
            this.kpimark = string.Empty;
            this.kpilabel = string.Empty;
            this.tiptype = "1";
        }
        /// <summary>
        /// 区域标识
        /// </summary>
        [DataMember]
        public string id { get; set; } 

        /// <summary>
        /// KPI值
        /// </summary>
        [DataMember]
        public string kpi { get; set; }

        /// <summary>
        /// kpimark值(直接显示在地图上的值)
        /// </summary>
        [DataMember]
        public string kpimark { get; set; }

        /// <summary>
        /// KPI显示文本
        /// </summary>
        [DataMember]
        public string kpilabel { get; set; }

        /// <summary>
        /// 是否弹出flex提示框
        /// 1:flex弹框
        /// 2:回调js（map_ToolTip_Result）
        /// 其它或属性为空：不处理
        /// </summary>
        [DataMember]
        public string tiptype { get; set; }
    }
}