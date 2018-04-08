/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/9 13:52:48
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
using System.Collections;
using System.Collections.Generic;
using System.Text;

using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.Web
{
    /// <summary>
    /// Javascript代码块
    /// </summary>
    public class JavascriptBlock:IJavascriptObject
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public JavascriptBlock()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pSentence">语句内容</param>
        /// <param name="pIncludeStringWrapper">语句内容是否要用引号括起来</param>
        public JavascriptBlock(string pSentence,bool pIncludeStringWrapper=false)
        {
            this.Sentences = new List<string>();
            if (pIncludeStringWrapper)
            {
                this.Sentences.Add(string.Format("{0}{1}{0}",JSONConst.STRING_WRAPPER,pSentence));
            }
            else
            {
                this.Sentences.Add(pSentence);
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pSentences">语句</param>
        public JavascriptBlock(IEnumerable<string> pSentences)
        {
            this.Sentences = new List<string>();
            this.Sentences.AddRange(pSentences);
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 语句行
        /// </summary>
        public List<string> Sentences { get; set; }
        #endregion

        #region IJavascriptObject 成员

        /// <summary>
        /// Javascript对象转换成对象的脚本块
        /// </summary>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <returns>Javascript脚本块</returns>
        public virtual string ToScriptBlock(int pPrevTabs)
        {
            if (this.Sentences != null && this.Sentences.Count > 0)
            {
                var tabs = Keyboard.TAB.ConcatRepeatly(pPrevTabs);
                StringBuilder script = new StringBuilder();
                bool isFirst = true;
                foreach (var s in this.Sentences)
                {
                    if (!isFirst)
                    {
                        script.Append(Environment.NewLine);
                    }
                    else
                    {
                        isFirst = false;
                    }
                    script.AppendFormat("{0}{1}",tabs,s);
                }
                //
                return script.ToString();
            }
            return string.Empty;
        }
        #endregion

        #region 添加JS语句
        /// <summary>
        /// 添加JS语句
        /// </summary>
        /// <param name="pSentence"></param>
        public void AddSentence(string pSentence)
        {
            if (this.Sentences == null)
                this.Sentences = new List<string>();
            this.Sentences.Add(pSentence);
        }
        /// <summary>
        /// 添加JS语句
        /// </summary>
        /// <param name="pTemplate">语句模板</param>
        /// <param name="pParams">模板参数</param>
        public void AddSentence(string pTemplate, params string[] pParams)
        {
            if (this.Sentences == null)
                this.Sentences = new List<string>();
            this.Sentences.Add(string.Format(pTemplate, pParams));
        }
        #endregion

        #region 支持将常见的类型显示类型转换为JavascriptBlock
        /// <summary>
        /// 支持将值类型显示类型转换为JavascriptBlock
        /// </summary>
        /// <param name="pObj"></param>
        /// <returns></returns>
        public static explicit operator JavascriptBlock(ValueType pObj)
        {
            string content = string.Empty;
            if (pObj == null)
                content = "null";
            else
                content = pObj.ToJSON();
            return new JavascriptBlock(content);
        }

        /// <summary>
        /// 支持将string显示类型转换为JavascriptBlock
        /// </summary>
        /// <param name="pS">字符串</param>
        /// <returns></returns>
        public static explicit operator JavascriptBlock(string pS)
        {
            string content = string.Empty;
            if (pS == null)
                content = "null";
            else
                content = pS.ToJSON();
            return new JavascriptBlock(content);
        }

        /// <summary>
        /// 支持将StringBuilder显示类型转换为JavascriptBlock
        /// </summary>
        /// <param name="pS">字符串</param>
        /// <returns></returns>
        public static explicit operator JavascriptBlock(StringBuilder pS)
        {
            string content = string.Empty;
            if (pS == null)
                content = "null";
            else
                content = pS.ToString().ToJSON();
            return new JavascriptBlock(content);
        }

        #endregion

        #region 重写ToString方法
        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToScriptBlock(0);
        }
        #endregion
    }
}
