<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pager.ascx.cs" Inherits="controls_Pager" %>
<table border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="90" align="center">
                    <asp:Label ID="lblCurPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                    /
                    <asp:Label ID="lblPagCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                    页
                </td>
                <%--<td width="80">
                    总共
                    <asp:Label ID="lblRecCount" runat="server" Text="<%# RecordCount %>"></asp:Label>
                    条
                </td>--%>
                <td width="60">
                    <asp:DropDownList ID="ddlPageSize" runat="server" SelectedValue="<%# ((GridView)Container.NamingContainer).PageSize%>"
                        OnSelectedIndexChanged="dllPageSize_SelectedIndexChanged">
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="96">
                    条/页
                </td>
                <td width="35" align="center">
                    <a href="#">
                        <asp:ImageButton ID="imgFirst" runat="server" ImageUrl="~/img/dot_lb.png" CommandArgument="First"
                            CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>"
                            style=" width:9px; height:6px;" /></a>
                </td>
                <td width="25">
                    <a href="#">
                        <asp:ImageButton ID="imgPrevious" runat="server" ImageUrl="~/img/dot_ls.png" CommandArgument="Prev"
                            CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>" 
                            style=" width:9px; height:6px;" /></a>
                </td>
                <td width="30" align="right">
                    <a href="#">
                        <asp:ImageButton ID="imgNext" runat="server" ImageUrl="~/img/dot_rs.png" CommandArgument="Next"
                        style=" width:9px; height:6px;" 
                            CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>" />
                    </a>
                </td>
                <td width="35" align="center">
                    <a href="#">
                        <asp:ImageButton ID="imgLast" runat="server" ImageUrl="~/img/dot_rb.png" CommandArgument="Last"
                        style=" width:9px; height:6px;" 
                            CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>" />
                    </a>
                </td>
                <td width="50">
                    <asp:TextBox ID="tbGoPage" runat="server" MaxLength="5" Width="20px"></asp:TextBox>
                </td>
                <td align="center">
                    <a href="#"><asp:Button ID="btnGoPage" runat="server" Text="转到" OnClick="btnGoPage_Click"/></a>
                </td>
            </tr>
        </table>