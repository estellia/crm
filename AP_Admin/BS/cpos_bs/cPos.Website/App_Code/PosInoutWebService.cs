using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using cPos.Model;
using cPos.Service;

/// <summary>
///PosInoutWebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class PosInoutWebService : System.Web.Services.WebService {

    public PosInoutWebService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod(Description = "获取未打包上传的POS小票数量")]
    public int GetPosInoutNotPackagedCount(string Customer_Id, string Unit_Id)
    {
        int iCount = 0;
        PosInoutService posInoutService = new PosInoutService();
        iCount = posInoutService.GetPosInoutNotPackagedCountWeb(Customer_Id, Unit_Id);
        return iCount;
    }

    [WebMethod(Description = "需要打包的POS小票集合同时更新批次号")]
    public List<InoutInfo> GetPosInoutListPackaged(string Customer_Id, string Unit_Id, int startRow, int rowsCount, string bat_id)
    {
        IList<InoutInfo> mlInfoList = new List<InoutInfo>();
        PosInoutService posInoutService = new PosInoutService();
        mlInfoList = posInoutService.GetPosInoutListPackagedWeb(Customer_Id, Unit_Id, rowsCount, startRow, bat_id);
        List<InoutInfo> list = mlInfoList.ToList();
        return list;
    }

    [WebMethod(Description = "需要打包的POS小票明细集合")]
    public List<InoutDetailInfo> GetPosInoutDetailListPackaged(string Customer_Id, string Unit_Id, List<InoutInfo> inoutInfoList)
    {
        IList<InoutDetailInfo> mlInfoList = new List<InoutDetailInfo>();
        PosInoutService posInoutService = new PosInoutService();
        mlInfoList = posInoutService.GetPosInoutDetailListPackageWeb(Customer_Id, Unit_Id, inoutInfoList);
        List<InoutDetailInfo> list = mlInfoList.ToList();
        return list;
    }

    [WebMethod(Description = "更新POS小票表打包标识方法")]
    public bool SetPosInoutIfFlagInfo(string Customer_Id, string bat_id)
    {
        bool bReturn = false;
        string strError = string.Empty;
        PosInoutService posInoutService = new PosInoutService();
        bReturn = posInoutService.SetPosInoutIfFlagInfoWeb(Customer_Id, bat_id, out strError);
        return bReturn;
    }
}
