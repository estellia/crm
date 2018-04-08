using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;
using cPos.Service;

public partial class report_shift_sale_rpt :PageBase
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
            this.selSalesUser.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(itme => new controls_DropDownTree.tvNode
            {
                Complete = false,
                ShowCheck = false,
                Text = itme.Name,
                Value = itme.Id
            }));
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    protected void selUnitChange_Click(object sender, EventArgs e)
    {

        //selSalesUser.DataBind(new controls_DropDownTree.tvNode[] { new controls_DropDownTree.tvNode { Complete = false, ShowCheck = false, Text = selUnit.SelectedText, Value = selUnit.SelectedValue } }.Select(item => item));
        
        selSalesUser.DataBind<UnitInfo>(selUnit.SelectValues.Select(o => new UnitService().GetUnitById(loggingSessionInfo, o)), o => new UnitService().GetSubUnitsByDefaultRelationMode(loggingSessionInfo, o.Id), o => new controls_DropDownTree.tvNode
        {
            ShowCheck = false,
            Complete = false,
            Text = o.Name,
            Value = o.Id
        });
    }
}