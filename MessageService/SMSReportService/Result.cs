using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSReportService
{
    public class Result
    {
        public string Command { get; set; }
        public string Spid { get; set; }
        public string Sppassword { get; set; }
        public string Spsc { get; set; }
        public string Mtmsgid { get; set; }
        public string Mtstat { get; set; }
        public string Mterrcode { get; set; }
        public string Rttime { get; set; }
    }
}