using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using cPos.Components;
using cPos.Model;
using System.Threading;
using System.Text;
using cPos.Service;
using cPos.Model.Advertise;

/// <summary>
///AdvertiseOrderWebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class AdvertiseOrderWebService : System.Web.Services.WebService {

    public AdvertiseOrderWebService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod(Description = "设置广告播放订单信息")]
    public bool SetAdvertiseOrderInfos(string str, string customer_id)
    {
        //this.log(LogLevel.DEBUG, "AuthBsWebServices", "SetMenuInfos", "Params", "menus=" + str);
        //this.log(LogLevel.DEBUG, "AuthBsWebServices", "SetMenuInfos", "Params", "customer_id=" + customer_id);
        AdvertiseOrderService advertiseOrderService = new AdvertiseOrderService();
        bool bReturn = advertiseOrderService.SetAdvertiseOrderXML(str, customer_id);
        return bReturn;
    }

    [WebMethod(Description = "设置广告播放订单与广告关系信息")]
    public bool SetAdvertiseOrderAdvertiseInfoXML(string str, string customer_id)
    {
        AdvertiseOrderAdvertiseService advertiseOrderService = new AdvertiseOrderAdvertiseService();
        bool bReturn = advertiseOrderService.SetAdvertiseOrderAdvertiseInfoXML(str, customer_id);
        return bReturn;
    }

    [WebMethod(Description = "设置广告播放订单与门店关系信息")]
    public bool SetAdvertiseOrderUnitInfoXML(string str, string customer_id)
    {
        AdvertiseOrderUnitService advertiseOrderUnitService = new AdvertiseOrderUnitService();
        bool bReturn = advertiseOrderUnitService.SetAdvertiseOrderUnitInfoXML(str, customer_id);
        return bReturn;
    }

    [WebMethod(Description = "设置广告信息")]
    public bool SetAdvertiseInfoXML(string str, string customer_id)
    {
        AdvertiseService advertiseOrderService = new AdvertiseService();
        bool bReturn = advertiseOrderService.SetAdvertiseOrderXML(str, customer_id);
        return bReturn;
    }
    
}
