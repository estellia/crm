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
    public class CCSalesCardOrderController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Five;

        // 售卡订单 入队列
        [HttpPost]
        public HttpResponseMessage SetSalesCardOrder([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCSalesCardOrder(order.CustomerID);
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

        // 售卡订单 出队列
        [HttpPost]
        public HttpResponseMessage GetSalesCardOrder([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCSalesCardOrder(order.CustomerID);
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

        // 售卡订单 队列长度
        [HttpPost]
        public HttpResponseMessage GetSalesCardOrderLength([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCSalesCardOrder(order.CustomerID);
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

        // 售卡订单 删除队列
        [HttpPost]
        public HttpResponseMessage DeleteSalesCardOrderList([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCSalesCardOrder(order.CustomerID);
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
