/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/20 14:50:51
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

namespace JIT.Utility.Reflection
{
    /// <summary>
    /// 字典式的属性名映射 
    /// </summary>
    public class DictionaryPropertyNameMapping:IPropertyNameMapping
    {
        #region 构造函数
        /// <summary>
        /// 字典式的属性名映射 
        /// </summary>
        public DictionaryPropertyNameMapping()
        {
            this.IgnoreCase = false;
        }
        /// <summary>
        /// 字典式的属性名映射
        /// </summary>
        /// <param name="pMappings">映射</param>
        public DictionaryPropertyNameMapping(Dictionary<string, string> pMappings)
        {
            this.IgnoreCase = false;
            if (pMappings != null)
                this._innerMappings = pMappings;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 内部的映射表
        /// </summary>
        protected Dictionary<string, string> _innerMappings = new Dictionary<string, string>();

        /// <summary>
        /// 是否忽略大小写
        /// </summary>
        public bool IgnoreCase { get; set; }
        #endregion

        #region 集合操作
        /// <summary>
        /// 添加映射项
        /// </summary>
        /// <param name="pSourceName">源名称</param>
        /// <param name="pPropertyName">属性名</param>
        public void Add(string pSourceName, string pPropertyName)
        {
            this._innerMappings.Add(pSourceName, pPropertyName);
        }
        /// <summary>
        /// 清除映射项
        /// </summary>
        public void Clear()
        {
            this._innerMappings.Clear();
        }
        #endregion

        #region IPropertyNameMapping 成员
        /// <summary>
        /// 根据名称找到相应的属性名
        /// </summary>
        /// <param name="pSourceName">源名称</param>
        /// <returns></returns>
        public string GetPropertyNameBy(string pSourceName)
        {
            if (pSourceName == null)
                return null;
            if (this.IgnoreCase)
                pSourceName = pSourceName.ToLower();
            foreach (var item in this._innerMappings)
            {
                if (this.IgnoreCase)
                {
                    if (item.Key.ToLower() == pSourceName)
                        return item.Value;
                }
                else
                {
                    if (item.Key == pSourceName)
                        return item.Value;
                }
            }
            return null;
        }
        #endregion
    }
}
