using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class item_itemSearch : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = this.Request["action"];
        if (action == null)
        {
            GetItem();
        }
        else if (action == "getSkuList")
        {
            GetSkuList();
        }
    }
    //获取Sku集合
    private void GetSkuList()
    {
        try
        {
            var item_id = this.Request["item_id"];
            if (item_id == null)
            {
                Response.Write(null);
                Response.End();
            }
            var source = new cPos.Admin.Service.SkuService().GetSkuListByItemId(loggingSessionInfo, item_id);
            Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(source));
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //获取商品名称
    private void GetItem()
    {
        var rult = GetMachItemInfo();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (!(rult == null || rult.Count == 0))
        {
            string str = "[";
            for (int i = 0; i < Math.Min(rult.Count, 10); i++)
            {
                sb.Append("{\"showName\":\"" + rult[i].Item_Name + "\",\"showId\":\"" + rult[i].Item_Id + "\",\"showNo\":\"" + rult[i].Item_Code + "\"},");
            }
            str += sb.ToString().Trim(',');
            str += "]";
            Response.Write(str);
        }
        else
        {
            Response.Write("");
        }
    }
    private IList<cPos.Model.ItemInfo> GetMachItemInfo()
    {
        try
        {
            return new cPos.Admin.Service.ItemService().GetItemListLikeItemName(loggingSessionInfo, this.Request["Key"]);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
            return null;
        }
    }
}