using OpenAPI.RedisX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC.OrderNotPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RedisOpenAPIClient.MethodExtensions.StringExtensions;
using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using RedisOpenAPIClient.Models.CC.CouponNotice;

namespace OpenAPI.Controllers
{
    //虚拟商品发优惠券后，模板消息通知
    public class CCCouponNoticeController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Two;

        // 优惠券发送成功模板消息 入队列
        [HttpPost]
        public HttpResponseMessage SetCouponNotice([FromBody] CC_CouponNotice couponNotice)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCCouponNotice(couponNotice.CustomerID);
                db.InsertListQueue(key, couponNotice);
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

        // 优惠券发送成功模板消息 出队列
        [HttpPost]
        public HttpResponseMessage GetCouponNotice([FromBody] CC_CouponNotice couponNotice)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCCouponNotice(couponNotice.CustomerID);
                var result = db.SelectListQueue<CC_CouponNotice>(key);
                if (result.CustomerID.IsNullStr() || result.ConfigData == null || result.CouponNoticeData == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                    {
                        Code = ResponseCode.DataNotFound,
                        Message = "success"
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_CouponNotice>
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

        // 优惠券发送成功模板消息 队列长度
        [HttpPost]
        public HttpResponseMessage GetCouponNoticeLength([FromBody] CC_CouponNotice couponNotice)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCCouponNotice(couponNotice.CustomerID);
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
       
    }
}
