using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPIServices.Models
{
     
    public class OMSGExists
    {
        

         public int SequenceID { get; set; }

         public int StatusMSG { get; set; }

         public int StatusMSG2 { get; set; }

        public string InsertOrUpdate { get; set; }
        



    }
}