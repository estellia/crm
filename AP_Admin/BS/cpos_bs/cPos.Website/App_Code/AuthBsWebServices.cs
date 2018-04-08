using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using cPos.Service;
using cPos.Model;
using cPos.Model.Pos;

/// <summary>
///AuthBsWebServices 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class AuthBsWebServices : System.Web.Services.WebService {

    public AuthBsWebServices () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    private void log(LogLevel level, string moduleName, string functionName, string messageType, string message)
    {
        FileLogService service = new FileLogService();
        service.Log(level, "BS", moduleName, functionName, messageType, message);
    }

    [WebMethod(Description = "设置菜单信息")]
    public bool SetMenuInfos(string str, string customer_id)
    {
        this.log(LogLevel.DEBUG, "AuthBsWebServices", "SetMenuInfos", "Params", "menus=" + str);
        this.log(LogLevel.DEBUG, "AuthBsWebServices", "SetMenuInfos", "Params", "customer_id=" + customer_id);
        cPos.Service.cMenuService menuInfo = new cPos.Service.cMenuService();
        bool bReturn = menuInfo.SetMenuInfo(str, customer_id);
        return bReturn;
    }

    [WebMethod(Description = "设置应用系统信息")]
    public bool SetAppSysInfos(string str, string customer_id)
    {
        this.log(LogLevel.DEBUG, "AuthBsWebServices", "SetAppSysInfos", "Params", "apps=" + str);
        this.log(LogLevel.DEBUG, "AuthBsWebServices", "SetAppSysInfos", "Params", "customer_id=" + customer_id);
        cPos.Service.cAppSysServices appSysInfo = new cPos.Service.cAppSysServices();
        bool bReturn = appSysInfo.SetAppSysInfo(str, customer_id);
        return bReturn;
    }

    [WebMethod(Description = "设置终端信息")]
    public bool SetPosInfo(string posInfo, string customerID, int type)
    {
        this.log(LogLevel.DEBUG, "AuthBsWebServices", "SetPosInfo", "Params", "pos=" + posInfo);
        this.log(LogLevel.DEBUG, "AuthBsWebServices", "SetPosInfo", "Params", "customer_id=" + customerID);
        this.log(LogLevel.DEBUG, "AuthBsWebServices", "SetPosInfo", "Params", "type=" + type.ToString());

        cPos.Service.PosService pos_service = new PosService();
        if (string.IsNullOrEmpty(posInfo))
            throw new ArgumentNullException("posInfo");

        if (string.IsNullOrEmpty(customerID))
            throw new ArgumentNullException("customerID");

        if (type < 1 || type > 2)
            throw new ArgumentOutOfRangeException("type");

        //todo:解密
        string decrypt_pos_info = posInfo;
        PosInfo pos = (PosInfo)cPos.Service.cXMLService.Deserialize(decrypt_pos_info, typeof(PosInfo));

        if (pos == null)
        {
            throw new ArgumentException("posInfo");
        }

        if (string.IsNullOrEmpty(pos.ID))
        {
            throw new ArgumentNullException("terminal_id");
        }
        if (string.IsNullOrEmpty(pos.SN))
        {
            throw new ArgumentNullException("terminal_sn");
        }
        if (string.IsNullOrEmpty(pos.HoldType))
        {
            throw new ArgumentNullException("terminal_hold_type");
        }
        if (string.IsNullOrEmpty(pos.Type))
        {
            throw new ArgumentNullException("terminal_type");
        }

        switch (type)
        {
            case 1:
                return pos_service.InsertPosFromAP(pos, customerID);
            case 2:
                return pos_service.ModifyPosFromAP(pos, customerID);
            default:
                return false;
        }
    }
}
