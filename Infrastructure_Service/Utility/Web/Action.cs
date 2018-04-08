/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/1/19 13:50:55
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
    /// MVC - 操作 
    /// </summary>
    public class Action
    {
        #region 构造函数
        /// <summary>
        /// MVC - 操作  
        /// </summary>
        public Action()
        {
            this.IsIgnoreCase = true;
        }
        /// <summary>
        /// MVC - 操作  
        /// </summary>
        /// <param name="pCode">编码</param>
        /// <param name="pText">如果操作对应一个具体的按钮,则为按钮的文本</param>
        public Action(string pCode,string pText=null)
        {
            this.IsIgnoreCase = true;
            this.Code = pCode;
            this.Text = pText;
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 编码是否忽略大小写
        /// </summary>
        public bool IsIgnoreCase { get; set; }

        /// <summary>
        /// 如果操作对应一个具体的按钮,则为按钮的文本
        /// </summary>
        public string Text { get; set; }
        #endregion

        #region 判断
        /// <summary>
        /// 判断操作是否为指定名称
        /// </summary>
        /// <param name="pCode">操作的编码</param>
        /// <returns></returns>
        public bool Is(string pCode)
        {
            if (pCode == null || this.Code == null)
                return false;
            pCode = pCode.Trim();
            if (this.IsIgnoreCase)
            {
                var name = this.Code.ToLower();
                return name == pCode.ToLower();
            }
            else
            {
                return this.Code == pCode;
            }
        }
        #endregion
    }
}
