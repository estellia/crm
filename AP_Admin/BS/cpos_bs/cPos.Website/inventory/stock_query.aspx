<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" CodeFile="stock_query.aspx.cs" Inherits="inventory_stock_query" %>
<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="cc1" %>
<%@ Register Src="~/controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
            .hidden
        {
            display:none;
        }
</style>
<script type="text/javascript">
    function showYearMonth(sender) {
        var value = $(sender).val();
        if (value == "0") {
            $("#<%=selDate.ClientID %>").css("display", "");
            $("#yearMonth").css("display", "");
        } else {
            $("#<%=selDate.ClientID %>").css("display", "none").val("");
            $("#yearMonth").css("display", "none");
        }
    }
    $(function () {
        $("#<%=selUnit.ClientID %>")[0].onchanged = unitChange;
        //$("#<%=selDate.ClientID %>").bind("change", getYearMonth);
    });
    function unitChange() {
        $("#<%=hidwarehouse.ClientID %>").trigger("click");
    }
    function getYearMonth() {
        var date = $("#<%=selDate.ClientID %>").val().split("-");
        date.pop(date[2]);
        var newvalue = date.join("-");
        $("#<%=selDate.ClientID %>").val(newvalue);
    }
    function downloadExcel() {
        var condition = { type: "stock", warhouse_id: $("#<%=selWarehouse.ClientID %>").val(), item_code: $("#<%=tbItemCode.ClientID %>").val(), item_name: $("#<%=tbItemName.ClientID %>").val(), stock_type: $("#<%=selType.ClientID %>").val(), sel_date: $("#<%=selDate.ClientID %>").val(), unit_id: $("#<%=selUnit.ClientID %>")[0].values()[0] };
        $.post("../ajaxhandler/ajax_handler_toExecl.ashx", condition, function (data) {
            if (data) {
                location.href = data;
            }
        }, "text")
    };
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="tit_con"><span>搜索条件</span></div>
<table border="0" cellpadding="0" cellspacing="0" class="ss_tj" >
    <tr>
        <td  class=" tit_c">单位：</td>
        <td class="td_lp"><uc1:DropDownTree  runat="server" ID="selUnit" MultiSelect="false" Url="../ajaxhandler/tree_query.ashx?action=unit"/></td>
        <td  class=" tit_c">仓库：</td>
        <td class="td_lp"><asp:DropDownList runat="server" ID="selWarehouse"></asp:DropDownList></td>
    </tr>
    <tr>
        <td  class=" tit_c">商品编码：</td>
        <td class="td_lp"><input type="text" maxlength="30" runat="server" id="tbItemCode" /></td>
        <td  class=" tit_c">商品名称：</td>
        <td class="td_lp"><input type="text" maxlength="25" runat="server" id="tbItemName" /></td>
    </tr>
    <tr>
        <td  class=" tit_c">类型：</td>
        <td class="td_lp"><asp:DropDownList runat="server" ID="selType" onchange="showYearMonth(this);">
            <asp:ListItem Selected="True" Text="实时库存" Value="1"></asp:ListItem>
            <asp:ListItem Text="历史库存" Value="0"></asp:ListItem>
        </asp:DropDownList></td>
        <td  class=" tit_c" id="yearMonth" style="display:none">年月：</td>
        <td class="td_lp"><input type="text" maxlength="7" runat="server" readonly="readonly" onchange="getYearMonth();" id="selDate" style="display:none" onclick="Calendar('MainContent_selDate');"/></td>
    </tr>
</table>
<div class="b_bg">
    <input type="button" runat="server" class="input_c" value="查询" id="btnQuery" onserverclick="btnQueryClick"/>
    <input type="button" runat="server" class="input_c" value="重置" id="btnReset" onserverclick="btnResetClick"/>
    <input type="button" runat="server" class="input_c" value="导出" id="btnDownload" onclick="downloadExcel();"/>
</div>
<div class="tit_con"><span>搜索结果</span></div>
<div class="ss_bg">
   <% if (this.IsPostBack)
      {%> 
    <table cellpadding="0" cellspacing="1" class="ss_jg" border="0">
        <thead>
            <tr class="b_c3">
                 <th scope="col" width="100">单位</th>
                 <th scope="col" width="100">仓库</th>
                 <th scope="col" width="100">品质</th>
                 <th scope="col" class="gv-code">商品编码</th>
                 <th scope="col" width="100">商品名称</th>
                 <% for (int i = 1; i <= 5; i++)
                    {
                        var item = SkuPropInfos.FirstOrDefault(obj => obj.display_index == i);
                        if (item != null)
                        {%>
                    <th scope="col" width="80"><%=item.prop_name%></th>
                 <%}
                        else
                        {%>
                    <th class="hidden"></th>
                 <%}
                    }%>
                 <th scope="col" width="80">期初数</th>
                 <th scope="col" width="80">入库数</th>
                 <th scope="col" width="80">出库数</th>
                 <th scope="col" width="80">调整入数</th>
                 <th scope="col" width="80">调整出数</th>
                 <th scope="col" width="80">期末数</th>
            </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="gvStock">
                    <ItemTemplate>
                        <tr class=" b_c4">
                            <td align="center">
                                <%#Eval("unit_name")%>
                            </td>
                            <td align="center">
                                <%#Eval("warehouse_name")%>
                            </td>
                            <td align="center">
                                <%#Eval("item_label_type_name")%>
                            </td>
                            <td align="center">
                                <%#Eval("item_code")%>
                            </td>
                            <td align="center">
                                <%#Eval("item_name")%>
                            </td>
                            <td align="center" class="<%=this.SkuPropInfos.FirstOrDefault(obj=>obj.display_index==1)==null?"hidden":"" %>">
                                <%#Eval("prop_1_detail_name")%>
                            </td>
                            <td align="center" class="<%=this.SkuPropInfos.FirstOrDefault(obj=>obj.display_index==2)==null?"hidden":"" %>">
                                <%#Eval("prop_2_detail_name")%>
                            </td>
                            <td align="center" class="<%=this.SkuPropInfos.FirstOrDefault(obj=>obj.display_index==3)==null?"hidden":"" %>">
                                <%#Eval("prop_3_detail_name")%>
                            </td>
                            <td align="center" class="<%=this.SkuPropInfos.FirstOrDefault(obj=>obj.display_index==4)==null?"hidden":"" %>">
                                <%#Eval("prop_4_detail_name")%>
                            </td>
                            <td align="center" class="<%=this.SkuPropInfos.FirstOrDefault(obj=>obj.display_index==5)==null?"hidden":"" %>">
                                <%#Eval("prop_5_detail_name")%>
                            </td>
                            <td align="center">
                                <%#Eval("begin_qty")%>
                            </td>
                            <td align="center">
                                <%#Eval("in_qty")%>
                            </td>
                            <td align="center">
                                <%#Eval("out_qty")%>
                            </td>
                            <td align="center">
                                <%#Eval("adjust_in_qty")%>
                            </td>
                            <td align="center">
                                <%#Eval("adjust_out_qty")%>
                            </td>
                            <td align="center">
                                <%#Eval("end_qty")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
    </table> 
    <cc1:SplitPageControl CssClass="pag"  runat="server" ID="pager1" PageSize="10" 
        onrequireupdate="pager1_RequireUpdate"/>
        <%} %>
</div>
<input type="button" runat="server" id="hidwarehouse" onserverclick="hidwarehouseClick" style="display:none"/>
</asp:Content>

