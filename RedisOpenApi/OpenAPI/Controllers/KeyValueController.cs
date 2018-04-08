using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OpenAPI.RedisX;
using RedisOpenAPIClient.Models;
using RedisOpenAPIClient.Models.CC.KeyValue;
using OpenAPI.Models;

namespace OpenAPI.Controllers
{
    /// <summary>
    /// 公共方法用于读取和设置keyvalue的值
    /// </summary>
    public class KeyValueController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Six;

        /// <summary>
        /// 设置key
        /// </summary>
        /// <param name="pSapMessageEntity"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Set(string key, string value, double time = 1440)
        {

            try
            {
                var db = new RedisOperation(_RedisDB, time);
                bool result = db.Insert(key, value);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                {
                    Code = result ? ResponseCode.Success : ResponseCode.Fail,
                    Message = result ? "success" : "fail"
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

        /// <summary>
        /// 设置key
        /// </summary>
        /// <param name="pSapMessageEntity"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SetEntity([FromBody] KeyValueEntity item)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                bool result = db.Insert(item.Key, item.Value);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                {
                    Code = result ? ResponseCode.Success : ResponseCode.Fail,
                    Message = result ? "success" : "fail"
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

        /// <summary>
        /// 获取value根据key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Get(string key)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var result = db.GetKeyString(key);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<string>
                {
                    Code = string.IsNullOrEmpty(result) ? ResponseCode.Fail : ResponseCode.Success,
                    Message = string.IsNullOrEmpty(result) ? "fail" : "success",
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

        /// <summary>
        /// 获取value根据key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Del(string key)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var result = db.DeleteString(key);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<string>
                {
                    Code = !result ? ResponseCode.Fail : ResponseCode.Success,
                    Message = !result ? "fail" : "success",
                    Result = ""
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

        /// <summary>
        /// 获取标识
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public Int64 GetIdentity()
        {
            try
            {
                return IdentitySingleton.GetIdentity;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
