using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Admin.Service;

public partial class item_item_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = this.Request.QueryString["strDo"];
            ViewState["action"] = action;
            string item_id = this.Request.QueryString["item_id"]; 
            switch (action)
            {
                case "Visible":
                    {
                        LoadPriceType();
                        LoadItemType();
                        LoadSkuProp();

                        btnItemPriceAdd.Visible = false;
                        btnSkuAdd.Visible = false;
                        LoadItemInfoData();
                        this.btnSave.Visible = false;

                        DisableAllInput(this);
                    }
                    break;
                case "Create":
                    {
                        LoadPriceType();
                        LoadItemType();
                        LoadSkuProp();
                    } break;
                case "Modify":
                    {
                        LoadPriceType();
                        LoadItemType();
                        LoadItemInfoData();
                    }
                    break;
                default: this.btnSave.Visible = false; break;
            }
        }
        LoadSkuProp();
    }
    #region 服务
    protected cPos.Admin.Service.ItemService ItemService
    {
        get
        {
            return new cPos.Admin.Service.ItemService();
        }
    }
    protected cPos.Admin.Service.ItemCategoryService ItemCategoryService
    {
        get
        {
            return new cPos.Admin.Service.ItemCategoryService();  
        }
    }
    protected cPos.Admin.Service.ItemPropService ItemPropService
    {
        get
        {
            return new cPos.Admin.Service.ItemPropService();
        }
    }
    protected cPos.Admin.Service.ItemPriceTypeService ItemPriceTypeService
    {
        get
        {
            return new cPos.Admin.Service.ItemPriceTypeService();
        }
    }
    protected cPos.Admin.Service.SkuPropServer SkuPropService
    {
        get
        {
            return new cPos.Admin.Service.SkuPropServer();
        }
    }
    protected cPos.Admin.Service.PropService PropService
    {
        get
        {
            return new cPos.Admin.Service.PropService();
        }
    }
    #endregion

    //商品价格列表
    protected string ItemPriceData
    {
        get
        {
            if (ViewState["itemPrice"] == null)
                ViewState["itemPrice"] = "[]";
            return ViewState["itemPrice"] as string;
        }
        set
        {
            ViewState["itemPrice"] = value;
        }
    }
    //商品属性列表数据
    protected string ItemPropData
    {
        get
        {
            if (ViewState["itemProp"] == null)
                ViewState["itemProp"] = "[]";
            return ViewState["itemProp"] as string;
        }
        set
        {
            ViewState["itemProp"] = value;
        }
    }
    //商品Sku列表数据
    protected string ItemSkuData
    {
        get
        {
            if (ViewState["itemSku"] == null)
                ViewState["itemSku"] = "[]";
            return ViewState["itemSku"] as string;
        }
        set
        {
            ViewState["itemSku"] = value;
        }
    }
    protected IList<cPos.Model.SkuInfo> SkuList
    {
        get;
        set;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var itemInfo = GetNewItemInfo();
            string errorMsg = string.Empty;
            var rult = this.ItemService.SetItemInfo(loggingSessionInfo, itemInfo, out errorMsg);
            if (rult)
            {
                this.Redirect("保存成功", InfoType.Info, this.Request.QueryString["from"] ?? "item_query.aspx");
            }
            else
            {
                this.InfoBox.ShowPopError(errorMsg);
                return;
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("保存失败！");
        }
    }
    //加载基础信息
    private void LoadItemInfoData()
    {
        try
        {
            var source = this.ItemService.GetItemInfoById(loggingSessionInfo, this.Request.QueryString["item_id"]);
            this.tbItemCode.Text = source.Item_Code ?? "";
            this.tbItemName.Text = source.Item_Name ?? "";
            this.tbItemNameEn.Text = source.Item_Name_En ?? "";
            this.tbItemNameShort.Text = source.Item_Name_Short ?? "";
            this.tbPyzjm.Text = source.Pyzjm ?? "";
            this.selItemCategory.SelectedValue = source.Item_Category_Id;
            this.selItemCategory.SelectedText = source.Item_Category_Name;
            this.tbRemark.Text = source.Item_Remark ?? "";
            this.ifgifts.SelectedValue = source.ifgifts.ToString();
            this.ifoften.SelectedValue = source.ifoften.ToString();
            this.ifservice.SelectedValue = source.ifservice.ToString();
            var json = new System.Web.Script.Serialization.JavaScriptSerializer();
            
            //this.repTablePrice.DataSource = source.ItemPriceList;
            //this.repTablePrice.DataBind();
            this.ItemPriceData = json.Serialize(source.ItemPriceList);//price
            this.hidItemPrice.Value = json.Serialize(source.ItemPriceList);
            
            this.ItemPropData = json.Serialize(source.ItemPropList);//prop
            this.hidItemProp.Value = json.Serialize(source.ItemPropList);

            this.ItemSkuData = json.Serialize(source.SkuList);//sku
            this.hidItemSku.Value = json.Serialize(source.SkuList);
            this.SkuList = source.SkuList;
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //加载商品类型
    private void LoadItemType()
    {
        try
        {
            var origin_source = this.ItemCategoryService.GetItemCagegoryList(loggingSessionInfo);
            var source = origin_source.Where(obj => obj.Parent_Id == "-99");
            this.selItemCategory.DataBind<cPos.Model.ItemCategoryInfo>(source, 
                obj => origin_source.Where(o => o.Parent_Id == obj.Item_Category_Id),
                obj => new controls_DropDownTree.tvNode
                {
                    ShowCheck = false,
                    Complete = true,
                    Text = obj.Item_Category_Name,
                    Value = obj.Item_Category_Id
                });
            //this.selItemCategory.DataBind(source.Select(obj => new controls_DropDownTree.tvNode
            //{
            //    CheckState = false,
            //    Complete = origin_source.Where(o => o.Parent_Id == obj.Item_Category_Id).Count() > 0,
            //    ShowCheck = false,
            //    Text = obj.Item_Category_Name,
            //    Value = obj.Item_Category_Id
            //}));
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //加载价格类型
    private void LoadPriceType()
    {
        try
        {
            var source = this.ItemPriceTypeService.GetItemPriceTypeList(loggingSessionInfo);
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
    #region Sku属性加载列表
    protected IList<cPos.Model.SkuPropInfo> SkuProInfos
    {
        get;
        set;
    }
    //加载Sku属性列表
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
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    protected IList<cPos.Model.PropInfo> LoadPropDetails(string parent_Id)
    {
        try
        {
            return this.PropService.GetPropListByParentId(loggingSessionInfo, "ITEM", parent_Id);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
            return null;
        }
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
            else if (c is CheckBox)
            {
                var temp = c as CheckBox;
                temp.Enabled = false;
            }
            if (c.Controls.Count > 0)
                DisableAllInput(c); 
            selItemCategory.ReadOnly = true; 
        }
    }


    #region 将界面数据同步到Model对象

    //获取新建或更新后的数据
    private cPos.Model.ItemInfo GetNewItemInfo()
    {
        var itemInfo = new cPos.Model.ItemInfo();

        itemInfo.Item_Code = tbItemCode.Text;
        itemInfo.Item_Name = tbItemName.Text;
        itemInfo.Item_Name_En = tbItemNameEn.Text;
        itemInfo.Item_Name_Short = tbItemNameShort.Text;
        itemInfo.Pyzjm = tbPyzjm.Text;
        itemInfo.Item_Category_Id = this.selItemCategory.SelectedValue;
        itemInfo.ifgifts = int.Parse(this.ifgifts.SelectedValue);
        itemInfo.ifoften = int.Parse(this.ifoften.SelectedValue);
        itemInfo.ifservice = int.Parse(this.ifservice.SelectedValue);
        itemInfo.Item_Remark = this.tbRemark.Text;
        if (ViewState["action"].ToString() == "Create")//新建
        {
            itemInfo.Create_Time = new BaseService().GetCurrentDateTime();
            itemInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            itemInfo.Item_Id = null;
        }
        else                                           //修改
        {
            itemInfo.Modify_Time = new BaseService().GetCurrentDateTime();
            itemInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            itemInfo.Item_Id = this.Request.QueryString["item_id"];
        }
        itemInfo.ItemPriceList = this.GetItemPriceInfoFromView();
        itemInfo.ItemPropList = this.GetItemPropInfoFromView();
        itemInfo.SkuList = this.GetItemSkuInfoFromView();
        return itemInfo;
    }

    //从界面获取商品属性信息
    private IList<cPos.Model.ItemPropInfo> GetItemPropInfoFromView()
    {
        IList<cPos.Model.ItemPropInfo> itemPropInfos = new List<cPos.Model.ItemPropInfo>();
        var json = new System.Web.Script.Serialization.JavaScriptSerializer();
        object[] obj = (object[])json.DeserializeObject(this.hidItemProp.Value);
        for (int i = 0; i < obj.Length; i++)
        {
            var itemProp = new cPos.Model.ItemPropInfo();
            var dictionary = (Dictionary<string, object>)obj[i];
            foreach (var item in dictionary)
            {
                if (item.Key == "PropertyDetailId")
                {
                    itemProp.PropertyDetailId = item.Value == null ? null : item.Value.ToString();
                }
                else if (item.Key == "PropertyCodeId")
                {
                    itemProp.PropertyCodeId = item.Value == null ? null : item.Value.ToString();
                }
                else if (item.Key == "PropertyCodeValue")
                {
                    itemProp.PropertyCodeValue = item.Value == null ? null : item.Value.ToString();
                }
            }
            itemPropInfos.Add(itemProp);
        }
        if (itemPropInfos.Count == 0)
        {
            return null;
        }
        return itemPropInfos;
    }
    //从界面获取商品价格信息
    private IList<cPos.Model.ItemPriceInfo> GetItemPriceInfoFromView()
    {
        IList<cPos.Model.ItemPriceInfo> itemPriceInfos = new List<cPos.Model.ItemPriceInfo>();
        var json = new System.Web.Script.Serialization.JavaScriptSerializer();
        object[] obj = (object[])json.DeserializeObject(this.hidItemPrice.Value);
        for (int i = 0; i < obj.Length; i++)
        {
            var itemPrice = new cPos.Model.ItemPriceInfo();
            var dictionary = (Dictionary<string, object>)obj[i];
            foreach (var item in dictionary)
            {
                if (item.Key == "item_price_id")
                {
                    itemPrice.item_price_id = item.Value == null ? null : item.Value.ToString();
                }
                else if (item.Key == "item_price_type_id")
                {
                    itemPrice.item_price_type_id = item.Value == null ? null : item.Value.ToString();
                }
                else if (item.Key == "item_price")
                {
                    itemPrice.item_price = Convert.ToDecimal(item.Value ?? "0");
                }
            }
            itemPriceInfos.Add(itemPrice);
        }
        if (itemPriceInfos.Count == 0) {
            return null;
        }
            return itemPriceInfos;
    }
    //从界面获取商品Sku信息
    private IList<cPos.Model.SkuInfo> GetItemSkuInfoFromView()
    {
        IList<cPos.Model.SkuInfo> itemSkuInfos = new List<cPos.Model.SkuInfo>();
        var json = new System.Web.Script.Serialization.JavaScriptSerializer();
        object[] obj = (object[])json.DeserializeObject(this.hidItemSku.Value);
        for (int i = 0; i < obj.Length; i++)
        {
            var itemSku = new cPos.Model.SkuInfo();
            var dictionary = (Dictionary<string, object>)obj[i];
            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "sku_id": itemSku.sku_id = item.Value == null ? null : item.Value.ToString(); break;
                    case "prop_1_id": itemSku.prop_1_id = item.Value == null ? null : item.Value.ToString(); break;
                    case "prop_1_detail_id": itemSku.prop_1_detail_id = item.Value == null ? null : item.Value.ToString(); break;
                    case "prop_2_id": itemSku.prop_2_id = item.Value == null ? null : item.Value.ToString(); break;
                    case "prop_2_detail_id": itemSku.prop_2_detail_id = item.Value == null ? null : item.Value.ToString(); break;
                    case "prop_3_id": itemSku.prop_3_id = item.Value == null ? null : item.Value.ToString(); break;
                    case "prop_3_detail_id": itemSku.prop_3_detail_id = item.Value == null ? null : item.Value.ToString(); break;
                    case "prop_4_id": itemSku.prop_4_id = item.Value == null ? null : item.Value.ToString(); break;
                    case "prop_4_detail_id": itemSku.prop_4_detail_id = item.Value == null ? null : item.Value.ToString(); break;
                    case "prop_5_id": itemSku.prop_5_id = item.Value == null ? null : item.Value.ToString(); break;
                    case "prop_5_detail_id": itemSku.prop_5_detail_id = item.Value == null ? null : item.Value.ToString(); break;
                    case "barcode": itemSku.barcode = item.Value == null ? null : item.Value.ToString(); break;
                    default: break;
                }
            }
            itemSkuInfos.Add(itemSku);
        }
        if (itemSkuInfos.Count == 0)
        {
            return null;
        }
        return itemSkuInfos;
    }
    #endregion

    protected void btnCancle_Click(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request["from"] ?? "item_query.aspx");
    }
}
