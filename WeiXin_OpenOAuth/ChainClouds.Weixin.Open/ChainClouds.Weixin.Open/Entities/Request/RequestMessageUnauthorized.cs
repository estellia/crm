/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：RequestMessageUnauthorized.cs
    文件功能描述：推送取消授权通知
    
    
    创建标识：ChainClouds - 20150430
----------------------------------------------------------------*/

namespace ChainClouds.Weixin.Open
{
    public class RequestMessageUnauthorized : RequestMessageBase
    {
        public override RequestInfoType InfoType
        {
            get { return RequestInfoType.unauthorized; }
        }
        public string AuthorizerAppid { get; set; }
    }
}
