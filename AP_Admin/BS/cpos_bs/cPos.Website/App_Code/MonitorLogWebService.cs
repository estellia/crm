using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using cPos.Model;
using cPos.Service;

/// <summary>
///MonitorLogWebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class MonitorLogWebService : System.Web.Services.WebService {

    public MonitorLogWebService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    #region 设置监控信息

    [WebMethod(Description = "获取未打包上传的监控日志数量")]
    public int GetMonitorLogNotPackagedCount(string Customer_Id, string Unit_Id)
    {
        int iCount = 0;
        MonitorLogService mlService = new MonitorLogService();
        iCount = mlService.GetMonitorLogNotPackagedCountWeb(Customer_Id, Unit_Id);
        return iCount;
    }

    [WebMethod(Description = "需要打包的MonitorLog集合同时更新批次号")]
    public List<MonitorLogInfo> GetMonitorLogListPackaged(string Customer_Id, string Unit_Id, int startRow, int rowsCount, string bat_id)
    {
        IList<MonitorLogInfo> mlInfoList = new List<MonitorLogInfo>();
        MonitorLogService mlService = new MonitorLogService();
        mlInfoList = mlService.GetMonitorLogListPackagedWeb(Customer_Id, Unit_Id, rowsCount, startRow, bat_id);
        List<MonitorLogInfo> list = mlInfoList.ToList();
        return list;
    }


    [WebMethod(Description = "更新MonitorLog表打包标识方法")]
    public bool SetMonitorLogIfFlagInfo(string Customer_Id, string bat_id)
    {
        bool bReturn = false;
        string strError = string.Empty;
        MonitorLogService mlService = new MonitorLogService();
        bReturn = mlService.SetMonitorLogIfFlagInfoWeb(Customer_Id, bat_id, out strError);
        return bReturn;
    }
    #endregion
}
