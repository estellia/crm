using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MSTRIntegration.Base
{
    public class ValuePromptAnswer : IPromptAnswer
    {
        /// <summary>
        /// 提问的标识
        /// </summary>
        private string _promptGuid;

        //构造器
        public ValuePromptAnswer(string pPromptGuid)
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
            if (pQueryCondition != null && pQueryCondition.Length > 0)
                return pQueryCondition[0];
            else
                return string.Empty;
        }

        /// <summary>
        /// 当前提问回答的类型
        /// </summary>
        public PromptAnswerType PromptType
        {
            get
            {
                return PromptAnswerType.ValuePromptAnswer;
            }
        }
    }
}
