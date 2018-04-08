<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="terminal_unit_query.aspx.cs" Inherits="terminal_terminal_unit_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<div class="tit_con">
    <span>搜索条件</span>
</div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj" style="text-align: center;
        margin: auto;">
        <tr>
            <td class="tit_c" >
                终端编码：
            </td>
            <td class="td_lp">
                <asp:TextBox runat="server" ID="tbPosCode" MaxLength="30"></asp:TextBox>
            </td>
            <td class="tit_c" >
                终端序列号：
            </td>
            <td class="td_lp">
                <asp:TextBox runat="server" ID="tbPosSn" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
                门店编码:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbUnitCode" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td  class="tit_c">
                门店名称:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbUnitName" runat="server" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg" style="padding-right: 14px;">
        <asp:GridView runat="server" AllowPaging="false" DataKeyNames="ID" ID="gvTerminal_Unit"
            CellPadding="0" CellSpacing="1" CssClass="ss_jg" AutoGenerateColumns="False"
            GridLines="None" ShowHeaderWhenEmpty="True">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-CssClass="gv-no">
                    <ItemTemplate>
                        <%# SplitPageControl1.PageSize * SplitPageControl1.PageIndex + Container.DataItemIndex + 1%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="gv-5icon" HeaderText="终端编码" >
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Pos.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="终端序列号" ItemStyle-HorizontalAlign="left">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Pos.SN")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="150px" HeaderText="门店编码" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Unit.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="150px" HeaderText="门店名称" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Unit.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PosNo" ItemStyle-Width="100px" HeaderText="Pos编号" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="AllocateTime" ItemStyle-CssClass="gv-datetime"  HeaderText="建立时间" />
            </Columns>
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        <uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass=" pag" PageSize="10"
            OnRequireUpdate="SplitPageControl1_RequireUpdate" />
    </div>
   <%-- <div class="bf">
        <input type="button" class="input_fh" value="返回" onclick="location.href='<%=this.Request.QueryString["from"]??"terminal_query.aspx" %>'" />
    </div>--%>
</asp:Content>
