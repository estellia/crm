<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="monitor_log_query.aspx.cs" Inherits="pos_monitor_log_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="cc1" %>
<%@ Register Src="~/controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        //查看
        function go_View(id) {
            location.href = "monitor_log_show.aspx?order_id=" + id + "&from=" + getFrom();
        }
        function getFrom() {
            return escape(document.getElementById("_from").value);
        }
        //条件重置
        function go_Reset() {
            $("#<%=tbCustomerCode.ClientID %>").val("");
            $("#<%=tbUnitCode.ClientID %>").val("");
            $("#<%=selDateStart.ClientID %>").val("");
            $("#<%=selDateEnd.ClientID %>").val("");
            $("#MainContent_selUnit_tbItemText").val("");
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
                客户代码:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCustomerCode" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td class="tit_c">
                门店:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbUnitCode" runat="server" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tit_c">
                日期:
            </td>
            <td class="td_lp" colspan="3">
                <input type="text" maxlength="10" runat="server" class="selectdate" readonly="readonly" onclick="Calendar('MainContent_selDateStart','MainContent_selDateEnd');"
                    title="双击清除时间" ondblclick="this.value='';" id="selDateStart" />
                至
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
                <asp:BoundField ItemStyle-Width="120px" DataField="customer_code" HeaderText="客户代码"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="100px" DataField="unit_name" HeaderText="门店" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="100px" DataField="pos_id" HeaderText="终端"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="100px" DataField="upload_time" HeaderText="上传时间"
                    ItemStyle-HorizontalAlign="Center"  />
                <asp:BoundField ItemStyle-Width="100px" DataField="remark" HeaderText="备注"
                    ItemStyle-HorizontalAlign="Center"  />
            </Columns>
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        <cc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pag" PageSize="10"
            OnRequireUpdate="SplitPageControl1_RequireUpdate" />
    </div>
</asp:Content>
