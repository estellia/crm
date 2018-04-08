using OpenAPI.RedisX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC.OrderPaySuccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RedisOpenAPIClient.MethodExtensions.StringExtensions;
using RedisOpenAPIClient.Models.CC.OrderReward;


namespace OpenAPI.Controllers
{
    public class CCOrderRewardController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Two;

        // 确认收货时处理积分、返现、佣金   入队列
        [HttpPost]
        public HttpResponseMessage SetOrderReward([FromBody] CC_OrderReward OrderReward)  //调用的地方传过来的是json字符串，默认就转化过来了
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCOrderReward(OrderReward.CustomerID);//获取相关的键
                db.InsertListQueue(key, OrderReward);//插入数据
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

        //确认收货时处理积分、返现、佣金 出队列
        [HttpPost]
        public HttpResponseMessage GetOrderReward([FromBody] CC_OrderReward OrderReward)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCOrderReward(OrderReward.CustomerID);
                var result = db.SelectListQueue<CC_OrderReward>(key);  //队列按照先进先出的原则，左边进右边出
                if (result.CustomerID.IsNullStr() || result.LogSession == null || result.OrderInfo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                    {
                        Code = ResponseCode.DataNotFound,
                        Message = "success"
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_OrderReward>
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

        // 确认收货时处理积分、返现、佣金 队列长度
        [HttpPost]
        public HttpResponseMessage GetOrderRewardLength([FromBody] CC_OrderReward OrderReward)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCOrderReward(OrderReward.CustomerID);
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
