using Newtonsoft.Json;

namespace JIT.Utility.CometManagement.Message
{
    public enum CometMessageType
    {
        /// <summary>
        /// Heartbeat message
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Command Message
        /// </summary>
        Command = 1,

        /// <summary>
        /// Notification Message
        /// </summary>
        Notify = 2
    }

    public abstract class CometMessageBase
    {
        public CometMessageType MessageType { get; set; }
        public string Message { get; set; }
        public string RequestJson { get; set; }

        protected CometMessageBase(CometMessageType code, string message, string request)
        {
            MessageType = code;
            Message = message;
            RequestJson = request;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
