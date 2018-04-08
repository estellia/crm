using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using cPos.Admin.Service;
using cPos.Admin.Model.Base;
using cPos.Admin.Model.Dex;

public partial class log_log_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadAppInfo();
            LoadLogTypeInfo();
            SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            this.CurrentQueryCondition = GetConditionFromUI();
            GetConditionByUrl();
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
        }
        this.tbIfCode.Focus();

    }
    //加载平台列表
    public void LoadAppInfo()
    {
        try
        {
            var ret = this.GetDexLogService().GetAppList();
            string info = "全部";
            ret.Insert(0,info);
            this.cbAppType.DataSource = ret;
            this.cbAppType.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }

    //加载日志类型列表
    public void LoadLogTypeInfo()
    {
        try
        {
            var ret = this.GetDexLogService().GetLogTypes();
            string info = "全部";
            ret.Insert(0, info);
            this.cbLogType.DataSource = ret;
            this.cbLogType.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    private void GetConditionByUrl()
    {
        var qs = this.Request.QueryString;
        if (!string.IsNullOrEmpty(qs["app_code"]))
            this.cbAppType.SelectedValue = qs["app_code"];
        if (!string.IsNullOrEmpty(qs["log_type_code"]))
            this.cbLogType.SelectedValue = qs["log_type_code"];

        if (!string.IsNullOrEmpty(qs["if_code"]))
            this.tbIfCode.Text = qs["if_code"];

        if (!string.IsNullOrEmpty(qs["log_code"]))
            this.tbLogCode.Text = qs["log_code"];
        if (!string.IsNullOrEmpty(qs["log_body"]))
            this.tbLogBody.Text = qs["log_body"];

        if (!string.IsNullOrEmpty(qs["biz_id"]))
            this.tbBizId.Text = qs["biz_id"];
        if (!string.IsNullOrEmpty(qs["biz_name"]))
            this.tbBizCode.Text = qs["biz_name"];

        if (!string.IsNullOrEmpty(qs["create_time_begin"]))
            this.tbCreateTimeBegin.Value = qs["create_time_begin"];
        if (!string.IsNullOrEmpty(qs["create_time_end"]))
            this.tbCreateTimeEnd.Value = qs["create_time_end"];
        if (!string.IsNullOrEmpty(qs["modify_time_begin"]))
            this.tbModifyTimeBegin.Value = qs["modify_time_begin"];
        if (!string.IsNullOrEmpty(qs["modify_time_end"]))
            this.tbModifyTimeEnd.Value = qs["modify_time_end"];

        if (!string.IsNullOrEmpty(qs["create_user_id"]))
            this.tbCreateUserId.Text = qs["create_user_id"];
        if (!string.IsNullOrEmpty(qs["modify_user_id"]))
            this.tbModifyUserId.Text = qs["modify_user_id"];

        if (!string.IsNullOrEmpty(qs["customer_code"]))
            this.tbCustomerCode.Text = qs["customer_code"];
        if (!string.IsNullOrEmpty(qs["customer_id"]))
            this.tbCustomerId.Text = qs["customer_id"];
        if (!string.IsNullOrEmpty(qs["log_id"]))
            this.tbLogId.Text = qs["log_id"];

        if (!string.IsNullOrEmpty(qs["unit_code"]))
            this.tbUnitCode.Text = qs["unit_code"];
        if (!string.IsNullOrEmpty(qs["unit_id"]))
            this.tbUnitId.Text = qs["unit_id"];

        if (!string.IsNullOrEmpty(qs["user_code"]))
            this.tbUserCode.Text = qs["user_code"];
        if (!string.IsNullOrEmpty(qs["user_id"]))
            this.tbUserId.Text = qs["user_id"];
    }
    //获取当前查询条件
    public QueryCondition GetConditionFromUI()
    {
        QueryCondition rult = new QueryCondition();
        if (cbAppType.SelectedIndex>0)
            rult.app_code = this.cbAppType.SelectedValue;
        if (this.cbLogType.SelectedIndex > 0)
            rult.log_type_code = this.cbLogType.SelectedValue;

        if (!string.IsNullOrEmpty(tbIfCode.Text.Trim()))
            rult.if_code = this.tbIfCode.Text.Trim();

        if (!string.IsNullOrEmpty(tbLogCode.Text.Trim()))
            rult.log_code= this.tbLogCode.Text.Trim();
        if (!string.IsNullOrEmpty(tbLogBody.Text.Trim()))
            rult.log_body = this.tbLogBody.Text.Trim();

        if (!string.IsNullOrEmpty(tbBizId.Text.Trim()))
            rult.biz_id = this.tbBizId.Text.Trim();
        if (!string.IsNullOrEmpty(tbBizCode.Text.Trim()))
            rult.biz_name = this.tbBizCode.Text.Trim();

        if (!string.IsNullOrEmpty(tbCreateTimeBegin.Value))
            rult.create_time_begin =this.tbCreateTimeBegin.Value;
        if (!string.IsNullOrEmpty(tbCreateTimeEnd.Value))
            rult.create_time_end = this.tbCreateTimeEnd.Value;
        if (!string.IsNullOrEmpty(tbModifyTimeBegin.Value))
            rult.modify_time_begin = this.tbModifyTimeBegin.Value;
        if (!string.IsNullOrEmpty(tbModifyTimeEnd.Value))
            rult.modify_time_end = this.tbModifyTimeEnd.Value;

        if (!string.IsNullOrEmpty(tbCreateUserId.Text.Trim()))
            rult.create_user_id = this.tbCreateUserId.Text.Trim();
        if (!string.IsNullOrEmpty(tbModifyUserId.Text.Trim()))
            rult.modify_user_id = this.tbModifyUserId.Text.Trim();

        if (!string.IsNullOrEmpty(tbCustomerCode.Text.Trim()))
            rult.customer_code = this.tbCustomerCode.Text.Trim();
        if (!string.IsNullOrEmpty(tbCustomerId.Text.Trim()))
            rult.customer_id = this.tbCustomerId.Text.Trim();
        if (!string.IsNullOrEmpty(this.tbLogId.Text.Trim()))
            rult.log_id = this.tbLogId.Text.Trim();

        if (!string.IsNullOrEmpty(tbUnitCode.Text.Trim()))
            rult.unit_code = this.tbUnitCode.Text.Trim();
        if (!string.IsNullOrEmpty(tbUnitId.Text.Trim()))
            rult.unit_id = this.tbUnitId.Text.Trim();

        if (!string.IsNullOrEmpty(tbUserCode.Text.Trim()))
            rult.user_code = this.tbUserCode.Text.Trim();
        if (!string.IsNullOrEmpty(tbUserId.Text.Trim()))
            rult.user_id = this.tbUserId.Text.Trim();
       
        return rult;
    }
    private Hashtable getCondition()
    {
        Hashtable ht = new Hashtable();
        //if (cbAppType.SelectedIndex > 0
        if ( !(this.cbAppType.SelectedValue == "全部"))
            ht.Add("app_code", this.cbAppType.SelectedValue);
       // if (cbAppType.SelectedIndex > 0)
         if (!(this.cbLogType.SelectedValue == "全部"))
            ht.Add("log_type_code", this.cbLogType.SelectedValue);

        if (!string.IsNullOrEmpty(tbIfCode.Text.Trim()))
            ht.Add("if_code",this.tbIfCode.Text.Trim());

        if (!string.IsNullOrEmpty(tbLogCode.Text.Trim()))
            ht.Add("log_code" ,this.tbLogCode.Text.Trim());
        if (!string.IsNullOrEmpty(tbLogBody.Text.Trim()))
            ht.Add("log_body", this.tbLogBody.Text.Trim());

        if (!string.IsNullOrEmpty(tbBizId.Text.Trim()))
            ht.Add(" biz_id",this.tbBizId.Text.Trim());
        if (!string.IsNullOrEmpty(tbBizCode.Text.Trim()))
            ht.Add("biz_name", this.tbBizCode.Text.Trim());

        if (!string.IsNullOrEmpty(this.tbLogId.Text.Trim()))
            ht.Add(" log_id", this.tbLogId.Text.Trim());

        
        if (!string.IsNullOrEmpty(tbCreateTimeBegin.Value))
            ht.Add("create_time_begin", Convert.ToDateTime(this.tbCreateTimeBegin.Value));
        if (!string.IsNullOrEmpty(tbCreateTimeEnd.Value))
            ht.Add("create_time_end", Convert.ToDateTime(this.tbCreateTimeEnd.Value));
        if (!string.IsNullOrEmpty(tbModifyTimeBegin.Value))
            ht.Add("modify_time_begin", Convert.ToDateTime(this.tbModifyTimeBegin.Value));
        if (!string.IsNullOrEmpty(tbModifyTimeEnd.Value))
            ht.Add("modify_time_end", Convert.ToDateTime(this.tbModifyTimeEnd.Value));

        if (!string.IsNullOrEmpty(tbCreateUserId.Text.Trim()))
            ht.Add("create_user_id", this.tbCreateUserId.Text.Trim());
        if (!string.IsNullOrEmpty(tbModifyUserId.Text.Trim()))
            ht.Add("modify_user_id", this.tbModifyUserId.Text.Trim());

        if (!string.IsNullOrEmpty(tbCustomerCode.Text.Trim()))
            ht.Add("customer_code", this.tbCustomerCode.Text.Trim());
        if (!string.IsNullOrEmpty(this.tbCustomerId.Text.Trim()))
            ht.Add("customer_id", this.tbCustomerId.Text.Trim());

        if (!string.IsNullOrEmpty(tbUnitCode.Text.Trim()))
            ht.Add("unit_code ", this.tbUnitCode.Text.Trim());
        if (!string.IsNullOrEmpty(tbUnitId.Text.Trim()))
            ht.Add("unit_id", this.tbUnitId.Text.Trim());

        if (!string.IsNullOrEmpty(tbUserCode.Text.Trim()))
            ht.Add("user_code", this.tbUserCode.Text.Trim());
        if (!string.IsNullOrEmpty(this.tbUnitId.Text.Trim()))
            ht.Add("user_id", this.tbUnitId.Text.Trim());

        return ht;
    }

    // 获取或设置当前查询条件
    public QueryCondition CurrentQueryCondition
    {
        get
        {
            if (this.ViewState["QueryCondition"] as QueryCondition == null)
            {
                this.ViewState["QueryCondition"] = getCondition();
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
        // public string UnitName { get; set; }
        public string user_id { set; get; }
        public string log_id { get; set; }
        public string biz_id { get; set; }
        public string biz_name { get; set; }
        public string log_type_code { get; set; }
        public string log_code { get; set; }
        public string log_body { get; set; }
        public string create_time_begin { get; set; }
        public string create_time_end { get; set; }
        public string modify_user_id { get; set; }
        public string customer_id { get; set; }
        public string create_user_id { get; set; }
        public string modify_time_begin { get; set; }
        public string modify_time_end { get; set; }
        public string customer_code { get; set; }
        public string unit_code { get; set; }
        public string unit_id { get; set; }
        public string user_code { get; set; }
        public string  app_code { get; set; }
        public string if_code { get; set; }
        //如果有其它条件可以在这里定义
    }
    #endregion


    protected void btnQuery_Click(object sender, EventArgs e)
    {
        //SplitPageControl1.Visible = true;
        btnQuery.Enabled = false;
        //设置当前查询条件
        this.CurrentQueryCondition = GetConditionFromUI();
        //查询第一页数据
        Query(0);
        btnQuery.Enabled = true;
    }
    protected void btnExportClick(object sender, EventArgs e)
    {
        var srvice = this.GetDexLogService();
        var source = new List<LogInfo>();
        foreach (var id in this.hidLogIds.Value.Split(','))
        {
            source.Add(srvice.GetLog(LoggingSession, id));
        }
        PageHelper.ExportToXml<LogInfo>(this, source, obj =>
        {
            var cells = new string[] {"<log_id>"+obj.log_id+"</log_id>",
                "<biz_id>"+obj.biz_id+"</biz_id>",
                "<biz_name>"+obj.biz_name+"</biz_name>",
                "<log_type_id>"+obj.log_type_id+"</log_type_id>",
                "<log_type_code>"+obj.log_type_code+"</log_type_code>",
                "<log_code>"+obj.log_code+"</log_code>",
                "<log_body><![CDATA["+obj.log_body+"]]></log_body>",
                "<create_time>"+obj.create_time+"</create_time>",
                "<create_user_id>"+obj.create_user_id+"</create_user_id>",
                "<modify_time>"+obj.modify_time+"</modify_time>",
                "<modify_user_id>"+obj.modify_user_id+"</modify_user_id>",
                "<customer_code>"+obj.customer_code+"</customer_code>",
                "<customer_id>"+obj.customer_id+"</customer_id>",
                "<unit_code>"+obj.unit_code+"</unit_code>",
                "<unit_id>"+obj.unit_id+"</unit_id>",
                "<user_code>"+obj.user_code+"</user_code>",
                "<user_id>"+obj.user_id+"</user_id>",
                "<if_code>"+obj.if_code+"</if_code>",
                "<app_code>"+obj.app_code+"</app_code>"
            };
            return cells;
        });
    }

    private void Query(int pageIndex)
    {
        try
        {
            var conn= getCondition();
            var service = this.GetDexLogService();
            var querylist = service.GetLogs(LoggingSession, conn
                , SplitPageControl1.PageSize
                , pageIndex * SplitPageControl1.PageSize);
            SplitPageControl1.RecoedCount = service.GetLogsCount(LoggingSession, conn);
            SplitPageControl1.PageIndex = pageIndex;
            //验证查询当前页索引是否在记录总数范围内。
            if (SplitPageControl1.PageIndex != pageIndex)
            {
                Query(SplitPageControl1.PageIndex);
                return;
            }
            else
            {
                gvLog.DataSource = querylist;
                gvLog.DataBind();
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(SplitPageControl1.PageIndex);
    }
    public cPos.Admin.Component.LoggingSessionInfo loggingSession { get; set; }

    //生成 From Url 参数
    protected override void OnPreRender(EventArgs e)
    {
        //生成 _from 隐藏域字段
        StringBuilder sb = new StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (this.gvLog.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["log_id"]))
        {
            sb.Append("&cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        if (cbAppType.SelectedIndex > 0)
            sb.Append("&app_code="+this.cbAppType.SelectedValue);
        if (!string.IsNullOrEmpty(tbIfCode.Text.Trim()))
            sb.Append("&if_code="+ this.tbIfCode.Text.Trim());
        if (!string.IsNullOrEmpty(tbLogCode.Text.Trim()))
            sb.Append("&log_code="+ this.tbLogCode.Text.Trim());
        if (!string.IsNullOrEmpty(tbBizId.Text.Trim()))
            sb.Append("&biz_id="+ this.tbBizId.Text.Trim());
        if (cbLogType.SelectedIndex > 0)
            sb.Append("&log_type_code="+ this.cbLogType.SelectedValue);

        if (!string.IsNullOrEmpty(tbBizCode.Text.Trim()))
            sb.Append("&biz_name="+this.tbBizCode.Text.Trim());
        if (!string.IsNullOrEmpty(tbCreateTimeBegin.Value))
            sb.Append("&create_time_begin="+ this.tbCreateTimeBegin.Value);
        if (!string.IsNullOrEmpty(tbCreateTimeEnd.Value))
            sb.Append("&create_time_end="+this.tbCreateTimeEnd.Value);
        if (!string.IsNullOrEmpty(tbModifyTimeBegin.Value))
            sb.Append("&modify_time_begin="+ this.tbModifyTimeBegin.Value);
        if (!string.IsNullOrEmpty(tbModifyTimeEnd.Value))
            sb.Append("&modify_time_end="+ this.tbModifyTimeEnd.Value);
        if (!string.IsNullOrEmpty(tbCreateUserId.Text.Trim()))
            sb.Append("&create_user_id="+ this.tbCreateUserId.Text.Trim());
        if (!string.IsNullOrEmpty(tbModifyUserId.Text.Trim()))
            sb.Append("&modify_user_id="+ this.tbModifyUserId.Text.Trim());
        if (!string.IsNullOrEmpty(tbCustomerCode.Text.Trim()))
            sb.Append("&customer_code="+ this.tbCustomerCode.Text.Trim());
        if (!string.IsNullOrEmpty(tbCustomerId.Text.Trim()))
            sb.Append("&customer_id=" + this.tbCustomerId.Text.Trim());
        if (!string.IsNullOrEmpty(tbUnitCode.Text.Trim()))
            sb.Append("&unit_code= "+this.tbUnitCode.Text.Trim());

        if (!string.IsNullOrEmpty(tbUnitId.Text.Trim()))
            sb.Append("&unit_id="+ this.tbUnitId.Text.Trim());

        if (!string.IsNullOrEmpty(tbUserCode.Text.Trim()))
            sb.Append("&user_code="+ this.tbUserCode.Text.Trim());
        if (!string.IsNullOrEmpty(this.tbLogId.Text.Trim()))
            sb.Append("log_id="+ this.tbLogId.Text.Trim());
        if (!string.IsNullOrEmpty(tbIfCode.Text.Trim()))
            sb.Append("&if_code="+this.tbIfCode.Text.Trim());
        if (!string.IsNullOrEmpty(tbLogBody.Text.Trim()))
            sb.Append("&log_body="+this.tbLogBody.Text.Trim());
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("log_query.aspx");
    }
}