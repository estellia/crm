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
    public class MstrDataRigthPromptAnswerItem
    {
        /// <summary>
        /// 层级
        /// </summary>
        [DataMember]
        public int Level;

        /// <summary>
        /// 取值
        /// </summary>
        [DataMember]
        public string[] Values;
    }
}
