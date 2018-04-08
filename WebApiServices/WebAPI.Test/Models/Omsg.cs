using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebAPI.Test.Models
{
    /// <summary>
    /// 消息对象
    /// </summary>
    [Table("IF_OMSG")]
    public class OMSG
    {
        /// <summary>
        /// 消息标识
        /// </summary>
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SequenceID { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        [Required]       
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 消息来源系统
        /// ZMIND/ERP/PCShop
        /// </summary>
        [MaxLength(50)]
        public string FromSystem { get; set; }
        /// <summary>
        /// 消息来源公司，default = FromSystem
        /// </summary>
        [MaxLength(50)]
        public string FromCompany { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        [MaxLength(10)]
        public string Flag { get; set; }
        /// <summary>
        /// 消息对象101-116
        /// </summary>
        [MaxLength(50)]
        public string ObjectType { get; set; }
        /// <summary>
        /// 消息操作类型A增加/U更新/D删除
        /// </summary>
        [MaxLength(50)]
        public string TransType { get; set; }
        /// <summary>
        /// 对象主键个数
        /// </summary>
        public int FieldsInKey { get; set; }
        /// <summary>
        /// 对象主键名称
        /// </summary>
        [MaxLength(100)]
        public string FieldNames { get; set; }
        /// <summary>
        /// 对象主键值
        /// </summary>
        [MaxLength(100)]
        public string FieldValues { get; set; }

        public int Status { get; set; }

        public string Comments { get; set; }
        public DateTime UpDateTime { get; set; }

        //public string CreateUser { get; set; }
    }
}