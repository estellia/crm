namespace JIT.Utility.CometManagement.Message
{
    public class CometHeartbeatMessage :  CometMessageBase
    {
        public CometHeartbeatMessage()
            : base(CometMessageType.Normal, "ok", "")
        {
        }
    }
}
