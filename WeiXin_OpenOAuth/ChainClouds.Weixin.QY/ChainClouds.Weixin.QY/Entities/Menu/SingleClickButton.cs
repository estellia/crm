﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：SingleClickButton.cs
    文件功能描述：单个按键
    
    
    创建标识：ChainClouds - 20150313
    
    修改标识：ChainClouds - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace ChainClouds.Weixin.QY.Entities.Menu
{
    /// <summary>
    /// 单个按键
    /// </summary>
    public class SingleClickButton : SingleButton
    {
        /// <summary>
        /// 类型为click时必须。
        /// 按钮KEY值，用于消息接口(event类型)推送，不超过128字节
        /// </summary>
        public string key { get; set; }

        public SingleClickButton()
            : base(ButtonType.click.ToString())
        {
        }
    }
}
