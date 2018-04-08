using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.Framework.Channel
{
    public abstract class BaseChannel
    {
        public BaseChannel()
        {
            RoyaltyItemList = new List<RoyaltyItem>();
        }

        public List<RoyaltyItem> RoyaltyItemList { get; set; }

        public string GetRoyaltyStr()
        {
            var sb = new StringBuilder();
            foreach (var item in RoyaltyItemList)
            {
                sb.AppendFormat("{0}^{1}^{2}|", item.Account, item.Account, item.Remark);
            }
            return sb.ToString().Trim('|');
        }
    }

    public class RoyaltyItem
    {
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
    }
}
