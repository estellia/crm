using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;
using System.Text;

public partial class item_category_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadTreeView();
            string action = this.Request.QueryString["strDo"];
            LoadTreeView();
            ViewState["action"] = action;
            if (action == "Visible") {
                this.selParentItemCategory.ReadOnly = true;
                this.btSave.Visible = false;
            }
            switch (action)
            {
                case "Create":
                    { ShowCreatePage(); } 
                    break;
                case "Modify":
                    { ShowModifyPage(); } 
                    break;
                case "Visible":
                    { ShowVisiablePage(); } 
                    break;
                default: break;
            }
        }

    }
    //页面加载上级商品类型树
    private void LoadTreeView()
    {
        var itemcategoryinfo = itemCategoryService.GetItemCagegoryList(loggingSessionInfo).Where(obj => obj.Parent_Id == null);
        selParentItemCategory.DataBind(new controls_DropDownTree.tvNode[]{ new controls_DropDownTree.tvNode
        {
            CheckState = false,
            Complete = false,
            ShowCheck = false,
            Text = "所有类别",
            Value = "-99",
        }});
    }
    //返回ItemCategoryService实例的属性
    protected ItemCategoryService itemCategoryService
    {
        get { return new ItemCategoryService(); }
    }
    //显示查看页面的加载函数
    private void ShowVisiablePage()
    {
        var category_id = Request.QueryString["item_category_id"];
        if (category_id == null)
        {
            ShowCreatePage();
        }
        else
        {
            btSave.Visible = false;
            //加载禁用所有input函数
            DisableAllTextBox(this);
            LoadCategoryInfo(category_id);


        }
    }
    //显示修改页面函数
    private void ShowModifyPage()
    {
        var category_id = Request.QueryString["item_category_id"];
        if (category_id == null)
        {
            ShowCreatePage();
        }
        else
        {
            btSave.Visible = true;
            LoadCategoryInfo(category_id);
        }

    }
    //加载用户的信息
    private void LoadCategoryInfo(string category_id)
    {
        var model = this.itemCategoryService.GetItemCategoryById(loggingSessionInfo, category_id);
        //把数据加载到控件中
        tbItemCategoryCode.Text = model.Item_Category_Code.ToString().Trim();
        tbItemCategoryName.Text = model.Item_Category_Name.ToString().Trim();
        if (model.Parent_Id != "") {
            selParentItemCategory.SelectedValue = model.Parent_Id.ToString().Trim();
            if (model.Parent_Id != "-99")
            {
                selParentItemCategory.SelectedText = model.Parent_Name.ToString().Trim();
            }
        }
        tbItemCategoryPYZJM.Text = model.Pyzjm.ToString().Trim();
    }
    //显示新建 页面
    private void ShowCreatePage()
    {
        btSave.Visible = true;
    }
    //禁用所有textbox函数
    protected void DisableAllTextBox(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c is TextBox)
            {
                var temp = c as TextBox;
                temp.ReadOnly = true;
            }
            if (c.Controls.Count > 0)
                DisableAllTextBox(c);
        }
    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        //btSave.Enabled = false;
        var model = new ItemCategoryInfo();
        model.Pyzjm = tbItemCategoryPYZJM.Text.Trim();
        model.Item_Category_Code = tbItemCategoryCode.Text.Trim();
        model.Item_Category_Name = tbItemCategoryName.Text.Trim();
        //model.Parent_Name = selParentItemCategory.SelectedValue.Trim();
        model.Parent_Id = selParentItemCategory.SelectedValue.Trim();
       //string ResultInfo = itemCategoryService.SetItemCategoryInfo(loggingSessionInfo, model);
       // this.Redirect(ResultInfo, InfoType.Info, this.Request.QueryString["from"] ?? "category_query.aspx");
        var action = this.Request.QueryString["strDo"];
         if (action == "Create") //新建
         {
                 string ResultInfo = itemCategoryService.SetItemCategoryInfo(loggingSessionInfo, model);
                 this.Redirect(ResultInfo, InfoType.Info, this.Request.QueryString["from"] ?? "category_query.aspx");
         }
         else if(action == "Modify")//修改
         {
             var Item_Category_Id = this.Request.QueryString["Item_Category_Id"];
             model.Item_Category_Id = Item_Category_Id;
              string ResultInfo = itemCategoryService.SetItemCategoryInfo(loggingSessionInfo, model);
              this.Redirect(ResultInfo, InfoType.Info, this.Request.QueryString["from"] ?? "category_query.aspx");
         }
         
    }
}