/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：RequestMessageEvent_View.cs
    文件功能描述：事件之URL跳转视图（View）
    
    
    创建标识：ChainClouds - 20150313
    
    修改标识：ChainClouds - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace ChainClouds.Weixin.QY.Entities
{
    /// <summary>
    /// 事件之URL跳转视图（View）
    /// </summary>
    public class RequestMessageEvent_View : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.VIEW; }
        }

        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey { get; set; }
    }
}
