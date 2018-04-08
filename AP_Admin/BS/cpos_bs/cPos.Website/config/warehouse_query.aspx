<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="warehouse_query.aspx.cs" Inherits="config_warehouse_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<%@ Register Src="../controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function getfrom() {
            return escape($("#_from").val());
        }
        function go_Visible(warehouse_id) {
            this.location.href = "warehouse_show.aspx?oper_type=1&warehouse_id=" + warehouse_id + "&from=" + getfrom();
        }
        function go_Create() {
            this.location.href = "warehouse_show.aspx?oper_type=2"  + "&from=" + getfrom();
        }
        function go_Modify(warehouse_id) {
            this.location.href = "warehouse_show.aspx?oper_type=3&warehouse_id=" + warehouse_id + "&from=" + getfrom();
        }
        //确认提示
        function go_Confirm(sender) {
            var msg = $(sender).attr("title") == "启用" ? "启用" : "停用";
            return confirm("是否要" + msg + "该仓库？");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <td  align="right" class="tit_c">
                所属单位名称:
            </td>
            <td class="td_lp">
               <%-- <asp:TextBox ID="tbUnitName" runat="server" MaxLength="50"></asp:TextBox>--%>
           <uc2:DropDownTree runat="server" ID="dvUnit" MultiSelect="false" Url="../ajaxhandler/tree_query.ashx?action=unit"/>
            </td>
            <td  align="right" class="tit_c">
                仓库编码:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbWarehouseCode" runat="server" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="tit_c">
                仓库名称:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbWarehouseName" runat="server" MaxLength="25"></asp:TextBox>
            </td>
            <td align="right" class="tit_c">
                仓库联系人:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbWarehouseContacter" runat="server" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  align="right" class="tit_c">
                仓库电话:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbWarehouseTel" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td  align="right" class="tit_c">
                仓库状态:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="cbWarehouseStatus" runat="server">
                    <asp:ListItem Value="">全部</asp:ListItem>
                    <asp:ListItem Value="1">正常</asp:ListItem>
                    <asp:ListItem Value="-1">停用</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
        <input type="button"  class="input_c" value="重置"  onclick="location.href='<%=this.Request.Path %>'" />
        <input type="button" runat="server" id="btnCreate" value="新建" onclick="go_Create();"
            class="input_c" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg">
        <asp:GridView ID="gvWarehourse" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
            AutoGenerateColumns="False" GridLines="None" ShowHeaderWhenEmpty="True" OnRowCommand="gvWarehourse_RowCommand"
            OnRowCreated="gvWarehourse_RowCreated" OnRowDataBound="gvWarehourse_RowDataBound">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="gv-no" HeaderText="序号">
                    <ItemTemplate>
                        <%#SplitPageControl1.PageSize*SplitPageControl1.PageIndex+ Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="所属单位名称"   ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Unit.DisplayName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="仓库编码" ItemStyle-CssClass="gv-4word">
                    <ItemTemplate>
                        <a id="a" onclick='go_Visible("<%#Eval("ID") %>")' style="cursor: pointer">
                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Code") %>' /></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="仓库名称"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contacter" HeaderText="仓库联系人" ItemStyle-Width="80px"
                      ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Tel" HeaderText="仓库电话" ItemStyle-CssClass="gv-tel" />
                <asp:BoundField DataField="IsDefaultDescription" HeaderText="默认仓库" ItemStyle-Width="60px"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="StatusDescription" HeaderText="仓库状态" ItemStyle-Width="60px"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField  HeaderText="操作" ItemStyle-CssClass="gv-2icon" >
                    <ItemTemplate>
                        <a id="imgModify" onclick='go_Modify("<%#Eval("ID") %>")' style="cursor: pointer">
                            <img src="../img/modify.png" title="修改" alt="修改" /></a>
                        <asp:ImageButton runat="server" status='<%#Eval("StatusDescription") %>' OnClientClick='if(!go_Confirm(this))return false;' ID="imgControl" CommandName='<%#Eval("Status") %>'
                            CommandArgument='<%#Eval("ID") %>' ImageUrl='<%#getImgUrl(Eval("StatusDescription")) %>' ToolTip='<%# getImgToolTip(Eval("StatusDescription").ToString()) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
    <uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass=" pag" PageSize="10"
        OnRequireUpdate="SplitPageControl1_RequireUpdate" />
    </div>
</asp:Content>
