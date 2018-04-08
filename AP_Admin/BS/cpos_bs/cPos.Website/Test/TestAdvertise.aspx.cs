using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using cPos.ExchangeBsService;
using cPos.Model.Advertise;


public partial class Test_TestAdvertise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TestSetAdvertiseInfo();
        //TestAdvertiseOrderDownload();
    }

    private void TestSetAdvertiseInfo()
    {
        //cPos.Service.cBillService bs = new cBillService();
        //cPos.Model.LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        //loggingSessionInfo = new cPos.Service.CLoggingSessionService().GetLoggingSessionInfo("29E11BDC6DAC439896958CC6866FF64E", "e3b3c960a42f41b0bf8e6e2ae3a8ad43");

        LoggingManager loggingManager = new cLoggingManager().GetLoggingManager("29E11BDC6DAC439896958CC6866FF64E");

        AdvertiseService advertiseOrderService = new AdvertiseService();
        IList<AdvertiseInfo> advertiseOrderInfoList = new List<AdvertiseInfo>();
        AdvertiseInfo adInfo1 = new AdvertiseInfo();
        adInfo1.advertise_id = "18A817FFE870453DBCB1E48CEFE932C5";
        adInfo1.advertise_name = "可口可乐1";
        adInfo1.advertise_code = "kkkl";
        adInfo1.brand_customer_id = "11";
        adInfo1.brand_id = "111";
        adInfo1.display = "1";
        adInfo1.file_format = "flv";
        adInfo1.file_size = "100k";
        adInfo1.playback_time = "100m";
        adInfo1.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        adInfo1.status = "10";
        adInfo1.url_address = "http://192.168.0.55:8201/";
        advertiseOrderInfoList.Add(adInfo1);
        this.lb1.Text = advertiseOrderService.SetAdvertiseInfoList(loggingManager, advertiseOrderInfoList, true).ToString();

    }
    //测试下载
    private void TestAdvertiseOrderDownload()
    {
        string customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        string user_id = "B87FBC7A6D664F67B65F9AD747C5E5DD";
        string unit_id = "f12e3eb7b773472fabcec82fe63a4ece";
        string bat_no = "bat0001";
        cPos.ExchangeBsService.AdvertiseOrderBsService aos = new AdvertiseOrderBsService();
        //获取下载数量
        int icount = aos.GetAdvertiseOrderNotPackagedCount(customer_id, user_id, unit_id);
        this.lb1.Text = icount.ToString();
        //获取下载订单集合
        IList<AdvertiseOrderInfo> aoiList = new List<AdvertiseOrderInfo>();
        aoiList = aos.GetAdvertiseOrderListPackaged(customer_id, user_id, unit_id, 0, 10);

        this.GridView1.DataSource = aoiList;
        GridView1.DataBind();

        //获取下载广告集合
        IList<AdvertiseInfo> aiList = new List<AdvertiseInfo>();
        aiList = aos.GetAdvertiseListPackaged(customer_id, user_id, unit_id, "1");
        this.GridView2.DataSource = aiList;
        GridView2.DataBind();
        
        //更新批次号
        IList<AdvertiseOrderUnitInfo> auiList = new List<AdvertiseOrderUnitInfo>();
        AdvertiseOrderUnitInfo aui = new AdvertiseOrderUnitInfo();
        aui.order_id = "1";
        aui.customer_id = customer_id;
        aui.unit_id = unit_id;
        auiList.Add(aui);
        bool bBat = aos.SetAdvertiseOrderBatInfo(customer_id, user_id, unit_id, bat_no, auiList);
        this.Label1.Text = bBat.ToString();

        //更新下载标识

        bool bIfFlag = aos.SetAdvertiseOrderIfFlagInfo(customer_id, user_id, unit_id, bat_no);
        this.Label2.Text = bIfFlag.ToString();
    }
}