using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPIServices.Models
{
    [Table("IF_MSG1")]
    public class MSG1
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SequenceID { get; set; }
        public string Content { get; set; }

        public int iLength { get; set; }

        //public string CreateUser { get; set; }
    }
}