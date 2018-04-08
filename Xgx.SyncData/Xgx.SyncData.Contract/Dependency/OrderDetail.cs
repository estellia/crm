using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xgx.SyncData.Contract
{
    public class OrderDetail
    {
        /// <summary>
        /// 明细编号
        /// </summary>
        public string OrderDetailId { get; set; }
        /// <summary>
        /// SKUID
        /// </summary>
        public string SKUID { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public string UnitId { get; set; }
        /// <summary>
        /// 单据数量
        /// </summary>
        public decimal? OrderQty { get; set; }
        /// <summary>
        /// 输入数量
        /// </summary>
        public decimal? EnterQty { get; set; }
        /// <summary>
        /// 输入单价（如同折扣价）
        /// </summary>
        public decimal? EnterPrice { get; set; }
        /// <summary>
        /// 输入金额
        /// </summary>
        public decimal? EnterAmount { get; set; }
        /// <summary>
        /// 标准单价(建议零售价)/促销价（特价 )  /组合促销分摊价
        /// </summary>
        public decimal? StdPrice { get; set; }
        /// <summary>
        /// 零售价（最终零售价）
        /// </summary>
        public decimal? RetailPrice { get; set; }
        /// <summary>
        /// 零售金额（总金额）
        /// </summary>
        public decimal? RetailAmount { get; set; }
        /// <summary>
        /// 计划价格
        /// </summary>
        public decimal? PlanPrice { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public decimal? ReceiverPoints { get; set; }
        /// <summary>
        /// 支付积分
        /// </summary>
        public decimal? PayPoints { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 终端号码
        /// </summary>
        public string PosOrderCode { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? DisplayIndex { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public string ModifyTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyUser { get; set; }
        /// <summary>
        /// 返现
        /// </summary>
        public decimal? ReturnCash { get; set; }
    }
}
