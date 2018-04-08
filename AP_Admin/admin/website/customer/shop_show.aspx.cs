using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Admin.Model.Customer;

public partial class customer_shop_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.showCustomerShop();
            ViewState["back"] = Request.UrlReferrer.ToString();
        }
    }
    private void showCustomerShop()
    {
        //查看
        if (PageHelper.IsViewOperate(this))
        {
            string customer_shop_id = PageHelper.GetRequestParam(this, "cs_id", "");
            if (!string.IsNullOrEmpty(customer_shop_id))
            {
                this.displayCustomerShop(this.GetCustomerService().GetCustomerShopByID(customer_shop_id));
            }
        }
    }

    private void displayCustomerShop(CustomerShopInfo customerShop)
    {
        if (customerShop == null)
        {
            return;
        }

        this.tbCustomerCode.Text = customerShop.Customer.Code;
        this.tbCustomerName.Text = customerShop.Customer.Name;
        this.tbCustomerStatus.Text = customerShop.Customer.StatusDescription;

        this.tbShopCode.Text = customerShop.Code;
        this.tbShopName.Text = customerShop.Name;
        this.tbShopEnglishName.Text = customerShop.EnglishName;
        this.tbShopShortName.Text = customerShop.ShortName;
        this.tbShopProvince.Text = customerShop.Province;
        this.tbShopCity.Text = customerShop.City;
        this.tbShopCountry.Text = customerShop.Country;
        this.tbShopAddress.Text = customerShop.Address;
        this.tbShopPostcode.Text = customerShop.PostCode;
        this.tbShopContact.Text = customerShop.Contact;
        this.tbShopTel.Text = customerShop.Tel;
        this.tbShopFax.Text = customerShop.Fax;
        this.tbShopEmail.Text = customerShop.Email;
        this.tbShopStatus.Text = customerShop.StatusDescription;
        this.tbShopRemark.Text=customerShop.Remark;

        //this.tbSysModifyTime.Text = PageHelper.FormatDateTime(customerShop.LastSystemModifyStamp);
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "E", "<mce:script language=javascript>history.go(-2);</mce:script>"); 
        //this.Response.Redirect(ViewState["back"].ToString());
        //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "e", "<script language=javascript>history.go(-1);</script>", true);
        //this.Response.Redirect(this.Request.QueryString["from"]??"~/customer/shop_query.aspx");
    }
}