using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Admin.Model.Right;
using cPos.Admin.Model.Customer;
using cPos.Admin.Component;
using cPos.Admin.Model.Base;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Text;

public partial class customer_customer_show : PageBase
{
    string DataDeployId = "";
    string UnitId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //界面默认进去，隐藏操作区域
            this.tbCode.Focus();
            this.ClientScript.RegisterStartupScript(typeof(int), "hideOper", "<script>showRegion('imgOper', 'tabOper');</script>");
            this.loadApp();
            int action = int.Parse(this.Request.QueryString["oper_type"] ?? "0");
            ViewState["action"] = action;
            switch (action)
            {
                case 1: { this.btnOK.Visible = false; loadCustomerInfo(); DisableAllInput(this); }; break;  //查看
                case 2: { loadDefalutValue(); } break;  //添加
                case 3: { loadCustomerInfo(); } break;//编辑
                default: this.btnOK.Visible = false; return;
            }
            //this.gvCustomer.DataBind();
            //LoadQueryByUrl();
            //this.QueryCondition = getCondition();

            this.QueryCondition = getCondition();
            this.gvCustomer.PageIndex = 0;
            this.gvCustomer.DataBind();

            var customerId = Request.QueryString["customer_id"];
            var customerService = this.GetCustomerService();
            Hashtable htDep = new Hashtable();
            htDep["IsDelete"] = "0";
            var depList = customerService.GetTDataDeployList(htDep, 1000, 0);
            var cusDepId = customerService.GetTDataDeployIdByCustomerId(customerId);

