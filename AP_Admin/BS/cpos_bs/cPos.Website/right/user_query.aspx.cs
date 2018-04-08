using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model.User;
using System.Collections;

public partial class right_user_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUserStatus();
            LoadQueryByUrl();
            this.QueryCodition = GetCodition();
            this.SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
               
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
        }
    }
    private cUserService UserService
    {
        get
        {
            return new cUserService();
        }
    }
    //获取界面上的查询条件
    private void LoadUserStatus()
    {
        try
        {
            var source = (new cPos.Service.cBillService()).GetBillStatusByKindCode(loggingSessionInfo, "USERMANAGER");
            source.Insert(0, new cPos.Model.BillStatusModel { Status = "0", Description = "全部" });
            this.cbUserStatus.DataTextField = "Description";
            this.cbUserStatus.DataValueField = "Status";
            this.cbUserStatus.DataSource = source;
            this.cbUserStatus.DataBind();
            this.cbUserStatus.Items.FindByText("正常").Selected = true;
            this.tbUserCode.Focus();

        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.QueryCodition = GetCodition();
        Query(this.gvUser.PageIndex);
    }
    protected void btnChangStatusClick(object sender, EventArgs e)
    {
        try
        {
            var status = this.hid_User_Status.Value;
            var user_id = this.hid_User_Id.Value;
            var msg = status == "1" ? "启用" : "停用";
            if (this.UserService.SetUserStatus(user_id, status, loggingSessionInfo))
            {
                this.InfoBox.ShowPopInfo(msg + "用户成功！");
            }
            else
            {
                this.InfoBox.ShowPopError(msg + "用户失败！");
            }
            Query(0);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //从界面获取查询条件
    private UserQueryCondition GetCodition()
    {
        var condition = new UserQueryCondition();
        if (!string.IsNullOrEmpty(this.tbUserName.Text))
        {
            condition.UserName = this.tbUserName.Text;
        }
        if (!string.IsNullOrEmpty(this.tbUserCode.Text))
        {
            condition.UserCode = this.tbUserCode.Text;
        }
        if (!string.IsNullOrEmpty(this.tbCellPhone.Text))
        {
            condition.UserCellPhone = this.tbCellPhone.Text;
        }
        if (this.cbUserStatus.SelectedIndex != 0)
        {
            condition.UserStatus = this.cbUserStatus.SelectedValue;
        }
            return condition;
    }
    //查询条件
    protected UserQueryCondition QueryCodition
    {
        get
        {
            if (ViewState["queryData"] == null)
                ViewState["queryData"] = new UserQueryCondition();
            return ViewState["queryData"] as UserQueryCondition;
        }
        set
        {
            ViewState["queryData"] = value;
        }
    }
    protected void SplitPageControl1_RequireUpdate(object sender,EventArgs e)
    {
        Query(this.SplitPageControl1.PageIndex);
    }
    //查询记录
    private void Query(int index)
    {
        try
        {
            var qc = this.QueryCodition;
            var source = this.UserService.SearchUserList(loggingSessionInfo, qc.UserName, qc.UserCode, qc.UserCellPhone, qc.UserStatus, this.SplitPageControl1.PageSize, this.SplitPageControl1.PageSize * index);
            //var temp = this.UserService.SearchUserList(loggingSessionInfo, null, null, null, null, this.SplitPageControl1.PageSize, this.SplitPageControl1.PageSize * index);
            this.SplitPageControl1.RecoedCount = source.ICount;
            this.SplitPageControl1.PageIndex = index;
            this.gvUser.DataSource = source.UserInfoList;
            this.gvUser.DataBind();
            if (this.SplitPageControl1.PageIndex != index)
                Query(this.SplitPageControl1.PageIndex);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //前台注册_from字段
    protected override void OnPreRender(EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (this.gvUser.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&user_name=" + Server.UrlEncode(tbUserName.Text));
        sb.Append("&user_code=" + Server.UrlEncode(tbUserCode.Text));
        sb.Append("&cell_phone=" + Server.UrlEncode(tbCellPhone.Text));
        sb.Append("&user_status=" + Server.UrlEncode(cbUserStatus.SelectedValue));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", Server.UrlEncode(this.SplitPageControl1.PageIndex.ToString()), Server.UrlEncode(this.SplitPageControl1.PageSize.ToString())));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
    //根据url加载查询条件
    private void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        if (!string.IsNullOrEmpty(qs["user_name"]))
        {
            this.tbUserName.Text = qs["user_name"];
        }
        if (!string.IsNullOrEmpty(qs["user_code"]))
        {
            this.tbUserCode.Text = qs["user_code"];
        }
        if (!string.IsNullOrEmpty(qs["cell_phone"]))
        {
            this.tbCellPhone.Text = qs["cell_phone"];
        }
        if (!string.IsNullOrEmpty(qs["user_status"]))
        {
            this.cbUserStatus.SelectedValue = qs["user_status"];
        }
    }
    [Serializable]
    public class UserQueryCondition
    {
        #region 用户查询条件
        //用户姓名
        public string UserName { get; set; }
        //用户编码
        public string UserCode { get; set; }
        //用户手机号码
        public string UserCellPhone { get; set; }
        //用户状态
        public string UserStatus { get; set; }
        #endregion
    }
}