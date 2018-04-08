using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;
using JIT.Utility.Msmq.Base;

namespace JIT.Utility.Msmq.Config
{
    public class MessageConfig:IMessageConfig
    {
        public virtual void Config(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
