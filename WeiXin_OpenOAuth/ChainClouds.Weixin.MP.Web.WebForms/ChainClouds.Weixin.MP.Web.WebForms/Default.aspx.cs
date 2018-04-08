﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChainClouds.Weixin.MP.Web.WebForms
{
    public partial class _Default : System.Web.UI.Page
    {
        public string DllVersion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(Server.MapPath("~/bin/ChainClouds.Weixin.MP.dll"));
            DllVersion = string.Format("{0}.{1}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart);
        }
    }
}