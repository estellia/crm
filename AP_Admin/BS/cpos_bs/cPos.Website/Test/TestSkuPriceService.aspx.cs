using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using cPos.ExchangeBsService;


public partial class Test_TestSkuPriceService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //TestGetSkuPriceNotPackagedCount();
        //TestItemPriceBsService();
        //TestSkuPriceBsService();

        GetItemListPackaged();
    }

    private void TestGetSkuPriceNotPackagedCount()
    {
        cPos.Service.cBillService bs = new cBillService();
        cPos.Model.LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
        loggingSessionInfo = new cPos.Service.CLoggingSessionService().GetLoggingSessionInfo("29E11BDC6DAC439896958CC6866FF64E", "7d4cda48970b4ed0aa697d8c2c2e4af3");
        SkuPriceService skuService = new SkuPriceService();//GetSkuPriceNotPackagedCount
        this.lb1.Text = skuService.GetSkuPriceNotPackagedCount(loggingSessionInfo, "").ToString();
        
    }

    private void TestItemPriceBsService()
    {
        ItemPriceBsService itemPriceBsService = new ItemPriceBsService();
        int icount = itemPriceBsService.GetItemPriceNotPackagedCount("29E11BDC6DAC439896958CC6866FF64E", "0ed1a737a178491c86278b001a059a15", "");
        this.lb1.Text = icount.ToString();
        IList<ItemPriceInfo> itemPriceInfoList = new List<ItemPriceInfo>();
        itemPriceInfoList = itemPriceBsService.GetItemPriceListPackaged("29E11BDC6DAC439896958CC6866FF64E", "0ed1a737a178491c86278b001a059a15", "", 0, 10);
        this.GridView1.DataSource = itemPriceInfoList;
        GridView1.DataBind();
        //IList<ItemPriceInfo> GetItemPriceListPackaged(string Customer_Id, string User_Id, string Unit_Id, int strartRow, int rowsCount)
        //public bool SetItemPriceBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id, IList<ItemPriceInfo> ItemPriceInfoList)
        bool bReturn = itemPriceBsService.SetItemPriceBatInfo("29E11BDC6DAC439896958CC6866FF64E", "0ed1a737a178491c86278b001a059a15", "", "2222", itemPriceInfoList);
        //this.lb1.Text = bReturn.ToString();

        //public bool SetItemPriceIfFlagInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        //bool bReturn1 = itemPriceBsService.SetItemPriceIfFlagInfo("29E11BDC6DAC439896958CC6866FF64E", "0ed1a737a178491c86278b001a059a15", "", "1111");
        //this.lb1.Text = bReturn1.ToString();
    }

    private void TestSkuPriceBsService()
    {
        SkuPriceBsService skuPriceBsService = new SkuPriceBsService();
        int icount = skuPriceBsService.GetSkuPriceNotPackagedCount("29E11BDC6DAC439896958CC6866FF64E", "0ed1a737a178491c86278b001a059a15", "");
        this.lb1.Text = icount.ToString();
        IList<SkuPriceInfo> skuPriceInfoList = new List<SkuPriceInfo>();
        skuPriceInfoList = skuPriceBsService.GetSkuPriceListPackaged("29E11BDC6DAC439896958CC6866FF64E", "0ed1a737a178491c86278b001a059a15", "", 0, 10);
        this.GridView1.DataSource = skuPriceInfoList;
        GridView1.DataBind();
        //IList<ItemPriceInfo> GetItemPriceListPackaged(string Customer_Id, string User_Id, string Unit_Id, int strartRow, int rowsCount)
        //public bool SetItemPriceBatInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id, IList<ItemPriceInfo> ItemPriceInfoList)
        bool bReturn = skuPriceBsService.SetSkuPriceBatInfo("29E11BDC6DAC439896958CC6866FF64E", "0ed1a737a178491c86278b001a059a15", "", "2222", skuPriceInfoList);
        //this.lb1.Text = bReturn.ToString();

        //public bool SetItemPriceIfFlagInfo(string Customer_Id, string User_Id, string Unit_Id, string bat_id)
        //bool bReturn1 = skuPriceBsService.SetSkuPriceIfFlagInfo("29E11BDC6DAC439896958CC6866FF64E", "0ed1a737a178491c86278b001a059a15", "", "1111");
        //this.lb1.Text = bReturn1.ToString();
    }

    private void GetItemListPackaged()
    {
        ItemAuthService itemServer = new ItemAuthService();
        IList<ItemInfo> list = new List<ItemInfo>();
        list = itemServer.GetItemListPackaged("29E11BDC6DAC439896958CC6866FF64E", "0ed1a737a178491c86278b001a059a15", "", 0, 100);

        this.GridView1.DataSource = list;
        GridView1.DataBind();
    }
}