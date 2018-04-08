using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Admin.Service.Implements;

public partial class customer_user_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadQueryByUrl();
            this.cbUserStatus.SelectedValue = "1";
            if (this.Request.Params["customer_id"] != null)
            {
                this.tbCustomerName.Text = this.Request.QueryString["customer_name"];
                this.tbCustomerName.Enabled = false;
                this.gvCustomerUser.DataBind();
            }
        }
        this.tbCustomerName.Focus();
    }
    
    private Hashtable getCondition()
    {
        Hashtable ht = new Hashtable();
        if (this.Request.Params["customer_id"] != null)
            ht.Add("CustomerID", this.Request.Params["customer_id"].ToString());
        if (!string.IsNullOrEmpty(this.tbCustomerName.Text))
            ht.Add("CustomerName", this.tbCustomerName.Text);
        if (!string.IsNullOrEmpty(this.tbUserAccount.Text.Trim()))
            ht.Add("CUAccount", this.tbUserAccount.Text.Trim());
        if (!string.IsNullOrEmpty(this.tbUserName.Text.Trim()))
            ht.Add("CUName", this.tbUserName.Text.Trim());
        if (this.cbUserStatus.SelectedIndex > 0)
            ht.Add("CUStatus", this.cbUserStatus.SelectedValue);
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

    #region 数据源对象事件
    protected void odsCustomerUser_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = this.GetCustomerService();
    }
    protected void odsCustomerUser_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(this.Request.Params["customer_id"]))
            {
                e.Cancel = true;
            }
            else
            {
                e.InputParameters.Clear();
                e.InputParameters.Add("condition", this.QueryCondition.Clone());
            }
            
        }
        else
        {
            e.InputParameters.Clear();
            e.InputParameters.Add("condition", this.QueryCondition.Clone());
        }
    }
    #endregion

    // 查询按钮
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.QueryCondition = getCondition();
        this.gvCustomerUser.PageIndex = 0;
        this.gvCustomerUser.DataBind();
    }
    // 生成 from
    protected override void OnPreRender(EventArgs e)
    {
        //生成 _from 隐藏域字段
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?");
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
          
            sb.Append("cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        } 
        if (!string.IsNullOrEmpty(this.Request.QueryString["customer_id"]))
        {
            
            sb.Append("&customer_id=" + this.Server.UrlEncode(this.Request.QueryString["customer_id"]));
        }
        sb.Append("&customer_name=" + Server.UrlEncode(this.tbCustomerName.Text));
        sb.Append("&user_account=" + Server.UrlEncode(this.tbUserAccount.Text));
        sb.Append("&user_name=" + Server.UrlEncode(this.tbUserName.Text));
        sb.Append("&status=" + Server.UrlEncode(this.cbUserStatus.SelectedValue));
        //sb.Append("&app_id=" + this.Server.UrlEncode(cbApp.SelectedValue));
        //sb.Append("&parent_menu_id=" + this.Server.UrlEncode(tvMenu.SelectedNode.Value));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
    }
    private void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        this.tbCustomerName.Text = qs["customer_name"] ?? "";
        this.tbUserAccount.Text = qs["user_account"] ?? "";
        this.tbUserName.Text = qs["user_name"] ?? "";
        this.cbUserStatus.SelectedValue = qs["status"] ?? "";
    }
}