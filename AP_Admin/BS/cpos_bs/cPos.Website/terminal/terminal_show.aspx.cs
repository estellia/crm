using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model.Pos;

public partial class terminal_terminal_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadTerminalType();
            int action = Convert.ToInt32(this.Request.QueryString["oper_type"]);
            ViewState["action"] = action;
            this.tabContainerTermianl.ActiveTabIndex = 0;
            switch (action)
            {
                case 1://查看
                    {
                        DisableAllInput(this);
                        LoadBasicData();
                        this.btnOK.Visible = false;
                    } break;
                case 2://新建
                    {
                        LoadDefaultValue();
                    } break;
                case 3://修改
                    {
                        LoadBasicData();
                    } break;
                default: this.btnOK.Visible = false; return;
            }
        }
    }
    private void LoadDefaultValue()
    {

        this.chkCashBox.Checked = false;
        this.chkPrinter.Checked = false;
        this.chkClientDisplay.Checked = false;
        this.chkScanner.Checked = false;
        this.chkEcard.Checked = false;
        this.chkHolder.Checked = false;
        this.chkOtherDevice.Checked = false;
        this.tbHoldType.Text = "自有";
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
            //this.tbInsuranceDate.Enabled = true;
            //this.tbPurchaseDate.Enabled = true;

            this.tbInsuranceDate.Disabled = false;
            
            this.tbPurchaseDate.Disabled= false;
            if (new PosService().InsertPos(loggingSessionInfo,terminalInfo))
            {
                this.Redirect("新建终端成功", InfoType.Info, this.Request.QueryString["from"] ?? "terminal_query.aspx");
            }
            else
            {
                this.InfoBox.ShowPopError("新建终端失败");
            }
        }
        else                                           //修改
        {
            terminalInfo.ID = this.Request.QueryString["pos_id"];

            if ((new PosService()).ModifyPos(loggingSessionInfo, terminalInfo))
            {
                this.Redirect("修改成功", InfoType.Info, this.Request.QueryString["from"] ?? "terminal_query.aspx");
            }
            else
            {
                this.InfoBox.ShowPopError("修改终端失败");
            }
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.Response.Redirect(this.Request.QueryString["from"] ?? "terminal_query.aspx");
    }
    //获取新建或更新后的终端数据
    private PosInfo GetNewTerminalInfo()
    {
        var TerminalInfo = new PosInfo();
        TerminalInfo.Type = this.tbType.SelectedValue;
        TerminalInfo.Brand = this.tbBrand.Text;
        TerminalInfo.Model = this.tbModel.Text;
        TerminalInfo.Code = this.tbCode.Text;
        TerminalInfo.SN = this.tbSN.Text;
        //TerminalInfo.PurchaseDate = this.tbPurchaseDate.Text;
        //修改为html服务器控件的value
        
        TerminalInfo.PurchaseDate = this.tbPurchaseDate.Value;
        TerminalInfo.InsuranceDate = this.tbInsuranceDate.Value;
        
        //TerminalInfo.InsuranceDate = this.tbInsuranceDate.Text;
        TerminalInfo.Remark = this.tbRemark.Text.Trim();
        if (Convert.ToInt32(ViewState["action"]) == 2)
        {
            TerminalInfo.HoldType = "2";
            //TerminalInfo.SoftwareVersion = tbSoftwareVersion.Text;
            //TerminalInfo.DBVersion = tbDBVersion.Text;
            //TerminalInfo.WS = tbWS.Text;
            //TerminalInfo.WS2 = tbWS2.Text;
            TerminalInfo.CreateUserID = loggingSessionInfo.CurrentUser.User_Id;
            TerminalInfo.CreateTime = DateTime.Now;
        }
        else
        {
            //TerminalInfo.HoldType = this.cbHoldType.SelectedValue;
            TerminalInfo.ModifyUserID =loggingSessionInfo.CurrentUser.User_Id;
            TerminalInfo.ModifyTime = DateTime.Now;
        }
        TerminalInfo.HaveCashbox = Convert.ToInt32(this.chkCashBox.Checked);
        TerminalInfo.CashboxNo = this.tbCashNo.Text;
        TerminalInfo.HavePrinter = Convert.ToInt32(this.chkPrinter.Checked);
        TerminalInfo.PrinterNo = this.tbPrinterNo.Text;
        TerminalInfo.HaveClientDisplay = Convert.ToInt32(this.chkClientDisplay.Checked);
        TerminalInfo.ClientDisplayNo = this.tbClientDisplayNo.Text;
        TerminalInfo.HaveScanner = Convert.ToInt32(this.chkScanner.Checked);
        TerminalInfo.ScannerNo = this.tbScannerNo.Text;
        TerminalInfo.HaveEcard = Convert.ToInt32(this.chkEcard.Checked);
        TerminalInfo.EcardNo = tbEcardNo.Text;
        TerminalInfo.HaveHolder = Convert.ToInt32(chkHolder.Checked);
        TerminalInfo.HolderNo = tbHolderNo.Text;
        TerminalInfo.HaveOtherDevice = Convert.ToInt32(chkOtherDevice.Checked);
        TerminalInfo.OtherDeviceNo = tbOtherDeviceNo.Text;
        TerminalInfo.ModifyUserName = loggingSessionInfo.CurrentUser.User_Name;
        TerminalInfo.ModifyTime = DateTime.Now;
        //TerminalInfo.SystemModifyStamp = DateTime.Now;
        //TerminalInfo.e

        //todo:检查
        //TerminalInfo.SystemModifyStamp = DateTime.Now;
        TerminalInfo.SoftwareVersion = this.tbSoftwareVersion.Text;
        TerminalInfo.DBVersion = this.tbDBVersion.Text;
        TerminalInfo.WS = this.tbWS.Text;
        TerminalInfo.WS2 = this.tbWS2.Text;
        return TerminalInfo;
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
                var exists = new PosService().ExistPosCode(this.Request.Params["pos_id"], this.tbCode.Text);
                if (exists)
                {
                    this.InfoBox.ShowPopError("编码已存在！");
                    this.tbCode.Focus();
                    return false;
                }
            }
            var existsSN = new PosService().ExistPosSN(this.Request.Params["pos_id"], this.tbSN.Text);
            if (existsSN)
            {
                this.InfoBox.ShowPopError("序列号已存在！");
                this.tbSN.Focus();
                return false;
            }
        }
        else
        {
            if (!ViewState["code"].ToString().Equals(this.tbCode.Text))
            {
                if (!string.IsNullOrEmpty(tbCode.Text))
                {
                    var exists = new PosService().ExistPosCode(this.Request.Params["pos_id"], this.tbCode.Text);
                    if (exists)
                    {
                        this.InfoBox.ShowPopError("编码已存在！");
                        this.tbCode.Focus();
                        return false;
                    }
                }
            }
            if (!ViewState["SN"].ToString().Equals(this.tbCode.Text))
            {
                if (!string.IsNullOrEmpty(tbSN.Text))
                {
                    var existsSN = new PosService().ExistPosSN(this.Request.Params["pos_id"], this.tbSN.Text);
                    if (existsSN)
                    {
                        this.InfoBox.ShowPopError("序列号已存在！");
                        this.tbSN.Focus();
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
            var source = new PosService().GetPosByID(loggingSessionInfo, this.Request.Params["pos_id"]);
            if (source == null)
            {
                this.btnOK.Visible = false;
            }
            ViewState["code"] = source.Code;
            ViewState["SN"] = source.SN;
            this.tbType.SelectedValue = source.Type;
            tbHoldType.Text = source.HoldTypeDescription;
            tbBrand.Text = source.Brand ?? "";
            tbModel.Text = source.Model ?? "";
            tbCode.Text = source.Code ?? "";
            tbSN.Text = source.SN ?? "";
            //tbPurchaseDate.Text = (source.PurchaseDate ?? "").ToString();
            tbPurchaseDate.Value = (source.PurchaseDate ?? "").ToString();
             tbInsuranceDate.Value = (source.InsuranceDate ?? "").ToString();
           
            // tbInsuranceDate.Text = (source.InsuranceDate ?? "").ToString();
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
            tbCreater.Text = source.CreateUserName?? "";
            tbCreateTime.Text = (source.CreateTime == null ? "" : source.CreateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            tbEditor.Text = source.ModifyUserName?? "";
            tbEditTime.Text = (source.ModifyTime == null ? "" : source.ModifyTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //tbSysModifyTime.Text = (source.SystemModifyStamp == null ? "" : source.SystemModifyStamp.ToString("yyyy-MM-dd HH:mm:ss.fff"));
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
            this.tbType.DataTextField = "Name";
            this.tbType.DataValueField = "Code";
            this.tbType.DataSource = new PosService().SelectPostTypeList();
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
        //this.tbInsuranceDate.Enabled = false;
       // this.tbPurchaseDate.Enabled = false;
        this.tbInsuranceDate.Disabled= true;
       
        this.tbPurchaseDate.Disabled = true;
    }
}