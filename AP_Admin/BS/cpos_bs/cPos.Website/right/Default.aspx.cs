using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

public partial class right_Default : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request["action"];
        string appSysId = Request["appSysId"];
        if (action == "getRole")
        {
            Response.Write(GetRoleInfo(appSysId));
            Response.End();
        }
    }

    private string GetRoleInfo(string appId)
    {
        try
        {
            
            var source = (new cPos.Service.cAppSysServices()).GetRolesByAppSysId(loggingSessionInfo, appId,9999,0);
            var json = new JavaScriptSerializer();
            return json.Serialize(source.RoleInfoList);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
            return null;
        }
    }
}