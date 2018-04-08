using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Admin.Service;
using cPos.Model;
using System.Text;

public partial class item_category_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            if (!string.IsNullOrEmpty(this.Request.QueryString["Code"]))
            {
                this.tbCode.Text = this.Request.QueryString["Code"].ToString();
            }
            if (!string.IsNullOrEmpty(this.Request.QueryString["Name"]))
            {
                this.tbName.Text = this.Request.QueryString["Name"].ToString();
            }
            if (!string.IsNullOrEmpty(this.Request.QueryString["Pyzj"]))
            {
                this.tbPYZJM.Text = this.Request.QueryString["Pyzj"].ToString();
            }
            if (!string.IsNullOrEmpty(this.Request.QueryString["statue"]))
            {
                this.tbStatus.Text = this.Request.QueryString["statue"].ToString();
            }
            this.CurrentQueryCondition = GetConditionFromUI();
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
            SplitPageControl1.Visible = true;
        }
    }
    //返回一个itemcategoryservice 实例
    private ItemCategoryService itemCategoryService
    {
        get
        {
            return new ItemCategoryService();
        }
    }
    protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "停用":
                {
                    string startRuselt = this.itemCategoryService.SetItemCategoryStatus(loggingSessionInfo, e.CommandArgument.ToString(), "1");
                    if (startRuselt == "状态修改成功.")
                    {
                        this.InfoBox.ShowPopInfo("启用成功！");
                    }
                    else
                    {
                        this.InfoBox.ShowPopError(startRuselt);
                    }
                    Query(SplitPageControl1.PageIndex);
                } break;
            case "正常":
                {
                    string stopRuselt = this.itemCategoryService.SetItemCategoryStatus(loggingSessionInfo, e.CommandArgument.ToString(), "-1");
                    if (stopRuselt == "状态修改成功.")
                    {
                        this.InfoBox.ShowPopInfo("停用成功！");
                    }
                    else
                    {
                        this.InfoBox.ShowPopError(stopRuselt);
                    }
                    Query(SplitPageControl1.PageIndex);
                } break;
            default: return;
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
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(SplitPageControl1.PageIndex);
    }
    protected void Query(int pageIndex)
    {
        try
        {
            var service = this.itemCategoryService;
            var querylist = service.SearchItemCategoryList(loggingSessionInfo,CurrentQueryCondition.Code,CurrentQueryCondition.Name,CurrentQueryCondition.PYZJM,CurrentQueryCondition.Status
             , SplitPageControl1.PageSize
             , pageIndex * SplitPageControl1.PageSize
             );
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
                gvCategory.DataSource = querylist.ItemCategoryInfoList;
                gvCategory.DataBind();
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
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

    protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var item = (ItemCategoryInfo)e.Row.DataItem;
            if (e.Row.FindControl("lbControl") == null)
                return;
            var button = (ImageButton)e.Row.FindControl("lbControl");
            if (item.Status == "-1")
            {
                button.ImageUrl = "~/img/enable.png";
            }
            else
            {
                button.ImageUrl = "~/img/disable.png";
            }
        }
    }
    //生成_from隐藏字段
    protected override void OnPreRender(EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?");
        if (this.gvCategory.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&Code="+this.Server.UrlEncode(tbCode.Text.Trim()));
        sb.Append("&Name="+this.Server.UrlEncode(tbName.Text.Trim()));
        sb.Append("&Pyzj="+this.Server.UrlEncode(tbPYZJM.Text.Trim()));
        sb.Append("&statue="+this.Server.UrlEncode(tbStatus.SelectedValue));
        sb.Append(string.Format("&pageSize={0}&pageIndex={1}", SplitPageControl1.PageSize, SplitPageControl1.PageIndex));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);

    }
    //用来保存当前页面的设置
   
    private QueryCondition GetConditionFromUI()
    {
        QueryCondition rult = new QueryCondition();
        rult.Code = tbCode.Text;
        rult.Name = tbName.Text;
        rult.PYZJM = tbPYZJM.Text;
        rult.Status = tbStatus.SelectedValue;
        return rult;
    }
    [System.Serializable]
    public class QueryCondition
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string PYZJM { get; set; }
        public string Status { get; set; }
    }
}
