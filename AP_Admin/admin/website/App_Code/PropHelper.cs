using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///属性生成类
/// </summary>
public class PropHelper:PageBase
{
    private static PropHelper _propHelper = null;
    private static readonly object _locker = new object();
    public static PropHelper PropHelperSingleton
    {
        get
        {
            lock (_locker)
            {
                if (_propHelper == null)
                    _propHelper = new PropHelper();
            }
            return _propHelper;
        }
    }
	private PropHelper()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public cPos.Admin.Service.PropService PropService
    {
        get
        {
            return new cPos.Admin.Service.PropService();
        }
    }
    public cPos.Admin.Service.SkuPropServer SkuPropService
    {
        get
        {
            return new cPos.Admin.Service.SkuPropServer();
        }
    }
    private IList<cPos.Model.PropInfo> GetPropGroupList(string domin)
    {
        var grouplist = new List<cPos.Model.PropInfo>();
        try
        {
            return this.PropService.GetPropListFirstByDomain(loggingSessionInfo, domin);
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            return grouplist;
        }
    }
    //获取sku属性集合
    private IList<cPos.Model.SkuPropInfo> GetSkuPropList()
    {
        var skuList = new List<cPos.Model.SkuPropInfo>();
        try
        {
            return this.SkuPropService.GetSkuPropList(loggingSessionInfo);
        }
        catch (Exception ex)
        {
            PageLog.Current.Write(ex);
            return skuList;
        }
    }
    //获取属性（明细）集合
    private IList<cPos.Model.PropInfo> GetPropList(string parent_id, string domin)
    {
        try
        {
            return this.PropService.GetPropListByParentId(loggingSessionInfo, domin, parent_id);
        }
        catch(Exception ex)
        {
            PageLog.Current.Write(ex);
            return null;
        }
    }
    #region 生成sku表头
    public string CreationSkuProp()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        var source = this.GetSkuPropList();
        if (source == null || source.Count == 0)
            return sb.ToString();
        foreach (var item in source)
        {
            sb.Append("<tr><td class=\"td_co\">");
            sb.Append(item.prop_name+"：");
            sb.Append("</td>");
            sb.Append("<td class=\"td_lp\">");
            sb.Append(CreationSkuPropDetail(item));
            sb.Append("</td>");
            sb.Append("</tr>");
        }
        return sb.ToString();
    }

    private string CreationSkuPropDetail(cPos.Model.SkuPropInfo prop)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        switch (prop.prop_input_flag)
        {
            case "text": sb.Append("<input prop_name=\"" + prop.prop_name + "\" columnindex=\"" + prop.display_index + "\" input_flag=\"text\" sku_prop_id=\"" + prop.prop_id + "\" class=\"itemSku\" type=\"text\" id=\"" + prop.prop_id + "\" />"); break;
            case "select": CreationSkuSelect(sb, prop); break;
            case "label": CreationSkuLabel(sb, prop); break;
            case "select-date-(yyyy-MM)": CreationSkuselectDate(sb, prop, "short"); break;
            case "select-date-(yyyy-MM-dd)": CreationSkuselectDate(sb, prop, "full"); break;
            case "radio": CreationSkuRadio(sb, prop); break;
            default: break;
        }
        return sb.ToString();
    }
    private void CreationSkuLabel(System.Text.StringBuilder sb, cPos.Model.SkuPropInfo prop)
    {
        sb.Append("<label ");
        sb.Append("columnindex = \"" + prop.display_index + "\" input_flag=\"" + prop.prop_input_flag + "\" sku_prop_id=\"" + prop.prop_id + "\" class=\"itemSku\" ");
        sb.Append("prop_name=\"" + prop.prop_name + "\" id=\"" + prop.prop_id + "\">" + prop.prop_name + "</label>");
    }
    private void CreationSkuselectDate(System.Text.StringBuilder sb, cPos.Model.SkuPropInfo prop, string type)
    {
        var onchangeFunc = "";
        if (type == "short")
        {
            onchangeFunc = "getShortDate(this);";
        }
        sb.Append("<input ");
        sb.Append("columnindex = \"" + prop.display_index + "\" input_flag=\"" + prop.prop_input_flag + "\" sku_prop_id=\"" + prop.prop_id + "\" class=\"itemSku\" ");
        sb.Append("id=\"" + prop.prop_id + "\" type=\"text\" readonly=\"readonly\" onclick=\"Calendar('" + prop.prop_id + "');\" ");
        sb.Append("title=\"双击清除日期\" ondblclick=\"this.value='';\" ");
        sb.Append("onchange=\"" + onchangeFunc + "\" />");
    }
    private void CreationSkuRadio(System.Text.StringBuilder sb, cPos.Model.SkuPropInfo prop)
    {
        var items = GetPropList(prop.prop_id, "ITEM");
        if (items == null || items.Count == 0)
            return;
        foreach (var item in items)
        {
            sb.Append("<input ");
            sb.Append("columnindex = \"" + prop.display_index + "\" input_flag=\"" + prop.prop_input_flag + "\" sku_prop_id=\"" + prop.prop_id + "\" class=\"itemSku\" ");
            sb.Append("type=\"radio\" prop_name=\"" + prop.prop_name + "\" name=\"" + prop.prop_id + "\" class=\"_prop_detail_radio\" PropertyDetailId=\"" + prop.prop_id + "\"  id=\"" + item.Prop_Id + "\" />");
            sb.Append("<label for=\"" + item.Prop_Id + "\">" + item.Prop_Name + "</label>");
            sb.Append("&nbsp;&nbsp;");
        }
    }
    private void CreationSkuSelect(System.Text.StringBuilder sb, cPos.Model.SkuPropInfo prop)
    {
        var items = GetPropList(prop.prop_id, "ITEM");
        if (items == null || items.Count == 0)
            return;
        sb.Append("<select columnindex=\"" + prop.display_index + "\" class=\"itemSku\" input_flag=\"" + prop.prop_input_flag + "\" sku_prop_id=\"" + prop.prop_id + "\"  id=\"" + prop.prop_id + "\" prop_name=\"" + prop.prop_name + "\" >");
        foreach (var item in items)
        {
            sb.Append("<option id=\"" + item.Prop_Id + "\" value=\"" + item.Prop_Id + "\">" + item.Prop_Name + "</option>");
        }
        sb.Append("</select>");
    }
    #endregion



    #region 生成属性
    /// <summary>
    /// 生成属性组、属性以及属性详细
    /// </summary>
    /// <param name="domin">属性域</param>
    /// <returns>属性字符串</returns>
    public string CreationPropGroup(string domin)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        var items = GetPropGroupList(domin);
        if (items == null || items.Count == 0)
            return sb.ToString();
        foreach (var item in items)
        {
            sb.Append("<div class=\"tit_con\"><span>" + item.Prop_Name + "</span></div>");
            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"con_tab\">");
            sb.Append(CreationProp(item, domin));
            sb.Append("</table>");
        }
        return sb.ToString();
    }
    private string CreationProp(cPos.Model.PropInfo prop, string domin)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        var items = GetPropList(prop.Prop_Id, domin);
        if (items == null || items.Count == 0)
        {
            return sb.ToString();
        }
        foreach (var item in items)
        {
            sb.Append("<tr><td class=\"td_co\">");
            sb.Append(item.Prop_Name + "：");
            sb.Append("</td>");
            sb.Append("<td class=\"td_lp\">");
            sb.Append(CreationPropDetail(item, domin));
            sb.Append("</td>");
            sb.Append("</tr>");
        }
        return sb.ToString();
    }
    private string CreationPropDetail(cPos.Model.PropInfo prop, string domin)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        switch (prop.Prop_Input_Flag)
        {
            case "text": sb.Append("<input class=\"_prop_detail\" id=\"" + prop.Prop_Id + "\" type=\"text\" prop_name=\"" + prop.Prop_Name + "\" maxlength=\"" + (prop.Prop_Max_Length==0?30:prop.Prop_Max_Length) + "\" />"); break;
            case "select": CreationSelect(sb, prop, domin); break;
            case "label": CreationLable(sb, prop, domin); break;
            case "select-date-(yyyy-MM)": CreationDate(sb, prop, domin, "short"); break;
            case "select-date-(yyyy-MM-dd)": CreationDate(sb, prop, domin, "full"); break;
            case "radio": CreationRadio(sb, prop, domin); break;
            default: break;
        }
        return sb.ToString();
    }
    private void CreationRadio(System.Text.StringBuilder sb, cPos.Model.PropInfo prop, string domin)
    {
        var items = GetPropList(prop.Prop_Id, domin);
        if (items == null || items.Count == 0)
            return;
        foreach (var item in items)
        {
            sb.Append("<input type=\"radio\" prop_name=\"" + prop.Prop_Name + "\" name=\"" + prop.Prop_Id + "\" class=\"_prop_detail_radio\" PropertyDetailId=\"" + prop.Prop_Id + "\"  id=\"" + item.Prop_Id + "\" />");
            sb.Append("<label for=\"" + item.Prop_Id + "\">" + item.Prop_Name + "</label>");
            sb.Append("&nbsp;&nbsp;");
        }
    }
    private void CreationDate(System.Text.StringBuilder sb, cPos.Model.PropInfo prop, string domin,string type)
    {
        var onchangeFunc = "";
        if (type == "short")
        {
            onchangeFunc = "getShortDate(this);";
        }
        sb.Append("<input id=\"" + prop.Prop_Id + "\" type=\"text\" class=\"_prop_detail\" readonly=\"readonly\" onclick=\"Calendar('" + prop.Prop_Id + "');\" ");
        sb.Append("title=\"双击清除日期\" ondblclick=\"this.value='';\" ");
        sb.Append("onchange=\"" + onchangeFunc + "\" />");
    }
    private void CreationLable(System.Text.StringBuilder sb, cPos.Model.PropInfo prop, string domin)
    {
        sb.Append("<label prop_name=\"" + prop.Prop_Name + "\" class=\"_prop_detail\" id=\"" + prop.Prop_Id + "\">" + prop.Prop_Name + "</label>");
    }
    private void CreationSelect(System.Text.StringBuilder sb,cPos.Model.PropInfo prop,string domin)
    {
        var items = GetPropList(prop.Prop_Id, domin);
        if (items == null || items.Count == 0)
            return;
        sb.Append("<select class=\"_prop_detail\" id=\"" + prop.Prop_Id + "\" prop_name=\"" + prop.Prop_Name + "\" >");
        foreach (var item in items)
        {
            sb.Append("<option id=\"" + item.Prop_Id + "\" value=\"" + item.Prop_Id + "\">" + item.Prop_Name + "</option>");
        }
        sb.Append("</select>");
    }
    #endregion
}

[Serializable]
public class UnitDetailDTO
{
    public string order_id { get; set; }
    public string create_time { get; set; }
    public string create_user_name { get; set; }
    public string sales_user { get; set; }
    public string order_no { get; set; }
    public string vip_no { get; set; }
    public string payment_name { get; set; }
    public decimal total_amount { get; set; }
}