using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisOpenAPIClient.Models.CC
{
    public class CC_Coupon
    {
        public string CustomerId { get; set; }
        public string CouponTypeId { get; set; }
        /// <summary>
        /// 优惠券描述
        /// </summary>
        public string CouponTypeDesc { get; set; }
        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string CouponTypeName { get; set; }
        /// <summary>
        /// 有效期开始时间
        /// </summary>
        public string BeginTime { get; set; }
        /// <summary>
        /// 有效期结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 相对有效期
        /// </summary>
        public int ServiceLife { get; set; }
        /// <summary>
        /// 优惠券码
        /// </summary>
        public string CouponCode { get; set; }
        /// <summary>
        /// 优惠券标识
        /// </summary>
        public string CouponId { get; set; }
        /// <summary>
        /// 优惠券类型
        /// </summary>
        public string CouponCategory { get; set; }
        /// <summary>
        /// 优惠券个数
        /// </summary>
        public int CouponLenth { get; set; }
    }
}
