<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="announce_show.aspx.cs" Inherits="exchange_announce_show" %>

<%@ Register Src="~/controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function ValidateInput() {

            //            if ($("#<%=tbNo.ClientID %>").val() == "") {
            //                this.infobox.showPop("编码不能为空");
            //                $("#<%=tbNo.ClientID %>").focus();
            //                return false;
            //            }
            //            var r = /^[0-9]*[1-9][0-9]*$///正整
            //            if (!r.test($("#<%=tbNo.ClientID %>").val())) {
            //                alert("编码输入格式为正整数");
            //                $("#<%=tbNo.ClientID %>").focus();
            //                return false;
            //            }
            if ($("#<%=tbTitle.ClientID %>").val() == "") {
                this.infobox.showPop("主题不能为空!");
                $("#<%=tbTitle.ClientID %>").focus();
                return false;
            }
            if ($("#<%=tvUnit.ClientID %>")[0].values().length == 0) {
                this.infobox.showPop("必须选择通告单位!");
                $("#<%=tvUnit.ClientID %>").focus();
                return false;
            }
            if ($("#<%=tbContent.ClientID %>").val() == "") {
                this.infobox.showPop("内容不能为空!");
                $("#<%=tbContent.ClientID %>").focus();
                return false;
            }
            if ($("#<%=tbBeginDate.ClientID %>").val() == "") {
                this.infobox.showPop("通告期限不能为空!");
                $("#<%=tbBeginDate.ClientID %>").focus();
                return false;
            }
            if ($("#<%=tbEndDate.ClientID %>").val() == "") {
                this.infobox.showPop("通告期限不能为空!");
                $("#<%=tbEndDate.ClientID %>").focus();
                return false;
            }
            return true;
        }
        function go_back() {
            location.href = '<%=this.Request.QueryString["from"] ?? "announce_query.aspx"%>'
        }
        $(function () {
            var resquert = '<%=this.Request.QueryString["strDo"] %>';
            if (resquert == "2") {
                $("#<%=tvUnit.ClientID %>").click();
            }
            $(".pop").css("width", $("#MainContent_tabContainerAnnounce_tabBasic_tvUnit").css("width"));
        })

        var max_length = 500; //默认textarea文本长度
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

         $(function(){
            if(<%=btnOK.Visible?"false":"true" %>)
            {
                disableCtrls("#<%=tabContainerAnnounce.ClientID %>");
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <cc1:TabContainer ID="tabContainerAnnounce" runat="server" ActiveTabIndex="0">
        <cc1:TabPanel ID="tabBasic" runat="server">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;">基本信息</span></div>
            
</HeaderTemplate>
            

<ContentTemplate>
                <div style="background-color: #f2f1ef;">
                    <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
                        <tr>
                            <td  class="td_co">
                                编号：
                            </td>
                            <td class="td_lp" colspan="3">
                                <asp:TextBox ID="tbNo" MaxLength="50" runat="server" CssClass="fl ban_in_oper_time"
                                    ReadOnly="True" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                            <td  class="td_co">
                                主题：
                            </td>
                            <td colspan="3">
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbTitle" runat="server" CssClass="fl" MaxLength="25" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                            <td width="150"  class="td_co">
                                通告单位：
                            </td>
                            <td colspan="3">
                                <div class="box_r">
                                </div>
                                 <uc1:DropDownTree ID="tvUnit" runat="server" MultiSelect="false" Url="../ajaxhandler/tree_query.ashx?action=unit"  /> 
                            </td>
                        </tr>
                        <tr>
                            <td  class="td_co">
                                内容：
                            </td>
                            <td colspan="3">
                                <div class="box_r" style="height: 104px;">
                                </div>
                                <asp:TextBox ID="tbContent"  TextMode="MultiLine" runat="server" MaxLength="200"
                                    CssClass="fl customer" Height="100px"></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                            <td  class="td_co">
                                发布人：
                            </td>
                            <td class="td_lp" colspan="3">
                                <asp:TextBox ID="tbPublisher" MaxLength="50" runat="server" ></asp:TextBox> 
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co" style="width: 150px;">
                                通告期限：
                            </td>
                            <td style="width:100px;*width:100px;">
                                    <input type="text" disabled="disabled" style="width:5px; height:22px; float:none; background-color:Red; border-width:0px; padding-left:0px; margin-left:3px;"/>
                                    <input type="text" runat="server" ondblclick='this.value=""' readonly="readonly"
                                        onclick="Calendar('MainContent_tabContainerAnnounce_tabBasic_tbBeginDate','MainContent_tabContainerAnnounce_tabBasic_tbInsuraceDateEnd');"
                                        title="双击清除时间" id="tbBeginDate" class="selectdate" style="float:none; margin-left:-5px;"/></td>
                               <td style="width:75px;text-align:center;*width:75px;">
                               至
                               </td>
                            <td>
                                <input id="tbEndDate" runat="server" onclick="Calendar('MainContent_tabContainerAnnounce_tabBasic_tbEndDate');"
                                        readonly="readonly" title="双击清除时间" type="text" style="float:none" class="selectdate" ondblclick='this.value=""' /></td>
                        </tr>
                        <tr>
                            <td  class="td_co" style="width: 150px;">
                                允许下发：
                            </td>
                            <td class="td_lp" colspan="3">
                                <asp:TextBox runat="server" ID="cbAllowDownload" CssClass="ban_in_oper_time"  
                                    ReadOnly="True"></asp:TextBox>


                            </td>
                        </tr>
                    </table>
                </div>
            
</ContentTemplate>
        

</cc1:TabPanel>
        <cc1:TabPanel ID="tabOper" runat="server" CssClass="tab_panel">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;">操作信息</span></div>
            
</HeaderTemplate>
            

<ContentTemplate>
                <div style="background-color: #f2f1ef;">
                    <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
                        <tr>
                            <td width="150"  class="td_co">
                                创建人：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbCreater" runat="server" CssClass="ban_in_oper_user" ReadOnly="true"
                                    Width="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td  class="td_co">
                                创建时间：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbCreateTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"
                                    Width="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td  class="td_co">
                                最后修改人：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbEditor" runat="server" CssClass="ban_in_oper_user" ReadOnly="true"
                                    Width="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td  class="td_co">
                                最后修改时间：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbEditTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"
                                    Width="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td  class="td_co">
                                系统最后修改时间：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbSysModifyTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"
                                    Width="200"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            
</ContentTemplate>
        

</cc1:TabPanel>
    </cc1:TabContainer>
    <div class="bf">
        <asp:Button ID="btnOK" runat="server" OnClientClick="if(!ValidateInput()) return false;"
            Text="保存" class="input_bc" OnClick="btnOK_Click" />
        <input type="button" onclick="go_back()" value="返回" class=" input_fh" />
    </div>
</asp:Content>