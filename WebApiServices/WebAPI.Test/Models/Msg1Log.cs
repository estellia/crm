using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Test.Models
{
    [Table("IF_MSG1_Log")]
    public class MSG1Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; set; }
        public int SequenceID { get; set; }
        public string Content { get; set; }

        public int iLength { get; set; }

        public DateTime UpdateTime { get; set; }
        [MaxLength(50)]
        public string UpdateUser { get; set; }
    }
}