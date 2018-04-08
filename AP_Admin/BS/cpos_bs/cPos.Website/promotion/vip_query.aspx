<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="vip_query.aspx.cs" Inherits="promotion_vip_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function test () {
            var status = $("#<%=tbNo.ClientID %>");
            status.focus();
        }
        function getfrom() {
            return escape($("#_from").val());
        }
        function go_Visible(sender) {
            this.location.href = "vip_show.aspx?oper_type=1&vip_id=" + sender+"&from="+getfrom();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj" >
        <tr>
            <td  class="tit_c">
                会员卡号:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbNo" runat="server" MaxLength="25"></asp:TextBox>
            </td>
            <td  class="tit_c">
                类型:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="cbType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
                姓名:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbName" runat="server" MaxLength="25"></asp:TextBox>
            </td>
            <td  class="tit_c">
                手机:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCell" runat="server" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
                办卡门店:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbUnit" runat="server" MaxLength="25"></asp:TextBox>
            </td>
            <td  class="tit_c">
                状态:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="cbStatus" runat="server">
                    <asp:ListItem Value="">全部</asp:ListItem>
                    <asp:ListItem Value="1">正常</asp:ListItem>
                    <asp:ListItem Value="-1">停用</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
         <input type="button" class="input_c" value="重置" onclick="location.href='<%=this.Request.Path %>'" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg">
        <asp:GridView ID="gvVip" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
            AutoGenerateColumns="False" GridLines="None" ShowHeaderWhenEmpty="True">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField ItemStyle-Width="30px" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#SplitPageControl1.PageSize * SplitPageControl1.PageIndex + Container.DataItemIndex + 1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="No" HeaderText="会员卡号" ItemStyle-HorizontalAlign="left" />
                <asp:TemplateField HeaderText="类型" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem,"Type.Name" )%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="姓名" ItemStyle-CssClass="gv-person">
                    <ItemTemplate>
                        <a id="Visible" onclick="go_Visible('<%#Eval("ID") %>');" style="cursor: pointer">
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' /></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="Cell" HeaderText="手机" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="办卡门店" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container.DataItem, "ActivateUnit.DisplayName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ExpiredDate" HeaderText="有效期限" ItemStyle-CssClass="gv-date" />
                <asp:BoundField DataField="Points" HeaderText="当前积分" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="StatusDescription" HeaderText="状态" ItemStyle-CssClass="gv-status" ItemStyle-HorizontalAlign="Center"/>
            </Columns>
            <PagerStyle CssClass="pag" />
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        <uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pagebar" PageSize="10"
            OnRequireUpdate="SplitPageControl1_RequireUpdate" />
    </div>
</asp:Content>
