using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class DropDownTreeHelper : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var comps = new DataItem[]{
            new  DataItem{CompId= "1",CompName = "公司1",ParentId=null}
            ,new  DataItem{CompId= "2",CompName = "公司1-1",ParentId="1"}
            ,new  DataItem{CompId= "3",CompName = "公司1-1-1",ParentId="2"}
            ,new  DataItem{CompId= "4",CompName = "公司2",ParentId=null}
            ,new  DataItem{CompId= "5",CompName = "公司3",ParentId=null}
        };

            DropDownTree1.DataBind<DataItem>(comps.Where(obj=>obj.ParentId == null)
                , obj => null
                ,obj=>new controls_DropDownTree.tvNode {
                    CheckState=null,
                    Text = obj.CompName,
                    Value = obj.CompId,
                    Complete = false ,
                    ShowCheck = true,
                });
        }
    }

    public class DataItem
    {
        public string CompId { get; set; }
        public string CompName { get; set; }
        public string ParentId { get; set; }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder ();
        sb.Append ("选择的值为:"+string.Join("," ,DropDownTree1.SelectValues));
        sb.Append ("\r\n");
        sb.Append("选择的文本为:" + string.Join(",", DropDownTree1.SelectTexts));
        div_rult.InnerText = sb.ToString();
    }
}