using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OpenAPI.RedisX;
using RedisOpenAPIClient;
using RedisOpenAPIClient.MethodExtensions.ObjectExtensions;
using RedisOpenAPIClient.Models.CC.Activity;
using RedisOpenAPIClient.Models;
using StackExchange.Redis;

namespace OpenAPI.Controllers
{
    public class CCActivityController : ApiController
    {
		private RedisDBEnum _RedisDB = RedisDBEnum.Five;
		// 入队列
		[HttpPost]
		public HttpResponseMessage SetActivityVipId([FromBody] ActivityVipMapping data) {
			try {
				var db = new RedisOperation(_RedisDB);
				var key = RedisKeys.CCActivity(data.CustomerId, data.ActivityId);
				var result = db.InsertSet(key, data.VipId);
				return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel {
					Code = ResponseCode.Success,
					Message = "success"
				});
			}
			catch (Exception ex) {
				return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel {
					Code = ResponseCode.Fail,
					Message = "fail:" + ex.Message
				});
			}
		}
		/// <summary>
		/// vip是否已在set里
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public HttpResponseMessage VipExistActivity([FromBody] ActivityVipMapping data) {
			try {
				var db = new RedisOperation(_RedisDB);
				var key = RedisKeys.CCActivity(data.CustomerId, data.ActivityId);
				var result = db.ExistInSet(key,data.VipId);
				return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel<bool> {
					Code = ResponseCode.Success,
					Message = "success",
					Result = result
				});
			}
			catch (Exception ex) {
				return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel {
					Code = ResponseCode.Fail,
					Message = "fail:" + ex.Message
				});
			}
		}
		public HttpResponseMessage DeleteActivity([FromBody] ActivityVipMapping data) {
			try {
				var db = new RedisOperation(_RedisDB);
				var key = RedisKeys.CCActivity(data.CustomerId, data.ActivityId);
				db.DeleteSet(key);

				return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel{
					Code = ResponseCode.Success,
					Message = "success",
				});
			}
			catch (Exception ex) {
				return Request.CreateResponse(HttpStatusCode.OK, new ResponseModel {
					Code = ResponseCode.Fail,
					Message = "fail:" + ex.Message
				});
			}
		}
	}
}
