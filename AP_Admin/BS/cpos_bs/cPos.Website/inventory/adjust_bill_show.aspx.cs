using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;
using cPos.Service;
using cPos.Model.Pos;
using System.Text;

public partial class inventory_adjust_bill_show : PageBase
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadSkuProp();
        if (!IsPostBack)
        {
            LoadUnitsInfo();
            loadReasonType();
            if (this.Request.QueryString["strDo"] != null)
            {
                string action = this.Request.QueryString["strDo"].ToString();
                ViewState["action"] = action;
                switch (action)
                {
                    case "Visible"://查看
                        {
                            DisableAllInput(this);
                            if (this.Request.Params["order_id"] != null)
                                showInoutInfo(this.Request.Params["order_id"].ToString());
                            this.btnSave.Visible = false;
                        } break;
                    case "Create": { initInvtoryNo(); }//新建
                        break;
                    case "Modify"://修改
                        {
                            if (this.Request.Params["order_id"] != null)
                                showInoutInfo(this.Request.Params["order_id"].ToString());
                        } break;
                    default: return;
                }
            }
        }
        this.tbOrderNo.Focus();
    }
    #region 属性
        //sku属性信息
    protected IList<SkuPropInfo> SkuPropInfos
    {
        get;
        set;
    }
    //InoutDetailInfo数据信息
    protected string InoutDetailInfoList
    {
        get
        {
            if (ViewState["InoutDetailInfo"] == null)
                ViewState["InoutDetailInfo"] = "[]";
            return ViewState["InoutDetailInfo"] as string;
        }
        set
        {
            ViewState["InoutDetailInfo"] = value;
        }
    }
    #endregion

    #region 加载Sku属性信息
    //加载Sku属性信息列表
    private void LoadSkuProp()
    {
        try
        {
            var source = new cPos.Service.SkuPropServer().GetSkuPropList(loggingSessionInfo);
            SkuPropInfos = source;
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    #endregion
    private void initInvtoryNo()
    {
        try
        {
            var bs = new cBillService();
            this.tbOrderNo.Text = bs.GetNo(loggingSessionInfo, "AJ");
        }
        catch(Exception ex)
        {
            this.InfoBox.ShowPopError("访问数据库失败!");
            PageLog.Current.Write(ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        loadWarehouse();
        this.InoutDetailInfoList = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(saveItemDetailList());
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveccbillData();
    }
    //保存AJBill的数据
    private InoutInfo currentajbillData()
    {
        InoutInfo ajbill = new InoutInfo();
        ajbill.order_no = this.tbOrderNo.Text;
        ajbill.order_reason_id = this.selReasonType.SelectedValue;
        ajbill.unit_id = this.drpUnit.SelectedValue;
        ajbill.warehouse_id = this.selWarehouse.SelectedValue;
        //ajbill.complete_date = this.tbCompleteDate.Value;
        ajbill.ref_order_no = this.tbCompleteDate.Value;
        ajbill.order_date = this.selOrderDate.Value;
        ajbill.remark = this.tbRemark.Text;
        ajbill.red_flag = "1";
        if (ViewState["action"].ToString() == "Create")
        {
            ajbill.operate = "Create";
            ajbill.order_id = "";
            ajbill.status = "1";
            ajbill.create_time = new BaseService().GetCurrentDateTime();
            ajbill.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
        }
        else
        {
            ajbill.operate = "Modify";
           // ajbill.status = "";
            ajbill.order_id = this.Request.QueryString["order_id"];
            ajbill.modify_time = new BaseService().GetCurrentDateTime();
            ajbill.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
        }
        IList<InoutDetailInfo> inoutdetailList = saveItemDetailList();
        decimal total_qty=0;
        foreach (var item in inoutdetailList)
        {
            total_qty += item.enter_qty;
        }
        ajbill.total_qty = total_qty;
        ajbill.InoutDetailList = inoutdetailList;
        return ajbill;
    }
    //保存ItemDetailList数据。
    private IList<InoutDetailInfo> saveItemDetailList()
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
                }
            }
            detail.Add(item);
        }
        return detail;
    }
    //执行保存AjBill数据。
    private void saveccbillData()
    {
        var data = currentajbillData();
        try
        {
            bool IsTrans = true;
            string strError;
            bool saved = new AJService().SetAJInfo(loggingSessionInfo, IsTrans, data, out strError);
            if (saved)
            {
                this.Redirect("保存成功", InfoType.Info, this.Request.QueryString["from"] ?? "adjust_bill_query.aspx");
            }
            else
            {
                this.InfoBox.ShowPopError(strError);
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError(string.Format("出错了，详细内容：{0}", ""));
        }
        this.InoutDetailInfoList = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(data.InoutDetailList);
    }


    #region 加载单据类型数据loadReasonType()
    private void loadReasonType()
    {
        try
        {
            var ddlList = new OrderReasonTypeService().GetOrderReasonTypeListByOrderTypeCode(loggingSessionInfo, "AJ.ReasonType");
            selReasonType.DataSource = ddlList;
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
    #region 加载仓库数据 由单位树的值来确定仓库。loadWarehouse(string unitID)
    private void loadWarehouse()
    {
        try
        {
            var ddlList = new PosService().GetWarehouseByUnitID(loggingSessionInfo, drpUnit.SelectedValue);
            selWarehouse.DataSource = ddlList;
            selWarehouse.DataTextField = "Name";
            selWarehouse.DataValueField = "ID";
            selWarehouse.DataBind();
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
            this.drpUnit.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(item => new controls_DropDownTree.tvNode
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
    #region 获取订单信息 ,根据从查询页面获取的orderId来显示相关的数据。loadInoutInfo(string orderId)
    private void showInoutInfo(string orderId)
    {
        InoutInfo IOInfo = new InoutService().GetInoutInfoById(loggingSessionInfo, orderId);
        this.tbOrderNo.Text = IOInfo.order_no;
        this.selOrderDate.Value = (IOInfo.order_date ?? "").ToString();
        this.selReasonType.SelectedValue = IOInfo.order_reason_id;
        this.drpUnit.SelectedValue = IOInfo.unit_id;
        this.drpUnit.SelectedText = IOInfo.unit_name;
        loadWarehouse();
        this.selWarehouse.SelectedValue = IOInfo.warehouse_id;
        this.tbCompleteDate.Value = (IOInfo.ref_order_no??"").ToString();
        this.tbRemark.Text = IOInfo.remark;
        this.InoutDetailInfoList = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(IOInfo.InoutDetailList);
    }
    #endregion

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
            else if (c is controls_DropDownTree)
            {
                var temp = c as controls_DropDownTree;
                temp.ReadOnly = true;
            }
            else if (c is CheckBox)
            {
                var temp = c as CheckBox;
                temp.Enabled = false;
            }
            if (c.Controls.Count > 0)
                DisableAllInput(c);
        }
        this.selOrderDate.Disabled = true;
        this.tbCompleteDate.Disabled = true;
    }
    [Serializable]
    public class GoodItem
    {
        public string item_code { get; set; }
        public string item_name { get; set; }
        public string prop_1_detail_name { get; set; }
        public string prop_2_detail_name { get; set; }
        public string prop_3_detail_name { get; set; }
        public string prop_4_detail_name { get; set; }
        public string prop_5_detail_name { get; set; }
        public string enter_qty { get; set; }
        public string order_qty { get; set; }
        public string sku_id { get; set; }
    }
}