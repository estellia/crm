using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;

public partial class TestPosInoutWebService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TestCount();
        TestList();
        TestUpdate();
    }
    private void TestCount()
    {
        //GetMonitorLogNotPackagedCount(
        //aa.MonitorLogWebService dd = new aa.MonitorLogWebService();
        PosInoutWebService.PosInoutWebService dd = new PosInoutWebService.PosInoutWebService();//0d2bf77f765849249a0270c0a07fef07
        this.lb1.Text = dd.GetPosInoutNotPackagedCount("29E11BDC6DAC439896958CC6866FF64E", null).ToString();
    }

    private void TestList()
    {
        PosInoutWebService.PosInoutWebService dd = new PosInoutWebService.PosInoutWebService();//0d2bf77f765849249a0270c0a07fef07

        this.GridView1.DataSource = dd.GetPosInoutListPackaged("29E11BDC6DAC439896958CC6866FF64E", null, 0, 10, "A0EEA5267DB6499EB11BF4D93964013C");
        GridView1.DataBind();

        this.GridView2.DataSource = dd.GetPosInoutDetailListPackaged("29E11BDC6DAC439896958CC6866FF64E", null, dd.GetPosInoutListPackaged("29E11BDC6DAC439896958CC6866FF64E", "", 0, 10, "A0EEA5267DB6499EB11BF4D93964013C"));
        GridView2.DataBind();
    }

    private void TestUpdate()
    {
        PosInoutWebService.PosInoutWebService dd = new PosInoutWebService.PosInoutWebService();
        lb2.Text = dd.SetPosInoutIfFlagInfo("29E11BDC6DAC439896958CC6866FF64E", "11").ToString();

    }
}