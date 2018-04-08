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

public partial class Test_TestAdvertiseOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //TestSetAdvertiseOrderInfoList();
        //TestSetAdvertiseOrderAdvertiseByOrder();
        //TestSetAdvertiseOrderAdvertiseInfoList();
        //TestSetAdvertiseOrderUnitByOrder();
        //TestSetAdvertiseOrderUnitInfoList();

        //TestSetAdvertiseOrderAdvertiseInfoXML();
    }

    #region 广告视频订单批量保存
    private void TestSetAdvertiseOrderInfoList()
    {
        //cPos.Service.cBillService bs = new cBillService();
        //cPos.Model.LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        //loggingSessionInfo = new cPos.Service.CLoggingSessionService().GetLoggingSessionInfo("29E11BDC6DAC439896958CC6866FF64E", "e3b3c960a42f41b0bf8e6e2ae3a8ad43");
        LoggingManager loggingManager = new cLoggingManager().GetLoggingManager("29E11BDC6DAC439896958CC6866FF64E");

        AdvertiseOrderService advertiseOrderService = new AdvertiseOrderService();
        IList<AdvertiseOrderInfo> advertiseOrderInfoList = new List<AdvertiseOrderInfo>();
        AdvertiseOrderInfo adInfo1 = new AdvertiseOrderInfo();
        adInfo1.order_id = "1";
        adInfo1.order_code = "可口可乐";
        adInfo1.order_date = "2012-11-19";
        adInfo1.date_start = "2012-12-01";
        adInfo1.date_end = "2012-12-31";
        adInfo1.time_start = "00:00";
        adInfo1.time_end = "24:00";
        adInfo1.playbace_no = Convert.ToInt32("100");
        adInfo1.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        adInfo1.status = "10";
        adInfo1.status_desc = "审批通过";
        adInfo1.url_address = "http://192.168.0.55:8201/";
        advertiseOrderInfoList.Add(adInfo1);
        this.lb1.Text = advertiseOrderService.SetAdvertiseOrderInfoList(loggingManager, advertiseOrderInfoList, true).ToString();
    }
    #endregion

    #region 广告视频订单与广告关系保存
    private void TestSetAdvertiseOrderAdvertiseByOrder()
    {
        //cPos.Service.cBillService bs = new cBillService();
        //cPos.Model.LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        //loggingSessionInfo = new cPos.Service.CLoggingSessionService().GetLoggingSessionInfo("29E11BDC6DAC439896958CC6866FF64E", "e3b3c960a42f41b0bf8e6e2ae3a8ad43");
        LoggingManager loggingManager = new cLoggingManager().GetLoggingManager("29E11BDC6DAC439896958CC6866FF64E");

        AdvertiseOrderInfo advertiseOrderInfo = new AdvertiseOrderInfo();
        IList<AdvertiseOrderAdvertiseInfo> aoaList = new List<AdvertiseOrderAdvertiseInfo>();
        AdvertiseOrderAdvertiseInfo aoaInfo1 = new AdvertiseOrderAdvertiseInfo();
        aoaInfo1.id = "1";
        aoaInfo1.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        aoaInfo1.order_id = "1";
        aoaInfo1.advertise_id = "1";
        aoaList.Add(aoaInfo1);
        advertiseOrderInfo.advertiseOrderAdvertiseInfoList = aoaList;
        AdvertiseOrderAdvertiseService aoaService = new AdvertiseOrderAdvertiseService();

        this.lb1.Text = aoaService.SetAdvertiseOrderAdvertiseByOrder(loggingManager, advertiseOrderInfo).ToString();
    }
    #endregion

    #region 广告视频订单与广告关系批量保存
    private void TestSetAdvertiseOrderAdvertiseInfoList()
    {
        //cPos.Service.cBillService bs = new cBillService();
        //cPos.Model.LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        //loggingSessionInfo = new cPos.Service.CLoggingSessionService().GetLoggingSessionInfo("29E11BDC6DAC439896958CC6866FF64E", "e3b3c960a42f41b0bf8e6e2ae3a8ad43");
        LoggingManager loggingManager = new cLoggingManager().GetLoggingManager("29E11BDC6DAC439896958CC6866FF64E");

        IList<AdvertiseOrderAdvertiseInfo> aoaList = new List<AdvertiseOrderAdvertiseInfo>();
        AdvertiseOrderAdvertiseInfo aoa1 = new AdvertiseOrderAdvertiseInfo();
        aoa1.id = "1";
        aoa1.order_id = "1";
        aoa1.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        aoa1.advertise_id = "1";
        aoaList.Add(aoa1);

        AdvertiseOrderAdvertiseInfo aoa2 = new AdvertiseOrderAdvertiseInfo();
        aoa2.id = "2";
        aoa2.order_id = "1";
        aoa2.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        aoa2.advertise_id = "2";
        aoaList.Add(aoa2);

        AdvertiseOrderAdvertiseInfo aoa3 = new AdvertiseOrderAdvertiseInfo();
        aoa3.id = "3";
        aoa3.order_id = "2";
        aoa3.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        aoa3.advertise_id = "1";
        aoaList.Add(aoa3);

        AdvertiseOrderAdvertiseInfo aoa4 = new AdvertiseOrderAdvertiseInfo();
        aoa4.id = "4";
        aoa4.order_id = "2";
        aoa4.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        aoa4.advertise_id = "2";
        aoaList.Add(aoa4);


        AdvertiseOrderAdvertiseService aoaService = new AdvertiseOrderAdvertiseService();
        this.lb1.Text = aoaService.SetAdvertiseOrderAdvertiseInfoList(loggingManager, aoaList,true).ToString();

    }
    #endregion

    #region 广告视频订单与门店关系保存
    private void TestSetAdvertiseOrderUnitByOrder()
    {
        //cPos.Service.cBillService bs = new cBillService();
        //cPos.Model.LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        //loggingSessionInfo = new cPos.Service.CLoggingSessionService().GetLoggingSessionInfo("29E11BDC6DAC439896958CC6866FF64E", "e3b3c960a42f41b0bf8e6e2ae3a8ad43");
        LoggingManager loggingManager = new cLoggingManager().GetLoggingManager("29E11BDC6DAC439896958CC6866FF64E");

        AdvertiseOrderInfo advertiseOrderInfo = new AdvertiseOrderInfo();
        IList<AdvertiseOrderUnitInfo> aoaList = new List<AdvertiseOrderUnitInfo>();
        AdvertiseOrderUnitInfo aoaInfo1 = new AdvertiseOrderUnitInfo();
        aoaInfo1.order_unit_id = "1";
        aoaInfo1.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        aoaInfo1.order_id = "1";
        aoaInfo1.unit_id = "f12e3eb7b773472fabcec82fe63a4ece";
        aoaList.Add(aoaInfo1);
        advertiseOrderInfo.advertiseOrderUnitInfoList = aoaList;
        AdvertiseOrderUnitService aoaService = new AdvertiseOrderUnitService();

        this.lb1.Text = aoaService.SetAdvertiseOrderUnitByOrder(loggingManager, advertiseOrderInfo).ToString();
    }
    #endregion

    #region 广告视频订单与门店关系批量保存
    private void TestSetAdvertiseOrderUnitInfoList()
    {
        //cPos.Service.cBillService bs = new cBillService();
        //cPos.Model.LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        //loggingSessionInfo = new cPos.Service.CLoggingSessionService().GetLoggingSessionInfo("29E11BDC6DAC439896958CC6866FF64E", "e3b3c960a42f41b0bf8e6e2ae3a8ad43");
        LoggingManager loggingManager = new cLoggingManager().GetLoggingManager("29E11BDC6DAC439896958CC6866FF64E");

        IList<AdvertiseOrderUnitInfo> aoaList = new List<AdvertiseOrderUnitInfo>();
        AdvertiseOrderUnitInfo aoa1 = new AdvertiseOrderUnitInfo();
        aoa1.order_unit_id = "1";
        aoa1.order_id = "1";
        aoa1.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        aoa1.unit_id = "1";
        aoaList.Add(aoa1);

        AdvertiseOrderUnitInfo aoa2 = new AdvertiseOrderUnitInfo();
        aoa2.order_unit_id = "2";
        aoa2.order_id = "1";
        aoa2.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        aoa2.unit_id = "f12e3eb7b773472fabcec82fe63a4ece";
        aoaList.Add(aoa2);

        AdvertiseOrderUnitInfo aoa3 = new AdvertiseOrderUnitInfo();
        aoa3.order_unit_id = "3";
        aoa3.order_id = "2";
        aoa3.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        aoa3.unit_id = "1";
        aoaList.Add(aoa3);

        AdvertiseOrderUnitInfo aoa4 = new AdvertiseOrderUnitInfo();
        aoa4.order_unit_id = "4";
        aoa4.order_id = "2";
        aoa4.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        aoa4.unit_id = "f12e3eb7b773472fabcec82fe63a4ece";
        aoaList.Add(aoa4);


        AdvertiseOrderUnitService aoaService = new AdvertiseOrderUnitService();
        this.lb1.Text = aoaService.SetAdvertiseOrderUnitInfoList(loggingManager, aoaList, true).ToString();

    }
    #endregion

    private void TestSetAdvertiseOrderAdvertiseInfoXML()
    {
        AdvertiseOrderInfo adInfo1 = new AdvertiseOrderInfo();
        adInfo1.order_id = "2";
        adInfo1.order_code = "可口可乐";
        adInfo1.order_date = "2012-11-19";
        adInfo1.date_start = "2012-12-01";
        adInfo1.date_end = "2012-12-31";
        adInfo1.time_start = "00:00";
        adInfo1.time_end = "24:00";
        adInfo1.playbace_no = Convert.ToInt32("100");
        adInfo1.customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        adInfo1.status = "10";
        adInfo1.status_desc = "审批通过";
        adInfo1.url_address = "http://192.168.0.55:8201/";
        IList<AdvertiseOrderInfo> advertiseOrderInfoList = new List<AdvertiseOrderInfo>();
        advertiseOrderInfoList.Add(adInfo1);
        string str1 = cXMLService.Serialiaze(advertiseOrderInfoList);

        string customer_id = "29E11BDC6DAC439896958CC6866FF64E";
        string str = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<ArrayOfAdvertiseOrderInfo xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><AdvertiseOrderInfo> <order_id>100</order_id><order_code>001</order_code> <order_date>2012-01-01</order_date><playbace_no>0</playbace_no> <status>0</status> <icount>0</icount><row_no>0</row_no></AdvertiseOrderInfo></ArrayOfAdvertiseOrderInfo>";
        //反序列化
        //IList<AdvertiseOrderAdvertiseInfo> advertiseOrderAdvertiseInfoList = (IList<AdvertiseOrderAdvertiseInfo>)cXMLService.Deserialize(str, typeof(List<cPos.Model.Advertise.AdvertiseOrderAdvertiseInfo>));


        AdvertiseOrderService advertiseOrderService = new AdvertiseOrderService();
        //bool bReturn = advertiseOrderService.SetAdvertiseOrderXML(str, customer_id);

        string strAdvertise = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<ArrayOfAdvertiseInfo xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <AdvertiseInfo>\r\n    <advertise_id>EA7E0557235B4295BAC72CCF470154A0</advertise_id>\r\n    <advertise_name>ad01</advertise_name>\r\n    <advertise_code>ad01</advertise_code>\r\n    <file_size>10000</file_size>\r\n    <file_format>.flv</file_format>\r\n    <url_address>ad01.flv</url_address>\r\n    <status>1</status>\r\n    <icount>0</icount>\r\n    <row_no>0</row_no>\r\n  </AdvertiseInfo>\r\n</ArrayOfAdvertiseInfo>";
        

        cPos.Service.AdvertiseService advertiseService =new AdvertiseService();
        bool b = advertiseService.SetAdvertiseOrderXML(strAdvertise, "FEA59DDDBB034817AD71DA12262ED0D8");

        this.lb1.Text = b.ToString();
         
    }
}