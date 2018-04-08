<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectRoleUnit.aspx.cs" Inherits="Login_SelectRoleUnit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="cPos_a">
        <div class="right">
            <div class="ss_bg">
                <table width="700px" align="center">
                    <tr>
                        <td align="left">
                            <div class="tit_con1">
                                <span>选择角色</span></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand"
                                CellPadding="0" GridLines="None" CellSpacing="1" CssClass="ss_jg1" OnRowDataBound="GridView1_RowDataBound"
                                HorizontalAlign="Center">
                                <HeaderStyle CssClass="b_c3" />
                                <RowStyle CssClass="b_c4" />
                                <AlternatingRowStyle CssClass="b_c5" />
                                <Columns>
                                    <asp:TemplateField HeaderText="角色">
                                        <ItemTemplate>
                                            <%#Eval("RoleName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="登录单位">
                                        <ItemTemplate>
                                            <%#Eval("UnitName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="默认" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#GetDefaultName((int)Eval("DefaultFlag"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfUnitId" runat="server" Value='<%# Eval("UnitId")%>' />
                                            <asp:LinkButton ID="lbUnitLogin" runat="server" Text="按单位登录" CommandName="UnitGo"
                                                CommandArgument='<%# Eval("RoleId")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
