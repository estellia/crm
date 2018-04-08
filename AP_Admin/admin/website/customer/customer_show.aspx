<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="customer_show.aspx.cs" Inherits="customer_customer_show" %>

<%@ Register Src="../Controls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">

    function myCheckBox(){
    var flag=0;
        $('#tvMenuContent input[type="checkbox"]').each(function(){
        if($(this).attr("checked")){
            flag=1;
            return false;
            }
        })
        if(flag>0)
        return true;
        else
        return false;
    }

    function validate() {
        if(!validateCode($("#<%=tbCode.ClientID %>"),"编码",false,$("#customer_info"))){
            return false;
        }
        if(!validateNotEmpty($("#<%= tbname.ClientID %>"),"名称",$("#customer_info"))){
            return false;
        }
        if(!validateEnName($("#<%=tbEnglishName.ClientID %>"),"英文名称",true,$("#customer_info"))){
            return false;
        }
        if(!validateNotEmpty($("#<%= tbStartDate.ClientID %>"),"开始使用日期",$("#customer_info"))){
            return false;
        }
        if('<%=ViewState["action"]%>'=="2"){
            var startDate = $("#<%= tbStartDate.ClientID %>");
            var start = startDate.val().replace("-", "/").replace("-","/");
            var now = new Date().toLocaleDateString().replace("年", "/").replace("月", "/").replace("日", "");
            if (Date.parse(start) - Date.parse(now) < 0) {
                infobox.showPop("开始使用日期不能小于当前日期!");
                $("#customer_info").click();
                return false;
            }
        }
        if(!validateNotEmpty($("#<%= tbAddress.ClientID %>"),"地址",$("#customer_info"))){
            return false;
        }
        if(!validatePostCode($("#<%= tbPostcode.ClientID%>"),"邮编",false,$("#customer_info"))){
            return false;
        }
        if(!validateNotEmpty($("#<%= tbContacter.ClientID %>"),"联系人",$("#customer_info"))){
            return false;
        }
        if(!validatePhone($("#<%= tbTel.ClientID %>"),"电话",false,$("#customer_info"))){
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
        var tbCell = $("#<%=tbCell.ClientID %>");
        if(!validateMobile(tbCell,"手机",true,$("#customer_info"))){
            return false;
        }
//        if(!myCheckBox())
//        {
//            $("#menu_info").click();
//            infobox.showPop("菜单信息选项不能为空");
//            return false;
//        }
        <%--if(!validateNotAllowCn($("#<%=cbCustomer.ClientID %>"),"连接信息",false,$("#connect_info"))){
            return false;
        }--%>
        <%--if(!validateNotAllowCn($("#<%=tbDBServer.ClientID %>"),"数据库服务器",false,$("#connect_info"))){
            return false;
        }
        if(!validateSpecChar($("#<%= tbDBName.ClientID %>"),"数据库名称",false,$("#connect_info"))){
            return false;
        }
        if(!validateNotAllowCn($("#<%= tbDBName.ClientID %>"),"数据库名称",false,$("#connect_info"))){
            return false;
        }
        if(!validateNotEmpty($("#<%= tbDBUser.ClientID %>"),"用户名",$("#connect_info"))){
            return false;
        }
        if(!validateNotAllowCn($("#<%= tbDBPwd.ClientID %>"),"密码",false,$("#connect_info"))){
            return false;
        }
        if(!validateNotAllowCn($("#<%=tbAccessURL.ClientID %>"),"访问基地址",false,$("#connect_info"))){
            return false;
        }
        if(!checkNumber("<%=tbMaxShopCount.ClientID %>","最大门店数必须为正整数!")){
            $("#connect_info").click();
            setTimeout(function(){ $("#<%=tbMaxShopCount.ClientID %>").focus();},100);
            return false;
        }
        if (!checkNumber("<%= tbMaxUserCount.ClientID %>", "最大用户数必须为正整数!")) {
            $("#connect_info").click();
            setTimeout(function(){ $("#<%=tbMaxUserCount.ClientID %>").focus();},100);
            return false;
        }
        if (!checkNumber("<%= tbMaxTerminalCount.ClientID %>", "用户最大终端数必须为正整数!")) {
            $("#connect_info").click();
            setTimeout(function(){  $("#<%=tbMaxTerminalCount.ClientID %>").focus();},100);
            return false;
        }
        if(!validateNotAllowCn($("#<%= tbKeyFile.ClientID %>"),"数据加密文件",false,$("#connect_info"))){
            return false;
        }--%>
        return true;
    }
    //是否为正整数
    function checkNumber(id, msg) {
        var number = /^[1-9]{1,}\d*$/;
        if (!number.test($("#" + id).val())) {
            infobox.showPop(msg);
            $("#" + id).focus();
            return false;
        }
        return true;
    }
    //是否为空
    function checkEmpty(id,msg) {
        var empty = /^\s*$/;
        if (empty.test($("#" + id).val())) {
            infobox.showPop(msg);
           // $("#" + id).focus();
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
            if(<%=btnOK.Visible?"false":"true" %>)
            {
                //disableCtrls("#<%=tabContainerCustomer.ClientID %>");
            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <cc1:TabContainer ID="tabContainerCustomer" runat="server">
        <cc1:TabPanel ID="tabBasic" runat="server">
            <HeaderTemplate>
                <div class="tab_head" id="customer_info">
                    <span style="font-size: 12px;">客户信息</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                <div style="background-color: #f2f1ef;">
                     
                    <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
                        <tr>
                            <td class="td_co">
                                编码：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbCode" runat="server" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                名称：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbname" runat="server" MaxLength="25"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                英文名称：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbEnglishName" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                开始使用日期：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <input type="text" id="tbStartDate" title="双击清除时间" ondblclick="this.value=''" onclick="Calendar('MainContent_tabContainerCustomer_tabBasic_tbStartDate');"
                                    readonly="readonly" runat="server" maxlength="10" />
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
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbPostcode" runat="server" MaxLength="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                联系人：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbContacter" runat="server" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                电话：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
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
                                邮箱：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbEmail" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                手机：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbCell" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                备注：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbRemark" runat="server" Height="50px" TextMode="MultiLine" Width="431px"
                                    MaxLength="180"></asp:TextBox>
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
                                是否同步到阿拉丁：
                            </td>
                            <td class="td_lp">
                                <asp:DropDownList ID="cbIsALD" runat="server" Width="196px" >
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tabMenu" runat="server" CssClass="tab_panel" Visible="false">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;" id="menu_info">菜单信息</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                <div id="tvMenuContent">
                    <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
                        <tr>
                            <td class="td_co">
                                菜单：
                            </td>
                            <td class="td_lp" class="td_lp" style="vertical-align: top; line-height:0px; ">
                                <div class="tree-container">
                                    <asp:TreeView ID="tvMenu" runat="server" Width="100%" ExpandDepth="0" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tabConnect" runat="server" CssClass="tab_panel">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;" id="connect_info">连接信息</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                <div style="background-color: #f2f1ef;" class="ss_bg">
                    <%--<asp:GridView ID="GridView1" runat="server"
                        CellPadding="0" 
                        CellSpacing="1" 
                        CssClass="ss_jg"
                        AllowPaging="True" 
                        GridLines="None" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
                    >
                        <RowStyle CssClass="b_c4" />
                        <Columns>
                            <asp:TemplateField ItemStyle-CssClass="gv-checkbox" HeaderText="<input type='checkbox' ID='power' onclick='selectAll(this);'/>">
                                <ItemTemplate>
                                    <asp:CheckBox ID="select" CssClass="select" onclick="check()"  runat="server"  DataDeployId='<%#Eval("DataDeployId") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="DataDeployName" HeaderText="名称" />
                            <asp:BoundField DataField="CustomerCount" HeaderText="客户数量" />
                            <asp:BoundField DataField="DataDeployDesc" HeaderText="描述" />
                            
                        </Columns>
                          <PagerTemplate>
                            <uc1:Pager ID="Pager1" runat="server" RecordCount="0" />
                        </PagerTemplate>
                        <PagerStyle CssClass="pag" />
                        <HeaderStyle CssClass="b_c3" />
                        <AlternatingRowStyle CssClass="b_c5" />
                    </asp:GridView>--%>

                    <div style="height:400px; padding:0px;">
                <asp:DropDownList ID="cbCustomer" runat="server" Visible="false">

                </asp:DropDownList>
                <%-- --%>
                    <asp:GridView ID="gvCustomer" runat="server" 
                    CellPadding="0" 
                    CellSpacing="1" 
                    CssClass="ss_jg"
                       AutoGenerateColumns="False"
                        DataSourceID="odsCustomer" 
                        AllowPaging="True" 
                        DataKeyNames="DataDeployId" 
                    OnRowDataBound="gvCustomer_RowDataBound"
                        OnRowCommand="gvCustomer_RowCommand" GridLines="None" ShowHeaderWhenEmpty="True">
                        <RowStyle CssClass="b_c4" />
                        <Columns>
                            <asp:TemplateField ItemStyle-CssClass="gv-checkbox" HeaderText="">
                                <ItemTemplate>
                                    <asp:CheckBox ID="select" CssClass="select" onclick="check()"  runat="server" DataDeployId='<%#Eval("DataDeployId") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DataDeployName" HeaderText="名称" />
                            <asp:BoundField DataField="CustomerCount" HeaderText="客户数量" />
                            <asp:BoundField DataField="DataDeployDesc" HeaderText="描述" />
                        </Columns>
                        <PagerTemplate>
                            <uc1:Pager ID="Pager1" runat="server" RecordCount="0" />
                        </PagerTemplate>
                        <PagerStyle CssClass="pag" />
                        <HeaderStyle CssClass="b_c3" />
                        <AlternatingRowStyle CssClass="b_c5" />
                    </asp:GridView>


    <div class="b_bg" style=" display:none;">
        <asp:Label ID="lblCustomerName" runat="server" Text="123"></asp:Label>
        <asp:Button ID="btnQuery" CssClass="input_c" Text="选择" runat="server" OnClick="btnQuery_Click" /> 
    </div>
                    <asp:ObjectDataSource ID="odsCustomer" runat="server" EnablePaging="True" MaximumRowsParameterName="maxRowCount"
                        OnObjectCreating="odsCustomer_ObjectCreating" OnSelecting="odsCustomer_Selecting"
                        SelectCountMethod="GetTDataDeployListCount" SelectMethod="GetTDataDeployList"
                        StartRowIndexParameterName="startRowIndex" TypeName="cPos.Admin.Service.Interfaces.ICustomerService">
                    </asp:ObjectDataSource>

                    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" style="display:none;">
                        <tr>
                            <td class="td_co">
                                数据库服务器：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbDBServer" runat="server" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                数据库名：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbDBName" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                用户名：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbDBUser" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                密码：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbDBPwd" runat="server" MaxLength="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                访问基地址：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbAccessURL" runat="server" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                最大门店数：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbMaxShopCount" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                最大用户数：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbMaxUserCount" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                最大终端数：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbMaxTerminalCount" runat="server" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                数据加密文件：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbKeyFile" runat="server" MaxLength="100"></asp:TextBox>
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
                                <asp:TextBox ID="tbEditor" runat="server" CssClass="ban_in_oper_user" ReadOnly="true"></asp:TextBox>
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
