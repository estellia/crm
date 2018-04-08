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

namespace OpenAPI.Controllers
{
    // 订单支付成功 发送消息模板
    public class CCOrderPaySuccessController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Two;

        // 支付成功消息 入队列
        [HttpPost]
        public HttpResponseMessage SetPaySuccess([FromBody] CC_PaySuccess paySuccess)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCPaySuccess(paySuccess.CustomerID);
                db.InsertListQueue(key, paySuccess);
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

        // 支付成功消息 出队列
        [HttpPost]
        public HttpResponseMessage GetPaySuccess([FromBody] CC_PaySuccess paySuccess)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCPaySuccess(paySuccess.CustomerID);
                var result = db.SelectListQueue<CC_PaySuccess>(key);
                if (result.CustomerID.IsNullStr() || result.ConfigData == null || result.PaySuccessData == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                    {
                        Code = ResponseCode.DataNotFound,
                        Message = "success"
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_PaySuccess>
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

        // 支付成功消息 队列长度
        [HttpPost]
        public HttpResponseMessage GetPaySuccessLength([FromBody] CC_PaySuccess paySuccess)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCPaySuccess(paySuccess.CustomerID);
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
