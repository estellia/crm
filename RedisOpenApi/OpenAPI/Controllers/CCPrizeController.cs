using OpenAPI.RedisX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RedisOpenAPIClient.MethodExtensions.StringExtensions;
using RedisOpenAPIClient.MethodExtensions.ObjectExtensions;
namespace OpenAPI.Controllers
{
    // 奖品缓存
    public class CCPrizeController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Two;

        // 设置
        [HttpPost]
        public HttpResponseMessage SetPrize([FromBody] CC_Prize Prize)
        {

            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCPrizeKey(Prize.CustomerId,Prize.EventId);
                db.InsertString(key, Prize);
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

        // 读取
        [HttpPost]
        public HttpResponseMessage GetPrize([FromBody] CC_Prize Prize)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCPrizeKey(Prize.CustomerId, Prize.EventId);
                var result = db.SelectString<CC_Prize>(key);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_Prize>
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