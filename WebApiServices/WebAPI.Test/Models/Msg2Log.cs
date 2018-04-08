using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Test.Models
{
    [Table("IF_MSG2_Log")]
    public class MSG2Log
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int SequenceID { get; set; }

        [MaxLength(50)]
        public string TargetSystem { get; set; }

        public int Status { get; set; }

        public string ErrorMSG { get; set; }

        public DateTime CreateTime { get; set; }
      
        public string MSGOriContent { get; set; }

        public string MSGJXContent { get; set; }
        [MaxLength(50)]
        public string TargetDB { get; set; }
        [MaxLength(50)]
        public string TargetType { get; set; }
        [MaxLength(50)]
        public string TargetValue { get; set; }
        public string Remark { get; set; }
        [MaxLength(50)]

        public string UpdateUser { get; set; }

    }
}