using System;
using Dapper.Contrib.Extensions;

namespace Xgx.SyncData.DbEntity
{
    /// <summary>
    /// 订单状态信息记录  
    /// </summary>
    [Table("TInoutStatus")]
    public class TInoutStatusEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TInoutStatusEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
        /// ID
		/// </summary>
		public Guid? InoutStatusID { get; set; }

		/// <summary>
        /// 订单ID
		/// </summary>
		public string OrderID { get; set; }

		/// <summary>
        /// 订单状态
		/// </summary>
		public int? OrderStatus { get; set; }

		/// <summary>
        /// 未审核理由
		/// </summary>
		public int? CheckResult { get; set; }

		/// <summary>
        /// 支付方式
		/// </summary>
		public int? PayMethod { get; set; }

		/// <summary>
        /// 配送公司
		/// </summary>
		public string DeliverCompanyID { get; set; }

		/// <summary>
        /// 配送单号
		/// </summary>
		public string DeliverOrder { get; set; }

		/// <summary>
        /// 图片
		/// </summary>
		public string PicUrl { get; set; }

		/// <summary>
        /// 备注
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
        /// 客户ID
		/// </summary>
		public string CustomerID { get; set; }

		/// <summary>
        /// 创建人
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
        /// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
        /// 最后修改人
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
        /// 最后修改时间
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
        /// 是否删除
		/// </summary>
		public int? IsDelete { get; set; }

        /// <summary>
        /// 订单状态描述
        /// </summary>
        public string StatusRemark { get; set; }

        #endregion

    }
}