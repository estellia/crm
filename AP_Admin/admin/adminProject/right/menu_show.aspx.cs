using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class right_menu_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //界面默认进去，隐藏操作区域
        this.ClientScript.RegisterStartupScript(typeof(int), "hideOper", "<script>showRegion('imgOper', 'tabOper');</script>");
        if (!IsPostBack)
        {
            string menu_id = this.Request.QueryString["menu_id"];
            switch (this.Request.QueryString["oper_type"] ?? "1")
            { 
                case "1":
                    SetControlsText(GetMenuInfo(menu_id));
                    btnOK.Visible = false;
                    SetReadOnly(); 
                    break; 
                case "2":
                    CheckSaveStatus();
                    SetDefaultVal();
                    tbDisplayIndex.Focus();
                    break;
                case "3":
                    SetControlsText(GetMenuInfo(menu_id));
                    tbDisplayIndex.Focus();
                break;
                default:
                    btnOK.Visible = false;
                break;
            }
        }
    }

    //设置控件中的文本信息
    private void SetControlsText(cPos.Admin.Model.Right.MenuInfo mi) 
    {
        if (mi != null)
        {
            tbDisplayIndex.Text = mi.DisplayIndex.ToString();
            tbCode.Text = mi.Code;
            tbName.Text = mi.Name;
            tbEnglishName.Text = mi.EnglishName;
            tbURL.Text = mi.URLPath;
            tbStatus.Text = mi.StatusDescription;
            cbCustomerVisible.SelectedValue = mi.CustomerVisible.ToString();
            tbCreater.Text = mi.Creater.Name;
            tbCreateTime.Text = mi.CreateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            tbEditor.Text = mi.LastEditor.Name;
            tbEditTime.Text = mi.LastEditTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            txtRemark.Value = mi.Remark;
            if (mi.LastSystemModifyStamp > DateTime.Parse("2000-1-1"))
            {
                tbSysModifyTime.Text = mi.LastSystemModifyStamp.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            else
            {
                tbSysModifyTime.Text = string.Empty;
            }
        }
    }

    //将录入项转换成对象
    private cPos.Admin.Model.Right.MenuInfo GetItemFormInput()
    {
        cPos.Admin.Model.Right.MenuInfo mi = new cPos.Admin.Model.Right.MenuInfo();
        mi.DisplayIndex= int.Parse (tbDisplayIndex.Text);
        mi.Code = tbCode.Text.Trim();
        mi.Name=tbName.Text;
        mi.EnglishName=tbEnglishName.Text;
        mi.URLPath=tbURL.Text;
        mi.StatusDescription=tbStatus.Text;
        mi.CustomerVisible=int.Parse (cbCustomerVisible.SelectedValue);
        mi.Creater.Name=tbCreater.Text;
        //mi.CreateTime = DateTime.Parse (tbCreateTime.Text);
        mi.LastEditor.Name=tbEditor.Text;
        mi.Remark = txtRemark.Value;
        //mi.LastEditTime=DateTime.Parse ( tbEditTime.Text);
        //mi.LastSystemModifyStamp=DateTime.Parse (tbSysModifyTime.Text);
        mi.IconPath = "icon_" + mi.Code;//为了方便前端处理样式
        return mi;
    }

    private cPos.Admin.Model.Right.MenuInfo GetMenuInfo(string menu_id)
    {
        return this.GetRightService().GetMenuByID(menu_id);
        //return new cPos.Admin.Model.Right.MenuInfo
        //{
        //    ApplicationID = "sdf",
        //    Name = "sdfds",
        //};
    }

    //设置控件为只读
    private void SetReadOnly()
    {
        tbDisplayIndex.ReadOnly = true;
        tbCode.ReadOnly = true;
        tbName.ReadOnly = true;
        tbEnglishName.ReadOnly = true;
        tbURL.ReadOnly = true;
        cbCustomerVisible.Enabled = false;
    }

    private void CheckSaveStatus()
    {
        if (string.IsNullOrEmpty(this.Request.QueryString["app_id"]))
        {
            btnOK.Visible = false;
            btnReturn.Focus();
        }
    }
    private void SetDefaultVal()
    {
        tbStatus.Text = "正常";
        cbCustomerVisible.SelectedValue = "1";
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {    
        this.Response.Redirect(this.Request.QueryString["from"]??"~/right/menu_query.aspx");
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool isCreate = "2".Equals(this.Request.QueryString["oper_type"]);
        if (!CheckInput())
        {
            return ;
        }
        var mi = GetItemFormInput();
        try
        {
            if (isCreate)
            { 
                if (this.Request.QueryString["parent_menu_id"] != "--" && !string.IsNullOrEmpty(this.Request.QueryString["parent_menu_id"]))
                {
                    mi.ParentMenuID = this.Request.QueryString["parent_menu_id"];
                }
                mi.ApplicationID = this.Request.QueryString["app_id"];
                
                this.GetRightService().InsertMenu(this.LoggingSession, mi);
                this.Redirect("添加菜单成功", InfoType.Info, this.Request.QueryString["from"] ?? "~/right/menu_query.aspx");
            }
            else
            {
                mi.ID = this.Request.QueryString["menu_id"];
                mi.LastSystemModifyStamp = DateTime.Now;
                this.GetRightService().ModifyMenu(this.LoggingSession, mi);
                this.Redirect("修改菜单成功", InfoType.Info, this.Request.QueryString["from"] ?? "~/right/menu_query.aspx");
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //对录入项验证
    private bool CheckInput()
    {
        bool isCreate = "2".Equals(this.Request.QueryString["oper_type"]);

        //if (string.IsNullOrWhiteSpace(tbDisplayIndex.Text))
        //{
        //    this.InfoBox.ShowPopError("显示顺序不能为空");
        //    tbDisplayIndex.Focus();
        //    return false;
        //}
        //else
        //{
        //    if (!System.Text.RegularExpressions.Regex.IsMatch(tbDisplayIndex.Text ?? "", @"^\s*\d{1,4}\s*$"))
        //    {
        //        this.InfoBox.ShowPopError("显示顺序为 1-4 位数字!");
        //        tbDisplayIndex.Focus();
        //        return false;
        //    }
        //}

        //if (string.IsNullOrWhiteSpace(tbCode.Text))
        //{
        //    this.InfoBox.ShowPopError("编码不能为空");
        //    tbCode.Focus();
        //    return false;
        //}

        if (CheckCodeExist(isCreate ? null : this.Request.QueryString["menu_id"], tbCode.Text.Trim()))
        {
            this.InfoBox.ShowPopError("编码已存在，请更改！");
            tbCode.Focus();
            return false;
        }

        //if (string.IsNullOrWhiteSpace(tbName.Text))
        //{
        //    this.InfoBox.ShowPopError("名称不能为空");
        //    tbName.Focus();
        //    return false;
        //}
        return true;
    }

    //验证 code 是否已存在
    bool CheckCodeExist(string menuId,string code)
    { 
        return this.GetRightService().ExistMenuCode(menuId,code);
    } 
}