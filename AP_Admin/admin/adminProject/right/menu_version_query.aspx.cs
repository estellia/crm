using cPos.Admin.Model.Right;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class right_menu_version_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //说明，每次页面初始化都调用，否则注册的事件将不起作用。
        InitControls();
        if (!IsPostBack)
        {
            InitAppList();
            InitVersionList();//行业版本
            if (cbApp.SelectedValue != null)
            {
                var vocversion_id = cbVersion.SelectedValue;
                var app_id = cbApp.SelectedValue as string;
                var status = cbStatus.SelectedValue;

                ShowTree(app_id);
                if (tvMenu.SelectedNode != null)
                {
                    ShowMenuList(app_id, tvMenu.SelectedNode.Value, vocversion_id, status);
                }
                else
                {
                    ShowMenuList(app_id, null, vocversion_id, status);
                }
            }
            cbApp.Focus();
        }
    }

    //初始化控件,处理方法订阅事件
    private void InitControls()
    {
        tvMenu.SelectedNodeChanged += new EventHandler(tvMenu_SelectedNodeChanged);
        cbApp.AutoPostBack = true;
        cbApp.SelectedIndexChanged += new EventHandler(cbApp_SelectedIndexChanged);
        cbVersion.AutoPostBack = true;
        cbVersion.SelectedIndexChanged += new EventHandler(cbVersion_SelectedIndexChanged);
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
            if (cbApp.SelectedIndex < 0 && cbApp.Items.Count > 0)
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

    /// <summary>
    /// 初始化下拉行业版本列表
    /// </summary>
    private void InitVersionList()
    {
        try
        {
            cbVersion.DataSource = this.GetRightService().GetAllVersionList();
            cbVersion.DataValueField = "ID";
            cbVersion.DataTextField = "Name";
            cbVersion.DataBind();

            //状态还原
            cbVersion.SelectedValue = this.Request.QueryString["app_id"];
            if (cbVersion.SelectedIndex < 0 && cbVersion.Items.Count > 0)
            {
                cbVersion.SelectedIndex = 0;
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
            IEnumerable<cPos.Admin.Model.Right.MenuInfo> menus = this.GetRightService().GetAllMenuListByAppID(app_id).OrderBy(obj => obj.Level).ThenBy(obj => obj.DisplayIndex);

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
        foreach (var item in menus.Where(obj => obj.ParentMenuID == parent_id))
        {
            var tv = new TreeNode()
            {
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
    private void ShowMenuList(string app_id, string parent_id)
    {
        if (parent_id == null || parent_id == "--")
        {
            gvMenu.DataSource = this.GetRightService().GetFirstLevelMenuListByAppID(app_id).OrderBy(obj => obj.DisplayIndex).ToArray();
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

    /// <summary>
    /// 按版本加载显示列表信息
    /// </summary>
    /// <param name="app_id"></param>
    /// <param name="parent_id"></param>
    private void ShowMenuList(string app_id, string parent_id, string vocversion_id, string status)
    {
        if (parent_id == null || parent_id == "--")
        {
            gvMenu.DataSource = this.GetRightService().GetFirstLevelMenuListByAppIDAndVersion(app_id, vocversion_id).OrderBy(obj => obj.DisplayIndex).ToArray();
            gvMenu.DataBind();
        }
        else
        {
            gvMenu.DataSource = this.GetRightService().GetSubMenuListByAppVersionAndMenuID(parent_id, vocversion_id, app_id);
            gvMenu.DataBind();
        }
    }
    /// <summary>
    /// 在单击 GridView 控件中的按钮时，将引发 RowCommand 事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvMenu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        var vocversion_id = cbVersion.SelectedValue;
        var app_id = cbApp.SelectedValue as string;
        var status = cbStatus.SelectedValue;
        switch (e.CommandName)
        {
            case "-1"://停用
                {
                    string startRuselt = this.GetRightService().SetVersionMenuStatus(LoggingSession, e.CommandArgument.ToString(), vocversion_id, app_id, "1");
                    if (startRuselt == "状态修改成功.")
                    {
                        this.InfoBox.ShowPopInfo("启用成功！");
                    }
                    else
                    {
                        this.InfoBox.ShowPopError(startRuselt);
                    }
                }
                break;
            case "1"://正常
                {
                    string stopRuselt = this.GetRightService().SetVersionMenuStatus(LoggingSession, e.CommandArgument.ToString(), vocversion_id, app_id, "-1");
                    if (stopRuselt == "状态修改成功.")
                    {
                        this.InfoBox.ShowPopInfo("停用成功！");
                    }
                    else
                    {
                        this.InfoBox.ShowPopError(stopRuselt);
                    }
                }
                break;
            case "SetIsCanAccess"://设置操作权限
                {
                    //获取行业版本对应菜单的关系ID
                    Guid mappingId = new Guid(e.CommandArgument.ToString());
                    //根据ID获取行业版本对应的菜单信息
                    MenuInfo mappingInfo = this.GetRightService().GetVocationVersionMenuMappingByID(mappingId);
                    if (mappingInfo != null)
                    {
                        if (mappingInfo.IsCanAccess == 0)
                            this.GetRightService().UpdateIsCanAccess(1, mappingId);//设置为可操作
                        else
                            this.GetRightService().UpdateIsCanAccess(0, mappingId);//设置为不可操作
                    }
                }
                break;
            default:
                return;
        }
        //显示菜单权限
        if (tvMenu.SelectedNode != null)
        {
            ShowMenuList(app_id, tvMenu.SelectedNode.Value, vocversion_id, status);
        }
        else
        {
            ShowMenuList(app_id, null, vocversion_id, status);
        }
    }

    #region 事件

    //下拉框事件
    void cbApp_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbVersion_SelectedIndexChanged(sender, e);
    }

    void cbVersion_SelectedIndexChanged(object sender, EventArgs e)
    {
        var vocversion_id = cbVersion.SelectedValue;
        var app_id = cbApp.SelectedValue as string;
        var status = cbStatus.SelectedValue;

        ShowTree(app_id);
        if (tvMenu.SelectedNode != null)
        {
            ShowMenuList(app_id, tvMenu.SelectedNode.Value, vocversion_id, status);
        }
        else
        {
            ShowMenuList(app_id, null, vocversion_id, status);
        }
    }

    //菜单树选中事件
    void tvMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        var vocversion_id = cbVersion.SelectedValue;
        var app_id = cbApp.SelectedValue as string;
        var status = cbStatus.SelectedValue;
        if (tvMenu.SelectedNode != null)
        {
            ShowMenuList(app_id, tvMenu.SelectedNode.Value, vocversion_id, status);
        }
        else
        {
            ShowMenuList(app_id, null, vocversion_id, status);
        }
    }
    #endregion

    //生成 From Url 参数
    protected override void OnPreRender(EventArgs e)
    {
        //生成 _from 隐藏域字段
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?and=");
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
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
            if (e.Row.FindControl("lbControl") == null)
                return;
            var button = (ImageButton)e.Row.FindControl("lbControl");
            var obj = (cPos.Admin.Model.Right.MenuInfo)e.Row.DataItem;
            if (obj.Status.Equals(-1))
            {
                button.ImageUrl = "~/img/enable.png";
            }
            else
            {
                button.ImageUrl = "~/img/disable.png";
            }
            //停用和启动菜单操作权限图标控制
            if (e.Row.FindControl("lbIsCanAccess") == null)
                return;
            var lbIsCanAccess = (ImageButton)e.Row.FindControl("lbIsCanAccess");
            var objIsCanAccess = (cPos.Admin.Model.Right.MenuInfo)e.Row.DataItem;
            if (objIsCanAccess.IsCanAccess.Equals(0))
            {
                lbIsCanAccess.ImageUrl = "~/img/online.png";
            }
            else
            {
                lbIsCanAccess.ImageUrl = "~/img/offline.png";
            }

        }
    }

    //protected void btnQuery_Click(object sender, EventArgs e)
    //{
    //    var vocversion_id = cbVersion.SelectedValue;
    //    var app_id = cbApp.SelectedValue as string;
    //    var status = cbStatus.SelectedValue;

    //    ShowTree(app_id);//不分版本
    //    if (tvMenu.SelectedNode != null)
    //    {
    //        ShowMenuList(app_id, tvMenu.SelectedNode.Value, vocversion_id, status);
    //    }
    //    else
    //    {
    //        ShowMenuList(app_id, null, vocversion_id, status);
    //    }

    //}

    /// <summary>
    /// 同步行业版本菜单和可操作权限
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSync_Click(object sender, EventArgs e)
    {
        var vocversion_id = cbVersion.SelectedValue;
        var app_id = cbApp.SelectedValue as string;

        loggingSessionInfo.CurrentLoggingManager.User_Id = LoggingSession.UserID;
        loggingSessionInfo.CurrentLoggingManager.User_Name = LoggingSession.UserName;

        var strResult = this.GetRightService().SyncCustomerVersionMenu(loggingSessionInfo, vocversion_id, app_id);

        this.InfoBox.ShowPopInfo(strResult);
    }
}