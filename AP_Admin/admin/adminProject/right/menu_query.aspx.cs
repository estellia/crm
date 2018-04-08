using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class right_menu_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //说明，每次页面初始化都调用，否则注册的事件将不起作用。
        InitControls();
        if (!IsPostBack)
        {    
            InitAppList();
            if (cbApp.SelectedValue != null)
            {
                var app_id = cbApp.SelectedValue as string;
                ShowTree(app_id);
                if (tvMenu.SelectedNode != null)
                {
                    ShowMenuList(app_id, tvMenu.SelectedNode.Value);
                }
                else
                {
                    ShowMenuList(app_id, null);
                }
            }
            cbApp.Focus();
        }
    }

    //初始化控件,处理方法订阅事件
    private void InitControls()
    { 
        tvMenu.SelectedNodeChanged+=new EventHandler(tvMenu_SelectedNodeChanged);
        cbApp.AutoPostBack = true;
        cbApp.SelectedIndexChanged += new EventHandler(cbApp_SelectedIndexChanged);
    }
     
    //初始化下拉应用列表
    private void InitAppList()
    {
        try
        {
            cbApp.DataSource = this.GetRightService().GetAllAppList();
            cbApp.DataValueField = "ID";
            cbApp.DataTextField = "Name"; 
            cbApp.DataBind();

            //状态还原
            cbApp.SelectedValue = this.Request.QueryString["app_id"];
            if (cbApp.SelectedIndex<0&&cbApp.Items.Count>0)
            {
                cbApp.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }

    //加载显示菜单树
    private void ShowTree(string app_id)
    {
        try
        { 
            IEnumerable<cPos.Admin.Model.Right.MenuInfo> menus = this.GetRightService().GetAllMenuListByAppID(app_id).OrderBy(obj=>obj.Level).ThenBy(obj=>obj.DisplayIndex);
             
            tvMenu.Nodes.Clear();
            var root = new TreeNode
            {
                Text = "所有菜单",
                Value = "--",
            }; 
            CreateChildrens(menus, "--").Select(obj => { root.ChildNodes.Add(obj); return 0; }).ToArray();
            tvMenu.Nodes.Add(root);

            if (string.IsNullOrEmpty(this.Request.QueryString["parent_menu_id"]) || this.Request.QueryString["parent_menu_id"] == "--")
            {
                root.Selected = true;
            }

            if (tvMenu.SelectedNode == null)
            {
                root.Selected = true;
            }

            root.Expanded = true;

            //展开选中树的结点
            var select = tvMenu.SelectedNode;
            while (select != null && select.Parent != null)
            {
                select.Parent.Expanded = true;
                select = select.Parent;
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }

  
    //根据父菜单id，创建子菜单treenode
    private IEnumerable<TreeNode> CreateChildrens(IEnumerable<cPos.Admin.Model.Right.MenuInfo> menus, string parent_id)
    { 
        foreach(var item in menus.Where (obj=>obj.ParentMenuID == parent_id))
        {
            var tv = new TreeNode() {
                Text = item.Name,
                Value = item.ID,
                Expanded = false,
            };
            if (this.Request.QueryString["parent_menu_id"] == item.ID)
            {
                tv.Selected = true;
            } 
            CreateChildrens(menus, item.ID).Select(obj => { tv.ChildNodes.Add(obj); return 0; }).ToArray();
            yield return tv;
        }
    }
    
    //加载显示列表信息
    private void ShowMenuList(string app_id,string parent_id)
    {
        if (parent_id == null || parent_id == "--")
        {
            gvMenu.DataSource = this.GetRightService().GetFirstLevelMenuListByAppID(app_id).OrderBy(obj=>obj.DisplayIndex).ToArray ();
            //gvMenu.DataSource = this.GetRightService().GetAllMenuListByAppID(app_id).Where(obj => obj.ParentMenuID == "--").OrderBy(obj => obj.DisplayIndex).ToArray();
            gvMenu.DataBind();      
        }
        else
        {
            gvMenu.DataSource = this.GetRightService().GetSubMenuListByMenuID(parent_id);
            //gvMenu.DataSource = this.GetRightService().GetAllMenuListByAppID(app_id).Where(obj => obj.ParentMenuID == parent_id).OrderBy(obj => obj.DisplayIndex).ToArray();
            gvMenu.DataBind();
        }  
    }
    
    protected void gvMenu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Page":
                return;
            case "Submit":
                string customer_id = e.CommandArgument.ToString();
                if (!this.GetCustomerService().PublishAppInfo(customer_id))
                {
                    //提交应用系统信息失败
                    return;
                }
                if (!this.GetCustomerService().PublishMenuInfo(customer_id))
                {
                    //提交菜单信息失败
                    return;
                }
                //提示提交信息成功
                return;
            default:
                return;
        }
    }

    #region 事件 

    //下拉框事件
    void cbApp_SelectedIndexChanged(object sender, EventArgs e)
    {
        var app_id = cbApp.SelectedValue as string;
        ShowTree(app_id);
        if (tvMenu.SelectedNode != null)
        {
            ShowMenuList(app_id, tvMenu.SelectedNode.Value);
        }
        else
        {
            ShowMenuList(app_id, null);
        }
    }

    //菜单树选中事件
    void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        var app_id = cbApp.SelectedValue as string;
        if (tvMenu.SelectedNode != null)
        {
            ShowMenuList(app_id, tvMenu.SelectedNode.Value);
        }
        else
        {
            ShowMenuList(app_id, null);
        }
    }
    #endregion

    //生成 From Url 参数
    protected override void OnPreRender(EventArgs e)
    { 
        //生成 _from 隐藏域字段
        System.Text.StringBuilder sb = new System.Text.StringBuilder ();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append ("?and=");
        if(!string.IsNullOrEmpty( this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        } 
        sb.Append("&app_id=" + this.Server.UrlEncode(cbApp.SelectedValue));
        sb.Append("&parent_menu_id=" + this.Server.UrlEncode(tvMenu.SelectedNode.Value));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
    }

    protected void Stop_Click(object sender, EventArgs e)
    {
        var id = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument;
        if (this.GetRightService().CanDisableMenu(id))
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "stop", "$(function(){stop_use('" + id + "')})", true);
        }
        else
        {
            this.InfoBox.ShowPopInfo("不能停用该菜单项");
        } 
    }
    protected void gvMenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var linkStop = (LinkButton)e.Row.FindControl("linkStop");
            if (linkStop == null)
                return;
            var obj = (cPos.Admin.Model.Right.MenuInfo)e.Row.DataItem;
            if (obj.Status.Equals(-1))
            {
                linkStop.Visible = false;
            }
            else
            {
                linkStop.Visible = true;
            }
        }
    }
}