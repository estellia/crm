using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class item_item_property : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var item_id = this.Request.QueryString["item_id"];
            BindRep(item_id);
        }
    }
    private void BindRep(string id)
    {
        try
        {
            var source = (new cPos.Admin.Service.ItemPropService()).GetItemPropListByItemId(loggingSessionInfo, id);
            this.repTable.DataSource = source;
            this.repTable.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    protected void btnCloseClick(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request["from"] ?? "item_query.aspx");
    }
}