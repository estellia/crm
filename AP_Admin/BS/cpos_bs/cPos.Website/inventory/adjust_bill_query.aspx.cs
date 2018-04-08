using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using System.Text;

public partial class inventory_adjust_bill_query :PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initStatus();
            initDataFrom();
            loadReasonType();
            LoadUnitsInfo();
            var isDispalyAdd = new cBillService().CanCreateBillKind(loggingSessionInfo, "AJ");
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

    #region 加载单据类型数据loadReasonType()
    private void loadReasonType()
    {
        try
        {
            var ddlList = new OrderReasonTypeService().GetOrderReasonTypeListByOrderTypeCode(loggingSessionInfo, "AJ.ReasonType");
            this.selReasonType.DataSource = ddlList;
            selReasonType.DataTextField = "reason_type_name";
            selReasonType.DataValueField = "reason_type_id";
            selReasonType.DataBind();
            ListItem lis = new ListItem();
            lis.Text = "--请选择--";
            lis.Value = "";
            selReasonType.Items.Insert(0, lis);
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
            this.selUnit.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(item => new controls_DropDownTree.tvNode { 
                    CheckState = false,
                    Complete = false,
                    ShowCheck = false,
                    Text=item.Name,
                    Value=item.Id,
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
        var source = new cBillService().GetBillStatusByKindCode(loggingSessionInfo, "AJ");
        selStatus.DataValueField = "Status";
        selStatus.DataTextField = "Description";
        selStatus.DataSource = source;
        selStatus.DataBind();
        ListItem lis = new ListItem();
        lis.Value = "";
        lis.Text = "全部";
        selStatus.Items.Insert(0, lis);
    }
    protected void btnResetClick(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.Path);
    }
    //加载仓库函数 在盘点单位选择后触发
    private void initWarehouse()
    {
        try
        {
            var source = new PosService().GetWarehouseByUnitID(loggingSessionInfo, selUnit.SelectedValue);
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
            var service = new AJService();
            var querylist = service.SearchAJInfo(loggingSessionInfo, CurrentQueryCondition.order_no, CurrentQueryCondition.status,
                CurrentQueryCondition.unit_id, CurrentQueryCondition.warehouse_id, CurrentQueryCondition.order_date_begin,
                CurrentQueryCondition.order_date_end, CurrentQueryCondition.order_reason_type_id, 
                CurrentQueryCondition.ref_order_no, CurrentQueryCondition.data_from_id
                , SplitPageControl1.PageSize , pageIndex * SplitPageControl1.PageSize);
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
                gvAdjBill.DataSource = querylist.InoutInfoList;
                gvAdjBill.DataBind();
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
            selWarehouse.SelectedValue = Request.QueryString["warehouse_id"].ToString() ??"" ;
        }
        if (Request.QueryString["order_date_begin"] != null)
            selOrderDateBegin.Value = Request.QueryString["order_date_begin"].ToString() ?? "";
        if (Request.QueryString["order_date_end"] != null)
            selOrderDateEnd.Value = Request.QueryString["order_date_end"].ToString() ?? "";
        if (Request.QueryString["order_reason_type_id"] != null)
        {
            this.selReasonType.SelectedValue = Request.QueryString["order_reason_type_id"];
        }
        if (Request.QueryString["ref_order_no"] != null)
            tbRefOrderNo.Text = Request.QueryString["ref_order_no"].ToString() ?? "";
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
        rult.warehouse_name = selWarehouse.Text;

        rult.order_date_begin = selOrderDateBegin.Value;
        rult.order_date_end = selOrderDateEnd.Value;
        rult.order_reason_type_id = selReasonType.SelectedValue;
        rult.ref_order_no =tbRefOrderNo.Text;
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
        public string warehouse_name { get; set; }

        public string order_date_begin { get; set; }
        public string order_date_end { get; set; }
        public string ref_order_no { get; set; }
        public string order_reason_type_id { get; set; }
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
        if (this.gvAdjBill.DataSource != null)
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
        sb.Append("&warehouse_name=" + this.Server.UrlEncode(CurrentQueryCondition.warehouse_name));

        sb.Append("&order_date_begin=" + this.Server.UrlEncode(CurrentQueryCondition.order_date_begin));
        sb.Append("&order_date_end=" + this.Server.UrlEncode(CurrentQueryCondition.order_date_end));
        sb.Append("&order_reason_type_id=" + this.Server.UrlEncode(CurrentQueryCondition.order_reason_type_id));
        sb.Append("&ref_order_no=" + this.Server.UrlEncode(CurrentQueryCondition.ref_order_no));
        sb.Append("&data_from_id=" + this.Server.UrlEncode(CurrentQueryCondition.data_from_id));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
    }
    protected void gvAdjBill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var obj = (cPos.Model.InoutInfo)e.Row.DataItem;
            var flagmodel = new cBillService().GetBillActionByBillId(loggingSessionInfo, obj.order_id.ToString());
            var lbcheck = (LinkButton)e.Row.FindControl("check");
            var lbback = (LinkButton)e.Row.FindControl("back");
            var lbdelete = (LinkButton)e.Row.FindControl("delete");
            var lbmodify = (LinkButton)e.Row.FindControl("modify");
            if (flagmodel == null)
            {
                lbcheck.Visible = false;
                lbdelete.Visible = false;
                lbmodify.Visible = false;
                lbback.Visible = false;
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
 
            }
        }
    }
    protected void gvAdjBill_RowCommand(object sender, GridViewCommandEventArgs e)
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
                case "ajDelete":
                    {
                        if (serv.SetInoutOrderStatus(loggingSessionInfo, e.CommandArgument.ToString(), BillActionType.Cancel))
                        {
                            this.InfoBox.ShowPopInfo("删除成功！");
                        }
                        else
                        {
                            this.InfoBox.ShowPopError("删除失败！");
                        }
                    } break;
            }
            Query(0);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(selUnit.SelectedValue))
            initWarehouse();
    }
}