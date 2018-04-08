<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="adjust_bill_show_item.aspx.cs" Inherits="inventory_adjust_bill_show_item" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<style type="text/css">
         .hidden
        {
            display:none;
        }
</style>
    <script type="text/javascript">
        function checkInput() {
            var value = $("#txtSkuid ")[0].item;
            if (!value) {
                this.infobox.showPop("请选择商品!");
                $("#<%=txtKeyword.ClientID %>").focus();
                return false;
            }
            if ($("#<%=tbOrderQty.ClientID %>").val() == "") {
                $("#<%=tbOrderQty.ClientID %>").focus();
                this.infobox.showPop("库存数不能为空!");
                return false;
            }
            var pirce = /^\-?\d{1,10}$|^\-?\d{1,10}\.?\d{1,4}$/;
            var number = /^\-?\d{1,10}$|^\-?\d{1,10}\.?\d{1,4}$/;
            if (!number.test($("#<%=tbOrderQty.ClientID %>").val())) {
                this.infobox.showPop("库存数格式不正确!");
                $("#<%=tbOrderQty.ClientID %>").focus();
                return false;
            }
            if ($("#<%=tbEnterQty.ClientID %>").val() == "") {
                $("#<%=tbEnterQty.ClientID %>").focus();
                this.infobox.showPop("调整数不能为空!");
                return false;
            }
            if (!number.test($("#<%=tbEnterQty.ClientID %>").val())) {
                $("#<%=tbEnterQty.ClientID %>").focus();
                this.infobox.showPop("调整数格式不正确!");
                return false;
            }
            return true;
        }
        function confirm() {
            var value = $("#txtSkuid ")[0].item;
            if (value) {
                value.order_qty = $("#<%=tbOrderQty.ClientID %>").val();
                value.enter_qty = $("#<%=tbEnterQty.ClientID %>").val();
            }
            window.returnValue = value;
            window.close();
        }
        function cancle() {
            window.returnValue = undefined;
            window.close();
        }
        autocomplete.searchboxid = "<%=txtKeyword.ClientID %>";
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
                maintxt = autocomplete.showlist[i].item_code + "/" + autocomplete.showlist[i].item_name + "\/" + (autocomplete.showlist[i].barcode == null ? "" : autocomplete.showlist[i].barcode + "\/") + (autocomplete.showlist[i].prop_1_detail_name == null ? "" : (autocomplete.showlist[i].prop_1_detail_name + "\/")) + (autocomplete.showlist[i].prop_2_detail_name == null ? "" : (autocomplete.showlist[i].prop_2_detail_name + "\/")) + (autocomplete.showlist[i].prop_3_detail_name == null ? "" : (autocomplete.showlist[i].prop_3_detail_name + "\/")) + (autocomplete.showlist[i].prop_4_detail_name == null ? "" : (autocomplete.showlist[i].prop_4_detail_name + "\/")) + (autocomplete.showlist[i].prop_5_detail_name == null ? "" : (autocomplete.showlist[i].prop_5_detail_name + "\/"));
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
                $("#<%=tbEnterQty.ClientID %>").val("");
                $("#<%=tbOrderQty.ClientID %>").val("");
                $("#txtSkuid")[0].item = autocomplete.selectValue;
                autocomplete.currentValue = autocomplete.selectValue.item_name;
                $("#<%=txtKeyword.ClientID %>").val(autocomplete.currentValue);
            }
        };

        //初始化 autocomplete
        $(function () { autocomplete.Initial(); });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table  cellspacing="0" cellpadding="0" border="0" class=" con_tab" style=" margin-left:auto; margin-right:auto;">
        <tr>
            <td class=" td_co" >
                商品查询：
            </td>
            <td colspan="3">
                <div class="box_r"></div>
                <asp:TextBox Width="91%" CssClass="fl" MaxLength="100" runat="server" ID="txtKeyword" ></asp:TextBox> 
            </td>
        </tr>
        <tr>
            <td class="td_co">
                &nbsp;
            </td>
            <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td class=" td_co"  >
                商品名称：
            </td>
            <td >
                <div class="box_r"></div>
                <asp:TextBox  CssClass="fl ban_in_oper_time" Width="150px" runat="server" ID="tbItemName" ReadOnly="true"></asp:TextBox>
            </td>
            <td class="td_co"  >
                商品编码：
            </td>
            <td >
                <div class="box_r"></div>
                <asp:TextBox  CssClass="fl ban_in_oper_time" Width="150px" runat="server" ID="tbItemCode" attr="tbProp1" ReadOnly="true"></asp:TextBox>
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
            <td class=" td_co" >
                库存数：
            </td>
            <td>
                <div class="box_r"></div>
                <asp:TextBox  runat="server" MaxLength="15" Width="150px" ID="tbOrderQty"></asp:TextBox>
            </td>
            <td class="td_co" >
                调整数：
            </td>
            <td>
                <div class="box_r"></div>
                <asp:TextBox  runat="server" MaxLength="15" Width="150px" ID="tbEnterQty"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="display:none">
        <input type="text" id="txtSkuid"/>
    </div>
    <div class="bf">
        <%--<asp:Button ID="btnSave" runat="server" Text="确认" class="input_bc" OnClientClick="if(!CheckRepeat()) return false;window.returnValue=getSkuInfo();window.close();" />--%>
        <input type="button" onclick="if(!checkInput()) return false;confirm();" value="确认" class="input_bc" />
        <input type="button" onclick="cancle();" value="关闭" class=" input_fh" />
    </div>
</asp:Content>
