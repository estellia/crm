using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class customer_customer_test : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.gvCustomer.DataBind();
            ////LoadQueryByUrl();
            //this.QueryCondition = getCondition();
            //ViewState["InitAction"] = "0";

            this.QueryCondition = getCondition();
            this.gvCustomer.PageIndex = 0;
            this.gvCustomer.DataBind();
        }
    }

    protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            //this.InfoBox.ShowPopError("访问数据出错:" + ex.ToString());
        }
    }

    protected void odsCustomer_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = this.GetCustomerService();
    }

    private Hashtable getCondition()
    {
        Hashtable ht = new Hashtable();
        //if (!string.IsNullOrEmpty(this.tbCode.Text.Trim()))
        //    ht.Add("Code", this.tbCode.Text.Trim());
        //if (!string.IsNullOrEmpty(this.tbName.Text.Trim()))
        //    ht.Add("Name", this.tbName.Text.Trim());
        //if (!string.IsNullOrEmpty(this.tbContacter.Text.Trim()))
        //    ht.Add("Contacter", this.tbContacter.Text.Trim());
        //if (this.cbStatus.SelectedIndex > 0)
        //    ht.Add("Status", this.cbStatus.SelectedValue);
        ht.Add("Status", "1");
        return ht;
    }

    // 获取或设置当前查询条件
    private Hashtable QueryCondition
    {
        get
        {
            if (this.ViewState["QueryCondition"] as Hashtable == null)
            {
                this.ViewState["QueryCondition"] = getCondition();
            }
            return this.ViewState["QueryCondition"] as Hashtable;
        }
        set
        {
            this.ViewState["QueryCondition"] = value;
        }
    }

    protected void odsCustomer_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    e.Cancel = true;
        //}
        //else
        //{
            e.InputParameters.Clear();
            e.InputParameters.Add("condition", this.QueryCondition.Clone());
        //}
    }
}