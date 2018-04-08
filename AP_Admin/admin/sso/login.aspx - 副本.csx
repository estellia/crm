using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using cPos.Admin.Component;
using cPos.Admin.Service.Interfaces;
using System.Xml;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.txtCustomerCode.Focus();

            if (Request.Cookies["cpos_sso_remember"] != null)
            {
                try
                {
                    string cpos_remember = Request.Cookies["cpos_sso_remember"].Value;
                    string cpos_cust = Request.Cookies["cpos_sso_cust"].Value;
                    string cpos_user = Request.Cookies["cpos_sso_user"].Value;
                    string cpos_pwd = Request.Cookies["cpos_sso_pwd"].Value;

                    if (cpos_remember == "1")
                    {
                        this.chkRemember.Checked = true;
                        this.txtCustomerCode.Value = cpos_cust;
                        this.txtUsername.Value = cpos_user;
                    }
                }
                catch
                { }
            }
        }
    }

    private ICustomerService GetCustomerService()
    {
        return (ICustomerService)BusinessServiceProxyLocator.GetService(typeof(ICustomerService));
    }

    protected void btnLogin_Click(object sender, ImageClickEventArgs e)
    {
        string customerCode = this.txtCustomerCode.Value.Trim();
        string account = this.txtUsername.Value.Trim();
        string pwd = this.txtPassword.Value.Trim();
        //string authCode = txtValidateCode.Text.Trim();     
        //将用户登录名密码提交到服务器检验   

        if (string.IsNullOrEmpty(customerCode) || customerCode == "公司编码")
        {
            lblInfor.Text = "请输入公司编码！";
            txtCustomerCode.Focus();
            return;
        }
        if (string.IsNullOrEmpty(account) || account == "用户名")
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
            int ret = this.GetCustomerService().ValidateUser(customerCode, account, pwd, out ht);
            switch (ret)
            {
                case -11:
                    lblInfor.Text = "公司不存在";
                    return;
                case -12:
                    lblInfor.Text = "公司被停用";
                    return;
                case -1:
                    lblInfor.Text = "用户不存在";
                    return;
                case -2:
                    lblInfor.Text = "用户被停用";
                    return;
                case -3:
                    lblInfor.Text = "密码不正确";
                    return;
                case -4:
                    lblInfor.Text = "用户不在线";
                    return;
                case 1:
                    //用户名和密码验证通过
                    break;
                default:
                    lblInfor.Text = "用户名和密码不正确";
                    return;
            }
        }
        catch
        {
            lblInfor.Text = "用户名和密码不正确";
            return;
        }

        // chkRemember
        if (chkRemember.Checked)
        {
            Response.Cookies["cpos_sso_remember"].Value = "1";
            Response.Cookies["cpos_sso_remember"].Expires = DateTime.MaxValue;

            Response.Cookies["cpos_sso_cust"].Value = customerCode;
            Response.Cookies["cpos_sso_cust"].Expires = DateTime.MaxValue;

            Response.Cookies["cpos_sso_user"].Value = account;
            Response.Cookies["cpos_sso_user"].Expires = DateTime.MaxValue;

            Response.Cookies["cpos_sso_pwd"].Value = pwd;
            Response.Cookies["cpos_sso_pwd"].Expires = DateTime.MaxValue;
        }
        else
        {
            Response.Cookies["cpos_sso_remember"].Value = "";
            Response.Cookies["cpos_sso_cust"].Value = "";
            Response.Cookies["cpos_sso_user"].Value = "";
            Response.Cookies["cpos_sso_pwd"].Value = "";
        }

        string goURL = string.Format("{0}{1}?tid={2}&cid={3}",
            ht["AccessURL"].ToString().EndsWith("/") ? ht["AccessURL"].ToString() : ht["AccessURL"].ToString() + "/", 
            ConfigurationManager.AppSettings["login_go_page"], ht["Token"], ht["CustomerID"]);
        this.Response.Redirect(goURL);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (Request.Cookies["cpos_sso_remember"] != null)
        {
            string cpos_remember = Request.Cookies["cpos_sso_remember"].Value;
            if (cpos_remember == "1")
            {
                if (Request.Cookies["cpos_sso_pwd"] != null)
                {
                    string cpos_pwd = Request.Cookies["cpos_sso_pwd"].Value;
                    //this.txtPassword.Attributes["value"] = cpos_pwd;
                    this.hdPwd.Value = cpos_pwd;
                }
            }
        }
    }
}