using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InfoBoxPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {    
        //?info=urlEncode(info)&type=[1,2,3]&go=url
        InfoType type = InfoType.Info;
        type = (InfoType)int.Parse(this.Request["type"]??"1");
        switch (type)
        { 
            case InfoType.Info:
                div_title.InnerText = "提示";
                break;
            case InfoType.Warning:
                div_title.InnerText = "警告";
                break;
            case InfoType.Error:
                div_title.InnerText = "错误";
                break; 
        }
        div_content.InnerText = this.Request["info"];
    }
      
}