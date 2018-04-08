using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace cPos.Components.Exceptions
{
    [Serializable]
    public class DrpException : ApplicationException
    {
        public DrpException() : base("Drp caused an exception.") { }

        public DrpException(Exception ex) : base("Drp caused an exception.", ex) { }

        public DrpException(string message) : base(message) { }

        public DrpException(string message, Exception inner) : base(message, inner) { }

        protected DrpException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
