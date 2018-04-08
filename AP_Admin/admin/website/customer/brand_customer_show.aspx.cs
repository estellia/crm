using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Admin.Model.Customer;
using cPos.Admin.Model;
using System.Collections;

public partial class brand_customer_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.showBrandCustomer();
            ViewState["code"] = "";
            ViewState["back"] = Request.UrlReferrer.ToString();

            this.tbSysModifyTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
    private void showBrandCustomer()
    {
        var service = new cPos.Admin.Service.Implements.CustomerService();
        string id = PageHelper.GetRequestParam(this, "bc_id", "");
        if (!string.IsNullOrEmpty(id))
        {
            this.displayBrandCustomer(service.GetBrandCustomerById(id));
        }

        if (PageHelper.IsViewOperate(this))
        {
            this.tbCode.ReadOnly = true;
            this.tbName.ReadOnly = true;
            this.tbEng.ReadOnly = true;
            this.tbAddress.ReadOnly = true;
            this.tbPost.ReadOnly = true;
            this.tbContacter.ReadOnly = true;
            this.tbTel.ReadOnly = true;
            this.tbEmail.ReadOnly = true;
            this.ddlStatus.Enabled = false;
            this.btnOK.Visible = false;
        }
    }

    private void displayBrandCustomer(BrandCustomerInfo obj)
    {
        if (obj == null) return;
        this.tbCode.Text = obj.brand_customer_code;
        ViewState["code"] = obj.brand_customer_code;
        this.tbName.Text = obj.brand_customer_name;
        this.tbEng.Text = obj.brand_customer_eng;
        this.tbAddress.Text = obj.brand_customer_address;
        this.tbPost.Text = obj.brand_customer_post;
        this.tbContacter.Text = obj.brand_customer_contacter;
        this.tbTel.Text = obj.brand_customer_tel;
        this.tbEmail.Text = obj.brand_customer_email;
        this.ddlStatus.SelectedValue = obj.status;
        this.tbCreateTime.Text = obj.create_time;
        this.tbCreateUser.Text = obj.create_user_name;
        this.tbModifyTime.Text = obj.modify_time;
        this.tbModifyUser.Text = obj.modify_user_name;
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "E", 
            "<mce:script language=javascript>history.go(-2);</mce:script>"); 
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (!CheckInput())
        {
            return;
        }
        var service = new cPos.Admin.Service.Implements.CustomerService();
        var obj = new BrandCustomerInfo();
        obj.brand_customer_code = this.tbCode.Text.Trim();
        obj.brand_customer_name = this.tbName.Text.Trim();
        obj.brand_customer_eng = this.tbEng.Text.Trim();
        obj.brand_customer_address = this.tbAddress.Text.Trim();
        obj.brand_customer_post = this.tbPost.Text.Trim();
        obj.brand_customer_contacter = this.tbContacter.Text.Trim();
        obj.brand_customer_tel = this.tbTel.Text.Trim();
        obj.brand_customer_email = this.tbEmail.Text.Trim();
        obj.status = this.ddlStatus.SelectedValue;
        obj.create_user_id = loggingSessionInfo.CurrentUser.User_Id;
        obj.create_time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        if (PageHelper.IsCreateOperate(this))//新建
        {
            obj.brand_customer_id = Guid.NewGuid().ToString().Replace("-", string.Empty);
            Hashtable ht = service.SaveBrandCustomerInfo(obj);
            if (Convert.ToBoolean(ht["status"]))
            {
                this.Redirect("新建成功", InfoType.Info, this.Request.QueryString["from"] ?? "brand_customer_query.aspx");
            }
            else
            {
                this.InfoBox.ShowPopError("新建失败！");
            }
        }
        else
        {
            obj.brand_customer_id = this.Request.QueryString["bc_id"];
            obj.modify_user_id = loggingSessionInfo.CurrentUser.User_Id;
            obj.modify_time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            Hashtable ht = service.SaveBrandCustomerInfo(obj);
            if (Convert.ToBoolean(ht["status"]))
            {
                this.Redirect("修改成功", InfoType.Info, this.Request.QueryString["from"] ?? "brand_customer_query.aspx");
            }
            else
            {
                this.InfoBox.ShowPopError("修改失败！");
            }
        }
    }

    private bool CheckInput()
    {
        var service = new cPos.Admin.Service.Implements.CustomerService();
        if (string.IsNullOrEmpty(tbCode.Text))
        {
            this.InfoBox.ShowPopError("编码不能为空");
            tbCode.Focus();
            return false;
        }
        if (PageHelper.IsModifyOperate(this))
        {
            if (!string.IsNullOrEmpty(tbCode.Text))
            {
                var exists = service.CheckExistBrandCustomer(this.Request.QueryString["bc_id"], this.tbCode.Text);
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
                    var exists = service.CheckExistBrandCustomer(this.Request.QueryString["bc_id"], this.tbCode.Text);
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
}