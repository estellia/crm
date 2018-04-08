using OpenAPI.RedisX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models.BI;
using RedisOpenAPIClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OpenAPI.Controllers
{
    // 记名埋点
    public class BIStatisticController : ApiController
    {
        // 测试
      //  [HttpPost]
        [HttpGet]
        public HttpResponseMessage Test()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
            {
                Code = ResponseCode.Success,
                Message = "test success."
            });
        }

        // 入队列
        [HttpPost]
        public HttpResponseMessage SetBIUserData([FromBody] BuryingPointEntity userData)
        {
            try
            {
                var db = new RedisOperation(RedisDBEnum.Default);
                db.InsertListQueue(RedisKeys.BIBuryingKey, userData);
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
                    Code =  ResponseCode.Fail,
                    Message = "fail:" + ex.Message
                });
            }
        }

        // 出队列
        [HttpPost]
        public HttpResponseMessage GetBIUserData()
        {
            try
            {
                var db = new RedisOperation(RedisDBEnum.Default);
                var result = db.SelectListQueue<BuryingPointEntity>(RedisKeys.BIBuryingKey);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<BuryingPointEntity>
                {
                     Code=  ResponseCode.Success,
                    Message = "success",
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                {
                    Code =  ResponseCode.Fail,
                    Message = "fail:" + ex.Message
                });
            }
        }

        // 队列长度
        [HttpPost]
        public HttpResponseMessage GetBIUserDataLength()
        {
            try
            {
                var db = new RedisOperation(RedisDBEnum.Default);
                var result = db.GetListLength(RedisKeys.BIBuryingKey);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    code = "0000",
                    message = "success",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    code = "0001",
                    message = "fail:" + ex.Message
                });
            }
        }
    }
}
