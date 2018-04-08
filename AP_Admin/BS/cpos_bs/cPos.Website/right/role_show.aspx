<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="role_show.aspx.cs" Inherits="right_role_show" %>

<%@ Register Src="../controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .tree-container
        {
            height: 450px;
            width: 400px;
            overflow: auto;
            background-color: White;
            border: 1px solid #ccc;
             margin-top:0px;
        }
    </style>
    <script type="text/javascript">
        function go_Back() {
            location.href = '<%=this.Request.QueryString["from"] ?? "role_query.aspx"%>'
        }
        function ValidateInput() {
            if ($("#<%= drpAppSys.ClientID%>").val() == "0") {
                this.infobox.showPop("必须选择应用系统!");
                $("#<%= drpAppSys.ClientID%>").focus();
                return false;
        }
            var test = "[ ,\\`,\\~,\\!,\\@,\#,\\$,\\%,\\^,\\+,\\*,\\&,\\\\,\\/,\\?,\\|,\\:,\\.,\\<,\\>,\\{,\\},\\(,\\),\\',\\;,\\=,\"]";
            var reg_is_char = new RegExp(test);
            var reg_is_cn = /^[\u0391-\uFFE5]+$/;
            if ($("#<%=tbRoleCode.ClientID %>").val() == "") {
                this.infobox.showPop("角色编码不能为空!");
                $("#<%= tbRoleCode.ClientID%>").focus();
                return false;
            }
            if(reg_is_char.test($("#<%=tbRoleCode.ClientID %>").val())|reg_is_cn.test($("#<%=tbRoleCode.ClientID %>").val())){
                this.infobox.showPop("角色编码不允许输入中文或特殊字符!");
                $("#<%=tbRoleCode.ClientID %>").focus();
                return false;
            }
            if ($("#<%=tbRoleName.ClientID %>").val() == "") {
                this.infobox.showPop("角色名称不能为空!");
                $("#<%=tbRoleName.ClientID %>").focus();
                return false;
            }
            if(reg_is_char.test($("#<%=tbRoleName.ClientID %>").val())){
                this.infobox.showPop("角色名称不允许输入特殊字符!");
                $("#<%=tbRoleName.ClientID %>").focus();
                return false;
            }
            var reg_is_en = /^[\s,a-z,A-Z]*$/;
            var tbRoleNameEn = $("#<%=tbRoleNameEn.ClientID %>");
            if(!reg_is_en.test(tbRoleNameEn.val())){
                infobox.showPop("请录入英文!");
                tbRoleNameEn.focus();
                return false;
            }
            return true;
        }

        $(function () {
            $("div.tree-container :checkbox").click(
            function () {
                if ($(this).is(":checked")) {
                    $(this).parentsUntil("div.tree-container").filter("div").prev("table").find(":checkbox").attr("checked", "checked");
                }else{ 
                    unsel_notice(this);
                } 
            } 
        );
        }); 

        function unsel_notice(chkbox)
            {
                if(!$(chkbox).closest("table").siblings().find(":checkbox").is(":checked"))
                {
                       var $check = $(chkbox).closest("div").prev("table").find(":checkbox"); 
                       if($check.length>0 && $("div.tree-container").has($check).length>0)
                       {
                            $check.removeAttr("checked");
                            unsel_notice($check);
                       }
                } 
            }

         
        $(function(){
            if(<%=btSave.Visible?"false":"true" %>)
            {
                disableCtrls("#tbShow");
            }
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span id="RoleBar" runat="server"></span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tbShow" width="500px">
        <tr>
            <td  class="td_co">
                应用系统：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:DropDownList ID="drpAppSys" runat="server"  AutoPostBack="true"
                    OnSelectedIndexChanged="drpAppSys_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                角色编码：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbRoleCode"  runat="server"  MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                角色名称：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbRoleName"  runat="server"  MaxLength="15"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                英文名：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbRoleNameEn" runat="server"  MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                系统保留：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:RadioButton Text="是" runat="server" ID="tbIsSys1" GroupName="tbIsSys" Checked="true" />
                <asp:RadioButton Text="否" runat="server" ID="tbIsSys2" GroupName="tbIsSys" />
            </td>
        </tr>
        <tr>
            <td  class="td_co" style="vertical-align: top; ">
                菜单：
            </td>
            <td class="td_lp" style="vertical-align: top; line-height:0px; ">
                <div class="tree-container">
                    <asp:TreeView ID="tvMenu" runat="server">
                    </asp:TreeView>
                </div>
            </td>
        </tr>
    </table>
    <div class="bf" style="width: 100%">
        <asp:Button ID="btSave" runat="server" Text="保存" CssClass="input_bc" OnClientClick="if(!ValidateInput()){return false;}"
            OnClick="btSave_Click" />&nbsp
        <input type="button" id="btCancle" onclick="go_Back()" value="返回" class="input_fh" />
    </div>
</asp:Content>
