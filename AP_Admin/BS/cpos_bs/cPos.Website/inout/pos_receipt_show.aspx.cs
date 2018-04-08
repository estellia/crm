using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class inout_pos_receipt_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisableAllInput(this);
            LoadSkuProp();
            LoadPosInfo();
            this.btnSave.Visible = false;
        }
    }
    protected IList<cPos.Model.SkuPropInfo> SkuProInfos
    {
        get;
        set;
    }
    protected IList<cPos.Model.InoutDetailInfo> InoutDetailInfos
    {
        get;
        set;
    }
    //加载小票信息
    private void LoadPosInfo()
    {
        try
        {
            var source = new cPos.Service.InoutService().GetInoutInfoById(loggingSessionInfo, this.Request.QueryString["order_id"]);
            order_no.Text = source.order_no ?? "";
            order_date.Text = source.order_date ?? "";
            status_desc.Text = source.status_desc ?? "";
            create_unit_name.Text = source.create_unit_name ?? "";
            pos_name.Text = source.pos_id ?? "";
            tital_qty.Text = source.total_qty.ToString();
            dicount_rate.Text = source.discount_rate.ToString();
            total_amoount.Text = source.total_amount.ToString();
            keep_the_change.Text = source.keep_the_change.ToString();
            wiping_zero.Text = source.wiping_zero.ToString();
            vip_no.Text = source.vip_no ?? "";
            pos_name.Text = source.sales_user;
            //cretae_user_name.Text = source.create_user_name ?? "";
            //send_user_name.Text = source.send_user_name ?? "";
            //accepet_user_name.Text = source.accpect_user_name ?? "";
            //create_time.Text = source.create_time ?? "";
            //send_time.Text = source.send_time ?? "";
            //accpect_time.Text = source.accpect_time ?? "";
            approve_user_name.Text = source.approve_user_name ?? "";
            approve_time.Text = source.approve_time ?? "";
            remark.Text = source.remark ?? "";
            this.InoutDetailInfos = source.InoutDetailList;
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //加载Sku属性信息列表
    private void LoadSkuProp()
    {
        try
        {
            var source = new cPos.Service.SkuPropServer().GetSkuPropList(loggingSessionInfo);
            SkuProInfos = source;
        }
        catch
        {
            this.InfoBox.ShowPopError("加载数据失败");
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
                temp.Enabled = false;
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