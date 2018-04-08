using OpenAPI.RedisX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RedisOpenAPIClient.MethodExtensions.StringExtensions;
using RedisOpenAPIClient.Models.CC.Order;

namespace OpenAPI.Controllers
{
    public class CCSuperRetailTraderOrderController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Two;

        // 超级分销商订单   入队列
        [HttpPost]
        public HttpResponseMessage SetSuperRetailTraderOrder([FromBody] CC_Order Order)  //调用的地方传过来的是json字符串，默认就转化过来了
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCSuperRetailTraderOrder(Order.CustomerID);//获取相关的键
                db.InsertListQueue(key, Order);//插入数据
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

        //超级分销商订单 出队列
        [HttpPost]
        public HttpResponseMessage GetSuperRetailTraderOrder([FromBody] CC_Order Order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCSuperRetailTraderOrder(Order.CustomerID);
                var result = db.SelectListQueue<CC_Order>(key);  //队列按照先进先出的原则，左边进右边出
                if (result.CustomerID.IsNullStr() || result.LogSession == null || result.OrderInfo == null)
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

        // 超级分销商订单 队列长度
        [HttpPost]
        public HttpResponseMessage GetSuperRetailTraderOrderLength([FromBody] CC_Order Order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCSuperRetailTraderOrder(Order.CustomerID);
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

        // 超级分销商订单 删除队列
        [HttpPost]
        public HttpResponseMessage DeleteSuperRetailTraderOrderList([FromBody] CC_Order order)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCSuperRetailTraderOrder(order.CustomerID);
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