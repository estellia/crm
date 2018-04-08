using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Components;
using cPos.Model;
using cPos.Service;
using System.Collections;
using cPos.Model.User;
using cPos.WebServices.AuthManagerWebServices;
using System.Configuration;

public partial class Login_LoginManager : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {  
        if (!IsPostBack)
        {
            string customer_id = Request["cid"];
            string token = Request["tid"];

            #region 正式发布时，请删除

            if (string.IsNullOrEmpty(customer_id))
            {
                customer_id = ConfigurationManager.AppSettings["test_customer_id"].ToString();
            }

            if (string.IsNullOrEmpty(token))
            {
                token = ConfigurationManager.AppSettings["test_token"].ToString();
            }

            #endregion

            this.loadUser(customer_id, token);
        }       
    }

    private void loadUser(string customer_id, string token)
    {
        try
        {
            //获取登录管理平台的用户信息
            AuthService AuthWebService = new AuthService();
            //设置地址
            AuthWebService.Url = ConfigurationManager.AppSettings["sso_url"].ToString() + "/AuthService.asmx";
            string str = AuthWebService.GetLoginUserInfo(token);

            cPos.Model.LoggingManager myLoggingManager = (cPos.Model.LoggingManager)cXMLService.Deserialize(str, typeof(cPos.Model.LoggingManager));

            //判断登录进来的用户是否存在,并且返回用户信息
            cPos.Service.cUserService userService = new cUserService();
            LoggingSessionInfo loggingSession = new LoggingSessionInfo();
            loggingSession.CurrentLoggingManager = myLoggingManager;
            if (!userService.IsExistUser(myLoggingManager))
            {
                this.lbErr.Text = "用户不存在,请与管理员联系";
                return;
            }
            cPos.Model.User.UserInfo login_user = userService.GetUserById(loggingSession, myLoggingManager.User_Id);
            loggingSession.CurrentUser = login_user;

            //SessionManager sm = new SessionManager();
            //sm.UserInfo = login_user;
            //sm.LoggingManager = myLoggingManager;
            //sm.loggingSessionInfo = loggingSession;

            this.Session["UserInfo"] = login_user;
            this.Session["LoggingManager"] = myLoggingManager;
            this.Session["loggingSessionInfo"] = loggingSession;

            //保存Cookie
            //HttpCookie cookie = new HttpCookie("DRP");
            //cookie.Values.Add("userid", login_user.User_Id);
            //cookie.Values.Add("username", login_user.User_Name);
            //cookie.Values.Add("languageid", ddlLanguage.SelectedItem.Value);
            //cookie.Expires = DateTime.Now.AddDays(7);
            //Response.AppendCookie(cookie);

            //清空密码
            login_user.User_Password = null;
            string go_url = "~/login/SelectRoleUnit.aspx?p=0";
            this.Response.Redirect(go_url);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            lbErr.Text = "登录失败:"+ ex.ToString();
        }
    }
}