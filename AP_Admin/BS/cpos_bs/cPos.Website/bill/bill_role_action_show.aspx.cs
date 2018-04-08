using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;

public partial class bill_bill_role_action_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadBillData();
            loadRoleData();
            if (!string.IsNullOrEmpty(this.Request.QueryString["BillKindID"]))
            {
                this.cbBillKind.SelectedValue = this.Request.QueryString["BillKindID"];
            }
            else
            {
                this.cbBillKind.SelectedIndex = 0;
            }
            if (!string.IsNullOrEmpty(this.Request.QueryString["cbRole"]))
            {
                this.cbRole.SelectedValue = this.Request.QueryString["cbRole"];
            }
            else
            {
                this.cbRole.SelectedIndex = 0;
            }
            this.cbRole.Focus();
            loadBillAction();
            loadBillStatus();
            loadBillMoney();
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        btn.Enabled = false;
        checkInput();
        btn.Enabled = true;
    }


    private void loadBillData()
    {
        try
        {
            var bills = new cBillService().GetAllBillKinds(loggingSessionInfo);
            // bills.Insert(0, new BillKindModel { Id = "-1", Description = "请选择" });
            this.cbBillKind.DataSource = bills;
            this.cbBillKind.DataValueField = "Id";
            this.cbBillKind.DataTextField = "Description";
            this.cbBillKind.DataBind();
            //ListItem lis = new ListItem();
            //lis.Text = "全部";
            //lis.Value = "";
            //this.cbBillKind.Items.Insert(0, lis);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }
    private void loadBillAction()
    {
        try
        {
            var billsAction = new cBillService().GetAllBillActionsByBillKindId(loggingSessionInfo, this.cbBillKind.SelectedValue);
            //billsAction.Insert(0, new BillActionModel { Id = "-1", Description = "请选择" });
            this.cbBillAction.DataSource = billsAction;
            this.cbBillAction.DataValueField = "Id";
            this.cbBillAction.DataTextField = "Description";
            this.cbBillAction.DataBind();
            //ListItem lis = new ListItem();
            //lis.Text = "全部";
            //lis.Value = "";
            //this.cbRole.Items.Insert(0, lis);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }
    private void loadBillStatus()
    {
        try
        {
            var billsAction = new cBillService().GetAllBillStatusesByBillKindID(loggingSessionInfo, this.cbBillKind.SelectedValue);
            this.cbPrevBillStatus.DataSource = billsAction;
            this.cbPrevBillStatus.DataValueField = "Status";
            this.cbPrevBillStatus.DataTextField = "Description";
            this.cbPrevBillStatus.DataBind();
            this.cbCurrBillStatus.DataSource = billsAction;
            this.cbCurrBillStatus.DataValueField = "Status";
            this.cbCurrBillStatus.DataTextField = "Description";
            this.cbCurrBillStatus.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }
    private void loadBillMoney()
    {
        try
        {
            BillKindModel billsAction = new cBillService().GetBillKindById(loggingSessionInfo, this.cbBillKind.SelectedValue);
            if (billsAction.MoneyFlag == 1)
            {
                this.tbMaxMoney.Enabled = true;
                this.tbMinMoney.Enabled = true;
            }
            else
            {
                this.tbMinMoney.Text = "0";
                this.tbMaxMoney.Text = "0";
                //this.tbMaxMoney.ReadOnly = true;
                this.tbMaxMoney.Enabled = false;
                this.tbMinMoney.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败！");
        }
    }
    private void loadRoleData()
    {
        try
        {
            var roles = new RoleService().GetAllRoles(loggingSessionInfo);
            //var bindSource = roles.Select(obj => new { Role_Name = obj.Role_Name, Role_Id = obj.Role_Id }).ToList();
            //bindSource.Insert(0, new { Role_Name = "请选择", Role_Id = "0" });
            //roles.Insert(0, new RoleModel { Role_Id = "-1", Role_Name = "请选择" });
            this.cbRole.DataSource = roles;
            this.cbRole.DataValueField = "Role_Id";
            this.cbRole.DataTextField = "Role_Name";
            this.cbRole.DataBind();
            //ListItem lis = new ListItem();
            //lis.Text = "全部";
            //lis.Value = "";
            //this.cbRole.Items.Insert(0, lis);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据失败");
        }
    }
    protected void cbBillKind_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBillAction();
        loadBillStatus();
        loadBillMoney();
    }

    private void checkInput()
    {
        if (this.tbMinMoney.ReadOnly == false && this.tbMaxMoney.ReadOnly == false)
        {
            int min;
            int max;
            if (int.TryParse(this.tbMinMoney.Text, out min))
            {
                if (int.TryParse(this.tbMaxMoney.Text, out max))
                {
                    if (min > max)
                        this.InfoBox.ShowPopError("最小金额应小于最大金额");
                    else if (min < 0)
                    {
                        this.InfoBox.ShowPopError("最小金额不能小于0");
                        this.tbMinMoney.Focus();
                    }
                    else if (max < 0)
                    {
                        this.InfoBox.ShowPopError("最大金额不能小于0");
                        this.tbMaxMoney.Focus();
                    }
                    else
                        if (checkRepeatBillARData())
                            saveBillActionRoleData(currBillActionRoleData());
                }
                else
                {
                    this.InfoBox.ShowPopError("最大金额应为整数!");
                    this.tbMaxMoney.Focus();
                }
            }
            else
            {
                this.InfoBox.ShowPopError("最小金额应为整数!");
                this.tbMinMoney.Focus();
            }
        }
        else
        {
            if (checkRepeatBillARData())
                saveBillActionRoleData(currBillActionRoleData());
        }
    }
    private bool checkRepeatBillARData()
    {
        var check = new cBillService().CheckAddBillActionRole(loggingSessionInfo, currBillActionRoleData());
        switch (check)
        {
            case BillActionRoleCheckState.Successful:
                {
                    return true;
                }
            case BillActionRoleCheckState.ExistModify:
                {
                    this.InfoBox.ShowPopError("修改已存在");
                    return false;
                }
            case BillActionRoleCheckState.ExistCreate:
                {
                    this.InfoBox.ShowPopError("新建已存在");
                    return false;
                }
            case BillActionRoleCheckState.ExistApprove:
                {
                    this.InfoBox.ShowPopError("审批已存在");
                    return false;
                }
            case BillActionRoleCheckState.ExistCancel:
                {
                    this.InfoBox.ShowPopError("删除已存在");
                    return false;
                }
            case BillActionRoleCheckState.ExistReject:
                {
                    this.InfoBox.ShowPopError("回退已存在");
                    return false;
                }
            case BillActionRoleCheckState.NotExistAction:
                {
                    this.InfoBox.ShowPopError("不存在的操作");
                    return false;
                }
            default: return false;
        }
    }
    private BillActionRoleModel currBillActionRoleData()
    {
        BillActionRoleModel billAcRoModel = new BillActionRoleModel();
        billAcRoModel.KindId = this.cbBillKind.SelectedValue;
        billAcRoModel.RoleId = this.cbRole.SelectedValue;
        billAcRoModel.ActionId = this.cbBillAction.SelectedValue;
        billAcRoModel.PreviousStatus = this.cbPrevBillStatus.SelectedValue;
        billAcRoModel.CurrentStatus = this.cbCurrBillStatus.SelectedValue;
        billAcRoModel.MinMoney = int.Parse(this.tbMinMoney.Text);
        billAcRoModel.MaxMoney = int.Parse(this.tbMaxMoney.Text);
        return billAcRoModel;
    }
    private void saveBillActionRoleData(BillActionRoleModel data)
    {
        bool save = new cBillService().InsertBillActionRole(loggingSessionInfo, data);
        if (save)
        {
            this.Redirect("保存成功", InfoType.Info, this.Request.QueryString["from"] ?? "bill_role_action_query.aspx");
            //this.InfoBox.ShowPopInfo("保存成功");
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>go_back();</script>");
        }
        else
            this.InfoBox.ShowPopError("保存失败");
    }
}