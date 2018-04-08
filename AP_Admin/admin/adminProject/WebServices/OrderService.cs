using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using cPos.Admin.Component;
using cPos.Admin.Model.Customer;
using cPos.Admin.Service.Interfaces;
using cPos.Admin.DataCrypt;
using cPos.Admin.Model.Right;
using System.Threading;
using System.Text;
using cPos.Model;
using cPos.Admin.Service;
using System.Collections;
using cPos.Admin.Service.Implements;
using cPos.Admin.Model.User;
using Jayrock.Json.Conversion;

/// <summary>
/// Summary description for MenuService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class OrderService : System.Web.Services.WebService
{
    public OrderService()
    {

    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    /// <summary>
    /// 申请客户及门店
    /// </summary>
    [WebMethod]
    public string SetCCOrderList(string userId, string type, string orderList)
    {
        //userId = "B87FBC7A6D664F67B65F9AD747C5E5DD";
        //type = "MOBILE";
        //orderList = "{\"row_No\":0,\"iCount\":0,\"cCDetail_ICount\":0,\"cCInfoList\":[{\"order_id\":\"A6F0575B403149439850A3360EC0F67F\",\"order_no\":\"mob_A6F0575B403149439850A3360EC0F67F\",\"order_date\":\"2013-01-01\",\"unit_id\":\"0d2bf77f765849249a0270c0a07fef07\",\"status\":\"1\",\"create_time\":\"2013-01-29 11:37:43\",\"create_user_id\":\"1\",\"row_No\":0,\"iCount\":0,\"cCDetail_ICount\":0,\"cCDetailInfoList\":[{\"order_detail_id\":\"41914F1FD64E4B19A053397BF90046E1\",\"order_id\":\"A6F0575B403149439850A3360EC0F67F\",\"sku_id\":\"00000672F66A4D169FCC1A9F7C53BF49\",\"end_qty\":15,\"order_qty\":0,\"difference_qty\":0,\"display_index\":0,\"if_flag\":0,\"item_code\":\"938844\",\"item_name\":\"情森QS012TJ牛仔裤(清110526)\",\"row_no\":0,\"icount\":0,\"barcode\":\"2000001388662\",\"sku_prop_1_name\":\"74-104cm\",\"enter_price\":\"100\",\"sales_price\":\"150\"},{\"order_detail_id\":\"B00A2B10D87D4860A95779D5283B4C1C\",\"order_id\":\"A6F0575B403149439850A3360EC0F67F\",\"sku_id\":\"E33384FB21664F38930CF751393D3441\",\"end_qty\":99,\"order_qty\":0,\"difference_qty\":0,\"display_index\":0,\"if_flag\":0,\"item_code\":\"938844\",\"item_name\":\"情森QS012TJ牛仔裤(清110526)\",\"row_no\":0,\"icount\":0,\"barcode\":\"m_2000001388662\",\"sku_prop_1_name\":\"74-104cm\",\"enter_price\":\"100\",\"sales_price\":\"150\"}],\"total_qty\":0}],\"total_qty\":0}";
        //orderList = "{\"row_No\":0,\"iCount\":0,\"cCDetail_ICount\":0,\"cCInfoList\":[{\"order_id\":\"A6F0575B403149439850A3360EC0F67F\",\"order_no\":\"mob_A6F0575B403149439850A3360EC0F67F\",\"order_date\":\"2013-01-01\",\"unit_id\":\"0d2bf77f765849249a0270c0a07fef07\",\"status\":\"1\",\"create_time\":\"2013-01-29 14:47:44\",\"create_user_id\":\"1\",\"row_No\":0,\"iCount\":0,\"cCDetail_ICount\":0,\"cCDetailInfoList\":[{\"order_detail_id\":\"41914F1FD64E4B19A053397BF90046E1\",\"order_id\":\"A6F0575B403149439850A3360EC0F67F\",\"sku_id\":\"00000672F66A4D169FCC1A9F7C53BF49\",\"end_qty\":15,\"order_qty\":0,\"difference_qty\":0,\"display_index\":0,\"if_flag\":0,\"item_code\":\"938844\",\"item_name\":\"情森QS012TJ牛仔裤(清110526)\",\"row_no\":0,\"icount\":0,\"barcode\":\"2000001388662\",\"sku_prop_1_name\":\"74-104cm\",\"enter_price\":\"100\",\"sales_price\":\"150\"},{\"order_detail_id\":\"47A5626FB7384B6AA3C540932A8D1219\",\"order_id\":\"A6F0575B403149439850A3360EC0F67F\",\"sku_id\":\"5D1D7801100C4B0EBE59302D0BA658E7\",\"end_qty\":99,\"order_qty\":0,\"difference_qty\":0,\"display_index\":0,\"if_flag\":0,\"item_code\":\"m-123456\",\"item_name\":\"M-123456\",\"row_no\":0,\"icount\":0,\"barcode\":\"M123456\",\"sku_prop_1_name\":\"mmm\",\"enter_price\":\"100\",\"sales_price\":\"150\"}],\"total_qty\":0}],\"total_qty\":0}";
        
        cPos.Model.LoggingSessionInfo loggingSessionInfo = new cPos.Model.LoggingSessionInfo();
        var orderService = new cPos.Admin.Service.CCOrderService();

        cPos.Admin.Model.CCInfo orderInfo = null;
        if (orderList != null && orderList.Length > 0)
        {
            orderInfo = JsonConvert.Import<cPos.Admin.Model.CCInfo>(orderList);
        }

        string error = string.Empty;
        if (orderInfo != null)
        {
            orderService.SaveCCOrderList(true, orderInfo.CCInfoList, type, userId);
        }
        return error;
    }
}
