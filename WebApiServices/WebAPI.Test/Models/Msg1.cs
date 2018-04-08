using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI.Test.Models
{
    /// <summary>
    /// 原始消息对象
    /// </summary>
    [Table("IF_MSG1")]
    public class MSG1
    {
        /// <summary>
        /// 消息标识
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SequenceID { get; set; }
        /// <summary>
        /// 消息内容，Json
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息长度
        /// </summary>
        public int iLength { get; set; }

        //public string CreateUser { get; set; }
    }
}