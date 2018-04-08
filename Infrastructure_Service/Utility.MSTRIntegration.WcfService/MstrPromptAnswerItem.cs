using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace JIT.MSTRIntegration.WcfService
{
    /// <summary>
    /// 提问回答对象
    /// </summary>    
    [DataContract]
    public class MstrPromptAnswerItem
    {
        /// <summary>
        /// 提问项代码
        /// </summary>
        [DataMember]
        public string PromptCode;

        /// <summary>
        /// 原始查询条件
        /// </summary>
        [DataMember]
        public string[] QueryCondition;
    }
}
