using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using System.Collections;

public partial class Repair_repair_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUnitsInfo();
            SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            GetConditionFromBack();
            this.CurrentQueryCondition = GetConditionFromUI();
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        btnQuery.Enabled = false;
        this.CurrentQueryCondition = GetConditionFromUI();
        Query(0);
        btnQuery.Enabled = true;
    }

    protected void btnResetClick(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.RawUrl);
    }

    private void Query(int pageIndex)
    {
        try
        {
            var querylist = new RepairService().SearchRepairInfo(this.loggingSessionInfo
            , CurrentQueryCondition.unit_ids
            , CurrentQueryCondition.status
            , CurrentQueryCondition.repair_date_begin
            , CurrentQueryCondition.repair_date_end
            , SplitPageControl1.PageSize
                , pageIndex * SplitPageControl1.PageSize
            );

            SplitPageControl1.RecoedCount = querylist.icount;
            SplitPageControl1.PageIndex = pageIndex;
            //验证查询当前页索引是否在记录总数范围内。
            if (SplitPageControl1.PageIndex != pageIndex)
            {
                Query(SplitPageControl1.PageIndex);
                return;
            }
            else
            {
                gvInoutBill.DataSource = querylist.repairInfoList;
                gvInoutBill.DataBind();
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

    public IList<cPos.Model.RepairInfo> repairInfoList
    {
        set;
        get;
    }

    #region 获取显示的字符串
    public string GetOutStr(IList<cPos.Model.RepairInfo> repairInfoList)
    {
        string str = string.Empty;
        if (repairInfoList != null && repairInfoList.Count > 0)
        {
            foreach (RepairInfo repairInfo in repairInfoList)
            {
                str += "<tr class=\"b_c4\">";
                //序号
                str += "<td align=\"center\">";
                str += repairInfo.row_no;
                str += "</td>";
                //门店
                str += "<td align=\"center\">";
                str += repairInfo.unit_name;
                str += "</td>";
                //报修类型
                str += "<td align=\"center\">";
                str += repairInfo.repair_type_name;
                str += "</td>";
                //备注
                str += "<td align=\"center\">";
                str += repairInfo.remark;
                str += "</td>";
                //终端序号
                str += "<td align=\"center\">";
                str += repairInfo.pos_sn;
                str += "</td>";
                //手机
                str += "<td align=\"center\">";
                str += repairInfo.phone;
                str += "</td>";
                //状态描述
                str += "<td align=\"center\">";
                str += repairInfo.status_desc;
                str += "</td>";
                //报修时间
                str += "<td align=\"center\">";
                str += repairInfo.repair_time;
                str += "</td>";
                //报修人
                str += "<td align=\"center\">";
                str += repairInfo.repair_user_name;
                str += "</td>";
                //响应时间
                str += "<td align=\"center\">";
                str += repairInfo.response_time;
                str += "</td>";
                //响应人
                str += "<td align=\"center\">";
                str += repairInfo.response_user_name;
                str += "</td>";
                //修复完成确认时间
                str += "<td align=\"center\">";
                str += repairInfo.complete_time;
                str += "</td>";
                //修复完成确认人
                str += "<td align=\"center\">";
                str += repairInfo.complete_user_name;
                str += "</td>";
              
                str += "</tr>";
            }
        }
        return str;
    }
    #endregion

    private void GetConditionFromBack()
    {
        if (Request.QueryString["unit_id"] != null)
        {
            this.selSalesUnit.SelectedValue = Request.QueryString["unit_id"].ToString();
            this.selSalesUnit.SelectedText = Request.QueryString["unit_name"].ToString();
        }
        if (Request.QueryString["repair_date_begin"] != null)
            this.selRepairDateBegin.Value = Request.QueryString["repair_date_begin"].ToString() ?? "";
        if (Request.QueryString["repair_date_end"] != null)
            this.selRepairDateEnd.Value = Request.QueryString["repair_date_end"].ToString() ?? "";
        if (Request.QueryString["status"] != null)
            this.selStatus.SelectedValue = Request.QueryString["status"];
    }

    private QueryCondition GetConditionFromUI()
    {
        var rult = new QueryCondition();
        if (selStatus.SelectedIndex > 0)
        {
            rult.status = selStatus.SelectedValue;
        }
        if (!string.IsNullOrEmpty(this.selSalesUnit.SelectedValue))
        {
            rult.unit_ids = this.selSalesUnit.SelectedValue;
            rult.unit_names = this.selSalesUnit.SelectedText;
        }
        if (!string.IsNullOrEmpty(this.selRepairDateBegin.Value))
        {
            rult.repair_date_begin = this.selRepairDateBegin.Value;
        }
        if (!string.IsNullOrEmpty(this.selRepairDateEnd.Value))
        {
            rult.repair_date_end = this.selRepairDateEnd.Value;
        }
        return rult;
    }

    #region 加载单位树的数据LoadUnitsInfo()
    //加载组织信息
    private void LoadUnitsInfo()
    {
        try
        {
            var service = new UnitService();
            this.selSalesUnit.DataBind(service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo).Select(item => new controls_DropDownTree.tvNode
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
    #endregion

    #region Condition 对象定义
    /// <summary>
    /// 查询条件对象
    /// </summary>
    [System.Serializable]
    public class QueryCondition
    {
        public string unit_ids { get; set; }
        public string unit_names { get; set; }
        public string status { set; get; }
        public string repair_date_begin { set; get; }
        public string repair_date_end { set; get; }


        //如果有其它条件可以在这里定义
    }
    #endregion

    #region 查询条件
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
    #endregion

    protected override void OnPreRender(EventArgs e)
    {
        //生成 _from 隐藏域字段
        StringBuilder sb = new StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (this.gvInoutBill.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        sb.Append("&status=" + this.Server.UrlEncode(CurrentQueryCondition.status));
        sb.Append("&unit_id=" + this.Server.UrlEncode(CurrentQueryCondition.unit_ids));
        sb.Append("&unit_name=" + this.Server.UrlEncode(CurrentQueryCondition.unit_names));
        sb.Append("&repair_date_begin=" + this.Server.UrlEncode(CurrentQueryCondition.repair_date_begin));
        sb.Append("&repair_date_end=" + this.Server.UrlEncode(CurrentQueryCondition.repair_date_end));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
    }
    
}