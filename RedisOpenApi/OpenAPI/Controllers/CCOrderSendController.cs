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
using RedisOpenAPIClient.Models.CC.OrderSend;

namespace OpenAPI.Controllers
{
    public class CCOrderSendController : ApiController
    {

        private RedisDBEnum _RedisDB = RedisDBEnum.Two;

        // APP/后台订单发货-发送微信模板消息   入队列
        [HttpPost]
        public HttpResponseMessage SetOrderSend([FromBody] CC_OrderSend OrderSend)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCOrderSend(OrderSend.CustomerID);
                db.InsertListQueue(key, OrderSend);
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

        //APP/后台订单发货-发送微信模板消息 出队列
        [HttpPost]
        public HttpResponseMessage GetOrderSend([FromBody] CC_OrderSend OrderSend)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCOrderSend(OrderSend.CustomerID);
                var result = db.SelectListQueue<CC_OrderSend>(key);
                if (result.CustomerID.IsNullStr() || result.ConfigData == null || result.OrderSendData == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                    {
                        Code = ResponseCode.DataNotFound,
                        Message = "success"
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_OrderSend>
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

        // APP/后台订单发货-发送微信模板消息 队列长度
        [HttpPost]
        public HttpResponseMessage GetOrderSendLength([FromBody] CC_OrderSend OrderSend)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCOrderSend(OrderSend.CustomerID);
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
