using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JIT.Utility.SMS.Util
{
    class RegexHelper
    {
        //手机号正则表达式
        public static Regex PhoneRegex = new Regex(@"1[3,4,5,8]\d{9}");

        //中国电信
        public static Regex CTCCRegex = new Regex(@"((133)|(153)|(18[0,1,9]))\d{8}");

        //中国移动
        public static Regex CMCCRegex = new Regex(@"((13[4,5,6,7,8,9])|(147)|(15[0,1,2,7,8,9])|(18[2,3,4,7,8]))\d{8}");

        //中国联通
        public static Regex CUCCRegex = new Regex(@"((13[0,1,2])|(145)|(15[5,6])|(186))\d{8}");
    }
}
