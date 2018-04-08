using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class item_item_query : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadItemTree();
            LoadQueryByUrl();
            this.QueryCodition = GetCodition();
            this.SplitPageControl1.PageSize = int.Parse(this.Request.QueryString["pageSize"] ?? "10");
            if (!string.IsNullOrEmpty(this.Request.QueryString["query_flg"]))
            {
                Query(int.Parse(this.Request.QueryString["pageIndex"] ?? "0"));
            }
        }
    }
#region 服务
    protected cPos.Admin.Service.ItemService ItemService
    {
        get
        {
            return new cPos.Admin.Service.ItemService();
        }
    }
    protected cPos.Admin.Service.ItemCategoryService ItemCateGoryService
    {
        get
        {
            return new cPos.Admin.Service.ItemCategoryService();
        }
    }
#endregion
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.btnQuery.Enabled = false;
        this.QueryCodition = GetCodition();
        Query(this.gvItem.PageIndex);
        this.btnQuery.Enabled = true;
    }
    //更改用户状态
    protected void btnChangStatusClick(object sender, EventArgs e)
    {
        try
        {
            var status = this.hid_Item_Status.Value;
            var item_id = this.hid_Item_Id.Value;
            var msg = status == "1" ? "启用" : "停用";
            this.ItemService.SetItemStatus(loggingSessionInfo, item_id, status);
            this.InfoBox.ShowPopInfo(msg + "商品成功");
            Query(0);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    //查询条件
    protected ItemQueryCodition QueryCodition
    {
        get
        {
            if (ViewState["queryData"] == null)
                ViewState["queryData"] = new ItemQueryCodition();
            return ViewState["queryData"] as ItemQueryCodition;
        }
        set
        {
            ViewState["queryData"] = value;
        }
    }
    //从界面获取查询条件
    private ItemQueryCodition GetCodition()
    {
        var condition = new ItemQueryCodition();
        if (!string.IsNullOrEmpty(this.tbCode.Text))
        {
            condition.ItemCode = this.tbCode.Text;
        }
        if (!string.IsNullOrEmpty(this.tbItemCategory.Text))
        {
            condition.ItemCategoryID = this.hid_ItemCategoryID.Value;
        }
        if (!string.IsNullOrEmpty(this.tbName.Text))
        {
            condition.ItemName = this.tbName.Text;
        }
        if (this.selStatus.SelectedIndex != 0)
        {
            condition.ItemStatus = this.selStatus.SelectedValue;
        }
        return condition;
    }
    private void Query(int index)
    {
        try
        {
            var qs = this.QueryCodition;
            var source = this.ItemService.SearchItemList(loggingSessionInfo, qs.ItemCode, qs.ItemName, 
                qs.ItemCategoryID, qs.ItemStatus ?? "", this.SplitPageControl1.PageSize, 
                this.SplitPageControl1.PageSize * index);
            this.SplitPageControl1.RecoedCount = source.ICount;
            this.SplitPageControl1.PageIndex = index;
            this.gvItem.DataSource = source.ItemInfoList;
            this.gvItem.DataBind();
            if (this.SplitPageControl1.PageIndex != index)
            {
                Query(this.SplitPageControl1.PageIndex);
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    protected void SplitPageControl1_RequireUpdate(object sender, EventArgs e)
    {
        Query(this.SplitPageControl1.PageIndex);
    }
    //加载商品类别树
    private void LoadItemTree()
    {
        try
        {
            var source = this.ItemCateGoryService.GetItemCagegoryList(loggingSessionInfo);
            var root = new TreeNode
            {
                Text = "所有类别",
                Value = "--",
                Expanded = true
            };
            source.Where(obj=>obj.Parent_Id=="-99").Select(o => {
                var node= new TreeNode { Text = o.Item_Category_Name, Value = o.Item_Category_Id, Expanded=true };
                GetChildNodes(source, node, o.Item_Category_Id);
                root.ChildNodes.Add(node);
                return 0;
            }).ToArray();
            tvTree.Nodes.Add(root);
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错");
        }
    }
    private void GetChildNodes(IEnumerable<cPos.Model.ItemCategoryInfo> source,TreeNode n1,string parent_id)
    {
        var childerens = source.Where(obj => obj.Parent_Id == parent_id);
        foreach (var item in childerens)
        {
            var node = new TreeNode { Text = item.Item_Category_Name, Value = item.Item_Category_Id, Expanded = true };
            GetChildNodes(source, node, item.Item_Category_Id);
            n1.ChildNodes.Add(node);
        }
    }
    //前台注册_from字段
    protected override void OnPreRender(EventArgs e)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(this.Request.Url.LocalPath);
        sb.Append("?");
        sb.Append("and=");
        if (this.gvItem.DataSource != null)
        {
            sb.Append("&query_flg=true");
        }
        if (!string.IsNullOrEmpty(this.Request.QueryString["cur_menu_id"]))
        {
            sb.Append("&cur_menu_id=" + Server.UrlEncode(this.Request.QueryString["cur_menu_id"]));
        }
        sb.Append("&item_code=" + Server.UrlEncode(tbCode.Text));
        sb.Append("&item_name=" + Server.UrlEncode(tbName.Text));
        sb.Append("&item_category_id=" + Server.UrlEncode(hid_ItemCategoryID.Value));
        sb.Append("&item_category_name=" + Server.UrlEncode(tbItemCategory.Text));
        sb.Append("&status="+ this.selStatus.SelectedValue);
        sb.Append("&selectedNode=" + Server.UrlEncode(this.tvTree.SelectedValue));
        sb.Append(string.Format("&pageIndex={0}&pageSize={1}", 
            Server.UrlEncode(this.SplitPageControl1.PageIndex.ToString()), 
            Server.UrlEncode(this.SplitPageControl1.PageSize.ToString())));
        this.ClientScript.RegisterHiddenField("_from", sb.ToString());
        base.OnPreRender(e);
    }
    //根据Url加载查询条件
    private void LoadQueryByUrl()
    {
        var qs = this.Request.QueryString;
        this.tbCode.Text = this.Request.QueryString["item_code"] ?? "";
        this.hid_ItemCategoryID.Value = this.Request.QueryString["item_category_id"] ?? "";
        this.tbItemCategory.Text = this.Request.QueryString["item_category_name"] ?? "";
        this.tbName.Text = this.Request.QueryString["item_name"] ?? "";
        this.selStatus.SelectedValue = this.Request.QueryString["status"] ?? "0";
        SelectedNode(this.tvTree.Nodes, this.Request.QueryString["selectedNode"]);
    }
    //设置treeview的选中节点
    private void SelectedNode(TreeNodeCollection nodes,string value)
    {
        foreach (TreeNode item in nodes)
        {
            if (item.Value == value)
            {
                item.Selected = true;
                return;
            }
            if (item.ChildNodes.Count != 0)
                SelectedNode(item.ChildNodes, value);
        }
    }
    [Serializable]
    public class ItemQueryCodition
    {
        public string ItemCode { get; set; }
        public string ItemCategoryID { get; set; }
        public string ItemName { get; set; }
        public string ItemStatus { get; set; }
    }
    protected void tvTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (this.tvTree.SelectedValue != "--")
        {
            this.tbItemCategory.Text = this.tvTree.SelectedNode.Text;
            this.hid_ItemCategoryID.Value = this.tvTree.SelectedNode.Value;
        }
        else
        {
            this.tbItemCategory.Text = "";
            this.hid_ItemCategoryID.Value = "";
        }
        this.QueryCodition = GetCodition();
        Query(0);
    }
}