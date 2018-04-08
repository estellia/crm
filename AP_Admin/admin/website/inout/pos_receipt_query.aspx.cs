using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;
using System.Text;
using cPos.Admin.Service;

public partial class inout_pos_receipt_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //LoadUnitsInfo();
            LoadQueryByUrl();

            this.SplitPageControl1.PageSize = Convert.ToInt32(this.Request.QueryString["pageSize"] ?? "10");
           this.Codition =  GetConditonFormUI();
           if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
           {
               Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
           }
        }
    }
    protected cPos.Admin.Service.UnitService UnitService
    {
        get
        {
            return new cPos.Admin.Service.UnitService();
        }
    }
    protected cPos.Admin.Service.PosInoutService PosInoutService
    {
        get
        {
            return new cPos.Admin.Service.PosInoutService();
        }
    }
    protected PosQueryConditon Codition
    {
        get
        {
            if (ViewState["posQuery"] == null)
                ViewState["posQuery"] =new PosQueryConditon();
           return ViewState["posQuery"] as PosQueryConditon;
        }
        set
        {
            ViewState["posQuery"] = value;
        }
    }
    private PosQueryConditon GetConditonFormUI()
    {
        var condition = new PosQueryConditon();
        if (!string.IsNullOrEmpty(tbItem.Text))
        {
            condition.item_name = this.tbItem.Text;
        }
        if (!string.IsNullOrEmpty(tbOrderNo.Text))
        {
            condition.order_no = tbOrderNo.Text;
        }
        if (!string.IsNullOrEmpty(selDateStart.Value))
        {
            condition.order_date_begin = selDateStart.Value;
        }
        if (!string.IsNullOrEmpty(selDateEnd.Value))
        {
            condition.order_date_end = selDateEnd.Value;
        }
        if (this.tbUnitCode.Text.Trim() != "")
        {
            condition.unit_code = this.tbUnitCode.Text.Trim();
        }
        return condition;
    }
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(this.SplitPageControl1.PageIndex);
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
       this.Codition= this.GetConditonFormUI();
        Query(0);
    }
    protected void btnResetClick(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.RawUrl);
    }
    //查询
    private void Query(int index)
    {
        try
        {
            var qc = this.Codition;
            var source = this.PosInoutService.SearchPosInfo(loggingSessionInfo, 
                qc.order_no, qc.unit_code, qc.item_name, qc.order_date_begin, qc.order_date_end, 
                this.SplitPageControl1.PageSize, this.SplitPageControl1.PageSize * index);
            this.SplitPageControl1.RecoedCount = source.ICount;
            this.gvPos.DataSource = source.InoutInfoList;
            this.gvPos.DataBind();
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
    //加载组织信息
    //private void LoadUnitsInfo()
    //{
    //    try
    //    {
    //        var service = new UnitService();
    //        this.selUnit.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(item => new controls_DropDownTree.tvNode
    //        {
    //            CheckState = false,
    //            Complete = false,
    //            ShowCheck = false,
    //            Text = item.Name,
    //            Value = item.Id,
    //        }));
    //    }
    //    catch (Exception ex)
    //    {
    //        PageLog.Current.Write(ex);
    //        this.InfoBox.ShowPopError("加载数据出错！");
    //    }
    //}

    private IEnumerable<UnitInfo> GetAllUnitInfo()
    {
        var keys = this.UnitService.GetRootUnitsByDefaultRelationMode(loggingSessionInfo);
        foreach (var item in keys)
        {
            yield return item;
            foreach (var sub in GetSubUnits(item))
            {
                sub.Parent_Unit_Id = item.Id;
                yield return sub;
            }
        }
    }


    //获取子级组织信息
    private IEnumerable<UnitInfo> GetSubUnits(UnitInfo unit_info)
    {

        foreach (var item in this.UnitService.GetSubUnitsByDefaultRelationMode(loggingSessionInfo, unit_info.Id))
        {
            item.Parent_Unit_Id = unit_info.Id;
            yield return item;
            foreach (var sub in GetSubUnits(item))
            {
                sub.Parent_Unit_Id = item.Id;
                yield return sub;
            }
        }
    }

    //前台注册_from字段
    protected override void OnPreRender(EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?");
        if (this.gvPos.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&order_no=" + Server.UrlEncode(this.tbOrderNo.Text));
        sb.Append("&item_name=" + Server.UrlEncode(this.tbItem.Text));
        sb.Append("&unit_code=" + Server.UrlEncode(this.tbUnitCode.Text.Trim()));
        sb.Append("&order_date_begin=" + Server.UrlEncode(this.selDateStart.Value));
        sb.Append("&order_date_end=" + Server.UrlEncode(this.selDateEnd.Value));
        sb.Append("&page_index=" + Server.UrlEncode(this.SplitPageControl1.PageIndex.ToString()));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
    //根据url加载查询条件
    private void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        if (!string.IsNullOrEmpty(qs["item_name"]))
        {
            this.tbItem.Text = qs["item_name"];
        }
        if (!string.IsNullOrEmpty(qs["order_no"]))
        {
            this.tbOrderNo.Text = qs["order_no"];
        }
        if (!string.IsNullOrEmpty(qs["unit_code"]))
        {
            this.tbUnitCode.Text = qs["unit_code"];
        }
        if (!string.IsNullOrEmpty(qs["order_date_begin"]))
        {
            this.selDateStart.Value= qs["order_date_begin"];
        }
        if (!string.IsNullOrEmpty(qs["order_date_end"]))
        {
            this.selDateEnd.Value = qs["order_date_end"];
        }
    }
    //查询条件
    [Serializable]
    protected class PosQueryConditon
    {
        public string order_no { get; set; }
        public string unit_id { get; set; }
        public string unit_code { get; set; }
        public string item_name { get; set; }
        public string order_date_begin { get; set; }
        public string order_date_end { get; set; }
    }
}