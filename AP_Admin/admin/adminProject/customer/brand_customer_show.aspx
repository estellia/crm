<%@ Page Title="品牌客户" Language="C#" MasterPageFile="~/common/Site.master" 
    AutoEventWireup="true" Inherits="brand_customer_show" Codebehind="brand_customer_show.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
    $(function(){
        disableCtrls("#tabOper");
    });
    function validate() {
        var code = $("#<%= tbCode.ClientID %>");
        var name = $("#<%= tbName.ClientID %>");
        var empty = /^\s*$/;
        var number = /^\d+$/;
        var test = "[ ,\\`,\\~,\\!,\\@,\#,\\$,\\%,\\^,\\+,\\*,\\&,\\\\,\\/,\\?,\\|,\\:,\\.,\\<,\\>,\\{,\\},\\(,\\),\\',\\;,\\=,\"]";
        var reg_is_char = new RegExp(test);
        var reg_is_cn = /^[\u0391-\uFFE5]+$/;
        var numb=/^[0-9a-zA-Z_,\/\-]{1,}$/;
        //英文
        var reg_is_en = /^[\s,a-z,A-Z]*$/;
            if(reg_is_char.test($("#<%= tbCode.ClientID %>").val())|reg_is_cn.test($("#<%= tbCode.ClientID %>").val())){
                infobox.showPop("编码不能输入汉字或特殊字符!");
                $("#basicinfo").click();
               setTimeout(function(){ $("#<%= tbCode.ClientID %>").focus();},100);
                return false;
            }
        if (empty.test(name.val())) {
            infobox.showPop("名称不能为空!");
             $("#basicinfo").click();
            setTimeout(function () { name.focus(); }, 100);
            return false;
        }
        var flag = true;
        var that = null;
        if (!flag) {
            if(that){
               setTimeout(function(){that.focus();},100);
             }
            return false;
        }
        return true;
    };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>品牌客户信息</span>
        <a href="#">
            <img id="imgCustomer" src="../img/tit_r.png" onclick="showRegion('imgCustomer', 'tabCustomer')"
                alt="" /></a>
    </div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabBrandCustomer">
        <tr>
            <td class="td_co">编码：</td>
            <td class="td_lp">
                <asp:TextBox ID="tbCode" runat="server" CssClass="ban_in" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">名称：</td>
            <td class="td_lp">
                <asp:TextBox ID="tbName" runat="server" CssClass="ban_in" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">英文名称：</td>
            <td class="td_lp">
                <asp:TextBox ID="tbEng" runat="server" CssClass="ban_in" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">地址：</td>
            <td class="td_lp">
                <asp:TextBox ID="tbAddress" runat="server" CssClass="ban_in" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">邮编：</td>
            <td class="td_lp">
                <asp:TextBox ID="tbPost" runat="server" CssClass="ban_in" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">联系人：</td>
            <td class="td_lp">
                <asp:TextBox ID="tbContacter" runat="server" CssClass="ban_in" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">电话：</td>
            <td class="td_lp">
                <asp:TextBox ID="tbTel" runat="server" CssClass="ban_in" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">邮箱：</td>
            <td class="td_lp">
                <asp:TextBox ID="tbEmail" runat="server" CssClass="ban_in" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">状态：</td>
            <td class="td_lp">
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ban_in">
                    <asp:ListItem Text="正常" Value="1" Selected="True" />
                    <asp:ListItem Text="停用" Value="0" />
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div class="tit_con">
        <span>操作信息</span><a href="#"><img id="imgOper" src="../img/tit_r.png" onclick="showRegion('imgOper', 'tabOper')"
            alt="" /></a>
    </div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabOper">
        <tr>
            <td class="td_co">创建时间：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCreateTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">创建人：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCreateUser" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">最后修改时间：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbModifyTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">最后修改人：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbModifyUser" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">系统最后修改时间：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbSysModifyTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="bf">
        <asp:Button ID="btnOK" runat="server" OnClientClick="if(!validate()) return false;"
            Text="保存" class="input_bc" OnClick="btnOK_Click" />
        <input type="button" name="button" onclick="location.href='<%=this.Request.QueryString["from"]??"show_query.aspx" %>    '"
            value="返回" class="input_fh" />
    </div>

</asp:Content>

