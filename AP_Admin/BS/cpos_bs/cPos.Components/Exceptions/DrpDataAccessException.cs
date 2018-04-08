using System;
using System.Runtime.Serialization;

namespace cPos.Components.Exceptions
{
    [Serializable]
    public class DrpDataAccessException : DrpException
    {
        public DrpDataAccessException() : base("iBatis.NET caused an exception.") { }


        public DrpDataAccessException(Exception ex) : base("iBatis.NET caused an exception.", ex) { }


        public DrpDataAccessException(string message) : base(message) { }


        public DrpDataAccessException(string message, Exception inner) : base(message, inner) { }


        protected DrpDataAccessException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
