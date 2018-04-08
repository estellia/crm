﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class csconfig_configList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected string ConfigAddUrl
    {
        get
        {
            string configAddUrl = ConfigurationManager.AppSettings["configAddUrl"];
            if (string.IsNullOrEmpty(configAddUrl))
            {
                configAddUrl = "";
            }
            return configAddUrl;
        }
    }
}