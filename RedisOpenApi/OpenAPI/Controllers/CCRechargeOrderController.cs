using OpenAPI.RedisX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RedisOpenAPIClient.MethodExtensions.StringExtensions;

namespace OpenAPI.Controllers
{
    public class CCRechargeOrderController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Five;

        // 充值订单 入队列
        [HttpPost]
        public HttpResponseMessage SetRechargeOrder([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCRechargeOrder(order.CustomerID);
                db.InsertListQueue(key, order);
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

        // 充值订单 出队列
        [HttpPost]
        public HttpResponseMessage GetRechargeOrder([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCRechargeOrder(order.CustomerID);
                var result = db.SelectListQueue<CC_Order>(key);
                if (result.CustomerID.IsNullStr())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                    {
                        Code = ResponseCode.DataNotFound,
                        Message = "success"
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_Order>
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

        // 充值订单 队列长度
        [HttpPost]
        public HttpResponseMessage GetRechargeOrderLength([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCRechargeOrder(order.CustomerID);
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

        // 充值订单 删除队列
        [HttpPost]
        public HttpResponseMessage DeleteRechargeOrderList([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCRechargeOrder(order.CustomerID);
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
