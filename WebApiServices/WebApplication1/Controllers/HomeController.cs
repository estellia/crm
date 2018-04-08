using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TestApi()
        {
            ViewBag.Message = "TestApi.";

            return View();
        }      

        [HttpGet]
        public ActionResult Test(string TxtUserName,string TxtPassWord,string MessageCount,string SequenceIDList)
        {
            ///登录
            if (!string.IsNullOrEmpty(TxtUserName) && !string.IsNullOrEmpty(TxtPassWord))
            {
                //ViewData["result"] = "用户名和密码";
                //AddMessage(TxtUserName, TxtPassWord);
                string token = CallWebApiMain.Login(TxtUserName, TxtPassWord);
                if (string.IsNullOrEmpty(token))
                {
                    ViewData["result"] = "登录失败！";
                }
                else
                {
                    ViewData["result"] = string.Format("登录成功！Token={0}", token);
                }
            }
            else if (!string.IsNullOrEmpty(TxtUserName))
            {
                //发送消息
                AddMessage();
            }
            else if (!string.IsNullOrEmpty(MessageCount))
            {
                GetMessageCount(MessageCount);
            }
            else if (!string.IsNullOrEmpty(SequenceIDList))
            {
                GetGetMessagesBySequenceIDList();
            }
            else
            {
                ViewData["result"] = "";
                return View();
            }
            return View();        
          
        }

        #region 发送消息
        private void AddMessage()
        {
            if (string.IsNullOrEmpty(CallWebApiMain._accToken))
            {
                ViewData["result"] = "请先登录！";
                return;
            }
            //CallWebApiMain.Login(TxtUserName, TxtPassWord);

            //"OMSG对象Json + MSG1对象Json";
            AddMessageModel msgObj = new AddMessageModel
            {
                MSG1 = ModelHelper.GetMSG1Model(),
                OMSG = ModelHelper.GetOmsgModel(),
            };

            //添加消息
            string result = CallWebApiMain.AddMessage(msgObj);
            APIReturnModel model = null;
            JavaScriptSerializer js = new JavaScriptSerializer();
            if (!string.IsNullOrEmpty(result))
            {
                model = js.Deserialize<APIReturnModel>(result);
            }
            ViewData["result"] = model != null ? model.SequenceId : "Error";
        }

        private void GetMessageCount(string MessageCount)
        {
            int count = 0;
                int.TryParse(MessageCount, out count);
            if (count <= 0) return;
            //添加消息
            string result = CallWebApiMain.GetMessagesByTOPNumber(count);           
            ViewData["result"] = result;
        }

        private void GetGetMessagesBySequenceIDList()
        {
            string result = CallWebApiMain.GetGetMessagesBySequenceIDList();
            ViewData["result"] = result;
        }
        #endregion


        public class MsgServiceModel
        {
            public string TxtUserName { get; set; }
            public string TxtPassWord { get; set; }
            public string MessageCount { get; set; }
        }





    }
}