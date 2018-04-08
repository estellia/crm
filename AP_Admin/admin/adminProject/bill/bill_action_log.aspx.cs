using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using cPos.Admin.Model.Bill;

public partial class bill_bill_action_log : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (this.Request.Params["bill_id"] != null)
        //{
        //    IList<BillActionLogInfo> bill_action_log_lst = this.GetBillService().GetBillActionLogHistoryList(this.Request.Params["bill_id"].ToString());
        //    this.gvBillActionLog.DataSource = bill_action_log_lst;
        //    this.gvBillActionLog.DataBind();
        //}
    }
}