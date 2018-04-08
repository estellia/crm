using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MSTRIntegration.Base
{
    /// <summary>
    /// 提问回答对象
    /// </summary>    
    public class PromptAnswerItem
    {
        /// <summary>
        /// 提问项代码
        /// </summary>
        public string PromptCode;
        /// <summary>
        /// 提问类型
        /// </summary>
        public PromptAnswerType PromptType;
        /// <summary>
        /// 原始查询条件
        /// </summary>
        public string[] QueryCondition;
    }
}
