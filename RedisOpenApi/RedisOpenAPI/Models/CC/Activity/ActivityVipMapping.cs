using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC.Activity {
	public class ActivityVipMapping {
		public string CustomerId { get; set; }

		public string ActivityId { get; set; }
		public string VipId { get; set; }
	}
}
