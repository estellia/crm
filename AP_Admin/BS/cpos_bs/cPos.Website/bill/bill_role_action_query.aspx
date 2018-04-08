<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="bill_role_action_query.aspx.cs" Inherits="bill_bill_role_action_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        //新建
        function go_Create() {
            this.location.href = "bill_role_action_show.aspx?" + "BillKindID=" + $("#<%=cbBillKind.ClientID %>").val() + "&cbRole=" + $("#<%=cbRole.ClientID  %>").val() + "&from=" + getForm();
        }
        function getForm() {
            return escape(document.getElementById("_from").value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj" >
        <tr>
            <td align="right" class="tit_c" width="150">
                表单类型:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="cbBillKind" runat="server">
                </asp:DropDownList>
            </td>
            <td align="right" class="tit_c" width="150">
                角 &nbsp &nbsp 色:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="cbRole" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div class="b_bg">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
        <input type="button"  class="input_c" value="重置"  onclick="location.href='<%=this.Request.Path %>'" />
        <input type="button" value="新建" onclick="go_Create()" class="input_c" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span></div>
    <div class="ss_bg">
        <asp:GridView ID="gvBill" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
            AutoGenerateColumns="False" OnRowCommand="gvBill_RowCommand" GridLines="None"
            ShowHeaderWhenEmpty="True">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-CssClass="gv-no">
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1%></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RoleDescription" HeaderText="角色" ItemStyle-CssClass="gv-5icon" />
                <asp:BoundField DataField="ActionDescription" HeaderText="表单操作"  ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="gv-4word"/>
                <asp:BoundField DataField="KindDescription" HeaderText="表单类型" ItemStyle-CssClass="gv-4word" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="PreviousStatusDescription" HeaderText="前置状态" ItemStyle-CssClass="gv-5icon" />
                <asp:BoundField DataField="CurrentStatusDescription" ItemStyle-CssClass="gv-5icon" HeaderText="当前状态" />
                <asp:BoundField DataField="MinMoney" HeaderText="最小金额" ItemStyle-CssClass="gv-4word"  />
                <asp:BoundField DataField="MaxMoney" HeaderText="最大金额" ItemStyle-CssClass="gv-4word" />
                <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="gv-4word">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDel" ImageUrl="~/img/delete.png" title="删除" runat="server"
                            CommandArgument='<%#Eval("Id") %>' OnClientClick="if(!confirm('确认要删除该条数据吗？')) return false;"
                            CommandName="BillDelete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pag" />
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        <%--<uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pagebar" PageSize="10"
            OnRequireUpdate="SplitPageControl1_RequireUpdate" />--%>
    </div>
</asp:Content>
