using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebAPIServices.Models
{
    [Table("IF_OMSG_Log")]
    public class OMSGLog
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; set; }
        public int SequenceID { get; set; }
        [Required]

        public DateTime Timestamp { get; set; }
        [MaxLength(50)]
        public string FromSystem { get; set; }
        [MaxLength(50)]
        public string FromCompany { get; set; }
        [MaxLength(10)]
        public string Flag { get; set; }
        [MaxLength(50)]
        public string ObjectType { get; set; }
        [MaxLength(50)]
        public string TransType { get; set; }

        public int FieldsInKey { get; set; }
        [MaxLength(100)]
        public string FieldNames { get; set; }
        [MaxLength(100)]
        public string FieldValues { get; set; }

        public int Status { get; set; }

        public string Comments { get; set; }
        public DateTime UpdateTime { get; set; }
        [MaxLength(50)]
        public string UpdateUser { get; set; }
    }
}