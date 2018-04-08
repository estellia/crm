<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="unit_show.aspx.cs" Inherits="unit_unit_show" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        var unitProp = <%=this.UnitPropListInfo %>;
    </script>
    <script type="text/javascript"> 
        function validate() {
       
            var unitCode = $("#<%= tbUnitCode.ClientID %>");
            var unitName = $("#<%= tbUnitName.ClientID %>");
            var address = $("#<%= tbAddress.ClientID %>");
            var contact = $("#<%= tbContact.ClientID %>");
            var empty = /^\s*$/;
            var number = /^\d+$/;
            var isNull = false;
            var test = "[ ,\\`,\\~,\\!,\\@,\#,\\$,\\%,\\^,\\+,\\*,\\&,\\\\,\\/,\\?,\\|,\\:,\\.,\\<,\\>,\\{,\\},\\(,\\),\\',\\;,\\=,\"]";
             var reg_is_char = new RegExp(test);
              var reg_is_cn = /^[\u0391-\uFFE5]+$/;

            var parentUnit = document.getElementById("<%=drpUnit.ClientID %>").values(); 
            if (parentUnit.length ==0)
            {
                infobox.showPop("必须选择上级门店"); 
                $("#basicInfo").click();
                setTimeout(function(){ $("#<%=drpUnit.ClientID %>").focus();},100);
                return false;
            }
            if(parentUnit[0] == '<%=this.Request.QueryString["unit_id"] %>'){
                infobox.showPop("上级门店不能选择自己！");
                 $("#basicInfo").click();
                setTimeout(function(){ $("#<%=drpUnit.ClientID %>").focus();},100);
                return false;
            }
            if (empty.test(unitCode.val())) {
                infobox.showPop("门店编码不能为空");
                 $("#basicInfo").click();
                 setTimeout(function(){ $("#<%=tbUnitCode.ClientID %>").focus();},100);
                return false;
            }
            var reg_is_en = /^[\s,a-z,A-Z]*$/;
            if(reg_is_char.test($("#<%= tbUnitCode.ClientID %>").val())|reg_is_cn.test($("#<%= tbUnitCode.ClientID %>").val())){
                infobox.showPop("门店编码不能输入汉字或特殊字符!");
                 $("#basicInfo").click();
               setTimeout(function(){ $("#<%= tbUnitCode.ClientID %>").focus();},100);
                return false;
            }
            if (empty.test(unitName.val())) {
                infobox.showPop("门店名称不能为空");
                 $("#basicInfo").click();
                 setTimeout(function(){ $("#<%=tbUnitName.ClientID %>").focus();},100);
                return false;
            }
             if(reg_is_char.test($("#<%=tbUnitName.ClientID %>").val())){
                this.infobox.showPop("门店名称不允许输入特殊字符!");
                $("#basicInfo").click();
                setTimeout(function(){ $("#<%=tbUnitName.ClientID %>").focus();},100);
                return false;
            }
            var tbEnglishName= $("#<%=tbUnitNameEn.ClientID %>");
            var reg_is_en = /^[\s,a-z,A-Z]*$/;
            if(tbEnglishName.val()!=""){
                if(!reg_is_en.test(tbEnglishName.val())){
                    alert("请录入英文名称!");
                    $("#basicInfo").click();
                    tbEnglishName.focus();
                    return false;
                }
            }
            var city = document.getElementById("<%=DropDownTree1.ClientID %>").values();
            if (city.length == 0) {
                infobox.showPop("必须选择城市");
                 $("#basicInfo").click();
                setTimeout(function(){ $("#<%=DropDownTree1.ClientID %>").focus();},100);
                return false;
            } 
            if (empty.test(address.val())) {
                infobox.showPop("地址不能为空");
                 $("#basicInfo").click();
                 setTimeout(function () { address.focus(); }, 100);
                return false;
            }
             var tbPostcode = $("#<%= tbPostcode.ClientID%>");
           if(!validatePostCode($("#<%= tbPostcode.ClientID%>"),"邮编",true,$("#basicInfo"))){
            return false;
        }
            if (empty.test(contact.val())) {
                infobox.showPop("联系人不能为空");
                 $("#basicInfo").click();
                 setTimeout(function () { contact.focus(); }, 100);
                return false;
            }
//            var tel = $("#<%= tbTelephone.ClientID %>"); 
//            if (empty.test(tel.val())) {
//                infobox.showPop("电话不能为空");
//                 $("#basicInfo").click();
//                 setTimeout(function () { tel.focus(); }, 100);
//                return false;
//            } 
//           if (isNaN(tel.val())) {
//               this.infobox.showPop("电话不正确，请重新输入");
//              $("#basicInfo").click();
//               setTimeout(function () { tel.focus(); }, 100);
//               return false;
//            }
            var is_reg_phone = /^0\d{2,3}(\-)?\d{7,8}$|^\d{7,8}$|^0{0,1}(13[0-9]|15[0-9]|18[1-9]|14[1-9])[0-9]{8}$/; //固话
            if($("#<%= tbTelephone.ClientID %>").val()==""){
                alert("电话号码不能为空!");
                $("#customer_info").click();
                setTimeout(function(){$("#<%= tbTelephone.ClientID %>").focus();},100);
                return false;
            }
            if(!is_reg_phone.test($("#<%= tbTelephone.ClientID %>").val())){
                alert("电话号码格式不正确!");
                $("#customer_info").click();
                setTimeout(function(){$("#<%= tbTelephone.ClientID %>").focus();},100);
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
        var email = $("#<%= tbEmail.ClientID%>");
         if(!validateEmail(email,"邮箱",true,$("#customer_info"))){
                return false;
            }
            return true;
        }

        function go_back() {
            this.location.href = '<%=this.Request.QueryString["from"] ?? "unit_query.aspx"%>';
        }
        function getUnitProp() {
            var unit = savePropData("UNIT");
            $("#<%=hid_unitPorp.ClientID %>").val(JSON.stringify(unit));
        }
        $(function () {
            //  加载属性数据
            loadPropData(unitProp, "UNIT");
            $("#<%=DropDownTree1.ClientID %>")[0].onselect = function (item) {
                return item.id && item.id.length>=6;
            }
        });

        $(function(){
            if(<%=btnSave.Visible?"false":"true" %>)
            {
                disableCtrls("#<%=tabContainer1.ClientID %>");
            }
        });
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <cc1:TabContainer ID="tabContainer1" runat="server">
        <cc1:TabPanel ID="tabBasic" runat="server">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;" id="basicInfo">基础信息</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                <table class="con_tab" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="td_co">
                            上级：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <uc2:DropDownTree ID="drpUnit" runat="server" MultiSelect="false" Url="../ajaxhandler/tree_query.ashx?action=unit" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            门店编码：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <asp:TextBox ID="tbUnitCode" runat="server" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            门店名称：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <asp:TextBox ID="tbUnitName" runat="server" MaxLength="25"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            英文名称：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbUnitNameEn" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            门店简称：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbUnitShortName" runat="server" MaxLength="15"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            门店类型：
                        </td>
                        <td class="td_lp">
                            <asp:DropDownList ID="ddlUnitType" runat="server" DataTextField="Type_Name" DataValueField="Type_Id"
                                Width="125px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            城市：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <uc2:DropDownTree ID="DropDownTree1" runat="server" style="float: left" Url="../ajaxhandler/tree_query.ashx?action=city" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            地址：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <asp:TextBox ID="tbAddress" runat="server" MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            邮编：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbPostcode" runat="server" MaxLength="6" Width="125px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            联系人：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <asp:TextBox ID="tbContact" runat="server" MaxLength="25"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            电话：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <asp:TextBox ID="tbTelephone" runat="server" MaxLength="50" Width="125px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            传真：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbFax" runat="server" MaxLength="30" Width="125px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            邮箱：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbEmail" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            经度：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbLongitude" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_co">
                            维度：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbDimension" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:Button runat="server" Text="店铺信息" ID="btnStoreInfo" Enabled="False" OnClick="btnStoreInfo_Click"
                    class="input_bc" Visible="False" />
                <asp:Panel ID="panelShop" runat="server" Visible="False" GroupingText="店铺信息">
                    <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
                        <tr>
                            <td class="td_co">
                                店商圈：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                店面积：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                店铺：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                临界否：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                十字路口：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                竞争环境：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
                    <tr>
                        <td class="td_co">
                            备注：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbRemark" runat="server" Height="70px" MaxLength="180" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tabPropertyInfo" runat="server" CssClass="tab_panel">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;">属性信息</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                <div style="background-color: #f2f1ef;">
                    <%= PropHelper.PropHelperSingleton.CreationPropGroup("UNIT") %>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <div class="bf">
        <asp:Button ID="btnSave" runat="server" OnClientClick="if(!validate()) return false;getUnitProp();"
            Text="保存" class="input_bc" OnClick="btnSave_Click" />
        <input type="button" onclick="go_back()" value="返回" class="input_fh" />
    </div>
    <input type="hidden" runat="server" id="hid_unitPorp" />
</asp:Content>
