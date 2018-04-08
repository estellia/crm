using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utility.Sync.WCFService.WCF.API
{
    public class SyncRespose
    {
        /// <summary>
        /// 小于100为成功,大于100为失败
        /// </summary>
        public int ResultCode { get; set; }
        public string Message { get; set; }
    }
}