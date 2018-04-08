﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Chainclouds
    
    文件名：HomeController.cs
    文件功能描述：首页Controller
    
    
    创建标识：ChainClouds - 20150312
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
//using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ChainClouds.Weixin.Open.CommonAPIs;

namespace ChainClouds.Weixin.MP.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestElmah()
        {
            try
            {
                throw new Exception("出错测试，使用Elmah保存错误结果(1)");
            }
            catch (Exception)
            {

            }

            throw new Exception("出错测试，使用Elmah保存错误结果(2)");
            return View();
        }


        public ActionResult DebugOpen()
        {
            ChainClouds.Weixin.Config.IsDebug = true;
            return Content("Debug状态已打开。");
        }

        public ActionResult DebugClose()
        {
            ChainClouds.Weixin.Config.IsDebug = false;
            return Content("Debug状态已关闭。");
        }
    }
}
