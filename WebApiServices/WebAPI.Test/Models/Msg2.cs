using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Test.Models
{
     [Table("IF_MSG2")]
    public class MSG2
    {
         [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.None)]
         public int SequenceID { get; set; }

        [MaxLength(50)]
        public string TargetSystem { get; set; }

        public int Status { get; set; }

         public string ErrorMSG { get; set; }

         public DateTime CreateTime { get; set; }
         public DateTime UpdateTime { get; set; }


        [MaxLength(50)]
        public string TargetDB { get; set; }
        [MaxLength(50)]
        public string TargetType  { get; set; }
        [MaxLength(50)]
        public string TargetValue { get; set; }

         //public string CreateUser { get; set; }

    }
}