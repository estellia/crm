using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using cPos.Model.Promotion;

public partial class promotion_vip_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var action = this.Request.QueryString["oper_type"] ?? "1";
            LoadVipType();
            this.ViewState["action"] = action;
            switch (action)
            {
                case "1":
                    {
                        ShowVisible();
                    }
                    break;
                case "2":
                    {
                        ShowModify();
                    }
                    break;
                default: btnSave.Visible = false;
                    break;
            }
        }

    }
    private void LoadVipType()
    {
        try
        {
            var birgesource = service.SelectVipTypeList();
            var source = birgesource.Select(obj => new { ID = obj.ID, Name = obj.Name }).ToList();
            source.Insert(0, new { ID = "0", Name = "全部" });
            cbType.DataTextField = "Name";
            cbType.DataValueField = "ID";
            cbType.DataSource = source;
            cbType.DataBind();
        }
        catch (Exception ex)
        {
            this.InfoBox.ShowPopError(string.Format("出错了，详细信息{0}", ex.Message));
            PageLog.Current.Write(ex.Message);
        }

    }
    //显示修改页面
    private void ShowModify()
    {
        LoadVipInfo();
        btnSave.Visible = true;
    }
    //显示会员信息
    private void ShowVisible()
    {
        LoadVipInfo();
        DisAllInput(this);
        btnSave.Visible = false;
    }

    private void LoadVipInfo()
    {
        string  vipID=this.Request.QueryString["vip_id"];
        if (!string.IsNullOrEmpty(vipID))
        {
            var vipModel = service.GetVipByID(loggingSessionInfo, vipID);
            tbNo.Text = vipModel.No;
            cbType.SelectedValue = vipModel.Type.ID;
            tbName.Text = vipModel.Name;
            cbGender.SelectedValue = vipModel.Gender;
            tbIdentityNo.Text = vipModel.IdentityNo;
            tbEnglishName.Text = vipModel.EnglishName;
            tbBirthday.Text = vipModel.Birthday;
            tbAddress.Text = vipModel.Address;
            tbPostcode.Text = vipModel.Postcode;
            tbCell.Text = vipModel.Cell;
            tbEmail.Text = vipModel.Email;
            tbQQ.Text = vipModel.QQ;
            tbMSN.Text = vipModel.MSN;
            tbWeibo.Text = vipModel.Weibo;
            tbPoints.Text = vipModel.Points.ToString();
            tbStatus.Text = vipModel.StatusDescription;
            tbActivateTime.Text = vipModel.ActivateTime.ToString("yyyy-MM-dd hh:mm:ss:fff");
            tbActivateUnitName.Text = vipModel.ActivateUnit.DisplayName;
            tbExpiredDate.Text = vipModel.ExpiredDate;
            tbRemark.Text = vipModel.Remark;
            tbCreater.Text = vipModel.CreateUserName;
            tbCreateTime.Text = vipModel.CreateTime.ToString("yyyy-MM-dd hh:mm:ss:fff");
            tbEditor.Text = vipModel.ModifyUserName;
            tbEditTime.Text = vipModel.ModifyTime.ToString("yyyy-MM-dd hh:mm:ss:fff");
           // tbSysModifyTime.Text = vipModel.SystemModifyStamp.ToString("yyyy-MM-dd hh:mm:ss:fff");

        }
        else
        {
            this.InfoBox.ShowPopError("数据加载失败");
            return;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected PromotionService service
    {
        get { return new PromotionService(); }
    }
    //禁用所有的控件
    protected void DisAllInput(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c is TextBox)
            {
                var temp = c as TextBox;
                temp.Enabled = false;
            }
            if (c is DropDownList)
            {
                var temp = c as DropDownList;
                temp.Enabled = false;
            }
            if (c.Controls.Count > 0)
                DisAllInput(c);
        }
 
    }
}