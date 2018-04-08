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
namespace OpenAPI.Controllers
{
    // 商户配置信息 
    public class CCBasicSettingController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Four;

        // 设置
        [HttpPost]
        public HttpResponseMessage SetBasicSetting([FromBody] CC_BasicSetting basicSetting)
        {

            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCBasicSetting(basicSetting.CustomerId);
                db.InsertString(key, basicSetting);
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

        // 删除
        [HttpPost]
        public HttpResponseMessage DelBasicSetting([FromBody] CC_BasicSetting basicSetting)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCBasicSetting(basicSetting.CustomerId);
                db.DeleteString(key);
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

        // 获取
        [HttpPost]
        public HttpResponseMessage GetBasicSetting([FromBody] CC_BasicSetting basicSetting)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCBasicSetting(basicSetting.CustomerId);
                var result = db.SelectString<CC_BasicSetting>(key);
                if (result.CustomerId.IsNullStr() || result.SettingList.Count <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                    {
                        Code = ResponseCode.DataNotFound,
                        Message = "success"
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_BasicSetting>
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