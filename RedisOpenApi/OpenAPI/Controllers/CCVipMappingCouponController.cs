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
    // VIP绑定Coupon
    public class CCVipMappingCouponController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Two;

        // 入队列
        [HttpPost]
        public HttpResponseMessage SetVipMappingCoupon([FromBody] CC_VipMappingCoupon VipMappingCoupon)
        {
            
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCVipMappingCouponKey(VipMappingCoupon.CustomerId);
                db.InsertListQueue(key, VipMappingCoupon);
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
                    Code =  ResponseCode.Fail,
                    Message = "fail:" + ex.Message
                });
            }
        }

        // 出队列
        [HttpPost]
        public HttpResponseMessage GetVipMappingCoupon([FromBody] CC_VipMappingCoupon VipMappingCoupon)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCVipMappingCouponKey(VipMappingCoupon.CustomerId);
                var result = db.SelectListQueue<CC_VipMappingCoupon>(key);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_VipMappingCoupon>
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
        public HttpResponseMessage GetVipMappingCouponLength([FromBody] CC_VipMappingCoupon VipMappingCoupon)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCVipMappingCouponKey(VipMappingCoupon.CustomerId);
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
        public HttpResponseMessage DeleteVipMappingCouponList([FromBody] CC_VipMappingCoupon VipMappingCoupon)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCVipMappingCouponKey(VipMappingCoupon.CustomerId);
                db.Delete(key);
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