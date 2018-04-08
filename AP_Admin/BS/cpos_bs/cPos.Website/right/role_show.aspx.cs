using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;

public partial class right_role_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = this.Request.QueryString["strDo"] ?? "Create";
            ViewState["action"] = action;  
            LoadAppSysInfo();
            switch (action)
            {
                case "Create":
                    { tbRoleCode.Enabled = true ; RoleBar.InnerText="角色新建"; ShowCreatePage(); } break;
                case "Modify":
                    { tbRoleCode.ReadOnly = false ;RoleBar.InnerText="角色修改"; ShowModifyPage(); } break;
                case "Visible":
                    { RoleBar.InnerText="角色查看";tvMenu.Enabled= false;ShowVisiablePage(); } break;
                default:
                    this.btSave.Visible = false; return;
            }
        }

    }

    protected void drpAppSys_SelectedIndexChanged(object sender, EventArgs e)
    {
        //createTree(drpAppSys.SelectedValue);
        //显示菜单树 
        ShowTree(drpAppSys.SelectedValue);
    }

    private void ShowTree(string appId)
    {
        //显示菜单树 
        var menus = (new cPos.Service.cAppSysServices()).GetAllMenusByAppSysCode(loggingSessionInfo,GetAppCodeByAppID(appId));
        CreateTree(menus);
    }

    private void createTree(string appId)
    {
        tvMenu.Nodes.Clear();
        var menus = (new cAppSysServices()).GetMainMenusByAppSysId(loggingSessionInfo, appId);
        foreach (var menu in menus)
        {
            IEnumerable<string> select = new string[0];
            TreeNode tn = new TreeNode();
            tn.Text = menu.Menu_Name;
            tn.Value = menu.Menu_Id;
            tn.ShowCheckBox = true;
            tn.Checked = select.Contains(menu.Menu_Id);
            tn.Expanded = true;

            var submenus = new cAppSysServices().GetSubMenus(loggingSessionInfo, menu.Menu_Id);
            foreach (var submenu in submenus)
            {
                IEnumerable<string> select2 = new string[0];
                TreeNode tn2 = new TreeNode();
                tn2.Text = submenu.Menu_Name;
                tn2.Value = submenu.Menu_Id;
                tn2.ShowCheckBox = true;
                tn2.Checked = select2.Contains(submenu.Menu_Id);
                tn2.Expanded = true;
                tn.ChildNodes.Add(tn2);
            }
            
            tvMenu.Nodes.Add(tn);
        }
    }


    //加载应用系统下拉列表
    private void LoadAppSysInfo()
    {
        try
        {
            var BrigeSource = new cAppSysServices().GetAllAppSyses(loggingSessionInfo);
            var bindSource = BrigeSource.Select(obj => new { Def_App_Name = obj.Def_App_Name, Def_App_Id=obj.Def_App_Id }).ToList();
            //bindSource.Insert(0, new { Def_App_Name ="请选择",Def_App_Id="0"});
            this.drpAppSys.DataTextField = "Def_App_Name";
            this.drpAppSys.DataValueField = "Def_App_Id";
            drpAppSys.DataSource = bindSource;
            if (bindSource.Count > 0)
            {
                drpAppSys.SelectedIndex = 0;
            }
            drpAppSys.DataBind(); 
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //显示详细页面函数
    private void ShowVisiablePage()
    {
        //新建按钮可不用
        btSave.Visible = false;
        //文本框是只读的
        DisableAllInput(this);
        //根据id显示页面
        var RoleId = Request.QueryString["role_id"].ToString();
        if (RoleId == null)
        {
            return;
        }
        else
        {
            //调用方法获取数据
            LoadInfo(RoleId); 
        }
    }
     
    //显示修改页面函数 先加载数据
    private void ShowModifyPage()
    {
        btSave.Visible = true;
        string RoleId = Request.QueryString["role_id"].ToString();
        if (RoleId == null)
        {
            return;
        }
        else
        {
           // drpAppSys.Enabled = false;
            LoadInfo(RoleId);
        }
    }
    //显示新建页面函数
    private void ShowCreatePage()
    {
        drpAppSys.Enabled = true;
        btSave.Visible = true;
        drpAppSys.SelectedValue = this.Request.QueryString["appId"];
        if (drpAppSys.Items.Count > 0 && drpAppSys.SelectedIndex < 0)
        {
            drpAppSys.SelectedIndex = 0;
        }
        ShowTree(drpAppSys.SelectedValue);
    }
    //所有input为只读的
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
    protected void LoadInfo(string roleid)
    {
        var item = new AppSysService().GetRoleById(loggingSessionInfo, roleid);
        drpAppSys.SelectedValue = item.Def_App_Id;
        tbRoleCode.Text = item.Role_Code;
        tbRoleName.Text = item.Role_Name;
        tbRoleNameEn.Text = item.Role_Eng_Name;
        if (item.Is_Sys == 1)
        {
            tbIsSys1.Checked = true;
            tbIsSys2.Checked = false;
            tbIsSys1.Enabled = false;
            tbIsSys2.Enabled = false;
            drpAppSys.Enabled = false;
            tbRoleCode.Enabled = false;
        }
        else
        {
            tbIsSys2.Checked = true;
            tbIsSys1.Checked = false;
        }
        //显示菜单树
        var select_menuids = (new AppSysService()).GetRoleMenus(loggingSessionInfo, roleid).Select(obj => obj.Menu_Id).ToArray();
        var menus = (new cPos.Service.cAppSysServices()).GetAllMenusByAppSysCode(loggingSessionInfo,GetAppCodeByAppID(item.Def_App_Id));
        CreateTree(menus);
       
        ShowUnitCheckedNode(select_menuids); 
    }
    // 从UI 得到 数据对象
    private RoleModel GetModelFromUI()
    {
        var item = new AppSysService().GetRoleById(loggingSessionInfo, Request.QueryString["role_id"]);
        if (item != null)
        {
            item.Role_Code = tbRoleCode.Text.Trim();
            item.Role_Name = tbRoleName.Text.Trim();
            item.Role_Eng_Name = tbRoleNameEn.Text.Trim();
            item.Def_App_Id = drpAppSys.SelectedValue;
            item.Is_Sys = tbIsSys1.Checked ? 1 : 0;
            var menu_list = new List<RoleMenuModel>();

            GetSelectetMenus(menu_list, this.tvMenu.Nodes);
            item.RoleMenuInfoList = menu_list;
            item.Modify_Time = new BaseService().GetCurrentDateTime();
            item.Modify_User_id = loggingSessionInfo.CurrentUser.User_Id;
            item.Modify_User_Name = loggingSessionInfo.CurrentUser.User_Name;
        }
        else
        {
            item = new RoleModel();
            item.Role_Code = tbRoleCode.Text.Trim();
            item.Role_Name = tbRoleName.Text.Trim();
            item.Role_Eng_Name = tbRoleNameEn.Text.Trim();
            item.Def_App_Id = drpAppSys.SelectedValue;
            item.Is_Sys = tbIsSys1.Checked ? 1 : 0;
            var menu_list = new List<RoleMenuModel>();

            GetSelectetMenus(menu_list, this.tvMenu.Nodes);
            item.RoleMenuInfoList = menu_list;
            item.Create_Time = new BaseService().GetCurrentDateTime();
            item.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            item.Create_User_Name = loggingSessionInfo.CurrentUser.User_Name;
        }
        return item;
    }
    private void GetSelectetMenus(List<RoleMenuModel> source,TreeNodeCollection nodes)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Checked)
            {
                var str = node.Text;
                source.Add(new RoleMenuModel { Menu_Id = node.Value});
            }
            if (node.ChildNodes.Count != 0)
            {
                GetSelectetMenus(source, node.ChildNodes);
            }
        }
    }
    private IEnumerable<string> GetSelectMenuIds(IEnumerable<TreeNode> nodes)
    { 
        foreach(TreeNode tn in nodes)
        {
            if(tn.Checked)
            {
                yield return tn.Value;
            }
            foreach (var sub in GetSelectMenuIds(tn.ChildNodes.OfType < TreeNode>()))
            {
                yield return sub;
            }
        }
    }

    //返回应用的 code
    private string GetAppCodeByAppID(string appId)
    {
        return
        (new cPos.Service.cAppSysServices()).GetAllAppSyses(loggingSessionInfo).Where(obj => obj.Def_App_Id == appId)
            .Select(obj => obj.Def_App_Code).FirstOrDefault();
    }
    #region test
    //选中节点
    private void CheckedTreeNode(TreeNodeCollection treeNodes, string checkedId)
    {
        foreach (TreeNode node in treeNodes)
        {
            if (node.Value == checkedId)
            {
                node.Checked = true;
                var parentNode = node.Parent;
                if (parentNode is TreeNode)
                {
                    parentNode.Expanded = true;
                }
            }
            if (node.ChildNodes.Count != 0)
                CheckedTreeNode(node.ChildNodes, checkedId);
        }
    }
    private void ShowUnitCheckedNode(string[] iList)
    {
        foreach (var item in iList)
        {
            CheckedTreeNode(this.tvMenu.Nodes, item);
        }
    }
    #endregion
    private void CreateTree(IEnumerable<MenuModel> menus)
    {
        //显示菜单树
        tvMenu.Nodes.Clear();
        foreach (var item in CreateNodeByParentId(menus, 1))
        {
            item.Expanded = true;
            tvMenu.Nodes.Add(item);
        }
    }

    private IEnumerable<TreeNode> CreateNodeByParentId(IEnumerable<MenuModel> source, int menu_level)
    {
        foreach (var item in source.Where(obj => obj.Menu_Level == menu_level))
        {
            TreeNode tn = new TreeNode();
            tn.Text = item.Menu_Name;
            tn.Value = item.Menu_Id;
            tn.ShowCheckBox = true;
            foreach (var child in CreateSubNodeByMenuId(source, new string[0], item.Menu_Id))
            {
                tn.ChildNodes.Add(child);
            }
            yield return tn;
        }
    }
    //创建树
    private IEnumerable<TreeNode> CreateSubNodeByMenuId(IEnumerable<MenuModel> source, IEnumerable<string> select, string menu_id)
    {
        foreach (var item in source.Where(obj => obj.Parent_Menu_Id == menu_id))
        {
            TreeNode tn = new TreeNode();
            tn.Text = item.Menu_Name;
            tn.Value = item.Menu_Id;
            tn.ShowCheckBox = true ;
            tn.Checked = select.Contains(item.Menu_Id);
            foreach (var child in CreateSubNodeByMenuId(source, select, item.Menu_Id))
            {
                tn.ChildNodes.Add(child);
            }
            yield return tn;
        }
    }


    protected void tbIsSys_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        try
        {
            //菜单树的修改
            //此处和文档不一致文档返回布尔值但是此处返回的是string
            var role = GetModelFromUI();
            var saveResult = new RoleService().SetRoleInfo(loggingSessionInfo,role);
            if (saveResult == "保存成功.")
            {
                this.Redirect("保存成功！", InfoType.Info, this.Request.QueryString["from"] ?? "role_query.aspx");
            }
            this.InfoBox.ShowPopError(saveResult);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("保存失败！");
        }
    }
}