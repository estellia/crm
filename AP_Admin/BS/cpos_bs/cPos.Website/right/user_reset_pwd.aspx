<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="user_reset_pwd.aspx.cs" Inherits="right_user_reset_pwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function validateInput() {
            var newpassword = $("#<%=tbNewPass.ClientID %>")
            var rnewpassword = $("#<%=tbRNewPass.ClientID%>")
            if (newpassword.val() == "") {
                this.infobox.showPop("新密码不能为空！");
                newpassword.focus();
                return false;
            }
            if (rnewpassword.val() == "") {
                this.infobox.showPop("确认密码不能为空！");
                rnewpassword.focus();
                return false;
            }
            if (newpassword.val() != rnewpassword.val()) {
                this.infobox.showPop("确认密码与新密码不一致，请重新输入！");
                rnewpassword.focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>修改密码</span>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" class="con_tab">
        <tr>
            <td class="td_co" >
                用户名：
            </td>
            <td class=" td_lp">
                <asp:TextBox CssClass="ban_in_oper_user" runat="server" ReadOnly="true"
                    ID="tbUserName"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co" >
                新密码：
            </td>
            <td>
                <div class="box_r"></div>
                <asp:TextBox runat="server" TextMode="Password" ID="tbNewPass"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co" >
                确认密码：
            </td>
            <td>
                <div class="box_r"></div>
                <asp:TextBox  runat="server" TextMode="Password" ID="tbRNewPass"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="bf">
         <asp:Button ID="btnOK" runat="server" Text="确认" CssClass=" input_bc" OnClick="btnOK_Click"  OnClientClick="if(!validateInput()) return false;"/>&nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass=" input_fh"
                    OnClick="btnReturn_Click" />
    </div>
</asp:Content>
