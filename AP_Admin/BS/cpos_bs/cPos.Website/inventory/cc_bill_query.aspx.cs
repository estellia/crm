using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using System.Text;

public partial class inventory_cc_bill_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initStatus();
            initUnitTree();
            initDataFrom();
            var isDispalyAdd = new cBillService().CanCreateBillKind(loggingSessionInfo, "CC");
            if (isDispalyAdd)
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }
            SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            getConditionFromBack();
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
    //加载状态函数
    private void initStatus()
    {
        var source = new cBillService().GetBillStatusByKindCode(loggingSessionInfo, "CC");
        selStatus.DataValueField = "Status";
        selStatus.DataTextField = "Description";
        selStatus.DataSource = source;
        selStatus.DataBind();
        ListItem lis = new ListItem();
        lis.Value = "";
        lis.Text = "全部";
        selStatus.Items.Insert(0, lis);
    }
    //加载盘点单位函数
    private void initUnitTree()
    {
        try
        {
            var service = new UnitService();
            this.selUnit.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(item => new controls_DropDownTree.tvNode
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
    //加载仓库函数 在盘点单位选择后触发
    private void initWarehouse()
    {
        var source = new PosService().GetWarehouseByUnitID(loggingSessionInfo, selUnit.SelectedValue);
        selWarehouse.DataValueField = "ID";
        selWarehouse.DataTextField = "Name";
        selWarehouse.DataSource = source;
        selWarehouse.DataBind();
    }
    //加载数据来源
    private void initDataFrom()
    {
        var source = new PropService().GetPropListByParentId(loggingSessionInfo, "DATAFROM", "AAE0B5C3708F4189B6D775D234295CB5");
        SelectDataFrom.DataValueField = "Prop_Id";
        SelectDataFrom.DataTextField = "Prop_Name";
        SelectDataFrom.DataSource = source;
        SelectDataFrom.DataBind();
        ListItem lis = new ListItem();
        lis.Text = "--请选择--";
        lis.Value = "";
        SelectDataFrom.Items.Insert(0, lis);
    }
    private void Query(int pageIndex)
    {
        try
        {
            var service = new CCService();
            var querylist = service.SearchCCInfo(loggingSessionInfo, CurrentQueryCondition.order_no, CurrentQueryCondition.status, CurrentQueryCondition.unit_id, CurrentQueryCondition.warehouse_id, CurrentQueryCondition.order_date_begin, CurrentQueryCondition.order_date_end, CurrentQueryCondition.complete_date_begin, CurrentQueryCondition.complete_date_end, CurrentQueryCondition.data_from_id
                , SplitPageControl1.PageSize
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
                gvCcBill.DataSource = querylist.CCInfoList;
                gvCcBill.DataBind();
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }


    private void getConditionFromBack()
    {
        if (Request.QueryString["order_no"] != null)
            tbOrderNo.Text = Request.QueryString["order_no"].ToString() ?? "";
        if (Request.QueryString["status"] != null)
        {
            this.selStatus.Text = Request.QueryString["status"];
        }
        if (Request.QueryString["unit_id"] != null)
        {
            selUnit.SelectedValue = Request.QueryString["unit_id"].ToString();
            selUnit.SelectedText = Request.QueryString["unit_name"].ToString();
            initWarehouse();
        }
        if (!string.IsNullOrEmpty(Request.QueryString["warehouse_id"]))
        {
            selWarehouse.SelectedValue = Request.QueryString["warehouse_id"].ToString() ?? "";
        }
        if (Request.QueryString["order_date_begin"] != null)
            selOrderDateBegin.Value = Request.QueryString["order_date_begin"].ToString() ?? "";
        if (Request.QueryString["order_date_end"] != null)
            selOrderDateEnd.Value = Request.QueryString["order_date_end"].ToString() ?? "";
        if (Request.QueryString["complete_date_begin"] != null)
            selCompleteDateBegin.Value = Request.QueryString["complete_date_begin"].ToString() ?? "";
        if (Request.QueryString["complete_date_end"] != null)
            selCompleteDateEnd.Value = Request.QueryString["complete_date_end"].ToString() ?? "";
        if (Request.QueryString["data_from_id"] != null)
        {
            this.SelectDataFrom.SelectedValue = Request.QueryString["data_from_id"];
        }
    }
    //获取当前的查询条件
    private QueryCondition GetConditionFromUI()
    {
        QueryCondition rult = new QueryCondition();
        rult.order_no = tbOrderNo.Text.Trim();
        rult.status = selStatus.SelectedValue.Trim();
        rult.unit_id = selUnit.SelectedValue;
        rult.unit_name = selUnit.SelectedText;
        rult.warehouse_id = selWarehouse.SelectedValue.Trim();
        rult.order_date_begin = selOrderDateBegin.Value;
        rult.order_date_end = selOrderDateEnd.Value;
        rult.complete_date_begin = selCompleteDateBegin.Value;
        rult.complete_date_end = selCompleteDateEnd.Value;
        rult.data_from_id = SelectDataFrom.SelectedValue;
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
        public string status { get; set; }
        public string unit_id { get; set; }
        public string unit_name { get; set; }
        public string warehouse_id { get; set; }
        public string order_date_begin { get; set; }
        public string order_date_end { get; set; }
        public string complete_date_begin { get; set; }
        public string complete_date_end { get; set; }
        public string data_from_id { get; set; }
        //如果有其它条件可以在这里定义
    }
    #endregion

    //生成 From Url 参数
    protected override void OnPreRender(EventArgs e)
    {
        //生成 _from 隐藏域字段
        StringBuilder sb = new StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (this.gvCcBill.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&order_no=" + this.Server.UrlEncode(CurrentQueryCondition.order_no));
        sb.Append("&status=" + this.Server.UrlEncode(CurrentQueryCondition.status));
        sb.Append("&unit_id=" + this.Server.UrlEncode(CurrentQueryCondition.unit_id));
        sb.Append("&unit_name=" + this.Server.UrlEncode(CurrentQueryCondition.unit_name));
        sb.Append("&warehouse_id=" + this.Server.UrlEncode(CurrentQueryCondition.warehouse_id));
        sb.Append("&order_date_begin=" + this.Server.UrlEncode(CurrentQueryCondition.order_date_begin));
        sb.Append("&order_date_end=" + this.Server.UrlEncode(CurrentQueryCondition.order_date_end));
        sb.Append("&complete_date_begin=" + this.Server.UrlEncode(CurrentQueryCondition.complete_date_begin));
        sb.Append("&complete_date_end=" + this.Server.UrlEncode(CurrentQueryCondition.complete_date_end));
        sb.Append("&data_from_id=" + this.Server.UrlEncode(CurrentQueryCondition.data_from_id));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
    }
    protected void gvCcBill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var obj = (cPos.Model.CCInfo)e.Row.DataItem;
            var flagmodel = new cBillService().GetBillActionByBillId(loggingSessionInfo, obj.order_id.ToString());
            var lbcheck = (LinkButton)e.Row.FindControl("check");
            var lbback = (LinkButton)e.Row.FindControl("back");
            var lbdelete = (LinkButton)e.Row.FindControl("delete");
            var lbmodify = (LinkButton)e.Row.FindControl("modify");
            var createAj = (LinkButton)e.Row.FindControl("createAj");
            if (flagmodel == null)
            {
                lbcheck.Visible = false;
                lbdelete.Visible = false;
                lbmodify.Visible = false;
                lbback.Visible = false;
                createAj.Visible = false;
                return;
            }
            else {
                if (flagmodel.approve_flag == 1)
                {
                    lbcheck.Visible = true;
                }
                else
                {
                    lbcheck.Visible = false;
                }
                if (flagmodel.delete_flag == 1)
                {
                    lbdelete.Visible = true;
                }
                else
                {
                    lbdelete.Visible = false;
                }
                if (flagmodel.modify_flag == 1)
                {
                    lbmodify.Visible = true;
                }
                else
                {
                    lbmodify.Visible = false;
                }
                if (flagmodel.reject_flag == 1)
                    lbback.Visible = true;
                else
                    lbback.Visible = false;
                if (new CCService().IsExistAJOrder(loggingSessionInfo, obj.order_id))
                {
                    createAj.Visible = true;
                }
                else
                {
                    createAj.Visible = false;
                }
            }
        }
    }
    protected void gvCcBill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var serv = new CCService();
        try
        {
            switch (e.CommandName)
            {
                case "Page": return;
                case "Check":
                    {
                        if (serv.SetCCOrderStatus(loggingSessionInfo, e.CommandArgument.ToString(), BillActionType.Approve))
                        {
                            this.InfoBox.ShowPopInfo("审批成功!");
                            Query(0);
                        }
                        else
                        {
                            this.InfoBox.ShowPopError("审批失败!");
                        }
                    } break;
                case "Back":
                    {
                        if (serv.SetCCOrderStatus(loggingSessionInfo, e.CommandArgument.ToString(), BillActionType.Reject))
                        {
                            this.InfoBox.ShowPopInfo("回退成功!");
                            Query(0);
                        }
                        else
                        {
                            this.InfoBox.ShowPopError("回退失败!");
                        }
                    } break;
                case "cDelete":
                    {
                        if (serv.SetCCOrderStatus(loggingSessionInfo, e.CommandArgument.ToString(), BillActionType.Cancel))
                        {
                            this.InfoBox.ShowPopInfo("删除成功!");
                            Query(0);
                        }
                        else
                        {
                            this.InfoBox.ShowPopError("删除失败!");
                        }
                    } break;
                case "cCreateAj":
                    {
                        var service = new CCService();
                        if (service.IsCCDifference(loggingSessionInfo, e.CommandArgument.ToString()))
                        {
                            if (service.SetCCToAJ(loggingSessionInfo, e.CommandArgument.ToString()))
                            {
                                this.InfoBox.ShowPopInfo("生成调整单成功!");
                                //(sender as LinkButton).Visible = false;
                            }
                            else
                            {
                                this.InfoBox.ShowPopError("生成调整单失败!");
                            }
                        }
                        else
                        {
                            this.InfoBox.ShowPopError("盘点单不存在有差异的sku,生成调整单失败!");
                        }
                        Query(0);
                    }break;
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
            initWarehouse();
    }
}