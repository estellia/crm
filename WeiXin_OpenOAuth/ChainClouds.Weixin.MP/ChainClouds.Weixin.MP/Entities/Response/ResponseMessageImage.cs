/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：ResponseMessageImage.cs
    文件功能描述：响应回复图片消息
    
    
    创建标识：ChainClouds - 20150211
    
    修改标识：ChainClouds - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace ChainClouds.Weixin.MP.Entities
{
    public class ResponseMessageImage : ResponseMessageBase, IResponseMessageBase
    {
        new public virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Image; }
        }

        public Image Image { get; set; }

        public ResponseMessageImage()
        {
            Image = new Image();
        }
    }
}
