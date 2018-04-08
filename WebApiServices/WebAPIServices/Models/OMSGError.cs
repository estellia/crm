using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIServices.Models
{
    public class OMSGError
    {
        public int SequenceID { get; set; }

        public DateTime Timestamp { get; set; }

        public string FromCompany { get; set; }

        public string FromSystem { get; set; }

        public string Flag { get; set; }

        public string ObjectType { get; set; }

        public string ObjectName { get; set; }

        public string TransType { get; set; }

        public string FieldNames { get; set; }

        public string FieldValues { get; set; }

        public int Status { get; set; }
        public string ErrorMsg { get; set; }
    }
}