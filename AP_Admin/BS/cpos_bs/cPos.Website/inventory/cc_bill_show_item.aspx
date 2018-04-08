<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="cc_bill_show_item.aspx.cs" Inherits="inventory_cc_bill_show_item" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
             .hidden
        {
            display:none;
        }
    </style>
    <script type="text/javascript">
        //添加验证规则
        function checkInput() {
            var value = $("#txtSkuid ")[0].item;
            if (!value) {
                this.infobox.showPop("请选择商品!");
                $("#<%=txtKeyword.ClientID %>").focus();
                return false;
            }
            var pirce = /^\-?\d{1,10}$|^\-?\d{1,10}\.?\d{1,4}$/;
            var number = /^\-?\d{1,10}$|^\-?\d{1,10}\.?\d{1,4}$/;
            if ($("#<%=tbSalesPrice.ClientID %>").val() == "") {
                $("#<%=tbSalesPrice.ClientID %>").focus();
                this.infobox.showPop("零售价不能为空!");
                return false;
            }
            if ($("#<%=tbPurchasePrice.ClientID %>").val() == "") {
                $("#<%=tbPurchasePrice.ClientID %>").focus();
                this.infobox.showPop("采购价不能为空!");
                return false;
            }
            if ($("#<%=tbEndQty.ClientID %>").val() == "") {
                $("#<%=tbEndQty.ClientID %>").focus();
                this.infobox.showPop("库存数不能为空!");
                return false;
            }
            if ($("#<%=tbOrderQty.ClientID %>").val() == "") {
                $("#<%=tbOrderQty.ClientID %>").focus();
                this.infobox.showPop("盘点数不能为空!");
                return false;
            }
            if (!pirce.test($("#<%=tbSalesPrice.ClientID %>").val())) {
                $("#<%=tbSalesPrice.ClientID %>").focus();
                alert("零售价格式不正确!");
                return false;
            }
            if (!pirce.test($("#<%=tbPurchasePrice.ClientID %>").val())) {
                $("#<%=tbPurchasePrice.ClientID %>").focus();
                alert("采购价格式不正确!");
                return false;
            }
            if (!number.test($("#<%=tbEndQty.ClientID %>").val())) {
                $("#<%=tbEndQty.ClientID %>").focus();
                alert("库存数格式不正确!");
                return false;
            }
            if (!number.test($("#<%=tbOrderQty.ClientID %>").val())) {
                $("#<%=tbOrderQty.ClientID %>").focus();
                alert("盘点数格式不正确!");
                return false;
            }
            return true;
        }

        function getSkuInfo() {
            if (!checkInput()) {
                return false;
            }
            var obj = $("#txtSkuid")[0].item;
            if (obj) {
                obj.salesPrice = document.getElementById("<%=tbSalesPrice.ClientID%>").value;
                obj.purchasePrice = document.getElementById("<%=tbPurchasePrice.ClientID%>").value;
                obj.end_qty = document.getElementById("<%=tbEndQty.ClientID%>").value;
                obj.order_qty = document.getElementById("<%=tbOrderQty.ClientID%>").value;
                obj.difference_qty = parseFloat(obj.end_qty) - parseFloat(obj.order_qty);  //差异数
            }
            window.returnValue = obj;
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
                $("#<%=tbSalesPrice.ClientID %>").val("");
                $("#<%=tbPurchasePrice.ClientID %>").val("");
                $("#<%=tbEndQty.ClientID %>").val("");
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
    <table cellspacing="0" cellpadding="1" border="0" class=" con_tab" style="margin-left: auto;
        margin-right: auto;">
        <tr>
            <td class=" td_co" >
                商品查询：
            </td>
            <td colspan="3">
                <div class="box_r">
                </div>
                <asp:TextBox Width="91%" MaxLength="100" runat="server" ID="txtKeyword"></asp:TextBox>
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
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox runat="server" ID="tbItemName" Width="150px" ReadOnly="true" CssClass="ban_in_oper_time"></asp:TextBox>
            </td>
            <td class="td_co"  >
                商品编码：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox runat="server" ID="tbItemCode" Width="150px" ReadOnly="true" CssClass="ban_in_oper_time"></asp:TextBox>
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
                零售价：
            </td>
            <td>
                <div class="box_r"></div>
                <asp:TextBox  runat="server" MaxLength="15" Width="150px" ID="tbSalesPrice"></asp:TextBox>
            </td>
            <td class="td_co" >
                采购价：
            </td>
            <td>
                <div class="box_r"></div>
                <asp:TextBox  runat="server" MaxLength="15" Width="150px" ID="tbPurchasePrice"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co" >
                库存数：
            </td>
            <td>
                <div class="box_r"></div>
                <asp:TextBox  runat="server" MaxLength="15" Width="150px" ID="tbEndQty"></asp:TextBox>
            </td>
            <td class="td_co" >
                盘点数：
            </td>
            <td>
                <div class="box_r"></div>
                <asp:TextBox  runat="server" MaxLength="15" Width="150px" ID="tbOrderQty"></asp:TextBox>
            </td>
        </tr>
    </table>
    <input type="hidden" id="txtSkuid" />
    <div class="bf">
        <input type="button" onclick="getSkuInfo();" value="确认" class="input_bc" />
        <input type="button" onclick="window.returnValue=null;window.close();" value="关闭"
            class="input_fh" />
    </div>
</asp:Content>
