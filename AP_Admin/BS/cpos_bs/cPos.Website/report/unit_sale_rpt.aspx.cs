using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;

public partial class report_unit_sale_rpt :PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUnitsInfo();
        }
    }
    protected void btnResetClick(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.Path);
    }
    private void LoadUnitsInfo()
    {
        try
        {
            var service = new UnitService();
            this.selUnit.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(item => new controls_DropDownTree.tvNode
            {
                CheckState = null,
                Complete = false,
                ShowCheck = true,
                Text = item.Name,
                Value = item.Id,
            }));
            
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "_alert", "<script>alert('" + "加载数据出错！" + "')</script>");
        }
    }

}