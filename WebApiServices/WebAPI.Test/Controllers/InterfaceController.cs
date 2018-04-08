using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using System.Configuration;
using WebAPI.Test.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace WebAPI.Test.Controllers
{
    public class InterfaceController : ApiController
    {

        #region 定义变量
        static string errMsg;
        private readonly static string _tokenUrl = "token";
        static string BaseUrl, UserName, UserPWD;
        static string statusCode, SessionId, _accToken;
        static int _timeout = 100;
        static int iLoginCount = 0;



        //添加消息的接口地址
        private readonly static string _addMsgObjUrl = "API/WebApiServices/AddMessage";

        //获取指定条数的接口地址
        private readonly static string _getMessageByTopNumberURL = "API/WebApiServices/GetMessagesByTOPNumber";
        
        #endregion

        #region 公开方法
        /// <summary>
        /// 1.Login
        /// </summary>
        public static void Login()
        {
            string errMsg = string.Empty;

            BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            UserName = ConfigurationManager.AppSettings["WebAPIUserName"];
            UserPWD = ConfigurationManager.AppSettings["WebAPIPWD"];
            var rul = string.Format("{0}/token", BaseUrl);
            try
            {

                try
                {
                    SessionId = Token(UserName, UserPWD);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                try
                {
                    if (string.IsNullOrEmpty(SessionId))
                    {
                        if (iLoginCount <= 3)
                        {
                            iLoginCount++;
                            Login();

                        }
                    }
                    else
                    {
                        var token = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenModel>(SessionId);
                        //var operateError = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenErrorModel>(SessionId);
                        if (!string.IsNullOrEmpty(token.access_token))
                        {
                            _accToken = token.access_token;
                            errMsg = "";
                        }
                        else
                        {
                            errMsg = "获取Token异常，请重新登录或联系管理员！";
                            if (iLoginCount <= 3)
                            {
                                iLoginCount++;
                                Login();
                            }

                        }

                    }
                }
                catch (System.Net.WebException ex)
                {

                    errMsg = "获取Token失败:" + ex.Response; ;
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
        }

        /// <summary>
        /// 根据账号和密码获取acctoken
        /// </summary>
        /// <param name="pUserName"></param>
        /// <param name="pPwd"></param>
        /// <returns></returns>
        public static string Token(string pUserName, string pPwd)
        {
            string postData = string.Format("grant_type=password&username={0}&password={1}&ran={2}", pUserName, pPwd, Guid.NewGuid().ToString("N"));

            return GetResponse(postData, _tokenUrl, out statusCode);
        }

        /// <summary>
        /// 创建待推送的消息
        /// </summary>
        /// <param name="msgObj">待推送消息实体类，OMSG对象Json + MSG1对象Json</param>
        /// <returns></returns>
        public static string AddMessage(AddMessageModel msgObj)
        {
            string resultStr = string.Empty;
            //参数
            string pars = string.Empty;
            string url = string.Format("{0}{1}", BaseUrl, _getMessageByTopNumberURL);



            string pReqPar = JsonConvert.SerializeObject(msgObj);

            return GetResponse(pReqPar, url, out statusCode);

            #region 参数初始化和返回结果处理
            ////"OMSG对象Json + MSG1对象Json";
            //AddMessageModel msgObj = new AddMessageModel {            
            //    Omsg1Model = ModelHelper.GetMSG1Model(),
            //    OmsgModel = ModelHelper.GetOmsgModel(),
            //};


            //try
            //{
            //    JavaScriptSerializer js = new JavaScriptSerializer();
            //    if (!string.IsNullOrEmpty(returnStr))
            //    {
            //        var operateResullt = js.Deserialize<APIReturnModel>(returnStr);
            //        ErrMsg = operateResullt.ErrMsg;
            //        SequenceId = operateResullt.SequenceId;
            //        if (string.IsNullOrEmpty(ErrMsg))
            //            oSuccess = true;
            //        else
            //            oSuccess = false;
            //    }
            //    else
            //    {
            //        ErrMsg = "";
            //        SequenceId = "";
            //        oSuccess = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);              
            //}          

            //return Request.CreateResponse(HttpStatusCode.OK, new
            //{
            //    //errcode = 0,
            //    //errmsg = "success",
            //    //viplist = rd.VipList,
            //    //pagecount = tempList.PageCount,
            //    //rowcount = tempList.RowCount
            //});


            #endregion
        }
        /// <summary>
        /// 根据指定条数获取对应消息记录
        /// </summary>
        /// <returns></returns>
        public static string GetMessagesByTOPNumber()
        {
            string result = string.Empty;
            string url = string.Format("{0}{1}", BaseUrl, _addMsgObjUrl);
            string pReqPar = string.Empty;
            result = GetResponse(pReqPar, url, out statusCode);
            return result;
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 获取API响应信息
        /// </summary>
        /// <param name="pReqPar">请求参数</param>
        /// <param name="pUrl">请求action</param>
        /// <param name="pReqType">请求头</param>
        /// <returns></returns>
        private static string GetResponse(string pReqPar, string pUrl, out string statusCode, string pReqType = "application/json")
        {
            // todo 记录日志
            string resultStr = string.Empty;
            try
            {

                Hashtable ht = new Hashtable();
                // todo 判断acctoken是否过期
                ht.Add("Authorization", string.Format("Bearer {0}", _accToken));


                resultStr = HttpHelper.SendSoapRequest(pReqPar, string.Format("{0}{1}", BaseUrl, pUrl), _timeout, ht, pReqType, "text/json"); //BaseUrl
                statusCode = "1";
                return resultStr;
            }

            catch (WebException ex)
            {
                if ((((System.Net.HttpWebResponse)ex.Response).StatusCode) == HttpStatusCode.Unauthorized)
                {
                    statusCode = "401";
                }
                else
                if ((((System.Net.HttpWebResponse)ex.Response).StatusCode) == HttpStatusCode.GatewayTimeout)
                {
                    statusCode = "504";//连接超时

                }
                else
                    statusCode = "-1";
                return resultStr = string.Empty;
            }
        }
        #endregion

    }
}
