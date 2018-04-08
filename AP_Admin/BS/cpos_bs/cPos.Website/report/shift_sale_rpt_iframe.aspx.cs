using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class report_shift_sale_rpt_iframe : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["shiftId"]))
            {
                string shiftid = Request["shiftId"];
                InoutInfo = new cPos.Service.InoutService().GetInoutListByShiftId(this.loggingSessionInfo, shiftid);
                var dto = InoutInfo.Select(obj => new UnitDetailDTO { order_id = obj.order_id, create_time = obj.create_time, sales_user = obj.sales_user, order_no = obj.order_no, vip_no = obj.vip_no, payment_name = obj.payment_name, total_amount = obj.total_amount,create_user_name=obj.create_user_name });
                if (Session["_shift_detail"] == null)
                {
                    Session["_shift_detail"] = dto;
                }
                else
                {
                    var currentData = (IEnumerable<UnitDetailDTO>)Session["_shift_detail"];
                    var filterData = dto.Where(obj => !currentData.Select(o => o.order_id).Contains(obj.order_id));
                    Session["_shift_detail"] = currentData.Union(filterData);
                }
                Query(0);
            }
        }
    }
    private void Query(int index)
    {
        try
        {
            if (Session["_shift_detail"] != null)
            {
                var data = ((IEnumerable<UnitDetailDTO>)Session["_shift_detail"]).OrderByDescending(o => o.total_amount);
                this.Source = data.Skip(this.SplitPageControl1.PageSize * index).Take(SplitPageControl1.PageSize);
                this.SplitPageControl1.RecoedCount = data.Count();
                if (this.SplitPageControl1.PageIndex != index)
                {
                    Query(this.SplitPageControl1.PageIndex);
                }
            }
        }
        catch
        {
        }
    }
    protected IEnumerable<UnitDetailDTO> Source
    {
        get;
        set;
    }
    protected int ICount
    {
        get
        {
            if (Session["_shift_detail"] == null)
                return 0;
            return ((IEnumerable<UnitDetailDTO>)Session["_shift_detail"]).Count();
        }
    }
    public IList<cPos.Model.InoutInfo> InoutInfo
    {

        set;
        get;
    }
    protected void btnDeleteClick(object sender, EventArgs e)
    {
        if (Session["_shift_detail"] != null)
        {
            var currentData = (IEnumerable<UnitDetailDTO>)Session["_shift_detail"];
            Session["_shift_detail"] = currentData.Where(obj => obj.order_id != hidOrderId.Value);
        } 
        Query(this.SplitPageControl1.PageIndex);
    }
    protected void btnResetDetail_Click(object sender, EventArgs e)
    {
        Session["_shift_detail"] = null;
        Query(0);
    }
    protected void SplitPageControl_OnRequireUpdate(object sender, EventArgs e)
    {
        Query(this.SplitPageControl1.PageIndex);
    }
}