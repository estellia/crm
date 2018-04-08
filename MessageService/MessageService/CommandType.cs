using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.MessageService
{
    public enum SmsCommandType
    {
        MT_REQUEST,
        MT_RESPONSE,
        MULTI_MT_REQUEST,
        MULTI_MT_RESPONSE,
        MULTIX_MT_REQUEST,
        MULTIX_MT_RESPONSE,
        MO_REQUEST,
        MO_RESPONSE
    }

}
