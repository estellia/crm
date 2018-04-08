using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.CometManagement
{
    class MessageResendItem
    {
        public string UserId { get; private set; }
        public string MessageBody { get; private set; }
        public DateTime SendTime { get; private set; }

        public MessageResendItem(string userId, string messageBody, DateTime sendTime)
        {
            this.UserId = userId;
            this.MessageBody = messageBody;
            this.SendTime = sendTime;
        }
    }
}
