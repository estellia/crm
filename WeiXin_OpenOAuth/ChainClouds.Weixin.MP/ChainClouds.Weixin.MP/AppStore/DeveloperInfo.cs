/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
  
    文件名：ReturnResult.cs
    文件功能描述：连锁掌柜开发者信息
    
    
    创建标识：ChainClouds - 20150319
----------------------------------------------------------------*/

namespace ChainClouds.Weixin.MP.AppStore
{
    /// <summary>
    /// 连锁掌柜开发者信息。申请开发者：http://www.chainclouds.com/User/Developer/Apply
    /// </summary>
    public class DeveloperInfo
    {
        ///// <summary>
        ///// 连锁掌柜开发者的AppKey，可以在 http://www.chainclouds.com/Developer/Developer 找到
        ///// </summary>
        //public string AppKey { get; set; }

        ///// <summary>
        ///// 连锁掌柜开发者的Secret，可以在 http://www.chainclouds.com/Developer/Developer 找到
        ///// </summary>
        //public string AppSecret { get; set; }

        /// <summary>
        /// 在www.chainclouds.com对接微信公众号之后，自动生成的ChaincloudsKey。
        /// </summary>
        public string ChaincloudsKey { get; set; }
    }
}
