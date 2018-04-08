using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.ExtensionMethod
{
    public static class GenericsExtensionMethods
    {

        /// <summary>
        /// 获取集合中指定起始位置和结束位置的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="startindex">起始位置</param>
        /// <param name="pEndindex">结束位置（不包含）</param>
        /// <returns></returns>
        public static List<T> GetSubs<T>(this List<T> pValue, int pStartindex, int pEndindex)
        {
            List<T> list = new List<T> { };
            for (int i = pStartindex; i < pEndindex; i++)
            {
                list.Add(pValue[i]);
            }
            return list;
        }
    }
}
