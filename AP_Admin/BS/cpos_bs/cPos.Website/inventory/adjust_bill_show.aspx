﻿<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="adjust_bill_show.aspx.cs" Inherits="inventory_adjust_bill_show" %>

<%@ Register Src="../controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
          .hidden
        {
            display:none;
        }
        #inv_data td
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        var data = <%=this.InoutDetailInfoList %>;
    </script>
    <script type="text/javascript">
        function go_Add() {
            var value = openWindow("adjust_bill_show_item.aspx", 350, 720, '');
            if (value) {
                if (checkDetailExisits(value)) {
                    alert("sku信息已存在!");
                    return;
                }
                value.index = data.length;
                data.push(value);
                buildItem(data);
            }
        }
        function go_back() {
            this.location.href = '<%=this.Request.QueryString["from"] ?? "adjust_bill_query.aspx"%>';
        }
    </script>
    <script type="text/javascript">
        //创建table
        function buildItem() {
            var array = [];
            data.sort(function(x,y){
                return x.item_name.localeCompare(y.item_name);
            });
            for (var i = 0; i < data.length; i++) {
                array.push(buildItemTr(data[i], i));
            }
            $("#inv_data tr").remove();
            $(array).each(function () {
                $("#inv_data").append($(this));
            });
            getInvTableShowOrHide("inv_data","title1");
        }
        //创建一行tr
        function buildItemTr(item, index) {
            var show = "";
            if ('<%=this.ViewState["action"] %>' == "Visible") {
                show = "class=\"hidden\"";
            } else {
                show = "";
            }
            var _class = "";
            if (parseInt(index) % 2 == 0) {
                _class = "b_c4";
            } else {
                _class = "b_c5";
            }
            var tr = $("<tr class=\"" + _class + "\"><td>" + (index + 1) + "</td><td>" + (item.item_code==null?"&nbsp;":item.item_code) + "</td><td>" + (item.item_name==null?"&nbsp;":item.item_name) + "</td><td " + getClass(1) + ">" + (item.prop_1_detail_name==null?"&nbsp;":item.prop_1_detail_name) + "</td><td " + getClass(2) + ">" + (item.prop_2_detail_name==null?"&nbsp;":item.prop_2_detail_name) + "</td><td " + getClass(3) + ">" + (item.prop_3_detail_name==null?"&nbsp;":item.prop_3_detail_name) + "</td><td " + getClass(4) + ">" + (item.prop_4_detail_name==null?"&nbsp;":item.prop_4_detail_name) + "</td><td " + getClass(5) + ">" + (item.prop_5_detail_name==null?"&nbsp;":item.prop_5_detail_name) + "</td><td>" + item.order_qty + "</td><td>" + item.enter_qty + "</td><td><a href='#' " + show + " detail_id=\"" + item.order_detail_id + "\" index=\"" + index + "\" onclick=\"removeThis(this);\"><img src=\"../img/delete.png\" alt=\"\" title=\"删除\" /></a></td></tr>");
            return tr;
        }
        //该属性单元格是否显示
        function getClass(prop) {
            var isShow = false;
            $("th[index]").each(function () {
                if ($(this).attr("index") == prop) {
                    isShow = true;
                    return false;
                }
            });
            if (isShow) {
                return "class=\"\"";
            } else {
                return "class=\"hidden\"";
            }
        }

        //检查商品是否存在
        function checkDetailExisits(value) {
            var is_exi = false;
            $(data).each(function () {
                if (this.sku_id == value.sku_id) {
                    is_exi = true;
                    return false;
                }
            });
            return is_exi;
        }
        //移除
        function removeThis(sender) {
            $(sender).parent().parent().remove();
            getInvTableShowOrHide("inv_data","title1");
            var index = $(sender).attr("index");
            var detail_id = $(sender).attr("detail_id");
            $(data).each(function (ind, item) {
                if (item.index|item.index==0) {
                    if (item.index == index) {
                        data.removeValue(item);
                        return false;
                    }
                } else {
                    if (item.order_detail_id == detail_id) {
                        data.removeValue(item);
                        return false;
                    }
                }
            });
        }
        //page_load
        $(function () {
            buildItem();
            $("#<%=drpUnit.ClientID %>")[0].onchanged = function () {
                $("#<%=hidInout.ClientID %>").val(JSON.stringify(data));
                $("#<%=Button1.ClientID %>").click();
            };
        });
        //open a window 
        function openWindow(url, width, height, NewWin) {

            var popUpWin = 0;
            var left = 200;
            var top = 200;
            if (screen.width >= width) {
                left = Math.floor((screen.width - width) / 2);
            }
            if (screen.height >= height) {
                top = Math.floor((screen.height - height) / 2);
            }

            var from = window.showModalDialog(url, null, "dialogHeight=" + width + "px;dialogWidth=" + height + "px;dialogTop=" + top + "px;dialogLeft=" + left + "px;help=no;scroll=no;");
            return from;
        }
        //检出输入数据是否合法
        function checkInput() {
            if ($("#<%=tbOrderNo.ClientID %>").val() == "") {
                this.infobox.showPop("调整单号不能为空!");
                $("#<%=tbOrderNo.ClientID %>").focus();
                return false;
            }
            if (!$("#<%=selReasonType.ClientID%>").val()) {
                $("#<%=selReasonType.ClientID %>").focus();
                alert("必须选择调整类型!");
                return false;
            }
            if (!$("#<%=drpUnit.ClientID %>")[0].values()[0]) {
                $("#<%=drpUnit.ClientID %>").focus();
                alert("必须选择调整单位!");
                return false;
            }
            if (!$("#<%=selWarehouse.ClientID %>").val()) {
                $("#<%=selWarehouse.ClientID %>").focus();
                alert("必须选择调整仓库!");
                return false;
            }
            if ($("#<%=selOrderDate.ClientID %>").val() == "") {
                this.infobox.showPop("单据日期不能为空!");
                $("#<%=selOrderDate.ClientID %>").focus();
                return false;
            }
            if(data.length==0){
                this.infobox.showPop("必须添加至少一条商品明细信息!");
                return false;
            }
            return true;
        }
        //数据保存
        function dosave() {
            if (!checkInput()) {
                return false;
            }
            $("#<%=hidInout.ClientID %>").val(JSON.stringify(data));
            return true;
        }


         $(function(){
            if(<%=btnSave.Visible?"false":"true" %>)
            {
                disableCtrls("#tab,#btns,#basic");
                disableCtrls("#basic");
            }
            getInvTableShowOrHide("inv_data","title1");
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class=" tit_con">
        <span>调整单详细</span>
    </div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" style="margin: auto;" id="tab">
        <tr>
            <td class="td_co">
                调整单单号：
            </td>
            <td width="320">
                <div class="box_r">
                </div>
                <asp:TextBox  runat="server" ReadOnly="true" MaxLength="30" ID="tbOrderNo"></asp:TextBox>
            </td>
            <td class="td_co">
                调整单类型：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:DropDownList  runat="server" ID="selReasonType">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td_co" >
                调整单位：
            </td>
            <td >
                <div class="box_r">
                </div>
                <uc2:DropDownTree  runat="server"  ID="drpUnit" MultiSelect="false" Url="../ajaxhandler/tree_query.ashx?action=unit" />
            </td>
            <td class="td_co">
                调整仓库：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:DropDownList  runat="server" ID="selWarehouse">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td_co" >
                单据日期：
            </td>
            <td>
                <div class="box_r">
                </div>
                <input type="text" readonly="readonly" runat="server" id="selOrderDate" 
                    onclick="Calendar('MainContent_selOrderDate');" title="双击清除时间"  ondblclick="this.value=''" />
            </td>
            <td class="td_co">
                来源单据号：
            </td>
            <td class="td_lp">
                <input type="text" maxlength="30" runat="server" id="tbCompleteDate"  />
            </td>
        </tr>
        <tr>
            <td class="td_co" >
                备注：
            </td>
            <td class="td_lp" colspan="3">
                <div style="width:718px;*width:852px;_width:852px">
                    <asp:TextBox Width="100%" TextMode="MultiLine" MaxLength="180" runat="server" ID="tbRemark" Height="50px"></asp:TextBox>
                </div>
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto" id="btns">
        <input type="button" value="添加商品" runat="server" id="btnAdd" class="input_c" onclick="go_Add();" />
    </div>
    <div style="display: none">
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </div>
    <div class="tit_con" id="title1">
        <span>商品明细</span>
    </div>
    <div class="ss_bg" style="padding-right: 14px;">
        <div style="width:auto; height:auto; overflow-y:auto;">
        <table cellpadding="0" cellspacing="1" border="0" class=" ss_jg" id="basic">
            <thead>
                <tr class="b_c3">
                    <th scope="col" class="gv-no" align="center">
                        序号
                    </th>
                    <th scope="col" class="gv-code" align="center">
                        商品编码
                    </th>
                    <th scope="col" width="100" align="center">
                        商品名称
                    </th>
                    <% for (int i = 1; i <= 5; i++)
                       {
                           var item = SkuPropInfos.FirstOrDefault(obj => obj.display_index == i);
                           if (item != null)
                           {%>
                    <th scope="col" align="center" width="80" index="<%= item.display_index %>">
                        <%=item.prop_name%>
                    </th>
                    <%}
                           else
                           {%>
                    <th class="hidden">
                    </th>
                    <%}
                       }%>
                    <th scope="col" width="100" align="center">
                        库存数
                    </th>
                    <th scope="col" width="100" align="center">
                        调整数
                    </th>
                    <th scope="col" width="100" align="center">
                        操作
                    </th>
                </tr>
            </thead>
            <tbody id="inv_data">
            </tbody>
        </table>
        </div>
    </div>
    <div class="bf">
        <asp:Button ID="btnSave" runat="server" Text="保存" class="input_bc" OnClientClick="if(!dosave()) return false;"
            OnClick="btnSave_Click" />
        <input type="button" onclick="go_back()" value="返回" class=" input_fh" />
    </div>
    <input type="hidden" runat="server" id="hidInout" />
</asp:Content>