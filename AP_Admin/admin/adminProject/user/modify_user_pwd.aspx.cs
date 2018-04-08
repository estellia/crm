using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Admin.DataCrypt;

public partial class user_modify_user_pwd : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbUserName.Text = cPos.Admin.Component.SessionManager.CurrentLoggingSession.UserName;
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
        if (!this.GetUserService().ValidateUserPassword(this.tbOldPwd.Text.Trim(), this.tbNewPwd.Text.Trim()))
        {
            this.InfoBox.ShowPopError("新密码无效");
            this.tbNewPwd.Focus();
            return;
        }

        string user_id = cPos.Admin.Component.SessionManager.CurrentLoggingSession.UserID;
        string old_pwd = HashManager.Hash(this.tbOldPwd.Text.Trim(), HashProviderType.MD5);
        string new_pwd = HashManager.Hash(this.tbNewPwd.Text.Trim(), HashProviderType.MD5);
        int ret = this.GetUserService().ModifyUserPassword(user_id, old_pwd, new_pwd);
        switch (ret)
        {
            case 1:
                this.InfoBox.ShowPopError("用户不存在");
                this.tbOldPwd.Focus();
                break;
            case 2:
                this.InfoBox.ShowPopError("用户被停用");
                this.tbOldPwd.Focus();
                break;
            case 3:
                this.InfoBox.ShowPopError("旧密码不正确");
                this.tbOldPwd.Focus();
                break;
            case 4:
                this.Redirect("修改成功", InfoType.Info, "../common/empty.aspx");
                break;
            case 5:
                this.InfoBox.ShowPopError("修改失败");
                this.tbNewPwd.Focus();
                break;
            default:
                break;
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("~/common/empty.aspx");
    }
}