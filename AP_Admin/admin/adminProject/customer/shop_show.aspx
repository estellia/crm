<%@ Page Title="查看客户下的门店" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="customer_shop_show" Codebehind="shop_show.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
  $(function(){
      disableCtrls("#tabCustomer");
      disableCtrls("#tabUser");
      disableCtrls("#tabOper");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="tit_con">
        <span>客户信息</span>
        <a href="#"><img id="imgCustomer" src="../img/tit_r.png" onclick="showRegion('imgCustomer', 'tabCustomer')" alt=""/></a></div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabCustomer">
        <tr>
            <td  class="td_co">
                编码：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCustomerCode" runat="server" CssClass="ban_in" MaxLength="30"  ReadOnly="true"  ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                名称：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCustomerName" runat="server" CssClass="ban_in" MaxLength="25" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                状态：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCustomerStatus" runat="server" MaxLength="10" CssClass="ban_in" ReadOnly="true"  ></asp:TextBox>
            </td>
        </tr>
    </table>
<div class="tit_con">
        <span>门店信息</span>
        <a href="#"><img id="imgUser" src="../img/tit_r.png" onclick="showRegion('imgUser', 'tabUser')" alt=""/></a></div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabUser">
        <tr>
            <td  class="td_co">
                门店编码：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopCode" runat="server" MaxLength="30" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                名称：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopName" runat="server" MaxLength="25" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                英文名称：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopEnglishName" runat="server" MaxLength="100" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                简称：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopShortName" runat="server" MaxLength="25" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                省：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopProvince" runat="server" MaxLength="15" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                市：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopCity" runat="server" MaxLength="15" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                区/县：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopCountry" runat="server" MaxLength="15" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                地址：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopAddress" runat="server" MaxLength="100" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                邮编：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopPostcode" runat="server" MaxLength="6" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                联系人：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopContact" runat="server" MaxLength="25" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                电话：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopTel" runat="server" MaxLength="30" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                传真：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopFax" runat="server" MaxLength="30" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                邮箱：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopEmail" runat="server" MaxLength="50" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                状态：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopStatus" runat="server" MaxLength="10" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                备注：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbShopRemark" runat="server" TextMode="MultiLine"  
                    CssClass="ban_in"  ReadOnly="True" Width="431px" Height="50px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="tit_con">
        <span>操作信息</span><a href="#"><img id="imgOper" src="../img/tit_r.png" onclick="showRegion('imgOper', 'tabOper')" alt=""/></a></div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabOper">
        <tr>
            <td  class="td_co">
                系统最后修改时间：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbSysModifyTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="bf">
        <input type="button" name="button" onclick="location.href='<%=this.Request.QueryString["from"]??"show_query.aspx" %>'" value="返回" class="input_fh" />
       <%-- <asp:Button ID="btnReturn" runat="server" Text="返回" class="input_fh"  OnClick="btnReturn_Click" />--%>
    </div>

</asp:Content> 

