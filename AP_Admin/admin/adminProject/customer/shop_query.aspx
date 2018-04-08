<%@ Page Title="客户下的门店查询" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="customer_shop_query" Codebehind="shop_query.aspx.cs" %>

<%@ Register Src="../Controls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <%--<td width="150"  class="tit_c">--%>
            <td  class="tit_c">
                客户编码/名称:
            </td>
            <td class="td_lp"  >
                <asp:TextBox  ID="tbCustomerName" runat="server" MaxLength="25"></asp:TextBox>
            </td>
            <td   class="tit_c" >
                门店编码:
            </td>
            <td class="td_lp" >
                <asp:TextBox  ID="tbShopCode" runat="server" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td   class="tit_c">
                门店名称:
            </td>
            <td class="td_lp" >
                <asp:TextBox  ID="tbShopName" runat="server" MaxLength="25"></asp:TextBox>
            </td>
            <td  class="tit_c">
                门店联系人:
            </td>
            <td class="td_lp" >
                <asp:TextBox ID="tbShopContact" runat="server" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
                门店电话:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopTel"  runat="server" MaxLength="20"></asp:TextBox>
            </td>
            <td  class="tit_c">
                门店状态:
            </td>
            <td class="td_lp" >
                <asp:DropDownList ID="cbShopStatus"  runat="server">
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
        &nbsp;</div>
    <div class="tit_con">
        <span>搜索结果</span>
        </div>
<div class="ss_bg">
        <asp:GridView ID="gvCustomerShop" runat="server" cellpadding="0" 
            cellspacing="1" CssClass="ss_jg" AutoGenerateColumns="False"
            DataSourceID="odsCustomerShop" AllowPaging="True" DataKeyNames="ID" GridLines="None" ShowHeaderWhenEmpty="True">
            <RowStyle CssClass="b_c4" />
            <Columns>
               <%-- <asp:TemplateField ItemStyle-Width="20px" HeaderText="<input type='checkbox' onclick='checkAll(event)'/>">
                    <ItemTemplate>
                        <asp:CheckBox ID="select" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>--%>

               <%-- <asp:BoundField DataField="ID" HeaderText="序号" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="left"/>   20120724   --%> 
               <asp:TemplateField ItemStyle-CssClass="gv-no" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                         <%#this.gvCustomerShop.PageIndex * this.gvCustomerShop.PageSize + Container.DataItemIndex + 1%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="gv-code" ItemStyle-HorizontalAlign="Center" HeaderText="客户编码">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerCode"  runat="server" Text='<%#Eval("Customer.Code") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="客户名称" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:Label ID="lblCustomerName"  runat="server" Text='<%#Eval("Customer.Name") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="门店编码" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Label ID="lblShopCode"  runat="server" Text='<%#Eval("Code") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="门店名称" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <a href='shop_show.aspx?oper_type=1&cs_id=<%#Eval("ID") %>&from=<%= Server.UrlEncode(this.Request.Url.PathAndQuery) %>'>
                        <asp:Label ID="lblShopName"  runat="server" Text='<%#Eval("Name") %>'/></a>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Address" HeaderText="门店地址" ItemStyle-Width="140px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Contact" HeaderText="门店联系人" ItemStyle-CssClass="gv-date"/>
                <asp:BoundField DataField="Tel" HeaderText="门店电话" ItemStyle-CssClass="gv-tel"/>
                <asp:BoundField DataField="StatusDescription" HeaderText="门店状态" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />
           <asp:BoundField DataField="unit_name" HeaderText="所属运营商名称" />
            </Columns>
            <PagerTemplate>
                <uc1:Pager ID="Pager2" runat="server" RecordCount="0" />
            </PagerTemplate>
            <PagerStyle CssClass="pag" />
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        </div>
    <asp:ObjectDataSource ID="odsCustomerShop" runat="server" EnablePaging="True" MaximumRowsParameterName="maxRowCount"
        OnObjectCreating="odsCustomerShop_ObjectCreating" OnSelecting="odsCustomerShop_Selecting"
        SelectCountMethod="SelectShopListCount" SelectMethod="SelectShopList" StartRowIndexParameterName="startRowIndex"
        TypeName="cPos.Admin.Service.Interfaces.ICustomerService" ></asp:ObjectDataSource>

</asp:Content>

