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
    // 注册/关注 触点业务
    public class CCContactController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Two;

        // 入队列
        [HttpPost]
        public HttpResponseMessage SetContact([FromBody] CC_Contact Contact)
        {

            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCContactKey(Contact.CustomerId);
                db.InsertListQueue(key, Contact);
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
        public HttpResponseMessage GetContact([FromBody] CC_Contact Contact)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCContactKey(Contact.CustomerId);
                var result = db.SelectListQueue<CC_Contact>(key);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_Contact>
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
        public HttpResponseMessage GetContactLength([FromBody] CC_Contact Contact)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCContactKey(Contact.CustomerId);
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
        public HttpResponseMessage DeleteContactList([FromBody] CC_Contact Contact)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCContactKey(Contact.CustomerId);
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