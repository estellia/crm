using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Model;

public partial class inventory_inout_bill_out_show_item : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadSkuProp();
    }

    protected cPos.Service.SkuPropServer SkuPropService
    {
        get
        {
            return new cPos.Service.SkuPropServer();
        }
    }
    protected IList<SkuPropInfo> SkuPropInfos
    {
        get;
        set;
    }

    #region 加载Sku属性信息
    //加载Sku属性信息列表
    private void LoadSkuProp()
    {
        try
        {
            var source = this.SkuPropService.GetSkuPropList(loggingSessionInfo);
            SkuPropInfos = source;
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    #endregion
}