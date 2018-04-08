<%@ Page Title="客户下的用户查询" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="customer_user_query" Codebehind="user_query.aspx.cs" %>

<%@ Register Src="../Controls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
    function show(cu_id) {
        this.location.href = "user_show.aspx?oper_type=1&cu_id=" + cu_id + "&" + getFrom();

    }
    function getFrom() {
        return "from=" + escape(document.getElementById("_from").value);
    }
</script>
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <td   class="tit_c" >
                客户编码/名称:
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox ID="tbCustomerName" runat="server" MaxLength="25"></asp:TextBox>
            </td>
            <td    class="tit_c">
                登录帐号:
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox ID="tbUserAccount"  runat="server" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
                用户姓名:
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox ID="tbUserName"  runat="server" MaxLength="25"></asp:TextBox>
            </td>
            <td  class="tit_c">
                用户状态:
            </td>
            <td class="td_lp" colspan="3">
                <asp:DropDownList ID="cbUserStatus"  runat="server" Width="196px">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                    <asp:ListItem Value="1">正常</asp:ListItem>
                    <asp:ListItem Value="-1">停用</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
         <tr id="tr_unit" runat="server">
            <td  class="tit_c">
                运营商:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="ddl_Unit" runat="server" Width="196px" >
                   
                </asp:DropDownList>
            </td>
            <td class="tit_c">
               
            </td>
            <td class="td_lp" >
              
            </td>
        </tr>
    </table>
    <div class="b_bg">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
        </div>
<div class="ss_bg">
        <asp:GridView ID="gvCustomerUser" runat="server" cellpadding="0" 
            cellspacing="1" CssClass="ss_jg" AuoGenerateColumns="False"
            DataSourceID="odsCustomerUser" AllowPaging="True" DataKeyNames="ID" GridLines="None" 
            ShowHeaderWhenEmpty="True" AutoGenerateColumns="False">
            <RowStyle  CssClass="b_c4" />
            <Columns>
                <%--<asp:TemplateField ItemStyle-Width="20px" HeaderText="<input type='checkbox' onclick='checkAll(event)'/>">
                    <ItemTemplate>
                        <asp:CheckBox ID="select" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>--%>

                <%--<asp:BoundField DataField="ID" HeaderText="序号" ItemStyle-Width="100px"/>--%>
                <asp:TemplateField ItemStyle-Width="30px" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#this.gvCustomerUser.PageIndex * this.gvCustomerUser.PageSize + Container.DataItemIndex + 1%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="100px" HeaderText="客户编码" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerCode"  runat="server" Text='<%#Eval("Customer.Code") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="客户名称" ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName"  runat="server" Text='<%#Eval("Customer.Name") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户登录帐号" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <%-- <a href='user_show.aspx?oper_type=1&cur_menu_id=F32E7181DA054C1090009A90C618506D&cu_id=<%#Eval("ID") %>'>
                        <asp:Label ID="lblAccount"  runat="server" Text='<%#Eval("Account") %>'/></a>--%>
                        <a onclick='show("<%#Eval("ID") %>")' style="cursor:pointer"> <asp:Label ID="Label1"  runat="server" Text='<%#Eval("Account") %>'/></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="用户姓名" ItemStyle-CssClass="gv-person"/>
                <asp:BoundField DataField="ExpiredDate" HeaderText="用户失效日期" ItemStyle-CssClass="gv-date"/>
                <asp:BoundField DataField="StatusDescription" HeaderText="用户状态" ItemStyle-CssClass="gv-status"/>
           <asp:BoundField DataField="unit_name" HeaderText="所属运营商名称" />
            </Columns>
            <PagerTemplate>
                <uc1:Pager ID="Pager1" runat="server" RecordCount="0" />
            </PagerTemplate>
            <PagerStyle CssClass="pag" />
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        </div>
    <asp:ObjectDataSource ID="odsCustomerUser" runat="server" EnablePaging="True" MaximumRowsParameterName="maxRowCount"
        OnObjectCreating="odsCustomerUser_ObjectCreating" OnSelecting="odsCustomerUser_Selecting"
        SelectCountMethod="SelectUserListCount" SelectMethod="SelectUserList" StartRowIndexParameterName="startRowIndex"
        TypeName="cPos.Admin.Service.Interfaces.ICustomerService"></asp:ObjectDataSource>
</asp:Content>
