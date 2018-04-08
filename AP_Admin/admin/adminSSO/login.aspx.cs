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
using System.Net;
using System.Text;
using System.IO;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.txtCustomerCode.Focus();

            if (Request.Cookies["cpos_sso_remember"]!=null)
            {
                try
                {
                    string cpos_remember = Request.Cookies["cpos_sso_remember"].Value;
                    string cpos_cust = Request.Cookies["cpos_sso_cust"].Value;
                    //string cpos_user = Request.Cookies["cpos_sso_user"].Value;
                    //string cpos_pwd = Request.Cookies["cpos_sso_pwd"].Value;

                    if (cpos_remember == "1")
                    {
                        this.chkRemember.Value = "true";
                        this.txtCustomerCode.Value = cpos_cust;
                        //this.txtUsername.Value = cpos_user;
                        //this.txtPassword.Value = cpos_pwd;
                    }
                }
                catch
                { }
            }
            else
            {
                this.chkRemember.Value = "false";
            }
            //else
            //{
            //    this.chkRemember.Checked = false;
            //    this.txtCustomerCode.Value = "";
            //    this.txtUsername.Value = "";
            //    this.txtPassword.Value = "";
            //}
        }
    }

    private ICustomerService GetCustomerService()
    {
        return (ICustomerService)BusinessServiceProxyLocator.GetService(typeof(ICustomerService));
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string customerCode = this.txtCustomerCode.Value.Trim();
        string account = this.txtUsername.Value.Trim();
        string pwd = this.txtPassword.Value.Trim();

      
        #region
        //string authCode = txtValidateCode.Text.Trim();     
        //将用户登录名密码提交到服务器检验 
        if (string.IsNullOrEmpty(customerCode) || customerCode == "输入公司编码")
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
        #endregion
        Hashtable ht = new Hashtable();
        try
        {
            int ret = this.GetCustomerService().ValidateUser(customerCode, account, pwd, out ht);
            #region
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
                case -5:
                    lblInfor.Text = "用户过期";
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
        //if (this.chkRemember.Checked)
        //{
            Response.Cookies["cpos_sso_remember"].Value = "1";
            Response.Cookies["cpos_sso_remember"].Expires = DateTime.MaxValue;

            Response.Cookies["cpos_sso_cust"].Value = customerCode;
            Response.Cookies["cpos_sso_cust"].Expires = DateTime.MaxValue;

        //Response.Cookies["cpos_sso_user"].Value = account;
        //Response.Cookies["cpos_sso_user"].Expires = DateTime.MaxValue;

        //Response.Cookies["cpos_sso_pwd"].Value = pwd;
        //Response.Cookies["cpos_sso_pwd"].Expires = DateTime.MaxValue;
        //}
        //else
        //{
        //this.chkRemember.Value = "false";
        //Remove("cpos_sso_remember");
        //Remove("cpos_sso_cust");
        //Remove("cpos_sso_user");
        //Remove("cpos_sso_pwd");
        //}
        #endregion

        //string goURL = string.Format("{0}{1}?tid={2}&cid={3}",
        //ht["AccessURL"].ToString().EndsWith("/") ? ht["AccessURL"].ToString() : ht["AccessURL"].ToString() + "/",
        //ConfigurationManager.AppSettings["login_go_page"], ht["Token"], ht["CustomerID"]);


        string goURL = string.Format("{0}{1}?tid={2}&cid={3}", "http://localhost:2330/",
        ConfigurationManager.AppSettings["login_go_page"], ht["Token"], ht["CustomerID"]);

        this.Response.Redirect(goURL);
    }

    protected void btnSend_Click(object sender, ImageClickEventArgs e)
    {
        if (string.IsNullOrEmpty(PUserName.Value))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(),
                "", "$(document).ready(function(){ alert('请输入姓名！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
                true);
            return;
        }
        if (string.IsNullOrEmpty(PCompany.Value))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(),
                "", "$(document).ready(function(){ alert('请输入公司名称！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
                true);
            return;
        }
        if (string.IsNullOrEmpty(PEmail.Value))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(),
                "", "$(document).ready(function(){ alert('请输入公司邮箱！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
                true);
            return;
        }
        if (string.IsNullOrEmpty(PTel.Value))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(),
                "", "$(document).ready(function(){ alert('请输入公司总机+分级！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
                true);
            return;
        }
        if (string.IsNullOrEmpty(PPhone.Value))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(),
                "", "$(document).ready(function(){ alert('请输入手机号！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
                true);
            return;
        }
        if (string.IsNullOrEmpty(PIndustry.Value))
        {
            this.ClientScript.RegisterStartupScript(this.GetType(),
                "", "$(document).ready(function(){ alert('请输入行业！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
                true);
            return;
        }

        var url = "http://180.153.154.21:9004/OnlineShopping/data/Data.aspx?action=setContact";
        url += string.Format("&reqContent={{\"common\":{{}},\"special\":{{\"userName\":\"{0}\", \"company\":\"{1}\", \"tel\":\"{2}\", \"email\":\"{3}\", \"phone\":\"{4}\", \"industry\":\"{5}\"}}}}",
            PUserName.Value.Trim(),
            PCompany.Value.Trim(),
            PTel.Value.Trim(),
            PEmail.Value.Trim(),
            PPhone.Value.Trim(),
            PIndustry.Value.Trim()
            );

        //var result = "200";
        var result = GetRemoteData(url, "GET", "");
        if (result.Contains("200"))
        {
            PUserName.Value = "";
            PCompany.Value = "";
            PTel.Value = "";
            PEmail.Value = "";
            PPhone.Value = "";
            PIndustry.Value = "";
            this.ClientScript.RegisterStartupScript(this.GetType(),
                "", "$(document).ready(function(){ alert('发送完成！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
                true);
        }
        else
        {
            hMsg.Value = result;
            this.ClientScript.RegisterStartupScript(this.GetType(),
                "", "$(document).ready(function(){ alert('发送失败！'); InputKeyDownAll(); document.getElementById('btnSend').focus(); });",
                true);
        }
    }

    #region GetRemoteData
    public static string GetRemoteData(string uri, string method, string content)
    {
        string respData = "";
        method = method.ToUpper();
        HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
        req.KeepAlive = false;
        req.Method = method.ToUpper();
        req.Credentials = System.Net.CredentialCache.DefaultCredentials;
        ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
        if (method == "POST")
        {
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            req.ContentLength = buffer.Length;
            //req.ContentType = "text/json";
            Stream postStream = req.GetRequestStream();
            postStream.Write(buffer, 0, buffer.Length);
            postStream.Close();
        }
        HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
        Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
        StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);
        respData = loResponseStream.ReadToEnd();
        loResponseStream.Close();
        resp.Close();
        return respData;
    }

    internal class AcceptAllCertificatePolicy : System.Net.ICertificatePolicy
    {
        public AcceptAllCertificatePolicy()
        { }

        public bool CheckValidationResult(ServicePoint sPoint,
            System.Security.Cryptography.X509Certificates.X509Certificate cert,
            WebRequest wRequest, int certProb)
        {
            return true;
        }
    }
    #endregion

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (Request.Cookies["cpos_sso_remember"]!=null)
        {
            string cpos_remember = Request.Cookies["cpos_sso_remember"].Value;
            if (cpos_remember == "1")
            {
                if (Request.Cookies["cpos_sso_pwd"] != null)
                {
                    //string cpos_pwd = Request.Cookies["cpos_sso_pwd"].Value;
                    //this.txtPassword.Attributes["value"] = cpos_pwd;
                    //this.hdPwd.Value = cpos_pwd;
                }
            }
        }
    }

    /// <summary>  
    /// 移除指定的Cookie  
    /// </summary>  
    /// <param name="cookieName"></param>  
    public static void Remove(string cookieName)
    {
        HttpContext.Current.Response.Cookies.Remove(HttpUtility.UrlEncode(cookieName));
        HttpContext.Current.Response.Cookies[HttpUtility.UrlEncode(cookieName)].Expires = DateTime.Now.AddDays(-1);
    }  
}