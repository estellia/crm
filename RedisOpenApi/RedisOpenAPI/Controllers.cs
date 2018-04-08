using RedisOpenAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RedisOpenAPIClient.MethodExtensions.ObjectExtensions;
using RedisOpenAPIClient.Models.BI;
using RedisOpenAPIClient.Models.CC;
using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using RedisOpenAPIClient.Models.CC.OrderSend;
using RedisOpenAPIClient.Models.CC.OrderReward;
using RedisOpenAPIClient.Models.CC.Order;
using RedisOpenAPIClient.Models.CC.OrderNotPay;
using RedisOpenAPIClient.Models.CC.CouponToBeExpired;
using RedisOpenAPIClient.Models.CC.OrderPushMessage;
using RedisOpenAPIClient.Models.CC.Activity;
using RedisOpenAPIClient.Models.CC.CouponNotice;

namespace RedisOpenAPIClient
{
    public static class Controllers
    {
        //
        // LM,2016/04/19
        // 请在此处添加 开放的 API
        //

        /// <summary>
        /// CPOS / 后台 / 优惠券发送成功 / Queue
        /// </summary>
        public static CCCouponNotice CCCouponNotice(this RedisOpenAPI remote)
        {
            remote.Controller = "CCCouponNotice";
            return new CCCouponNotice(remote);
        }

        /// <summary>
        /// CPOS / 后台 / 订单支付成功 / Queue
        /// </summary>
        public static CCOrderPaySuccess CCOrderPaySuccess(this RedisOpenAPI remote)
        {
            remote.Controller = "CCOrderPaySuccess";
            return new CCOrderPaySuccess(remote);
        }


      
        /// <summary>
        /// APP/后台订单发货-发送微信模板消息 / Queue
        /// </summary>
        public static CCOrderSend CCOrderSend(this RedisOpenAPI remote)
        {
            remote.Controller = "CCOrderSend";
            return new CCOrderSend(remote);
        }
        /// <summary>
        /// 确认收货时处理积分、返现、佣金 / Queue
        /// </summary>
        public static CCOrderReward CCOrderReward(this RedisOpenAPI remote)
        {
            remote.Controller = "CCOrderReward";
            return new CCOrderReward(remote);
        }

        /// <summary>
        /// CPOS / 后台 / 角色菜单 / Cache
        /// </summary>
        public static CCRole CCRole(this RedisOpenAPI remote)
        {
            remote.Controller = "CCRole";
            return new CCRole(remote);
        }

        /// <summary>
        /// BI / 用户行为 / 记名埋点 / Cache
        /// </summary>
        public static BIStatistic BIStatistic(this RedisOpenAPI remote)
        {
            remote.Controller = "BIStatistic";
            return new BIStatistic(remote);
        }
        public static CCCoupon CCCoupon(this RedisOpenAPI remote)
        {
            remote.Controller = "CCCoupon";
            return new CCCoupon(remote);
        }

        public static CCConnection CCConnection(this RedisOpenAPI remote)
        {
            remote.Controller = "CCConnection";
            return new CCConnection(remote);
        }
        public static CCVipMappingCoupon CCVipMappingCoupon(this RedisOpenAPI remote)
        {
            remote.Controller = "CCVipMappingCoupon";
            return new CCVipMappingCoupon(remote);
        }
        public static CCContact CCContact(this RedisOpenAPI remote)
        {
            remote.Controller = "CCContact";
            return new CCContact(remote);
        }
        public static CCBasicSetting CCBasicSetting(this RedisOpenAPI remote)
        {
            remote.Controller = "CCBasicSetting";
            return new CCBasicSetting(remote);
        }
        public static CCPrizePools CCPrizePools(this RedisOpenAPI remote)
        {
            remote.Controller = "CCPrizePools";
            return new CCPrizePools(remote);
        }
        public static CCSuperRetailTraderOrder CCSuperRetailTraderOrder(this RedisOpenAPI remote)
        {
            remote.Controller = "CCSuperRetailTraderOrder";
            return new CCSuperRetailTraderOrder(remote);
        }

        //增加工厂类对具体实现类的引用(这里定义的都是RedisOpenAPI的静态方法)
        //未支付订单，发送模板消息
        public static CCOrderNotPay CCOrderNotPay(this RedisOpenAPI remote)
        {
            remote.Controller = "CCOrderNotPay";//要访问的Controller，传递给remote（RedisOpenAPI）
            return new CCOrderNotPay(remote);
        }


