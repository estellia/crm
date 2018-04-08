using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text.RegularExpressions;


using cPos.Admin.Component;
using cPos.Admin.Service.Interfaces;

public partial class login_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.txtUsername.Focus();

            if (Request.Cookies["cpos_remember"] != null)
            {
                try
                {
                    string cpos_remember = Request.Cookies["cpos_bs_remember"].Value;
                    string cpos_user = Request.Cookies["cpos_bs_user"].Value;
                    string cpos_pwd = Request.Cookies["cpos_bs_pwd"].Value;

                    if (cpos_remember == "1")
                    {
                        this.chkRemember.Checked = true;
                        this.txtUsername.Value = cpos_user;
                    }
                }
                catch
                { }
            }
        }
    }

    private IUserService GetUserService()
    {
        return (IUserService)BusinessServiceProxyLocator.GetService(typeof(IUserService));
    }

    /// <summary>
    /// 用户点击登录按钮后检查用户是否合法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        PageLog.Current.Write(string.Format("btnLogin sessionId is :{0},count:{1}", this.Session.SessionID, this.Session.Count));
        //将用户登录名密码提交到服务器检验
        string account = txtUsername.Value.Trim();
        string pwd = txtPassword.Value.Trim();
        //string authCode = txtValidateCode.Text.Trim();

        if (string.IsNullOrEmpty(account))
        {
            lblInfor.Text = "请输入用户名！";
            txtUsername.Focus();
            return;
        }
        if (string.IsNullOrEmpty(pwd))
        {
            lblInfor.Text = "请输入密码！";
            txtPassword.Focus();
            return;
        }
         
        //if (string.IsNullOrEmpty(authCode))
        //{
        //    lblInfor.Text = "请输入验证码！";
        //    txtValidateCode.Focus();
        //    return;
        //}
        //if (authCode != this.Session[SessionManager.KEY_AUTH_CODE].ToString())
        //{
        //    lblInfor.Text = "验证码错误！";
        //    txtValidateCode.Focus();
        //    return;
        //}

        Hashtable ht = new Hashtable();
        try
        {

            int ret = this.GetUserService().ValidateUser(account, pwd, out ht);
            switch (ret)
            {
                case -1:
                    lblInfor.Text = "用户不存在";
                    return;
                case -2:
                    lblInfor.Text = "用户被停用";
                    return;
                case -3:
                    lblInfor.Text = "密码不正确";
                    return;
                case 1:
                    //用户名和密码验证通过
                    this.Session.Add("UserID", ht["UserID"]);
                    this.Session.Add("UserName", ht["UserName"]);
                    LoggingSessionInfo loggingSession = new LoggingSessionInfo();
                    loggingSession.UserID = ht["UserID"].ToString();
                    loggingSession.UserName = ht["UserName"].ToString();
                    SessionManager.CurrentLoggingSession = loggingSession;

                    var loggingObj = new cPos.Model.LoggingSessionInfo();
                    loggingObj.CurrentUser = new cPos.Model.User.UserInfo();
                    loggingObj.CurrentUser.User_Id = loggingSession.UserID;
                    loggingObj.CurrentUser.User_Name = loggingSession.UserName;
                    loggingObj.CurrentLoggingManager = new cPos.Model.LoggingManager();
                    loggingObj.CurrentUserRole = new cPos.Model.User.UserRoleInfo();
                    this.Session["loggingSessionInfo"] = loggingObj;

                    break;
                default:
                    lblInfor.Text = "用户名和密码不正确";
                    return;
            }
        }     
        catch(Exception ex)
        {
            PageLog.Current.Write(string.Format("登录失败 :{0}", ex.Message));
            lblInfor.Text = "用户名和密码不正确";
            return;
        }

        // chkRemember
        if (chkRemember.Checked)
        {
            Response.Cookies["cpos_bs_remember"].Value = "1";
            Response.Cookies["cpos_bs_remember"].Expires = DateTime.MaxValue;

            Response.Cookies["cpos_bs_user"].Value = account;
            Response.Cookies["cpos_bs_user"].Expires = DateTime.MaxValue;

            Response.Cookies["cpos_bs_pwd"].Value = pwd;
            Response.Cookies["cpos_bs_pwd"].Expires = DateTime.MaxValue;
        }
        else
        {
            Response.Cookies["cpos_bs_remember"].Value = "";
            Response.Cookies["cpos_bs_user"].Value = "";
            Response.Cookies["cpos_bs_pwd"].Value = "";
        }

        //this.Response.Redirect("~/common/homepage.aspx");
        this.Response.Redirect("~/default.aspx");
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (Request.Cookies["cpos_bs_remember"] != null)
        {
            string cpos_remember = Request.Cookies["cpos_bs_remember"].Value;
            if (cpos_remember == "1")
            {
                if (Request.Cookies["cpos_bs_pwd"] != null)
                {
                    string cpos_pwd = Request.Cookies["cpos_bs_pwd"].Value;
                    //this.txtPassword.Attributes["value"] = cpos_pwd;
                    this.hdPwd.Value = cpos_pwd;
                }
            }
        }
    }
}