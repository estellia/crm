<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="customer_brand_customer_query" Codebehind="brand_customer_query.aspx.cs" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="cc1" %>
<%@ Register Src="~/controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
         //新建
         function create() {
             this.location.href = "brand_customer_show.aspx?oper_type=2"
                     + "&cur_menu_id=" + ""
                     + "&" + getFrom();
         }
        //查看
        function go_View(id) {
            location.href = "brand_customer_show.aspx?oper_type=1&bc_id=" + id + "&from=" + getFrom();
        }
        function go_Edit(id) {
            location.href = "brand_customer_show.aspx?oper_type=3&bc_id=" + id + "&from=" + getFrom();
        }
        function getFrom() {
            return escape(document.getElementById("_from").value);
        }
        //条件重置
        function go_Reset() {
            $("#<%=tbBcCode.ClientID %>").val("");
            $("#<%=tbBcName.ClientID %>").val("");
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
                代码:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbBcCode" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td class="tit_c">
                名称:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbBcName" runat="server" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="b_bg">
        <input type="button" id="btnNew" value="新建" class="input_c" onclick="create()" />
        <input type="button" runat="server" id="btnQuery" value="查询" onserverclick="btnQuery_Click"
            class="input_c" />
        <input type="button" runat="server" id="btnReset" value="重置" onserverclick="btnResetClick"
            class="input_c" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg">
        <asp:GridView ID="gvPos" runat="server" CellPadding="0" Width="100%" GridLines="None"
            CellSpacing="1" CssClass="ss_jg" AutoGenerateColumns="False" DataKeyNames="brand_customer_id">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:BoundField ItemStyle-Width="30px" DataField="row_no" HeaderText="序号" 
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="120px" DataField="brand_customer_code" HeaderText="品牌客户代码"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="100px" DataField="brand_customer_name" HeaderText="品牌客户名称" 
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="100px" DataField="brand_customer_contacter" HeaderText="联系人"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="100px" DataField="brand_customer_tel" HeaderText="联系电话"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="100px" DataField="create_time" HeaderText="创建时间"
                    ItemStyle-HorizontalAlign="Center"  />
                <asp:TemplateField ItemStyle-Width="30px" HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a onclick="go_View('<%#Eval("brand_customer_id") %>');" href="javascript:void(0);">
                            <img alt="" src="../img/view.png" /></a>
                        <a onclick="go_Edit('<%#Eval("brand_customer_id") %>');" href="javascript:void(0);">
                            <img alt="" src="../img/edit.png" /></a>
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
