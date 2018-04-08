<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="bill_action_query.aspx.cs" Inherits="bill_bill_action_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
    //新建
        function go_Create() {
            this.location.href = "bill_action_show.aspx?from=" + getForm() + "&BillKindID=" + $("#<%=cbBillKind.ClientID %>").val();
    }
    function getForm() {
        return escape(document.getElementById("_from").value);
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <td align="right" class="tit_c" width="256">
                表单类型:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="cbBillKind" runat="server" 
                    onselectedindexchanged="cbBillKind_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div class="b_bg">
        <input type="button" value="新建" onclick="go_Create()" class="input_c" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span></div>
    <div class="ss_bg">
        <asp:GridView ID="gvBillAction" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
            AutoGenerateColumns="False" OnRowCommand="gvBillAction_RowCommand" GridLines="None"
            ShowHeaderWhenEmpty="True">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-CssClass="gv-no">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1%></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Code" HeaderText="编码"  ItemStyle-CssClass="gv-code"/>
                <asp:BoundField DataField="Description" HeaderText="描述"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreateFlag" HeaderText="新建标志" ItemStyle-CssClass="gv-3icon" />
                <asp:BoundField DataField="ModifyFlag" ItemStyle-CssClass="gv-3icon"
                    HeaderText="修改标志"  />
                <asp:BoundField DataField="ApproveFlag" HeaderText="审核标志" ItemStyle-CssClass="gv-3icon"/>
                <asp:BoundField DataField="RejectFlag" HeaderText="退回标志" ItemStyle-CssClass="gv-3icon" />
                <asp:BoundField DataField="CancelFlag" HeaderText="删除标志" ItemStyle-CssClass="gv-3icon" />
                <asp:BoundField DataField="display_index" HeaderText="排序" ItemStyle-CssClass="gv-3icon" />
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
        <%--<uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pagebar" PageSize="1"
            OnRequireUpdate="SplitPageControl1_RequireUpdate" />--%>
    </div>
</asp:Content>
