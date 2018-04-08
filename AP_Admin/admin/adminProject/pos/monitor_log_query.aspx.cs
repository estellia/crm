using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;
using System.Text;
using cPos.Admin.Service;

public partial class pos_monitor_log_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadQueryByUrl();

            this.SplitPageControl1.PageSize = Convert.ToInt32(this.Request.QueryString["pageSize"] ?? "10");
            this.Codition = GetConditonFormUI();
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
    protected cPos.Admin.Service.MonitorService MonitorService
    {
        get
        {
            return new cPos.Admin.Service.MonitorService();
        }
    }
    protected LogQueryConditon Codition
    {
        get
        {
            if (ViewState["logQuery"] == null)
                ViewState["logQuery"] =new LogQueryConditon();
           return ViewState["logQuery"] as LogQueryConditon;
        }
        set
        {
            ViewState["logQuery"] = value;
        }
    }
    private LogQueryConditon GetConditonFormUI()
    {
        var condition = new LogQueryConditon();
        if (!string.IsNullOrEmpty(tbCustomerCode.Text))
        {
            condition.customer_code = tbCustomerCode.Text;
        }
        if (!string.IsNullOrEmpty(selDateStart.Value))
        {
            condition.date_begin = selDateStart.Value;
        }
        if (!string.IsNullOrEmpty(selDateEnd.Value))
        {
            condition.date_end = selDateEnd.Value;
        }
        if (this.tbUnitCode.Text.Trim() != "")
        {
            condition.unit_code = this.tbUnitCode.Text.Trim();
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
            var source = this.MonitorService.SearchMonitorLogList(loggingSessionInfo, 
                qc.customer_code, qc.unit_code, qc.date_begin, qc.date_end, 
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
        sb.Append("&customer_no=" + Server.UrlEncode(this.tbCustomerCode.Text));
        sb.Append("&unit_code=" + Server.UrlEncode(this.tbUnitCode.Text.Trim()));
        sb.Append("&date_begin=" + Server.UrlEncode(this.selDateStart.Value));
        sb.Append("&date_end=" + Server.UrlEncode(this.selDateEnd.Value));
        sb.Append("&page_index=" + Server.UrlEncode(this.SplitPageControl1.PageIndex.ToString()));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
    //根据url加载查询条件
    private void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        if (!string.IsNullOrEmpty(qs["customer_code"]))
        {
            this.tbCustomerCode.Text = qs["customer_code"];
        }
        if (!string.IsNullOrEmpty(qs["unit_code"]))
        {
            this.tbUnitCode.Text = qs["unit_code"];
        }
        if (!string.IsNullOrEmpty(qs["date_begin"]))
        {
            this.selDateStart.Value = qs["date_begin"];
        }
        if (!string.IsNullOrEmpty(qs["date_end"]))
        {
            this.selDateEnd.Value = qs["date_end"];
        }
    }
    //查询条件
    [Serializable]
    protected class LogQueryConditon
    {
        public string monitor_log_id { get; set; }
        public string customer_id { get; set; }
        public string customer_code { get; set; }
        public string unit_id { get; set; }
        public string unit_code { get; set; }
        public string pos_id { get; set; }
        public string date_begin { get; set; }
        public string date_end { get; set; }
        public string remark { get; set; }
        public string create_time { get; set; }
        public string create_user_id { get; set; }
    }
}