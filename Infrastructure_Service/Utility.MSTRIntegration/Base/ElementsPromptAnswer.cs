using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MSTRIntegration.Base
{
    /// <summary>
    /// ElementsPromptAnswer
    /// </summary>
    public class ElementsPromptAnswer : IPromptAnswer
    {
        /// <summary>
        /// 提问的标识
        /// </summary>
        private string _promptGuid;

        //构造器
        public ElementsPromptAnswer(string pPromptGuid)
        {
            this._promptGuid = pPromptGuid;
        }
        /// <summary>
        /// 生成回答语句
        /// </summary>
        /// <param name="pQueryCondition">回答结果</param>
        /// <returns>Mstr回答语句</returns>
        public string GetAnswerExpression(string[] pQueryCondition)
        {
            if (pQueryCondition == null)
                return string.Empty;
            string result = string.Empty;

            var sourceValue = pQueryCondition;         
            foreach (var item in sourceValue)
            {
                result += this._promptGuid+":"+item + ";";
            } 
            return result;
        }

        /// <summary>
        /// 当前提问回答的类型
        /// </summary>
        public PromptAnswerType PromptType
        {
            get
            {
                return PromptAnswerType.ElementsPromptAnswer;
            }
        }
    }
}
