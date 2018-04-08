//using OpenAPIClient;
using RedisOpenAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisOpenAPIClient.Models.BI;
using RedisOpenAPIClient.Models.CC;
using RedisOpenAPIClient.Models.CC.Order;
using RedisOpenAPIClient.Models.CC.Activity;

namespace OpenAPITest
{
    class Program
    {
        static void Main(string[] args)
        {

			//var ro = RedisOpenAPI.Instance.CCSalesCardOrder().SetSalesCardOrder(new CC_Order
			//{
			//    CustomerID = "xxxxx",
			//    OrderId = "0123456789",
			//    OrderInfo = "ppppppppp"
			//});

			//RedisOpenAPI.Instance.CCSalesCardOrder().GetSalesCardOrder()
			//var r = RedisOpenAPI.Instance.CCSalesCardOrder().GetSalesCardOrderLength(new CC_Order
			//{
			//    CustomerID = "xxxxx",
			//    OrderId = "0123456789",
			//    OrderInfo = "ppppppppp"
			//});

			//var r = RedisOpenAPI.Instance.CCSalesCardOrder().GetSalesCardOrderLength(new CC_Order
			//{
			//    CustomerID = "xxxxx",
			//    OrderId = "0123456789",
			//    OrderInfo = "ppppppppp"
			//});
			//var rr = RedisOpenAPI.Instance.CCSalesCardOrder().GetSalesCardOrder(new CC_Order
			//{
			//    CustomerID = "xxxxx",
			//    OrderId = "0123456789",
			//    OrderInfo = "ppppppppp"
			//});

			var r = RedisOpenAPI.Instance.CCActivity().SetActivityVipId(new ActivityVipMapping {
				CustomerId="ddd",
				ActivityId="sss",

				VipId="aaa",
			});
			Console.ReadLine();


            //RedisOpenAPI.Instance.CCAllOrder().SetOrder()
            //RedisOpenAPI.Instance.CCAllOrder().GetOrder()
            //RedisOpenAPI.Instance.CCAllOrder().GetOrderLength()

            //RedisOpenAPI.Instance.CCVipMappingCoupon().DeleteVipMappingCouponList()
            //RedisOpenAPI.Instance.CCSuperRetailTraderOrder().DeleteSuperRetailTraderOrderList()
            //RedisOpenAPI.Instance.CCSalesCardOrder().DeleteSalesCardOrderList()
            //RedisOpenAPI.Instance.CCRechargeOrder().DeleteRechargeOrderList()
            //RedisOpenAPI.Instance.CCCouponToBeExpired().DeleteCouponToBeExpiredList()
            //RedisOpenAPI.Instance.CCContact().DeleteContactList()
            //RedisOpenAPI.Instance.CCAllOrder().DeleteOrderList()


            Console.ReadLine();



            //RedisOpenAPI.Instance.CCRechargeOrder().SetRechargeOrder()
            //RedisOpenAPI.Instance.CCRechargeOrder().GetRechargeOrder()
            //RedisOpenAPI.Instance.CCRechargeOrder().GetRechargeOrderLength()

            //
            //RedisOpenAPI.Instance.CCOrderPushMessage().SetOrderPushMessage()
            //RedisOpenAPI.Instance.CCOrderPushMessage().GetOrderPushMessage()
            //RedisOpenAPI.Instance.CCOrderPushMessage().GetOrderPushMessageLength()

            //var str = RedisOpenAPI
            //    .Instance
            //    .SetTimeOut(10000)   // 可不设置
            //    .SetRetryCount(3)   // 可不设置
            //    .CCContact()   // Controller
            //    .GetContactLength(new CC_Contact() { CustomerId = "a05ba7adc78a450592e730af0da663dc"});
            //var str1 = RedisOpenAPI
            //   .Instance
            //   .SetTimeOut(10000)   // 可不设置
            //   .SetRetryCount(3)   // 可不设置
            //   .CCCoupon()   // Controller
            //   .GetCouponListLength(new CC_Coupon() { CustomerId = "a05ba7adc78a450592e730af0da663dc", CouponTypeId = "7B800BFD-1CBD-4764-B3E8-E8AE7376F526" });
            //var str13 = RedisOpenAPI
            //.Instance
            //.SetTimeOut(10000)   // 可不设置
            //.SetRetryCount(3)   // 可不设置
            //.CCVipMappingCoupon()   // Controller
            //.GetVipMappingCouponLength(new CC_VipMappingCoupon() { CustomerId = "a05ba7adc78a450592e730af0da663dc" });

            //Console.WriteLine("触点"+str.Result);
            //Console.WriteLine("优惠券" + str1.Result);
            //Console.WriteLine("绑定" + str13.Result);

            //for (var i = 0; i < 5;i++ )
            //{
            //    var str1 = RedisOpenAPI
            //       .Instance
            //       .SetTimeOut(10000)   // 可不设置
            //       .SetRetryCount(3)   // 可不设置
            //       .CCCoupon()   // Controller
            //       .GetCouponListLength(new CC_Coupon() { CustomerId = "a05ba7adc78a450592e730af0da663dc", CouponTypeId = "7B800BFD-1CBD-4764-B3E8-E8AE7376F526" });
            //}

        }
    }
}
