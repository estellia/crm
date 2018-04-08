/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/1/19 13:46:04
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

namespace JIT.Utility.Web
{
    /// <summary>
    /// MVC - 视图 
    /// </summary>
    public class View
    {
        #region 构造函数
        /// <summary>
        /// MVC - 视图 
        /// </summary>
        public View()
        {
            this.IsIgnoreCase = true;
        }
        /// <summary>
        /// MVC - 视图 
        /// </summary>
        /// <param name="pName">视图名称</param>
        public View(string pName)
        {
            this.IsIgnoreCase = true;
            this.Name = pName;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 视图的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否忽略大小谢
        /// </summary>
        public bool IsIgnoreCase { get; set; }
        #endregion

        #region 判断
        /// <summary>
        /// 判断视图是否为指定名称
        /// </summary>
        /// <param name="pName">视图名称</param>
        /// <returns></returns>
        public bool Is(string pName)
        {
            if (pName == null || this.Name ==null)
                return false;
            pName = pName.Trim();
            if (this.IsIgnoreCase)
            {
                var name = this.Name.ToLower();
                return name == pName.ToLower();
            }
            else
            {
                return this.Name == pName;
            }
        }
        #endregion

        #region 隐式类型转换
        /// <summary>
        /// 将字符串隐式转换为View
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        public static explicit operator View(string pName)
        {
            return new View(pName);
        }
        #endregion
    }
}
