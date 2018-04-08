using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model.Pos;
using System.Collections;

public partial class terminal_terminal_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.QueryCondition = GetHashtableQueryString();
            //var pos_id = this.Request.QueryString["pos_id"];
            LoadTerminalType();

            #region //从"show"页面返回时给控件赋原来的值。
            if(this.QueryCondition["Code"]!=null)
                tbCode.Text = this.QueryCondition["Code"].ToString();
            if (this.QueryCondition["insuraceDateBegin"] != null)
                this.tbInsuraceDateBegin.Value = this.QueryCondition["insuraceDateBegin"].ToString();
            if (this.QueryCondition["insuraceDateEnd"] != null)
                this.tbInsuraceDateEnd.Value = this.QueryCondition["insuraceDateEnd"].ToString();
            if (this.QueryCondition["purchaseDateBegin"] != null)
                this.tbPurchaseDateBegin.Value = this.QueryCondition["purchaseDateBegin"].ToString();
            if (this.QueryCondition["purchaseDateEnd"] != null)
                this.tbPurchaseDateEnd.Value = this.QueryCondition["purchaseDateEnd"].ToString();
            if (this.QueryCondition["sn"] != null)
                this.tbSn.Text = this.QueryCondition["sn"].ToString();
            if (this.QueryCondition["holdType"] != null)
            {
                this.cbHoldType.SelectedValue = this.QueryCondition["holdType"].ToString();
            }
            if (this.QueryCondition["type"] != null)
            {
                this.cbType.SelectedValue = this.QueryCondition["type"].ToString();
            }

            #endregion

            SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            this.QueryCondition = getCodition();
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
        }
        this.cbHoldType.Focus();
    }

    Hashtable GetHashtableQueryString()
    {
        Hashtable ht = new Hashtable();
        var hc = this.Request.QueryString;
        ht.Add("Code",hc["Code"]);
        ht.Add("insuraceDateBegin", hc["insuraceDateBegin"]);
        ht.Add("insuraceDateEnd", hc["insuraceDateEnd"]);
        ht.Add("purchaseDateBegin", hc["purchaseDateBegin"]);
        ht.Add("purchaseDateEnd", hc["purchaseDateEnd"]);
        ht.Add("sn", hc["sn"]);
        ht.Add("holdType", hc["holdType"]);
        ht.Add("type", hc["type"]);
        return ht;
    }
    protected Hashtable getCodition()
    {
        Hashtable ht = new Hashtable();
        //if (!string.IsNullOrEmpty(this.Request.QueryString["customer_id"]))
        //{
        //    ht.Add("CustomerID", this.Request.QueryString["customer_id"]);
        //}
        if (!string.IsNullOrEmpty(this.tbCode.Text))
        {
            ht.Add("Code", this.tbCode.Text);
        }
        if (!string.IsNullOrEmpty(this.tbInsuraceDateBegin.Value))
        {
            string s = this.tbInsuraceDateBegin.Value;
            ht.Add("InsuranceDateBegin",s);
        }
        if (!string.IsNullOrEmpty(this.tbInsuraceDateEnd.Value))
        {
            string s = this.tbInsuraceDateEnd.Value;
            ht.Add("InsuranceDateEnd", s);
        }
        if (!string.IsNullOrEmpty(this.tbPurchaseDateBegin.Value))
        {
            string s = this.tbPurchaseDateBegin.Value;
            ht.Add("PurchaseDateBegin", s);
        }
        if (!string.IsNullOrEmpty(this.tbPurchaseDateEnd.Value))
        {
            string s=this.tbPurchaseDateEnd.Value;
            ht.Add("PurchaseDateEnd",s);
        }
        if (!string.IsNullOrEmpty(this.tbSn.Text))
        {
            ht.Add("SN", this.tbSn.Text);
        }
        if (cbHoldType.SelectedIndex > 0)
        {
            ht.Add("HoldType", cbHoldType.SelectedValue);
        }
        if (cbType.SelectedIndex > 0)
        {
            ht.Add("Type", cbType.SelectedValue);
        }
        return ht;
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.QueryCondition = getCodition();
        Query(0);
    }

    //获取或设置当前查询条件
    private Hashtable QueryCondition
    {
        get
        {
            if (this.ViewState["QueryCondition"] as Hashtable == null)
            {
                this.ViewState["QueryCondition"] = getCodition();
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
            var querylist = service.SelectPosList(QueryCondition.Clone() as Hashtable
                , SplitPageControl1.PageSize
                , pageIndex * SplitPageControl1.PageSize);
            SplitPageControl1.RecoedCount = service.SelectPosListCount(QueryCondition.Clone() as Hashtable);
            SplitPageControl1.PageIndex = pageIndex;
            if (SplitPageControl1.PageIndex != pageIndex)
            {
                Query(SplitPageControl1.PageIndex);
                return;
            }
            else
            {
                gvTerminal.DataSource = querylist;
                gvTerminal.DataBind();
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


    //加载终端信息
    private void LoadTerminalType()
    {
        try
        {
            this.cbType.DataTextField = "Name";
            this.cbType.DataValueField = "Code";
            this.cbType.DataSource = (new PosService()).SelectPostTypeList();
            ListItem lis=new ListItem();
            lis.Text="全部";
            lis.Value="";
            this.cbType.DataBind();
            this.cbType.Items.Insert(0, lis);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    

    //生成From Url隐藏字段
    protected override void OnPreRender(EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (this.gvTerminal.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        //if (!string.IsNullOrEmpty(this.Request.QueryString["customer_id"]))
        //{
        //    sb.Append("&customer_id=" + Server.UrlEncode(this.Request.QueryString["customer_id"]));
        //}
        sb.Append("&Code=" + Server.UrlEncode(tbCode.Text));
        sb.Append("&insuraceDateBegin=" + Server.UrlEncode(tbInsuraceDateBegin.Value));
        sb.Append("&insuraceDateEnd=" + Server.UrlEncode(tbInsuraceDateEnd.Value));
        sb.Append("&purchaseDateBegin=" + Server.UrlEncode(tbPurchaseDateBegin.Value));
        sb.Append("&purchaseDateEnd=" + Server.UrlEncode(tbPurchaseDateEnd.Value));
        sb.Append("&sn=" + Server.UrlEncode(tbSn.Text));
        sb.Append("&holdType=" + Server.UrlEncode(cbHoldType.SelectedIndex > 0 ? cbHoldType.SelectedValue : "0"));
        sb.Append("&type=" + Server.UrlEncode(cbType.SelectedIndex > 0 ? cbType.SelectedValue : "0"));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        //base.OnPreRender(e);
    }
}