using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIServices.Models
{
    public class SKUOnHand
    {
        public string ItemCode { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public decimal OnHand { get; set; }
        public string InvntryUom { get; set; }

    }
}