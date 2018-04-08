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
    // 商户数据库连接
    public class CCConnectionController : ApiController
    {
        private RedisDBEnum _RedisDB = RedisDBEnum.Three;//放到数据库3里
        // 种植,更新 
        [HttpPost]
        public HttpResponseMessage SetConnection([FromBody] CC_Connection Connection)//FromBody代表接收的是form提交过来的方式，默认不填写时是get的方式
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCConnectionKey(Connection.CustomerID);//key的定义格式:Connection.CustomerID
                db.InsertString(key, Connection);//会把Connection转换成json字符串来存储
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
        public HttpResponseMessage DelConnection([FromBody] CC_Connection Connection)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCConnectionKey(Connection.CustomerID);//根据CustomerID生成对应的key
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
        public HttpResponseMessage GetConnection([FromBody] CC_Connection connection)
        {
            try
            {
                var db = new RedisOperation(_RedisDB);
                var key = RedisKeys.CCConnectionKey(connection.CustomerID);
                var result = db.SelectString<CC_Connection>(key);
                if (result.Customer_Name.IsNullStr() || result.ConnectionStr.IsNullStr())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel
                    {
                        Code = ResponseCode.DataNotFound,
                        Message = "success"
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<CC_Connection>
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
