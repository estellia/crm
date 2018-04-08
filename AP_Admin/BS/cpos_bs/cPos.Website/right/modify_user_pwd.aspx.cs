using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Components;
using cPos.Service;
using cPos.Model.User;

public partial class right_modify_user_pwd : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbUserName.Text = new SessionManager().loggingSessionInfo.CurrentUser.User_Name;
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.tbOldPwd.Text.Trim()))
        {
            this.InfoBox.ShowPopError("旧密码不能为空");
            this.tbOldPwd.Focus();
            return;
        }
        if (string.IsNullOrEmpty(this.tbNewPwd.Text.Trim()))
        {
            this.InfoBox.ShowPopError("新密码不能为空");
            this.tbNewPwd.Focus();
            return;
        }
        if (this.tbNewPwd.Text.Trim() != this.tbNewPwd2.Text.Trim())
        {
            this.InfoBox.ShowPopError("新密码两次输入不一致");
            this.tbNewPwd2.Focus();
            return;
        }

        cUserService user_service = new cUserService();
        string user_id = this.loggingSessionInfo.CurrentUser.User_Id;
        UserInfo user = user_service.GetUserById(this.loggingSessionInfo, user_id);
        if (user == null)
        {
            this.InfoBox.ShowPopError("当前用户不存在");
            this.tbNewPwd.Focus();
            return;
        }
        string old_pwd = EncryptManager.Hash(this.tbOldPwd.Text.Trim(), HashProviderType.MD5);
        if (!old_pwd.Equals(user.User_Password))
        {
            this.InfoBox.ShowPopError("旧密码不正确");
            this.tbOldPwd.Focus();
            return;
        }
        string new_pwd = this.tbNewPwd.Text.Trim();
        if (!user_service.IsValidPassword(loggingSessionInfo, user, new_pwd))
        {
            this.InfoBox.ShowPopError("新密码无效");
            this.tbNewPwd.Focus();
            return;
        }

        if (user_service.ModifyUserPassword(this.loggingSessionInfo, user_id, new_pwd))
        {
            this.InfoBox.ShowPopInfo("密码修改成功");
            this.Response.Redirect("~/common/emtpy.aspx");
        }
        else
        {
            this.InfoBox.ShowPopError("密码修改失败");
        }        
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/common/empty.aspx");
    }
}