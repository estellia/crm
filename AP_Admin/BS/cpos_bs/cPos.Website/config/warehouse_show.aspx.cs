using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos .Service;
using cPos.Model;
using cPos.Model.Pos;

public partial class config_warehouse_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = this.Request.QueryString["oper_type"] ?? "2";
            ViewState["action"] = action;
            switch (action)
            {
                case "2":
                    {
                        ShowCreatePage();
                    } break;
                case "3":
                    {
                        ShowModifyPage();
                    } break;
                case "1":
                    {
                        ShowVisiblePage();
                    } break;
                default:
                    break;
            }
        }

    }

   private void initUnitTree()
    {
        //var serv = new UnitService();
        //DropDownTree1.DataBind<UnitInfo>(serv.GetRootUnitsByDefaultRelationMode(base.loggingSessionInfo)
        //    , obj => serv.GetSubUnitsByDefaultRelationMode(base.loggingSessionInfo, obj.Id)
        //    , obj => new controls_DropDownTree.tvNode { Text=obj.Name,Value=obj.Id,Complete=true});
        try
        {
            var service = new UnitService();
            var source = service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo);

            this.DropDownTree1.DataBind(source.Select(obj =>
                new controls_DropDownTree.tvNode
                {
                    CheckState = false,
                    Complete = false,
                    ShowCheck = false,
                    Text = obj.Name,
                    Value = obj.Id,
                }).ToArray());
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }

    //显示详细页
    private void ShowVisiblePage()
    {
        
        string warehourse_id = this.Request.QueryString["warehouse_id"];
        if (string.IsNullOrEmpty(warehourse_id))
        {
            ShowCreatePage();
        }
        else
        {
            initUnitTree();
            disabledAllInput(this);
            btnSave.Visible = false;
            LoadWarehourseInfo(warehourse_id);
        }
    }
    //生成类实例的属性
    protected PosService posservice
    {
        get { return new PosService();}
    }
    private void LoadWarehourseInfo(string warehourse_id)
    {
        var warehourse_model = posservice.GetWarehouseByID(loggingSessionInfo, warehourse_id);
        if (warehourse_model == null)
        {
            this.InfoBox.ShowPopError("数据加载失败");
            btnSave.Visible = false;
            return;
        }
        DropDownTree1.SelectedValue = warehourse_model.Unit.Id;
        DropDownTree1.SelectedText = warehourse_model.Unit.Name;

        tbCode.Text = warehourse_model.Code.Trim();
        tbName.Text = warehourse_model.Name.Trim();
        tbEnglishName.Text = warehourse_model.EnglishName.Trim();
        tbAddress.Text = warehourse_model.Address.Trim();
        tbContacter.Text = warehourse_model.Contacter.Trim();
        tbTel.Text = warehourse_model.Tel.Trim();
        tbFax.Text = warehourse_model.Fax.Trim();
        cbDefault.SelectedValue = warehourse_model.IsDefault.ToString();
        tbStatus.Text = warehourse_model.StatusDescription;
        tbRemark.Text = warehourse_model.Remark;
        tbCreater.Text = warehourse_model.CreateUserName;
        tbCreateTime.Text = warehourse_model.CreateTime.ToString("yyyy-MM-dd HH:mm:sss");
        tbEditor.Text = warehourse_model.ModifyUserName;
        tbEditTime.Text = warehourse_model.ModifyTime.ToString("yyyy-MM-dd HH:mm:sss");
       // tbSysModifyTime.Text = warehourse_model.SystemModifyStamp.ToString("yyyy-MM-dd HH:mm:sss");
    }
    //显示修改页面
    private void ShowModifyPage()
    {
        string warehouseid = this.Request.QueryString["warehouse_id"];
        if (string.IsNullOrEmpty(warehouseid))
        {
            ShowCreatePage();
        }
        else
        {
            initUnitTree();
            btnSave.Visible = true;
            LoadWarehourseInfo(warehouseid);
        }
    }
    //显示新建页面
    private void ShowCreatePage()
    {
        initUnitTree();
        btnSave.Visible = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        var warehourse = new WarehouseInfo();
        warehourse.Unit.Id = DropDownTree1.SelectedValue;
        //少一个所属单位
        warehourse.Code = tbCode.Text.Trim();
        warehourse.Name = tbName.Text.Trim();
        warehourse.EnglishName = tbEnglishName.Text.Trim();
        warehourse.Address = tbAddress.Text.Trim();
        warehourse.Contacter = tbContacter.Text.Trim();
        warehourse.Tel = tbTel.Text.Trim();
        warehourse.Fax = tbFax.Text.Trim();
        warehourse.IsDefault = int.Parse(cbDefault.SelectedValue);
        warehourse.StatusDescription = tbStatus.Text.Trim();
        warehourse.Remark = tbRemark.Text.Trim();
        var opertype = this.Request.QueryString["oper_type"];
        if (opertype == "2")//新建
        {
            if (posservice.ExistWarehouseCode("", tbCode.Text.Trim()))
            {
                this.InfoBox.ShowPopError("编码已经存在请输入新的编码！");
                return;
            }
            else
            {
                warehourse.ID = "";
                warehourse.CreateTime = DateTime.Now;
                if (posservice.InsertWarehouse(loggingSessionInfo, warehourse))
                {
                    this.Redirect("新建仓库成功", InfoType.Info, this.Request.QueryString["from"] ?? "warehouse_query.aspx");
                }
                else 
                {
                    this.InfoBox.ShowPopError("新建仓库失败");
                }
            }
 
        }
        else if (opertype == "3")//修改
        {
            var warehourseid=this.Request.QueryString["warehouse_id"];
            warehourse.ModifyTime = DateTime.Now;
            if (posservice.ExistWarehouseCode(warehourseid, tbCode.Text.Trim()))
            {
                this.InfoBox.ShowPopError("编码已经存在请输入新的编码");
                return;
            }
            else
            {
                warehourse.ID = warehourseid;
                if (posservice.ModifyWarehouse(loggingSessionInfo,warehourse))
                {
                    this.Redirect("修改仓库成功", InfoType.Info, this.Request.QueryString["from"] ?? "warehouse_query.aspx");
                }
                else
                {
                    this.InfoBox.ShowPopError("修改仓库失败");
                }
            }
 
        }
        else
        {
            throw new Exception();
        }
    }
    protected void disabledAllInput(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c is TextBox)
            {
                var temp = c as TextBox;
                temp.ReadOnly = true;
            }
            if( c is  DropDownList)
            {
                var temp = c as DropDownList;
                temp.Enabled = false;
            }
            if (c.Controls.Count > 0)
                disabledAllInput(c);
        }
 
    }
}