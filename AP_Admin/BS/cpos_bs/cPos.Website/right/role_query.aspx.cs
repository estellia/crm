using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using cPos.Service;
using cPos.Model;

public partial class right_role_query : PageBase
{
    //页面加载下拉列表绑定，光标聚焦下拉列表，数据不显示
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initAppList();
            SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            this.CurrentQueryCondition = GetConditionFromUI();
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
            tbAppSys.Focus();
        }
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
    protected void imgDel_Click(object sender, EventArgs e)
    {

    }
    //初始化下拉列表
    protected void initAppList()
    {
        try
        {
            var BrigeDatasource = new cAppSysServices().GetAllAppSyses(loggingSessionInfo);
            var bindSource = BrigeDatasource.Select(obj => new { Def_App_Id = obj.Def_App_Id, Def_App_Name = obj.Def_App_Name }).ToList();
            bindSource.Insert(0, new { Def_App_Id = "0", Def_App_Name = "全部" });
            tbAppSys.DataSource = bindSource;
            tbAppSys.DataValueField = "Def_App_Id";
            tbAppSys.DataTextField = "Def_App_Name";
            var model = new AppSysModel();
            tbAppSys.DataBind();
            tbAppSys.SelectedValue = this.Request.QueryString["sppSys"];
            if (tbAppSys.SelectedIndex < 0 && tbAppSys.Items.Count > 0)
            {
                tbAppSys.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }

    protected void gv_role_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Operate-Delete":
                {
                    Row_DeleteItem(e.CommandArgument.ToString());
                }
                break;
        }
    }
    //数据行 删除处理逻辑
    private void Row_DeleteItem(string id)
    {
        try
        {
            //string DelRuselt = "删除" + id.ToString() + "成功";
            string DelRuselt = new cAppSysServices().DeleteRoleById(loggingSessionInfo, id);
            this.InfoBox.ShowPopInfo(DelRuselt);
            //总查询记录数减 1
            SplitPageControl1.RecoedCount -= 1;
            //刷新当前页
            Query(SplitPageControl1.PageIndex);
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
            var service = new cPos.Service.cAppSysServices();
            var querylist = service.GetRolesByAppSysId(loggingSessionInfo, CurrentQueryCondition.AppSys
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
                gv_role.DataSource = querylist.RoleInfoList;
                gv_role.DataBind();
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
        rult.AppSys = tbAppSys.SelectedValue;
        if (rult.AppSys == "0")
        {
            rult.AppSys = null;
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

    #region Condition 对象定义
    /// <summary>
    /// 查询条件对象
    /// </summary>
    [System.Serializable]
    public class QueryCondition
    {
        public string AppSys { get; set; }
        //如果有其它条件可以在这里定义
    }
    #endregion

    //生成 From Url 参数
    protected override void OnPreRender(EventArgs e)
    {
        //生成 _from 隐藏域字段
        StringBuilder sb = new StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=and");
        if(gv_role.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&sppSys=" + this.Server.UrlEncode(CurrentQueryCondition.AppSys));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
}