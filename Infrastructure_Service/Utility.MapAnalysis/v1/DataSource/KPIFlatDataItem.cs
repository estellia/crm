using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MapAnalysis.v1.ComponentModel
{
    public class KPIFlatDataItem
    { 
        public KPIFlatDataItem()
        {
            this.Ignore = false;
        }
        /// <summary>
        /// 数据标识
        /// </summary>
        public string DataID { get; set; } 
        /// <summary>
        /// 饼图分块数据
        /// </summary>
        public string KPIData { get; set; } 
        /// <summary>
        /// 忽略此KPI数据（仅用于计算饼图的大小）
        /// </summary>
        public bool Ignore { get; set; }
    }
}
