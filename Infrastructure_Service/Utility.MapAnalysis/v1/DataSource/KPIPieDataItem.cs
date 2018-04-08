using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MapAnalysis.v1.ComponentModel
{
    /// <summary>
    /// 原始饼图数据项
    /// </summary>
    public class KPIPieDataItem
    {
        public KPIPieDataItem()
        {
            this.Ignore = false;
        }
        /// <summary>
        /// 数据标识
        /// </summary>
        public string DataID { get; set; }
        /// <summary>
        /// 影响饼图大小的基数
        /// </summary>
        public string PieSizeData { get; set; }
        /// <summary>
        /// 饼图分块数据1
        /// </summary>
        public string KPIData1 { get; set; }
        /// <summary>
        /// 饼图分块数据2
        /// </summary>
        public string KPIData2 { get; set; }
        /// <summary>
        /// 饼图分块数据3
        /// </summary>
        public string KPIData3 { get; set; }
        /// <summary>
        /// 饼图分块数据4
        /// </summary>
        public string KPIData4 { get; set; }
        /// <summary>
        /// 饼图分块数据5
        /// </summary>
        public string KPIData5 { get; set; }
        /// <summary>
        /// 忽略此KPI数据（仅用于计算饼图的大小）
        /// </summary>
        public bool Ignore { get; set; }
    }
}
