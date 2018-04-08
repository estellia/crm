using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using System.Text;
using System.Collections;
public partial class promotion_vip_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initcbType();
            cbStatus.SelectedValue = "";
            tbNo.Focus();
            SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            LoadQueryByUrl();
            this.CurrentQueryCondition = GetConditionFromUI();
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
        }
    }
    //生成服务的属性
    protected PromotionService service
    {
        get
        {
            return new PromotionService();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        SplitPageControl1.Visible = true;
        btnQuery.Enabled = false;
        this.CurrentQueryCondition = GetConditionFromUI();
        Query(0);
        btnQuery.Enabled = true;
    }
    //获取类型下拉列表内容
    protected void initcbType()
    {
        try
        {
            var birgesource = service.SelectVipTypeList();
            var source = birgesource.Select(obj => new { ID = obj.Code, Name = obj.Name }).ToList();
            source.Insert(0, new { ID = "0", Name = "全部" });
            cbType.DataTextField = "Name";
            cbType.DataValueField = "ID";
            cbType.DataSource = source;
            cbType.DataBind();
            cbType.SelectedValue = this.Request.QueryString["cbType"];
            if (cbType.SelectedIndex < 0 && cbType.Items.Count > 0)
            {
                cbType.SelectedIndex = 0;
            }
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
            Hashtable condition = new Hashtable();
            //var cond = new Hashtable();
            condition.Add("No", CurrentQueryCondition.No);
            condition.Add("Name", CurrentQueryCondition.Name);
            condition.Add("Type",CurrentQueryCondition.TypeName);
            condition.Add("Cell",CurrentQueryCondition.Cell);
            condition.Add("UnitName", CurrentQueryCondition.UnitName);
            condition.Add("Status", CurrentQueryCondition.StatusDescription);
            var servi = this.service;
            var querylist = servi.SelectVipList(condition
             , SplitPageControl1.PageSize
             , pageIndex * SplitPageControl1.PageSize
             );
            SplitPageControl1.RecoedCount = servi.SelectVipListCount(condition);
            SplitPageControl1.PageIndex = pageIndex;
            //验证查询当前页索引是否在记录总数范围内。
            if (SplitPageControl1.PageIndex != pageIndex)
            {
                Query(SplitPageControl1.PageIndex);
                return;
            }
            else
            {
                gvVip.DataSource = querylist;
                gvVip.DataBind();
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //分页控件 请求更新事件
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(SplitPageControl1.PageIndex);
    }
    // 从UI上获取条件对象
    private QueryCondition GetConditionFromUI()
    {
        QueryCondition rult = new QueryCondition();
        rult.No = tbNo.Text.Trim();
        rult.TypeName = cbType.SelectedValue;
        rult.Name = tbName.Text.Trim();
        rult.Cell = tbCell.Text.Trim();
        rult.UnitName = tbUnit.Text.Trim();
        rult.StatusDescription = cbStatus.SelectedValue;
        if(rult.No=="")
        {
            rult.No = null;
        }
        if (rult.TypeName=="0")
        {
            rult.TypeName = null;
        }
        if(rult.Name=="")
        {
           rult.Name=null;
        }
        if(rult.Cell=="")
        {
            rult.Cell=null;
        }
        if(rult.UnitName=="")
        {
            rult.UnitName=null;
        }
        if(rult.StatusDescription=="")
        {
            rult.StatusDescription=null;
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

    private void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        if (!string.IsNullOrEmpty(qs["Cell"]))
        {
            this.tbCell.Text = qs["Cell"];
        }
        if (!string.IsNullOrEmpty(qs["Name"]))
        {
            this.tbName.Text = qs["Name"];
        }
        if (!string.IsNullOrEmpty(qs["No"]))
        {
            this.tbNo.Text = qs["No"];
        }
        if (!string.IsNullOrEmpty(qs["ActivateUnitDisplayName"]))
        {
            this.tbUnit.Text = qs["ActivateUnitDisplayName"];
        }
        if (!string.IsNullOrEmpty(qs["StatusDescription"]))
        {
            this.cbStatus.SelectedValue = qs["StatusDescription"];
        }
        if (!string.IsNullOrEmpty(qs["TypeName"]))
        {
            this.cbType.SelectedValue = qs["TypeName"];
        }
    }
    //生成 From Url 参数
    protected override void OnPreRender(EventArgs e)
    {
        //生成 _from 隐藏域字段
        StringBuilder sb = new StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (this.gvVip.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&No=" + this.Server.UrlEncode(CurrentQueryCondition.No) ?? "");
        sb.Append("&TypeName=" + this.Server.UrlEncode(CurrentQueryCondition.TypeName) ?? "");
        sb.Append("&Name=" + this.Server.UrlEncode(CurrentQueryCondition.Name) ?? "");
        sb.Append("&Cell=" + this.Server.UrlEncode(CurrentQueryCondition.Cell) ?? "");
        sb.Append("&ActivateUnitDisplayName=" + this.Server.UrlEncode(CurrentQueryCondition.ActivateUnitDisplayName) ?? "");
        sb.Append("&StatusDescription=" + this.Server.UrlEncode(CurrentQueryCondition.StatusDescription) ?? "");
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
    [System.Serializable]
    public class QueryCondition
    {
        public string No { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string Cell { get; set; }
        public string UnitName { get; set; }
        public string ActivateUnitDisplayName { get; set; }
        public string StatusDescription { get; set; }
        //如果有其它条件可以在这里定义
    }
}