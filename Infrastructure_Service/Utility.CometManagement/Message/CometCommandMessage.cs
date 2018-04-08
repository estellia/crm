namespace JIT.Utility.CometManagement.Message
{
    public class CometCommandMessage : CometMessageBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestJson">json格式的request</param>
        /// <param name="requestType">request分类，可以自定义</param>
        public CometCommandMessage(string requestJson, string requestType)
            : base(CometMessageType.Command, requestType, requestJson)
        {
        }
    }
}
