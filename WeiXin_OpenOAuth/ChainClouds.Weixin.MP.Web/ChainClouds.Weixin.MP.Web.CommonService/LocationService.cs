﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：LocationService.cs
    文件功能描述：地理位置信息处理
    
    
    创建标识：ChainClouds - 20150312
----------------------------------------------------------------*/

using System.Collections.Generic;
using ChainClouds.Weixin.MP.Entities;
using ChainClouds.Weixin.MP.Entities.GoogleMap;
using ChainClouds.Weixin.MP.Helpers;

namespace ChainClouds.Weixin.MP.Web.CommonService
{
    public class LocationService
    {
        public ResponseMessageNews GetResponseMessage(RequestMessageLocation requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);

            var markersList = new List<GoogleMapMarkers>();
            markersList.Add(new GoogleMapMarkers()
            {
                X = requestMessage.Location_X,
                Y = requestMessage.Location_Y,
                Color = "red",
                Label = "S",
                Size = GoogleMapMarkerSize.Default,
            });
            var mapSize = "480x600";
            var mapUrl = GoogleMapHelper.GetGoogleStaticMap(19 /*requestMessage.Scale*//*微信和GoogleMap的Scale不一致，这里建议使用固定值*/,
                                                            markersList, mapSize);
            responseMessage.Articles.Add(new Article() 
            {
                Description = string.Format("您刚才发送了地理位置信息。Location_X：{0}，Location_Y：{1}，Scale：{2}，标签：{3}",
                              requestMessage.Location_X, requestMessage.Location_Y,
                              requestMessage.Scale, requestMessage.Label),
                PicUrl = mapUrl,
                Title = "定位地点周边地图",
                Url = mapUrl
            });
            responseMessage.Articles.Add(new Article()
            {
                Title = "微信公众平台SDK 官网链接",
                Description = "SDK地址",
                PicUrl = "http://open.chainclouds.com/images/logo.jpg",
                Url = "http://open.chainclouds.com"
            });

            return responseMessage;
        }
    }
}