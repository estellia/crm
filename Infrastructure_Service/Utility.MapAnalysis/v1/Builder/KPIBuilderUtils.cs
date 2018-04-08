using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.MapAnalysis.v1.ComponentModel;

namespace JIT.Utility.MapAnalysis.v1.Builder
{
    public static class KPIBuilderUtils
    {
        /// <summary>
        /// 根据指定的格式对字符串进行格式化
        /// </summary>
        /// <param name="pOriginalValue">原始数据字符串</param>
        /// <param name="pDataValueFormat">格式化类型</param>
        /// <returns>格式化之后的字符串</returns>
        public static string GetFormatedValue(string pOriginalValue, DataValueFormat pDataValueFormat)
        {
            string result = pOriginalValue;
            double tempValue;
            //处理小数位数格式化
            if ((pDataValueFormat & DataValueFormat.Decimal1) != 0)
            {//转换1位小数
                if (double.TryParse(result, out tempValue))
                    result = tempValue.ToString("f1");
            }

            if ((pDataValueFormat & DataValueFormat.Decimal2) != 0)
            {//转换2位小数
                if (double.TryParse(result, out tempValue))
                    result = tempValue.ToString("f2"); 
            }

            if ((pDataValueFormat & DataValueFormat.Decimal3) != 0)
            {//转换3位小数
                if (double.TryParse(result, out tempValue))
                    result = tempValue.ToString("f3");  
            }

            if ((pDataValueFormat & DataValueFormat.Decimal4) != 0)
            {//转换4位小数
                if (double.TryParse(result, out tempValue))
                    result = tempValue.ToString("f4");
            }

            if ((pDataValueFormat & DataValueFormat.Decimal5) != 0)
            {//转换5位小数
                if (double.TryParse(result, out tempValue))
                    result = tempValue.ToString("f5");
            }

            if ((pDataValueFormat & DataValueFormat.Decimal6) != 0)
            {//转换6位小数
                if (double.TryParse(result, out tempValue))
                    result = tempValue.ToString("f6");
            }

            if ((pDataValueFormat & DataValueFormat.Decimal7) != 0)
            {//转换7位小数
                if (double.TryParse(result, out tempValue))
                    result = tempValue.ToString("f7");
            }

            if ((pDataValueFormat & DataValueFormat.Decimal8) != 0)
            {//转换8位小数
                if (double.TryParse(result, out tempValue))
                    result = tempValue.ToString("f8");
            }

            if ((pDataValueFormat & DataValueFormat.Percentage) != 0)
            {//转换成百分数
                if (double.TryParse(result, out tempValue))
                    result = tempValue.ToString("p");
            }
            if ((pDataValueFormat & DataValueFormat.Permil) != 0)
            {//转换成带千分位的字符串
                if (double.TryParse(result, out tempValue))
                    result = tempValue.ToString("n");
            }
            if ((pDataValueFormat & DataValueFormat.Int) != 0)
            {//转换成整数
                if (result.IndexOf(".") >= 0)
                    result = result.Substring(0,result.IndexOf("."));
            }
            return result;
        }
    }
}
