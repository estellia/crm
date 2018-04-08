using OpenAPI.RedisX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RedisOpenAPIClient.MethodExtensions.StringExtensions;
using RedisOpenAPIClient.Models.CC.Order;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;

namespace OpenAPI.Controllers
{
    public class CCAllOrderController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Five;

        // 所有订单 入队列
        [HttpPost]
        public HttpResponseMessage SetOrder([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCAllOrder(order.CustomerID);
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

        // 所有订单 出队列
        [HttpPost]
        public HttpResponseMessage GetOrder([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCAllOrder(order.CustomerID);
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

        // 所有订单 队列长度
        [HttpPost]
        public HttpResponseMessage GetOrderLength([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCAllOrder(order.CustomerID);
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

        // 所有订单 删除队列
        [HttpPost]
        public HttpResponseMessage DeleteOrderList([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCAllOrder(order.CustomerID);
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
