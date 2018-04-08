using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAPI.Test.Controllers;
using WebAPI.Test.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace WebAPI.Test
{
    public partial class CallWebAPIByBackend : System.Web.Mvc.ViewPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAddMessage_Click(object sender, EventArgs e)
        {
            //"OMSG对象Json + MSG1对象Json";
            AddMessageModel msgObj = new AddMessageModel
            {
                Omsg1Model = ModelHelper.GetMSG1Model(),
                OmsgModel = ModelHelper.GetOmsgModel(),
            };

            string result = InterfaceController.AddMessage(msgObj);
            APIReturnModel model = null;

            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                if (!string.IsNullOrEmpty(result))
                {
                    model = js.Deserialize<APIReturnModel>(result);
                  
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Response.Write(JsonHelper.DataContractJsonSerialize<APIReturnModel>(model));      

        }
    }
}