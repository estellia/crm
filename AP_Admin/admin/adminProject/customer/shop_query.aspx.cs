using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using cPos.Admin.Component;
using cPos.Model;

public partial class customer_shop_query : PageBase
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
            tbCustomerName.Focus();
            this.cbShopStatus.SelectedValue = "1";
            if (this.Request.Params["customer_id"] != null)
            {
                this.tbCustomerName.Text = this.Request.QueryString["customer_name"];
                this.tbCustomerName.Enabled = false;
                this.gvCustomerShop.DataBind();
            }
            LoadQueryByUrl();
            this.QueryCondition = getCondition();
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

    protected void odsCustomerShop_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = this.GetCustomerService();
    }

    private Hashtable getCondition()
    {
        Hashtable ht = new Hashtable();
        if (this.Request.Params["customer_id"] != null)
            ht.Add("CustomerID", this.Request.Params["customer_id"].ToString());
        if (!string.IsNullOrEmpty(this.tbCustomerName.Text))
            ht.Add("CustomerName", this.tbCustomerName.Text);
        if (!string.IsNullOrEmpty(this.tbShopCode.Text.Trim()))
            ht.Add("CSCode", this.tbShopCode.Text.Trim());
        if (!string.IsNullOrEmpty(this.tbShopName.Text.Trim()))
            ht.Add("CSName", this.tbShopName.Text.Trim());
        if (!string.IsNullOrEmpty(this.tbShopContact.Text.Trim()))
            ht.Add("CSContact", this.tbShopContact.Text.Trim());
        if(!string .IsNullOrEmpty(this.tbShopTel.Text.Trim()))
            ht.Add("CSTel",this.tbShopTel.Text.Trim());
        if (this.cbShopStatus.SelectedIndex > 0)
            ht.Add("CSStatus", this.cbShopStatus.SelectedValue);
        //如果当前是运营商客户
        if (SessionManager.CurrentLoggingSession.unit_id != "")
        {
            ht.Add("unit_id", SessionManager.CurrentLoggingSession.unit_id);
        }
        else
        {
            //cdc进来可以选择运营商
            if (this.ddl_Unit.SelectedValue != "")
                ht.Add("unit_id", this.ddl_Unit.SelectedValue);
        }
        return ht;
    }
    //前台注册_from字段
    protected override void OnPreRender(EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        sb.Append("&customer_id=" + Server.MapPath(this.Request.QueryString["customer_id"]));
        sb.Append("&customer_name=" + Server.MapPath(this.tbCustomerName.Text));
        sb.Append("&shop_code=" + Server.MapPath(this.tbShopCode.Text));
        sb.Append("&shop_name=" + Server.MapPath(this.tbShopName.Text));
        sb.Append("&shop_contact=" + Server.MapPath(this.tbShopContact.Text));
        sb.Append("&shop_tel=" + Server.MapPath(this.tbShopTel.Text));
        sb.Append("&shop_status=" + Server.MapPath(this.cbShopStatus.SelectedValue));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
    private void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        this.tbCustomerName.Text = qs["customer_name"] ?? "";
        this.tbShopCode.Text = qs["shop_code"] ?? "";
        this.tbShopName.Text = qs["shop_name"] ?? "";
        this.tbShopContact.Text = qs["shop_contact"] ?? "";
        this.tbShopTel.Text = qs["shop_tel"] ?? "";
        this.cbShopStatus.SelectedValue = qs["shop_status"] ?? "";
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

    protected void odsCustomerShop_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    e.Cancel = true;
        //}
        //else
        //{
            //e.InputParameters.Add("condition", this.QueryCondition.Clone());
       // }


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

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.QueryCondition = getCondition();
        this.gvCustomerShop.PageIndex = 0;
        this.gvCustomerShop.DataBind(); 
    }
}