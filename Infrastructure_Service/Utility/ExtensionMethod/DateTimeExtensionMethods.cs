using System;
using System.Collections.Generic;
using System.Text;

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    /// DateTime的扩展方法
    /// </summary>
    public static class DateTimeExtensionMethods
    {
        /// <summary>
        /// 扩展方法：计算日期是年内的第几周,一周从星期天开始
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static int GetWeeksOfYear(this DateTime pCaller)
        {
            return pCaller.GetWeeksOfYear(DayOfWeek.Sunday);
        }

        /// <summary>
        /// 扩展方法：计算日期是年内的第几周
        /// </summary>
        /// <param name="pCaller">调用发起者</param>
        /// <param name="pFirstDayOfWeek">星期几算是一周的第一天</param>
        /// <returns></returns>
        public static int GetWeeksOfYear(this DateTime pCaller,DayOfWeek pFirstDayOfWeek)
        {
            var start = new DateTime(pCaller.Year, 1, 1);
            var margin = pCaller.DayOfYear;
            var firstWeekDays = (int)pFirstDayOfWeek - (int)start.DayOfWeek;//年内第一周的天数
            if (firstWeekDays <= 0)
                firstWeekDays += 7;
            if (margin <= firstWeekDays)
                return 1;
            else
            {
                int remainer = 0;
                int divisor = Math.DivRem(margin - firstWeekDays, 7, out remainer);
                return 1 + divisor + (remainer > 0 ? 1 : 0);
            }
        }

        /// <summary>
        /// 扩展方法：计算日期是月内的第几周
        /// </summary>
        /// <param name="pCaller">调用发起者</param>
        /// <param name="pFirstDayOfWeek">星期几算是一周的第一天</param>
        /// <returns></returns>
        public static int GetWeeksOfMonth(this DateTime pCaller, DayOfWeek pFirstDayOfWeek)
        {
            var start = new DateTime(pCaller.Year, pCaller.Month, 1);
            var margin = pCaller.Day;
            var firstWeekDays = (int)pFirstDayOfWeek - (int)start.DayOfWeek;//月内第一周的天数
            if (firstWeekDays <= 0)
                firstWeekDays += 7;
            if (margin <= firstWeekDays)
                return 1;
            else
            {
                int remainer = 0;
                int divisor = Math.DivRem(margin - firstWeekDays, 7, out remainer);
                return 1 + divisor + (remainer > 0 ? 1 : 0);
            }
        }

        /// <summary>
        /// 扩展方法：获取指定日期所属月份的天数
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static int GetDaysOfMonth(this DateTime pCaller)
        {
            switch (pCaller.Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    {
                        var year = pCaller.Year;
                        if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0))
                            return 29;
                        else
                            return 28;
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 扩展方法：获取时间字符串,字符串的格式为yyyyMMddHHmmssfff
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string ToSerialString(this DateTime pCaller)
        {
            return pCaller.ToString("yyyyMMddHHmmssfff");
        }

        /// <summary>
        /// 扩展方法：获取SQL语句格式的时间字符串,字符串的格式为yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string ToSQLFormatString(this DateTime pCaller)
        {
            return pCaller.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 扩展方法：JIT统一使用的时间字符串格式
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string ToJITFormatString(this DateTime pCaller)
        {
            return pCaller.ToString("yyyy-MM-dd HH:mm:ss fff");
        }

        /// <summary>
        /// 扩展方法：JIT统一使用的时间字符串格式
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static string To19FormatString(this DateTime pCaller)
        {
            return pCaller.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}
