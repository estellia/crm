<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="right_menu_show" Codebehind="menu_show.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function validate() {
            var display_index = $('#<%=tbDisplayIndex.ClientID %>');
            var code = $('#<%=tbCode.ClientID %>');
            var name = $('#<%=tbName.ClientID %>');
            var name_en = $('#<%=tbEnglishName.ClientID %>'); 
            var url = $('#<%=tbURL.ClientID %>'); 
            //数字验证
            var reg_is_num = new RegExp("^[0-9]{1,5}$");
            //空
            var reg_is_empty = /^\s*$/;
            var test = "[ ,\\`,\\~,\\!,\\@,\#,\\$,\\%,\\^,\\+,\\*,\\&,\\\\,\\/,\\?,\\|,\\:,\\.,\\<,\\>,\\{,\\},\\(,\\),\\',\\;,\\=,\"]";
            var reg_is_char = new RegExp(test);
            var reg_is_cn = /^[\u0391-\uFFE5]+$/;
            //英文
            var reg_is_en = /^[\s,a-z,A-Z]*$/;
            //url
            var reg_is_url = /^[a-z,A-Z,0-9,\.\&\-\&&\=_\?\/]*$/;//目前没做URL验证

            if (!reg_is_num.test(display_index.val())){
                alert("显示顺序应为 1-5 位数字!");
                display_index.focus();
                return false;
            }

            if (reg_is_empty.test(code.val())) {
                code.focus();
                alert("编码不能为空!");
                return false;
            }
            if (reg_is_cn.test(code.val())|reg_is_char.test(code.val())) {
                code.focus();
                alert("编码不允许输入汉字或特殊字符!");
                return false;
            }
            if (reg_is_empty.test(name.val())) {
                name.focus();
                alert("名称不能为空!");
                return false;
            }
            if (reg_is_char.test(name.val())) {
                name.focus();
                alert("名称不允许输入特殊字符!");
                return false;
            }
            if(!reg_is_en.test(name_en.val())){
                name_en.focus();
                alert("请录入英文字母!");
                return false;
            }

            //if (url.val() == "" || url.val()==undefined) {
            //   url.focus();
            //    alert("请录正确的url");
            //    return false;
            //}

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>菜单信息</span><%--<a href="#"></a>--%>
        <a href="#"><img id="imgMenu" src="../img/tit_r.png" onclick="showRegion('imgMenu', 'tabMenu')" alt=""/></a></div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabMenu">
        <tr>
            <td  class="td_co">
                显示顺序：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbDisplayIndex" runat="server" MaxLength="5"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                编码：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbCode" runat="server" MaxLength="30"  ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                名称：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbName" runat="server" MaxLength="25" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                英文名称：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbEnglishName" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                访问路径：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbURL" runat="server" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                状态：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbStatus" runat="server"  CssClass="ban_in" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                客户可见：
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="cbCustomerVisible" runat="server" CssClass="td_select" >
                    <asp:ListItem Value="1">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                备注：
            </td>
            <td class="td_lp">
                <textarea runat="server" CssClass="ban_in" id="txtRemark"  rows="3" cols="21"></textarea>
            </td>
        </tr>
    </table>
    <div class="tit_con">
        <span>操作信息</span><a href="#"><img id="imgOper" src="../img/tit_r.png" onclick="showRegion('imgOper', 'tabOper')" alt=""/></a></div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabOper">
        <tr>
            <td  class="td_co">
                创建人：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCreater" runat="server" CssClass="ban_in_oper_user" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                创建时间：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCreateTime" runat="server" CssClass="ban_in_oper_time"  ReadOnly="true"/>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                最后修改人：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbEditor" runat="server" CssClass="ban_in_oper_user"  ReadOnly="true"/>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                最后修改时间：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbEditTime" runat="server" CssClass="ban_in_oper_time"  ReadOnly="true"/>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                最后操作时间：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbSysModifyTime" runat="server" CssClass="ban_in_oper_time"  ReadOnly="true"/>
            </td>
        </tr>
    </table>
    <div class="bf">
        <asp:Button ID="btnOK" runat="server" Text="保存" class="input_bc" OnClick="btnOK_Click" OnClientClick="if(!validate()){return false;}" />
        <asp:Button ID="btnReturn" runat="server" Text="返回" class="input_fh" OnClick="btnReturn_Click" />
    </div>
</asp:Content>
