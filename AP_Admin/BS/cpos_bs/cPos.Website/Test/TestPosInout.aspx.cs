using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;
using cPos.Service;
using cPos.ExchangeBsService;

public partial class Test_TestPosInout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //TestInoutDetail();
        //testInout();
        TestSetPosInout();
    }

    private void TestSetPosInout()
    {
        InoutInfo inoutInfo = new InoutInfo();

        inoutInfo.order_id = "";
        inoutInfo.order_no = "POS0033";
        inoutInfo.order_type_id = "";
        inoutInfo.order_reason_id = "";
        inoutInfo.red_flag = "1";
        inoutInfo.ref_order_id = "2";
        inoutInfo.ref_order_no = "3";
        inoutInfo.warehouse_id = "4";
        inoutInfo.order_date = "2012-06-01";
        inoutInfo.request_date = "2012-06-30";
        inoutInfo.complete_date = "2012-06-25";
        inoutInfo.create_unit_id = "5";
        inoutInfo.unit_id = "6";
        inoutInfo.related_unit_id = "7";
        inoutInfo.related_unit_code = "8";
        inoutInfo.pos_id = "9";
        inoutInfo.shift_id = "10";
        inoutInfo.sales_user = "11";
        inoutInfo.total_amount = Convert.ToDecimal("100");
        inoutInfo.discount_rate = Convert.ToDecimal("0.9");
        inoutInfo.actual_amount = Convert.ToDecimal("100");
        inoutInfo.receive_points = Convert.ToDecimal("200");
        inoutInfo.pay_points = Convert.ToDecimal("99");
        inoutInfo.pay_id = "12";
        inoutInfo.print_times = Convert.ToInt32("13");
        inoutInfo.carrier_id = "14";
        inoutInfo.remark = "15";
        inoutInfo.status = "16";
        inoutInfo.status_desc = "17";
        inoutInfo.total_qty = Convert.ToDecimal("10.5");
        inoutInfo.total_retail = Convert.ToDecimal("11.5");
        inoutInfo.keep_the_change = Convert.ToDecimal("0.5");
        inoutInfo.wiping_zero = Convert.ToDecimal("0.6");
        inoutInfo.vip_no = "18";
        inoutInfo.create_time = "";
        inoutInfo.create_user_id = "";
        inoutInfo.approve_time = "";
        inoutInfo.approve_user_id = "";
        inoutInfo.send_time = "";
        inoutInfo.send_user_id = "";
        inoutInfo.accpect_time = "";
        inoutInfo.accpect_user_id = "";
        inoutInfo.modify_time = "";
        inoutInfo.modify_user_id = "";
        inoutInfo.BillKindCode = "DO";
        //--------------------------------------
        InoutDetailInfo inoutDetailInfo1 = new InoutDetailInfo();
        inoutDetailInfo1.order_detail_id = "";
        inoutDetailInfo1.order_id = "";
        inoutDetailInfo1.ref_order_detail_id = "1";
        inoutDetailInfo1.sku_id = "1";
        inoutDetailInfo1.unit_id = "2";
        inoutDetailInfo1.order_qty = Convert.ToDecimal("10.11");
        inoutDetailInfo1.enter_qty = Convert.ToDecimal("10.22");
        inoutDetailInfo1.enter_price = Convert.ToDecimal("10.33");
        inoutDetailInfo1.enter_amount = Convert.ToDecimal("10.44");
        inoutDetailInfo1.std_price = Convert.ToDecimal("10.55");
        inoutDetailInfo1.discount_rate = Convert.ToDecimal("0.9");
        inoutDetailInfo1.retail_price = Convert.ToDecimal("10.66");
        inoutDetailInfo1.retail_amount = Convert.ToDecimal("10.77");
        inoutDetailInfo1.plan_price = Convert.ToDecimal("10.88");
        inoutDetailInfo1.receive_points = Convert.ToDecimal("10.99");
        inoutDetailInfo1.pay_points = Convert.ToDecimal("10.1");
        inoutDetailInfo1.remark = "4";
        inoutDetailInfo1.pos_order_code = "5";
        inoutDetailInfo1.order_detail_status = "6";
        inoutDetailInfo1.display_index = Convert.ToInt32("1");
        inoutDetailInfo1.create_time = "";
        inoutDetailInfo1.create_user_id = "";
        inoutDetailInfo1.modify_time = "";
        inoutDetailInfo1.modify_user_id = "";
        inoutDetailInfo1.ref_order_id = "";
        inoutDetailInfo1.if_flag = Convert.ToInt32("0");

        IList<InoutDetailInfo> inoutDetailList = new List<InoutDetailInfo>();
        inoutDetailList.Add(inoutDetailInfo1);

        inoutInfo.InoutDetailList = inoutDetailList;

        PosInoutAuthService posInoutAuthServices = new PosInoutAuthService();
        //UserInfo userInfo = userAuthServices.GetUserInfoByUserId("B87FBC7A6D664F67B65F9AD747C5E5DD", "29E11BDC6DAC439896958CC6866FF64E");
        IList<InoutInfo> inoutList = new List<InoutInfo>();
        inoutList.Add(inoutInfo);
        bool b = posInoutAuthServices.SetPosInoutInfo("c4db40c6a38e4a0cb319b65691a6296d", "bbdf6d65e09d4d8a8dbcd4991427cced", "7C47A0C6049F459E930B1083B0264BF5", inoutList);
        //bool b = posInoutAuthServices.SetPosInoutInfo("29E11BDC6DAC439896958CC6866FF64E", "bae1ed3ce4db4524a6d2398299075fbf", "B87FBC7A6D664F67B65F9AD747C5E5DD", inoutList);
    }

    private void testInout()
    { 
        //A0EEA5267DB6499EB11BF4D93964013C'
        List<InoutInfo> inoutInfoList = new List<InoutInfo>();
        PosInoutService piService = new PosInoutService();
        this.GridView2.DataSource = piService.GetPosInoutListPackagedWeb("29E11BDC6DAC439896958CC6866FF64E", null, 10, 1, "A0EEA5267DB6499EB11BF4D93964013C");

    GridView2.DataBind();
    //GetPosInoutDetailListPackaged("29E11BDC6DAC439896958CC6866FF64E", null, dd.GetPosInoutListPackaged("29E11BDC6DAC439896958CC6866FF64E", "", 0, 10, "A0EEA5267DB6499EB11BF4D93964013C"));
    }

    private void TestInoutDetail()
    {
        List<InoutInfo> inoutInfoList = new List<InoutInfo>();
        InoutInfo inoutInfo = new InoutInfo();
        inoutInfo.order_id = "065ce71a390e4d7580c22852575a99a7";
        inoutInfoList.Add(inoutInfo);

        InoutInfo inoutInfo1 = new InoutInfo();
        inoutInfo1.order_id = "24e5078c2b004d73b8a0a7af0f8dee68";
        inoutInfoList.Add(inoutInfo1);

        PosInoutService piService = new PosInoutService();
        this.GridView2.DataSource = piService.GetPosInoutDetailListPackageWeb("29E11BDC6DAC439896958CC6866FF64E", "", inoutInfoList);
        GridView2.DataBind();
    }
}