using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using System.Collections;

public partial class bill_bill_action_query :PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadBillData();
            if (this.Request.QueryString["BillKindID"] != null)
                this.cbBillKind.SelectedValue = this.Request.QueryString["BillKindID"].ToString();
            //if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            //{
                Query(this.cbBillKind.SelectedValue);
           // }
        }
        this.cbBillKind.Focus();
    }
    protected void cbBillKind_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        Query(ddl.SelectedValue);
    }
    protected void gvBillAction_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "BillDelete":
                {
                    Row_DeleteItem(e.CommandArgument as string);
                }
                break;
        }
    }



    //数据行 删除处理逻辑
    private void Row_DeleteItem(string id)
    {
        try
        {
            bool CanDel = new cBillService().CanDeleteBillAction(loggingSessionInfo, id);
            if (CanDel)
            {
                bool DelRuselt = new cBillService().DeleteBillAction(loggingSessionInfo, id);
                if (DelRuselt)
                {
                    this.InfoBox.ShowPopInfo("删除成功！");
                }
                else
                    this.InfoBox.ShowPopError("删除失败！");
                //刷新当前页
                Query(this.cbBillKind.SelectedValue);
            }
            else
                this.InfoBox.ShowPopError("表单操作被引用，不能被删除");
        }
        catch(Exception ex)
        { 
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("删除失败！");
        }
    }
    void Query(string s)
    {

        try
        {
            var service = new cBillService();
            var querylist = service.GetAllBillActionsByBillKindId(loggingSessionInfo, s);
                gvBillAction.DataSource = querylist;
                gvBillAction.DataBind();
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }

    ////分页控件 请求更新事件
    //protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    //{
    //    Query(this.cbBillKind.SelectedValue, SplitPageControl1.PageIndex);
    //}
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
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }

   //生成From Url隐藏字段
   protected override void OnPreRender(EventArgs e)
   {
       System.Text.StringBuilder sb = new System.Text.StringBuilder();
       sb.Append(this.Request.Url.LocalPath);
       sb.Append("?and=");
        if(this.gvBillAction.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
       if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
       {
           sb.Append("&cur_menu_id=" + Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
       }
       sb.Append("&BillKindID=" + Server.UrlEncode(cbBillKind.SelectedIndex > 0 ? cbBillKind.SelectedValue : "0"));
       //sb.Append(string.Format("&pageIndex={0}&pageSize={1}", SplitPageControl1.PageIndex, SplitPageControl1.PageSize));
       this.ClientScript.RegisterHiddenField("_from", sb.ToString());
       base.OnPreRender(e);
   }
}