﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Model
{
    /// <summary>
    /// 订单类型2模板
    /// </summary>
    public class OrderReasonTypeInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string reason_type_id { get; set; }
        /// <summary>
        /// 号码
        /// </summary>
        public string reason_type_code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string reason_type_name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string create_user_id { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string modify_user_id { get; set; }

    }
}
