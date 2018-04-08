namespace JIT.Utility.CometManagement.Message
{
    class CometNotifyMessage:  CometMessageBase
    {
        public CometNotifyMessage(string notifyMessage)
            : base(CometMessageType.Notify, notifyMessage, "")
        {
        }
    }
}
