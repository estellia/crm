using OpenAPI.RedisX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC.OrderPushMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RedisOpenAPIClient.MethodExtensions.StringExtensions;

namespace OpenAPI.Controllers
{
    public class CCOrderPushMessageController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Two;

        // 根据订单状态发消息 入队列
        [HttpPost]
        public HttpResponseMessage SetOrderPushMessage([FromBody] CC_OrderPushMessage orderPushMessage)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCOrderPushMessageKey(orderPushMessage.CustomerID);
                db.InsertListQueue(key, orderPushMessage);
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

        // 根据订单状态发消息 出队列
        [HttpPost]
        public HttpResponseMessage GetOrderPushMessage([FromBody] CC_OrderPushMessage orderPushMessage)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCOrderPushMessageKey(orderPushMessage.CustomerID);
                var result = db.SelectListQueue<CC_OrderPushMessage>(key);
                if (result.CustomerID.IsNullStr() || result.OrderID.IsNullStr())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                    {
                        Code = ResponseCode.DataNotFound,
                        Message = "success"
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_OrderPushMessage>
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

        // 根据订单状态发消息 队列长度
        [HttpPost]
        public HttpResponseMessage GetOrderPushMessageLength([FromBody] CC_OrderPushMessage orderPushMessage)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCOrderPushMessageKey(orderPushMessage.CustomerID);
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
