using OpenAPI.RedisX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RedisOpenAPIClient.MethodExtensions.StringExtensions;
using RedisOpenAPIClient.MethodExtensions.ObjectExtensions;

namespace OpenAPI.Controllers
{
    // 优惠券
    public class CCCouponController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Two;

        // 入队列
        [HttpPost]
        public HttpResponseMessage SetCouponList([FromBody] CC_Coupon couponData)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCCouponKey(couponData.CustomerId, couponData.CouponTypeId);
                //
                var coupons = new List<CC_Coupon>();
                for(var i=0;i<couponData.CouponLenth;i++)
                {
                    coupons.Add(couponData.DeepClone());
                }
                //
                var total = couponData.CouponLenth;
                var limit = 100;
                var start = 0;

                //
                do
                {
                    //
                    var models = coupons.Skip(start).Take(limit).ToList();

                    //
                    db.InsertListQueueBatch<CC_Coupon>(key, models);

                    //
                    if (start < (total - limit))
                    {
                        start = start + limit;
                    }
                    else
                    {
                        start = total;
                    }
                }
                while (start < total);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                {
                    Code = ResponseCode.Success,
                    Message = "success"
                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                {
                    Code = ResponseCode.Fail,
                    Message = "fail:" + ex.Message
                });
            }
        }

        // 出队列
        [HttpPost]
        public HttpResponseMessage GetCoupon([FromBody] CC_Coupon couponData)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCCouponKey(couponData.CustomerId, couponData.CouponTypeId);
                var result = db.SelectListQueue<CC_Coupon>(key);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_Coupon>
                {
                    Code = ResponseCode.Success,
                    Message = "success",
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                {
                    Code = ResponseCode.Fail,
                    Message = "fail:" + ex.Message
                });
            }
        }

        // 队列长度
        [HttpPost]
        public HttpResponseMessage GetCouponListLength([FromBody] CC_Coupon couponData)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCCouponKey(couponData.CustomerId, couponData.CouponTypeId);
                var result = db.GetListLength(key);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<long>
                {
                    Code = ResponseCode.Success,
                    Message = "success",
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                {
                    Code = ResponseCode.Fail,
                    Message = "fail:" + ex.Message
                });
            }
        }

        // 删除队列
        [HttpPost]
        public HttpResponseMessage DeleteCouponList([FromBody] CC_Coupon couponData)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCCouponKey(couponData.CustomerId, couponData.CouponTypeId);
                var result = db.Delete(key);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                {
                    Code = ResponseCode.Success,
                    Message = "success"
                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                {
                    Code = ResponseCode.Fail,
                    Message = "fail:" + ex.Message
                });
            }
        }
    }
}