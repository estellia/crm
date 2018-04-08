using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;

public partial class report_unit_sale_rpt_content :PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CurrentQueryCondition = GetConditionfromUI();
            Query(0);
        }


    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.CurrentQueryCondition = GetConditionfromUI();
        Query(0);
    }
    private QueryCondition GetConditionfromUI()
    {
        var rult = new QueryCondition();
        var qs = this.Request.QueryString;
        if (!string.IsNullOrEmpty(qs["unit_id"]))
        {
            rult.Unit_Ids = string.Join(",", qs["unit_id"].Split(',').Select(o => "'" + o + "'").ToArray());
        }
        rult.Order_No = qs["order_no"]??"";
        rult.Order_Date_Begin = qs["order_date_begin"]??"";
        rult.Order_Date_End = qs["order_date_end"]??"";
        return rult;
    }

    public cPos.Model.Report.SalesReportInfo ReportInfoList
    {
        set;
        get;
    }
    //查询
    private void Query(int pageIndex)
    {

        var source = new ReportService().SearchSalesReport(this.loggingSessionInfo, this.CurrentQueryCondition.Unit_Ids, this.CurrentQueryCondition.Order_No, this.CurrentQueryCondition.Order_Date_Begin, this.CurrentQueryCondition.Order_Date_End, SplitPageControl1.PageSize, pageIndex * SplitPageControl1.PageSize);
        this.ICount = source.icount;
        SplitPageControl1.RecoedCount = source.icount;
        SplitPageControl1.PageIndex = pageIndex;
        //验证查询当前页索引是否在记录总数范围内。
        if (SplitPageControl1.PageIndex != pageIndex)
        {
            Query(SplitPageControl1.PageIndex);
            return;
        }
        else
        {
            ReportInfoList = source;
        }
    }
    protected int ICount
    {
        get
        {
            if (ViewState["count"] == null)
                ViewState["count"] = 0;
            return Convert.ToInt32(ViewState["count"]);
        }
        set
        {
            ViewState["count"] = value;
        }
    }
    private IList<cPos.Model.Report.SalesReportInfo> GetReportInfoList(IList<cPos.Model.Report.SalesReportInfo> iReportList)
    {
        IList<cPos.Model.Report.SalesReportInfo> itemPriceInfos = iReportList;
        return itemPriceInfos;
    }
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(SplitPageControl1.PageIndex);
    }
    public QueryCondition CurrentQueryCondition
    {
        get
        {
            if (this.ViewState["QueryCondition"] == null)
            {
                this.ViewState["QueryCondition"] = new QueryCondition();
            }
            return this.ViewState["QueryCondition"] as QueryCondition;
        }
        set
        {
            this.ViewState["QueryCondition"] = value;
        }
    }
    #region Condition 对象定义
    /// <summary>
    /// 查询条件对象
    /// </summary>
    [System.Serializable]
    public class QueryCondition
    {
        public string Unit_Ids { get; set; }
        public string Order_No { set; get; }
        public string Order_Date_Begin { set; get; }
        public string Order_Date_End { set; get; }

        //如果有其它条件可以在这里定义
    }
    #endregion
    //protected override void OnPreRender(EventArgs e)
    //{
    //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //    sb.Append(this.Request.Url.LocalPath);
    //    sb.Append("?and=");
    //    if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
    //    {
    //        sb.Append("&cur_menu_id=" + Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
    //    }
    //    //if (!string.IsNullOrEmpty(this.Request.QueryString["customer_id"]))
    //    //{
    //    //    sb.Append("&customer_id=" + Server.UrlEncode(this.Request.QueryString["customer_id"]));
    //    //}

    //    sb.Append("&tbOrderNo=" + Server.UrlEncode(tbOrderNo.Text));

    //    sb.Append("&selOrderDateBegin=" + Server.UrlEncode(selOrderDateBegin.Value));
    //    sb.Append("&selOrderDateEnd=" + Server.UrlEncode(selOrderDateEnd.Value));
    //    //sb.Append("&purchaseDateBegin=" + Server.UrlEncode(tbPurchaseDateBegin.Value));
    //    //sb.Append("&purchaseDateEnd=" + Server.UrlEncode(tbPurchaseDateEnd.Value));
    //    //sb.Append("&sn=" + Server.UrlEncode(tbSn.Text));
    //    //sb.Append("&holdType=" + Server.UrlEncode(cbHoldType.SelectedIndex > 0 ? cbHoldType.SelectedValue : "0"));
    //    //sb.Append("&type=" + Server.UrlEncode(cbType.SelectedIndex > 0 ? cbType.SelectedValue : "0"));
    //    sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
    //    this.ClientScript.RegisterHiddenField("_from", sb.ToString());
    //    //base.OnPreRender(e);
    //}

}