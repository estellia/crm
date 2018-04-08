using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;
using System.Text;
using cPos.Admin.Service;

public partial class customer_brand_customer_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadQueryByUrl();

            this.SplitPageControl1.PageSize = Convert.ToInt32(this.Request.QueryString["pageSize"] ?? "10");
           this.Codition =  GetConditonFormUI();
           if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
           {
               Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
           }
        }
    }
    protected cPos.Admin.Service.UnitService UnitService
    {
        get
        {
            return new cPos.Admin.Service.UnitService();
        }
    }
    protected cPos.Admin.Service.Implements.CustomerService CustomerService
    {
        get
        {
            return new cPos.Admin.Service.Implements.CustomerService();
        }
    }
    protected QueryConditon Codition
    {
        get
        {
            if (ViewState["logQuery"] == null)
                ViewState["logQuery"] =new QueryConditon();
           return ViewState["logQuery"] as QueryConditon;
        }
        set
        {
            ViewState["logQuery"] = value;
        }
    }
    private QueryConditon GetConditonFormUI()
    {
        var condition = new QueryConditon();
        if (!string.IsNullOrEmpty(tbBcCode.Text))
        {
            condition.brand_customer_code = tbBcCode.Text.Trim();
        }
        if (!string.IsNullOrEmpty(tbBcName.Text))
        {
            condition.brand_customer_name = tbBcName.Text.Trim();
        }
        return condition;
    }
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(this.SplitPageControl1.PageIndex);
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
       this.Codition= this.GetConditonFormUI();
        Query(0);
    }
    protected void btnResetClick(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.RawUrl);
    }
    //查询
    private void Query(int index)
    {
        try
        {
            var qc = this.Codition;
            var source = this.CustomerService.SearchBrandCustomerList(loggingSessionInfo,
                qc.brand_customer_code, qc.brand_customer_name, 
                this.SplitPageControl1.PageSize, this.SplitPageControl1.PageSize * index);
            this.SplitPageControl1.RecoedCount = source.icount;
            this.gvPos.DataSource = source.List;
            this.gvPos.DataBind();
            if (this.SplitPageControl1.PageIndex != index)
            {
                Query(this.SplitPageControl1.PageIndex);
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }

    //前台注册_from字段
    protected override void OnPreRender(EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?");
        if (this.gvPos.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&brand_customer_code=" + Server.UrlEncode(this.tbBcCode.Text.Trim()));
        sb.Append("&brand_customer_name=" + Server.UrlEncode(this.tbBcName.Text.Trim()));
        sb.Append("&page_index=" + Server.UrlEncode(this.SplitPageControl1.PageIndex.ToString()));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
    //根据url加载查询条件
    private void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        if (!string.IsNullOrEmpty(qs["brand_customer_code"]))
        {
            this.tbBcCode.Text = qs["brand_customer_code"];
        }
        if (!string.IsNullOrEmpty(qs["brand_customer_name"]))
        {
            this.tbBcName.Text = qs["brand_customer_name"];
        }
    }
    //查询条件
    [Serializable]
    protected class QueryConditon
    {
        public string brand_customer_code { get; set; }
        public string brand_customer_name { get; set; }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {

    }
}