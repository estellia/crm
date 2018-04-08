using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace JIT.Utility.MapAnalysis.v1.ComponentModel
{
    /// <summary>
    /// KPI数据层级
    /// </summary>
    public enum BoundLevel
    {
        [Description("省")]
        Province=1,
        [Description("市")]
        City=2,
        [Description("区县")]
        District=3,
        [Description("终端点")]
        Point=4
    }

    /// <summary>
    /// KPI数据显示格式
    /// </summary>
    [Flags]
    public enum DataValueFormat
    {
        [Description("显示百分号格式")]
        Percentage=1,
        [Description("数值转换成整数")]
        Int=2,
        [Description("显示千分号")]
        Permil=4,
        [Description("显示一位小数")]
        Decimal1=8,
        [Description("显示两位小数")]
        Decimal2 = 16,
        [Description("显示三位小数")]
        Decimal3 = 32,
        [Description("显示四位小数")]
        Decimal4 = 64,
        [Description("显示五位小数")]
        Decimal5 = 128,
        [Description("显示六位小数")]
        Decimal6 = 256,
        [Description("显示七位小数")]
        Decimal7 = 512,
        [Description("显示八位小数")]
        Decimal8 = 1024
    }

    /// <summary>
    /// 阀值类型（值，类型）
    /// </summary>
    public enum ThresholdType
    {
        [Description("按数值范围")]
        Value = 1,
        [Description("按类型（如：渠道）")]
        Type = 2
    }
}
