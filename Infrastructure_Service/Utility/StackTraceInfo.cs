/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2012/8/10 16:22:07
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
using System.Reflection;
using System.Text;

namespace JIT.Utility
{
    /// <summary>
    /// 堆栈信息 
    /// </summary>
    [Serializable]
    public class StackTraceInfo
    {
        #region 构造函数
        /// <summary>
        /// 堆栈信息构造函数
        /// </summary>
        public StackTraceInfo()
        {
        }
        /// <summary>
        /// 堆栈信息构造函数 
        /// </summary>
        /// <param name="pMethod">调用方法</param>
        public StackTraceInfo(MethodBase pMethod)
        {
            if (pMethod != null)
            {
                this.Method = pMethod.Name;
                if (pMethod.ReflectedType != null)
                {
                    this.Class = pMethod.ReflectedType.FullName;
                    this.ClassAssemblyQualifiedName = pMethod.ReflectedType.AssemblyQualifiedName;
                    this.NameSpace = pMethod.ReflectedType.Namespace;
                }
                else
                {
                    this.Class = string.Empty;
                    this.ClassAssemblyQualifiedName = string.Empty;
                    this.NameSpace = string.Empty;
                }
            }
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 类
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// 类的程序集限定名
        /// </summary>
        public string ClassAssemblyQualifiedName { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 类文件位置
        /// </summary>
        public string ClassFileLocation { get; set; }

        /// <summary>
        /// 行号
        /// </summary>
        public int LineNumber { get; set; }
        #endregion

        #region 工具方法
        /// <summary>
        /// 是否为JIT的代码
        /// </summary>
        public bool IsJITCode
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Class)&& this.Class.StartsWith("JIT."))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 是否为最外层框架代码
        /// </summary>
        public bool IsOuterFrameworkCode
        {
            get
            {
                if(this.IsJITCode)
                {
                    foreach (var item in OUTER_FRAMEWORK_CLASS_NAME)
                    {
                        if (this.Class == item)
                            return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 获取方法全名
        /// </summary>
        /// <returns></returns>
        public string GetFullMethodName()
        {
            return string.Format("{0}.{1}",this.Class,this.Method);
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("  在类 {0}  方法 {1}  位置 {2}  行号 {3}",this.Class,this.Method,this.ClassFileLocation,this.LineNumber);
        }
        #endregion

        /// <summary>
        /// 最外层框架类的类名
        /// </summary>
        private static readonly string[] OUTER_FRAMEWORK_CLASS_NAME = new string[] { 
            ""
        };
    }
}
