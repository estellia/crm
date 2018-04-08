using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;
using cPos.Service;

public partial class inventory_inout_bill_out_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadSkuProp();
        if (!IsPostBack)
        {
            var action = this.Request.QueryString["strDo"];
            ViewState["action"] = action;
            LoadUnitsInfo();
            loadReasonType();
            switch (action)
            {
                case "Visible":
                    {
                        this.btnSave.Visible = false;
                        DisableAllInput(this);
                        LoadInOutData();
                    } break;
                case "Create":
                    {
                        initInvtoryNo();
                    } break;
                case "Modify":
                    {
                        LoadInOutData();
                    } break;
                default: break;
            }
        }
    }
    #region 服务
    protected cPos.Service.SkuPropServer SkuPropService
    {
        get
        {
            return new cPos.Service.SkuPropServer();
        }
    }
    protected cPos.Service.CCService CCService
    {
        get
        {
            return new cPos.Service.CCService();
        }
    }
    protected cPos.Service.InoutService InoutService
    {
        get
        {
            return new cPos.Service.InoutService();
        }
    }
    protected cPos.Service.UnitService UnitService
    {
        get
        {
            return new cPos.Service.UnitService();
        }
    }
    protected cPos.Service.PosService PosService
    {
        get
        {
            return new cPos.Service.PosService();
        }
    }
    #endregion
    #region 属性
    //sku属性信息
    protected IList<SkuPropInfo> SkuPropInfos
    {
        get;
        set;
    }
    //商品明细标识
    protected string InoutDetail
    {
        get
        {
            if (ViewState["inout"] == null)
                ViewState["inout"] = "[]";
            return ViewState["inout"] as string;
        }
        set
        {
            ViewState["inout"] = value;
        }
    }
    #endregion
    #region 事件
    protected void btnSaveClick(object sender, EventArgs e)
    {
        var inoutinfo = GetDataFromUI();
        try
        {
            string errorMsg = string.Empty;
            var rult = this.InoutService.SetInoutInfo(loggingSessionInfo, inoutinfo, true, out errorMsg);
            if (rult)
            {
                this.Redirect("保存成功", InfoType.Info, this.Request.QueryString["from"] ?? "inout_bill_out_query.aspx");
            }
            else
            {
                this.InfoBox.ShowPopError(errorMsg);
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError(string.Format("出错了，详细内容：{0}", "系统错误，可能是出库单号已存在!"));
        }
        this.InoutDetail = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(inoutinfo.InoutDetailList);
    }
    protected void btnUnitClick(object sender, EventArgs e)
    {
        LoadWareHouse(this.selSalesUnit.SelectedValue);
        this.InoutDetail = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(SerializeFromUI());
        this.tbTotalAmount.Text = this.hidTotalAmount.Value;
    }
    #endregion
    #region 加载Sku属性信息
    //加载Sku属性信息列表
    private void LoadSkuProp()
    {
        try
        {
            var source = this.SkuPropService.GetSkuPropList(loggingSessionInfo);
            SkuPropInfos = source;
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    #endregion
    #region 加载组织信息
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
    #region 加载基础数据
    private void initInvtoryNo()
    {
        try
        {
            var bs = new cBillService();
            this.tbOrderNo.Text = bs.GetNo(loggingSessionInfo, "DO");
        }
        catch (Exception ex)
        {
            this.InfoBox.ShowPopError("访问数据库失败!");
            PageLog.Current.Write(ex);
        }
    }
    private void LoadInOutData()
    {
        try
        {
            var rult = this.InoutService.GetInoutInfoById(loggingSessionInfo, this.Request.QueryString["order_id"]);
            if (rult == null)
                return;
            this.tbOrderNo.Text = rult.order_no ?? "";
            this.selReasonType.SelectedValue = rult.order_reason_id ?? "";
            this.selSalesUnit.SelectedValue = rult.sales_unit_id;
            this.selSalesUnit.SelectedText = rult.sales_unit_name;
            this.selPurchaseUnit.SelectedValue = rult.purchase_unit_id;
            this.selPurchaseUnit.SelectedText = rult.purchase_unit_name;
            LoadWareHouse(rult.sales_unit_id);
            this.selWarehouse.SelectedValue = rult.warehouse_id;
            this.selOrderDate.Value = rult.order_date;
            this.selCompleteDate.Value = rult.complete_date;
            this.tbRefOrderNo.Text = rult.ref_order_no;
            this.tbTotalAmount.Text = rult.total_amount.ToString();
            this.hidTotalAmount.Value = rult.total_amount.ToString();
            this.tbDiscountRate.Text = rult.discount_rate.ToString();
            this.tbRemark.Text = rult.remark;
            var js = new System.Web.Script.Serialization.JavaScriptSerializer();
            this.InoutDetail = js.Serialize(rult.InoutDetailList);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //加载仓库信息
    private void LoadWareHouse(string value)
    {
        var source = this.PosService.GetWarehouseByUnitID(loggingSessionInfo, value);
        selWarehouse.DataValueField = "ID";
        selWarehouse.DataTextField = "Name";
        selWarehouse.DataSource = source;
        selWarehouse.DataBind();
    }
    //禁用所有input
    private void DisableAllInput(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c is TextBox)
            {
                var temp = c as TextBox;
                temp.ReadOnly = true;
            }
            else if (c is DropDownList)
            {
                var temp = c as DropDownList;
                temp.Enabled = false;
            }
            else if (c is CheckBox)
            {
                var temp = c as CheckBox;
                temp.Enabled = false;
            }
            if (c.Controls.Count > 0)
                DisableAllInput(c);
        }
        this.btnSave.Visible =false;
        this.selSalesUnit.ReadOnly = true;
        this.selPurchaseUnit.ReadOnly = true;
        this.selOrderDate.Disabled = true;
        this.selCompleteDate.Disabled = true;
    }
    //从界面获取数据
    private InoutInfo GetDataFromUI()
    {
        var inoutInfo = new InoutInfo();
        inoutInfo.order_no = tbOrderNo.Text;
        inoutInfo.order_reason_id = selReasonType.SelectedValue;
        inoutInfo.order_reason_name = selReasonType.SelectedItem.Text;
        inoutInfo.sales_unit_id = selSalesUnit.SelectedValue;
        inoutInfo.purchase_unit_id = selPurchaseUnit.SelectedValue;
        inoutInfo.warehouse_id = selWarehouse.SelectedValue;
        inoutInfo.order_date = selOrderDate.Value;
        inoutInfo.complete_date = selCompleteDate.Value;
        inoutInfo.ref_order_no = tbRefOrderNo.Text;
        decimal total = 0;
        Decimal.TryParse(this.hidTotalAmount.Value, out total);
        inoutInfo.total_amount = total;
        inoutInfo.remark = tbRemark.Text;
        decimal rate = 0;
        Decimal.TryParse(this.tbDiscountRate.Text, out rate);
        inoutInfo.discount_rate = rate;
        inoutInfo.order_type_id = "1F0A100C42484454BAEA211D4C14B80F";
        inoutInfo.BillKindCode = "DO";
        inoutInfo.data_from_id = "B8DF5D46D3CA430ABE21E20F8D71E212";
        inoutInfo.red_flag = "1";
        
        if (ViewState["action"].ToString() == "Create")
        {
            inoutInfo.operate = "Create";
            inoutInfo.order_id = "";
            inoutInfo.status = "1";//modify by lihao 2012-8-3
            inoutInfo.create_time = new cPos.Service.BaseService().GetCurrentDateTime();
            inoutInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
        }
        else
        {
            inoutInfo.operate = "Modify";
            inoutInfo.order_id = this.Request.QueryString["order_id"];
           // inoutInfo.status = "";//modify by lihao 2012-8-3
            inoutInfo.modify_time = new cPos.Service.BaseService().GetCurrentDateTime();
            inoutInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
        }
        inoutInfo.InoutDetailList = SerializeFromUI();
        return inoutInfo;
    }
    //将前台商品详细数据序列化
    private IList<InoutDetailInfo> SerializeFromUI()
    {
        IList<InoutDetailInfo> detail = new List<InoutDetailInfo>();
        var obj = (object[])new System.Web.Script.Serialization.JavaScriptSerializer().DeserializeObject(this.hidInout.Value);
        for (int i = 0; i < obj.Length; i++)
        {
            var dic = (Dictionary<string, object>)obj[i];
            var item = new InoutDetailInfo();
            foreach (var key in dic)
            {
                switch (key.Key)
                {
                    case "item_name": { item.item_name = (key.Value ?? "").ToString(); } break;
                    case "item_code": item.item_code = (key.Value ?? "").ToString(); break;
                    case "prop_1_detail_name": item.prop_1_detail_name = (key.Value ?? "").ToString(); break;
                    case "prop_2_detail_name": item.prop_2_detail_name = (key.Value ?? "").ToString(); break;
                    case "prop_3_detail_name": item.prop_3_detail_name = (key.Value ?? "").ToString(); break;
                    case "prop_4_detail_name": item.prop_4_detail_name = (key.Value ?? "").ToString(); break;
                    case "prop_5_detail_name": item.prop_5_detail_name = (key.Value ?? "").ToString(); break;
                    case "order_detail_id": item.order_detail_id = key.Value == null ? null : key.Value.ToString(); break;
                    case "sku_id": item.sku_id = key.Value == null ? null : key.Value.ToString(); break;
                    case "order_qty": item.order_qty = Convert.ToDecimal(key.Value ?? "0"); break;
                    case "enter_qty": item.enter_qty = Convert.ToDecimal(key.Value ?? "0"); break;
                    case "std_price": item.std_price = Convert.ToDecimal(key.Value ?? "0"); break;
                    case "retail_price": item.retail_price = Convert.ToDecimal(key.Value ?? "0"); break;
                    case "retail_amount": item.retail_amount = Convert.ToDecimal(key.Value ?? "0"); break;
                    case "discount_rate": item.discount_rate = Convert.ToDecimal(key.Value ?? "0"); break;
                    case "retail_rate": item.retail_price = Convert.ToDecimal(key.Value ?? "0"); break;
                    case "enter_price": item.enter_price = Convert.ToDecimal(key.Value ?? "0"); break;
                    case "order_discount_rate": item.order_discount_rate = Convert.ToDecimal(key.Value ?? "0"); break;
                }
            }
            detail.Add(item);
        }
        return detail;
    }
    //加载出库类型
    private void loadReasonType()
    {
        try
        {
            var ddlList = new cPos.Service.OrderReasonTypeService().GetOrderReasonTypeListByOrderTypeCode(loggingSessionInfo, "DO.ReasonType");
            //ddlList.Insert(0, new OrderReasonTypeInfo { reason_type_name = "---请选择---", reason_type_id = "-1" });
            this.selReasonType.DataSource = ddlList;
            selReasonType.DataTextField = "reason_type_name";
            selReasonType.DataValueField = "reason_type_id";
            selReasonType.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    #endregion
}