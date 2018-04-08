/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：IMessageHandler.cs
    文件功能描述：MessageHandler接口
    
    
    创建标识：ChainClouds - 20150924
    
----------------------------------------------------------------*/

using ChainClouds.Weixin.MessageHandlers;
using ChainClouds.Weixin.MP.Entities;

namespace ChainClouds.Weixin.MP.MessageHandlers
{

    public interface IMessageHandler : IMessageHandler<IRequestMessageBase, IResponseMessageBase>
    {
        new IRequestMessageBase RequestMessage { get; set; }
        new IResponseMessageBase ResponseMessage { get; set; }
    }
}
