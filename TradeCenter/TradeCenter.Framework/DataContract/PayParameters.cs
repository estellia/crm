using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.Framework.DataContract
{
    public class PayParameters
    {

        public int? ChannelId { set; get; }
        public string AppOrderID { get; set; }
        public string merchantId { set; get; }
        public string merchantSerial { set; get; }
        public string transTime { set; get; }
        public string amount { set; get; }
        public string account { set; get; }
        public string accName { set; get; }
        public string accBankCode { set; get; }
        public string transDesc { set; get; }
        public string transStatus { set; get; }
        public string msgExt { set; get; }
        public string misc { set; get; }
        public string transList { set; get; }
    }
}
