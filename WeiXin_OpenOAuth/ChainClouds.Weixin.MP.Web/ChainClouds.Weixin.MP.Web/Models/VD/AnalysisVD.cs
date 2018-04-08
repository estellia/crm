using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChainClouds.Weixin.MP.Entities;
using ChainClouds.Weixin.MP.Web.Controllers;

namespace ChainClouds.Weixin.MP.Web.Models.VD
{
    public class Analysis_IndexVD
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public AnalysisType AnalysisType { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public object Result { get; set; }
        public WxJsonResult WxJsonResult { get; set; }
    }
}