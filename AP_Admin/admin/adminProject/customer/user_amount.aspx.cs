using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;

public partial class customer_user_amount : System.Web.UI.Page
{
    public string ALDGatewayURL
    {
        get
        {
            return ConfigurationManager.AppSettings["ALDGatewayURL"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Clear();
        string content = string.Empty;
        string action = Request["action"];
        if (!string.IsNullOrEmpty(action))
        {

            try
            {
                switch (action)
                {
                    case "GetMember":
                        content = GetMember();
                        break;
                    case "AddAmountDetail":
                        content = AddAmountDetail();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }
    }

    private string GetMember()
    {
        var jsonData = Request["ReqContent"];
        try
        {
            var resstr = HttpClient.GetQueryString(ALDGatewayURL, string.Format("Action=GetMember&ReqContent={0}", jsonData));
            // Loggers.Debug(new DebugLogInfo() { Message = "调用ALD更改状态接口:" + resstr });          
            return resstr;
        }
        catch (Exception ex)
        {
            return "调用ALD平台失败:" + ex.Message; 
           // Loggers.Exception(new ExceptionLogInfo(ex));        
        }
    }
    private string AddAmountDetail()
    {      
        var jsonData = Request["ReqContent"];
        try
        {
            var resstr = HttpClient.GetQueryString(ALDGatewayURL, string.Format("Action=AddAmountDetail&ReqContent={0}", jsonData));
            // Loggers.Debug(new DebugLogInfo() { Message = "调用ALD更改状态接口:" + resstr });           
            return resstr;
        }
        catch (Exception ex)
        {
            //  Loggers.Exception(new ExceptionLogInfo(ex));
            return "调用ALD平台失败:" + ex.Message; 
        }



    }
}