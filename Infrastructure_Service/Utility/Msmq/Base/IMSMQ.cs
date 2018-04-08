using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace JIT.Utility.Msmq.Base
{
    public interface IMSMQ<T>
    {
        void Send(T obj);

        void Listen();

        MessageEnumerator GetEnumerator();
    }
}
