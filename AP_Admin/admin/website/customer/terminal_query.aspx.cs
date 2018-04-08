using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using cPos.Admin.Model.Customer;
using cPos.Admin.Service;
using cPos.Admin.Model.Base;
using System.Text.RegularExpressions;

public partial class customer_terminal_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {  
        if (!IsPostBack)
        {
            var customer_id = this.Request.QueryString["customer_id"];
            LoadTerminalHoldType();
            LoadTerminalType();
            this.tbCustomerName.Focus();
            if (string.IsNullOrEmpty(customer_id))
            {
                this.btnCreate.Visible = false;
            }
            else
            {
                this.tbCustomerName.Text = this.Request.QueryString["customer_name"];
                this.tbCustomerName.Enabled = false;
                if (!this.GetCustomerService().CanCreateTerminal(customer_id))
                {
                    this.btnCreate.Visible = true;
                }
            }
            LoadQueryByUrl();
            this.QueryCondition = getCodition();
        }
    } 

    protected Hashtable getCodition()
    {
        Hashtable ht = new Hashtable();
        if (!string.IsNullOrEmpty(this.Request.QueryString["customer_id"]))
        {
            ht.Add("CustomerID", this.Request.QueryString["customer_id"]);
        }
        if (!string.IsNullOrEmpty(this.tbCode.Text))
        {
            ht.Add("CTCode", this.tbCode.Text);
        }
        if (!string.IsNullOrEmpty(this.tbCustomerName.Text))
        {
            ht.Add("CustomerName", this.tbCustomerName.Text);
        }
        if (!string.IsNullOrEmpty(this.tbInsuraceDateBegin.Value))
        {
            ht.Add("CTInsuranceDateBegin", this.tbInsuraceDateBegin.Value);
        }
        if (!string.IsNullOrEmpty(this.tbInsuraceDateEnd.Value))
        {
            ht.Add("CTInsuranceDateEnd", this.tbInsuraceDateEnd.Value);
        }
        if (!string.IsNullOrEmpty(this.tbPurchaseDateBegin.Value))
        {
            ht.Add("CTPurchaseDateBegin", this.tbPurchaseDateBegin.Value);
        }
        if (!string.IsNullOrEmpty(this.tbPurchaseDateEnd.Value))
        {
            ht.Add("CTPurchaseDateEnd", this.tbPurchaseDateEnd.Value);
        }
        if (!string.IsNullOrEmpty(this.tbSn.Text))
        {
            ht.Add("CTsn", this.tbSn.Text);
        }
        if (!string.IsNullOrEmpty(this.tbSoftwareVersion.Text))
        {
            ht.Add("CTSoftwareVersion", this.tbSoftwareVersion.Text);
        }
        if (cbHoldType.SelectedIndex > 0)
        {
            ht.Add("CTHoldType", cbHoldType.SelectedValue);
        }
        if (cbType.SelectedIndex > 0)
        {
            ht.Add("CTType", cbType.SelectedValue);
        }
        return ht;
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.QueryCondition = getCodition();
        this.gvTerminal.PageIndex = 0;
        this.gvTerminal.DataBind();  
    } 
    
    //获取或设置当前查询条件
    private Hashtable QueryCondition {
        get {
            if (this.ViewState["QueryCondition"] as Hashtable == null)
            {
                this.ViewState["QueryCondition"] = getCodition();
            }
            return this.ViewState["QueryCondition"] as Hashtable;
        }
        set {
            this.ViewState["QueryCondition"] = value;
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        var url = string.Format("terminal_show.aspx?oper_type={0}&customer_id={1}&from={2}", 2, this.Request.QueryString["customer_id"],Server.UrlEncode(this.Request.Url.PathAndQuery));
        this.Response.Redirect(url);
    }
    //加载用户持有终端信息
    private void LoadTerminalHoldType()
    {
        try
        {
            var ret = (this.GetCustomerService() as BaseService).SelectDictionaryDetailListByDictionaryCode("terminal_hold_type");
            DictionaryDetailInfo info = new DictionaryDetailInfo { Code = "--", Name = "全部" };
            ret.Insert(0, info);
            this.cbHoldType.DataTextField = "Name";
            this.cbHoldType.DataValueField = "Code";
            this.cbHoldType.DataSource = ret;
            this.cbHoldType.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //加载终端信息
    private void LoadTerminalType()
    {
        try
        {
            var ret = (this.GetCustomerService() as BaseService).SelectDictionaryDetailListByDictionaryCode("terminal_type");
            DictionaryDetailInfo info = new DictionaryDetailInfo { ID = "-1", Name = "全部" };
            ret.Insert(0, info);
            this.cbType.DataTextField = "Name";
            this.cbType.DataValueField = "Code";
            this.cbType.DataSource = ret;
            this.cbType.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    protected void osdTerminal_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    { 
        e.ObjectInstance = this.GetCustomerService();
    }
    protected void osdTerminal_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //e.InputParameters.Clear();
        //e.InputParameters.Add("condition", this.QueryCondition.Clone());

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
    //获取用户的编码和姓名信息
    protected string GetCustomerCode(object customer,string type)
    {
        var cust = customer as CustomerInfo;
        switch(type)
        {
            case "code":return cust.Code;
            case "name":return cust.Name;
            default:return "";
        }
    }
    //生成From Url隐藏字段
    protected override void OnPreRender(EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?");
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["customer_id"]))
        {
            sb.Append("&customer_id=" + Server.UrlEncode(this.Request.QueryString["customer_id"]));
        }
        sb.Append("&code=" + Server.UrlEncode(tbCode.Text));
        sb.Append("&customerName=" + Server.UrlEncode(tbCustomerName.Text));
        sb.Append("&insuraceDateBegin=" + Server.UrlEncode(tbInsuraceDateBegin.Value));
        sb.Append("&insuraceDateEnd=" + Server.UrlEncode(tbInsuraceDateEnd.Value));
        sb.Append("&purchaseDateBegin=" + Server.UrlEncode(tbPurchaseDateBegin.Value));
        sb.Append("&purchaseDateEnd=" + Server.UrlEncode(tbPurchaseDateEnd.Value));
        sb.Append("&sn=" + Server.UrlEncode(tbSn.Text));
        sb.Append("&softwareVersion=" + Server.UrlEncode(tbSoftwareVersion.Text));
        sb.Append("&holdType=" + Server.UrlEncode(cbHoldType.SelectedIndex > 0 ? cbHoldType.SelectedValue : "0"));
        sb.Append("&type=" + Server.UrlEncode(cbType.SelectedIndex > 0 ? cbType.SelectedValue : "0"));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
    private void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        if (!string.IsNullOrEmpty(qs["code"]))
        {
            this.tbCode.Text = qs["code"].ToString();
        }
        if (!string.IsNullOrEmpty(qs["customerName"]))
        {
            this.tbCustomerName.Text = qs["customerName"].ToString();
        }
        if (!string.IsNullOrEmpty(qs["insuraceDateBegin"]))
        {
            this.tbInsuraceDateBegin.Value = qs["insuraceDateBegin"].ToString();
        }
        if (!string.IsNullOrEmpty(qs["insuraceDateEnd"]))
        {
            this.tbInsuraceDateEnd.Value = qs["insuraceDateEnd"].ToString();
        }
        if (!string.IsNullOrEmpty(qs["purchaseDateBegin"]))
        {
            this.tbPurchaseDateBegin.Value = qs["purchaseDateBegin"].ToString();
        }
        if (!string.IsNullOrEmpty(qs["purchaseDateEnd"]))
        {
            this.tbPurchaseDateEnd.Value = qs["purchaseDateEnd"].ToString();
        }
        if (!string.IsNullOrEmpty(qs["sn"]))
        {
            this.tbSn.Text = qs["sn"].ToString();
        }
        if (!string.IsNullOrEmpty(qs["softwareVersion"]))
        {
            this.tbSoftwareVersion.Text = qs["softwareVersion"].ToString();
        }
        if (Regex.IsMatch(qs["holdType"] ?? "", @"^\d+$", RegexOptions.IgnoreCase))
        {
            this.cbHoldType.SelectedValue = qs["holdType"].ToString();
        }
        if (Regex.IsMatch(qs["type"] ?? "", @"^\d+$", RegexOptions.IgnoreCase))
        {
            this.cbType.SelectedValue = qs["type"].ToString();
        }
    }
}