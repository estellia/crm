using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using cPos.Service;

public partial class bill_bill_role_action_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadBillData();
            loadRoleData();
            //this.QueryCondition = GetHashtableQueryString();
            if (this.QueryCondition["BillKindID"] != null)
                this.cbBillKind.SelectedValue = this.QueryCondition["BillKindID"].ToString();
            if (this.QueryCondition["RoleID"] != null)
                this.cbRole.SelectedValue = this.QueryCondition["RoleID"].ToString();
            this.QueryCondition = getCodition();
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                Query();
            }
           
        }
        this.cbBillKind.Focus();
    }
    Hashtable GetHashtableQueryString()
    {
        Hashtable ht = new Hashtable();
        var hc = this.Request.QueryString;
        ht.Add("BillKindID", hc["BillKindID"]);
        ht.Add("RoleID", hc["RoleID"]);
        return ht;
    }
    protected Hashtable getCodition()
    {
        Hashtable ht = new Hashtable();
        if (cbBillKind.SelectedIndex >= 0)
        {
            ht.Add("BillKindID", cbBillKind.SelectedValue);
        }
        if (cbRole.SelectedIndex > 0)
        {
            ht.Add("RoleID", cbRole.SelectedValue);
        }
        return ht;
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.QueryCondition = getCodition();
        Query();
    }
    protected void gvBill_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "BillDelete":
                {
                    Row_DeleteItem(e.CommandArgument as string);
                }
                break;
            default:
                break;
        }
    }



    //数据行 删除处理逻辑
    private void Row_DeleteItem(string id)
    {
        try
        {
            //string DelRuselt = "删除" + id.ToString() + "成功";
            bool DelRuselt = new cBillService().DeleteBillActionRole(loggingSessionInfo, id);
            if (DelRuselt)
            {
                this.InfoBox.ShowPopInfo("删除成功！");
                ////总查询记录数减 1
                //SplitPageControl1.RecoedCount -= 1;
            }
            else
                this.InfoBox.ShowPopError("删除失败！");
            //刷新当前页
            Query();
        }
        catch(Exception ex)
        { 
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("删除失败！");
        }
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
    private void Query()
    {
        try
        {
            var service = new cBillService();
            var querylist = service.SelectBillActionRoleList(loggingSessionInfo, QueryCondition);
                gvBill.DataSource = querylist;
                gvBill.DataBind();
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }


    private void loadBillData()
    {
        try
        {
            var bills = new cBillService().GetAllBillKinds(loggingSessionInfo);
            this.cbBillKind.DataSource = bills;
            this.cbBillKind.DataValueField = "Id";
            this.cbBillKind.DataTextField = "Description";
            this.cbBillKind.DataBind();
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }
    private void loadRoleData()
    {
        try
        {
            var roles = new RoleService().GetAllRoles(loggingSessionInfo);
            roles.Insert(0, new cPos.Model.RoleModel { Role_Id = "0", Role_Name = "请选择" });
            this.cbRole.DataSource = roles;
            this.cbRole.DataValueField = "Role_Id";
            this.cbRole.DataTextField = "Role_Name";
            this.cbRole.DataBind();
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }

    //生成From Url隐藏字段
    protected override void OnPreRender(EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (this.gvBill.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&BillKindID=" + Server.UrlEncode(cbBillKind.SelectedIndex > 0 ? cbBillKind.SelectedValue : "0"));
        sb.Append("&RoleID=" + Server.UrlEncode(cbRole.SelectedIndex > 0 ? cbRole.SelectedValue : "0"));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
}