<%@ WebHandler Language="C#" Class="tree_query" %>

using System;
using System.Web;
using System.Linq;

public class tree_query : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    cPos.Components.SessionManager sessionManage = new cPos.Components.SessionManager();
    public void ProcessRequest (HttpContext context) {
        object rult = null;
        switch ((context.Request.QueryString["action"] ?? "").ToLower())
        { 
            case "test":
                rult = Test_Handler(context);
                break;
            case "city":
                rult = City_Handler(context);
                break;
            case "unit":
                rult = Unit_Handler(context);
                break;
            case "sale":
                rult = Sales_Handler(context);
                break;
            case "category":
                rult = ItemCategory_Handler(context);
                break;    
        }
        context.Response.ContentType = "text/json";
        context.Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(rult));
    }

    private object Test_Handler(HttpContext context)
    {
            var id = context.Request.Params["value"];
            
            var comps = new DataItem[]{
            new  DataItem{CompId= "1",CompName = "公司1",ParentId=null}
            ,new  DataItem{CompId= "2",CompName = "公司1-1",ParentId="1"}
            ,new  DataItem{CompId= "3",CompName = "公司1-1-1",ParentId="2"}
            ,new  DataItem{CompId= "4",CompName = "公司2",ParentId=null}
            ,new  DataItem{CompId= "5",CompName = "公司3",ParentId=null}};
             
            return comps.Where(obj => obj.ParentId == id).Select(obj => new 
            {
                id = obj.GetHashCode().ToString (),
                text = obj.CompName,
                value = obj.CompId,
                complete = false,
                showcheck = true,
                hasChildren = true,
            });
    }

    private object City_Handler(HttpContext context)
    { 
        var serv = new cPos.Service.CityService();
        var code = context.Request.Params["id"]??"";
        System.Collections.Generic.IEnumerable<cPos.Model.CityInfo> citys = new cPos.Model.CityInfo[0] ;
        if(code.Length == 2)
        {
            citys = serv.GetCityListByProvince(LoggingSessionInfo, code).OrderBy(obj=>obj.City_Code);
        }

        if (code.Length == 4)
        {
            citys = serv.GetAreaListByCity(LoggingSessionInfo, code).OrderBy(obj => obj.City_Code);
        }

        return citys.Select(obj => new
        {
            id = obj.City_Code,
            text = GetName(obj),
            value = obj.City_Id, 
            complete = obj.City_Code.Length >= 6,
            showcheck = false,
            hasChildren = obj.City_Code.Length <= 4,
        }).ToArray ();
    } 

    private int GetLevel(cPos.Model.CityInfo city)
    {
        if (!string.IsNullOrEmpty(city.City3_Name))
        {
            return 3;
        }
        if (!string.IsNullOrEmpty(city.City2_Name))
        {
            return 2;
        }
        if (!string.IsNullOrEmpty(city.City1_Name))
        {
            return 1;
        }
        return 0;
    }

    private string GetName(cPos.Model.CityInfo city)
    {
        if (city.City_Code.Length == 2)
        {
            return city.City1_Name;
        }
        if (city.City_Code.Length == 4)
        {
            return city.City2_Name;
        }
        return city.City3_Name;
    }

    private object Unit_Handler(HttpContext context)
    {
        var id = context.Request.Params["value"]; 
        var service = new cPos.Service.UnitService();
        System.Collections.Generic.IEnumerable<cPos.Model.UnitInfo> source = new cPos.Model.UnitInfo[0];
        if (id == null)
        {
            source = service.GetRootUnitsByDefaultRelationMode(LoggingSessionInfo);
        }
        else
        {
            source = service.GetSubUnitsByDefaultRelationMode(LoggingSessionInfo, id);
        }
        var showCheck = false;
        bool.TryParse(context.Request.Params["showCheck"], out showCheck);
        return source.Select(obj => new
        {
            id = obj.GetHashCode().ToString(),
            text = obj.Name,
            value = obj.Id,
            complete = false,
            showcheck = showCheck,
            hasChildren = true,
        });
    }
    private object Sales_Handler(HttpContext context)
    {
        var id = context.Request.Params["value"];
        var service = new cPos.Service.UnitService();
        var cCservice = new cPos.Service.cUserService();
        System.Collections.Generic.IEnumerable<cPos.Model.UnitInfo> source = new cPos.Model.UnitInfo[0];
        System.Collections.Generic.IEnumerable<cPos.Model.User.UserInfo> userSource = new cPos.Model.User.UserInfo[0];
        if (id == null)
        {
            source = service.GetRootUnitsByDefaultRelationMode(LoggingSessionInfo);
        }
        else
        {
            source = service.GetSubUnitsByDefaultRelationMode(LoggingSessionInfo, id);
            userSource = cCservice.GetUserListByUnitIdsAndRoleId(LoggingSessionInfo, id, "45ab8773d40f47bfb9c94974be18b73c");
        }
        var showCheck = false;
        bool.TryParse(context.Request.Params["showCheck"], out showCheck);
        return (userSource.Select(obj => new
        {
            id = obj.GetHashCode().ToString(),
            text = obj.User_Name,
            value = obj.User_Name,
            complete = true,
            showcheck = true,
            hasChildren = false,
        })).Union(source.Select(obj => new
        {
            id = obj.GetHashCode().ToString(),
            text = obj.Name,
            value = obj.Id,
            complete = false,
            showcheck = false,
            hasChildren = true,
        }));
    }
    private object ItemCategory_Handler(HttpContext context)
    {
        var id = context.Request.Params["value"];
        var service = new cPos.Service.ItemCategoryService();
        var soucrce = service.GetItemCagegoryList(LoggingSessionInfo).Where(obj => obj.Parent_Id == id);
        return soucrce.Select(obj => new
        {
            id = obj.GetHashCode().ToString(),
            text = obj.Item_Category_Name,
            value = obj.Item_Category_Id,
            complete = false,
            showcheck = false,
            hasChildren = service.GetItemCagegoryList(LoggingSessionInfo).Where(o=>o.Parent_Id==obj.Item_Category_Id).Count()>0,
        });
    }
    public class DataItem
    {
        public string CompId { get; set; }
        public string CompName { get; set; }
        public string ParentId { get; set; }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

    #region LoggingSessionInfo 登录信息类集合
    public cPos.Model.LoggingSessionInfo LoggingSessionInfo
    {
        get { return sessionManage.loggingSessionInfo; }
        set { sessionManage.loggingSessionInfo = value; }
    }
    #endregion

}