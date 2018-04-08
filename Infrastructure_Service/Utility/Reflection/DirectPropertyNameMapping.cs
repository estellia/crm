/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/2/20 14:46:47
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
    /// 直接的属性名映射 
    /// </summary>
    public class DirectPropertyNameMapping:IPropertyNameMapping
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DirectPropertyNameMapping()
        {
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
            return pSourceName;
        }
        #endregion
    }
}
