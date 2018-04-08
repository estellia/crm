using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class item_item_price_adjust_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPriceType();
            LoadItemStatus();
            LoadQueryByUrl();
            this.SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            if (!this.BillService.CanCreateBillKind(loggingSessionInfo, "ADJUSTMENTORDER"))
            {
                this.btnCreate.Visible = false;
            }
            this.QueryCondition = GetCondition();

            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
        }
    }
    #region 服务
    protected cPos.Service.AdjustmentOrderService OrderService
    {
        get
        {
            return new cPos.Service.AdjustmentOrderService();
        }
    }
    protected cPos.Service.ItemPriceTypeService PriceTypeService
    {
        get
        {
            return new cPos.Service.ItemPriceTypeService();
        }
    }
    protected cPos.Service.cBillService BillService
    {
        get
        {
            return new cPos.Service.cBillService();
        }
    }
    #endregion
    //查询条件
    protected ItemAdjustQueryCodition QueryCondition
    {
        get
        {
            if (ViewState["queryData"] == null)
            {
                ViewState["queryData"] = new ItemAdjustQueryCodition();
            }
            return ViewState["queryData"] as ItemAdjustQueryCodition;
        }
        set
        {
            ViewState["queryData"] = value;
        }
    }
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(this.SplitPageControl1.PageIndex);
    }
    private ItemAdjustQueryCodition GetCondition()
    {
        var condition = new ItemAdjustQueryCodition();
        if (!string.IsNullOrEmpty(tbOrderNo.Text))
        {
            condition.OrderNo = tbOrderNo.Text;
        }
        if (!string.IsNullOrEmpty(selOrderDate.Value))
        {
            condition.OrderDate = selOrderDate.Value;
        }
        if (!string.IsNullOrEmpty(selBeginDate.Value))
        {
            condition.BeginDate = selBeginDate.Value;
        }
        if (!string.IsNullOrEmpty(selEndDate.Value))
        {
            condition.EndDate = selEndDate.Value;
        }
        if (selItemPriceType.SelectedIndex != 0)
        {
            condition.PriceType = selItemPriceType.SelectedValue;
        }
        if (selStatus.SelectedIndex != 0)
        {
            condition.PriceStatus = selStatus.SelectedValue;
        }
        return condition;
    }
    //加载价格类型数据
    private void LoadPriceType()
    {
        try
        {
            var source = this.PriceTypeService.GetItemPriceTypeList(loggingSessionInfo);
            source.Insert(0, new cPos.Model.ItemPriceTypeInfo { item_price_type_id = "0", item_price_type_name = "---请选择---" });
            this.selItemPriceType.DataTextField = "item_price_type_name";
            this.selItemPriceType.DataValueField = "item_price_type_id";
            this.selItemPriceType.DataSource = source;
            this.selItemPriceType.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //加载状态数据
    private void LoadItemStatus()
    {
        try
        {
            var source = this.BillService.GetBillStatusByKindCode(loggingSessionInfo, "ADJUSTMENTORDER");
            source.Insert(0, new cPos.Model.BillStatusModel { Id = "0", Description = "全部" });
            this.selStatus.DataTextField = "Description";
            this.selStatus.DataValueField = "Status";
            this.selStatus.DataSource = source;
            this.selStatus.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.QueryCondition = GetCondition();
        Query(this.gvPriceAdjust.PageIndex);
    }
    protected void gvPriceAdjust_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "Page": return;
                case "Del": 
                    {
                        if (this.OrderService.SetAdjustmentOrderStatus(loggingSessionInfo, e.CommandArgument.ToString(), cPos.Model.BillActionType.Cancel))
                        {
                            this.InfoBox.ShowPopInfo("删除成功");
                        }
                        else
                        {
                            this.InfoBox.ShowPopError("删除失败");
                        }
                    } break;
                case "Check": 
                    {
                        if (this.OrderService.SetAdjustmentOrderStatus(loggingSessionInfo, e.CommandArgument.ToString(), cPos.Model.BillActionType.Approve))
                        {
                            this.InfoBox.ShowPopInfo("审批成功");
                        }
                        else
                        {
                            this.InfoBox.ShowPopError("审批失败");
                        }
                    } break;
                case "Back": 
                    {
                        if (this.OrderService.SetAdjustmentOrderStatus(loggingSessionInfo, e.CommandArgument.ToString(), cPos.Model.BillActionType.Reject))
                        {
                            this.InfoBox.ShowPopInfo("回退成功");
                        }
                        else
                        {
                            this.InfoBox.ShowPopError("回退失败");
                        }
                    } break;
                default: break;
            }
            Query(0);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //查询数据
    private void Query(int index)
    {
        try
        {
            var qc = this.QueryCondition;
            var source = this.OrderService.SearchItemAdjustmentOrderList(loggingSessionInfo, qc.OrderNo, qc.OrderDate, qc.BeginDate, qc.EndDate, qc.PriceType, qc.PriceStatus, this.SplitPageControl1.PageSize, this.SplitPageControl1.PageSize * index);
            this.SplitPageControl1.RecoedCount = source.ICount;
            this.SplitPageControl1.PageIndex = index;
            this.gvPriceAdjust.DataSource = source.AdjustmentOrderInfoList;
            this.gvPriceAdjust.DataBind();
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
        sb.Append("?and=");
        if (this.gvPriceAdjust.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&orderNo=" + Server.UrlEncode(this.tbOrderNo.Text));
        sb.Append("&orderDate=" + Server.UrlEncode(selOrderDate.Value));
        sb.Append("&beginDate=" + Server.UrlEncode(selBeginDate.Value));
        sb.Append("&endDate=" + Server.UrlEncode(selEndDate.Value));
        sb.Append("&priceType=" + Server.UrlEncode(selItemPriceType.SelectedValue));
        sb.Append("&priceStatus=" + Server.UrlEncode(selStatus.SelectedValue));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", Server.UrlEncode(this.SplitPageControl1.PageIndex.ToString()), Server.UrlEncode(this.SplitPageControl1.PageSize.ToString())));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
    //根据Url加载查询状态
    private void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        this.tbOrderNo.Text = qs["orderNo"] ?? "";
        this.selOrderDate.Value = qs["orderDate"] ?? "";
        this.selBeginDate.Value = qs["beginDate"] ?? "";
        this.selEndDate.Value = qs["endDate"] ?? "";
        this.selItemPriceType.SelectedValue = qs["priceType"]??"--";
        this.selStatus.SelectedValue = qs["priceStatus"] ?? "0";
    }
    protected void gvPriceAdjust_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var obj = (cPos.Model.AdjustmentOrderInfo)(e.Row.DataItem);
            var action = this.BillService.GetBillActionByBillId(loggingSessionInfo, obj.order_id);
            LinkButton delete = (LinkButton)e.Row.FindControl("delete");
            LinkButton check = (LinkButton)e.Row.FindControl("check");
            LinkButton back = (LinkButton)e.Row.FindControl("back");
            HyperLink modify = (HyperLink)e.Row.FindControl("modify");
            if (action != null)
            {
                delete.Visible = action.delete_flag == 1;
                check.Visible = action.approve_flag == 1;
                back.Visible = action.reject_flag == 1;
                modify.Visible = action.modify_flag == 1;
            }
            else
            {
                delete.Visible = false;
                check.Visible = false;
                back.Visible = false;
                modify.Visible = false;
            }
        }
    }
    //查询条件类
    [Serializable]
    public class ItemAdjustQueryCodition
    {
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string PriceType { get; set; }
        public string PriceStatus { get; set; }
    }
}