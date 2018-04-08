using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.ExchangeBsService;
using cPos.Model;


public partial class TestMonitorLog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TestCount();
        //TestList();
        //TestUpdate();
        //TestInsertOrUpdate();
    }

    private void TestCount()
    {
        //GetMonitorLogNotPackagedCount(
        aa.MonitorLogWebService dd = new aa.MonitorLogWebService();//"0d2bf77f765849249a0270c0a07fef07"
        this.lb1.Text = dd.GetMonitorLogNotPackagedCount("29E11BDC6DAC439896958CC6866FF64E",null ).ToString();
    }

    private void TestList()
    {
        aa.MonitorLogWebService dd = new aa.MonitorLogWebService();
        this.GridView1.DataSource = dd.GetMonitorLogListPackaged("29E11BDC6DAC439896958CC6866FF64E", null, 0, 10, "11");
        GridView1.DataBind();
    }

    private void TestUpdate()
    {
        aa.MonitorLogWebService dd = new aa.MonitorLogWebService();
        lb2.Text = dd.SetMonitorLogIfFlagInfo("29E11BDC6DAC439896958CC6866FF64E", "11").ToString();    
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
        bool b = monitorLogAuthService.SetMonitorLogInfo("2241b59b81ba4684b2a3095c66a489db", "A8FB3A3A7A604DB78FF963394EF12DF1", "0714B2ACD7E24486A66A435C937D4CE7", m1);
        this.lb1.Text = b.ToString();
    }
}