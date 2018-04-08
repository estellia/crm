/*
 * Author		:yong.liu
 * EMail		:yong.liu@jitmarketing.cn
 * Company		:JIT
 * Create On	:11/7/2012 4:36:01 PM
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
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JIT.Utility.ExtensionMethod
{
    /// <summary>
    ///  IEnumerable泛型接口的扩展方法 
    /// </summary>
    public static class IEnumerableExtensionMethods
    {
        #region 判断2个迭代器中的对象是一样的
        /// <summary>
        /// 判断2个迭代器中的对象是一样的
        /// <remarks>
        /// <para>1.Null等于Null.</para>
        /// <para>2.调用者不能为null,否则抛出NullReferenceException</para>
        /// </remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pCaller">调用者</param>
        /// <param name="pTarget">进行比较的对象</param>
        /// <returns></returns>
        public static bool AreEquals<T>(this IEnumerable<T> pCaller, IEnumerable<T> pTarget)
        {
            return IEnumerableExtensionMethods.AreEquals<T>(pCaller, pTarget, null);
        }
        /// <summary>
        /// 判断2个迭代器中的对象是一样的
        /// <remarks>
        /// <para>1.Null等于Null.</para>
        /// <para>2.调用者不能为null,否则抛出NullReferenceException</para>
        /// </remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pCaller">调用者</param>
        /// <param name="pTarget">进行比较的对象</param>
        /// <param name="pComparer">迭代器中元素值比较的比较器</param>
        /// <returns></returns>
        public static bool AreEquals<T>(this IEnumerable<T> pCaller, IEnumerable<T> pTarget, IEqualityComparer<T> pComparer)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            if (pTarget == null)
                return false;
            var e1 = pCaller.GetEnumerator();
            var e2 = pTarget.GetEnumerator();
            while (e1.MoveNext())
            {
                if (e2.MoveNext())
                {
                    if (pComparer != null)
                    {
                        if (!pComparer.Equals(e1.Current, e2.Current))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (e1.Current == null && e2.Current == null)
                        {
                            continue;
                        }
                        else if (e1.Current != null)
                        {
                            if (!e1.Current.Equals(e2.Current))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            //
            return !e2.MoveNext();  //对比完后，e2中也应该没有剩余元素了
        }
        #endregion

        #region 多值分组
        /// <summary>
        /// 扩展方法：多值分组
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="pCaller"></param>
        /// <param name="pGroupSelectors"></param>
        /// <returns></returns>
        public static IEnumerable<GroupResult> GroupByMany<TElement>(this IEnumerable<TElement> pCaller, params Func<TElement, object>[] pGroupSelectors)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            if (pGroupSelectors == null)
                throw new ArgumentNullException("pGroupSelectors");
            if (pGroupSelectors.Length > 0)
            {
                var selector = pGroupSelectors.First();
                var nextSelectors = pGroupSelectors.Skip(1).ToArray();
                //
                return pCaller.GroupBy(selector).Select(
                    g => new GroupResult
                    {
                        Key = g.Key,
                        Count = g.Count(),
                        Items = g,
                        SubGroups = g.GroupByMany(nextSelectors)
                    });
            }
            else
                return null;
        }
        #endregion
    }
}
