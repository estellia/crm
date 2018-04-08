<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="inout_bill_out_show_item.aspx.cs" Inherits="inventory_inout_bill_out_show_item" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
             .hidden
        {
            display:none;
        }
        .llinput
        {
            background: url(../img/butt01.png) no-repeat;
            width: 65px;
            height: 26px;
            color: #0e6099;
            font-weight: bold;
            border: 0px;
            margin-right: 10px;
        }
    .style1
    {
        height: 26px;
    }
    </style>
    <script type="text/javascript">
        function confirm() {
            var order_discount_rate = window.dialogArguments;
            if (!$("#sku_id")[0].item) {
                this.infobox.showPop("请选择商品");
                $("#<%=selSku.ClientID %>").focus();
                return false;
            }
            if (checkInput()) {
                var value = $("#sku_id")[0].item;
                value.order_qty = $("#<%= tbOrderQty.ClientID%>").val();
                value.enter_qty = $("#<%=tbEnterQty.ClientID %>").val();
                value.std_price = $("#<%=std_price.ClientID %>").val();
                value.discount_rate = $("#<%=discount_rate.ClientID %>").val();
                value.order_discount_rate = order_discount_rate;
                value.enter_price = (parseFloat(order_discount_rate) * parseFloat(value.std_price)).toFixed(2);
                value.retail_price = (parseFloat(value.enter_price) * parseFloat(value.discount_rate)).toFixed(2);
                value.retail_amount = (parseInt(value.enter_qty) * parseFloat(value.retail_price)).toFixed(2);
                window.returnValue = value;
                window.close();
            }
        }
        function cancle() {
            window.returnValue = undefined;
            window.close();
        }
    </script>
    <script type="text/javascript">
        autocomplete.searchboxid = "<%=selSku.ClientID %>";
        autocomplete.currentValue = null;
        autocomplete.search = function () {
            if (autocomplete.IsFocus && autocomplete.searchBarObj != null) {
                if (autocomplete.currentValue == autocomplete.searchBarObj.val())
                    return;
                autocomplete.showboxselectindex = -1;
                autocomplete.selectValue = null;
                autocomplete.showlist = [];
                autocomplete.currentValue = autocomplete.searchBarObj.val();
                if (autocomplete.currentValue.trim() == "") { autocomplete.hidshowbox(); return; }
                autocomplete.searchValue = autocomplete.currentValue;
                //post|get 自己修改
                var paras = { key: autocomplete.currentValue };
                if (autocomplete.searchpara != null) {
                    paras = autocomplete.searchpara();
                    paras.key = autocomplete.currentValue
                }
                $.post("../ajaxhandler/query.ashx?action=searchskuinfo", { keyword: autocomplete.searchValue }, function (data) {
                    autocomplete.showboxselectindex = -1;
                    if (data && data.Success) {
                        autocomplete.showlist = data.Rult;
                        autocomplete.showUL();
                    } else autocomplete.hidshowbox();
                })
            }
        }
        //Builder Li标记
        autocomplete.BuildLiStr = function () {
            var stringBuffer = [];
            for (var i = 0; i < autocomplete.showlist.length; i++) {
                var maintxt;
                maintxt = autocomplete.showlist[i].item_code + "/" + autocomplete.showlist[i].item_name + "\/" + (autocomplete.showlist[i].barcode==null?"":autocomplete.showlist[i].barcode + "\/") + (autocomplete.showlist[i].prop_1_detail_name == null ? "" : (autocomplete.showlist[i].prop_1_detail_name + "\/")) + (autocomplete.showlist[i].prop_2_detail_name == null ? "" : (autocomplete.showlist[i].prop_2_detail_name + "\/")) + (autocomplete.showlist[i].prop_3_detail_name == null ? "" : (autocomplete.showlist[i].prop_3_detail_name + "\/")) + (autocomplete.showlist[i].prop_4_detail_name == null ? "" : (autocomplete.showlist[i].prop_4_detail_name + "\/")) + (autocomplete.showlist[i].prop_5_detail_name == null ? "" : (autocomplete.showlist[i].prop_5_detail_name + "\/"));
                //var reg = new RegExp(autocomplete.searchValue, "gim");
                //if (autocomplete.Ishighlight) maintxt = autocomplete.RegChange(maintxt, reg);
                var str = String.Format("<li flag='{0}' >{1}</li>", i, maintxt);
                stringBuffer.push(str);
            }
            return stringBuffer.join("");
        }
        //li 变色
        autocomplete.UL_hover = function (index, Istxtshow) {
            var list_li = autocomplete.showBoxObj.children("li");

            list_li.css("background", autocomplete.licolor);
            for (var i = 0; i < list_li.length; i++) {
                if (i == index) {
                    list_li.eq(index).css("background", autocomplete.lihover);
                    if (list_li.length > 10 && Istxtshow)
                        autocomplete.showBoxObj.scrollTop(20 * index);
                    if (Istxtshow) {
                        autocomplete.currentValue = autocomplete.showlist[i].item_name;
                        autocomplete.searchBarObj.val(autocomplete.currentValue);
                    }
                    else { autocomplete.showboxselectindex = index; }
                }
            }
        }
        //用户选择某一项时
        autocomplete.finish = function () {
            if (autocomplete.selectValue != null) {
                $("#<%=tbItemName.ClientID%>").val(autocomplete.selectValue.item_name);
                $("#<%=tbItemCode.ClientID%>").val(autocomplete.selectValue.item_code);
                $("#prop_1_detail_name").val((autocomplete.selectValue.prop_1_detail_name == null ? "" : autocomplete.selectValue.prop_1_detail_name));
                $("#prop_2_detail_name").val((autocomplete.selectValue.prop_2_detail_name == null ? "" : autocomplete.selectValue.prop_2_detail_name));
                $("#prop_3_detail_name").val((autocomplete.selectValue.prop_3_detail_name == null ? "" : autocomplete.selectValue.prop_3_detail_name));
                $("#prop_4_detail_name").val((autocomplete.selectValue.prop_4_detail_name == null ? "" : autocomplete.selectValue.prop_4_detail_name));
                $("#prop_5_detail_name").val((autocomplete.selectValue.prop_5_detail_name == null ? "" : autocomplete.selectValue.prop_5_detail_name));
                $("#<%=tbOrderQty.ClientID %>").val("");
                $("#<%=tbEnterQty.ClientID %>").val("");
                $("#<%=std_price.ClientID %>").val("");
                $("#<%=discount_rate.ClientID %>").val("1");
                $("#sku_id")[0].item = autocomplete.selectValue;
                autocomplete.currentValue = autocomplete.selectValue.item_name;
                $("#<%=selSku.ClientID %>").val(autocomplete.currentValue);
            }
        };

        //初始化 autocomplete
        $(function () { autocomplete.Initial(); });
    </script>
    <script type="text/javascript">
        function checkInput() {
            var pirce = /^\-?\d{1,10}$|^\-?\d{1,10}\.?\d{1,4}$/;
            var number = /^\-?\d{1,10}$|^\-?\d{1,10}\.?\d{1,4}$/;
            if (!validateNotEmpty($("#<%=tbOrderQty.ClientID %>"), "预订数")) {
                return false;
            }
            if (!number.test($("#<%=tbOrderQty.ClientID %>").val())) {
                $("#<%=tbOrderQty.ClientID %>").focus();
                alert("预订数格式不正确!");
                return false;
            }
            if (!validateNotEmpty($("#<%=tbEnterQty.ClientID %>"), "出库数")) {
                return false;
            }
            if (!number.test($("#<%=tbEnterQty.ClientID %>").val())) {
                $("#<%=tbEnterQty.ClientID %>").focus();
                alert("出库数格式不正确!");
                return false;
            }
            var std_price = $("#<%=std_price.ClientID %>").val();
            if (std_price == "") {
                $("#<%=std_price.ClientID %>").focus();
                alert("建议零售价不能为空!");
                return false;
            }
            else if (!pirce.test(std_price)) {
                $("#<%=std_price.ClientID %>").focus();
                alert("建议零售价格式不正确!");
                return false;
            }
            var discount_rate=$("#<%=discount_rate.ClientID %>").val();
            if(discount_rate=="") {
                $("#<%=discount_rate.ClientID %>").focus();
                alert("折上折不能为空!");
                return false;
            }
            else if(!pirce.test(discount_rate)) {
                $("#<%=discount_rate.ClientID %>").focus();
                alert("折上折格式不正确!");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="ss_bg" style="background: rgb(242, 241, 239);">
        <table cellpadding="0" cellspacing="0" class="con_tab" border="0" style="margin-left: auto;
            margin-right: auto">
            <tr >
                <td  class="td_co">
                    商品查询：
                </td>
                <td colspan="3">
                    <div class="box_r">
                    </div>
                    <asp:TextBox ID="selSku" runat="server" Width="93%"  MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_co">
                    &nbsp;
                </td>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td class="td_co"  >
                    商品名称：
                </td>
                <td >
                    <div class="box_r">
                    </div>
                    <asp:TextBox ID="tbItemName" ReadOnly="true" Width="150px" runat="server"  CssClass="fl ban_in_oper_time"
                        MaxLength="50"></asp:TextBox>
                </td>
                <td class="td_co"  >
                    商品编码：
                </td>
                <td >
                    <div class="box_r">
                    </div>
                    <asp:TextBox ID="tbItemCode" ReadOnly="true" Width="150px" runat="server"  CssClass="fl ban_in_oper_time"
                        MaxLength="30"></asp:TextBox>
                </td>
            </tr>
            <tr class="<%=this.SkuPropInfos.FirstOrDefault(obj=>obj.display_index==1)==null&&this.SkuPropInfos.FirstOrDefault(obj=>obj.display_index==2)==null?"hidden":"" %>">
                <td class="td_co" ><%= this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 1) != null ? this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 1).prop_name + "：" : "&nbsp;"%>
                    </td>
                <td>
                    <div class="<%=this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 1)!=null?"":"hidden" %>">
                        <div class="box_r">
                    </div>
                    <input type="text" style="width:150px" class="ban_in_oper_time fl" readonly="readonly"  id="prop_1_detail_name"/>
                    </div>
                </td>
                <td class="td_co" ><%= this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 2) != null ? this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 2).prop_name + "：" : "&nbsp;"%>
                    </td>
                <td>
                    <div class="<%=this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 2)!=null?"":"hidden" %>">
                        <div class="box_r">
                    </div>
                    <input type="text" style="width:150px" class="ban_in_oper_time fl" readonly="readonly"  id="prop_2_detail_name"/>
                    </div>
                </td>
            </tr>
            <tr class="<%=this.SkuPropInfos.FirstOrDefault(obj=>obj.display_index==3)==null&&this.SkuPropInfos.FirstOrDefault(obj=>obj.display_index==4)==null?"hidden":"" %>">
                <td class="td_co" ><%= this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 3) != null ? this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 3).prop_name + "：" : "&nbsp;"%>
                    </td>
                <td>
                    <div class="<%=this.SkuPropInfos.FirstOrDefault(obj=>obj.display_index==3)!=null?"":"hidden" %>">
                        <div class="box_r">
                    </div>
                    <input type="text" style="width:150px" class="ban_in_oper_time fl" readonly="readonly"  id="prop_3_detail_name"/>
                    </div>
                </td>
                <td class="td_co" ><%= this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 4) != null ? this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 4).prop_name + " ：" : "&nbsp;"%>
                   </td>
                <td>
                    <div class="<%=this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 4)!=null?"":"hidden" %>">
                        <div class="box_r">
                    </div>
                    <input type="text" style="width:150px" class="ban_in_oper_time fl" readonly="readonly"  id="prop_4_detail_name"/>
                    </div>
                </td>
            </tr>
            <tr class="<%=this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 5)==null?"hidden":"" %>">
                <td class="td_co" ><%= this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 5) != null ? this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 5).prop_name + " ：" : "&nbsp;"%>
                   </td>
                <td>
                    <div class="<%=this.SkuPropInfos.FirstOrDefault(obj => obj.display_index == 5)!=null?"":"hidden" %>">
                        <div class="box_r">
                    </div>
                    <input type="text" style="width:150px" class="ban_in_oper_time fl" readonly="readonly"  id="prop_5_detail_name"/>
                    </div>
                </td>
                <td class="td_co" >&nbsp;</td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="td_co" >
                    预订数：
                </td>
                <td>
                    <div class="box_r">
                    </div>
                    <asp:TextBox ID="tbOrderQty" runat="server" Width="150px"   MaxLength="15"></asp:TextBox>
                </td>
                <td class="td_co" >
                    出库数：
                </td>
                <td>
                    <div class="box_r">
                    </div>
                    <asp:TextBox ID="tbEnterQty" runat="server" Width="150px"   MaxLength="15"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td_co" >
                    建议零售价：
                </td>
                <td>
                    <div class="box_r">
                    </div>
                    <asp:TextBox ID="std_price" runat="server"  Width="150px"  MaxLength="15"></asp:TextBox>
                </td>
                <td class="td_co" >
                    折上折：
                </td>
                <td>
                    <div class="box_r">
                    </div>
                    <asp:TextBox ID="discount_rate" runat="server" Text="1"   Width="150px"
                        MaxLength="15"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="bf">
        <input type="button" class=" input_bc" id="btnSave" value="确认" onclick="confirm();" />
        <input type="button" class=" input_fh" id="btnCancle" value="关闭" onclick="cancle();" />
    </div>
    <input type="hidden" id="sku_id" />
</asp:Content>
