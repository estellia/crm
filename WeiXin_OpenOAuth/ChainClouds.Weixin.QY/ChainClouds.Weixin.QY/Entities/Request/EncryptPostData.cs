/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：EncryptPostData.cs
    文件功能描述：原始加密信息
    
    
    创建标识：ChainClouds - 20150313
----------------------------------------------------------------*/

namespace ChainClouds.Weixin.QY.Entities
{
    public class EncryptPostData
    {
        public string ToUserName { get; set; }
        public string Encrypt { get; set; }
        public int AgentID { get; set; }
    }
}
