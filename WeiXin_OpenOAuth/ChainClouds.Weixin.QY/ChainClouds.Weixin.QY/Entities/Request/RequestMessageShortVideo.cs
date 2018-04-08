/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：RequestMessageShortVideo.cs
    文件功能描述：接收小视频消息
    
    
    创建标识：ChainClouds - 20150708
----------------------------------------------------------------*/

namespace ChainClouds.Weixin.QY.Entities
{
    public class RequestMessageShortVideo : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.ShortVideo; }
        }

        public string MediaId { get; set;}
        public string ThumbMediaId { get; set; }
    }
}
