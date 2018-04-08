/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/4/12 11:43:00
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

namespace JIT.Utility.Web
{
    /// <summary>
    /// Javascript的函数 
    /// </summary>
    public class JSFunction:JavascriptBlock
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public JSFunction()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// 函数的参数集合
        /// </summary>
        public List<string> Params { get; set; }

        /// <summary>
        /// 函数名称
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// 函数的类别
        /// </summary>
        public JSFunctionTypes Type { get; set; }
        #endregion

        #region 生成JS函数声明脚本
        /// <summary>
        /// 生成JS函数声明脚本
        /// </summary>
        /// <param name="pPrevTabs">脚本所有内容都包含的前导tab键数,用于排版</param>
        /// <returns></returns>
        public override string ToScriptBlock(int pPrevTabs)
        {
            StringBuilder script = new StringBuilder();
            //
            switch (this.Type)
            {
                case JSFunctionTypes.Inline:
                    {
                        var content = base.ToScriptBlock(0);
                        content = content.Replace(Keyboard.SPACE.ToString(), string.Empty).Replace(Keyboard.TAB, string.Empty);
                        script.AppendFormat("javascript:{0}", content);
                    }
                    break;
                case JSFunctionTypes.Common:
                    {
                        var tabs = Keyboard.TAB.ConcatRepeatly(pPrevTabs);
                        script.AppendFormat("{0}function {1}(",tabs,this.FunctionName);
                        if (this.Params != null && this.Params.Count > 0)
                        {
                            for (var i = 0; i < this.Params.Count; i++)
                            {
                                if (i != 0)
                                    script.Append(",");
                                script.Append(this.Params[i]);
                            }
                        }
                        script.AppendFormat("){{{0}",Environment.NewLine);
                        script.Append(base.ToScriptBlock(pPrevTabs + 1));
                        script.Append(Environment.NewLine);
                        script.AppendFormat("{0}}}",tabs);
                    }
                    break;
                case JSFunctionTypes.Variable:
                    {
                        var tabs = Keyboard.TAB.ConcatRepeatly(pPrevTabs);
                        script.AppendFormat("{0}function(", tabs);
                        if (this.Params != null && this.Params.Count > 0)
                        {
                            for (var i = 0; i < this.Params.Count; i++)
                            {
                                if (i != 0)
                                    script.Append(",");
                                script.Append(this.Params[i]);
                            }
                        }
                        script.AppendFormat("){{{0}", Environment.NewLine);
                        script.Append(base.ToScriptBlock(pPrevTabs + 1));
                        script.Append(Environment.NewLine);
                        script.AppendFormat("{0}}}", tabs);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
            //
            return script.ToString();
        }
        #endregion

        #region 生成函数调用的语句
        /// <summary>
        /// 生成函数调用的语句
        /// </summary>
        /// <param name="pParams">形参,如果函数是函数变量形式的函数,则pParams的第一个元素为函数的变量名</param>
        /// <returns></returns>
        public IJavascriptObject Call(params string[] pParams)
        {
            string functionName = string.Empty;
            switch (this.Type)
            {
                case JSFunctionTypes.Common:
                    {
                        functionName = this.FunctionName;
                    }
                    break;
                case JSFunctionTypes.Variable:
                    {
                        if (pParams == null || pParams.Length <= 1)
                        {
                            throw new ArgumentException("调用函数变量时,pParams中第一个元素必须为函数变量名.");
                        }
                        functionName = pParams[0];
                        var temp = new string[pParams.Length - 1];
                        Array.Copy(pParams, 1, temp, 0, temp.Length);
                        pParams = temp;
                    }
                    break;
                case JSFunctionTypes.Inline:
                    throw new NotSupportedException("内联函数不支持调用.");
                default:
                    throw new NotImplementedException();
            }
            //
            StringBuilder script = new StringBuilder();
            script.AppendFormat("{0}(", functionName);
            if (pParams != null && pParams.Length > 0)
            {
                for(var i=0;i<pParams.Length;i++)
                {
                    if (i != 0)
                        script.Append(",");
                    script.Append(pParams[i]);
                }
            }
            script.Append(");");
            var js = new JavascriptBlock();
            js.AddSentence(script.ToString());
            //
            return js;
        }
        #endregion
    }
}
