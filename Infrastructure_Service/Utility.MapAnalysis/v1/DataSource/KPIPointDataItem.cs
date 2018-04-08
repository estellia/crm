using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MapAnalysis.v1.ComponentModel
{
    public class KPIPointDataItem
    {
        public KPIPointDataItem()
        {
            this.KPIFilter1 = string.Empty;
            this.KPIFilter2 = string.Empty;
            this.KPIFilter3 = string.Empty;
            this.KPIFilter4 = string.Empty;
            this.KPIFilter5 = string.Empty;
            this.StoreID = string.Empty;
            this.KPIData = string.Empty;
            this.KPILabel = string.Empty;
            this.StoreAddress = string.Empty;
            this.PopWindowHeight = "350";
            this.PopWindowTitle = string.Empty;
            this.PopWindowUrl = string.Empty;
            this.PopWindowWidth = "500";
            this.PopWindowType = "2";
            this.StoreName = string.Empty;
            this.Longitude = string.Empty;
            this.Latitude = string.Empty;
            this.GPSType = 3;
        }
        /// <summary>
        /// 终端标识
        /// </summary>
        public string StoreID { get; set; } 

        /// <summary>
        /// 终端名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 终端地址
        /// </summary>
        public string StoreAddress { get; set; } 

        /// <summary>
        /// 坐标类型(1(gps数据)，2（百度数据），3（google、高德数据）)
        /// </summary>
        public int GPSType { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// KPI值
        /// </summary>
        public string KPIData { get; set; }

        /// <summary>
        /// KPI显示文本
        /// </summary>
        public string KPILabel { get; set; }

        /// <summary>
        /// KPI筛选关键字1
        /// </summary>
        public string KPIFilter1 { get; set; }

        /// <summary>
        /// KPI筛选关键字2
        /// </summary>
        public string KPIFilter2 { get; set; }

        /// <summary>
        /// KPI筛选关键字3
        /// </summary>
        public string KPIFilter3 { get; set; }

        /// <summary>
        /// KPI筛选关键字4
        /// </summary>
        public string KPIFilter4 { get; set; }

        /// <summary>
        /// KPI筛选关键字5
        /// </summary>
        public string KPIFilter5 { get; set; }

        /// <summary>
        /// 容纳终端信息的IFrame宽度(前台控制)
        /// </summary>
        public string PopWindowWidth { get; set; }

        /// <summary>
        /// 容纳终端信息的IFrame高度(前台控制)
        /// </summary>
        public string PopWindowHeight { get; set; }

        /// <summary>
        /// IFrame的地址(前台控制)
        /// </summary>
        public string PopWindowUrl { get; set; }

        /// <summary>
        /// IFrame标题(前台控制)
        /// </summary>
        public string PopWindowTitle{ get; set; }

        /// <summary>
        /// 是否弹出提示框
        /// 1:flex弹框
        ///2:回调js（map_Pop_Result）
        /// </summary>
        public string PopWindowType { get; set; } 
    }
}
