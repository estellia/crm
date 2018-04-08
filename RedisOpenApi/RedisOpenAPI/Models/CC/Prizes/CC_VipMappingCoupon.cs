using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC
{
    public class CC_VipMappingCoupon
    {
        public string CustomerId { get; set; }
        public string ObjectId { get; set; }
        public string VipId { get; set; }
        public string Source { get; set; }
        public CC_Coupon Coupon { get; set; }
    }
}
