using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;

public partial class bill_bill_status_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadBillData();
            this.cbBillKind.SelectedValue = this.Request.QueryString["BillKindId"] ?? "";
            cbBillKind.Focus();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        btn.Enabled = false;
        if (checkInput())
            saveBillStatusData(currBillStatusData());
        btn.Enabled = true;
    }
    //检查输入选项
    private bool checkInput()
    {
        int result;
        if (cbBeginFlag.SelectedIndex == 0)
        {
            if (cbEndFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"结束标志\"必须为\"否\"");
                this.cbEndFlag.Focus();
                return false;
            }
            if (cbDeleteFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"删除标志\"必须为\"否\"");
                this.cbDeleteFlag.Focus();
                return false;
            }
            if (tbCustomerFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"自定义标志\"必须为否");
                this.tbCustomerFlag.Focus();
                return false;
            }
        }
        else if (cbEndFlag.SelectedIndex == 0)
        {
            if (cbBeginFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"起始标志\"必须为\"否\"");
                this.cbBeginFlag.Focus();
                return false;
            }
            if (cbDeleteFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"删除标志\"必须为\"否\"");
                this.cbDeleteFlag.Focus();
                return false;
            }
            if (tbCustomerFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"自定义标志\"必须为否");
                this.tbCustomerFlag.Focus();
                return false;
            }
        }
        else if (cbDeleteFlag.SelectedIndex == 0)
        {
            if (cbBeginFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"起始标志\"必须为\"否\"");
                this.cbBeginFlag.Focus();
                return false;
            }
            if (cbEndFlag.SelectedIndex != 1)
            {
                this.InfoBox.ShowPopError("\"结束标志\"必须为\"否\"");
                this.cbEndFlag.Focus();
                return false;
            }
            if (tbCustomerFlag.Text.Trim() != "0")
            {
                this.InfoBox.ShowPopError("\"自定义标志\"必须为否");
                this.tbCustomerFlag.Focus();
                return false;
            }
        }
        else
        {
            if (int.TryParse(tbCustomerFlag.Text.Trim(), out result))
            {
                if (tbCustomerFlag.Text.IndexOf(".") > -1)
                {
                    this.InfoBox.ShowPopError("请输入整数");
                    this.tbCustomerFlag.Focus();
                    return false;
                }
                else if (result < 1)
                {
                    this.InfoBox.ShowPopError("\"自定义标志\"必须大于0");
                    this.tbCustomerFlag.Focus();
                    return false;
                }
            }
            else
            {
                this.InfoBox.ShowPopError("请输入整数");
                this.tbCustomerFlag.Focus();
                return false;
            }

        }
        var checkStatus = new cBillService().CheckAddBillStatus(loggingSessionInfo, currBillStatusData());
        switch (checkStatus)
        {
            case BillStatusCheckState.Successful:
                {
                    return true;
                }
            case BillStatusCheckState.ExistBillStatus:
                {
                    this.InfoBox.ShowPopError("状态已存在");
                    this.tbStatus.Focus();
                    return false;
                }
            case BillStatusCheckState.ExistBegin:
                {
                    this.InfoBox.ShowPopError("起始标志已存在");
                    this.cbBeginFlag.Focus();
                    return false;
                }
            case BillStatusCheckState.ExistEnd:
                {
                    this.InfoBox.ShowPopError("结束标志已存在");
                    this.cbEndFlag.Focus();
                    return false;
                }
            case BillStatusCheckState.ExistDelete:
                {
                    this.InfoBox.ShowPopError("删除标志已存在");
                    this.cbDeleteFlag.Focus();
                    return false;
                }
            case BillStatusCheckState.ExistCustom:
                {
                    this.InfoBox.ShowPopError("自定义标志已存在");
                    this.tbCustomerFlag.Focus();
                    return false;
                }
            default: return false;
        }
    }

    //当前表单状态的数据
    private BillStatusModel currBillStatusData()
    {
        BillStatusModel billStatusModel = new BillStatusModel();
        billStatusModel.BillKindDescription = this.cbBillKind.SelectedItem.ToString();
        billStatusModel.KindId = this.cbBillKind.SelectedValue;
        billStatusModel.BeginFlag = int.Parse(this.cbBeginFlag.SelectedValue);
        billStatusModel.EndFlag = int.Parse(this.cbEndFlag.SelectedValue);
        billStatusModel.DeleteFlag = int.Parse(this.cbDeleteFlag.SelectedValue);
        billStatusModel.Status = this.tbStatus.Text;
        billStatusModel.Description = this.tbDescription.Text;
        billStatusModel.CustomFlag = int.Parse(this.tbCustomerFlag.Text);
        return billStatusModel;
    }
    //保存表单状态数据。
    private void saveBillStatusData(BillStatusModel data)
    {
        bool save = new cBillService().InsertBillStatus(loggingSessionInfo, data);
        if (save)
        {
            this.Redirect("保存成功", InfoType.Info, this.Request.QueryString["from"] ?? "bill_status_query.aspx");
            //this.InfoBox.ShowPopInfo("保存成功");
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>go_back();</script>");
        }
        else
            this.InfoBox.ShowPopError("保存失败");
    }

    private void loadBillData()
    {
        var bills = new cBillService().GetAllBillKinds(loggingSessionInfo);
        this.cbBillKind.DataSource = bills;
        this.cbBillKind.DataValueField = "Id";
        this.cbBillKind.DataTextField = "Description";
        this.cbBillKind.DataBind();
    }
}