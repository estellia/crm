/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/17 17:49:35
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

using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Web.ComponentModel.ExtJS
{
    /// <summary>
    /// 事件处理 
    /// </summary>
    public class ExtJSEventHandler:IJavascriptObject
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ExtJSEventHandler()
        {
        }
        #endregion

        #region 属性集

        #region 事件名
        /// <summary>
        /// 事件名
        /// </summary>
        public string EventName { get; set; }
        #endregion

        #region 元素
        /// <summary>
        /// 元素
        /// </summary>
        public string Element { get; set; }
        #endregion

        #region 函数
        /// <summary>
        /// 函数
        /// </summary>
        public JSFunction Function { get; set; }
        #endregion

        #endregion

        #region IJavascriptObject 成员

        /// <summary>
        /// Javascript对象转换成对象的脚本块
        /// <remarks>
        /// <para>对于Ext JS的类,生成脚本块都采用配置项的方式来生成.</para>
        /// </remarks>
        /// </summary>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <returns>Javascript脚本块</returns>
        public string ToScriptBlock(int pPrevTabs)
        {
            var tabs = Keyboard.TAB.ConcatRepeatly(pPrevTabs);
            //
            StringBuilder script = new StringBuilder();
            script.AppendFormat("{0}{1}: {{{2}",tabs,this.EventName,Environment.NewLine);
            bool isFirst = true;
            if (!string.IsNullOrWhiteSpace(this.Element))
            {
                script.AppendFormat("{0}{1}element: {2}{3}{2}{4}",tabs,Keyboard.TAB,JSONConst.STRING_WRAPPER,this.Element,Environment.NewLine);
                isFirst = false;
            }
            this.Function.Type = JSFunctionTypes.Variable;
            var functionCreation = this.Function.ToScriptBlock(pPrevTabs+1);
            functionCreation = functionCreation.TrimStart(Keyboard.SPACE);
            script.AppendFormat("{0}{1}{2}fn: {3}{4}", tabs, Keyboard.TAB,isFirst?string.Empty:",", functionCreation,Environment.NewLine);
            script.AppendFormat("{0}}}",tabs);
            //
            return script.ToString();
        }

        #endregion
    }
}
