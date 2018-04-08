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

public partial class config_warehouse_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbWarehouseStatus.SelectedValue = "1";
            dvUnit.Focus();
            LoadUnitsInfo();
            SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            LoadQueryByUrl();
            this.CurrentQueryCondition = GetConditionFromUI();
            if (Convert.ToBoolean(this.Request.QueryString["q_flag"] ?? "false"))
            {
                Query(Convert.ToInt32(this.Request.QueryString["pageIndex"] ?? "0"));
            }
        }
    }
    private void LoadUnitsInfo()
    {
        try
        {
            var service = new UnitService();
            this.dvUnit.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(item => new controls_DropDownTree.tvNode
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
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }

    //生成类实例的属性
    protected PosService posservice
    {
        get { return new PosService(); }
    }
    protected void gvWarehourse_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void gvWarehourse_RowCreated(object sender, GridViewRowEventArgs e)
    {
    }
    protected void gvWarehourse_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "-1":
                {
                    bool startRuselt = this.posservice.EnableWarehouse(loggingSessionInfo, e.CommandArgument.ToString());
                    if (startRuselt)
                    {
                        this.InfoBox.ShowPopInfo("启用仓库成功");
                        gvWarehourse.DataBind();
                    }
                    else
                    {
                        this.InfoBox.ShowPopInfo("启用仓库失败");
                    }
                    Query(0);
                } break;
            case "1":
                {
                    bool stopRuselt = this.posservice.DisableWarehouse(loggingSessionInfo, e.CommandArgument.ToString());
                    if (stopRuselt)
                    {
                        this.InfoBox.ShowPopInfo("停用仓库成功");
                        gvWarehourse.DataBind();
                    }
                    else
                    {
                        this.InfoBox.ShowPopInfo("停用仓库失败");
                    }
                    Query(0);
                } break;
            default: return;
        }

    }
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
    //查询函数
    protected string getImgUrl(object obj)
    {
        var status = obj as string;
        if (status == "正常")
        {
            return "~/img/disable.png";
        }
        else
        {
            return "~/img/enable.png";
        }
    }
    private void Query(int pageIndex)
    {
        try
        {
            var condition = new Hashtable();
            if (this.CurrentQueryCondition.UnitID != null)
                condition.Add("UnitID", this.CurrentQueryCondition.UnitID);
            if (this.CurrentQueryCondition.Code != null)
                condition.Add("Code", this.CurrentQueryCondition.Code);
            if (this.CurrentQueryCondition.Name != null)

                condition.Add("Name", this.CurrentQueryCondition.Name);

            if (this.CurrentQueryCondition.Contacter != null)
                condition.Add("Contacter", this.CurrentQueryCondition.Contacter);

            if (this.CurrentQueryCondition.Tel != null)
                condition.Add("Tel", this.CurrentQueryCondition.Tel);

            if (this.CurrentQueryCondition.Status != null)
                condition.Add("Status", this.CurrentQueryCondition.Status);
            var service = this.posservice;
            var querylist = service.SearchWarehouseList(this.loggingSessionInfo, condition
                , SplitPageControl1.PageSize
                , pageIndex * SplitPageControl1.PageSize);
            //var querylist = service.SelectWarehouseList(condition
            //       , SplitPageControl1.PageSize
            //       , pageIndex * SplitPageControl1.PageSize);
            SplitPageControl1.RecoedCount = service.SelectWarehouseListCount(condition);
            SplitPageControl1.PageIndex = pageIndex;
            //验证查询当前页索引是否在记录总数范围内。
            if (SplitPageControl1.PageIndex != pageIndex)
            {
                Query(SplitPageControl1.PageIndex);
                return;
            }
            else
            {
                //gvWarehourse.DataSource = querylist;

                gvWarehourse.DataSource = querylist.Data;
                gvWarehourse.DataBind();
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(SplitPageControl1.PageIndex);
    }
    // 从UI上获取条件对象
    private QueryCondition GetConditionFromUI()
    {
        QueryCondition rult = new QueryCondition();
        if (!string.IsNullOrEmpty(tbWarehouseContacter.Text.Trim()))
            rult.Contacter = this.tbWarehouseContacter.Text.Trim();
        if (!string.IsNullOrEmpty(tbWarehouseCode.Text.Trim()))
            rult.Code = this.tbWarehouseCode.Text.Trim();
        if (!string.IsNullOrEmpty(tbWarehouseName.Text.Trim()))
            rult.Name = this.tbWarehouseName.Text.Trim();
        if (!string.IsNullOrEmpty(tbWarehouseTel.Text.Trim()))
            rult.Tel = this.tbWarehouseTel.Text.Trim();
        if (!string.IsNullOrEmpty(cbWarehouseStatus.SelectedValue))
            rult.Status = this.cbWarehouseStatus.SelectedValue;
        if (this.dvUnit.SelectedValue != null)
        {
            rult.UnitID = this.dvUnit.SelectedValue;
            rult.UnitName = this.dvUnit.SelectedText;
        }

        //if(rult.Status=="")
        //{
        //    rult.Status = null;
        //}
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
    #region Condition 对象定义
    /// <summary>
    /// 查询条件对象
    /// </summary>
    [System.Serializable]
    public class QueryCondition
    {
        public string UnitName { get; set; }
        public string UnitID { set; get; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Contacter { get; set; }
        public string Tel { get; set; }
        public string Status { get; set; }
        //如果有其它条件可以在这里定义
    }
    #endregion
    public string getImgToolTip(string status)
    {
        if (status == "正常")
        {
            return "停用";
        }
        else
        {
            return "启用";
        }

    }

    //生成 From Url 参数
    protected override void OnPreRender(EventArgs e)
    {
        //生成 _from 隐藏域字段
        StringBuilder sb = new StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&UnitID=" + this.Server.UrlEncode(CurrentQueryCondition.UnitID));
        sb.Append("&UnitName=" + this.Server.UrlEncode(CurrentQueryCondition.UnitName));
        sb.Append("&Code=" + this.Server.UrlEncode(CurrentQueryCondition.Code));
        sb.Append("&Name=" + this.Server.UrlEncode(CurrentQueryCondition.Name));
        sb.Append("&Contacter=" + this.Server.UrlEncode(CurrentQueryCondition.Contacter));
        sb.Append("&Tel=" + this.Server.UrlEncode(CurrentQueryCondition.Tel));
        sb.Append("&Status=" + this.Server.UrlEncode(CurrentQueryCondition.Status));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
        if (this.gvWarehourse.DataSource!=null)
        {
            sb.Append("&q_flag=" + Server.UrlEncode("true"));
        }
        
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
    }
    private void LoadQueryByUrl()
    {
        if (this.Request.QueryString["UnitID"] != null)
        {
            this.dvUnit.SelectedValue = this.Request.QueryString["UnitID"].ToString();
            this.dvUnit.SelectedText = this.Request.QueryString["UnitName"].ToString();
        }
        if (this.Request.QueryString["Code"] != null)
        {
            this.tbWarehouseCode.Text = this.Request.QueryString["Code"].ToString();
        }
        if (this.Request.QueryString["Name"] != null)
        {
            this.tbWarehouseName.Text = this.Request.QueryString["Name"].ToString();
        }
        if (this.Request.QueryString["Contacter"] != null)
        {
            this.tbWarehouseContacter.Text = this.Request.QueryString["Contacter"].ToString();
        }
        if (this.Request.QueryString["Tel"] != null)
        {
            this.tbWarehouseTel.Text = this.Request.QueryString["Tel"].ToString();
        }
        if (this.Request.QueryString["Status"] != null)
        {
            this.cbWarehouseStatus.SelectedValue = this.Request.QueryString["Status"].ToString();
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
        {
            Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
        }
    }
}
