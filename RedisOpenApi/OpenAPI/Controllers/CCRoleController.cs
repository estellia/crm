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
    // 角色菜单 
    public class CCRoleController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.One;

        // 种植,更新 
        [HttpPost]
        public HttpResponseMessage SetRole([FromBody] CC_Role role)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCMenuRoleKey(role.CustomerID, role.RoleID);
                db.InsertString(key, role);
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                {
                    Code =  ResponseCode.Success,
                    Message = "success"
                });
            }
            catch(Exception ex)
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
        public HttpResponseMessage DelRole([FromBody] CC_Role role)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCMenuRoleKey(role.CustomerID, role.RoleID);
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
        public HttpResponseMessage GetRole([FromBody] CC_Role role)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCMenuRoleKey(role.CustomerID, role.RoleID);
                var result = db.SelectString<CC_Role>(key);
                if (result.RoleID.IsNullStr() || result.MenuList.Count <= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                    {
                        Code = ResponseCode.DataNotFound,
                        Message = "success"
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK,    new ResponseModel<CC_Role>
                {
                    Code =  ResponseCode.Success,
                    Message = "success",
                    Result=result
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
    }
}
