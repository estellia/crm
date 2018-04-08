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
    // 奖品池
    public class CCPrizePoolsController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Two;

        // 入队列
        [HttpPost]
        public HttpResponseMessage SetPrizePools([FromBody] List<CC_PrizePool> prizePoolData)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCPrizePoolsKey(prizePoolData.FirstOrDefault().CustomerId, prizePoolData.FirstOrDefault().EventId);
                //db.InsertListQueue(key, prizePoolData);
                db.InsertListQueueBatch(key, prizePoolData);
                db.Expire(TimeSpan.FromHours(prizePoolData.FirstOrDefault().ValidHours), key);
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

        // 出队列
        [HttpPost]
        public HttpResponseMessage GetPrizePools([FromBody] CC_PrizePool prizePoolData)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCPrizePoolsKey(prizePoolData.CustomerId, prizePoolData.EventId);
                var result = db.SelectListQueue<CC_PrizePool>(key);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_PrizePool>
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

        // 队列长度
        [HttpPost]
        public HttpResponseMessage GetPrizePoolsListLength([FromBody] CC_PrizePool prizePoolData)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCPrizePoolsKey(prizePoolData.CustomerId, prizePoolData.EventId);
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

        // 删除队列
        [HttpPost]
        public HttpResponseMessage DeletePrizePoolsList([FromBody] CC_PrizePool prizePoolData)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCPrizePoolsKey(prizePoolData.CustomerId, prizePoolData.EventId);
                var result = db.Delete(key);
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