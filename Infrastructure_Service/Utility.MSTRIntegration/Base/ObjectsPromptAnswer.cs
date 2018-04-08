using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MSTRIntegration.Base
{
    public class ObjectsPromptAnswer : IPromptAnswer
    {
        /// <summary>
        /// 提问的标识
        /// </summary>
        private string _promptGuid;
        /// <summary>
        /// 针对一个提问的多个回答的分隔符
        /// </summary>
        private const string PROMPT_SEPARATOR= "%1B"; 

        //构造器
        public ObjectsPromptAnswer(string pPromptGuid)
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
            var objectType = "12";//The object type, such as "4" for metric or "12" for attribute

            int itemIndex = 0;
            foreach (var item in sourceValue)
            {
                var objectAnswer = item;
                if (!objectAnswer.Contains("~"))
                    objectAnswer += "~" + objectType;

                if (itemIndex == sourceValue.Length - 1)//最后一个
                    result += objectAnswer;
                else
                    result += objectAnswer + PROMPT_SEPARATOR;
                itemIndex++;
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
                return PromptAnswerType.ValuePromptAnswer;
            }
        }
    }
}
