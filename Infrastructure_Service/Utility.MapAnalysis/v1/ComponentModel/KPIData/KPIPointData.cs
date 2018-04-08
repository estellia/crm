using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace JIT.MapAnalysis.v1.ComponentModel
{
    /// <summary>
    /// 4.	点 (终端)
    /// </summary>
    [DataContract]
    public class KPIPointData : IKPIData
    {
        public KPIPointData()
        {
            this.kpifilter1 = string.Empty;
            this.kpifilter2 = string.Empty;
            this.kpifilter3 = string.Empty;
            this.kpifilter4 = string.Empty;
            this.kpifilter5 = string.Empty;
            this.id = string.Empty;
            this.kpi = string.Empty;
            this.kpilabel = string.Empty;
            this.address = string.Empty;
            this.popheight = string.Empty;
            this.poptitle = string.Empty;
            this.popurl = string.Empty;
            this.popwidth = string.Empty;
            this.title = string.Empty;
            this.poptype = "2";
            this.x = string.Empty;
            this.y = string.Empty;
            this.xytype = 0;
        }
        /// <summary>
        /// 终端标识
        /// </summary>
        [DataMember]
        public string id { get; set; } 

        /// <summary>
        /// 终端名称
        /// </summary>
        [DataMember]
        public string title { get; set; }

        /// <summary>
        /// 终端地址
        /// </summary>
        [DataMember]
        public string address { get; set; }
        
        /// <summary>
        /// 坐标类型(1(gps数据)，2（百度数据），3（google、高德数据）)
        /// </summary>
        [DataMember]
        public int xytype { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [DataMember]
        public string x { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember]
        public string y { get; set; }

        /// <summary>
        /// KPI值
        /// </summary>
        [DataMember]
        public string kpi { get; set; }

        /// <summary>
        /// KPI显示文本
        /// </summary>
        [DataMember]
        public string kpilabel { get; set; }

        /// <summary>
        /// KPI筛选关键字1
        /// </summary>
        [DataMember]
        public string kpifilter1 { get; set; }

        /// <summary>
        /// KPI筛选关键字2
        /// </summary>
        [DataMember]
        public string kpifilter2 { get; set; }

        /// <summary>
        /// KPI筛选关键字3
        /// </summary>
        [DataMember]
        public string kpifilter3 { get; set; }

        /// <summary>
        /// KPI筛选关键字4
        /// </summary>
        [DataMember]
        public string kpifilter4 { get; set; }

        /// <summary>
        /// KPI筛选关键字5
        /// </summary>
        [DataMember]
        public string kpifilter5 { get; set; }

        /// <summary>
        /// 容纳终端信息的IFrame宽度(前台控制)
        /// </summary>
        [DataMember]
        public string popwidth { get; set; }

        /// <summary>
        /// 容纳终端信息的IFrame高度(前台控制)
        /// </summary>
        [DataMember]
        public string popheight { get; set; }

        /// <summary>
        /// IFrame的地址(前台控制)
        /// </summary>
        [DataMember]
        public string popurl { get; set; }

        /// <summary>
        /// IFrame标题(前台控制)
        /// </summary>
        [DataMember]
        public string poptitle { get; set; }

        /// <summary>
        /// 是否弹出提示框
        /// 1:flex弹框
        ///2:回调js（map_Pop_Result）
        /// </summary>
        [DataMember]
        public string poptype { get; set; } 
    }
}