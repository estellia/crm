using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using cPos.Service;
using cPos.Model;

public partial class unit_unit_show : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitAreaTree();
            loadUnitType();
            LoadUnitsInfo();
            if (this.Request.Params["strDo"] == "Visible")
            {
                //获取数据
                ShowUnitData();
                btnSave.Visible = false;
                DisableAllInput(this);//只能显示信息，不能修改
            }
            else if (this.Request.Params["strDo"] == "Modify")
            {
                ShowUnitData();
            }
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        btn.Enabled = false;
        try
        {
            var data = SaveUnitData();
            string s = (new UnitService()).SetUnitInfo(loggingSessionInfo, data);
            if (s == "保存成功!")
            {
                this.Redirect(s, InfoType.Info, this.Request.QueryString["from"] ?? "unit_query.aspx");
            }
            else
            {
                this.InfoBox.ShowPopError(s);
            }
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("保存失败！");
        }
        btn.Enabled = true;
    }
    #region 新加内容
    //组织属性集合
    protected string UnitPropListInfo
    {
        get
        {
            if (ViewState["unitprop"] == null)
            {
                ViewState["unitprop"] = "[]";
            }
            return ViewState["unitprop"] as string;
        }
        set
        {
            ViewState["unitprop"] = value;
        }
    }
    #endregion



    //点击显示有关商铺的信息：
    protected void btnStoreInfo_Click(object sender, EventArgs e)
    {

    }
    private void loadUnitType()
    {
        if ((new TypeService()).GetTypeInfoListByDomain(loggingSessionInfo, "UnitType") != null)
        {
            ddlUnitType.DataSource = (new TypeService()).GetTypeInfoListByDomain(loggingSessionInfo, "UnitType");
            ddlUnitType.DataBind();
        }
    }
    private void InitAreaTree()
    {
        var serv = new CityService();
        var list = serv.GetProvinceList(loggingSessionInfo).OrderBy(obj => obj.City_Code).Select(obj =>
                   new controls_DropDownTree.tvNode
                   {
                       Complete = false,
                       Text = obj.City1_Name,
                       Value = obj.City_Id,
                       Id = obj.City_Code,
                   });

        DropDownTree1.DataBind(list);

        DropDownTree1.SelectedText = "";
        DropDownTree1.SelectedValue = ""; 
    }
    //加载组织信息
    private void LoadUnitsInfo()
    {
        try
        {
            var service = new UnitService(); 
            var source = service.GetRootUnitsByDefaultRelationMode(loggingSessionInfo);
            this.drpUnit.DataBind(source.Select(obj =>
                new controls_DropDownTree.tvNode
                {
                    CheckState = false,
                    Complete = false,
                    ShowCheck = false,
                    Value = obj.Id,
                    Text = obj.Name
                }
            ).ToArray());
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            this.InfoBox.ShowPopError("加载数据出错！");
        }
    }
    private void ShowUnitData()
    {
        UnitInfo ui = new UnitInfo();
        if (this.Request.Params["unit_id"] != null)
        {
            ui = (new UnitService()).GetUnitById(loggingSessionInfo, this.Request.Params["unit_id"].ToString());
            //this.selParentUnit.Text = ui.Parent_Unit_Id;
            var service = new UnitService();
            
            this.drpUnit.SelectedValue = ui.Parent_Unit_Id;
            if (!string.IsNullOrEmpty(ui.Parent_Unit_Id)&&ui.Parent_Unit_Id!="-99")
            {
                var _unit = service.GetUnitById(loggingSessionInfo, ui.Parent_Unit_Id);
                if (_unit != null)
                {
                    this.drpUnit.SelectedText = _unit.Name;
                }
            }
            this.tbUnitCode.Text = ui.Code;
            this.tbUnitName.Text = ui.Name;
            this.tbUnitNameEn.Text = ui.EnglishName;
            this.tbUnitShortName.Text = ui.ShortName;
            this.ddlUnitType.SelectedValue = ui.TypeId;

            var serv = new CityService();
            var Cities = serv.GetCityInfoList(loggingSessionInfo);
            if (!string.IsNullOrEmpty(ui.CityId))
            {
                DropDownTree1.SelectedValue = ui.CityId;
                var item = Cities.FirstOrDefault(obj => obj.City_Id ==ui.CityId);
                if (item != null)
                {
                    DropDownTree1.SelectedText = item.City1_Name + @"\" + item.City2_Name + @"\" + item.City3_Name;
                }
            }
            this.tbAddress.Text = ui.Address;
            this.tbPostcode.Text = ui.Postcode;
            this.tbContact.Text = ui.Contact;
            this.tbTelephone.Text = ui.Telephone;
            this.tbFax.Text = ui.Fax;
            this.tbEmail.Text = ui.Email;
            this.tbRemark.Text=ui.Remark;
            this.tbLongitude.Text = ui.longitude;
            this.tbDimension.Text = ui.dimension;
            //属性信息

            this.UnitPropListInfo = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ui.PropertyList);
        }
    }

    //存储表单数据。
    private UnitInfo SaveUnitData()
    {
        UnitInfo ui = new UnitInfo();
        if (!string.IsNullOrEmpty(this.Request.Params["unit_id"]))
        {
            ui = (new UnitService()).GetUnitById(loggingSessionInfo, this.Request.Params["unit_id"].ToString());
            //ui.Parent_Unit_Id = this.selParentUnit.Text;
            ui.Parent_Unit_Id = this.drpUnit.SelectedValue;
            ui.Code = this.tbUnitCode.Text;
            ui.Name = this.tbUnitName.Text;
            ui.EnglishName = this.tbUnitNameEn.Text;
            ui.ShortName = this.tbUnitShortName.Text;
            ui.TypeId = ddlUnitType.SelectedValue;
            //ui.TypeName = ddlUnitType.SelectedItem.ToString();
            ui.CityId = (DropDownTree1.SelectedValue??"").Split(',')[0];
            var service = new cPos.Service.CityService();
            var city = service.GetCityInfoList(loggingSessionInfo).Where(obj => obj.City_Id == ui.CityId).FirstOrDefault();
            ui.CityName = city.City1_Name + city.City2_Name + city.City3_Name;
            ui.Modify_Time = new BaseService().GetCurrentDateTime();
            ui.Modify_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            ui.Address = this.tbAddress.Text;
            ui.Postcode = this.tbPostcode.Text;
            ui.Contact = this.tbContact.Text;
            ui.Telephone = this.tbTelephone.Text;
            ui.Fax = this.tbFax.Text;
            ui.Email = this.tbEmail.Text;
            ui.Remark = this.tbRemark.Text;
            ui.dimension = this.tbDimension.Text;
            ui.longitude = this.tbLongitude.Text;
           
        }
        else
        {
            ui.TypeId = ddlUnitType.SelectedValue;
            ui.Parent_Unit_Id = this.drpUnit.SelectedValue;
            ui.Code = this.tbUnitCode.Text;
            ui.Id = null;
            ui.Create_Time = new BaseService().GetCurrentDateTime();
            ui.Create_User_Id = loggingSessionInfo.CurrentUser.User_Id;
            ui.Name = this.tbUnitName.Text;
            ui.EnglishName = this.tbUnitNameEn.Text;
            ui.ShortName = this.tbUnitShortName.Text;
            ui.TypeId = ddlUnitType.SelectedValue;
            //ui.TypeName = ddlUnitType.SelectedItem.ToString();
            ui.CityId = DropDownTree1.SelectedValue;
            var service = new cPos.Service.CityService();
            var city = service.GetCityById(loggingSessionInfo, ui.CityId);
            ui.CityName = city.City1_Name + city.City2_Name + city.City3_Name;
            ui.Address = this.tbAddress.Text;
            ui.Postcode = this.tbPostcode.Text;
            ui.Contact = this.tbContact.Text;
            ui.Telephone = this.tbTelephone.Text;
            ui.Fax = this.tbFax.Text;
            ui.Email = this.tbEmail.Text;
            ui.Remark = this.tbRemark.Text;
            ui.dimension = this.tbDimension.Text;
            ui.longitude = this.tbLongitude.Text;
        
        }
        //属性
        ui.PropertyList = GetUnitPropFormUI();
        return ui;
     }
    //从界面获得组织属性集合
    protected List<UnitPropertyInfo> GetUnitPropFormUI()
    {
        var source = new List<UnitPropertyInfo>();
        var value = (object[])new System.Web.Script.Serialization.JavaScriptSerializer().DeserializeObject(hid_unitPorp.Value);
        for (int i = 0; i < value.Length; i++)
        {
            var dic = (Dictionary<string, object>)value[i];
            var unitPropInfo = new UnitPropertyInfo();
            foreach (var item in dic)
            {
                switch (item.Key)
                {
                    case "PropertyDetailId": unitPropInfo.PropertyDetailId = (item.Value ?? "").ToString(); break;
                    case "PropertyCodeId": unitPropInfo.PropertyCodeId = (item.Value ?? "").ToString(); break;
                    case "PropertyDetailCode": unitPropInfo.PropertyDetailCode = (item.Value ?? "").ToString(); break;
                    default: break;
                }
            }
            source.Add(unitPropInfo);
        }
        if (source.Count == 0)
        {
            return null;
        }
        return source;
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
            else if (c is controls_DropDownTree)
            {
                var temp = c as controls_DropDownTree;
                temp.ReadOnly=true;
            }
            if (c.Controls.Count > 0)
                DisableAllInput(c);
        }
    }
}