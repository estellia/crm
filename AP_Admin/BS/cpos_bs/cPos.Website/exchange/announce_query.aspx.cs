using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using System.Collections;
using System.Text;


public partial class exchange_announce_query :PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUnitsInfo();
            SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            this.tbTitle.Text = this.Request.QueryString["Title"];
            this.tbEndDate.Value = this.Request.QueryString["EndDate"];
            this.tbBeginDate.Value = this.Request.QueryString["BeginDate"];
            this.cbAllowDownload.SelectedValue = this.Request.QueryString["AllowDownload"];
            if (!string.IsNullOrEmpty(this.Request.QueryString["UnitID"]))
            {
                var item = this.UnitService.GetUnitById(loggingSessionInfo, this.Request.QueryString["UnitID"]);
                if(item != null )
                {
                     tvUnit.SelectedValue = item.Id;
                     tvUnit.SelectedText = item.Name;
                }  
            }
            this.CurrentQueryCondition = GetConditionFromUI();
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
        }
        this.tbTitle.Focus();
    }

    //生成类实例的属性
    protected ExchangeService exchangeService
    {
        get { return new ExchangeService(); }
    }
    protected UnitService UnitService
    {
        get { return new UnitService(); }
    }
    //查询按钮
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        SplitPageControl1.Visible = true;
        btnQuery.Enabled = false;
        //设置当前查询条件
        this.CurrentQueryCondition = GetConditionFromUI();
        //查询第一页数据
        Query(0);
        btnQuery.Enabled = true;
    }



    private void LoadUnitsInfo()
    {
        try
        {
            var service = new UnitService();
            this.tvUnit.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(item => new controls_DropDownTree.tvNode
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

    protected void gvAnnounce_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Operate-Delete":
                {
                    Row_DeleteItem(e.CommandArgument.ToString());
                }
                break;
            case "Publish":
                {
                    bool pubRuselt = this.exchangeService.PublishAnnounce(loggingSessionInfo, e.CommandArgument.ToString());
                    if (pubRuselt)
                    {
                        this.InfoBox.ShowPopInfo("下发成功");
                    }
                    else
                    {
                        this.InfoBox.ShowPopInfo("下发失败");
                    }
                    Query(0);
                } break;
            case "StopPublish":
                {
                    bool pubRuselt = this.exchangeService.StopPublishAnnounce(loggingSessionInfo, e.CommandArgument.ToString());
                    if (pubRuselt)
                    {
                        this.InfoBox.ShowPopInfo("停止下发成功");
                    }
                    else
                    {
                        this.InfoBox.ShowPopInfo("停止下发失败");
                    }
                    Query(0);
                } break;
            default: break;
        }
    }
    //数据行 删除处理逻辑
    private void Row_DeleteItem(string id)
    {
        try
        {
           // string DelRuselt = "删除" + id.ToString() + "成功";
            bool DelRuselt = new ExchangeService().DeleteAnnounce(loggingSessionInfo, id);
            if (DelRuselt)
            {
                string DelRu = "删除成功";
                this.InfoBox.ShowPopInfo(DelRu);
                //刷新当前页
                Query(SplitPageControl1.PageIndex);
            }
            else {
                this.InfoBox.ShowPopInfo("删除失败");
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("删除失败！");
        }
    }
    // 从UI上获取条件对象
    private QueryCondition GetConditionFromUI()
    {
        QueryCondition rult = new QueryCondition();
        rult.Title = this.tbTitle.Text.Trim();
        rult.UnitID = (this.tvUnit.SelectedValue??"").Trim();
        rult.BeginDate = this.tbBeginDate.Value.Trim();
        rult.EndDate = this.tbEndDate.Value.Trim();
        if (!(this.cbAllowDownload.SelectedValue == "全部")) 
        {
            rult.AllowDownload = this.cbAllowDownload.SelectedValue;
        }
        return rult;
    }


    private void Query(int pageIndex)
    {
        try
        {
            var condition = new Hashtable();
            if (!string.IsNullOrEmpty(this.CurrentQueryCondition.Title)) 
            {
                condition.Add("Title", this.CurrentQueryCondition.Title);
            }
            if (!string.IsNullOrEmpty(this.CurrentQueryCondition.UnitID))
            {
                condition.Add("UnitID", this.CurrentQueryCondition.UnitID);
            }
            if (!string.IsNullOrEmpty(this.CurrentQueryCondition.BeginDate))
            {
                condition.Add("BeginDate", this.CurrentQueryCondition.BeginDate);
            }
            if (!string.IsNullOrEmpty(this.CurrentQueryCondition.EndDate))
            {
                condition.Add("EndDate", this.CurrentQueryCondition.EndDate);
            }
            if (!string.IsNullOrEmpty(this.CurrentQueryCondition.AllowDownload))
            {
                condition.Add("AllowDownload", this.CurrentQueryCondition.AllowDownload);
            }
            //condition.Add("Title", this.CurrentQueryCondition.Title);
            //condition.Add("UnitID", this.CurrentQueryCondition.UnitID);
            //condition.Add("BeginDate", this.CurrentQueryCondition.BeginDate);
            //condition.Add("EndDate", this.CurrentQueryCondition.EndDate);
            //condition.Add("AllowDownload", this.CurrentQueryCondition.AllowDownload);
            var service = this.exchangeService;
            var querylist = service.SelectAnnounceList(loggingSessionInfo, condition
                , SplitPageControl1.PageSize
                , pageIndex * SplitPageControl1.PageSize);
            SplitPageControl1.RecoedCount = querylist.DataCount;
            SplitPageControl1.PageIndex = pageIndex;
            //验证查询当前页索引是否在记录总数范围内。
            if (SplitPageControl1.PageIndex != pageIndex)
            {
                Query(SplitPageControl1.PageIndex);
                return;
            }
            else
            {
                gvAnnounce.DataSource = querylist.Data;
                gvAnnounce.DataBind();
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
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


    //分页控件 请求更新事件
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(SplitPageControl1.PageIndex);
    }

    //前台注册_from字段
    protected override void OnPreRender(EventArgs e)
    {
        System.Text.StringBuilder sb = new StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?");
        sb.Append("?and=");
        if (this.gvAnnounce.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&Title=" + this.Server.UrlEncode(CurrentQueryCondition.Title));
        sb.Append("&UnitID=" + this.Server.UrlEncode(CurrentQueryCondition.UnitID));
        sb.Append("&BeginDate=" + this.Server.UrlEncode(CurrentQueryCondition.BeginDate));
        sb.Append("&EndDate=" + this.Server.UrlEncode(CurrentQueryCondition.EndDate));
        sb.Append("&AllowDownload=" + this.Server.UrlEncode(CurrentQueryCondition.AllowDownload));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
    }

    #region Condition 对象定义
    /// <summary>
    /// 查询条件对象
    /// </summary>
    [System.Serializable]
    public class QueryCondition
    {
        public string Title { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string UnitID { get; set; }
        public string AllowDownload { get; set; }
        //如果有其它条件可以在这里定义
    }
    #endregion
}