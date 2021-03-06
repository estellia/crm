﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cPos.Admin.Model
{
    /// <summary>
    /// 进出库模板
    /// </summary>
    [Serializable]
    public class InoutInfo
    {
        /// <summary>
        /// 入出库单主标识 (必须)
        /// </summary>
        public string order_id { get;set; }
        /// <summary>
        /// 订单号(必须)
        /// </summary>
        public string order_no { get;set; }
        /// <summary>
        /// 订单类型标识(必须)
        /// </summary>
        public string order_type_id { get;set; }
        /// <summary>
        /// 订单类型理由标识(必须)
        /// </summary>
        public string order_reason_id { get;set; }
        /// <summary>
        /// 红单标识（-1=红单，1=正单）(必须)
        /// </summary>
        public string red_flag { get;set; }
        /// <summary>
        /// 原单据标识
        /// </summary>
        public string ref_order_id { get;set; }
        /// <summary>
        /// 原单据号码
        /// </summary>
        public string ref_order_no { get;set; }
        /// <summary>
        /// 仓库标识
        /// </summary>
        public string warehouse_id { get;set; }
        /// <summary>
        /// 订单日期(必须)
        /// </summary>
        public string order_date { get;set; }
        /// <summary>
        /// 要求完成日期
        /// </summary>
        public string request_date { get;set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string complete_date { get;set; }
        /// <summary>
        /// 创建单位(必须)
        /// </summary>
        public string create_unit_id { get;set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string unit_id { get;set; }
        /// <summary>
        /// 调整单位名称
        /// </summary>
        public string unit_name { get; set; }
        /// <summary>
        /// 相关单位标识
        /// </summary>
        public string related_unit_id { get;set; }
        /// <summary>
        /// 相关单位号码
        /// </summary>
        public string related_unit_code { get;set; }
        /// <summary>
        /// 终端标识
        /// </summary>
        public string pos_id { get;set; }
        /// <summary>
        /// 班次标识
        /// </summary>
        public string shift_id { get;set; }
        /// <summary>
        /// 销售人员
        /// </summary>
        public string sales_user { get;set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal total_amount { get;set; }
        /// <summary>
        /// 折扣
        /// </summary>
        public decimal discount_rate { get; set; }
        /// <summary>
        /// 实际金额
        /// </summary>
        public decimal actual_amount { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public decimal receive_points { get; set; }
        /// <summary>
        /// 支付积分
        /// </summary>
        public decimal pay_points { get; set; }
        /// <summary>
        /// 支付标识
        /// </summary>
        public string pay_id { get; set; }
        /// <summary>
        /// 支付方式名称【显示】
        /// </summary>
        public string payment_name { get; set; }
        /// <summary>
        /// 打印次数
        /// </summary>
        public int print_times { get; set; }
        /// <summary>
        /// 承运商标识
        /// </summary>
        public string carrier_id { get;set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get;set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string status { get;set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        public string status_desc { get;set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public decimal total_qty { get; set; }
        /// <summary>
        /// 零售总价
        /// </summary>
        public decimal total_retail { get; set; }
        /// <summary>
        /// 找零
        /// </summary>
        public decimal keep_the_change { get; set; }
        /// <summary>
        /// 抹零
        /// </summary>
        public decimal wiping_zero { get; set; }
        /// <summary>
        /// vip卡号
        /// </summary>
        public string vip_no { get;set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string create_time { get;set; }
        /// <summary>
        /// 创建人标识
        /// </summary>
        public string create_user_id { get;set; }
        /// <summary>
        /// 审批时间
        /// </summary>
        public string approve_time { get;set; }
        /// <summary>
        /// 审批人标识
        /// </summary>
        public string approve_user_id { get;set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string send_time { get;set; }
        /// <summary>
        /// 发送人标识
        /// </summary>
        public string send_user_id { get;set; }
        /// <summary>
        /// 验收时间
        /// </summary>
        public string accpect_time { get;set; }
        /// <summary>
        /// 验收人标识
        /// </summary>
        public string accpect_user_id { get;set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string modify_time { get;set; }
        /// <summary>
        /// 修改人标识
        /// </summary>
        public string modify_user_id { get; set; }
        /// <summary>
        /// 创建单位名称
        /// </summary>
        public string create_unit_name { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string create_user_name { get; set; }
        /// <summary>
        /// 审批人名称
        /// </summary>
        public string approve_user_name { get; set; }
        /// <summary>
        /// 发送人名称
        /// </summary>
        public string send_user_name { get; set; }
        /// <summary>
        /// 验收人名称
        /// </summary>
        public string accpect_user_name { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string modify_user_name { get; set; }
        /// <summary>
        /// 订单类型1号码
        /// </summary>
        public string order_type_code { get; set; }
        /// <summary>
        /// 订单类型1名称
        /// </summary>
        public string order_type_name { get; set; }
        /// <summary>
        /// 订单类型2号码
        /// </summary>
        public string order_reason_code { get; set; }
        /// <summary>
        /// 订单类型2名称
        /// </summary>
        public string order_reason_name { get; set; }
        /// <summary>
        /// 销售单位标识
        /// </summary>
        public string sales_unit_id { get; set; }
        /// <summary>
        /// 采购单位标识
        /// </summary>
        public string purchase_unit_id { get; set; }
        /// <summary>
        /// 销售单位名称
        /// </summary>
        public string sales_unit_name { get; set; }
        /// <summary>
        /// 采购单位名称
        /// </summary>
        public string purchase_unit_name { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string warehouse_name { get; set; }

        /// <summary>
        /// inout 明细集合
        /// </summary>
        public IList<InoutDetailInfo> InoutDetailList { get; set; }
        /// <summary>
        /// inout集合
        /// </summary>
        public IList<InoutInfo> InoutInfoList { get; set; }
        /// <summary>
        /// 表单类型(必须)
        /// </summary>
        public string BillKindCode { get; set; }
        /// <summary>
        /// 单据来源标识(必须)
        /// </summary>
        public string data_from_id { get; set; }
        /// <summary>
        /// 单据来源名称
        /// </summary>
        public string data_from_name { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int Row_No { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int ICount { get; set; }
        /// <summary>
        /// 操作 新建=“Create”，修改=“Modify”
        /// </summary>
        public string operate { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        private string _if_flag = "0";
        /// <summary>
        /// 是否下载标识
        /// </summary>
        public string if_flag { get { return _if_flag; } set { _if_flag = value; } }
    }
}
