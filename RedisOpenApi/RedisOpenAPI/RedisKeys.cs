using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using RedisOpenAPIClient.MethodExtensions.EnumExtensions;

namespace RedisOpenAPIClient
{
    /// <summary>
    /// Key 命名空间
    /// </summary>
    public class RedisKeys
    {


        /// <summary>
        /// CPOS / 虚拟商品发优惠券成功后发送通知 / OrderMessage.CustomerID.PaySuccess
        /// </summary>
        public static string CCCouponNotice(string customerID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.CouponNotice.ToEnumDesc<PreFix>(), 
                customerID.ToLower().Trim()
               
            });
        }
        /// <summary>
        /// CPOS / 角色菜单 / Role.CustomerID.RoleID
        /// </summary>
        public static string CCMenuRoleKey(string customerID, string roleID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.Role.ToEnumDesc<PreFix>(), 
                customerID.ToLower().Trim(), 
                roleID.ToLower().Trim() 
            });
        }

        /// <summary>
        /// CPOS / 订单支付完成 / OrderMessage.CustomerID.PaySuccess
        /// </summary>
        public static string CCPaySuccess(string customerID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.OrderMessage.ToEnumDesc<PreFix>(), 
                customerID.ToLower().Trim(), 
                PostFix.PaySuccess.ToEnumDesc<PostFix>() 
            });
        }

        /// <summary>
        /// CPOS / 商户数据库链接相关 / Connection.CustomerID
        /// </summary>
        public static string CCConnectionKey(string customerID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.Connection.ToEnumDesc<PreFix>(), 
                customerID.ToLower().Trim() 
            });
        }

        /// <summary>
        /// CPOS / 订单发货完成 / OrderMessage.CustomerID.Send
        /// </summary>
        public static string CCOrderSend(string customerID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.OrderMessage.ToEnumDesc<PreFix>(), 
                customerID.ToLower().Trim(), 
                PostFix.Send.ToEnumDesc<PostFix>() 
            });
        }

        /// <summary>
        /// CPOS / 确认收货时处理积分、返现、佣金 / OrderMessage.CustomerID.Reward
        /// </summary>
        public static string CCOrderReward(string customerID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.OrderMessage.ToEnumDesc<PreFix>(), 
                customerID.ToLower().Trim(), 
                PostFix.Reward.ToEnumDesc<PostFix>() 
            });
        }

        /// <summary>
        /// CPOS / 超级分销商订单 / SuperRetailTraderOrder.CustomerID
        /// </summary>
        public static string CCSuperRetailTraderOrder(string strCustomerID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.SuperRetailTraderOrder.ToEnumDesc<PreFix>(), 
                strCustomerID.ToLower().Trim() 
            });
        }

        /// <summary>
        /// CPOS / 优惠券 / Coupon.CustomerID.CouponTypeID
        /// </summary>
        public static string CCCouponKey(string strCustomerId, string CouponTypeID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.Coupon.ToEnumDesc<PreFix>(), 
                strCustomerId.ToLower().Trim(), 
                CouponTypeID.ToLower().Trim() 
            });
        }

        /// <summary>
        /// CPOS / VIP绑定Coupon队列 / VipMappingCoupon.CustomerID
        /// </summary>
        public static string CCVipMappingCouponKey(string strCustomerId)
        {
            return string.Concat(new string[] 
            { 
                PreFix.VipMappingCoupon.ToEnumDesc<PreFix>(), 
                strCustomerId.ToLower().Trim() 
            });
        }

        /// <summary>
        /// CPOS / 奖品缓存 / Prize.CustomerID.EventID
        /// </summary>
        public static string CCPrizeKey(string strCustomerId, string strEventId)
        {
            return string.Concat(new string[] 
            { 
                PreFix.Prize.ToEnumDesc<PreFix>(), 
                strCustomerId.ToLower().Trim(), 
                strEventId.ToLower().Trim() 
            });
        }

        /// <summary>
        /// CPOS / 奖品池队列 / PrizePools.CustomerID.EventID
        /// </summary>
        public static string CCPrizePoolsKey(string strCustomerId, string strEventId)
        {
            return string.Concat(new string[] 
            { 
                PreFix.PrizePools.ToEnumDesc<PreFix>(), 
                strCustomerId.ToLower().Trim(), 
                strEventId.ToLower().Trim() 
            });
        }

        /// <summary>
        /// CPOS / 注册/关注-触点业务 / Contact.CustomerID
        /// </summary>
        public static string CCContactKey(string strCustomerId)
        {
            return string.Concat(new string[] 
            { 
                PreFix.Contact.ToEnumDesc<PreFix>(), 
                strCustomerId.ToLower().Trim() 
            });
        }

        /// <summary>
        /// CPOS / 商户配置信息 / BasicSetting.CustomerID
        /// </summary>
        public static string CCBasicSetting(string strCustomerId)
        {
            return string.Concat(new string[] 
            { 
                PreFix.BasicSetting.ToEnumDesc<PreFix>(), 
                strCustomerId.ToLower().Trim() 
            });
        }

        /// <summary>
        /// BI / 记名埋点 / BI.Burying.User.Key
        /// </summary>
        public static string BIBuryingKey
        {
            get
            {
                return PreFix.BI.ToEnumDesc<PreFix>() + ".Burying.User.Key";
            }
        }

        /// <summary>
        /// CPOS / 订单未付款模板消息队列的键 / OrderMessage.CustomerID.NotPay
        /// </summary>
        public static string CCOrderNotPay(string customerID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.OrderMessage.ToEnumDesc<PreFix>(), 
                customerID.ToLower().Trim(), 
                PostFix.NotPay.ToEnumDesc<PostFix>() 
            });
        }

        /// <summary>
        /// CPOS / 优惠券到期消息模板 / Coupon.CustomerID.CouponToBeExpired
        /// </summary>
        public static string CCCouponToBeExpired(string customerID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.Coupon.ToEnumDesc<PreFix>(), 
                customerID.ToLower().Trim(), 
                PostFix.CouponToBeExpired.ToEnumDesc<PostFix>() 
            });
        }

        /// <summary>
        /// CPOS / 订单状态信息 / OrderMessage.CustomerID.PushMessage
        /// </summary>
        public static string CCOrderPushMessageKey(string customerID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.OrderMessage.ToEnumDesc<PreFix>(), 
                customerID.ToLower().Trim(), 
                PostFix.PushMessage.ToEnumDesc<PostFix>() 
            });
        }

        /// <summary>
        /// CPOS / 售卡订单 / OrderMessage.CustomerID.SalesCardOrder
        /// </summary>
        public static string CCSalesCardOrder(string customerID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.OrderMessage.ToEnumDesc<PreFix>(), 
                customerID.ToLower().Trim(), 
                PostFix.SalesCardOrder.ToEnumDesc<PostFix>() 
            });
        }

        /// <summary>
        /// CPOS / 充值订单 / OrderMessage.CustomerID.RechargeOrder
        /// </summary>
        public static string CCRechargeOrder(string customerID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.OrderMessage.ToEnumDesc<PreFix>(), 
                customerID.ToLower().Trim(), 
                PostFix.RechargeOrder.ToEnumDesc<PostFix>() 
            });
        }

        /// <summary>
        /// CPOS / 所有订单 / OrderMessage.CustomerID.AllOrder
        /// </summary>
        public static string CCAllOrder(string customerID)
        {
            return string.Concat(new string[] 
            { 
                PreFix.OrderMessage.ToEnumDesc<PreFix>(), 
                customerID.ToLower().Trim(), 
                PostFix.AllOrder.ToEnumDesc<PostFix>() 
            });
        }
		public static string CCActivity(string customerID,string activityId) {
			return string.Concat(new string[]
			{
				PreFix.Activity.ToEnumDesc<PreFix>(),
				customerID.ToLower().Trim(),
				activityId.ToLower().Trim()
			});
		}
	}

    /// <summary>
    /// Key 前缀
    /// </summary>
    public enum PreFix
    {
        /// <summary>
        /// 角色菜单 前缀
        /// </summary>
        [Description("Role")]
        Role,

        /// <summary>
        /// 优惠券 前缀
        /// </summary>
        [Description("Coupon")]
        Coupon,

        /// <summary>
        /// 虚拟商品发优惠券成功后，推送消息 前缀
        /// </summary>
        [Description("CouponNotice")]
        CouponNotice,

        /// <summary>
        /// vip绑定Coupon 前缀
        /// </summary>
        [Description("VipMappingCoupon")]
        VipMappingCoupon,

        /// <summary>
        /// 奖品 前缀 
        /// </summary>
        [Description("Prize")]
        Prize,

        /// <summary>
        /// 奖品池 前缀 
        /// </summary>
        [Description("PrizePools")]
        PrizePools,

        /// <summary>
        /// 触点 前缀 
        /// </summary>
        [Description("Contact")]
        Contact,

        /// <summary>
        /// 商户数据库链接相关 前缀
        /// </summary>
        [Description("ConnectionTest")]
        Connection,

        /// <summary>
        /// 订单队列 前缀
        /// </summary>
        [Description("OrderMessage")]
        OrderMessage,

        /// <summary>
        /// BasicSetting缓存 前缀
        /// </summary>
        [Description("BasicSetting")]
        BasicSetting,

        /// <summary>
        /// 超级分销商 前缀
        /// </summary>
        [Description("SuperRetailTraderOrder")]
        SuperRetailTraderOrder,

        /// <summary>
        /// 报表 前缀
        /// </summary>
        [Description("BI")]
        BI,
		/// <summary>
		/// 活动 前缀
		/// </summary>
		[Description("Activity")]
		Activity

	}

    /// <summary>
    /// Key 后缀
    /// </summary>
    public enum PostFix
    {
        /// <summary>
        /// 订单支付成功 后缀
        /// </summary>
        [Description("PaySuccess")]
        PaySuccess,

        /// <summary>
        /// APP/后台订单发货 后缀
        /// </summary>
        [Description("Send")]
        Send,

        /// <summary>
        /// APP/后台 确认收货/完成订单 - 处理奖励  后缀
        /// </summary>
        [Description("Reward")]
        Reward,

        /// <summary>
        /// APP/订单未付款 后缀
        /// </summary>
        [Description("NotPay")]
        NotPay,

        /// <summary>
        /// 优惠券即将到期 后缀
        /// </summary>
        [Description("CouponToBeExpired")]
        CouponToBeExpired,

        /// <summary>
        /// 根据订单状态发送信息  后缀
        /// </summary>
        [Description("PushMessage")]
        PushMessage,

        /// <summary>
        /// 售卡订单
        /// </summary>
        [Description("SalesCardOrder")]
        SalesCardOrder,

        /// <summary>
        /// 充值订单
        /// </summary>
        [Description("RechargeOrder")]
        RechargeOrder,

        /// <summary>
        /// 所有订单
        /// </summary>
        [Description("AllOrder")]
        AllOrder


    }
}
