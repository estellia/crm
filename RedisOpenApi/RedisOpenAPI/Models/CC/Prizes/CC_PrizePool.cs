using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC
{
    public class    CC_PrizePool
    {
        public string CustomerId { get; set; }
        public string EventId { get; set; }
        public string PrizePoolsID { get; set; }
        public string PrizeID { get; set; }
        public string PrizeName { get; set; }
        public int Location { get; set; }
        public string AppearTime { get; set; }
        public int Status { get; set; }
        public string CreateTime { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public int ValidHours { get; set; }

    }
}
