/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：BaseAnalysisResult.cs
    文件功能描述：分析数据接口返回结果基类
    
    
    创建标识：ChainClouds - 20150309
    
    修改标识：ChainClouds - 20150310
    修改描述：整理接口
----------------------------------------------------------------*/

using ChainClouds.Weixin.Entities;

namespace ChainClouds.Weixin.MP.AdvancedAPIs.Analysis
{
    public interface IBaseAnalysisResult
    {
        object ListObj { get; set; }
    }

    public abstract class BaseAnalysisResult : WxJsonResult, IBaseAnalysisResult
    {
        public object ListObj { get; set; }

        //public BaseAnalysisResult()
        //{
        //    list = new List<BaseAnalysisObject>();
        //}
    }
}
