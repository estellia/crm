using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaiYunMessageService
{
    public class PhoneNOHelper
    {
        /// <summary>
        /// 是否是移动号码
        /// </summary>
        /// <param name="NO"></param>
        /// <returns></returns>
        public static bool IsCMCC(string NO)
        {
            return RegexHelper.CMCCRegex.IsMatch(NO);
        }

        /// <summary>
        /// 是否是联通号码
        /// </summary>
        /// <param name="NO"></param>
        /// <returns></returns>
        public static bool IsCUCC(string NO)
        {
            return RegexHelper.CUCCRegex.IsMatch(NO);
        }

        /// <summary>
        /// 是否是电信号码
        /// </summary>
        /// <param name="NO"></param>
        /// <returns></returns>
        public static bool IsCTCC(string NO)
        {
            return RegexHelper.CTCCRegex.IsMatch(NO);
        }

        /// <summary>
        /// 是否是手机号码
        /// </summary>
        /// <param name="NO"></param>
        /// <returns></returns>
        public static bool IsPhoneNo(string NO)
        {
            return RegexHelper.PhoneRegex.IsMatch(NO);
        }
    }
}
