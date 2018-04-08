using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Admin.Service.Interfaces;
using cPos.Admin.Service.Implements;

public partial class dex_test : PageBase
{
    IDexLogService service = new DexLogService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    public static string GetJsonString(object obj)
    {
        Jayrock.Json.JsonTextWriter writer = new Jayrock.Json.JsonTextWriter();
        Jayrock.Json.Conversion.JsonConvert.Export(obj, writer);
        return writer.ToString();
    }

    protected void btnGetLogTypes_Click(object sender, EventArgs e)
    {
        var list = service.GetLogTypes();
        this.tbResult.Text = GetJsonString(list);
    }

    protected void btnGetAppList_Click(object sender, EventArgs e)
    {
        var list = service.GetAppList();
        this.tbResult.Text = GetJsonString(list);
    }

    protected void btnGetLogs_Click(object sender, EventArgs e)
    {
        Hashtable ht = new Hashtable();
        ht["create_time_begin"] = "2012-07-20";
        ht["create_time_end"] = "2012-07-21";
        var list = service.GetLogs(LoggingSession, ht, 10, 0);
        this.tbResult.Text = GetJsonString(list);
    }

    protected void btnGetLogsCount_Click(object sender, EventArgs e)
    {
        var list = service.GetLogsCount(LoggingSession, new Hashtable());
        this.tbResult.Text = GetJsonString(list);
    }

    protected void btnGetLog_Click(object sender, EventArgs e)
    {
        var list = service.GetLog(LoggingSession, "0010FFEF71CB40A9AD5F28F9959CCEFA");
        this.tbResult.Text = GetJsonString(list);
    }
}