using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.MSTRIntegration.Base
{
    /// <summary>
    /// Prompt应答类接口
    /// </summary>
    interface IPromptAnswer
    {
        /// <summary>
        /// 根据前台查询条件生成Mstr所需的PromptAnswer字符串
        /// </summary>
        /// <param name="pQueryObject">查询参数值</param>
        /// <returns>Mstr格式的字符串</returns>
        string GetAnswerExpression(string[] pQueryCondition);
        
        /// <summary>
        /// Prompt类型
        /// </summary>
        PromptAnswerType PromptType { get; }
    }
}
