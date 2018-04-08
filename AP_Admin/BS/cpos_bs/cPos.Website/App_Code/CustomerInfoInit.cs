using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using cPos.Service;

/// <summary>
///CustomerInfoInit 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class CustomerInfoInit : System.Web.Services.WebService {

    public CustomerInfoInit () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod(Description = "设置客户初始化信息")]
    public bool SetCustomerInfoInit(string sCustomerInfo,string strUnitInfo,string strMenu,string typeId)
    {
        InitialService initialService = new InitialService();
        return initialService.SetBSInitialInfo(sCustomerInfo, strUnitInfo, strMenu,typeId);
    }
}
