using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.Framework.DataContract
{
    public class WXGetSignReqPara
    {
        /// <summary>
        /// 支付通道ID
        /// </summary>
        public int? PayChannelID { get; set; }
        /// <summary>
        /// 需加密的参数信息,KEY,VALUE
        /// </summary>
        public Dictionary<string, object> NoSignDic { get; set; }
    }
}
