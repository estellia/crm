using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using cPos.Service;
using cPos.Model;
using System.Web.UI.HtmlControls;

public partial class inventory_inout_bill_in_query : PageBase
{
    protected bool isModify = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initStatus();
            LoadUnitsInfo();
            loadReasonType();
            initDataFrom();
            SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            GetConditionFromBack();
            this.CurrentQueryCondition = GetConditionFromUI();
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        btnQuery.Enabled = false;
        this.CurrentQueryCondition = GetConditionFromUI();
        Query(0);
        btnQuery.Enabled = true;

    }
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(SplitPageControl1.PageIndex);
    }
    protected void gvInoutBill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var obj = (cPos.Model.InoutInfo)e.Row.DataItem;
            var flagmodel = new cBillService().GetBillActionByBillId(loggingSessionInfo, obj.order_id.ToString());
            var lbcheck = (LinkButton)e.Row.FindControl("check");
            var lbback = (LinkButton)e.Row.FindControl("back");
            var modify = (HtmlAnchor)e.Row.FindControl("modify");
            var delete = (LinkButton)e.Row.FindControl("delete");
            if (flagmodel == null)
            {
                lbcheck.Visible = false;
                modify.Visible = false;
                lbback.Visible = false;
                delete.Visible = false;
                return;
            }
            // var lbmodify = (LinkButton)e.Row.FindControl("modify");
            if (flagmodel != null)
            {
                if (flagmodel.approve_flag == 1)
                {
                    lbcheck.Visible = true;
                }
                else
                {
                    lbcheck.Visible = false;
                }
                if (flagmodel.modify_flag == 1)
                {
                    modify.Visible = true;
                    //isModify = true;
                }
                else
                {
                    modify.Visible = false;
                    // isModify = false;
                }
                if (flagmodel.reject_flag == 1)
                { lbback.Visible = true; }
                else
                { lbback.Visible = false; }
                delete.Visible = flagmodel.delete_flag == 1;
            }
        }
    }
    protected void btnResetClick(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.RawUrl);
    }
    protected void gvInoutBill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var serv = new InoutService();
        try
        {
            switch (e.CommandName)
            {
                case "Page": return;
                case "Check":
                    {
                        if (serv.SetInoutOrderStatus(loggingSessionInfo, e.CommandArgument.ToString(), BillActionType.Approve))
                        {
                            this.InfoBox.ShowPopInfo("审批成功！");
                        }
                        else
                        {
                            this.InfoBox.ShowPopError("审批失败！");
                        }
                    } break;
                case "Back":
                    {
                        if (serv.SetInoutOrderStatus(loggingSessionInfo, e.CommandArgument.ToString(), BillActionType.Reject))
                        {
                            this.InfoBox.ShowPopInfo("回退成功！");
                        }
                        else
                        {
                            this.InfoBox.ShowPopError("回退失败！");
                        }
                    } break;
                case "inDelete":
                    {
                        if (serv.SetInoutOrderStatus(loggingSessionInfo, e.CommandArgument.ToString(), BillActionType.Cancel))
                        {
                            this.InfoBox.ShowPopInfo("删除成功!");
                        }
                        else
                        {
                            this.InfoBox.ShowPopError("删除失败!");
                        }
                    }break;
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


    private void Query(int pageIndex)
    {
        try
        {
            var service = new InoutService();
            var querylist = service.SearchInoutInfo(loggingSessionInfo, CurrentQueryCondition.order_no, CurrentQueryCondition.order_reason_type_id, CurrentQueryCondition.sales_unit_id, CurrentQueryCondition.warehouse_id, CurrentQueryCondition.purchase_unit_id, CurrentQueryCondition.status, CurrentQueryCondition.order_date_begin, CurrentQueryCondition.order_date_end, CurrentQueryCondition.complete_date_begin, CurrentQueryCondition.complete_date_end, CurrentQueryCondition.data_from_id
                , CurrentQueryCondition.ref_order_no, "C1D407738E1143648BC7980468A399B8", SplitPageControl1.PageSize
                , pageIndex * SplitPageControl1.PageSize);
            SplitPageControl1.RecoedCount = querylist.ICount;
            SplitPageControl1.PageIndex = pageIndex;
            //验证查询当前页索引是否在记录总数范围内。
            if (SplitPageControl1.PageIndex != pageIndex)
            {
                Query(SplitPageControl1.PageIndex);
                return;
            }
            else
            {
                gvInoutBill.DataSource = querylist.InoutInfoList;
                gvInoutBill.DataBind();
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }



    private void GetConditionFromBack()
    {
        if (this.Request.QueryString["order_no"] != null)
            tbOrderNo.Text = this.Request.QueryString["order_no"].ToString() ?? "";
        if (Request.QueryString["status"] != null)
            this.selStatus.Text = Request.QueryString["status"];

        if (Request.QueryString["sales_unit_id"] != null)
        {
            selSalesUnit.SelectedValue = Request.QueryString["sales_unit_id"].ToString();
            selSalesUnit.SelectedText = Request.QueryString["sales_unit_name"].ToString();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["warehouse_id"]))
            selWarehouse.SelectedValue = Request.QueryString["warehouse_id"].ToString() ?? "";

        if (Request.QueryString["purchase_unit_id"] != null)
        {
            this.selPurchaseUnit.SelectedValue = Request.QueryString["purchase_unit_id"].ToString();
            selPurchaseUnit.SelectedText = Request.QueryString["purchase_unit_name"].ToString();
        }
            if (Request.QueryString["order_date_begin"] != null)
            selOrderDateBegin.Value = Request.QueryString["order_date_begin"].ToString() ?? "";
        if (Request.QueryString["order_date_end"] != null)
            selOrderDateEnd.Value = Request.QueryString["order_date_end"].ToString() ?? "";
        if (Request.QueryString["complete_date_begin"] != null)
            selCompleteDateBegin.Value = Request.QueryString["complete_date_begin"].ToString() ?? "";
        if (Request.QueryString["complete_date_end"] != null)
            selCompleteDateEnd.Value = Request.QueryString["complete_date_end"].ToString() ?? "";
        if (Request.QueryString["order_reason_type_id"] != null)
            this.selOrderType.SelectedValue = Request.QueryString["order_reason_type_id"];
        if (Request.QueryString["ref_order_no"] != null)
            tbRefOrderNo.Text = Request.QueryString["ref_order_no"].ToString() ?? "";
        if (Request.QueryString["data_from_id"] != null)
            this.selDataFrom.SelectedValue = Request.QueryString["data_from_id"];
    }
    //获取当前的查询条件
    private QueryCondition GetConditionFromUI()
    {
        QueryCondition rult = new QueryCondition();
        if (!string.IsNullOrEmpty(this.tbOrderNo.Text.Trim()))
        {
            rult.order_no = tbOrderNo.Text.Trim();
        }
        if (!string.IsNullOrEmpty(this.selOrderType.SelectedValue))
        {
            rult.order_reason_type_id = selOrderType.SelectedValue;
        }
        if (!string.IsNullOrEmpty(this.selStatus.SelectedValue.Trim()))
        {
            rult.status = selStatus.SelectedValue.Trim();
        }
        if (!string.IsNullOrEmpty(this.selSalesUnit.SelectedValue))
        {
            rult.sales_unit_id = selSalesUnit.SelectedValue;
            rult.sales_unit_name = selSalesUnit.SelectedText;
        }
        if (!string.IsNullOrEmpty(this.selPurchaseUnit.SelectedValue))
        {
            rult.purchase_unit_id = selPurchaseUnit.SelectedValue;
            rult.purchase_unit_name = selPurchaseUnit.SelectedText;
            initWarehouse();
        }
        if (!string.IsNullOrEmpty(this.selWarehouse.SelectedValue.Trim()))
        {
            rult.warehouse_id = selWarehouse.SelectedValue.Trim();
        }
        if (!string.IsNullOrEmpty(this.selOrderDateBegin.Value))
        {
            rult.order_date_begin = selOrderDateBegin.Value;
        }
        if (!string.IsNullOrEmpty(this.selOrderDateEnd.Value))
        {
            rult.order_date_end = selOrderDateEnd.Value;
        }
        if (!string.IsNullOrEmpty(this.selCompleteDateBegin.Value))
        {
            rult.complete_date_begin = selCompleteDateBegin.Value;
        }
        if (!string.IsNullOrEmpty(this.selCompleteDateEnd.Value))
        {
            rult.complete_date_end = selCompleteDateEnd.Value;
        }
        if (!string.IsNullOrEmpty(this.selDataFrom.SelectedValue))
        {
            rult.data_from_id = selDataFrom.SelectedValue;
        }
        if (!string.IsNullOrEmpty(this.tbRefOrderNo.Text.Trim()))
        {
            rult.ref_order_no = tbRefOrderNo.Text.Trim();
        }
        return rult;
    }
    //当前查询条件
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
        public string order_no { get; set; }
        public string order_reason_type_id { get; set; }
        public string status { get; set; }
        public string sales_unit_id { get; set; }
        public string sales_unit_name { get; set; }

        public string warehouse_id { get; set; }
        public string purchase_unit_id { get; set; }
        public string purchase_unit_name { get; set; }

        public string order_date_begin { get; set; }
        public string order_date_end { get; set; }
        public string complete_date_begin { get; set; }
        public string complete_date_end { get; set; }
        public string data_from_id { get; set; }
        public string ref_order_no { get; set; }
        //如果有其它条件可以在这里定义
    }
    #endregion


    #region 加载单据类型数据loadReasonType()
    private void loadReasonType()
    {
        try
        {
            var ddlList = new OrderReasonTypeService().GetOrderReasonTypeListByOrderTypeCode(loggingSessionInfo, "RO.ReasonType");
            this.selOrderType.DataSource = ddlList;
            selOrderType.DataTextField = "reason_type_name";
            selOrderType.DataValueField = "reason_type_id";
            selOrderType.DataBind();
            ListItem lis = new ListItem();
            lis.Text = "--请选择--";
            lis.Value = "";
            selOrderType.Items.Insert(0, lis);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    #endregion
    #region 加载单位树的数据LoadUnitsInfo()
    //加载组织信息
    private void LoadUnitsInfo()
    {
        try
        {
            var service = new UnitService();
            this.selSalesUnit.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(item => new controls_DropDownTree.tvNode
                {
                    CheckState = false,
                    Complete = false,
                    ShowCheck = false,
                    Text = item.Name,
                    Value = item.Id,
                }));
            this.selPurchaseUnit.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(item => new controls_DropDownTree.tvNode
                {
                    CheckState = false,
                    Complete = false,
                    ShowCheck = false,
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
    #endregion
    //加载状态函数
    private void initStatus()
    {
        var source = new cBillService().GetBillStatusByKindCode(loggingSessionInfo, "RO");
        source.Insert(0, new BillStatusModel { Status = "", Description = "全部" });
        selStatus.DataValueField = "Status";
        selStatus.DataTextField = "Description";
        selStatus.DataSource = source;
        selStatus.DataBind();
    }
    //加载仓库函数 在盘点单位选择后触发
    private void initWarehouse()
    {
        try
        {
            var source = new PosService().GetWarehouseByUnitID(loggingSessionInfo, selPurchaseUnit.SelectedValue);
            selWarehouse.DataValueField = "ID";
            selWarehouse.DataTextField = "Name";
            selWarehouse.DataSource = source;
            selWarehouse.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //加载数据来源
    private void initDataFrom()
    {
        var source = new PropService().GetPropListByParentId(loggingSessionInfo, "DATAFROM", "AAE0B5C3708F4189B6D775D234295CB5");
        selDataFrom.DataValueField = "Prop_Id";
        selDataFrom.DataTextField = "Prop_Name";
        selDataFrom.DataSource = source;
        selDataFrom.DataBind();
        ListItem lis = new ListItem();
        lis.Text = "--请选择--";
        lis.Value = "";
        selDataFrom.Items.Insert(0, lis);
    }
    //调用仓库函数
    protected void Button1_Click(object sender, EventArgs e)
    {
        initWarehouse();
    }

    //生成 From Url 参数
    protected override void OnPreRender(EventArgs e)
    {
        //生成 _from 隐藏域字段
        StringBuilder sb = new StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (this.gvInoutBill.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        sb.Append("&order_no=" + this.Server.UrlEncode(CurrentQueryCondition.order_no));
        sb.Append("&order_reason_type_id=" + this.Server.UrlEncode(CurrentQueryCondition.order_reason_type_id));
        sb.Append("&status=" + this.Server.UrlEncode(CurrentQueryCondition.status));
        sb.Append("&sales_unit_id=" + this.Server.UrlEncode(CurrentQueryCondition.sales_unit_id));
        sb.Append("&sales_unit_name=" + this.Server.UrlEncode(CurrentQueryCondition.sales_unit_name));
        sb.Append("&warehouse_id=" + this.Server.UrlEncode(CurrentQueryCondition.warehouse_id));
        sb.Append("&purchase_unit_id=" + this.Server.UrlEncode(CurrentQueryCondition.purchase_unit_id));
        sb.Append("&purchase_unit_name=" + this.Server.UrlEncode(CurrentQueryCondition.purchase_unit_name));
        sb.Append("&order_date_begin=" + this.Server.UrlEncode(CurrentQueryCondition.order_date_begin));
        sb.Append("&order_date_end=" + this.Server.UrlEncode(CurrentQueryCondition.order_date_end));
        sb.Append("&complete_date_begin=" + this.Server.UrlEncode(CurrentQueryCondition.complete_date_begin));
        sb.Append("&complete_date_end=" + this.Server.UrlEncode(CurrentQueryCondition.complete_date_end));
        sb.Append("&data_from_id=" + this.Server.UrlEncode(CurrentQueryCondition.data_from_id));
        sb.Append("&ref_order_no=" + this.Server.UrlEncode(CurrentQueryCondition.ref_order_no));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
    }
}
