using OpenAPI.RedisX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC.CouponToBeExpired;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RedisOpenAPIClient.MethodExtensions.StringExtensions;

namespace OpenAPI.Controllers
{
    public class CCCouponToBeExpiredController : ApiController
    {

        private RedisDBEnum _RedisDB = RedisDBEnum.Two;

        // 优惠券即将过期-发送微信模板消息    入队列
        [HttpPost]
        public HttpResponseMessage SetCouponToBeExpired([FromBody] CC_CouponToBeExpired CouponToBeExpired)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCCouponToBeExpired(CouponToBeExpired.CustomerID);
                db.InsertListQueue(key, CouponToBeExpired);
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

        //优惠券即将过期---发送微信模板消息 出队列
        [HttpPost]
        public HttpResponseMessage GetCouponToBeExpired([FromBody] CC_CouponToBeExpired CouponToBeExpired)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCCouponToBeExpired(CouponToBeExpired.CustomerID);
                var result = db.SelectListQueue<CC_CouponToBeExpired>(key);
                if (result.CustomerID.IsNullStr() || result.ConfigData == null || result.CouponToBeExpiredData == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                    {
                        Code = ResponseCode.DataNotFound,
                        Message = "success"
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_CouponToBeExpired>
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

        // 优惠券即将过期---发送微信模板消息 队列长度
        [HttpPost]
        public HttpResponseMessage GetCouponToBeExpiredLength([FromBody] CC_CouponToBeExpired CouponToBeExpired)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCCouponToBeExpired(CouponToBeExpired.CustomerID);
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

        // 优惠券即将过期---发送微信模板消息 删除队列
        [HttpPost]
        public HttpResponseMessage DeleteCouponToBeExpiredList([FromBody] CC_CouponToBeExpired CouponToBeExpired)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCCouponToBeExpired(CouponToBeExpired.CustomerID);
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
