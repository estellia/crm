using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC
{
    public class CC_Prize
    {
        public string CustomerId { get; set; }
        public string EventId { get; set; }
        public string PrizeName { get; set; }
        public string PrizeId { get; set; }
        public string PrizeType { get; set; } 
        public int Point { get; set; }
        public string CouponTypeId { get; set; }
        public int Location { get; set; }
    }
}
