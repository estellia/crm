using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.Framework.DataContract
{
    public class WXGetUpdateFeedBackReqPara
    {
        public int PayChannelID { get; set; }
        public string FeedBackID { get; set; }
        public string OpenID { get; set; }
    }
}
