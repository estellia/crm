using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace JIT.Utility.Msmq.Builder
{
    public class MsmqBulder
    {
        /// <summary>
        /// 创建消息队列
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static MessageQueue Create(string Name)
        {
            string path = string.Empty;
            Name = Name.Replace(".\\private$\\", "");
            path = string.Format(".\\private$\\{0}", Name);
            if (!MessageQueue.Exists(path))
            {
                return MessageQueue.Create(path);
            }
            else
                return new MessageQueue(path);
        }
    }
}
