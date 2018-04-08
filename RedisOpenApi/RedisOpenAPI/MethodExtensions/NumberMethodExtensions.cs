using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.MethodExtensions.NumberExtensions
{
    public static class NumberMethodExtensions
    {

        /// <summary>
        /// string ---> short
        /// </summary>
        public static short ToShort(this string numStr)
        {
            short result = 0;
            try
            {
                result = Convert.ToInt16(numStr);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:ToShort(),转换Int16格式出错.", ex);
            }
            return result;
        }

        /// <summary>
        /// int ---> short   /   !!! 长变短
        /// </summary>
        public static short ToShort(this int intNum)
        {
            short result = 0;
            try
            {
                result = Convert.ToInt16(intNum);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:ToShort(),转换Int16格式出错.", ex);
            }
            return result;
        }

        /// <summary>
        /// int? ---> int
        /// </summary>
        public static int ToInt(this int? intValue)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(intValue);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:ToInt()出错.", ex);
            }
            return result;
        }

        /// <summary>
        /// long ---> int   /   !!! 长变短
        /// </summary>
        public static int ToInt(this long longValue)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(longValue);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:ToInt()出错.", ex);
            }
            return result;
        }

        /// <summary>
        /// string --->  int
        /// </summary>
        public static int ToInt(this string numStr)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(numStr);
            }
            catch (Exception ex)
            {
                throw new Exception("方法:ToInt(),转换Int32格式出错.", ex);
            }
            return result;
        }

        /// <summary>
        /// string ---> long
        /// </summary>
        public static long ToLong(this string numStr)
        {
            long result = 0;
            try
            {
                result = Convert.ToInt64(numStr);
            }
            catch (Exception ex)
            {
                //LogHelper.OrderLogger.WriteError(string.Format("string转long失败:str={0}", numStr), ex, "0200");
                throw new Exception("方法:ToLong()出错", ex);
            }
            return result;
        }

        /// <summary>
        /// object ---> long 
        /// </summary>
        public static long ToLong(this object numObj)
        {
            long result = 0;
            try
            {
                result = Convert.ToInt64(numObj);
            }
            catch (Exception ex)
            {
                //LogHelper.OrderLogger.WriteError(string.Format("object转long失败:obj={0}", numObj.ToString()), ex, "0200");
                throw new Exception("方法:ToLong()出错", ex);
            }
            return result;
        }

    }
}
