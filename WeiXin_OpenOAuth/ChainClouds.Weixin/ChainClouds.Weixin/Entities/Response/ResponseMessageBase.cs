/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：ResponseMessageBase.cs
    文件功能描述：响应回复消息基类
    
    
    创建标识：ChainClouds - 20150211
    
    修改标识：ChainClouds - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace ChainClouds.Weixin.Entities
{
	public interface IResponseMessageBase : IMessageBase
	{
		//ResponseMsgType MsgType { get; }
		//string Content { get; set; }
		//bool FuncFlag { get; set; }
	}

	/// <summary>
	/// 响应回复消息
	/// </summary>
	public abstract class ResponseMessageBase : MessageBase, IResponseMessageBase
	{
        //public virtual ResponseMsgType MsgType
        //{
        //    get { return ResponseMsgType.Text; }
        //}
	}
}
