using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using System.Collections;
using cPos.Model;
using cPos.Model.Pos;

public partial class terminal_terminal_unit_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if(this.Request.Params["pos_id"]!=null||this.Request.Params["unit_id"]!=null)
            {
                SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
                this.QueryCondition = getCondition();
                //if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
                //{
                    Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
                //}
            }
        }
        this.tbPosCode.Focus();

    }
    private Hashtable getCondition()
    {
        Hashtable ht = new Hashtable();
        if (this.Request.Params["pos_id"] != null)
            ht.Add("PosID", this.Request.Params["pos_id"].ToString());
        if(this.Request.Params["unit_id"]!=null)
            ht.Add("UnitID", this.Request.Params["unit_id"].ToString());
        if(!string .IsNullOrEmpty(tbPosCode.Text.Trim()))
            ht.Add("PosCode",tbPosCode.Text.Trim());
        if(!string.IsNullOrEmpty(tbPosSn.Text.Trim()))
            ht.Add("PosSN",tbPosSn.Text.Trim());
        if (!string.IsNullOrEmpty(tbUnitCode.Text.Trim()))
            ht.Add("UnitCode", tbUnitCode.Text.Trim());
        if (!string.IsNullOrEmpty(tbUnitName.Text.Trim()))
            ht.Add("UnitName", tbUnitName.Text.Trim());
        return ht;
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        btn.Enabled = false;
        this.QueryCondition = getCondition();
        Query(0);
        btn.Enabled = true;
    }

    //获取或设置当前查询条件
    private Hashtable QueryCondition
    {
        get
        {
            if (this.ViewState["QueryCondition"] as Hashtable == null)
            {
                this.ViewState["QueryCondition"] = getCondition();
            }
            return this.ViewState["QueryCondition"] as Hashtable;
        }
        set
        {
            this.ViewState["QueryCondition"] = value;
        }
    }

    private void Query(int pageIndex)
    {
        try
        {
            var service = new PosService();
            PosUnitInfo pUi = new PosUnitInfo(new UnitInfo(), new PosInfo());
            var querylist = service.SelectPosUnitList(loggingSessionInfo,QueryCondition);
            SplitPageControl1.RecoedCount = querylist.Count;
            SplitPageControl1.PageIndex = pageIndex;
            //验证查询当前页索引是否在记录总数范围内。
            if (SplitPageControl1.PageIndex != pageIndex)
            {
                Query(SplitPageControl1.PageIndex);
                return;
            }
            else
            {
                gvTerminal_Unit.DataSource = querylist;
                gvTerminal_Unit.DataBind();
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
    protected override void OnPreRender(EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?");
        if (this.gvTerminal_Unit.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["customer_id"]))
        {
            sb.Append("&customer_id=" + Server.UrlEncode(this.Request.QueryString["customer_id"]));
        }
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
}