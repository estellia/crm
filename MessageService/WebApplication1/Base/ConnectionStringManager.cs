﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.DataAccess;
using System.Configuration;

namespace JIT.SMSReportService.Base
{
    class ConnectionStringManager : IConnectionStringManager
    {
        public string GetConnectionStringBy(Utility.BasicUserInfo pUserInfo)
        {
            return ConfigurationManager.AppSettings["conn"];
        }
    }
}
