using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JIT.Utility
{
    /// <summary>
    /// 反射工具
    /// <remarks>
    /// <para>1.包含一组用于反射的静态工具方法</para>
    /// </remarks>
    /// </summary>
    public static class ReflectionUtils
    {
        #region 从对象中获取指定属性(属性不是索引属性)的值
        /// <summary>
        /// 从对象中获取指定属性(属性不是索引属性)的值
        /// <remarks>
        /// <para>特殊处理：</para>
        /// <para>1.如果对象为null,则直接返回null.</para>
        /// </remarks>
        /// </summary>
        /// <param name="pTarget"></param>
        /// <param name="pPropertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(object pTarget, string pPropertyName)
        {
            if (pTarget == null)
                return null;
            Type t = pTarget.GetType();
            PropertyInfo pi = t.GetProperty(pPropertyName);
            return pi.GetValue(pTarget, null);
        }
        #endregion

        #region 获取嵌入的资源文件
        /// <summary>
        /// 获取嵌入的资源文件
        /// <remarks>
        /// <para>1.资源文件必须是在调用本方法的客户端代码所处的程序集中</para>
        /// </remarks>
        /// </summary>
        /// <param name="pResourceName">资源名称,为资源所属的 命名空间+目录名+文件名</param>
        /// <returns></returns>
        public static Stream GetEmbeddedResource(string pResourceName)
        {
            if (!string.IsNullOrEmpty(pResourceName))
            {
                //获取当前运行的Assembly
                Assembly ass = Assembly.GetCallingAssembly();
                //根据资源名称从Assembly中获取资源文件
                Stream s = ass.GetManifestResourceStream(pResourceName);
                //
                return s;
            }
            return null;
        }

        /// <summary>
        /// 获取文本格式的嵌入式资源的文件内容
        /// <remarks>
        /// <para>1.资源文件必须是在调用本方法的客户端代码所处的程序集中</para>
        /// </remarks>
        /// </summary>
        /// <param name="pResourceName">资源名称,为资源所属的 命名空间+目录名+文件名</param>
        /// <returns></returns>
        public static string GetEmbeddedText(string pResourceName)
        {
            //获取当前运行的Assembly
            Assembly ass = Assembly.GetCallingAssembly();
            //根据资源名称从Assembly中获取资源文件
            using (var  s = ass.GetManifestResourceStream(pResourceName))
            {
                TextReader reader = new StreamReader(s);
                var content = reader.ReadToEnd();
                return content;
            }
        }
        #endregion
    }
}
