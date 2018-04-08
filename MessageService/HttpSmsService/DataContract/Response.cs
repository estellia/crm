using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HttpSmsService.DataContract
{
    public class Response
    {
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public object ResData { get; set; }
    }
}