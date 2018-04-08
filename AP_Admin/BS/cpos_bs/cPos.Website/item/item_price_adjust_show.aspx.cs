using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model.User;
using cPos.Model;

public partial class item_item_price_adjust_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = this.Request.QueryString["strDo"];
            ViewState["strDo"] = action;
            LoadItemPriceType();
            LoadUnitsTree();
            LoadSkuProp();
            switch (action)
            {
                case "Visible": { this.btnSave.Visible = false; DisableAllInput(this); LoadBasicInfo(); } break;
                case "Create": { InitOrderNo(); } break;
                case "Modify": { this.tbOrderNo.Enabled = false; this.LoadBasicInfo(); } break;
                default: break;
            }
        }
    }
    #region 服务
    protected cPos.Service.UnitService UnitService
    {
        get
        {
            return new cPos.Service.UnitService();
        }
    }
    protected cPos.Service.AdjustmentOrderService AdjustmentOrderService
    {
        get
        {
            return new cPos.Service.AdjustmentOrderService();
        }
    }
    protected cPos.Service.ItemPriceTypeService ItemPriceTypeService
    {
        get
        {
            return new cPos.Service.ItemPriceTypeService();
        }
    }
    protected cPos.Service.SkuPropServer SkuPropService
    {
        get
        {
            return new cPos.Service.SkuPropServer();
        }
    }
    protected cPos.Service.PropService PropService
    {
        get
        {
            return new cPos.Service.PropService();
        }
    }
    #endregion

    #region 属性
    //SkuProInfo集合
    protected IList<cPos.Model.SkuPropInfo> SkuProInfos
    {
        get;
        set;
    }
    //AdjustmentOrderDetailSkuInfo集合
    protected IList<cPos.Model.AdjustmentOrderDetailSkuInfo> AdjustmentOrderDetailSkuInfos
    {
        get;
        set;
    }
    //调价单价格数据集合
    protected string AdjustPriceData
    {
        get
        {
            if (ViewState["adjustPriceData"] == null)
                ViewState["adjustPriceData"] = "[]";
            return ViewState["adjustPriceData"] as string;
        }
        set
        {
            ViewState["adjustPriceData"] = value;
        }
    }
    //调价单sku数据集合
    protected string AdjustSkuListData
    {
        get
        {
            if (ViewState["adjustSkuListData"] == null)
                ViewState["adjustSkuListData"] = "[]";
            return ViewState["adjustSkuListData"] as string;
        }
        set
        {
            ViewState["adjustSkuListData"] = value;
        }
    }
    #endregion
    #region 基本信息加载流程
    private void InitOrderNo()
    {
        try
        {
            var service = new cPos.Service.cBillService();
            this.tbOrderNo.Text = service.GetNo(loggingSessionInfo, "AJ");
        }
        catch (Exception ex)
        {
            this.InfoBox.ShowPopError("加载数据出错！");
            PageLog.Current.Write(ex.Message);
        }
    }
    //加载商品价格类型
    private void LoadItemPriceType()
    {
        try
        {
            var source = this.ItemPriceTypeService.GetItemPriceTypeList(loggingSessionInfo);
            source.Insert(0, new ItemPriceTypeInfo { item_price_type_id = "--", item_price_type_name = "---请选择---" });
            this.selItemPriceType.DataTextField = "item_price_type_name";
            this.selItemPriceType.DataValueField = "item_price_type_id";
            this.selItemPriceType.DataSource = source;
            this.selItemPriceType.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }
    
    //加载基本信息
    private void LoadBasicInfo()
    {
        try
        {
            var source = this.AdjustmentOrderService.GetAdjustmentOrderByOrderId(loggingSessionInfo, this.Request.QueryString["order_id"]);
            this.tbOrderNo.Text = source.order_no ?? "";
            this.tbOrderDate.Text = source.order_date ?? "";
            this.selItemPriceType.SelectedValue = source.item_price_type_id;
            this.selBeginDate.Text = source.begin_date ?? "";
            this.selEndDate.Text = source.end_date ?? "";
            this.tbRemark.Text = source.remark ?? "";
            LoadItemPrice(source.AdjustmentOrderDetailItemList);
            LoadSkuInfo(source.AdjustmentOrderDetailSkuList);
            ShowUnitCheckedNode(source.AdjustmentOrderDetailUnitList);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }

    
    #endregion

    #region 商品信息加载流程

    private void LoadItemPrice(IList<AdjustmentOrderDetailItemInfo> itemInfo)
    {
        try
        {
            this.AdjustPriceData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(itemInfo);
            this.hidItemPrice.Value = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(itemInfo);
            //this.repItemInfo.DataSource = itemInfo;
            //this.itemInfo.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }
    #endregion

    #region Sku信息加载流程

    //获取属性明细
    protected IList<cPos.Model.PropInfo> LoadPropDetails(string parentPropId)
    {
        try
        {
            return this.PropService.GetPropListByParentId(loggingSessionInfo, "ITEM", parentPropId);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
            return null;
        }
    }
    //加载Sku属性信息列表
    private void LoadSkuProp()
    {
        try
        {
            var source = this.SkuPropService.GetSkuPropList(loggingSessionInfo);
            SkuProInfos = source;
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }
    //加载商品sku信息列表
    private void LoadSkuInfo(IList<cPos.Model.AdjustmentOrderDetailSkuInfo> source)
    {
        var json = new System.Web.Script.Serialization.JavaScriptSerializer();
        this.AdjustSkuListData = json.Serialize(source);
        this.hidItemSku.Value = json.Serialize(source);
        this.AdjustmentOrderDetailSkuInfos = source;
    }

    #endregion

    #region 组织信息加载流程

    //加载组织信息
    private void LoadUnitsTree()
    {
        try
        {
            var keys = this.UnitService.GetRootUnitsByDefaultRelationMode(loggingSessionInfo);
            var root = keys.Where(obj => obj.Parent_Unit_Id == "-99").Select(obj => new TreeNode { Text = obj.Name, Value = obj.Id, ShowCheckBox = true, NavigateUrl = "#", Expanded = true });
            foreach (var item in root)
            {
                CreateChildrens(this.UnitService.GetSubUnitsByDefaultRelationMode(loggingSessionInfo, item.Value), item.Value).Select(obj => { item.ChildNodes.Add(obj); return 0; }).ToArray();
                this.unitTree.Nodes.Add(item);
                item.Expanded = true;
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }
    //加载子级组织信息
    private IEnumerable<TreeNode> CreateChildrens(IEnumerable<cPos.Model.UnitInfo> units, string parentId)
    {
        foreach (var item in units.Where(obj=>obj.Parent_Unit_Id == parentId))
        {
            TreeNode tv = new TreeNode
            {
                Text = item.Name,
                Value = item.Id,
                ShowCheckBox = true,
                Expanded = false,
                NavigateUrl="#"
            };
            var keys = this.UnitService.GetSubUnitsByDefaultRelationMode(loggingSessionInfo, item.Id).Select(obj => { obj.Parent_Unit_Id = item.Id; return obj; });
            CreateChildrens(keys, item.Id).Select(obj => { tv.ChildNodes.Add(obj); return 0; }).ToArray();
            yield return tv;
        }
    }
    //选中节点
    private void CheckedTreeNode(TreeNodeCollection treeNodes,string checkedId)
    {
        foreach (TreeNode node in treeNodes)
        {
            if (node.Value == checkedId)
            {
                node.Checked = true;
                var parentNode = node.Parent;
                if (parentNode is TreeNode) {
                    parentNode.Expanded = true;
                }
            }
            if (node.ChildNodes.Count != 0)
                CheckedTreeNode(node.ChildNodes, checkedId);
        }
    }
    private void ShowUnitCheckedNode(IList<AdjustmentOrderDetailUnitInfo> iList)
    {
        foreach (var item in iList)
        {
            CheckedTreeNode(this.unitTree.Nodes, item.unit_id);
        }
    }
    #endregion

    #region 事件

    protected void btnCancle_Click(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.QueryString["from"] ?? "item_price_adjust_query.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var orderInfo = GetNewAdjustOrderInfo();
            if (orderInfo.AdjustmentOrderDetailUnitList==null)
            {
                this.InfoBox.ShowPopError("必须选择组织单位");
                return;
            }
            var rult = this.AdjustmentOrderService.SetAdjustmentOrderInfo(loggingSessionInfo, orderInfo);
            if (rult == "保存成功!")
            {
                this.Redirect("保存成功", InfoType.Info, this.Request.QueryString["from"] ?? "item_price_adjust_query.aspx");
            }
            else
            {
                this.InfoBox.ShowPopError(rult);
                return;
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("保存失败！");
        }
    }

    #endregion

    #region 从界面获取数据
    private AdjustmentOrderInfo GetNewAdjustOrderInfo()
    {
        var adjustmentOrderInfo = new AdjustmentOrderInfo();
        adjustmentOrderInfo.order_date = tbOrderDate.Text;
        adjustmentOrderInfo.item_price_type_id = selItemPriceType.SelectedValue;
        adjustmentOrderInfo.begin_date = selBeginDate.Text;
        adjustmentOrderInfo.end_date = selEndDate.Text;
        adjustmentOrderInfo.remark = tbRemark.Text;
        adjustmentOrderInfo.order_no = tbOrderNo.Text;
        if (this.Request.QueryString["strDo"] == "Create")
        {
            adjustmentOrderInfo.create_time = new cPos.Service.BaseService().GetCurrentDateTime();
            adjustmentOrderInfo.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
            adjustmentOrderInfo.order_id = null;
        }
        else
        {
            adjustmentOrderInfo.order_id = this.Request.QueryString["order_id"];
            adjustmentOrderInfo.modify_time = new cPos.Service.BaseService().GetCurrentDateTime();
            adjustmentOrderInfo.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
        }
        adjustmentOrderInfo.AdjustmentOrderDetailItemList = GetNewPirceDataFromUI();
        adjustmentOrderInfo.AdjustmentOrderDetailSkuList = GetNwSkuDataFromUI();
        adjustmentOrderInfo.AdjustmentOrderDetailUnitList = GetNewUnitDataFromUI();
        return adjustmentOrderInfo;
    }
    //获取AdjustmentOrderDetailItemInfo数据
    private IList<cPos.Model.AdjustmentOrderDetailItemInfo> GetNewPirceDataFromUI()
    {
        IList<cPos.Model.AdjustmentOrderDetailItemInfo> priceInfos = new List<cPos.Model.AdjustmentOrderDetailItemInfo>();
        var hidPirceData = this.hidItemPrice.Value;
        object[] obj = (object[])new System.Web.Script.Serialization.JavaScriptSerializer().DeserializeObject(hidPirceData);
        if (obj == null)
            return priceInfos;
        for (int i = 0; i < obj.Length; i++)
        {
            var dictionary = (Dictionary<string, object>)obj[i];
            var priceInfo = new AdjustmentOrderDetailItemInfo();
            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "item_id": priceInfo.item_id = string.IsNullOrEmpty(item.Value.ToString()) ? null : item.Value.ToString(); break;
                    case "order_id": priceInfo.order_id = string.IsNullOrEmpty(item.Value.ToString()) ? null : item.Value.ToString(); break;
                    case "order_detail_item_id": priceInfo.order_detail_item_id = item.Value==null? null : item.Value.ToString(); break;
                    case "price": priceInfo.price = Convert.ToDecimal(item.Value ?? "0"); break;
                    default: break;
                }
            }
            priceInfos.Add(priceInfo);
        }
        if (priceInfos.Count == 0) {
            return null;
        }
        return priceInfos;
    }
    //获取AdjustmentOrderDetailSkuInfo数据
    private IList<AdjustmentOrderDetailSkuInfo> GetNwSkuDataFromUI()
    {
        IList<AdjustmentOrderDetailSkuInfo> skuInfos = new List<AdjustmentOrderDetailSkuInfo>();
        var hidSkuData = this.hidItemSku.Value;
        object[] obj = (object[])new System.Web.Script.Serialization.JavaScriptSerializer().DeserializeObject(hidSkuData);
        if (obj == null)
            return skuInfos;
        for (int i = 0; i < obj.Length; i++)
        {
            var dictionary = (Dictionary<string, object>)obj[i];
            var skuInfo = new AdjustmentOrderDetailSkuInfo();
            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "order_detail_sku_id": skuInfo.order_detail_sku_id = string.IsNullOrEmpty(item.Value.ToString()) ? null : item.Value.ToString(); break;
                    case "price": skuInfo.price = Convert.ToDecimal(item.Value ?? "0"); break;
                    case "sku_id": skuInfo.sku_id = string.IsNullOrEmpty(item.Value.ToString()) ? null : item.Value.ToString(); break;
                    default: break;
                }
            }
            skuInfos.Add(skuInfo);
        }
        if (skuInfos.Count == 0) {
            return null;
        }
        return skuInfos;
    }
    private IList<AdjustmentOrderDetailUnitInfo> GetNewUnitDataFromUI()
    {
        IList<AdjustmentOrderDetailUnitInfo> adjustmentOrderDetailUnitInfos = new List<AdjustmentOrderDetailUnitInfo>();
        if (this.unitTree.CheckedNodes==null||this.unitTree.CheckedNodes.Count==0)
        {
            return adjustmentOrderDetailUnitInfos;
        }
        foreach (TreeNode node in this.unitTree.CheckedNodes)
        {
            var adjustmentOrderDetailUnitInfo = new AdjustmentOrderDetailUnitInfo();
            adjustmentOrderDetailUnitInfo.order_id = this.Request.QueryString["order_id"];
            adjustmentOrderDetailUnitInfo.unit_id = node.Value;
            adjustmentOrderDetailUnitInfos.Add(adjustmentOrderDetailUnitInfo);
        }
        if (adjustmentOrderDetailUnitInfos.Count == 0) {
            return null;
        }
        return adjustmentOrderDetailUnitInfos;
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
                temp.Enabled = false;
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
            else if (c is TreeView)
            {
                var temp = c as TreeView;
                temp.Enabled = false;
            }
            if (c.Controls.Count > 0)
                DisableAllInput(c);
        }
    }
    
}