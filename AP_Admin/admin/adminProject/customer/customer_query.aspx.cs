using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using cPos.Admin.Component;
using cPos.Model;
using System.Configuration;
public partial class customer_customer_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUnit();
            if (SessionManager.CurrentLoggingSession.unit_id != "")
            {
                tr_unit.Visible = false;
            }
            this.cbStatus.SelectedValue = "1";
            this.tbCode.Focus();
            this.gvCustomer.DataBind();
            LoadQueryByUrl();
            this.QueryCondition = getCondition();
            ViewState["InitAction"] = "0";
        }
    }
    protected string CustomerVocationUrl
    {
        get
        {
            string customerVocationUrl = ConfigurationManager.AppSettings["customerVocationUrl"];
            if (string.IsNullOrEmpty(customerVocationUrl))
            {
                customerVocationUrl = "";
            }
            return customerVocationUrl;
        }
    }
    //绑定运营商下拉框
    private void BindUnit()
    {
        Hashtable ht = new Hashtable();
         ht.Add("Status", "1");
        ht.Add("type_code", "代理商");
        IList<UnitInfo> ls = this.GetUnitService().GetUnitInfoList(ht, 10000, 0);
        ddl_Unit.DataSource = ls;
        ddl_Unit.DataTextField = "name";
        ddl_Unit.DataValueField = "id";
        ddl_Unit.DataBind();
        ddl_Unit.Items.Insert(0, new ListItem("", ""));
    }

    protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string customer_id = "";
            switch (e.CommandName)
            {
                case "Page":
                    return;
                case "Submit":
                    customer_id = e.CommandArgument.ToString();
                    if (!this.GetCustomerService().PublishAppInfo(customer_id))
                    {
                        this.InfoBox.ShowPopError("提交应用系统信息失败");
                        return;
                    }
                    if (!this.GetCustomerService().PublishMenuInfo(customer_id))
                    {
                        this.InfoBox.ShowPopError("提交菜单信息失败");
                        return;
                    }
                    this.InfoBox.ShowPopInfo("提交成功");
                    return;
                case "SubmitInit":
                    customer_id = e.CommandArgument.ToString();
                    if (ViewState["InitAction"] == "1")
                    {
                        return;
                    }
                    //if (!this.GetCustomerService().PublishAppInfo(customer_id))
                    //{
                    //    this.InfoBox.ShowPopError("提交应用系统信息失败");
                    //    return;
                    //}
                    //if (!this.GetCustomerService().PublishMenuInfo(customer_id))
                    //{
                    //    this.InfoBox.ShowPopError("提交菜单信息失败");
                    //    return;
                    //}
                    var error = string.Empty;
                    ViewState["InitAction"] = "1";
                    this.GetCustomerService().SetBSSystemStart(customer_id, out error);
                    System.Threading.Thread.Sleep(3000);
                    ViewState["InitAction"] = "0";
                    this.InfoBox.HideLoading(error);
                    //this.InfoBox.ShowPopInfo(error);
                    return;
                default:
                    return;
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("访问数据出错:" + ex.ToString());
        }
    }

    protected void odsCustomer_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = this.GetCustomerService();
    }

    private Hashtable getCondition()
    {
        Hashtable ht = new Hashtable();
        if (!string.IsNullOrEmpty(this.tbCode.Text.Trim()))
            ht.Add("Code", this.tbCode.Text.Trim());
        if (!string.IsNullOrEmpty(this.tbName.Text.Trim()))
            ht.Add("Name", this.tbName.Text.Trim());
        if (!string.IsNullOrEmpty(this.tbContacter.Text.Trim()))
            ht.Add("Contacter", this.tbContacter.Text.Trim());
        if (this.cbStatus.SelectedIndex > 0)
            ht.Add("Status", this.cbStatus.SelectedValue);
        //如果当前是运营商客户
        if (SessionManager.CurrentLoggingSession.unit_id != "")
        {
            ht.Add("unit_id", SessionManager.CurrentLoggingSession.unit_id);
        }
        else
        {
            //cdc进来可以选择运营商
            if (this.ddl_Unit.SelectedValue !="")
                ht.Add("unit_id", this.ddl_Unit.SelectedValue);
        }

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
        if (!IsPostBack)
        {
            e.Cancel = true;
        }
        else
        {
            e.InputParameters.Clear();
            e.InputParameters.Add("condition", this.QueryCondition.Clone());//在查询时，把查询条件传给前台页面的SelectMethod和SelectCustomerListCount方法
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.QueryCondition = getCondition();//获取查询条件
        this.gvCustomer.PageIndex = 0;
        this.gvCustomer.DataBind();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.gvCustomer.DataBind();
    }

    protected void btnDisable_Click(object sender, EventArgs e)
    {
        var selId = GetSelectedIds();
        if (selId == null)
        {
            this.InfoBox.ShowPopError("必须选择至少一个客户");
            return;
        }
        this.Response.Redirect("~/bill/bill_action.aspx?bill_kind_code=customer&bill_id=" + selId + "&have_flow=0& action_flag_type=6&action_flag_value=2");
    }
    protected void btnEnable_Click(object sender, EventArgs e)
    {
        var selId = GetSelectedIds();
        if (selId == null)
        {
            this.InfoBox.ShowPopError("必须选择至少一个客户");
            return;
        }
        this.Response.Redirect("~/bill/bill_action.aspx?bill_kind_code=customer&bill_id=" + selId + "&have_flow=0& action_flag_type=6&action_flag_value=1");
    }
    //获取选择项
    private string GetSelectedIds()
    {
        string selId = null;
        for (int i = 0; i < this.gvCustomer.Rows.Count; i++)
        {
            var ctrl = (CheckBox)this.gvCustomer.Rows[i].FindControl("select");
            if (ctrl.Checked)
            {
                selId += ctrl.Attributes["customerId"] + ",";
            }
        }
        if (selId == null)
            return null;
        return selId.Trim(',');
    }
    //生成From Url 隐藏字段
    protected override void OnPreRender(EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?");
        if (!string.IsNullOrEmpty(this.Request.QueryString["customer_id"]))
        {
            sb.Append("&customer_id=" + Server.UrlEncode(this.Request.QueryString["customer_id"]));
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {

            sb.Append("&cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&code=" + Server.UrlEncode(this.tbCode.Text));
        sb.Append("&name=" + Server.UrlEncode(this.tbName.Text));
        sb.Append("&contarter=" + Server.UrlEncode(this.tbContacter.Text));
        sb.Append("&status=" + Server.UrlEncode(this.cbStatus.SelectedValue));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
    protected void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        if (!string.IsNullOrEmpty(qs["code"]))
        {
            tbCode.Text = qs["code"].ToString();
        }
        if (!string.IsNullOrEmpty(qs["name"]))
        {
            tbName.Text = qs["name"].ToString();
        }
        if (!string.IsNullOrEmpty(qs["contarter"]))
        {
            tbContacter.Text = qs["contarter"].ToString();
        }
        if (!string.IsNullOrEmpty(qs["status"]))
        {
            cbStatus.SelectedIndex = Convert.ToInt32(qs["status"]);
        }

    }
}