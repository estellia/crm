using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model
{
    /// <summary>
    /// 订单查询条件model
    /// </summary>
    public class OrderSearchInfo
    {
        /// <summary>
        /// 订单标识
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 订单类型标识1
        /// </summary>
        public string order_type_id { get; set; }
        /// <summary>
        /// 订单类型标识2
        /// </summary>
        public string order_reason_id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 组织标识
        /// </summary>
        public string unit_id { get; set; }
        /// <summary>
        /// 组织代码
        /// </summary>
        public string unit_code { get; set; }
        /// <summary>
        /// 商品名称或者号码
        /// </summary>
        public string item_name { get; set; }
        /// <summary>
        /// 订单日期开始
        /// </summary>
        public string order_date_begin { get; set; }
        /// <summary>
        /// 订单日期结束
        /// </summary>
        public string order_date_end { get; set; }
        /// <summary>
        /// 查询人标识
        /// </summary>
        public string loging_user_id { get; set; }
        /// <summary>
        /// 状态标识
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 完成日期开始【yyyy-MM-dd】
        /// </summary>
        public string complete_date_begin { get; set; }
        /// <summary>
        /// 完成日期结束【yyyy-MM-dd】
        /// </summary>
        public string complete_date_end { get; set; }
        /// <summary>
        /// 仓库标识
        /// </summary>
        public string warehouse_id { get; set; }
        /// <summary>
        /// 数据来源
        /// </summary>
        public string data_from_id { get; set; }
        /// <summary>
        /// 上级单据号码
        /// </summary>
        public string ref_order_no { get; set; }
        /// <summary>
        /// 销售单位
        /// </summary>
        public string sales_unit_id { get; set; }
        /// <summary>
        /// 采购单位
        /// </summary>
        public string purchase_unit_id { get; set; }
        /// <summary>
        /// 开始行
        /// </summary>
        public int StartRow { get; set; }
        /// <summary>
        /// 结束行
        /// </summary>
        public int EndRow { get; set; }
        /// <summary>
        /// 客户标识
        /// </summary>
        public string customer_id { get; set; }
    }
}