        public static CCCouponToBeExpired CCCouponToBeExpired(this RedisOpenAPI remote)
        {
            remote.Controller = "CCCouponToBeExpired";//要访问的Controller，传递给remote（RedisOpenAPI）
            return new CCCouponToBeExpired(remote);
        }


        /// <summary>
        /// CPOS / 后台 / 根据订单状态发消息 / Queue
        /// </summary>
        public static CCOrderPushMessage CCOrderPushMessage(this RedisOpenAPI remote)
        {
            remote.Controller = "CCOrderPushMessage";
            return new CCOrderPushMessage(remote);
        }

        /// <summary>
        /// CPOS / 后台 / 售卡订单 / Queue
        /// </summary>
        public static CCSalesCardOrder CCSalesCardOrder(this RedisOpenAPI remote)
        {
            remote.Controller = "CCSalesCardOrder";
            return new CCSalesCardOrder(remote);
        }

        /// <summary>
        /// CPOS / 后台 / 充值订单 / Queue
        /// </summary>
        public static CCRechargeOrder CCRechargeOrder(this RedisOpenAPI remote)
        {
            remote.Controller = "CCRechargeOrder";
            return new CCRechargeOrder(remote);
        }

        /// <summary>
        /// CPOS / 后台 / 所有订单 / Queue
        /// </summary>
        public static CCAllOrder CCAllOrder(this RedisOpenAPI remote)
        {
            remote.Controller = "CCAllOrder";
            return new CCAllOrder(remote);
        }
		/// <summary>
		/// cpos /后台/ 活动/ set
		/// </summary>
		/// <param name="remote"></param>
		/// <returns></returns>
		public static CCActivity CCActivity(this RedisOpenAPI remote) {
			remote.Controller = "CCActivity";
			return new CCActivity(remote);
		}
		
	}

