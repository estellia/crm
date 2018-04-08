using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xgx.SyncData.Contract.Dependency;
using Xgx.SyncData.Contract.Enum;

namespace Xgx.SyncData.Contract
{
    public class OrderContract : IXgxToZmind, IZmindToXgx
    {
        /// <summary>
        /// 增删改标志
        /// </summary>
        public OptEnum Operation { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public EnumOrderType OrderType { get; set; }
        /// <summary>
        /// 订单原因
        /// </summary>
        public EnumOrderReason OrderReason { get; set; }
        /// <summary>
        /// 订单日期
        /// </summary>
        public string OrderDate { get; set; }
        /// <summary>
        /// 服务时间
        /// </summary>
        public string RequestDate { get; set; }
        /// <summary>
        /// 服务时间段
        /// </summary>
        public string RequestDateQuantum { get; set; }
        /// <summary>
        /// 完成日期/支付日期
        /// </summary>
        public string CompleteDate { get; set; }
        /// <summary>
        /// 创建单位
        /// </summary>
        public string CreateUnit { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal? TotalAmount { get; set; }
        /// <summary>
        /// 销售折扣（折上折）
        /// </summary>
        public decimal? DiscountRate { get; set; }
        /// <summary>
        /// 实际支付金额
        /// </summary>
        public decimal? ActualAmount { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public decimal? ReceivePoints { get; set; }
        /// <summary>
        /// 支付积分
        /// </summary>
        public decimal? PayPoints { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EnumOrderStatus Status { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public decimal? TotalQty { get; set; }
        /// <summary>
        /// 零售总价
        /// </summary>
        public decimal? TotalRetail { get; set; }
        /// <summary>
        /// 找零
        /// </summary>
        public decimal? KeepTheChange { get; set; }
        /// <summary>
        /// 抹零/折扣额
        /// </summary>
        public decimal? WipingZero { get; set; }
        /// <summary>
        /// 会员信息
        /// </summary>
        public string VipNo { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// CreateUserId
        /// </summary>
        public string CreateUserId { get; set; }
        /// <summary>
        /// ApproveTime
        /// </summary>
        public string ApproveTime { get; set; }
        /// <summary>
        /// ApproveUserId
        /// </summary>
        public string ApproveUserId { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string SendUserId { get; set; }
        /// <summary>
        /// 发送时间--预约时间
        /// </summary>
        public string SendTime { get; set; }
        /// <summary>
        /// 验收人
        /// </summary>
        public string AccpectUserId { get; set; }
        /// <summary>
        /// 验收日期
        /// </summary>
        public string AccpectTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string ModifyTime { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public EnumOrderDataFrom DataFrom { get; set; }
        /// <summary>
        /// 销售单位
        /// </summary>
        public string SalesUnt { get; set; }
        /// <summary>
        /// 采购单位
        /// </summary>
        public string PurchaseUnit { get; set; }
        /// <summary>
        /// 销售仓库
        /// </summary>
        public string SalesWarehouse { get; set; }
        /// <summary>
        /// 采购仓库
        /// </summary>
        public string PurchaseWarehouse { get; set; }
        /// <summary>
        /// 是否付款
        /// </summary>
        public EnumIsPay IsPay { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string TrackingNumber { get; set; }
        /// <summary>
        /// 余额支付
        /// </summary>
        public string BalancePayment { get; set; }
        /// <summary>
        /// 配送地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 配送方式
        /// </summary>
        public EnumDelivery Delivery { get; set; }
        /// <summary>
        /// 配送时间/发货时间/服务时间
        /// </summary>
        public string DeliveryDateTime { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 回退现金
        /// </summary>
        public decimal? CashBack { get; set; }
        /// <summary>
        /// 明细信息列表
        /// </summary>
        public List<OrderDetail> DetailList { get; set; }


    }
}
