using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Admin.Model.Customer;
using cPos.Admin.Model.Base;
using cPos.Admin.Service;

public partial class customer_terminal_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadTerminalType();
            int action = Convert.ToInt32(this.Request.QueryString["oper_type"]);
            ViewState["action"] = action;
            switch (action)
            {
                case 1:
                    {
                        DisableAllInput(this); 
                        LoadBasicData(); 
                        this.btnOK.Visible = false;
                    } break;
                case 2:
                    {
                        LoadDefaultValue();
                        this.tbHoldType.ReadOnly = true;
                    } break;
                case 3: 
                    { 
                        LoadBasicData();
                        this.tbHoldType.ReadOnly = true;
                    } break;
                default: this.btnOK.Visible = false; return;
            }
        }
    }
    private void LoadDefaultValue()
    {
        try
        {
            var customer = this.GetCustomerService().GetCustomerByID(this.Request.QueryString["customer_id"], false, false);
            if (customer == null)
            {
                this.btnOK.Visible = false;
            }
            else
            {
                this.tbCustomerName.Text = customer.Name ?? "";
                this.tbCustomerCode.Text = customer.Code ?? "";
                this.tbCustomerStatus.Text = customer.StatusDescription ?? "";
            }
            this.tbHoldType.Text = "租赁";
            this.tabContainerTermianl.ActiveTab = this.tabTerminal;
            this.tbCreater.Text = LoggingSession.UserName;
            this.tbCreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            this.tbHoldType.ReadOnly = true;
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (!CheckInput())
        {
            return;
        }
        var terminalInfo = GetNewTerminalInfo();
        if (Convert.ToInt32(ViewState["action"]) == 2)//新建
        {
            terminalInfo.ID = "";
            if (this.GetCustomerService().InsertCustomerTerminal(LoggingSession, terminalInfo))
            {
                this.Redirect("新建终端成功", InfoType.Info, this.Request.QueryString["from"]??"terminal_query.aspx");
            }
            else
            {
                this.InfoBox.ShowPopError("新建终端失败！");
            }
        }
        else                                           //修改
        {
            terminalInfo.ID = this.Request.QueryString["ct_id"];

            if (this.GetCustomerService().ModifyCustomerTerminal(LoggingSession, terminalInfo))
            {
                this.Redirect("修改成功", InfoType.Info, this.Request.QueryString["from"]??"terminal_query.aspx");
            }
            else
            {
                this.InfoBox.ShowPopError("修改终端失败！");
            }
        }
    }
    //获取新建或更新后的终端数据
    private CustomerTerminalInfo GetNewTerminalInfo()
    {
        var customerTerminalInfo = new CustomerTerminalInfo();
        customerTerminalInfo.Type = this.tbType.SelectedValue;
        customerTerminalInfo.Brand = this.tbBrand.Text??"";
        customerTerminalInfo.Model = this.tbModel.Text ?? "";
        customerTerminalInfo.Code = this.tbCode.Text ?? "";
        customerTerminalInfo.SN = this.tbSN.Text ?? "";
        customerTerminalInfo.PurchaseDate = this.tbPurchaseDate.Value;
        customerTerminalInfo.InsuranceDate = this.tbInsuranceDate.Value;
        customerTerminalInfo.Remark = this.tbRemark.Text ?? "";
        if (Convert.ToInt32(ViewState["action"]) == 2)
        {
            customerTerminalInfo.HoldType = "1";
            var customerInfo = new CustomerInfo();
            customerInfo.ID = this.Request.QueryString["customer_id"];
            customerTerminalInfo.Customer = customerInfo;
            customerTerminalInfo.SoftwareVersion = tbSoftwareVersion.Text ?? "";
            customerTerminalInfo.DBVersion = tbDBVersion.Text ?? "";
            customerTerminalInfo.WS = tbWS.Text ?? "";
            customerTerminalInfo.WS2 = tbWS2.Text ?? "";
            var creater = new UserOperateInfo();
            creater.Name = tbCreater.Text;
            customerTerminalInfo.Creater = creater;
            customerTerminalInfo.CreateTime = DateTime.Now;
        }
        customerTerminalInfo.HaveCashbox = Convert.ToInt32(this.chkCashBox.Checked);
        customerTerminalInfo.CashboxNo = this.tbCashNo.Text ?? "";
        customerTerminalInfo.HavePrinter = Convert.ToInt32(this.chkPrinter.Checked);
        customerTerminalInfo.PrinterNo = this.tbPrinterNo.Text ?? "";
        customerTerminalInfo.HaveClientDisplay = Convert.ToInt32(this.chkClientDisplay.Checked);
        customerTerminalInfo.ClientDisplayNo = this.tbClientDisplayNo.Text ?? "";
        customerTerminalInfo.HaveScanner = Convert.ToInt32(this.chkScanner.Checked);
        customerTerminalInfo.ScannerNo = this.tbScannerNo.Text ?? "";
        customerTerminalInfo.HaveEcard = Convert.ToInt32(this.chkEcard.Checked);
        customerTerminalInfo.EcardNo = tbEcardNo.Text ?? "";
        customerTerminalInfo.HaveHolder = Convert.ToInt32(chkHolder.Checked);
        customerTerminalInfo.HolderNo = tbHolderNo.Text ?? "";
        customerTerminalInfo.HaveOtherDevice = Convert.ToInt32(chkOtherDevice.Checked);
        customerTerminalInfo.OtherDeviceNo = tbOtherDeviceNo.Text;
        var editor = new UserOperateInfo();
        editor.Name = LoggingSession.UserName;
        customerTerminalInfo.LastEditor = editor;
        customerTerminalInfo.LastEditTime = DateTime.Now;
        //todo:检查
        //customerTerminalInfo.LastSystemModifyStamp = DateTime.Now;
        customerTerminalInfo.SoftwareVersion = this.tbSoftwareVersion.Text;
        customerTerminalInfo.DBVersion = this.tbDBVersion.Text;
        customerTerminalInfo.WS = this.tbWS.Text;
        customerTerminalInfo.WS2 = this.tbWS2.Text;
        return customerTerminalInfo;
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.QueryString["from"]??"terminal_query.aspx");
    }
    //输入检查
    private bool CheckInput()
    {
        //if (string.IsNullOrEmpty(tbCode.Text))
        //{
        //    this.InfoBox.ShowPopError("编码不能为空！");
        //    tbCode.Focus();
        //    return false;
        //}
        if (string.IsNullOrEmpty(tbSN.Text))
        {
            this.InfoBox.ShowPopError("序列号不能为空");
            tbSN.Focus();
            return false;
        }
        if (Convert.ToInt32(ViewState["action"]) == 2)
        {
            if (!string.IsNullOrEmpty(tbCode.Text))
            {
                var exists = this.GetCustomerService().ExistCustomerTerminalCode(this.Request.QueryString["ct_id"], this.tbCode.Text);
                if (exists)
                {
                    this.InfoBox.ShowPopError("编码已存在！");
                    this.tbCode.Focus();
                    return false;
                }
            }
        }
        else
        {
            if (!ViewState["code"].ToString().Equals(this.tbCode.Text))
            {
                if (!string.IsNullOrEmpty(tbCode.Text))
                {
                    var exists = this.GetCustomerService().ExistCustomerTerminalCode(this.Request.QueryString["ct_id"], this.tbCode.Text);
                    if (exists)
                    {
                        this.InfoBox.ShowPopError("编码已存在！");
                        this.tbCode.Focus();
                        return false;
                    }
                }
            }
        }
        return true;
    }
    //加载终端基本数据
    private void LoadBasicData()
    {
        try
        {
            var source = this.GetCustomerService().GetCustomerTerminalByID(this.Request.QueryString["ct_id"]);
            if (source == null)
            {
                this.btnOK.Visible = false;
            }
            tbCustomerCode.Text = source.Customer.Code ?? "";
            ViewState["code"] = source.Code;
            tbCustomerName.Text = source.Customer.Name ?? "";
            tbCustomerStatus.Text = source.Customer.StatusDescription ?? "";
            tbHoldType.Text = source.HoldTypeDescription;
            foreach (ListItem item in this.tbType.Items)
            {
                if (item.Value == source.Type)
                {
                    item.Selected = true;
                }
            }
            tbBrand.Text = source.Brand ?? "";
            tbModel.Text = source.Model ?? "";
            tbCode.Text = source.Code ?? "";
            tbSN.Text = source.SN ?? "";
            tbPurchaseDate.Value = (source.PurchaseDate ?? "").ToString();
            tbInsuranceDate.Value = (source.InsuranceDate ?? "").ToString();
            tbSoftwareVersion.Text = source.SoftwareVersion ?? "";
            tbDBVersion.Text = source.DBVersion ?? "";
            tbWS.Text = source.WS ?? "";
            tbWS2.Text = source.WS2 ?? "";
            tbRemark.Text = source.Remark ?? "";
            chkCashBox.Checked = source.HaveCashbox == 1;              
            tbCashNo.Text = source.CashboxNo ?? "";
            chkPrinter.Checked = source.HavePrinter == 1;
            tbPrinterNo.Text = source.PrinterNo ?? "";
            chkClientDisplay.Checked = source.HaveClientDisplay == 1;
            tbClientDisplayNo.Text = source.ClientDisplayNo ?? "";
            chkScanner.Checked = source.HaveScanner == 1;
            tbScannerNo.Text = source.ScannerNo ?? "";
            chkEcard.Checked = source.HaveEcard == 1;
            tbEcardNo.Text = source.EcardNo ?? "";
            chkHolder.Checked = source.HaveHolder == 1;
            tbHolderNo.Text = source.HolderNo ?? "";
            chkOtherDevice.Checked = source.HaveOtherDevice == 1;
            tbOtherDeviceNo.Text = source.OtherDeviceNo ?? "";
            tbCreater.Text = source.Creater.Name ?? "";
            tbCreateTime.Text = (source.CreateTime == null ? "" : source.CreateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            tbEditor.Text = source.LastEditor.Name ?? "";
            tbEditTime.Text = (source.LastEditTime == null ? "" : source.LastEditTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
           // tbSysModifyTime.Text = (source.LastSystemModifyStamp == null ? "" : source.LastSystemModifyStamp.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //加载终端类型
    private void LoadTerminalType()
    {
        try
        {
            var ret = (this.GetCustomerService() as BaseService).SelectDictionaryDetailListByDictionaryCode("terminal_type");
            this.tbType.DataTextField = "Name";
            this.tbType.DataValueField = "Code";
            this.tbType.DataSource = ret;
            this.tbType.DataBind();
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
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
}