using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using System.Text;
using System.Xml.Serialization;

namespace cPos.Admin.Model.Dex
{
    public class LogInfo
    {
        /// <summary>
        /// 日志ID
        /// </summary>
        public string log_id { get; set; }

        /// <summary>
        /// 业务ID
        /// </summary>
        public string biz_id { get; set; }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string biz_name { get; set; }

        /// <summary>
        /// 日志类型ID
        /// </summary>
        public string log_type_id { get; set; }

        /// <summary>
        /// 日志类型代码
        /// </summary>
        public string log_type_code { get; set; }

        /// <summary>
        /// 日志代码
        /// </summary>
        public string log_code { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string log_body { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string create_user_id { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        public string modify_user_id { get; set; }

        /// <summary>
        /// 客户代码
        /// </summary>
        public string customer_code { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public string customer_id { get; set; }

        /// <summary>
        /// 门店代码
        /// </summary>
        public string unit_code { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public string unit_id { get; set; }

        /// <summary>
        /// 用户代码
        /// </summary>
        public string user_code { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string user_id { get; set; }

        /// <summary>
        /// 接口代码
        /// </summary>
        public string if_code { get; set; }

        /// <summary>
        /// 平台代码
        /// </summary>
        public string app_code { get; set; }
    }
}