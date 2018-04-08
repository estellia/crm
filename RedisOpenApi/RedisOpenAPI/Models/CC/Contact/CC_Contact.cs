using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC
{
    public class CC_Contact
    {
        public string CustomerId { get; set; }
        public string WeiXinID { get; set; }
		public string ContactType { get; set; }
        public string EventId { get; set; }
        public string VipId { get; set; }
    }
}