            this.cbCustomer.Items.Clear();
            this.cbCustomer.Items.Add(new ListItem("", ""));
            foreach (var depItem in depList)
            {
                var name = string.Format("{0}  ({1}, 客户数量：{2})",
                    //depItem.DataDeployName.PadRight(maxLen, Convert.ToChar(160)), 
                    depItem.DataDeployName,
                    depItem.DataDeployDesc,
                    depItem.CustomerCount);
                var cbItem = new ListItem(name, depItem.DataDeployId);
                this.cbCustomer.Items.Add(cbItem);
                if (depItem.DataDeployId == cusDepId)
                {
                    DataDeployId = depItem.DataDeployId;
                    cbItem.Selected = true;
                }
            }
            //this.GridView1.DataSource = depList;
            //this.GridView1.DataBind();
        }

        if (true)
        {
            var customerId = Request.QueryString["customer_id"];
            var customerService = this.GetCustomerService();
            Hashtable htDep = new Hashtable();
            htDep["IsDelete"] = "0";
            var depList = customerService.GetTDataDeployList(htDep, 1000, 0);
            var cusDepId = customerService.GetTDataDeployIdByCustomerId(customerId);
            foreach (var depItem in depList)
            {
                if (depItem.DataDeployId == cusDepId)
                {
                    DataDeployId = depItem.DataDeployId;
                }
            }
        }
        if (SessionManager.CurrentLoggingSession.unit_id != "")//如果当前的用户是运营商客户，那么不让其选择运营商
        {
            tabUnit.Visible = false;
        }
    }

    //加载应用系统节点
    private void loadApp()
    {
        this.tvMenu.Nodes.Clear();
        IList<AppInfo> app_lst = this.GetRightService().GetCustomerVisibleAppList();
        foreach (AppInfo app in app_lst)
        {
            TreeNode tnApp = new TreeNode(app.Name, app.ID);
            this.loadAppMenu(tnApp);
            tnApp.ShowCheckBox = false;
            tnApp.CollapseAll();
            this.tvMenu.Nodes.Add(tnApp);
        }
    }

    //加载应用系统下的第一层菜单节点
    private void loadAppMenu(TreeNode tnApp)
    {
        IList<MenuInfo> menu_lst = this.GetRightService().GetAllMenuListByAppID(tnApp.Value);
        for (int i = 0; i < menu_lst.Count; i++)
        {
            MenuInfo menu = menu_lst[i];
            if (menu.Level == 1)
            {
                TreeNode tnFirstLevelMenu = new TreeNode(menu.Name, menu.ID);
                tnFirstLevelMenu.ShowCheckBox = true;
                this.loadSubMenu(tnFirstLevelMenu, menu, menu_lst);
                tnApp.ChildNodes.Add(tnFirstLevelMenu);
            }
        }
    }
    //加载菜单的子菜单节点
    private void loadSubMenu(TreeNode tnMenu, MenuInfo menu, IList<MenuInfo> appMenus)
    {
        if (menu.Level == 3) return;

        for (int i = 0; i < appMenus.Count; i++)
        {
            if (appMenus[i].ParentMenuID.Equals(menu.ID))
            {
                TreeNode tnSubMenu = new TreeNode(appMenus[i].Name, appMenus[i].ID);
                tnSubMenu.ShowCheckBox = true;
                this.loadSubMenu(tnSubMenu, appMenus[i], appMenus);
                tnMenu.ChildNodes.Add(tnSubMenu);
            }
        }
    }
    //加载默认值
    private void loadDefalutValue()
    {
        this.tbStartDate.Value = DateTime.Now.Date.ToString("yyyy-MM-dd");
        this.tbStatus.Text = "正常";
        this.tbCreater.Text = SessionManager.CurrentLoggingSession.CustomerName;
        this.tbCreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        this.tbEditor.Text = SessionManager.CurrentLoggingSession.CustomerName;
        this.tbEditTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        // this.tbSysModifyTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }
    //加载客户信息
    private void loadCustomerInfo()
    {
        try
        {
            var customerService = this.GetCustomerService();
            var customerInfo = this.GetCustomerService().GetCustomerByID(this.Request.QueryString["customer_id"], true, true);
            if (customerInfo == null)
            {
                this.btnOK.Visible = false;
            }
            tbCode.Text = customerInfo.Code;
            ViewState["code"] = customerInfo.Code;
            tbname.Text = customerInfo.Name;
            tbEnglishName.Text = customerInfo.EnglishName;
            tbStartDate.Value = customerInfo.StartDate;
            tbAddress.Text = customerInfo.Address;
            tbPostcode.Text = customerInfo.PostCode;
            tbContacter.Text = customerInfo.Contacter;
            tbFax.Text = customerInfo.Fax;
            tbTel.Text = customerInfo.Tel;
            tbCell.Text = customerInfo.Cell;
            tbEmail.Text = customerInfo.Email;
            tbRemark.Text = customerInfo.Memo;
            //tbRemark.Text = customerInfo.;
            tbStatus.Text = customerInfo.StatusDescription;
            var connect = customerInfo.Connect;
            tbDBServer.Text = connect.DBServer;
            tbDBName.Text = connect.DBName;
            tbDBUser.Text = connect.DBUser;
            tbDBPwd.Text = connect.DBPassword;
            tbAccessURL.Text = connect.AccessURL;
            tbMaxShopCount.Text = connect.MaxShopCount.ToString();
            tbMaxTerminalCount.Text = connect.MaxTerminalCount.ToString();
            tbMaxUserCount.Text = connect.MaxUserCount.ToString();
            tbKeyFile.Text = connect.KeyFile;
            //cbIsALD.SelectedValue = customerInfo.IsALD.ToString();
            UnitId = customerInfo.UnitId.ToString();

            this.QueryCondition = getCondition();
            this.gvCustomer.PageIndex = 0;
            this.gvCustomer.DataBind();

            var menuIds = customerInfo.Menus.Select(obj => obj.Menu.ID).ToArray();
            EnumerTreeNode(tvMenu.Nodes.OfType<TreeNode>())
                .Where(obj => menuIds.Contains(obj.Value)).Select(obj => { obj.Checked = true; if (obj.Parent is TreeNode) { obj.Parent.Expanded = true; } return 0; }).ToArray();

            tbCreater.Text = customerInfo.Creater.Name;
            tbCreateTime.Text = customerInfo.CreateTime == null ? "" : customerInfo.CreateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            tbEditor.Text = customerInfo.LastEditor.Name;
            tbEditTime.Text = customerInfo.LastEditTime == null ? "" : customerInfo.LastEditTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
			tbUnits.Text = customerInfo.Units==-1?"不限制": customerInfo.Units.ToString();

			this.tbSysModifyTime.Text = "";

        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }

    private IEnumerable<TreeNode> EnumerTreeNode(IEnumerable<TreeNode> tns)
    {
        foreach (var item in tns)
        {
            yield return item;
            foreach (var sub in EnumerTreeNode(item.ChildNodes.OfType<TreeNode>()))
            {
                yield return sub;
            }
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.QueryString["from"] ?? "customer_query.aspx");
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        var result = CheckInput();
        if (result == false)
        {
            return;
        }
        var customerInfo = getCustomerInfo();
        if (customerInfo == null)
        {
            this.InfoBox.ShowPopError("输入的数据格式不正确");
            return;
        }
        if (Convert.ToInt32(ViewState["action"]) == 2)//新建
        {
            try
            {
                customerInfo.ID = "";
                this.tbDBServer.Focus();
                this.tbCreater.Focus();
                this.GetCustomerService().InsertCustomer(SessionManager.CurrentLoggingSession, customerInfo);
                this.Redirect("新建客户成功", InfoType.Info, this.Request.QueryString["from"] ?? "customer_query.aspx");
            }
            catch (Exception ex)
            {
                PageLog.Current.Write(ex);
                this.InfoBox.ShowPopError("新建失败：" + ex.ToString());
            }
        }
        else if (Convert.ToInt32(ViewState["action"]) == 3)//修改
        {
            try
            {
                customerInfo.ID = this.Request.QueryString["customer_id"];
                this.GetCustomerService().ModifyCustomer(SessionManager.CurrentLoggingSession, customerInfo);
                this.Redirect("修改客户成功", InfoType.Info, this.Request.QueryString["from"] ?? "customer_query.aspx");
            }
            catch (Exception ex)
            {
                PageLog.Current.Write(ex);
                this.InfoBox.ShowPopError("修改失败：" + ex.ToString());
            }
        }
    }
    //加载客户数据
    private CustomerInfo getCustomerInfo()
    {
        try
        {
            var customerService = this.GetCustomerService();
            var customerInfo = new CustomerInfo();
            customerInfo.Code = tbCode.Text;
            customerInfo.Name = tbname.Text;
            customerInfo.EnglishName = tbEnglishName.Text;
            customerInfo.StartDate = tbStartDate.Value;
            customerInfo.Address = tbAddress.Text;
            customerInfo.PostCode = tbPostcode.Text;
            customerInfo.Contacter = tbContacter.Text;
            customerInfo.Fax = tbFax.Text;
            customerInfo.Tel = tbTel.Text;
            customerInfo.Cell = tbCell.Text;
            customerInfo.Email = tbEmail.Text;
            customerInfo.Memo = tbRemark.Text;
            customerInfo.StatusDescription = tbStatus.Text;
			if (tbUnits.Text.Trim().Length > 0 && isNumberic(tbUnits.Text.Trim())) {
					customerInfo.Units = Convert.ToInt32(tbUnits.Text.Trim());
			}
			else
				customerInfo.Units = -1;
			//customerInfo.IsALD = Convert.ToInt32(cbIsALD.SelectedValue);
			//customerInfo.DataDeployId = cbCustomer.SelectedValue;
			var depIds = GetSelectedIds();
            var depIdList = depIds.Split(',');
            if (depIdList != null && depIdList.Length > 1)
            {
                this.InfoBox.ShowPopError("只能选择一个连接信息!");
                return null;
            }
            if (depIdList != null && depIdList.Length == 1)
            {
                customerInfo.DataDeployId = depIdList[0];
            }

            var tmpDepQuery = new Hashtable();
            tmpDepQuery["DataDeployId"] = customerInfo.DataDeployId;
            var depItemList = customerService.GetTDataDeployList(tmpDepQuery, 1, 0);//为什么还要这一步，前面取的已经是数据库配置信息了。
           //再次判断数据库里是否有这个链接
            if (depItemList == null || depItemList.Count == 0)
            {
                this.InfoBox.ShowPopError("请选择连接信息!");
                return null;
            }
            var depItemObj = depItemList[0];//从数据库里获取完整的配置信息


            var connection = new CustomerConnectInfo();
            connection.Customer.ID = customerInfo.ID;
            connection.AccessURL = depItemObj.access_url;//从完整的数据库配置信息里，取数据。
            connection.DBName = depItemObj.db_name;
            connection.DBPassword = depItemObj.db_pwd;
            connection.DBServer = depItemObj.db_server;
            connection.DBUser = depItemObj.db_user;
            connection.KeyFile = depItemObj.key_file;
            connection.MaxShopCount = depItemObj.max_shop_count;
            connection.MaxTerminalCount = depItemObj.max_terminal_count;
            connection.MaxUserCount = depItemObj.max_user_count;
            customerInfo.Connect = connection;
            #region 菜单信息
            List<CustomerMenuInfo> menus = new List<CustomerMenuInfo>();
            foreach (TreeNode item in tvMenu.CheckedNodes)
            {
                menus.Add(new CustomerMenuInfo { Menu = new MenuInfo { ID = item.Value } });
            }
            customerInfo.Menus = menus.Count == 0 ? null : menus;
            #endregion
            #region 操作信息
            if (Convert.ToInt32(ViewState["action"]) == 2)
            {
                var creater = new UserOperateInfo();
                creater.Name = tbCreater.Text;
                customerInfo.Creater = creater;
                customerInfo.CreateTime = Convert.ToDateTime(tbCreateTime.Text);
                customerInfo.Status = 1;//状态正常
            }
            else
            {
                customerInfo.ID = this.Request.QueryString["customer_id"];
                var editor = new UserOperateInfo();
                editor.Name = SessionManager.CurrentLoggingSession.CustomerName;//修改人用当前登录人信息
                customerInfo.LastEditor = editor;
                customerInfo.LastEditTime = DateTime.Now;
            }
            #endregion
            //添加UnitID ，运营商ID
            if (SessionManager.CurrentLoggingSession.unit_id != "")//如果当前的用户是运营商客户，那么不让其选择运营商
            {
                customerInfo.UnitId = SessionManager.CurrentLoggingSession.unit_id;
            }
            else
            {
                var unitIds = GetUnitSelectedIds();
                if (unitIds != null)
                {
                    var unitIdsList = unitIds.Split(',');
                    if (unitIdsList != null && unitIdsList.Length > 1)
                    {
                        this.InfoBox.ShowPopError("只能选择一个运营商!");
                        return null;
                    }
                    if (unitIdsList != null && unitIdsList.Length == 1)
                    {
                        customerInfo.UnitId = unitIdsList[0];
                    }
                }
            }
            //这里要根据登录账户本身带不带unitId，如果带，就用账户自身的unitId，如果不带就用上面的id

            return customerInfo;
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错!");
            return null;
        }
    }
    //检查客户输入
    private bool CheckInput()
    {
        if (CheckCodeExists())
        {
            this.InfoBox.ShowPopError("编码已存在");
            this.tbCode.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(tbname.Text))
        {
            this.InfoBox.ShowPopError("客户名不能为空");
            this.tbname.Focus();
            return false;
        }
        if (Convert.ToInt32(ViewState["action"]) == 2)
        {
            DateTime start = new DateTime();
            if (!DateTime.TryParse(tbStartDate.Value, out start) || start < DateTime.Now.Date)
            {
                this.InfoBox.ShowPopError("输入的时间格式不正确");
                tbStartDate.Focus();
                return false;
            }
        }
        return true;
    }
    private bool CheckCodeExists()
    {
        if (Convert.ToInt32(ViewState["action"]) == 2)
        {
            return this.GetCustomerService().ExistCustomerCode(this.Request.QueryString["customer_id"], this.tbCode.Text);
        }
        else
        {
            if (!this.tbCode.Text.Trim().Equals(ViewState["code"].ToString()))
            {
                return this.GetCustomerService().ExistCustomerCode(this.Request.QueryString["customer_id"], this.tbCode.Text);
            }
            return false;
        }
    }

    //禁用所有input
    private void DisableAllInput(Control parent)
    {
        this.tbStartDate.Disabled = true;
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
            else if (c is TreeView)
            {
                var temp = c as TreeView;
                temp.Enabled = false;
            }
            if (c.Controls.Count > 0)
                DisableAllInput(c);
        }
    }

    protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string customer_id = "";
            switch (e.CommandName)
            {
                case "Page":
                    return;
                case "Submit":
                    customer_id = e.CommandArgument.ToString();
                    if (!this.GetCustomerService().PublishAppInfo(customer_id))
                    {
                        this.InfoBox.ShowPopError("提交应用系统信息失败");
                        return;
                    }
                    if (!this.GetCustomerService().PublishMenuInfo(customer_id))
                    {
                        this.InfoBox.ShowPopError("提交菜单信息失败");
                        return;
                    }
                    this.InfoBox.ShowPopInfo("提交成功");
                    return;
                case "SubmitInit":
                    customer_id = e.CommandArgument.ToString();
                    //if (!this.GetCustomerService().PublishAppInfo(customer_id))
                    //{
                    //    this.InfoBox.ShowPopError("提交应用系统信息失败");
                    //    return;
                    //}
                    //if (!this.GetCustomerService().PublishMenuInfo(customer_id))
                    //{
                    //    this.InfoBox.ShowPopError("提交菜单信息失败");
                    //    return;
                    //}
                    var error = string.Empty;
                    this.GetCustomerService().SetBSSystemStart(customer_id, out error);
                    this.InfoBox.ShowPopInfo(error);
                    return;
                default:
                    return;
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("访问数据出错:" + ex.ToString());
        }
    }

    protected void odsCustomer_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = this.GetCustomerService();
    }

    private Hashtable getCondition()
    {
        Hashtable ht = new Hashtable();
        //if (!string.IsNullOrEmpty(this.tbCode.Text.Trim()))
        //    ht.Add("Code", this.tbCode.Text.Trim());
        //if (!string.IsNullOrEmpty(this.tbName.Text.Trim()))
        //    ht.Add("Name", this.tbName.Text.Trim());
        //if (!string.IsNullOrEmpty(this.tbContacter.Text.Trim()))
        //    ht.Add("Contacter", this.tbContacter.Text.Trim());
        //if (this.cbStatus.SelectedIndex > 0)
        //    ht.Add("Status", this.cbStatus.SelectedValue);
        ht.Add("Status", "1");
        ht.Add("IsDelete", 0);
        return ht;
    }

    // 获取或设置当前查询条件
    private Hashtable QueryCondition
    {
        get
        {
            if (this.ViewState["QueryCondition"] as Hashtable == null)
            {
                this.ViewState["QueryCondition"] = getCondition();
            }
            return this.ViewState["QueryCondition"] as Hashtable;
        }
        set
        {
            this.ViewState["QueryCondition"] = value;
        }
    }

    //生成From Url 隐藏字段
    protected override void OnPreRender(EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?");
        if (!string.IsNullOrEmpty(this.Request.QueryString["customer_id"]))
        {
            sb.Append("&customer_id=" + Server.UrlEncode(this.Request.QueryString["customer_id"]));
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {

            sb.Append("&cur_menu_id=" + this.Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        //sb.Append("&code=" + Server.UrlEncode(this.tbCode.Text));
        //sb.Append("&name=" + Server.UrlEncode(this.tbName.Text));
        //sb.Append("&contarter=" + Server.UrlEncode(this.tbContacter.Text));
        //sb.Append("&status=" + Server.UrlEncode(this.cbStatus.SelectedValue));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
    protected void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        //if (!string.IsNullOrEmpty(qs["code"]))
        //{
        //    tbCode.Text = qs["code"].ToString();
        //}
        //if (!string.IsNullOrEmpty(qs["name"]))
        //{
        //    tbName.Text = qs["name"].ToString();
        //}
        //if (!string.IsNullOrEmpty(qs["contarter"]))
        //{
        //    tbContacter.Text = qs["contarter"].ToString();
        //}
        //if (!string.IsNullOrEmpty(qs["status"]))
        //{
        //    cbStatus.SelectedIndex = Convert.ToInt32(qs["status"]);
        //}

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.QueryCondition = getCondition();
        this.gvCustomer.PageIndex = 0;
        this.gvCustomer.DataBind();
    }

    private string GetSelectedIds()
    {
        string selId = null;
        for (int i = 0; i < this.gvCustomer.Rows.Count; i++)
        {
            var ctrl = (CheckBox)this.gvCustomer.Rows[i].FindControl("select");
            if (ctrl != null && ctrl.Checked)
            {
                selId += ctrl.Attributes["DataDeployId"] + ",";
            }
        }
        if (selId == null)
            return "";
        return selId.Trim(',');
    }
    protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (DataDeployId == null || DataDeployId.Length == 0)
        {
            var customerId = Request.QueryString["customer_id"];
            var customerService = this.GetCustomerService();
            Hashtable htDep = new Hashtable();
            htDep["IsDelete"] = "0";
            var depList = customerService.GetTDataDeployList(htDep, 1000, 0);
            var cusDepId = customerService.GetTDataDeployIdByCustomerId(customerId);
            foreach (var depItem in depList)
            {
                if (depItem.DataDeployId == cusDepId)
                {
                    DataDeployId = depItem.DataDeployId;
                }
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var ctrl = (CheckBox)e.Row.FindControl("select");
            if (ctrl.Attributes["DataDeployId"] == DataDeployId)
                ctrl.Checked = true;
        }
    }
    protected void odsCustomer_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    e.Cancel = true;
        //}
        //else
        //{
        e.InputParameters.Clear();
        e.InputParameters.Add("condition", this.QueryCondition.Clone());
        //}
    }

    //运营商相关
    protected void odsUnit_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    {
        e.ObjectInstance = this.GetUnitService();
    }
    protected void odsUnit_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    e.Cancel = true;
        //}
        //else
        //{
        e.InputParameters.Clear();

        Hashtable t=(Hashtable)this.QueryCondition.Clone();
         t.Add("type_code", "代理商");
        e.InputParameters.Add("condition",t );//只显示代理商的
        //}
    }

    protected void gvUnit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (DataDeployId == null || DataDeployId.Length == 0)
        //{
        //    var customerId = Request.QueryString["customer_id"];
        //    var customerService = this.GetCustomerService();
        //    Hashtable htDep = new Hashtable();
        //    htDep["IsDelete"] = "0";
        //    var depList = customerService.GetTDataDeployList(htDep, 1000, 0);
        //    var cusDepId = customerService.GetTDataDeployIdByCustomerId(customerId);
        //    foreach (var depItem in depList)
        //    {
        //        if (depItem.DataDeployId == cusDepId)
        //        {
        //            DataDeployId = depItem.DataDeployId;
        //        }
        //    }
        //}

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var ctrl = (CheckBox)e.Row.FindControl("select");
            if (ctrl.Attributes["UnitId"] == UnitId)
                ctrl.Checked = true;
        }
    }

    protected void gvUnit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //try
        //{
        //    string customer_id = "";
        //    switch (e.CommandName)
        //    {
        //        case "Page":
        //            return;
        //        case "Submit":
        //            customer_id = e.CommandArgument.ToString();
        //            if (!this.GetCustomerService().PublishAppInfo(customer_id))
        //            {
        //                this.InfoBox.ShowPopError("提交应用系统信息失败");
        //                return;
        //            }
        //            if (!this.GetCustomerService().PublishMenuInfo(customer_id))
        //            {
        //                this.InfoBox.ShowPopError("提交菜单信息失败");
        //                return;
        //            }
        //            this.InfoBox.ShowPopInfo("提交成功");
        //            return;
        //        case "SubmitInit":
        //            customer_id = e.CommandArgument.ToString();

        //            var error = string.Empty;
        //            this.GetCustomerService().SetBSSystemStart(customer_id, out error);
        //            this.InfoBox.ShowPopInfo(error);
        //            return;
        //        default:
        //            return;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    PageLog.Current.Write(ex);
        //    this.InfoBox.ShowPopError("访问数据出错:" + ex.ToString());
        //}
    }

    private string GetUnitSelectedIds()
    {
        string selId = null;
        for (int i = 0; i < this.gvUnit.Rows.Count; i++)
        {
            var ctrl = (CheckBox)this.gvUnit.Rows[i].FindControl("select");
            if (ctrl != null && ctrl.Checked)
            {
                selId += ctrl.Attributes["UnitId"] + ",";
            }
        }
        if (selId == null)
            return "";
        return selId.Trim(',');
    }
	/// <summary>
	/// 判断是否是数字
	/// </summary>
	/// <param name="message"></param>
	/// <param name="result"></param>
	/// <returns></returns>
	protected bool isNumberic(string message) {
		System.Text.RegularExpressions.Regex rex =
		new System.Text.RegularExpressions.Regex(@"^\d+$");
		if (rex.IsMatch(message)) {
			return true;
		}
		else
			return false;
	}

}