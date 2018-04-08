﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChainClouds.Weixin.MP.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Open",
                url: "Open/Callback/{appId}",
                defaults: new { controller = "Open", action = "Callback", appId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "sysmessage",
                url: "receive/sysmessage",
                defaults: new { controller = "Open", action = "Notice", appId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "eventmsg",
                url: "receive/eventmsg/{appId}",
                defaults: new { controller = "Open", action = "Callback", appId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "receive",
                url: "receive",
                defaults: new { controller = "OpenOAuth", action = "JumpToMpOAuth", appId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}