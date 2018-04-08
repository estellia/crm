using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using cPos.ExchangeBsService;

public partial class Test_TestMonitorLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //TestCount();
        TestInsertOrUpdate();
    }
    private void TestCount()
    {
        MonitorLogService mlService = new MonitorLogService();
        this.lb1.Text = mlService.GetMonitorLogNotPackagedCountWeb("29E11BDC6DAC439896958CC6866FF64E", "0d2bf77f765849249a0270c0a07fef07").ToString();
        //GetMonitorLogNotPackagedCount(
        //aa.MonitorLogWebService dd = new aa.MonitorLogWebService();
        //this.lb1.Text = dd.GetMonitorLogNotPackagedCount("29E11BDC6DAC439896958CC6866FF64E", "0d2bf77f765849249a0270c0a07fef07").ToString();
    }

    private void TestInsertOrUpdate()
    {
        MonitorLogAuthService monitorLogAuthService = new MonitorLogAuthService();
        IList<MonitorLogInfo> monitorLogList = new List<MonitorLogInfo>();
        MonitorLogInfo m1 = new MonitorLogInfo();
        m1.customer_id = "2241b59b81ba4684b2a3095c66a489db";
        m1.unit_id = "A8FB3A3A7A604DB78FF963394EF12DF1";
        m1.upload_time = System.DateTime.Now.ToString();
        m1.remark = "it is test.";
        bool b = monitorLogAuthService.SetMonitorLogInfo("2241b59b81ba4684b2a3095c66a489db", "A8FB3A3A7A604DB78FF963394EF12DF1", "AEB213066C604579821D26B0C77DEC29", m1);
        this.lb1.Text = b.ToString();
    }
}