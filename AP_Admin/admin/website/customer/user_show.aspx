<%@ Page Title="查看客户下的用户" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" CodeFile="user_show.aspx.cs" Inherits="customer_user_show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script type="text/javascript">
    $(function () {
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
                <asp:TextBox ID="tbCustomerCode" runat="server" CssClass="ban_in" ReadOnly="true"  ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_co">
                名称：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCustomerName" runat="server" CssClass="ban_in" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_co">
                状态：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCustomerStatus" runat="server" CssClass="ban_in" ReadOnly="true"  ></asp:TextBox>
            </td>
        </tr>
    </table>
<div class="tit_con">
        <span>用户信息</span>
        <a href="#"><img id="imgUser" src="../img/tit_r.png" onclick="showRegion('imgUser', 'tabUser')" alt=""/></a></div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabUser">
        <tr>
            <td  class="td_co">
                登录帐号：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbUserAccount" runat="server" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_co">
                姓名：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbUserName" runat="server" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_co">
                失效日期：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbUserExpiredDate" runat="server" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_co">
                状态：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbUserStatus" runat="server" CssClass="ban_in" ReadOnly="true" ></asp:TextBox>
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
        <input type="button" name="Submit" onclick="location.href='<%=this.Request.QueryString["from"]??"user_query.aspx" %>'" value="返回" class="input_fh" />
       <%-- <asp:Button ID="btnReturn" runat="server" Text="返回" class="input_fh" OnClick="btnReturn_Click" />--%>
    </div>
</asp:Content>

