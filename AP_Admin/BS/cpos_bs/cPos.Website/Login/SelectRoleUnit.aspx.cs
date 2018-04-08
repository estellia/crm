using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;
using cPos.Service;
using cPos.Components;
using cPos.Model.User;

public partial class Login_SelectRoleUnit : PageBase
{
    public string GetDefaultName(int flag)
    {
        if (flag == 1)
            return "是";
        else
            return "否";

    }
    SessionManager sesManage = new SessionManager();
    cPos.Service.cUserService userService = new cUserService();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        //Response.Cache.SetNoStore();
        //检验用户是否合法
        if (UserInfo == null)
        {
            //Response.Redirect("LoginManagerError.aspx?ReturnPath=" + Request.Url.ToString());
            Response.Write("用户不合法");
            return;
        }
        if (!IsPostBack)
        {
            //得到该用户所对应的角色列表
            string userId = this.UserInfo.User_Id;
            string applicationId = ApplicationManager.GetApplicationId();



            IList<UserRoleInfo> userroleinfoList = userService.GetUserRoles(this.loggingSessionInfo, userId, applicationId);

            GridView1.DataSource = userroleinfoList;
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        cPos.Service.AppSysService appSysService = new AppSysService();
        cPos.Service.UnitService unitService = new UnitService();

        //设置用户角色
        //按角色登录
        if (e.CommandName == "Go")
        {
            this.UserRoleInfo = new UserRoleInfo();
            this.UserRoleInfo.RoleId = e.CommandArgument.ToString();

            this.UserRoleInfo.RoleName = appSysService.GetRoleById(loggingSessionInfo, this.UserRoleInfo.RoleId).Role_Name;

            this.UserRoleInfo.UserId = this.UserInfo.User_Id;
            this.UserRoleInfo.UserName = this.UserInfo.User_Name;

            try
            {
                this.UserRoleInfo.UnitId = userService.GetDefaultUnitByUserIdAndRoleId(this.UserRoleInfo.UserId, this.UserRoleInfo.RoleId);
            }
            catch(Exception ex)
            {
                PageLog.Current.Write(ex);
                Response.Write("找不到默认单位");
                Response.End();
            }

            try
            {
                
                this.UserRoleInfo.UnitName = unitService.GetUnitById(loggingSessionInfo, UserRoleInfo.UnitId).ShortName;
            }

            catch(Exception ex)
            {
                PageLog.Current.Write(ex);
                Response.Write("找不到单位");
                Response.End();

            }
            //将注册信息写入LoggingSessionInfo 2008-9-19
            this.loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo.CurrentUser = this.UserInfo;
            loggingSessionInfo.CurrentUserRole = this.UserRoleInfo;
            loggingSessionInfo.CurrentLanguageKindId = this.CurrentLanguageKindId;
            loggingSessionInfo.CurrentLoggingManager = this.LoggingManagerInfo;

            //判断有没有菜单权限
            IList<MenuModel> menus = appSysService.GetRoleMenus(loggingSessionInfo, e.CommandArgument.ToString());
            if (menus.Count == 0)
            {
                this.Label1.Text = "没有菜单" + "&nbsp;<a href=\"~/common/homepage.aspx\">返回首页</a>";
            }
            else
            {
                //转到主页
                if (Request["ReturnPath"] == null)
                {
                    Response.Redirect("~/common/homepage.aspx");
                }
                else
                {
                    Response.Redirect(Request["ReturnPath"].ToString());
                }
            }
        }

        //按单位登录
        //UserService.CheckUserRole
        if (e.CommandName == "UnitGo")
        {

            this.UserRoleInfo = new UserRoleInfo();
            this.UserRoleInfo.RoleId = e.CommandArgument.ToString();
            this.UserRoleInfo.RoleName = appSysService.GetRoleById(loggingSessionInfo, this.UserRoleInfo.RoleId).Role_Name;
            this.UserRoleInfo.UserId = this.UserInfo.User_Id;
            this.UserRoleInfo.UserName = this.UserInfo.User_Name;

            GridViewRow row = (e.CommandSource as LinkButton).NamingContainer as GridViewRow;
            string unitId = ((HiddenField)row.FindControl("hfUnitId")).Value;
            try
            {
                this.UserRoleInfo.UnitId = unitId;

            }
            catch(Exception ex)
            {
                PageLog.Current.Write(ex);
                Response.Write("找不到默认单位");
                Response.End();
            }
            try
            {
                this.UserRoleInfo.UnitName = unitService.GetUnitById(loggingSessionInfo, UserRoleInfo.UnitId).ShortName;
            }
            catch(Exception ex)
            { 
                PageLog.Current.Write(ex);
                this.UserRoleInfo.UnitName = "单位信息加载出错";
                //throw (ex);
            }

            //检查单位角色信息,没有,添加
            userService.CheckUserRole(this.UserRoleInfo);

            //将注册信息写入LoggingSessionInfo 2008-9-19
            this.loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo.CurrentUser = this.UserInfo;
            loggingSessionInfo.CurrentUserRole = this.UserRoleInfo;
            loggingSessionInfo.CurrentLanguageKindId = this.CurrentLanguageKindId;
            loggingSessionInfo.CurrentLoggingManager = this.LoggingManagerInfo;

            //判断有没有菜单权限
            IList<MenuModel> menus = appSysService.GetRoleMenus(loggingSessionInfo, e.CommandArgument.ToString());
            if (menus.Count == 0)
            {
                this.Label1.Text = "没有菜单";
            }
            else
            {
                //转到主页
                if (Request["ReturnPath"] == null)
                {
                    Response.Redirect("~/common/homepage.aspx");
                }
                else
                {
                    Response.Redirect(Request["ReturnPath"].ToString());
                }
            }

        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}