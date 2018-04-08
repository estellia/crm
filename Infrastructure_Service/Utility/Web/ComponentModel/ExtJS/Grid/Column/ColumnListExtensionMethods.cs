/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/9 10:16:55
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

namespace JIT.Utility.Web.ComponentModel.ExtJS.Grid.Column
{
    /// <summary>
    /// IEnumerable Column的扩展方法
    /// </summary>
    public static class ColumnListExtensionMethods
    {
        /// <summary>
        /// 扩展方法：生成列数组的创建语句
        /// </summary>
        /// <param name="pCaller">调用者</param>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <returns></returns>
        public static string ToCreationConfigScript(this IEnumerable<Column> pCaller, int pPrevTabs)
        {
            if (pCaller == null)
                throw new NullReferenceException();
            var tabs = Keyboard.TAB.ConcatRepeatly(pPrevTabs);
            var script = new StringBuilder();
            script.AppendFormat("{0}[{1}",tabs,Environment.NewLine);
            bool isFirst = true;
            foreach (var col in pCaller)
            {
                var temp = col.ToCreationConfigScript(pPrevTabs + 1);
                temp = temp.TrimStart(Keyboard.SPACE);
                if (!isFirst)
                {
                    script.AppendFormat("{0}{1},", tabs, Keyboard.TAB);
                }
                else
                {
                    script.AppendFormat("{0}{1}", tabs, Keyboard.TAB);
                    isFirst = false;
                }
                script.Append(temp);
            }
            script.AppendFormat("{0}]", tabs);
            //
            return script.ToString();
        }
    }
}
