﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：AnalysisResultJson.cs
    文件功能描述：分析数据接口返回结果
    
    
    创建标识：ChainClouds - 20150309
    
    修改标识：ChainClouds - 20150310
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace ChainClouds.Weixin.MP.AdvancedAPIs.Analysis
{
    /// <summary>
    /// 分析数据接口返回结果
    /// </summary>
    public class AnalysisResultJson<T> : BaseAnalysisResult
    {
        public List<T> list
        {
            get { return base.ListObj as List<T>; }
            set { base.ListObj = value; }
        }

        public AnalysisResultJson()
        {
            ListObj = new List<T>();
        }
    }

}
