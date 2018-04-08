using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;

public partial class report_shift_sale_rpt_content : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CurrentQueryCondition = GetConditionfromUI();
            Query(0);
        }
    }
    #region
    [System.Serializable]
    public class QueryCondition
    {
        public string Unit_Ids { get; set; }
        public string User_Names { set; get; }

        public string Order_date_begin { set; get; }
        public string Order_date_end { set; get; }


        //如果有其它条件可以在这里定义
    }
    #endregion
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
    public IList<cPos.Model.ShiftInfo> ShiftInfos
    {
        set;
        get;
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
    public cPos.Model.ShiftInfo Info
    {
        get;
        set;
    }
    private void Query(int pageIndex)
    {
        var source = new ReportService().SearchShiftReport(this.loggingSessionInfo, CurrentQueryCondition.Unit_Ids, CurrentQueryCondition.User_Names, CurrentQueryCondition.Order_date_begin, CurrentQueryCondition.Order_date_end, SplitPageControl1.PageSize, pageIndex * SplitPageControl1.PageSize);
        this.ICount = source.icount;
        Info = source;
        SplitPageControl1.RecoedCount = source.icount;
        ShiftInfos = source.ShiftListInfo;
        if (SplitPageControl1.PageIndex != pageIndex)
        {
            Query(SplitPageControl1.PageIndex);
            return;
        }
        else
        {
            ShiftInfos = source.ShiftListInfo;
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
        if (!string.IsNullOrEmpty(qs["user_name"]))
        {
            rult.User_Names = string.Join(",", (qs["user_name"] ?? "").Split(',').Select(o => "'" + o + "'").ToArray());
        }
        rult.Order_date_begin = qs["order_date_begin"] ?? "";
        rult.Order_date_end = qs["order_date_end"] ?? "";
        return rult;
    }
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(SplitPageControl1.PageIndex);
    }
}