    /// <summary>
    /// CPOS / 后台 /优惠券发送通知 / Queue
    /// </summary>
    public class CCCouponNotice : ControllerBase
    {
        public CCCouponNotice(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 支付成功消息 入队列
        /// </summary>
        public ResponseModel SetCouponNotice(CC_CouponNotice request)
        {
            base.Remote.Action = "SetCouponNotice";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 支付成功消息 出队列
        /// </summary>
        public ResponseModel<CC_CouponNotice> GetCouponNotice(CC_CouponNotice request)
        {
            base.Remote.Action = "GetCouponNotice";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_CouponNotice>>();
        }

        /// <summary>
        /// 支付成功消息 队列长度
        /// </summary>
        public ResponseModel<long> GetCouponNoticeLength(CC_CouponNotice request)
        {
            base.Remote.Action = "GetCouponNoticeLength";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

    }





    /// <summary>
    /// CPOS / 后台 / 所有订单 / Queue
    /// </summary>
    public class CCAllOrder : ControllerBase
    {
        public CCAllOrder(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 所有订单 入队列
        /// </summary>
        public ResponseModel SetOrder(CC_Order request)
        {
            base.Remote.Action = "SetOrder";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 所有订单 出队列
        /// </summary>
        public ResponseModel<CC_Order> GetOrder(CC_Order request)
        {
            base.Remote.Action = "GetOrder";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_Order>>();
        }

        /// <summary>
        /// 所有订单 队列长度
        /// </summary>
        public ResponseModel<long> GetOrderLength(CC_Order request)
        {
            base.Remote.Action = "GetOrderLength";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

        public ResponseModel DeleteOrderList(CC_Order request)
        {
            base.Remote.Action = "DeleteOrderList";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

    }

    /// <summary>
    /// CPOS / 后台 / 售卡订单 / Queue
    /// </summary>
    public class CCSalesCardOrder : ControllerBase
    {
        public CCSalesCardOrder(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 售卡订单 入队列
        /// </summary>
        public ResponseModel SetSalesCardOrder(CC_Order request)
        {
            base.Remote.Action = "SetSalesCardOrder";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 售卡订单 出队列
        /// </summary>
        public ResponseModel<CC_Order> GetSalesCardOrder(CC_Order request)
        {
            base.Remote.Action = "GetSalesCardOrder";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_Order>>();
        }

        /// <summary>
        /// 售卡订单 队列长度
        /// </summary>
        public ResponseModel<long> GetSalesCardOrderLength(CC_Order request)
        {
            base.Remote.Action = "GetSalesCardOrderLength";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

        public ResponseModel DeleteSalesCardOrderList(CC_Order request)
        {
            base.Remote.Action = "DeleteSalesCardOrderList";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

    }

    /// <summary>
    /// CPOS / 后台 / 充值订单 / Queue
    /// </summary>
    public class CCRechargeOrder : ControllerBase
    {
        public CCRechargeOrder(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 充值订单 入队列
        /// </summary>
        public ResponseModel SetRechargeOrder(CC_Order request)
        {
            base.Remote.Action = "SetRechargeOrder";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 充值订单 出队列
        /// </summary>
        public ResponseModel<CC_Order> GetRechargeOrder(CC_Order request)
        {
            base.Remote.Action = "GetRechargeOrder";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_Order>>();
        }

        /// <summary>
        /// 充值订单 队列长度
        /// </summary>
        public ResponseModel<long> GetRechargeOrderLength(CC_Order request)
        {
            base.Remote.Action = "GetRechargeOrderLength";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

        public ResponseModel DeleteRechargeOrderList(CC_Order request)
        {
            base.Remote.Action = "DeleteRechargeOrderList";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

    }

    /// <summary>
    /// CPOS / 后台 / 订单支付成功 / Queue
    /// </summary>
    public class CCOrderPaySuccess : ControllerBase
    {
        public CCOrderPaySuccess(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 支付成功消息 入队列
        /// </summary>
        public ResponseModel SetPaySuccess(CC_PaySuccess request)
        {
            base.Remote.Action = "SetPaySuccess";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 支付成功消息 出队列
        /// </summary>
        public ResponseModel<CC_PaySuccess> GetPaySuccess(CC_PaySuccess request)
        {
            base.Remote.Action = "GetPaySuccess";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_PaySuccess>>();
        }

        /// <summary>
        /// 支付成功消息 队列长度
        /// </summary>
        public ResponseModel<long> GetPaySuccessLength(CC_PaySuccess request)
        {
            base.Remote.Action = "GetPaySuccessLength";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

    }

    /// <summary>
    /// CPOS / 后台 / 根据订单状态发消息 / Queue
    /// </summary>
    public class CCOrderPushMessage : ControllerBase
    {
        public CCOrderPushMessage(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 根据订单状态发消息 入队列
        /// </summary>
        public ResponseModel SetOrderPushMessage(CC_OrderPushMessage request)
        {
            base.Remote.Action = "SetOrderPushMessage";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 根据订单状态发消息 出队列
        /// </summary>
        public ResponseModel<CC_OrderPushMessage> GetOrderPushMessage(CC_OrderPushMessage request)
        {
            base.Remote.Action = "GetOrderPushMessage";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_OrderPushMessage>>();
        }

        /// <summary>
        /// 根据订单状态发消息 队列长度
        /// </summary>
        public ResponseModel<long> GetOrderPushMessageLength(CC_OrderPushMessage request)
        {
            base.Remote.Action = "GetOrderPushMessageLength";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

    }

    /// <summary>
    /// APP/后台订单发货-发送微信模板消息 / Queue
    /// </summary>
    public class CCOrderSend : ControllerBase
    {
        public CCOrderSend(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 订单发货消息 入队列
        /// </summary>
        public ResponseModel SetOrderSend(CC_OrderSend request)
        {
            base.Remote.Action = "SetOrderSend";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 订单发货消息 出队列
        /// </summary>
        public ResponseModel<CC_OrderSend> GetOrderSend(CC_OrderSend request)
        {
            base.Remote.Action = "GetOrderSend";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_OrderSend>>();
        }

        /// <summary>
        /// 订单发货消息 队列长度
        /// </summary>
        public ResponseModel<long> GetOrderSendLength(CC_OrderSend request)
        {
            base.Remote.Action = "GetOrderSendLength";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

    }

    /// <summary>
    /// 确认收货时处理积分、返现、佣金 / Queue
    /// </summary>
    public class CCOrderReward : ControllerBase
    {
        public CCOrderReward(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 订单确认收货奖励 入队列
        /// </summary>
        public ResponseModel SeOrderReward(CC_OrderReward request)
        {
            base.Remote.Action = "SetOrderReward";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 订单确认收货奖励 出队列
        /// </summary>
        public ResponseModel<CC_OrderReward> GetOrderReward(CC_OrderReward request)
        {
            base.Remote.Action = "GetOrderReward";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_OrderReward>>();
        }

        /// <summary>
        /// 订单确认收货奖励 队列长度
        /// </summary>
        public ResponseModel<long> GetOrderRewardLength(CC_OrderReward request)
        {
            base.Remote.Action = "GetOrderRewardLength";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

    }

    /// <summary>
    /// CPOS / 后台 / 角色菜单 / Cache
    /// </summary>
    public class CCRole : ControllerBase
    {
        public CCRole(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 种植,更新  角色菜单
        /// </summary>
        public ResponseModel SetRole(CC_Role request)
        {
            base.Remote.Action = "SetRole";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 删除 角色菜单
        /// </summary>
        public ResponseModel DelRole(CC_Role request)
        {
            base.Remote.Action = "DelRole";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 获取 角色菜单
        /// </summary>
        public ResponseModel<CC_Role> GetRole(CC_Role request)
        {
            base.Remote.Action = "GetRole";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_Role>>();
        }
    }
    public class CCCoupon : ControllerBase
    {
        public CCCoupon(RedisOpenAPI remote)
            : base(remote)
        { }
        /// <summary>
        /// 种植 更新优惠券
        /// </summary>
        public ResponseModel SetCouponList(CC_Coupon request)
        {
            this.Remote.Action = "SetCouponList";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 获取优惠券
        /// </summary>
        public ResponseModel<CC_Coupon> GetCoupon(CC_Coupon request)
        {
            this.Remote.Action = "GetCoupon";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_Coupon>>();
        }
        public ResponseModel<long> GetCouponListLength(CC_Coupon request)
        {
            this.Remote.Action = "GetCouponListLength";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

        public ResponseModel DeleteCouponList(CC_Coupon request)
        {
            base.Remote.Action = "DeleteCouponList";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }
    }
    public class CCPrizePools : ControllerBase
    {
        public CCPrizePools(RedisOpenAPI remote)
            : base(remote)
        { }
        /// <summary>
        /// 种植 更新优惠券
        /// </summary>
        public ResponseModel SetPrizePools(List<CC_PrizePool> request)
        {
            this.Remote.Action = "SetPrizePools";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 获取优惠券
        /// </summary>
        public ResponseModel<CC_PrizePool> GetPrizePools(CC_PrizePool request)
        {
            this.Remote.Action = "GetPrizePools";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_PrizePool>>();
        }
        public ResponseModel<long> GetPrizePoolsListLength(CC_PrizePool request)
        {
            this.Remote.Action = "GetPrizePoolsListLength";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

        public ResponseModel DeletePrizePoolsList(CC_PrizePool request)
        {
            base.Remote.Action = "DeletePrizePoolsList";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }
    }
    public class CCVipMappingCoupon : ControllerBase
    {
        public CCVipMappingCoupon(RedisOpenAPI remote)
            : base(remote)
        { }
        /// <summary>
        /// 种植 更新会员绑定优惠券
        /// </summary>
        public ResponseModel SetVipMappingCoupon(CC_VipMappingCoupon request)
        {
            this.Remote.Action = "SetVipMappingCoupon";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 获取会员绑定优惠券
        /// </summary>
        public ResponseModel<CC_VipMappingCoupon> GetVipMappingCoupon(CC_VipMappingCoupon request)
        {
            this.Remote.Action = "GetVipMappingCoupon";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_VipMappingCoupon>>();
        }
        /// <summary>
        /// 会员绑定优惠券队列长度
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseModel<long> GetVipMappingCouponLength(CC_VipMappingCoupon request)
        {
            this.Remote.Action = "GetVipMappingCouponLength";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

        public ResponseModel DeleteVipMappingCouponList(CC_VipMappingCoupon request)
        {
            this.Remote.Action = "DeleteVipMappingCouponList";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }
    }
    public class CCPrize : ControllerBase
    {
        public CCPrize(RedisOpenAPI remote)
            : base(remote)
        { }
        /// <summary>
        /// 种植 活动奖励
        /// </summary>
        public ResponseModel SetPrize(CC_Prize request)
        {
            this.Remote.Action = "SetPrize";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 获取活动奖励
        /// </summary>
        public ResponseModel<CC_Prize> GetPrize(CC_Prize request)
        {
            this.Remote.Action = "GetPrize";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_Prize>>();
        }
        /// <summary>
        /// 活动奖励长度
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseModel<long> GetPrizeLength(CC_Prize request)
        {
            this.Remote.Action = "GetPrizeLength";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }
    }
    public class CCConnection : ControllerBase
    {
        public CCConnection(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 种植,更新  商户数据库链接
        /// </summary>
        public ResponseModel SetConnection(CC_Connection request)
        {
            base.Remote.Action = "SetConnection";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 删除 商户数据库链接
        /// </summary>
        public ResponseModel DelConnection(CC_Connection request)
        {
            base.Remote.Action = "DelConnection";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 获取 商户数据库链接
        /// </summary>
        public ResponseModel<CC_Connection> GetConnection(CC_Connection request)
        {
            base.Remote.Action = "GetConnection";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_Connection>>();
        }
    }
    public class CCContact : ControllerBase
    {
        public CCContact(RedisOpenAPI remote)
            : base(remote)
        { }
        /// <summary>
        /// 种植 活动奖励
        /// </summary>
        public ResponseModel SetContact(CC_Contact request)
        {
            this.Remote.Action = "SetContact";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 获取活动奖励
        /// </summary>
        public ResponseModel<CC_Contact> GetContact(CC_Contact request)
        {
            this.Remote.Action = "GetContact";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_Contact>>();
        }
        /// <summary>
        /// 活动奖励长度
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseModel<long> GetContactLength(CC_Contact request)
        {
            this.Remote.Action = "GetContactLength";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

        public ResponseModel DeleteContactList(CC_Contact request)
        {
            this.Remote.Action = "DeleteContactList";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }
    }
    public class CCBasicSetting : ControllerBase
    {
        public CCBasicSetting(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 种植,更新  基础配置
        /// </summary>
        public ResponseModel SetBasicSetting(CC_BasicSetting request)
        {
            base.Remote.Action = "SetBasicSetting";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 删除 基础配置
        /// </summary>
        public ResponseModel DelBasicSetting(CC_BasicSetting request)
        {
            base.Remote.Action = "DelBasicSetting";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 获取 基础配置
        /// </summary>
        public ResponseModel<CC_BasicSetting> GetBasicSetting(CC_BasicSetting request)
        {
            base.Remote.Action = "GetBasicSetting";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_BasicSetting>>();
        }
    }
    public class CCSuperRetailTraderOrder : ControllerBase
    {
        public CCSuperRetailTraderOrder(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 超级分销商订单 入队列
        /// </summary>
        public ResponseModel SetSuperRetailTraderOrder(CC_Order request)
        {
            base.Remote.Action = "SetSuperRetailTraderOrder";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// 超级分销商订单 出队列
        /// </summary>
        public ResponseModel<CC_Order> GetSuperRetailTraderOrder(CC_Order request)
        {
            base.Remote.Action = "GetSuperRetailTraderOrder";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_Order>>();
        }

        /// <summary>
        /// 超级分销商订单 队列长度
        /// </summary>
        public ResponseModel<long> GetSuperRetailTraderOrderLength(CC_Order request)
        {
            base.Remote.Action = "GetSuperRetailTraderOrderLength";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

        public ResponseModel DeleteSuperRetailTraderOrderList(CC_Order request)
        {
            base.Remote.Action = "DeleteSuperRetailTraderOrderList";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

    }
    /// <summary>
    /// BI / 用户行为 / 记名埋点 / Cache
    /// </summary>
    public class BIStatistic : ControllerBase
    {
        public BIStatistic(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// BIStatistic/Test
        /// </summary>
        public ResponseModel Test()
        {
            this.Remote.Action = "Test";
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// BIStatistic/SetBIUserData
        /// </summary>
        public ResponseModel SetBIUserData(BuryingPointEntity request)
        {
            this.Remote.Action = "SetBIUserData";
            this.Remote.Content = request.JsonSerialize();
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel>();
        }

        /// <summary>
        /// BIStatistic/GetBIUserData
        /// </summary>
        public ResponseModel<BuryingPointEntity> GetBIUserData()
        {
            this.Remote.Action = "GetBIUserData";
            _JSON = this.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<BuryingPointEntity>>();
        }
    }

    /// <summary>
    /// 订单未付款-发送微信模板消息 / Queue
    /// </summary>
    public class CCOrderNotPay : ControllerBase
    {
        public CCOrderNotPay(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 订单未付款 入队列
        /// </summary>
        public ResponseModel SetOrderNotPay(CC_OrderNotPay request)
        {
            base.Remote.Action = "SetOrderNotPay";   //跳到对应controller里的这个名字的action(传递给Remote，即RedisOpenAPI)
            base.Remote.Content = request.JsonSerialize();//参数信息
            _JSON = base.Remote.GetRemoteData();  //提交请求获取返回结果
            return _JSON.JsonDeserialize<ResponseModel>();//反序列化成统一的回复实体模型
        }

        /// <summary>
        /// 订单未付款 出队列
        /// </summary>
        public ResponseModel<CC_OrderNotPay> GetOrderNotPay(CC_OrderNotPay request)
        {
            base.Remote.Action = "GetOrderNotPay";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_OrderNotPay>>();//反序列化成统一的回复实体模型，并且里面含有个性的泛型对象 
        }

        /// <summary>
        /// 订单未付款 队列长度
        /// </summary>
        public ResponseModel<long> GetOrderNotPayLength(CC_OrderNotPay request)
        {
            base.Remote.Action = "GetOrderNotPayLength";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

    }

    /// 订单未付款-发送微信模板消息 / Queue
    /// </summary>
    public class CCCouponToBeExpired : ControllerBase
    {
        public CCCouponToBeExpired(RedisOpenAPI remote)
            : base(remote)
        { }

        /// <summary>
        /// 订单未付款 入队列
        /// </summary>
        public ResponseModel SetCouponToBeExpired(CC_CouponToBeExpired request)
        {
            base.Remote.Action = "SetCouponToBeExpired";   //跳到对应controller里的这个名字的action(传递给Remote，即RedisOpenAPI)
            base.Remote.Content = request.JsonSerialize();//参数信息
            _JSON = base.Remote.GetRemoteData();  //提交请求获取返回结果
            return _JSON.JsonDeserialize<ResponseModel>();//反序列化成统一的回复实体模型
        }

        /// <summary>
        /// 订单未付款 出队列
        /// </summary>
        public ResponseModel<CC_CouponToBeExpired> GetCouponToBeExpired(CC_CouponToBeExpired request)
        {
            base.Remote.Action = "GetCouponToBeExpired";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<CC_CouponToBeExpired>>();//反序列化成统一的回复实体模型，并且里面含有个性的泛型对象 
        }

        /// <summary>
        /// 订单未付款 队列长度
        /// </summary>
        public ResponseModel<long> GetCouponToBeExpiredLength(CC_CouponToBeExpired request)
        {
            base.Remote.Action = "GetCouponToBeExpiredLength";
            base.Remote.Content = request.JsonSerialize();
            _JSON = base.Remote.GetRemoteData();
            return _JSON.JsonDeserialize<ResponseModel<long>>();
        }

        public ResponseModel DeleteCouponToBeExpiredList(CC_CouponToBeExpired request)
        {
            base.Remote.Action = "DeleteCouponToBeExpiredList";   //跳到对应controller里的这个名字的action(传递给Remote，即RedisOpenAPI)
            base.Remote.Content = request.JsonSerialize();//参数信息
            _JSON = base.Remote.GetRemoteData();  //提交请求获取返回结果
            return _JSON.JsonDeserialize<ResponseModel>();//反序列化成统一的回复实体模型
        }

    }
	public class CCActivity:ControllerBase {
		public CCActivity(RedisOpenAPI remote)
            : base(remote)
        { }
		/// <summary>
		/// 往活动set中添加vip
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public ResponseModel SetActivityVipId(ActivityVipMapping request) {
			base.Remote.Action = "SetActivityVipId";
			base.Remote.Content = request.JsonSerialize();
			_JSON = base.Remote.GetRemoteData();
			return _JSON.JsonDeserialize<ResponseModel>();
		}
		/// <summary>
		/// 某个vip是否存在活动set中
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public ResponseModel<bool> VipExistActivity(ActivityVipMapping request) {
			base.Remote.Action = "VipExistActivity";
			base.Remote.Content = request.JsonSerialize();
			_JSON = base.Remote.GetRemoteData();
			return _JSON.JsonDeserialize<ResponseModel<bool>>();
		}
		/// <summary>
		/// 删除活动set
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public ResponseModel DeleteActivity(ActivityVipMapping request) {
			this.Remote.Action = "DeleteActivity";
			this.Remote.Content = request.JsonSerialize();
			_JSON = this.Remote.GetRemoteData();
			return _JSON.JsonDeserialize<ResponseModel>();
		}
	}
    public class ControllerBase
    {
        /// <summary>
        /// Remote
        /// </summary>
        protected RedisOpenAPI Remote
        { get; set; }
        /// <summary>
        /// ResponseJSON
        /// </summary>
        protected string _JSON
        { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected   ControllerBase(RedisOpenAPI remote)
        {
            this.Remote = remote;
        }
    }
}
