<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="repair_query.aspx.cs" Inherits="Repair_repair_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<%@ Register Src="~/controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function resetQueryParameter() {
            this.location.href = '<%=this.Request.Path %>';

        }
        function getTree() {
            return document.getElementById("<%=selSalesUnit.ClientID %>");
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <td width="150" align="right" class="tit_c">
                门店:
            </td>
            <td class="td_lp" colspan="3">
                <uc2:DropDownTree ID="selSalesUnit" runat="server" MultiSelect="false" Url="../ajaxhandler/tree_query.ashx?action=unit" />
            </td>
            <td width="256" align="right" class="tit_c">
                状态:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="selStatus" runat="server">
                    <asp:ListItem Value="">全部</asp:ListItem>
                    <asp:ListItem Value="1">未处理</asp:ListItem>
                    <asp:ListItem Value="2">响应</asp:ListItem>
                    <asp:ListItem Value="3">已处理</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tit_c">
                报修日期：
            </td>
            <td class="td_lp" align="left">
                <input type="text" runat="server" class="selectdate" readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_selRepairDateBegin','MainContent_selRepairDateBegin');"
                    title="双击清除时间" id="selRepairDateBegin" />
            </td>
            <td align="center">
                至
            </td>
            <td class="td_lp" align="right">
                <input type="text" runat="server" class="selectdate" readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_selRepairDateEnd');" title="双击清除时间" id="selRepairDateEnd" />
            </td>
        </tr>
    </table>
    <div class="b_bg">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
        <input type="button" id="btnReset" runat="server" class="input_c" value="重置" onserverclick="btnResetClick" />
    </div>
    <div style="background-color: #f2f1ef;">
        <div class="tit_con">
            <span>搜索结果</span>
        </div>
        <div class="ss_bg">
            <asp:GridView ID="gvInoutBill" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
                AutoGenerateColumns="False" GridLines="None" ShowHeaderWhenEmpty="True">
                <RowStyle CssClass="b_c4" />
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="gv-no" HeaderText="序号" ItemStyle-Width="30"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <%#SplitPageControl1.PageIndex*SplitPageControl1.PageSize +Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="unit_name" HeaderText="门店" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Width="100px" />
                    <asp:BoundField DataField="repair_type_name" HeaderText="报修类型" ItemStyle-Width="80px"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="pos_sn" HeaderText="终端序列号" ItemStyle-CssClass="gv-tel"
                        ItemStyle-Width="80px" />
                    <asp:BoundField DataField="phone" HeaderText="手机" ItemStyle-CssClass="gv-tel" ItemStyle-Width="80px" />
                    <asp:BoundField DataField="status_desc" HeaderText="状态" ItemStyle-CssClass="gv-tel"
                        ItemStyle-Width="80px" />
                    <asp:BoundField DataField="remark" HeaderText="备注" ItemStyle-CssClass="gv-tel" />
                    <asp:BoundField DataField="repair_time" HeaderText="报修时间" ItemStyle-Width="60px"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="repair_user_name" HeaderText="报修人" ItemStyle-Width="60px"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="response_time" HeaderText="响应时间" ItemStyle-Width="60px"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="response_user_name" HeaderText="响应人" ItemStyle-Width="60px"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="complete_time" HeaderText="修复时间" ItemStyle-Width="60px"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="complete_user_name" HeaderText="修复人" ItemStyle-Width="60px"
                        ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <PagerStyle CssClass="pag" />
                <HeaderStyle CssClass="b_c3" />
                <AlternatingRowStyle CssClass="b_c5" />
            </asp:GridView>
            <uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pag" PageSize="10"
                OnRequireUpdate="SplitPageControl1_RequireUpdate" />
        </div>
    </div>
</asp:Content>
