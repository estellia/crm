<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="terminal_show.aspx.cs" Inherits="customer_terminal_show" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

    var max_length = 100; //默认textarea文本长度
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

    function validate() {
        var code = $("#<%= tbCode.ClientID %>");
        var sn = $("#<%= tbSN.ClientID %>");
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
        if (empty.test(sn.val())) {
            infobox.showPop("序列号不能为空!");
             $("#basicinfo").click();
            setTimeout(function () { sn.focus(); }, 100);
            return false;
        }
        if(reg_is_char.test(sn.val())|reg_is_cn.test(sn.val())){
            infobox.showPop("序列号不能输入汉字或特殊字符!");
            $("#basicinfo").click();
            setTimeout(function(){sn.focus();},100);
            return false;
        }
        var flag = true;
        var that = null;
        $("input[num='']").each(function () {
            if (!empty.test($(this).val())) {
                if (!numb.test($(this).val())) {
                    that = $(this);
                    infobox.showPop($(this).attr("msg") + "编号应为数字、字母、下划线!");
                    $("#device").click();
                    flag = false;
                    return false;
                }
            }
        });
        if (!flag) {
            if(that){
               setTimeout(function(){that.focus();},100);
             }
            return false;
        }
        return true;
    }

    $(function () {
        $("#deviceform :checkbox").click(refreshStatus);
        function refreshStatus() {
            if(event){
                var target = event.target;
                if(!target){
                    target = event.srcElement;
                }
                if(!$(target).attr("checked")){
                    $(target).parent().parent().find(":text").val("");
                }
            }
            $("#deviceform tr").not(":checked").find(":text").attr("readonly", "readonly");
            $("#deviceform tr").has(":checked").find(":text").removeAttr("readonly");
        }
        refreshStatus();
    });  
     $(function(){
            if(<%=btnOK.Visible?"false":"true" %>)
            {
                disableCtrls("#<%=tabContainerTermianl.ClientID %>");
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <cc1:TabContainer ID="tabContainerTermianl" runat="server">
        <cc1:TabPanel ID="tabCustomer" runat="server">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;">客户信息</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                <div style="background-color: #f2f1ef;">
                    <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
                        <tr>
                            <td class="td_co">
                                编码：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbCustomerCode" ReadOnly="true" runat="server" CssClass="ban_in_oper_time"
                                    MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                名称：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbCustomerName" CssClass="ban_in_oper_time" ReadOnly="true" runat="server"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                状态：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbCustomerStatus" ReadOnly="true" CssClass="ban_in_oper_time" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tabTerminal" runat="server">
            <HeaderTemplate>
                <div class="tab_head" id="basicinfo">
                    <span style="font-size: 12px;">终端信息</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                <div style="background-color: #f2f1ef;">
                    <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
                        <tr>
                            <td class="td_co">
                                持有方式：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox runat="server" CssClass="ban_in_oper_time" ID="tbHoldType" Width="123px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                类型：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:DropDownList runat="server" Height="22px" ID="tbType" Width="127px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                品牌：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbBrand" MaxLength="25" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                型号：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbModel" MaxLength="25" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                编码：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbCode" MaxLength="30" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                序列号：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbSN" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                购买日期：
                            </td>
                            <td class="td_lp">
                                <%--<asp:TextBox ID="tbPurchaseDate" runat="server" ></asp:TextBox>--%>
                                <input type="text" runat="server" readonly="readonly" onclick="Calendar('MainContent_tabContainerTermianl_tabTerminal_tbPurchaseDate');"
                                    title="双击清除时间" id="tbPurchaseDate" ondblclick="this.value=''"  class="selectdate" style="width:123px;" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                出保日期：
                            </td>
                            <td class="td_lp">
                                <%--<asp:TextBox ID="tbInsuranceDate" runat="server" ></asp:TextBox>--%>
                                <input type="text" runat="server" readonly="readonly" ondblclick="this.value=''"
                                    onclick="Calendar('MainContent_tabContainerTermianl_tabTerminal_tbInsuranceDate');"
                                    title="双击清除时间" id="tbInsuranceDate" class="selectdate" style="width:123px;" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                程序版本：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbSoftwareVersion" CssClass="ban_in_oper_time" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                数据库版本：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbDBVersion" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                连接地址：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbWS" runat="server" Width="431px" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                备用连接地址：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbWS2" runat="server" Width="431px" CssClass="ban_in_oper_time"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                备注：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbRemark" runat="server" Height="70px" TextMode="MultiLine" MaxLength="180"
                                    Width="431px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tabDevice" runat="server" CssClass="tab_panel">
            <HeaderTemplate>
                <div class="tab_head" id="device">
                    <span style="font-size: 12px;">相关设备</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                <div style="background-color: #f2f1ef;">
                    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="deviceform">
                        <tr>
                            <td class="td_co">
                                钱箱：
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkCashBox" />
                            </td>
                            <td class="td_co">
                                钱箱编号：
                            </td>
                            <td>
                                <asp:TextBox num="" msg="钱箱" ID="tbCashNo" runat="server" MaxLength="30" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                小票打印机：
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkPrinter" />
                            </td>
                            <td class="td_co">
                                小票打印机编号：
                            </td>
                            <td>
                                <asp:TextBox num="" msg="小票打印机" ID="tbPrinterNo" runat="server" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                客显：
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkClientDisplay" />
                            </td>
                            <td class="td_co">
                                客显编号：
                            </td>
                            <td>
                                <asp:TextBox num="" msg="客显" ID="tbClientDisplayNo" runat="server" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                扫描枪：
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkScanner" />
                            </td>
                            <td class="td_co">
                                扫描枪编号：
                            </td>
                            <td>
                                <asp:TextBox num="" msg="扫描枪" ID="tbScannerNo" runat="server" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                刷卡器：
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkEcard" />
                            </td>
                            <td class="td_co">
                                刷卡器编号：
                            </td>
                            <td>
                                <asp:TextBox num="" msg="刷卡器" ID="tbEcardNo" runat="server" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                支架：
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkHolder" />
                            </td>
                            <td class="td_co">
                                支架编号：
                            </td>
                            <td>
                                <asp:TextBox num="" msg="支架" ID="tbHolderNo" runat="server" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                其它：
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkOtherDevice" />
                            </td>
                            <td class="td_co">
                                其它编号：
                            </td>
                            <td>
                                <asp:TextBox num="" msg="其它" ID="tbOtherDeviceNo" runat="server" MaxLength="30" onclick=""></asp:TextBox>
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
                            <td class="td_co">
                                创建人：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbCreater" runat="server" CssClass="ban_in_oper_user" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                创建时间：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbCreateTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                最后修改人：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbEditor" runat="server" CssClass="ban_in_oper_user" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                最后修改时间：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbEditTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="td_co">
                                系统最后修改时间：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbSysModifyTime" runat="server" CssClass="ban_in_oper_time" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <div class="bf">
        <asp:Button ID="btnOK" runat="server" OnClientClick="if(!validate()) return false;"
            Text="保存" class="input_bc" OnClick="btnOK_Click" />
        <asp:Button ID="btnReturn" runat="server" Text="返回" class="input_fh" OnClick="btnReturn_Click" />
    </div>
</asp:Content>
