/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：ResponseMessageVideo.cs
    文件功能描述：响应回复视频消息
    
    
    创建标识：ChainClouds - 20150211
    
    修改标识：ChainClouds - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace ChainClouds.Weixin.MP.Entities
{
    /// <summary>
    /// 需要预先上传多媒体文件到微信服务器，只支持认证服务号。
    /// </summary>
    public class ResponseMessageVideo : ResponseMessageBase, IResponseMessageBase
    {
        new public virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Video; }
        }

        public Video Video { get; set; }

        public ResponseMessageVideo()
        {
            Video = new Video();
        }
    }
}
