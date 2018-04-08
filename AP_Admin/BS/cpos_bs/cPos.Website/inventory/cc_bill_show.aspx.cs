using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using System.Text;

public partial class inventory_cc_bill_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadSkuProp();
        if (!IsPostBack)
        {
            initUnitTree();
            var action = this.Request.QueryString["strDo"] ?? "Create";
            this.ViewState["action"] = action;
            switch (action)
            {
                case "Create": { initInvtoryNo(); }
                    break;
                case "Visible":
                    {
                        if (this.Request.Params["order_id"] != null)
                            ShowccBillData(0);
                        DisableAllInput(this);
                        //this.btnQuery.Visible = false;
                        //this.btnAdd.Visible = false;
                        this.btnSave.Visible = false;
                    } break;
                case "Modify":
                    {
                        this.btnQuery.Visible = false;
                        if (this.Request.Params["order_id"] != null)
                            ShowccBillData(0);
                    } break;
                default: return;

            }

        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        searchccDetailInfo();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        saveccbillData();
    }
    private void initInvtoryNo()
    {
        try
        {
            var bs = new cBillService();
            this.tbOrderNo.Text = bs.GetNo(loggingSessionInfo, "CC");
        }
        catch (Exception ex)
        {
            this.InfoBox.ShowPopError("访问数据库失败!");
            PageLog.Current.Write(ex);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        initWarehouse();
        this.CCDetailInfoList = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(saveItemDetailList());
    }
    //显示查看和修改页面
    private void ShowccBillData(int pageIndex)
    {
        string orderid = this.Request.Params["order_id"].ToString();
        var cclist = new CCService().GetCCInfoById(loggingSessionInfo, orderid, 99999
                ,0);
        this.tbOrderNo.Text = cclist.order_no;
        this.selUnit.SelectedValue = cclist.unit_id;
        this.selUnit.SelectedText = cclist.unit_name;
        initWarehouse();
        this.selWarehouse.SelectedValue = cclist.warehouse_id;
        this.selCompleteDate.Value = cclist.complete_date;
        this.selOrderDate.Value = cclist.order_date;
        this.tbRemark.Text = cclist.remark;
        this.CCDetailInfoList = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(cclist.CCDetailInfoList);
    }
    //查询商品明细（只有在新建页面的时候才可以用）
    private void searchccDetailInfo()
    {
       // string order_id = this.tbOrderNo.Text;
        string unit_id = this.selUnit.SelectedValue;
        string warehouse_id = this.selWarehouse.SelectedValue;
        CCDetailInfo detailInfo = new CCService().GetCCDetailListStockBalance(loggingSessionInfo, "", unit_id, warehouse_id, 1000
                , 0);
        IList<CCDetailInfo> itemDetailList = detailInfo.CCDetailInfoList;
        var currentData = saveItemDetailList();
        var afterFilter = currentData.Where(obj => !itemDetailList.Select(o => o.sku_id).Contains(obj.sku_id));
        var uionData = itemDetailList.Union(afterFilter);
        this.CCDetailInfoList = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(uionData);

    }
    //保存CCBill的数据
    private CCInfo currentccbillData()
    {
        CCInfo ccbill = new CCInfo();
        ccbill.order_no = this.tbOrderNo.Text;
        ccbill.unit_id = this.selUnit.SelectedValue;
        ccbill.warehouse_id = this.selWarehouse.SelectedValue??"";
        ccbill.complete_date = this.selCompleteDate.Value;
        ccbill.order_date = this.selOrderDate.Value;
        ccbill.remark = this.tbRemark.Text;
        ccbill.status = "1";
        //IList<CCDetailInfo> ccdetailList;//获取从添加商品页面回来的数据信息。
        if (ViewState["action"].ToString() == "Create")
        {
            ccbill.operate = "Create";
            ccbill.order_id = "";
            ccbill.status = "1";
            ccbill.create_time = new BaseService().GetCurrentDateTime();
            ccbill.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
        }
        else
        {
            ccbill.operate = "Modify";
            //ccbill.status = "";
            ccbill.order_id = this.Request.QueryString["order_id"];
            ccbill.modify_time = new BaseService().GetCurrentDateTime();
            ccbill.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
        }
        IList<CCDetailInfo> ccDetalList = saveItemDetailList();
        ccbill.CCDetailInfoList = ccDetalList;
        return ccbill;
    }

    //保存ItemDetailList数据。
    private IList<CCDetailInfo> saveItemDetailList()
    {
        IList<CCDetailInfo> detail = new List<CCDetailInfo>();
        var obj = (object[])new System.Web.Script.Serialization.JavaScriptSerializer().DeserializeObject(this.hidInout.Value);
        for (int i = 0; i < obj.Length; i++)
        {
            var dic = (Dictionary<string, object>)obj[i];
            var item = new CCDetailInfo();
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
                    case "end_qty": item.end_qty = Convert.ToDecimal(key.Value ?? "0"); break;
                    case "difference_qty": item.difference_qty = Convert.ToDecimal(key.Value ?? "0"); break;
                    case "warehouse_id": item.warehouse_id = (key.Value ?? "").ToString(); break;
                }
            }
            detail.Add(item);
        }
        return detail;
    }

    private void saveccbillData()
    {
        CCInfo ccInfo = currentccbillData();
        try
        {
            bool IsTrans = true;
            string strError = string.Empty;
            bool saved = new CCService().SetCCInfo(loggingSessionInfo, ccInfo, IsTrans, out strError);
            if (saved)
            {
                this.Redirect("保存成功", InfoType.Info, this.Request.QueryString["from"] ?? "cc_bill_query.aspx");               
            }
            else
            {
                this.InfoBox.ShowPopError(strError);
            }
            this.CCDetailInfoList = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ccInfo.CCDetailInfoList);
        }
        catch (Exception ex)
        {
            this.InfoBox.ShowPopError("访问数据库出错!");
            this.CCDetailInfoList = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ccInfo.CCDetailInfoList);
        }
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

    #region 属性
    //sku属性信息
    protected IList<SkuPropInfo> SkuPropInfos
    {
        get;
        set;
    }
    //InoutDetailInfo数据信息
    protected string CCDetailInfoList
    {
        get
        {
            if (ViewState["CCDetailInfo"] == null)
                ViewState["CCDetailInfo"] = "[]";
            return ViewState["CCDetailInfo"] as string;
        }
        set
        {
            ViewState["CCDetailInfo"] = value;
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

    public List<GoodItem> Goods
    {
        get
        {
            if (this.ViewState["Goods"] as List<GoodItem> == null)
            {
                this.ViewState["Goods"] = new List<GoodItem>();
            }
            return this.ViewState["Goods"] as List<GoodItem>;
        }
        set { this.ViewState["Goods"] = value; }
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
        public string salesPrice { get; set; }
        public string purchasePrice { get; set; }
        public string difference_qty { get; set; }
        public string end_qty { get; set; }
        public string order_qty { get; set; }
        public string sku_id { get; set; }
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
        this.selCompleteDate.Disabled = true;
        this.selUnit.ReadOnly = true;
    }

    //生成 _SkuID 参数
    //protected override void OnPreRender(EventArgs e)
    //{
    //    //生成 _from 隐藏域字段
    //    StringBuilder sb = new StringBuilder();
    //    int i = 0;
    //    foreach (GoodItem gooditem in Goods)
    //    {
    //        sb.Append(gooditem.sku_id.ToString());
    //        if (i < Goods.Count - 1)
    //        {
    //            sb.Append(",");
    //            i++;
    //        }
    //    }
    //    this.ClientScript.RegisterHiddenField("_SkuID", sb.ToString());
    //}

    public string cclist { get; set; }
}