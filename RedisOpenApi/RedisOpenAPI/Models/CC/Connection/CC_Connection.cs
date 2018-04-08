using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC
{
    public class CC_Connection
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string Customer_Name { get; set; }
        /// <summary>
        /// 商户编号
        /// </summary>
        public string Customer_Code { get; set; }
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionStr { get; set; }
    }
}
