using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.MethodExtensions.StringExtensions
{
    public static class StringMethodExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool ToBool(this string boolString)
        {
            var result = false;
            try
            {
                boolString = boolString.ToLower();
                if (boolString == "false")
                {
                    result = false;
                }
                else if (boolString == "true")
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("方法ToBool出错:" + boolString + "," + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Is null/empty/whitespace ?
        /// </summary>
        public static bool IsNullStr(this string str)
        {
            bool result = false;
            try
            {
                result = string.IsNullOrWhiteSpace(str);
            }
            catch (Exception ex)
            {
                //throw new Exception("方法:IsNullStr()出错.", ex);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// List  /T/   ---->   string
        /// </summary>
        public static string IDsToStr<T>(this List<T> ids, string symbol = ",")
            where T : struct
        {
            var result = string.Empty;
            try
            {
                if (ids == null || ids.Count <= 0)
                {
                    return result;
                }

                result = string.Join<T>(symbol, ids);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:IDsToStr()出错.", ex);
            }
            return result;
        }

        /// <summary>
        /// string   ---->   List  / T /
        /// </summary>
        public static List<T> StrToIDs<T>(this string idsStr, Converter<string, T> converter, char symbol = ',')
            where T : struct
        {
            var result = default(List<T>);

            try
            {
                if (idsStr.IsNullStr())
                {
                    return result;
                }

                var newStr = idsStr.Trim();

                if (string.IsNullOrWhiteSpace(newStr))
                {
                    return result;
                }

                var idArry = newStr.Split(symbol);

                if (idArry.Count() <= 0)
                {
                    return result;
                }

                var newArry = Array.ConvertAll(idArry, converter);

                if (newArry.Count() <= 0)
                {
                    return result;
                }

                result = new List<T>(newArry);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:StrToIDs<T>(this string idsStr, char symbol = ',')出错.", ex);
            }

            return result;
        }
    }
}
