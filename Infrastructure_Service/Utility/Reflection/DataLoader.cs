/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/20 13:40:32
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
using System.Collections.Specialized;
using System.Data;
using System.Text;

using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Reflection
{
    /// <summary>
    /// 数据装载器 
    /// </summary>
    public static class DataLoader
    {
        #region 从DataTable中装载数据到对象
        /// <summary>
        /// 从DataTable中装载数据到对象
        /// </summary>
        /// <typeparam name="T">T必须是一个类，而且必须带有无参构造函数</typeparam>
        /// <param name="pDataes">数据集</param>
        /// <param name="pPropertyNameMapping">属性名映射</param>
        /// <returns>T数组</returns>
        public static T[] LoadFrom<T>(DataTable pDataes, IPropertyNameMapping pPropertyNameMapping) where T : class,new()
        {
            //参数处理
            if (pPropertyNameMapping == null)
                throw new ArgumentNullException("pPropertyNameMapping");
            if (pDataes == null)
                return null;
            if (pDataes.Rows.Count <= 0)
                return new T[0];
            //
            Type tType = typeof(T);
            List<T> list = new List<T>();
            Dictionary<string, SetValueDelegate> propertySetters = new Dictionary<string, SetValueDelegate>();
            //获取需要进行数据赋值的属性设置器
            foreach (DataColumn column in pDataes.Columns)
            {
                var columnName = column.ColumnName;
                var propertyName = pPropertyNameMapping.GetPropertyNameBy(columnName);
                if (!string.IsNullOrEmpty(propertyName))
                {
                    var propertyInfo = tType.GetProperty(columnName);
                    if (propertyInfo != null)
                    {
                        var setter = DynamicMethodFactory.CreatePropertySetter(propertyInfo);
                        if (setter != null)
                            propertySetters.Add(columnName, setter);
                    }
                }
            }
            //将数据从DataTable中装载到T中
            foreach (DataRow dr in pDataes.Rows)
            {
                //创建实例
                T t = (T)Activator.CreateInstance(tType);
                //给实例赋值
                foreach (var item in propertySetters)
                {
                    var setter = item.Value;
                    try
                    {
                        if (dr[item.Key] != DBNull.Value)
                        {
                            setter(t, dr[item.Key]);
                        }
                        //else
                        //{
                        //    setter(t, null);
                        //}
                    }
                    catch (Exception ex)
                    {
                        string errorMessage = string.Format("DataLoader设置值失败,数据列名[{0}],值[{1}].内部错误:[{2}]", item.Key, item.Value, ex.Message);
                        throw new ArgumentException(errorMessage, ex);
                    }
                }
                //将实例添加进结果集
                list.Add(t);
            }
            //返回结果
            return list.ToArray();
        }

        /// <summary>
        /// 从DataTable中装载数据(属性映射采用列名与属性名的直接匹配)
        /// </summary>
        /// <typeparam name="T">T必须是一个类，而且必须带有无参构造函数</typeparam>
        /// <param name="pDataes">数据集</param>
        /// <returns>T数组</returns>
        public static T[] LoadFrom<T>(DataTable pDataes) where T : class,new()
        {
            return DataLoader.LoadFrom<T>(pDataes, new DirectPropertyNameMapping());
        }

        /// <summary>
        /// 从DataTable中装载数据
        /// </summary>
        /// <typeparam name="T">T必须是一个类，而且必须带有无参构造函数</typeparam>
        /// <param name="pDataes">数据集</param>
        /// <param name="pSourceNames">源名称数组</param>
        /// <param name="pPropertyNames">对应的属性名称数组</param>
        /// <returns>T数组</returns>
        public static T[] LoadFrom<T>(DataTable pDataes, string[] pSourceNames, string[] pPropertyNames) where T : class,new()
        {
            var mapping = new DictionaryPropertyNameMapping();
            if (pSourceNames != null && pSourceNames.Length > 0
                && pPropertyNames != null && pPropertyNames.Length > 0)
            {
                for (int i = 0; i < pSourceNames.Length && i < pPropertyNames.Length; i++)
                {
                    mapping.Add(pSourceNames[i], pPropertyNames[i]);
                }
            }
            //
            return DataLoader.LoadFrom<T>(pDataes, mapping);
        }

        /// <summary>
        /// 从DataTable中装载数据
        /// </summary>
        /// <typeparam name="T">T必须是一个类，而且必须带有无参构造函数</typeparam>
        /// <param name="pDataes">数据集</param>
        /// <param name="pMappings">源名称与属性名的映射字典,字典的KEY是源名称,字典的VALUE是属性名称</param>
        /// <returns>T数组</returns>
        public static T[] LoadFrom<T>(DataTable pDataes, Dictionary<string, string> pMappings) where T : class,new()
        {
            var mapping = new DictionaryPropertyNameMapping(pMappings);
            return DataLoader.LoadFrom<T>(pDataes, mapping);
        }
        #endregion

        #region 从HTTP上下文的名称值对中装载数据到对象
        /// <summary>
        /// 从HTTP上下文的名称值对中装载数据到对象
        /// <remarks>
        /// <para>需要将名称值对中的值(string类型)转型为对象属性的类型，当前对象属性的类型只支持：</para>
        /// <para>1.string</para>
        /// <para>2.值类型(基础值类型+GUID+Enum)</para>
        /// <para>3.可空值类型</para>
        /// <para>如果不是以上类型，则会抛出转型无效的异常</para>
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">T必须是一个类，而且必须带有无参构造函数</typeparam>
        /// <param name="pDataes">数据集</param>
        /// <param name="pPropertyNameMapping">属性名映射</param>
        /// <param name="pInstance">T实例,如果实例为null则自动创建一个T实例</param>
        /// <returns></returns>
        public static T LoadFrom<T>(NameValueCollection pDataes, IPropertyNameMapping pPropertyNameMapping, T pInstance = null) where T : class,new()
        {
            //参数处理
            if (pPropertyNameMapping == null)
                throw new ArgumentNullException("pPropertyNameMapping");
            if (pDataes == null)
                return null;
            //
            Type tType = typeof(T);
            if (pInstance == null)
                pInstance = (T)Activator.CreateInstance(tType);
            //获取需要进行数据赋值的属性设置器
            foreach (string key in pDataes.Keys)
            {
                var propertyName = pPropertyNameMapping.GetPropertyNameBy(key);
                if (!string.IsNullOrEmpty(propertyName))//存在属性映射
                {
                    var propertyInfo = tType.GetProperty(propertyName);
                    if (propertyInfo != null)//对象中存在指定名称的属性
                    {
                        var setter = DynamicMethodFactory.CreatePropertySetter(propertyInfo);
                        if (setter != null) //属性存在设置器
                        {
                            var value = pDataes[key];
                            try
                            {
                                setter(pInstance, value.ChangeTypeTo(propertyInfo.PropertyType));
                            }
                            catch (Exception ex)
                            {
                                string errorMessage = string.Format("DataLoader设置值失败,数据键名[{0}],值[{1}].内部错误:[{2}]", key, value, ex.Message);
                                throw new ArgumentException(errorMessage, ex);
                            }
                        }
                    }
                }
            }
            //返回值
            return pInstance;
        }

        /// <summary>
        /// 从HTTP上下文的名称值对中装载数据到对象(属性映射采用键名与属性名的直接匹配)
        /// <remarks>
        /// <para>需要将名称值对中的值(string类型)转型为对象属性的类型，当前对象属性的类型只支持：</para>
        /// <para>1.string</para>
        /// <para>2.值类型(基础值类型+GUID+Enum)</para>
        /// <para>3.可空值类型</para>
        /// <para>如果不是以上类型，则会抛出转型无效的异常</para>
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">T必须是一个类，而且必须带有无参构造函数</typeparam>
        /// <param name="pDataes">数据集</param>
        /// <param name="pInstance">T实例,如果实例为null则自动创建一个T实例</param>
        /// <returns></returns>
        public static T LoadFrom<T>(NameValueCollection pDataes, T pInstance = null) where T : class,new()
        {
            return DataLoader.LoadFrom<T>(pDataes, new DirectPropertyNameMapping(), pInstance);
        }

        /// <summary>
        /// 从HTTP上下文的名称值对中装载数据到对象
        /// </summary>
        /// <typeparam name="T">T必须是一个类，而且必须带有无参构造函数</typeparam>
        /// <param name="pDataes">数据集</param>
        /// <param name="pSourceNames">源名称数组</param>
        /// <param name="pPropertyNames">对应的属性名称数组</param>
        /// <returns></returns>
        public static T LoadFrom<T>(NameValueCollection pDataes, string[] pSourceNames, string[] pPropertyNames) where T : class,new()
        {
            var mapping = new DictionaryPropertyNameMapping();
            if (pSourceNames != null && pSourceNames.Length > 0
                && pPropertyNames != null && pPropertyNames.Length > 0)
            {
                for (int i = 0; i < pSourceNames.Length && i < pPropertyNames.Length; i++)
                {
                    mapping.Add(pSourceNames[i], pPropertyNames[i]);
                }
            }
            //
            return DataLoader.LoadFrom<T>(pDataes, mapping);
        }

        /// <summary>
        /// 从HTTP上下文的名称值对中装载数据到对象
        /// </summary>
        /// <typeparam name="T">T必须是一个类，而且必须带有无参构造函数</typeparam>
        /// <param name="pDataes">数据集</param>
        /// <param name="pMappings">源名称与属性名的映射字典,字典的KEY是源名称,字典的VALUE是属性名称</param>
        /// <param name="pInstance">T实例,如果实例为null则自动创建一个T实例</param>
        /// <returns></returns>
        public static T LoadFrom<T>(NameValueCollection pDataes, Dictionary<string, string> pMappings, T pInstance = null) where T : class,new()
        {
            var mapping = new DictionaryPropertyNameMapping(pMappings);
            return DataLoader.LoadFrom<T>(pDataes, mapping, pInstance);
        }

        #endregion
    }
}
