using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;

public partial class inventory_stock_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUnitsInfo();
            LoadQueryByUrl();
            GetCondition();
            this.pager1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
        }
        LoadSkuProp();
    }
    #region 服务
    protected cPos.Service.StockBalanceService StockBalanceService
    {
        get
        {
            return new cPos.Service.StockBalanceService();
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
    protected cPos.Service.SkuPropServer SkuPropService
    {
        get
        {return new cPos.Service.SkuPropServer();
        }
    }
    #endregion
    #region 属性
    protected StockQueryCondition QueryCondition
    {
        get
        {
            if (ViewState["stock"] == null)
                ViewState["stock"] = new StockQueryCondition();
            return ViewState["stock"] as StockQueryCondition;
        }
        set
        {
            ViewState["stock"] = value;
        }
    }
    protected IList<SkuPropInfo> SkuPropInfos
    {
        get;
        set;
    }
    #endregion
    #region 事件
    protected void btnQueryClick(object sender, EventArgs e)
    {
        GetCondition();
        Query(0);
    }
    protected void pager1_RequireUpdate(object sender, EventArgs e)
    {
        Query(this.pager1.PageIndex);
    }
    //前台注册_from字段
    protected override void OnPreRender(EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (this.gvStock.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&unti_id=" + Server.UrlEncode(this.selUnit.SelectedValue));
        sb.Append("&warehouse_id=" + Server.UrlEncode(this.selWarehouse.SelectedValue));
        sb.Append("&item_code=" + Server.UrlEncode(this.tbItemCode.Value));
        sb.Append("&item_name=" + Server.UrlEncode(this.tbItemName.Value));
        sb.Append("&typeValue=" + Server.UrlEncode(this.selType.SelectedValue));
        sb.Append("&YearMonth=" + Server.UrlEncode(this.selDate.Value));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", Server.UrlEncode(this.pager1.PageIndex.ToString()), Server.UrlEncode(this.pager1.PageSize.ToString())));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
    //组织信息触发
    protected void hidwarehouseClick(object sender, EventArgs e)
    {
        try
        {
            LoadWareHouse();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    protected void btnResetClick(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.Path);
    }
    #endregion
    #region 加载组织信息
    //加载组织信息
    private void LoadUnitsInfo()
    {
        try
        {
            var service = this.UnitService;
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
    //加载仓库信息
    private void LoadWareHouse()
    {
        var source = this.PosService.GetWarehouseByUnitID(loggingSessionInfo, selUnit.SelectedValue);
        selWarehouse.DataValueField = "ID";
        selWarehouse.DataTextField = "Name";
        selWarehouse.DataSource = source;
        selWarehouse.DataBind();
    }
    //加载查询条件
    private void GetCondition()
    {
        var qc = new StockQueryCondition();
        if (!string.IsNullOrEmpty(this.selUnit.SelectedValue))
        {
            qc.unit_id = this.selUnit.SelectedValue;
            qc.unit_name = this.selUnit.SelectedText; 
        }
        if (!string.IsNullOrEmpty(this.selWarehouse.SelectedValue))
        {
            qc.warehouse_id = this.selWarehouse.SelectedValue;
            qc.warehouse_name = this.selWarehouse.Text; 
        }
        if (!string.IsNullOrEmpty(this.tbItemCode.Value))
        {
            qc.item_code = this.tbItemCode.Value;
        }
        if (!string.IsNullOrEmpty(this.tbItemName.Value))
        {
            qc.item_name = this.tbItemName.Value;
        }
            qc.typeValue = this.selType.SelectedValue;
        if (!string.IsNullOrEmpty(this.selDate.Value) && this.selType.SelectedIndex == 2)
        {
            qc.YearMonth = this.selDate.Value;
        }
        else
        {
            qc.YearMonth = null;
        }
        this.QueryCondition = qc;
    }
    //查询数据
    private void Query(int index)
    {
        try
        {
            var qc = this.QueryCondition;
            var rult = this.StockBalanceService.SearchStockBalance(loggingSessionInfo, qc.unit_id??"", qc.warehouse_id??"", qc.item_code??"", qc.item_name??"", qc.typeValue??"", qc.YearMonth??"", this.pager1.PageSize, this.pager1.PageIndex * this.pager1.PageSize);
            this.pager1.PageIndex = index;
            this.pager1.RecoedCount = rult.icount;
            this.gvStock.DataSource = rult.StockBalanceInfoList;
            this.gvStock.DataBind();
            if (this.pager1.PageIndex != index)
            {
                Query(this.pager1.PageIndex);
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //根据url加载查询条件
    private void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        this.selUnit.SelectedValue = qs["unit_id"] ?? "";
        this.selUnit.SelectedText = qs["unit_name"]??"";

        this.selWarehouse.SelectedValue = qs["warehouse_id"] ?? "";
        this.selWarehouse.SelectedValue = qs["warehouse_name"] ?? "";

        this.tbItemCode.Value = qs["item_code"] ?? "";
        this.tbItemName.Value = qs["item_name"] ?? "";
        this.selType.SelectedValue = qs["typeValue"] ?? "";
        this.selDate.Value = qs["YearMonth"] ?? "";
    }
    /// <summary>
    /// 查询条件
    /// </summary>
    [Serializable]
    protected class StockQueryCondition
    {
        public string unit_id { get; set; }
        public string unit_name { get; set; }

        public string warehouse_id { get; set; }
        public string warehouse_name { get; set; }

        public string item_code { get; set; }
        public string item_name { get; set; }
        public string typeValue { get; set; }
        public string YearMonth { get; set; }
    }
}