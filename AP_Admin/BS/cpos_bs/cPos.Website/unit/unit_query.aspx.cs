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

public partial class unit_unit_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            InitUnitType();
            InitAreaTree();
            InitUnitStatus(); 
            if (!string.IsNullOrEmpty(this.Request.QueryString["unit_city_id"]))
            {
                this.DropDownTree1.SelectedValue = this.Request.QueryString["unit_city_id"].ToString();
                this.DropDownTree1.SelectedText = this.Request.QueryString["unit_city_name"].ToString();
            }
            if (!string.IsNullOrEmpty(this.Request.QueryString["unit_Code"]))
            {
                this.tbUnitCode.Text = this.Request.QueryString["unit_Code"].ToString();
            }
            if (!string.IsNullOrEmpty(this.Request.QueryString["unit_Status"]))
            {
                this.selUnitStatus.SelectedValue = this.Request.QueryString["unit_Status"].ToString();
            }
            else
            {
                this.selUnitStatus.SelectedIndex = 1;
            }
            if (!string.IsNullOrEmpty(this.Request.QueryString["unit_name"]))
            {
                this.tbUnitName.Text = this.Request.QueryString["unit_name"].ToString();
            }
            if (!string.IsNullOrEmpty(this.Request.QueryString["unit_type_id"]))
            {
                this.ddlUnitType.SelectedValue = this.Request.QueryString["unit_type_id"].ToString();
                this.ddlUnitType.Text = this.Request.QueryString["unit_type_name"].ToString();
            }
            if (!string.IsNullOrEmpty(this.Request.QueryString["unit_Tel"]))
            {
                this.tbUnitTel.Text = this.Request.QueryString["unit_Tel"].ToString();
            }
            SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10"); 
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                this.CurrentQueryCondition = GetConditionFromUI();
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
            tbUnitCode.Focus();
        }
    }
    protected string getStatusText(object obj)
    {
        var status = obj as string;
        if (status == "停用") {
            return "启用";
        }
        return "停用";
    }
    QueryCondition GetConditionFromQueryString()
    {
        QueryCondition qc = new QueryCondition();
        var qs = this.Request.QueryString;
        qc.unit_city_id = qs["unit_city_id"];
        qc.unit_Code = qs["unit_Code"];
        qc.unit_name = qs["unit_name"];

        qc.unit_Status = qs["unit_Status"];
       // qc.unit_Status = qs["unit_Status"];

        qc.unit_Tel = qs["unit_Tel"];

        qc.unit_type_id = qs["unit_type_id"];
        qc.unit_type_id = qs["unit_type_name"];

        qc.unit_city_name = qs["unit_city_name"];
        Session["unit_query"] = qc;
        return qc;
    }

    void InitUnitType()
    {
        if ((new TypeService()).GetTypeInfoListByDomain(loggingSessionInfo, "UnitType") != null)
        {
            ddlUnitType.DataSource = new TypeInfo[] { new TypeInfo { Type_Id = "", Type_Name = "所有" } }.Union((new TypeService()).GetTypeInfoListByDomain(loggingSessionInfo, "UnitType"));
            ddlUnitType.DataBind();
            ddlUnitType.SelectedValue = this.CurrentQueryCondition.unit_type_id;
            if (ddlUnitType.SelectedIndex < 0 && ddlUnitType.Items.Count > 0)
            {
                ddlUnitType.SelectedIndex = 0;
            }
        }
    }

    void InitUnitStatus()
    {
        if ((new cBillService()).GetBillStatusByKindCode(loggingSessionInfo, "UNITMANAGER") != null)
        {
            var source = (new cBillService()).GetBillStatusByKindCode(loggingSessionInfo, "UNITMANAGER");
            source.Insert(0, new BillStatusModel { Status="", Description="全部"});
            selUnitStatus.DataSource = source;
            selUnitStatus.DataBind();
            selUnitStatus.SelectedValue = this.CurrentQueryCondition.unit_Status;
            if (selUnitStatus.SelectedIndex < 0 && ddlUnitType.Items.Count > 0)
            {
                selUnitStatus.Items.FindByText("正常").Selected = true;
            }
        }
    }

    private void InitAreaTree()
    {
        var serv = new CityService();
        var list = serv.GetProvinceList(loggingSessionInfo).OrderBy(obj => obj.City_Code).Select(obj =>
                   new controls_DropDownTree.tvNode
                   {
                       Complete = false,
                       Text = obj.City1_Name,
                       Value = obj.City_Id, 
                       Id = obj.City_Code,
                   });

        DropDownTree1.DataBind(new controls_DropDownTree.tvNode[]{
            new controls_DropDownTree.tvNode{Complete = true,Text = "请选择区域",Value = "0",Id="000000"}
        }.Union(list));

        DropDownTree1.SelectedText = "请选择区域";
        DropDownTree1.SelectedValue = "0";

        if (!string.IsNullOrEmpty(this.CurrentQueryCondition.unit_city_id))
        {
            DropDownTree1.SelectedValue = this.CurrentQueryCondition.unit_city_id;
            DropDownTree1.SelectedText = this.Request.QueryString["unit_city_name"] ?? "";
        }
    }
    //返回图片路径的方法
    protected string getImgUrl(string status)
    {
        if (status == "正常")
        {
            return "../img/disable.png";
        }
        else
        {
            return "../img/enable.png";
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //设置当前查询条件
        this.CurrentQueryCondition = GetConditionFromUI();
        //查询第一页数据
        Query(0);
    }

    #region Condition “查询条件"的对象定义
    /// <summary>
    /// 查询条件对象
    /// </summary>
    [System.Serializable]
    public class QueryCondition
    {
        public LoggingSessionInfo loggingSession { get; set; }
        public string unit_Code { get; set; }
        public string unit_name { get; set; }
        public string unit_id { get; set; }
        public string unit_type_id { get; set; }
        public string unit_Tel { get; set; }
        public string unit_city_id { get; set; }
        public string unit_Status { get; set; }
        public string unit_city_name { get; set; }
        public string unit_type_name { get; set; }
        //如果有其它条件可以在这里定义
    }
    #endregion

    private void Query(int pageIndex)
    {
        try
        {
            var service = new UnitService();
            var querylist = service.SearchUnitInfo(CurrentQueryCondition.loggingSession, CurrentQueryCondition.unit_Code, CurrentQueryCondition.unit_name, CurrentQueryCondition.unit_type_id, CurrentQueryCondition.unit_Tel, CurrentQueryCondition.unit_city_id, CurrentQueryCondition.unit_Status, SplitPageControl1.PageSize, pageIndex * SplitPageControl1.PageSize);
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
                gvUnit.DataSource = querylist.UnitInfoList;
                gvUnit.DataBind();
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
        if (loggingSessionInfo != null)
            rult.loggingSession = loggingSessionInfo;
        if (!string.IsNullOrEmpty(this.tbUnitCode.Text.Trim()))
            rult.unit_Code = this.tbUnitCode.Text.Trim();
        if (!string.IsNullOrEmpty(this.tbUnitName.Text.Trim()))
            rult.unit_name = this.tbUnitName.Text.Trim();
        if (this.ddlUnitType.SelectedIndex > 0)
            rult.unit_type_id = this.ddlUnitType.SelectedValue;
            rult.unit_type_name = this.ddlUnitType.Text;
        if (!string.IsNullOrEmpty(this.tbUnitTel.Text.Trim()))
            rult.unit_Tel = this.tbUnitTel.Text.Trim();
        if (this.selUnitStatus.SelectedIndex > 0)
            rult.unit_Status = this.selUnitStatus.SelectedValue;
        if (this.DropDownTree1.SelectedValue!="0")
        {
            rult.unit_city_id = this.DropDownTree1.SelectedValue.Split(',')[0];
            rult.unit_city_name = this.DropDownTree1.SelectedText;
        }
        return rult;
    }

    //获取并保存当前查询条件
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


    protected void gvUnit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "正常":
                {
                    bool Result = new UnitService().SetUnitStatus(e.CommandArgument.ToString(), "-1", loggingSessionInfo);
                    if (Result)
                    {
                        this.InfoBox.ShowPopInfo("停用成功！");
                        Query(0);
                        //gvUnit.DataBind();
                    }
                    else
                    {
                        this.InfoBox.ShowPopError("停用失败！");
                    }
                } break;
            case "停用":
                {
                    bool Result = new UnitService().SetUnitStatus(e.CommandArgument.ToString(), "1", loggingSessionInfo);
                    if (Result)
                    {
                        this.InfoBox.ShowPopInfo("启用成功！");
                        Query(0);
                        // gvUnit.DataBind();
                    }
                    else
                    {
                        this.InfoBox.ShowPopError("启用失败！");
                    }
                } break;
            default:
                break;
        }
    }
    //生成 From Url 参数
    protected override void OnPreRender(EventArgs e)
    {
        //生成 _from 隐藏域字段
        StringBuilder sb = new StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (this.gvUnit.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&unit_city_id=" + this.Server.UrlEncode(CurrentQueryCondition.unit_city_id));
        sb.Append("&unit_city_name=" + this.Server.UrlEncode(CurrentQueryCondition.unit_city_name));
        //sb.Append("&city_text=" + this.Server.UrlEncode(CurrentQueryCondition.unit_city_name));
        sb.Append("&unit_Code=" + this.Server.UrlEncode(CurrentQueryCondition.unit_Code));
        sb.Append("&unit_name=" + this.Server.UrlEncode(CurrentQueryCondition.unit_name));
        sb.Append("&unit_Status=" + this.Server.UrlEncode(CurrentQueryCondition.unit_Status));
        sb.Append("&unit_Tel=" + this.Server.UrlEncode(CurrentQueryCondition.unit_Tel));
        sb.Append("&unit_type_id=" + this.Server.UrlEncode(CurrentQueryCondition.unit_type_id));
        sb.Append("&unit_type_name=" + this.Server.UrlEncode(CurrentQueryCondition.unit_type_name));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
    }

    //查询状态函数
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
}
