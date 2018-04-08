<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="category_show.aspx.cs" Inherits="item_category_show" %>

<%@ Register Src="../controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="../css/tree.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Plugins/jquery.tree.js" type="text/javascript"></script>
    <script type="text/javascript">
        function go_Back() {
            location.href = '<%=this.Request.QueryString["from"] ?? "category_query.aspx"%>'
        }
        function ValidateInput() {
            if ($("#<%=selParentItemCategory.ClientID %>")[0].values().length == 0) {
                alert("请选择上级商品类型");
                $("#<%=selParentItemCategory.ClientID %>").focus();
                return false;
            }
             var test = "[ ,\\`,\\~,\\!,\\@,\#,\\$,\\%,\\^,\\+,\\*,\\&,\\\\,\\/,\\?,\\|,\\:,\\.,\\<,\\>,\\{,\\},\\(,\\),\\',\\;,\\=,\"]";
            var reg_is_char = new RegExp(test);
            var reg_is_cn = /^[\u0391-\uFFE5]+$/;
           if ($("#<%=tbItemCategoryCode.ClientID %>").val() == "") {
                alert("请输入商品类别编码");
                $("#<%=tbItemCategoryCode.ClientID %>").focus();
                return false;
            }
            if(reg_is_char.test($("#<%=tbItemCategoryCode.ClientID %>").val())|reg_is_cn.test($("#<%=tbItemCategoryCode.ClientID %>").val())){
                this.infobox.showPop("商品类别编码不允许输入中文或特殊字符!");
                $("#<%=tbItemCategoryCode.ClientID %>").focus();
                return false;
            }
            if ($("#<%=tbItemCategoryName.ClientID %>").val() == "") {
                alert("请输入商品类别名称");
                $("#<%=tbItemCategoryName.ClientID %>").focus();
                return false;
            }
            if ($("#<%=tbItemCategoryPYZJM.ClientID %>").val() == "") {
                alert("请输入拼音助记码");
                $("#<%=tbItemCategoryPYZJM.ClientID %>").focus();
                return false;
            }
            var tbEnglishName= $("#<%=tbItemCategoryPYZJM.ClientID %>");
            var reg_is_en = /^[\s,a-z,A-Z]*$/;
            if(tbEnglishName.val()!=""){
                if(!reg_is_en.test(tbEnglishName.val())){
                    alert("拼音助记码请录入拼音!");
                   $("#<%=tbItemCategoryPYZJM.ClientID %>").focus();
                    return false;
                }
            }
            return true;
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
        <span>分类信息</span>
    </div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tbShow">
        <tr>
            <td  class="td_co">
                上级商品类型：
            </td>
            <td>
                <div class="box_r">
                </div>
                <uc1:DropDownTree ID="selParentItemCategory"  runat="server"  Url="../ajaxhandler/tree_query.ashx?action=category"/>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                商品类别编码：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbItemCategoryCode"  runat="server"  MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                商品类别名称：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbItemCategoryName"  runat="server"  MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                拼音助记码：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbItemCategoryPYZJM" runat="server" 
                    MaxLength="30"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="bf">
        <asp:Button ID="btSave" runat="server" Text="保存" CssClass="input_bc"  OnClientClick="if(!ValidateInput()){return false;}"
            OnClick="btSave_Click" />&nbsp
        <input type="button" id="btCancle" onclick="go_Back()" value="返回" class="input_fh" />
    </div>
</asp:Content>
