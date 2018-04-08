<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="warehouse_show.aspx.cs" Inherits="config_warehouse_show" %>

<%@ Register Src="../controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">


    var max_length = 90; //默认textarea文本长度
        $(function () {
            $("textarea").each(function () {
                if ($(this).hasClass("customer")) {
                    $(this).keyup(validateMaxLength).blur(validateMaxLength);
                }
            });
        });
        function validateMaxLength() {
            var target = event.target;
            if (!target) {
                target=event.srcElement;
            }
            var _length = $(target).val().length;
            if (_length > max_length) {
                target.value = $(target).val().substring(0, max_length);
            }
        }
        function ValidateInput() {
        if (!$("#<%=DropDownTree1.ClientID %>")[0].values()[0]){
            this.infobox.showPop("所属单位不能为空!");
            $("#<%=DropDownTree1.ClientID %>").focus();
            return false;
        }
        if(!validateCode($("#<%=tbCode.ClientID %>"),"编码",false)){
            return false;
        }
        if(!validateSpecChar($("#<%=tbName.ClientID %>"),"名称",false)){
            return false;
        }
        if(!validateEnName($("#<%=tbEnglishName.ClientID %>"),"英文名称",true)){
            return false;
        }
          if(!validatePhone($("#<%= tbTel.ClientID %>"),"电话",false)){
            return false;
        }
          var tbFax = $("#<%=tbFax.ClientID %>");
            var testFax=/^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})/;
            if(tbFax.val()!=""){
            if (!testFax.test(tbFax.val())) {
                this.infobox.showPop("传真不正确，请重新输入");
                  $("#customer_info").click();
                setTimeout(function () { tbFax.focus(); }, 100);
                return false;
            }
            }
        return true;
    }
    function go_Back() {
        location.href = '<%=this.Request.QueryString["from"] ?? "warehouse_query.aspx"%>'
    }
    function showRegion(img, tab) {
        var imageid = $("#" + img);
        var table = $("#" + tab);
        if (imageid.attr("src") == "../img/tit_r.png") {
            imageid.attr("src", "../img/tit_dr.png");
            table.slideUp("slow");
        }
        else if (imageid.attr("src") == "../img/tit_dr.png") 
        {
            imageid.attr("src", "../img/tit_r.png");
            table.slideDown("slow");
        }
    }
            $(function(){
            if(<%=btnSave.Visible?"false":"true" %>)
            {
                disableCtrls("#tabCustomer");
            }
        });
    </script>
    <link href="../css/tree.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Plugins/jquery.tree.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>仓库信息</span> <a href="#">
            <img id="imgCustomer" src="../img/tit_r.png" onclick="showRegion('imgCustomer', 'tabCustomer')"
                alt="" /></a></div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabCustomer">
        <tr>
            <td class="td_co">
                所属单位：
            </td>
            <td>
                <div class="box_r">
                </div>
                <uc1:DropDownTree ID="DropDownTree1" runat="server" MultiSelect="false" Url="../ajaxhandler/tree_query.ashx?action=unit" />
            </td>
        </tr>
        <tr>
            <td class="td_co">
                编码：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbCode" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                名称：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbName" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                英文名称：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbEnglishName" runat="server" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                地址：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbAddress" runat="server" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                联系人：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbContacter" runat="server" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                电话：
            </td>
            <td>
                <div class="box_r"></div>
                <asp:TextBox ID="tbTel" runat="server" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                传真：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbFax" runat="server" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;默认仓库：
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="cbDefault" runat="server">
                    <asp:ListItem Value="1">是</asp:ListItem>
                    <asp:ListItem Value="-1">否</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                状态：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbStatus" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                备注：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbRemark" runat="server" Width="431px" Height="50px" MaxLength="180"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="tit_con">
        <span>操作信息</span> <a href="#">
            <img id="imgUser" src="../img/tit_dr.png" onclick="showRegion('imgUser', 'tabUser')"
                alt="" /></a></div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabUser" style="display: none">
        <tr>
            <td class="td_co">
                创建人：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCreater" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                创建时间：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCreateTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                最后修改人：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbEditor" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                最后修改时间：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbEditTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td_co">
                系统最后修改时间：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbSysModifyTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="bf">
        <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="input_bc" OnClientClick="if(!ValidateInput()){return false;}"
            OnClick="btnSave_Click" />&nbsp
        <input type="button" id="btnReturn" onclick="go_Back()" value="返回" class="input_fh" />
    </div>
</asp:Content>
