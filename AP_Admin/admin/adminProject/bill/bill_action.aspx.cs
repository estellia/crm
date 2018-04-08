using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Admin.Service.Implements;
using cPos.Admin.Service.Interfaces;
using cPos.Admin.Model.Bill;
using cPos.Admin.Service;

public partial class bill_bill_action : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
     
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string str_Remark = tbRemark.Text;
        string str_bill_kind_code = this.Request.QueryString["bill_kind_code"];
        string str_bill_id = this.Request.QueryString["bill_id"];
        string str_action_flag_type = this.Request.QueryString["action_flag_type"];
        string str_action_flag_value = this.Request.QueryString["action_flag_value"];
        string str_have_flow = this.Request.QueryString["have_flow"];
        if (!string.IsNullOrEmpty(str_Remark) && !string.IsNullOrEmpty(str_bill_kind_code) && !string.IsNullOrEmpty(str_bill_kind_code)
          && !string.IsNullOrEmpty(str_bill_kind_code) && !string.IsNullOrEmpty(str_bill_kind_code) && !string.IsNullOrEmpty(str_bill_kind_code))
        {
            try
            {
                BillService billService = new BillService();
                billService.ActionBills(LoggingSession, str_bill_kind_code, str_bill_id, str_have_flow == "1" ? true : false, (BillActionFlagType)int.Parse(str_action_flag_type), int.Parse(str_action_flag_value), str_Remark);
                this.InfoBox.ShowPopInfo("保存成功");
                this.ClientScript.RegisterStartupScript(typeof(int), "ok", "window.returnValue=true;window.close();", true);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('保存成功')", true);
                Response.Write("<script>window.close();</script> "); 
            }
            catch (Exception ex)
            {
                this.InfoBox.ShowPopError("保存失败");
                this.ClientScript.RegisterStartupScript(typeof(int), "error", "window.returnValue=false;window.close();", true);
            } 
        }
        else
        {
            this.InfoBox.ShowPopInfo("数据验证不通过");
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('数据验证不通过')", true);
        }
    }
    //protected void btnReturn_Click(object sender, EventArgs e)
    //{
    //    this.ClientScript.RegisterStartupScript(typeof(int), "return", "window.returnValue=false;window.close();", true);
    //}
}