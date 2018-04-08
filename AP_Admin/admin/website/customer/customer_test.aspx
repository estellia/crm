<%@ Page Title="测试" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
 CodeFile="customer_test.aspx.cs" Inherits="customer_customer_test" %>
<%@ Register Src="../Controls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div>
<div class="ss_bg">
        <asp:GridView ID="gvCustomer" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
            AutoGenerateColumns="False" DataSourceID="odsCustomer" AllowPaging="True" DataKeyNames="ID"
            OnRowCommand="gvCustomer_RowCommand" GridLines="None" ShowHeaderWhenEmpty="True">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="gv-checkbox" HeaderText="<input type='checkbox' ID='power' onclick='selectAll(this);'/>">
                    <ItemTemplate>
                        <asp:CheckBox ID="select" CssClass="select" onclick="check()"  runat="server" customerId='<%#Eval("ID") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编码"  ItemStyle-CssClass="gv-code" ItemStyle-Width="120px">
                    <ItemTemplate >
                        <a href='customer_show.aspx?oper_type=1&customer_id=<%#Eval("ID") %>'>
                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Code") %>' /></a>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="名称" />
                <asp:BoundField DataField="Address" HeaderText="地址" />
                <asp:BoundField DataField="Contacter" HeaderText="联系人" ItemStyle-CssClass="gv-person" ItemStyle-Width="100px"/>
                <asp:BoundField DataField="Tel" ItemStyle-HorizontalAlign="Center" HeaderText="电话" ItemStyle-CssClass="gv-tel"/>
                <asp:BoundField DataField="StatusDescription" HeaderText="状态" ItemStyle-CssClass="gv-status" />
                <asp:TemplateField ItemStyle-Width="150px" HeaderText="操作" ItemStyle-CssClass="gv-5icon">
                    <ItemTemplate>
                        <a href='../customer/customer_show.aspx?oper_type=3&customer_id=<%# Eval("ID")%>'>
                            <img src="../img/edit.png" alt="" title="编辑" />
                        </a>
                        &nbsp;
                        <a style="display:none;" href='../customer/customer_show.aspx?oper_type=3&customer_id=<%# Eval("ID")%>'>
                            <asp:ImageButton ID="imgSubmit" runat="server" ImageUrl="../img/exchange.png" alt=""
                                CommandArgument='<%#Eval("ID") %>' CommandName="Submit" AlternateText="提交" title="提交" />
                        </a>
                        <a href='../customer/shop_query.aspx?customer_id=<%# Eval("ID")%>&customer_name=<%#Eval("Name") %>'>
                            <img src="../img/shop.png" alt="" title="门店" />
                        </a>
                        <a style="display:none;" href='../customer/terminal_query.aspx?customer_id=<%# Eval("ID")%>&customer_name=<%#Eval("Name") %>'>
                            <img src="../img/terminal.png" alt="" title="终端" />
                        </a>
                        <a style="display:none;" href='../customer/user_query.aspx?customer_id=<%# Eval("ID")%>&customer_name=<%#Eval("Name") %>'>
                            <img src="../img/user.png" alt="" title="用户" />
                        </a>
                        &nbsp;
                        <a href='../customer/customer_show.aspx?oper_type=3&customer_id=<%# Eval("ID")%>'>
                            <asp:ImageButton ID="imgSubmitInit" runat="server" ImageUrl="../img/exchange.png" alt="" OnClientClick="fnShowLoading(this);"
                                CommandArgument='<%#Eval("ID") %>' CommandName="SubmitInit" AlternateText="客户初始化" title="客户初始化" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
                <uc1:Pager ID="Pager1" runat="server" RecordCount="0" />
            </PagerTemplate>
            <PagerStyle CssClass="pag" />
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="odsCustomer" runat="server" EnablePaging="True" MaximumRowsParameterName="maxRowCount"
        OnObjectCreating="odsCustomer_ObjectCreating" OnSelecting="odsCustomer_Selecting"
        SelectCountMethod="SelectCustomerListCount" SelectMethod="SelectCustomerList"
        StartRowIndexParameterName="startRowIndex" TypeName="cPos.Admin.Service.Interfaces.ICustomerService">
    </asp:ObjectDataSource>
</div>
</asp:Content>

