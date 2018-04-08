using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cPos.Admin.Model.Customer;

public partial class customer_user_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.showCustomerUser();
        }
    }

    private void showCustomerUser()
    {
        //查看
        if (PageHelper.IsViewOperate(this))
        {
            string customer_user_id = PageHelper.GetRequestParam(this, "cu_id", "");
            if (!string.IsNullOrEmpty(customer_user_id))
            {
                this.displayCustomerUser(this.GetCustomerService().GetCustomerUserByID(customer_user_id));
            }
        }
    }
    //显示CustomerUser
    private void displayCustomerUser(CustomerUserInfo customerUser)
    {
        if (customerUser == null)
        {
            return;
        }

        this.tbCustomerCode.Text = customerUser.Customer.Code;
        this.tbCustomerName.Text = customerUser.Customer.Name;
        this.tbCustomerStatus.Text = customerUser.Customer.StatusDescription;

        this.tbUserAccount.Text = customerUser.Account;
        this.tbUserName.Text = customerUser.Name;
        this.tbUserExpiredDate.Text = customerUser.ExpiredDate;
        this.tbUserStatus.Text = customerUser.StatusDescription;

        //this.tbSysModifyTime.Text = PageHelper.FormatDateTime(customerUser.LastSystemModifyStamp);
    }
     
    protected void btnReturn_Click(object sender, EventArgs e)
    {

        this.Response.Redirect(this.Request.QueryString["from"] ?? "~/customer/user_query.aspx");
        this.Response.Redirect("~/customer/user_query.aspx");
    }
}
