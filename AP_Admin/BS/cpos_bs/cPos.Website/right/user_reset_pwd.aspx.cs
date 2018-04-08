using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;

public partial class right_user_reset_pwd : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbUserName.Text = this.Request.QueryString["user_name"] ?? "";
            if (string.IsNullOrEmpty(this.Request.QueryString["user_name"])) 
            {
                this.btnOK.Visible = false;
            }
            tbNewPass.Focus();
        }
    }
    protected cUserService UserService
    {
        get { return new cUserService(); }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            var userInfo = this.UserService.GetUserById(loggingSessionInfo, this.Request.QueryString["user_id"]);
            if (!this.UserService.IsValidPassword(loggingSessionInfo, userInfo, this.tbNewPass.Text))
            {
                this.InfoBox.ShowPopError("密码长度至少为8位，需要包含数字、字母和字符！");
                this.tbNewPass.Focus();
                return;
            }
            string user_id = Request["user_id"];
            if (this.UserService.ModifyUserPassword(loggingSessionInfo, user_id, this.tbNewPass.Text))
            {
                this.InfoBox.ShowPopInfo("密码修改成功");
                this.Response.Redirect(this.Request.QueryString["from"] ?? "user_query.aspx");
            }
            else
            {
                this.InfoBox.ShowPopError("密码修改失败");
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("密码修改失败！");
        }
      
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.QueryString["from"]);
    }
}