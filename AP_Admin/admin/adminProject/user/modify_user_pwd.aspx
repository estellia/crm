<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="user_modify_user_pwd" Codebehind="modify_user_pwd.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>用户信息</span><a href="#"></a></div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabUser">
        <tr>
            <td class="td_co">
                姓名：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbUserName" runat="server" CssClass="ban_in_oper_user" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                旧密码：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbOldPwd" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_co">
                新密码：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbNewPwd" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_co">
                确认新密码：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbNewPwd2" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="td_co">
                密码规则：
            </td>
            <td>
                &nbsp;密码至少8位，且必须含有字母、数字、特殊字符
            </td>
        </tr>
    </table>
    <div class="bf">
        <asp:Button ID="btnOK" runat="server" Text="保存" class="input_bc" OnClick="btnOK_Click" />
        <asp:Button ID="btnReturn" runat="server" Text="返回" class="input_fh" OnClick="btnReturn_Click" />
    </div>
</asp:Content>
