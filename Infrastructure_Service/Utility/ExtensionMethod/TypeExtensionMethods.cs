/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/22 13:59:23
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    /// Type的扩展方法
    /// </summary>
    public static class TypeExtensionMethods
    {
        /// <summary>
        /// 扩展方法：判断类型是否为公共语言运行时类型
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static bool IsCRT(this Type pCaller)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            if (pCaller == typeof(bool)
                || pCaller == typeof(byte)
                || pCaller == typeof(char)
                || pCaller == typeof(DateTime)
                || pCaller == typeof(decimal)
                || pCaller == typeof(double)
                || pCaller == typeof(Int16)
                || pCaller == typeof(Int32)
                || pCaller == typeof(Int64)
                || pCaller == typeof(sbyte)
                || pCaller == typeof(Single)
                || pCaller == typeof(string)
                || pCaller == typeof(UInt16)
                || pCaller == typeof(UInt32)
                || pCaller == typeof(UInt64)
            )
                return true;
            else
                return false;
        }

        /// <summary>
        /// 扩展方法：判断类型是否为可空值类型
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static bool IsNullableValueType(this Type pCaller)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            if (pCaller.IsGenericType && pCaller.GetGenericTypeDefinition() == typeof(Nullable<>))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 扩展方法：判断类型是否为基础类型为公共语言运行时类型的可空值类型
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static bool IsNullableCRTValueType(this Type pCaller)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            if (pCaller == typeof(bool?)
                || pCaller == typeof(byte?)
                || pCaller == typeof(char?)
                || pCaller == typeof(DateTime?)
                || pCaller == typeof(decimal?)
                || pCaller == typeof(double?)
                || pCaller == typeof(Int16?)
                || pCaller == typeof(Int32?)
                || pCaller == typeof(Int64?)
                || pCaller == typeof(sbyte?)
                || pCaller == typeof(Single?)
                || pCaller == typeof(UInt16?)
                || pCaller == typeof(UInt32?)
                || pCaller == typeof(UInt64?)
            )
                return true;
            else
                return false;
        }

        /// <summary>
        /// 扩展方法：获取可空值类型的基本类型
        /// </summary>
        /// <param name="pCaller"></param>
        /// <returns></returns>
        public static Type GetNullableUnderlyingType(this Type pCaller)
        {
            if (pCaller.IsNullableValueType())
                return pCaller.GetGenericArguments()[0];
            else
                return null;
        }
    }
}
