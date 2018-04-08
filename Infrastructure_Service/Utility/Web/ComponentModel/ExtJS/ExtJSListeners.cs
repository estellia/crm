/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/17 18:19:36
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
    /// ExtJS的监听器 
    /// </summary>
    public class ExtJSListeners : List<ExtJSEventHandler>, IJavascriptObject
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ExtJSListeners()
        {
        }
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
            var script = new StringBuilder();
            script.AppendFormat("{0}{{{1}",tabs,Environment.NewLine);
            bool isFirst = true;
            foreach (var item in this)
            {
                if (!isFirst)
                    script.AppendFormat("{0},", tabs);
                else
                {
                    script.Append(tabs);
                    isFirst = false;
                }
                script.Append(item.ToScriptBlock(pPrevTabs));
                script.Append(Environment.NewLine);
            }
            script.AppendFormat("{0}{1}}}{2}", tabs,Keyboard.TAB, Environment.NewLine);
            //
            return script.ToString();
        }

        #endregion
    }
}
