<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="pos_receipt_query.aspx.cs" Inherits="inout_pos_receipt_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="cc1" %>
<%@ Register Src="~/controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        //查看
        function go_View(id) {
            location.href = "pos_receipt_show.aspx?order_id=" + id + "&from=" + getFrom();
        }
        function getFrom() {
            return escape(document.getElementById("_from").value);
        }
        //条件重置
        function go_Reset() {
            $("#<%=tbOrderNo.ClientID %>").val("");
            $("#<%=tbItem.ClientID %>").val("");
            $("#<%=selDateStart.ClientID %>").val("");
            $("#<%=selDateEnd.ClientID %>").val("");
            $("#MainContent_selUnit_tbItemText").val("");
        }
        function downloadExcel() {
            var condition = { type: "pos", order_no: $("#<%=tbOrderNo.ClientID %>").val(), item_name: $("#<%=tbItem.ClientID %>").val(), order_date_begin: $("#<%=selDateStart.ClientID %>").val(), order_date_end: $("#<%=selDateEnd.ClientID %>").val(), unit_code: $("#<%=tbUnitCode.ClientID %>")[0].val() };
            $.post("../ajaxhandler/ajax_handler_toExecl.ashx", condition, function (data) {
                if (data) {
                    location.href = data;
                }
            }, "text");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span>
    </div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <td class="tit_c">
                单据编号:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbOrderNo" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td class="tit_c">
                门店:
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox ID="tbUnitCode" runat="server" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tit_c">
                商品:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbItem" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td class="tit_c">
                小票日期:
            </td>
            <td class="td_lp">
                <input type="text" maxlength="10" runat="server" class="selectdate" readonly="readonly" onclick="Calendar('MainContent_selDateStart','MainContent_selDateEnd');"
                    title="双击清除时间" ondblclick="this.value='';" id="selDateStart" />
            </td>
            <td>
                至
            </td>
            <td align="right">
                <input class="selectdate" maxlength="10" type="text" runat="server" readonly="readonly" id="selDateEnd"
                    onclick="Calendar('MainContent_selDateEnd');" title="双击清除时间" ondblclick="this.value='';" />
            </td>
        </tr>
    </table>
    <div class="b_bg">
        <input type="button" runat="server" id="btnQuery" value="查询" onserverclick="btnQuery_Click"
            class="input_c" />
        <input type="button" runat="server" id="btnReset" value="重置" onserverclick="btnResetClick"
            class="input_c" />
        <input style="display:none;" type="button" class="input_c" value="导出" onclick="downloadExcel();" id="btnDownLoad" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg">
        <asp:GridView ID="gvPos" runat="server" CellPadding="0" Width="100%" GridLines="None"
            CellSpacing="1" CssClass="ss_jg" AutoGenerateColumns="False" DataKeyNames="pos_id">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:BoundField ItemStyle-Width="30px" DataField="Row_No" HeaderText="序号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="120px" DataField="create_unit_name" HeaderText="门店"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="100px" DataField="order_no" HeaderText="单据编号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="100px" DataField="order_date" HeaderText="单据日期"
                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField ItemStyle-Width="100px" DataField="total_qty" HeaderText="总数量" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="100px" DataField="total_amount" HeaderText="总金额"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="60px" DataField="create_user_name" HeaderText="收银员"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="100px" DataField="create_time" HeaderText="收银时间"
                    ItemStyle-HorizontalAlign="Center"  />
                <asp:BoundField ItemStyle-Width="100px" DataField="sales_user" HeaderText="业务员"
                    ItemStyle-HorizontalAlign="Center"  />
                <asp:TemplateField HeaderText="操作" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="#" onclick="go_View('<%#Eval("order_id") %>');"><img src="../img/view.png" title="查看详细" alt="查看详细" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        <cc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pag" PageSize="10"
            OnRequireUpdate="SplitPageControl1_RequireUpdate" />
    </div>
</asp:Content>
