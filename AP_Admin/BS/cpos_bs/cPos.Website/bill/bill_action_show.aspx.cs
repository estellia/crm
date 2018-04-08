using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;

public partial class bill_bill_action_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadBillData();
            this.cbBillKind.SelectedValue = this.Request.QueryString["BillKindID"] ?? "";
            cbBillKind.Focus();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        btn.Enabled = false;
        if(checkInput())
            saveBillStatusData(currBillActionData());
        btn.Enabled = true;
    }
    //检查输入选项
    private bool checkInput()
    {
        if (this.cbCreateFlag.SelectedIndex == 0)
        {
            if (cbModifyFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"修改标志\"必须为\"否\"");
                this.cbModifyFlag.Focus();
                return false;
            }
            if (cbApproveFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"审核标志\"必须为\"否\"");
                this.cbApproveFlag.Focus();
                return false;
            }
            if (cbRejectFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"退回标志\"必须为否");
                this.cbRejectFlag.Focus();
                return false;
            }
            if (cbCancelFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"删除标志\"必须为否");
                this.cbCancelFlag.Focus();
                return false;
            }
        }
        else if (cbModifyFlag.SelectedIndex == 0)
        {
            if (cbCreateFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"新建标志\"必须为\"否\"");
                this.cbCreateFlag.Focus();
                return false;
            }
            if (cbApproveFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"审核标志\"必须为\"否\"");
                this.cbApproveFlag.Focus();
                return false;
            }
            if (cbRejectFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"退回标志\"必须为否");
                this.cbRejectFlag.Focus();
                return false;
            }
            if (cbCancelFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"删除标志\"必须为否");
                this.cbCancelFlag.Focus();
                return false;
            }
        }
        else if (cbApproveFlag.SelectedIndex == 0)
        {
            if (cbCreateFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"新建标志\"必须为\"否\"");
                this.cbCreateFlag.Focus();
                return false;
            }
            if (cbModifyFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"修改标志\"必须为\"否\"");
                this.cbModifyFlag.Focus();
                return false;
            }
            if (cbRejectFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"退回标志\"必须为否");
                this.cbRejectFlag.Focus();
                return false;
            }
            if (cbCancelFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"删除标志\"必须为否");
                this.cbCancelFlag.Focus();
                return false;
            }
        }
        else if (cbRejectFlag.SelectedIndex == 0)
        {
            if (cbCreateFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"新建标志\"必须为\"否\"");
                this.cbCreateFlag.Focus();
                return false;
            }
            if (cbModifyFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"修改标志\"必须为\"否\"");
                this.cbModifyFlag.Focus();
                return false;
            }
            if (cbApproveFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"审核标志\"必须为否");
                this.cbApproveFlag.Focus();
                return false;
            }
            if (cbCancelFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"删除标志\"必须为否");
                this.cbCancelFlag.Focus();
                return false;
            }
        }
        else if (cbCancelFlag.SelectedIndex == 0)
        {
            if (cbCreateFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"新建标志\"必须为\"否\"");
                this.cbCreateFlag.Focus();
                return false;
            }
            if (cbModifyFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"修改标志\"必须为\"否\"");
                this.cbModifyFlag.Focus();
                return false;
            }
            if (cbApproveFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"审核标志\"必须为否");
                this.cbApproveFlag.Focus();
                return false;
            }
            if (cbRejectFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"退回标志\"必须为否");
                this.cbRejectFlag.Focus();
                return false;
            }
        }
        var checkAction = new cBillService().CheckAddBillAction(loggingSessionInfo, currBillActionData());
        switch (checkAction)
        {
            case BillActionCheckState.Successful:
                {
                    return true; ;
                }
            case BillActionCheckState.ExistCode:
                {
                    this.InfoBox.ShowPopError("编码已存在");
                    this.tbCode.Focus();
                    return false;
                }
            case BillActionCheckState.ExistCreateAction:
                {
                    this.InfoBox.ShowPopError("新建标志已存在");
                    this.cbCreateFlag.Focus();
                    return false;
                }
            case BillActionCheckState.ExistApproveAction:
                {
                    this.InfoBox.ShowPopError("审核标志已存在");
                    this.cbApproveFlag.Focus();
                    return false;
                }
            case BillActionCheckState.ExistModifyAction:
                {
                    this.InfoBox.ShowPopError("修改标志已存在");
                    this.cbModifyFlag.Focus();
                    return false;
                }
            case BillActionCheckState.ExistRejectAction:
                {
                    this.InfoBox.ShowPopError("退回标志已存在");
                    this.cbRejectFlag.Focus();
                    return false;
                }
            case BillActionCheckState.ExistCancelAction:
                {
                    this.InfoBox.ShowPopError("删除标志已存在");
                    this.cbCancelFlag.Focus();
                    return false;
                }
            default: return false;
        }
    }

    //当前表单状态的数据
    private BillActionModel currBillActionData()
    {
        BillActionModel billActModel = new BillActionModel();
        billActModel.KindId = this.cbBillKind.SelectedValue;
        billActModel.Code = this.tbCode.Text;
        billActModel.Description = this.tbDescription.Text;
        billActModel.CreateFlag = int.Parse(this.cbCreateFlag.SelectedValue);
        billActModel.ApproveFlag = int.Parse(this.cbApproveFlag.SelectedValue);
        billActModel.ModifyFlag = int.Parse(this.cbModifyFlag.SelectedValue);
        billActModel.RejectFlag = int.Parse(this.cbRejectFlag.SelectedValue);
        billActModel.CancelFlag = int.Parse(this.cbCancelFlag.SelectedValue);
        billActModel.display_index = int.Parse(this.txtDisplayIndex.Text.Trim());
        return billActModel;
    }
    //保存表单状态数据。
    private void saveBillStatusData(BillActionModel data)
    {
        bool save = new cBillService().InsertBillAction(loggingSessionInfo, data);
        if (save)
        {
            this.Redirect("保存成功", InfoType.Info, this.Request.QueryString["from"] ?? "bill_action_query.aspx");


        }
        else
            this.InfoBox.ShowPopError("保存失败");
    }

    private void loadBillData()
    {
        try
        {
            var bills = new cBillService().GetAllBillKinds(loggingSessionInfo);
            this.cbBillKind.DataSource = bills;
            this.cbBillKind.DataValueField = "Id";
            this.cbBillKind.DataTextField = "Description";
            this.cbBillKind.DataBind();
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
}