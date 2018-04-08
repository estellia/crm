using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    /// Enum的扩展方法
    /// </summary>
    public static class EnumExtensionMethods
    {
        /// <summary>
        /// 扩展方法:获取枚举的描述
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static string GetDescription(this Enum pCaller)
        {
            //获取枚举上的描述特性
            Type t = pCaller.GetType();
            DescriptionAttribute att = null;
            FieldInfo fi = t.GetField(Enum.GetName(t, pCaller));
            att = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));
            //如果有描述特性且值不为空则返回描述否则返回枚举的值
            if (att != null && string.IsNullOrEmpty(att.Description) == false)
            {
                return att.Description;
            }
            else
            {
                return pCaller.ToString();
            }
        }

        /// <summary>
        /// 扩展方法：获取指定枚举的所有值
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetValues(this Enum pCaller)
        {
            Type t = pCaller.GetType();
            //获取枚举的静态公共成员
            FieldInfo[] fields = t.GetFields(BindingFlags.Static | BindingFlags.Public);
            List<Enum> members = new List<Enum>();
            foreach (var field in fields)
            {
                members.Add((Enum)field.GetValue(pCaller));
            }
            //
            return members;
        }

        /// <summary>
        /// 扩展方法：获取下一个枚举
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <param name="pOrderOption">排序的方式</param>
        /// <param name="pIsCyclely">是否循环获取</param>
        /// <returns></returns>
        public static Enum GetNext(this Enum pCaller, OrderOptions pOrderOption, bool pIsCyclely)
        {
            Enum[] values = pCaller.GetValues().ToArray();
            if (pOrderOption == OrderOptions.ByValue)
                values = values.OrderBy(item => item).ToArray();
            var next = values.SkipWhile(item => Convert.ToInt32(item) != Convert.ToInt32(pCaller)).Skip(1).FirstOrDefault();
            if (next == null && pIsCyclely)
            {
                next = values.First();
            }
            return next;
        }

        /// <summary>
        /// 扩展方法：获取下一个枚举值,如果当前已经是最大值,则返回null
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static Enum GetNext(this Enum pCaller)
        {
            return pCaller.GetNext(OrderOptions.ByValue, false);
        }

        /// <summary>
        /// 扩展方法：获取上一个枚举
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <param name="pOrderOption">排序的方式</param>
        /// <param name="pIsCyclely">是否循环获取</param>
        /// <returns></returns>
        public static Enum GetPrevious(this Enum pCaller, OrderOptions pOrderOption, bool pIsCyclely)
        {
            var values = pCaller.GetValues();
            if (pOrderOption == OrderOptions.ByValue)
                values = values.OrderBy(item => item);
            values = values.Reverse();
            var next = values.SkipWhile(item => Convert.ToInt32(item) != Convert.ToInt32(pCaller)).Skip(1).FirstOrDefault();
            if (next == null && pIsCyclely)
            {
                next = values.First();
            }
            return next;
        }

        /// <summary>
        /// 扩展方法：获取上一个枚举值,如果当前枚举值已经是最小值,则返回null
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <returns></returns>
        public static Enum GetPrevious(this Enum pCaller)
        {
            return pCaller.GetPrevious(OrderOptions.ByValue, false);
        }

        /// <summary>
        /// 扩展方法：获取枚举中的第一个值
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <param name="pOrderOption">排序的方式</param>
        /// <returns></returns>
        public static Enum GetFirst(this Enum pCaller, OrderOptions pOrderOption)
        {
            var values = pCaller.GetValues();
            if (pOrderOption == OrderOptions.ByValue)
                values = values.OrderBy(item => item);
            return values.First();
        }

        /// <summary>
        /// 扩展方法：获取枚举中最后一个值
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <param name="pOrderOption">排序的方式</param>
        /// <returns></returns>
        public static Enum GetLast(this Enum pCaller, OrderOptions pOrderOption)
        {
            var values = pCaller.GetValues();
            if (pOrderOption == OrderOptions.ByValue)
                values = values.OrderBy(item => item);
            return values.Last();
        }
    }
}
