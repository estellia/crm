using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using System.Web.Script.Serialization;
using cPos.Model.User;

public partial class right_user_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strDo = this.Request.QueryString["strDo"];
            ViewState["user_id"] = this.Request.QueryString["user_id"];
            ViewState["strDo"] = strDo;
            LoadAppSysInfo();
            LoadUnitsInfo();
            switch (strDo)
            {
                case "Visible": { this.btnSave.Visible = false; rd1.Disabled = true; rd2.Disabled = true; drpUnit.ReadOnly = true; LoadBasicData(); DisableAllInput(this); LoadRoleInfoList(); }; break;
                case "Create": { this.tbUserPwd.Text = ""; }
                    break;
                case "Modify": { this.btnSave.Visible = true; LoadBasicData(); LoadRoleInfoList(); }; break;
                default: this.btnSave.Visible = false; return;
            }
        }
        this.dropAppSys.SelectedIndex = 0;
    }
    protected UnitService UnitService
    {
        get
        {
            return new UnitService();
        }
    }
    protected cUserService UserService
    {
        get
        {
            return new cUserService();
        }
    }
    protected cAppSysServices AppSysService
    {
        get { return new cAppSysServices(); }
    }
    //禁用所有input
    private void DisableAllInput(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c is TextBox)
            {
                var temp = c as TextBox;
                temp.ReadOnly = true;
            }
            else if (c is DropDownList)
            {
                var temp = c as DropDownList;
                temp.Enabled = false;
            }
            else if (c is CheckBox)
            {
                var temp = c as CheckBox;
                temp.Enabled = false;
            }
            if (c.Controls.Count > 0)
                DisableAllInput(c);
        }
    }
    //角色数据
    protected string RoleInfoData
    {
        get
        {
            if (ViewState["roleData"] == null)
                ViewState["roleData"] = "[]";
            return ViewState["roleData"] as string;
        }
        set
        {
            ViewState["roleData"] = value;
        }
    }
    //加载应用系统信息
    private void LoadAppSysInfo()
    {
        try
        {
            var source = this.AppSysService.GetAllAppSyses(loggingSessionInfo);
            var bindsource = source.Select(obj => new { Def_App_Id = obj.Def_App_Id, Def_App_Name = obj.Def_App_Name }).ToList();
            bindsource.Insert(0, new { Def_App_Id = "-1", Def_App_Name = "---请选择---" });
            //source.Insert(0, new AppSysModel { Def_App_Id = "", Def_App_Name = "---请选择---" });
            this.dropAppSys.DataTextField = "Def_App_Name";
            this.dropAppSys.DataValueField = "Def_App_Id";
            this.dropAppSys.DataSource = bindsource;
            this.dropAppSys.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    #region 加载组织信息
    //加载组织信息
    private void LoadUnitsInfo()
    {
        try
        {
            var service = new UnitService();
            var source = service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo);

            this.drpUnit.DataBind(source.Select(obj =>
                new controls_DropDownTree.tvNode
                {
                    CheckState = false,
                    Complete = false,
                    ShowCheck = false,
                    Text = obj.Name,
                    Value = obj.Id,
                }).ToArray());
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    #endregion
    //加载基本信息
    private void LoadBasicData()
    {
        try
        {
            var rult = this.UserService.GetUserById(loggingSessionInfo, this.Request.QueryString["user_id"]);
            if (rult == null)
            {
                this.btnSave.Visible = false;
            }
            tbUserName.Text = rult.User_Name ?? "";
            if (!string.IsNullOrEmpty(rult.User_Gender))
            {
                if (rult.User_Gender == "男")
                    tbUserGender.SelectedValue ="1";
                else if(rult.User_Gender == "女")
                    tbUserGender.SelectedValue = "-1";
            }
            tbUserGender.SelectedValue = rult.User_Gender ?? "";
            tbUserCode.Text = rult.User_Code ?? "";
            tbUserNameEn.Text = rult.User_Name_En ?? "";
            tbUserIdentity.Text = rult.User_Identity ?? "";
            tbUserBirthday.Value = rult.User_Birthday ?? "";
            //tbUserPwd.Text = rult.User_Password ?? "";
            tbCallPhone.Text = rult.User_Telephone ?? "";
            tbTelPhone.Text = rult.User_Cellphone ?? "";
            tbEmail.Text = rult.User_Email ?? "";
            tbMsn.Text = rult.MSN ?? "";
            tbQQ.Text = rult.QQ ?? "";
            tbBlog.Text = rult.Blog ?? "";
            tbAddress.Text = rult.User_Address ?? "";
            tbPostcode.Text = rult.User_Postcode ?? "";
            tbRemark.Text = rult.User_Remark ?? "";
            this.tbFailDate.Value = rult.Fail_Date??"";
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    private void LoadRoleInfoList()
    {
        try
        {
            var source = this.UserService.GetUserRoles(loggingSessionInfo, this.Request.QueryString["user_id"], null);
            this.RoleInfoData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(source);
            this.hidRoleInfo.Value = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(source);
            //this.repTable.DataSource = source;
            //this.repTable.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var userInfo = GetNewUserInfo();
            var userRoleInfos = this.GetFormView();
            string strError = string.Empty;
            bool rult = this.UserService.SetUserInfo(loggingSessionInfo, userInfo, userRoleInfos, out strError);
            if (rult)
            {
                this.Redirect("保存成功!", InfoType.Info, this.Request.QueryString["from"] ?? "user_query.aspx");
            }
            this.InfoBox.ShowPopError(strError);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("保存失败！");
            this.tabContainer1.Visible = true;
        }
    }
    //获取新建或更新后的数据
    private UserInfo GetNewUserInfo()
    {
        var userInfo = new UserInfo();
        userInfo.User_Name = tbUserName.Text;
        if (!string.IsNullOrEmpty(tbUserGender.SelectedValue))
        {
            if (tbUserGender.SelectedValue == "-1")
                userInfo.User_Gender = "女";
            else if (tbUserGender.SelectedValue == "1")
                userInfo.User_Gender = "男";
               
        }
        userInfo.User_Code = tbUserCode.Text;
        userInfo.User_Name_En = tbUserNameEn.Text;
        userInfo.User_Identity = tbUserIdentity.Text;

        userInfo.User_Birthday = tbUserBirthday.Value;
        userInfo.User_Password = tbUserPwd.Text;
        userInfo.User_Telephone = tbCallPhone.Text;
        userInfo.User_Cellphone = tbTelPhone.Text;
        userInfo.User_Email = tbEmail.Text;
        userInfo.MSN = tbMsn.Text;
        userInfo.QQ = tbQQ.Text;
        userInfo.Blog = tbBlog.Text;
        userInfo.User_Address = tbAddress.Text;
        userInfo.User_Postcode = tbPostcode.Text;
        userInfo.User_Remark = tbRemark.Text;
        userInfo.Fail_Date = tbFailDate.Value;
        if (ViewState["strDo"].ToString() == "Create")
        {
            userInfo.User_Id = null;
            userInfo.Create_Time = new BaseService().GetCurrentDateTime();
            userInfo.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            userInfo.Modify_Time = new BaseService().GetCurrentDateTime();
            userInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;

        }
        else
        {
            userInfo.User_Id = this.Request.QueryString["user_id"];
            userInfo.Modify_Time = new BaseService().GetCurrentDateTime();
            userInfo.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
        }
        return userInfo;
    }
    //获取更新后的组织信息
    private List<UserRoleInfo> GetFormView()
    {
        try
        {
            var obj = this.hidRoleInfo.Value;
            var json = new JavaScriptSerializer();
            object[] source = (object[])json.DeserializeObject(obj);
            List<UserRoleInfo> userRole = new List<UserRoleInfo>();
            for (int i = 0; i < source.Length; i++)
            {
                var info = new UserRoleInfo();
                var souceDic = (Dictionary<string, object>)source[i];
                foreach (var item in souceDic)
                {
                    if (item.Key == "UnitId")
                        info.UnitId = item.Value.ToString();
                    else if (item.Key == "RoleId")
                        info.RoleId = item.Value.ToString();
                    else if (item.Key == "DefaultFlag")
                        info.DefaultFlag = Convert.ToInt32(item.Value);
                    else if (item.Key == "Id")
                        info.Id = item.Value.ToString() == "" ? null : item.Value.ToString();
                }
                userRole.Add(info);
            }
            return userRole.ToList();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
            return null;
        }
    }
    protected void btnCancleClick(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.QueryString["_from"] ?? "user_query.aspx");
    }
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