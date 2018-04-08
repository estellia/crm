using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;
using cPos.Service;

public partial class report_item_sale_rpt :PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUnitsInfo();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.CurrentQueryCondition = GetConditionFromUI();
        Query(0);
    }
    private void Query(int pageIndex)
    {
      var source =new ReportService().SearchItemSalesReport(this.loggingSessionInfo,CurrentQueryCondition.Item_Code,CurrentQueryCondition.Item_Name,CurrentQueryCondition.BarCode,CurrentQueryCondition.Unit_Ids,CurrentQueryCondition.Order_date_begin,CurrentQueryCondition.Order_date_end,SplitPageControl1.PageSize,pageIndex*SplitPageControl1.PageSize);
        SplitPageControl1.RecoedCount=source.icount;
       itemSalesReportInfoList=source.ItemSalesReportList;
        if (SplitPageControl1.PageIndex != pageIndex)
        {
            Query(SplitPageControl1.PageIndex);
            return;
        }
        else
        {
            itemSalesReportInfoList = source.ItemSalesReportList;
        }
    }
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(SplitPageControl1.PageIndex);
    }
     public IList<cPos.Model.Report.ItemSalesReportInfo> itemSalesReportInfoList
    {
        set;
       get;
  
    }
    private QueryCondition GetConditionFromUI()
    {
        var rult = new QueryCondition();
        rult.Item_Code = tbItemCode.Text.Trim();
        rult.Item_Name = tbItemName.Text.Trim();
        rult.BarCode = tbBarcode.Text.Trim();
        if (selUnit.SelectValues.Length != 0)
        {
            rult.Unit_Ids = string.Join(",", selUnit.SelectValues.Select(o => "'" + o + "'").ToArray());
        }
        rult.Order_date_begin = selOrderDateBegin.Value.Trim();
        rult.Order_date_end = selOrderDateEnd.Value.Trim();
        return rult;
    }
    private void LoadUnitsInfo()
    {
        try
        {
            var service = new UnitService();
            this.selUnit.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(item => new controls_DropDownTree.tvNode
                {
                    CheckState = null,
                    Complete = false,
                    ShowCheck = true,
                    Text = item.Name,
                    Value = item.Id,
                })); 
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    #region Condition 对象定义
    /// <summary>
    /// 查询条件对象
    /// </summary>
    [System.Serializable]
    public class QueryCondition
    {
        public string Item_Code { get; set; }
        public string Item_Name { set; get; }
        public string BarCode { set; get; }
        public string Unit_Ids { set; get; }
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
    protected override void OnPreRender(EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        //if (!string.IsNullOrEmpty(this.Request.QueryString["customer_id"]))
        //{
        //    sb.Append("&customer_id=" + Server.UrlEncode(this.Request.QueryString["customer_id"]));
        //}

        sb.Append("&tbItemCode=" + Server.UrlEncode(tbItemCode.Text));

        sb.Append("&selOrderDateBegin=" + Server.UrlEncode(selOrderDateBegin.Value));
        sb.Append("&selOrderDateEnd=" + Server.UrlEncode(selOrderDateEnd.Value));
        sb.Append("&tbItemName=" + Server.UrlEncode(tbItemName.Text));
        sb.Append("&tbBarcode=" + Server.UrlEncode(tbBarcode.Text));
        //sb.Append("&purchaseDateBegin=" + Server.UrlEncode(tbPurchaseDateBegin.Value));
        //sb.Append("&purchaseDateEnd=" + Server.UrlEncode(tbPurchaseDateEnd.Value));
        //sb.Append("&sn=" + Server.UrlEncode(tbSn.Text));
        //sb.Append("&holdType=" + Server.UrlEncode(cbHoldType.SelectedIndex > 0 ? cbHoldType.SelectedValue : "0"));
        //sb.Append("&type=" + Server.UrlEncode(cbType.SelectedIndex > 0 ? cbType.SelectedValue : "0"));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        //base.OnPreRender(e);
    }
}