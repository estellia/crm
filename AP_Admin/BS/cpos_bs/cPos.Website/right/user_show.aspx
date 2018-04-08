<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="user_show.aspx.cs" Inherits="right_user_show" %>

<%@ Register Src="~/controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/JSON2.js" type="text/javascript"></script>
    <script type="text/javascript">
        var roleInfoData = <%= this.hidRoleInfo.Value %>;
    </script>
    <script type="text/javascript">
        function addPost(){
            var appSys = $("#<%=dropAppSys.ClientID %>");
            var roleId = $("#<%=drpRole.ClientID %>").val();
            var role = $("#" + roleId).text();
            var unit = $("#<%=drpUnit.ClientID %>")[0].texts()[0];
            var unitId = $("#<%=drpUnit.ClientID %>")[0].values()[0];
            var temp = $("#<%=rd1.ClientID %>").attr("checked") ? "是" : "否";
            if(appSys.val()=="-1"){
                infobox.showPop("必须选择应用系统!");
                appSys.focus();
                return;
            }
            if(!roleId){
                infobox.showPop("必须选择角色!")
                $("#<%=drpRole.ClientID %>").focus();
                return;
            }
            if(!unitId){
                infobox.showPop("必须选择单位!");
                $("#<%=drpUnit.ClientID %>").focus();
                return;
            }
            for (var i = 0; i < roleInfoData.length; i++) {
                if (roleInfoData[i].RoleId == roleId && roleInfoData[i].UnitId == unitId) {
                    infobox.showPop("该职务信息已存在，请重新输入!");
                    return;
                }
            }
            var para = {};
            para.RoleId = roleId;
            para.UnitId = unitId;
            para.Id = "";
            para.DefaultFlag = temp == "是" ? 1 : 0;
            para.RoleName = role;
            para.UnitName = unit;
            roleInfoData.push(para);
            buildUnitTable();
        }
        function buildUnitTable(){
            $("#tbRoleInfo tr[role_id]").remove();
            for(var i=0;i<roleInfoData.length;i++){
                buildUnitTr(roleInfoData[i]);
            }
        }
        //添加组织信息
        function buildUnitTr(data) {
            var length = $("#tbRoleInfo tr").length;
            var _class = "";
            if (length % 2 == 0) {
                _class = "b_c5";
            } else {
                _class = "b_c4";
            }
            var show = "";
            if('<%=ViewState["strDo"] %>'=='Visible'){
                show = "none";
            }
            var tr = $("<tr class=\"" + _class + "\" role_info_id=\"\" role_id=\"" + data.RoleId + "\" unit_id=\"" + data.UnitId + "\"><td>" + data.RoleName + "</td><td>" + data.UnitName + "</td><td>" + (data.DefaultFlag==1?"是":"否") + "</td><td><a style=\"display:"+show+"\" href=\"#\" onclick=\"go_Del(this);\"><img src=\"../img/delete.png\" title='删除' /></a></td></tr>");
            $("#tbRoleInfo tbody").append(tr);
            getTableShowOrHide("tbRoleInfo","title1");
            
        }
        //获取角色数据并加载到select
        function getRole(sender) {
            $("#<%=drpRole.ClientID %>").children().remove();
            $.post("Default.aspx", { "action": "getRole", "appSysId": $(sender).val() }, function (data, success) {
                if (data) {
                    for (var i = 0; i < data.length; i++) {
                        var option = "<option id=\"" + data[i].Role_Id + "\" value=\"" + data[i].Role_Id + "\">" + data[i].Role_Name + "</option>";
                        $("#<%=drpRole.ClientID %>").append(option);
                    }
                }
            }, "json");
        }
        //删除组织信息
        function go_Del(sender) {
            var parent = $(sender).parent().parent();
            var roleId = $(parent).attr("role_id");
            var unitId = $(parent).attr("unit_id");
            $(parent).remove();
            getTableShowOrHide("tbRoleInfo","title1");
            $(roleInfoData).each(function () {
                if (this.RoleId == roleId && this.UnitId == unitId) {
                    roleInfoData.removeValue(this);
                }
            });
        }
        //验证用户输入的数据
        function checkData() {
            var username = $("#<%=tbUserName.ClientID %>");
            var usercode = $("#<%=tbUserCode.ClientID %>");
            var userpassword = $("#<%=tbUserPwd.ClientID %>");
            var telphone = $("#<%=tbTelPhone.ClientID %>");
            var email = $("#<%= tbEmail.ClientID%>");
            var tbPostcode = $("#<%= tbPostcode.ClientID%>");
            var tbCallPhone = $("#<%= tbCallPhone.ClientID%>");
            var tbFailDate=$("#<%= tbFailDate.ClientID%>");
            var tbUserNameEn = $("#<%=tbUserNameEn.ClientID %>");
            var tbUserIdentity=$("#<%=tbUserIdentity.ClientID %>");

          
           
            if(!validateNotEmpty(username,"姓名",$("#basicinfo"))){
                return false;
            }
            if(!validateNotEmpty(usercode,"工号",$("#basicinfo"))){
                return false;
            }
            if(!validateEnName(tbUserNameEn,"英文名称",true,$("#basicinfo"))){
                return false;
            }

           

            if(!validateNotEmpty(userpassword,"密码",$("#basicinfo"))){
                return false;
            }
            if(!validateNotEmpty(tbFailDate,"有效日期",$("#basicinfo"))){
                return false;
            }
            if(!Date.prototype.isBigThanCurrentDate(tbFailDate.val()))
            {
                alert("有效日期不能小于当前日期!");
                $("#basicinfo").click();
                setTimeout(function(){ tbFailDate.focus();},100);
                return false;
            }
             if(!validatePhone(tbCallPhone,"固定电话",true,$("#basicinfo"))){
                return false;
            }
            if(!validateMobile(telphone,"手机号码",false,$("#basicinfo"))){
                return false;
            }
            if(!validateEmail(email,"邮箱",false,$("#basicinfo"))){
                return false;
            }
            if(!validatePostCode(tbPostcode,"邮编",true,$("#basicinfo"))){
                return false;
            }
            if (roleInfoData.length == "0") {
                $("#basicinfo").click();
                this.infobox.showPop("必须选择职务信息!");
                setTimeout(function () { $("#roleInfo").click(); }, 100);
                return false;
            }
            var data = JSON.stringify(roleInfoData);
            $("#<%=hidRoleInfo.ClientID %>").val(data);
            return true;
        }

          $(function(){
            buildUnitTable();
            getTableShowOrHide("tbRoleInfo","title1");
            if(<%=btnSave.Visible?"false":"true" %>)
            {
                disableCtrls("#<%=tabContainer1.ClientID %>");
            }
            if('<%=ViewState["strDo"] %>'=="Create"){
                $("#<%=tbUserPwd.ClientID %>").val("");
            }
        });

    </script>
    <style type="text/css">
        .white
        {
            background-color:White;
            }
            .b_c4 td{ text-align:center}
            .b_c5 td{ text-align:center}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <cc1:TabContainer runat="server" ID="tabContainer1">
        <cc1:TabPanel runat="server" ID="basicInfo">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;" id="basicinfo">基本信息</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                <div style="background-color: #f2f1ef;">
                    <table cellpadding="0" cellspacing="0" class="con_tab">
                        <tr>
                            <td class="td_co">
                                姓名：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbUserName" MaxLength="25" runat="server" Width="125px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                性别：
                            </td>
                            <td class="td_lp">
                                <asp:DropDownList ID="tbUserGender" runat="server" Width="125px">
                                    <asp:ListItem Value="1">男</asp:ListItem>
                                    <asp:ListItem Value="-1">女</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:TextBox ID="tbUserGender" MaxLength="30" runat="server"></asp:TextBox>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                工号：
                            </td>
                            <td>
                                <div class=" box_r">
                                </div>
                                <asp:TextBox ID="tbUserCode" MaxLength="15" runat="server" Width="125px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                英文名：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbUserNameEn" MaxLength="50" runat="server" Width="125px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                身份证号：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbUserIdentity" MaxLength="18" runat="server" Width="125px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                生日：
                            </td>
                            <td class="td_lp">
                                <input type="text" runat="server" id="tbUserBirthday" readonly="readonly" onclick="Calendar('MainContent_tabContainer1_basicInfo_tbUserBirthday');"
                                    ondblclick="this.value=''" title="双击清除日期" style="width:125px;"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                密码：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbUserPwd" Width="125px" value="888888" TextMode="Password" MaxLength="15" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td class="td_co">
                                有效日期：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <input type="text" runat="server" id="tbFailDate" readonly="readonly" onclick="Calendar('MainContent_tabContainer1_basicInfo_tbFailDate');"
                                    ondblclick="this.value=''" title="双击清除日期" style="width:125px;"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                固定电话：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbCallPhone" MaxLength="30" runat="server" Width="125px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                手机：
                            </td>
                            <td>
                                <div class=" box_r">
                                </div>
                                <asp:TextBox ID="tbTelPhone" MaxLength="30" runat="server" Width="125px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                邮箱：
                            </td>
                            <td>
                                <div class="box_r">
                                </div>
                                <asp:TextBox ID="tbEmail" MaxLength="50" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                MSN：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbMsn" MaxLength="25" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                QQ：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbQQ" MaxLength="50" runat="server" Width="125px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                Blog：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbBlog" MaxLength="50" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                地址：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbAddress" MaxLength="100" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                邮编：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox ID="tbPostcode" MaxLength="6" runat="server" Width="125px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_co">
                                备注：
                            </td>
                            <td class="td_lp">
                                <asp:TextBox TextMode="MultiLine" Width="431px" Height="80px" ID="tbRemark" MaxLength="180" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" ID="roleInfos">
            <HeaderTemplate>
                <div class="tab_head" id="roleInfo">
                    <span style="font-size: 12px;">职务信息</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                <div>
                    <div class="tit_con">
                        <span>职务操作</span>
                    </div>
                    <div class="ss_tj">
                            <table cellpadding="0" cellspacing="0" class="con_tab">
                                <tr>
                                    <td class="td_co" style="text-align:right">
                                        应用系统：
                                    </td>
                                    <td>
                                        <div class="box_r"></div>
                                        <asp:DropDownList runat="server" ID="dropAppSys" style="height:22px; width:260px" onchange="getRole(this);">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_co white"  style="text-align:right">
                                        角色：
                                    </td>
                                    <td>
                                        <div class="box_r"></div>
                                        <asp:DropDownList runat="server" style="height:22px;width:260px" ID="drpRole">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_co white"  style="text-align:right">
                                        单位：
                                    </td>
                                    <td>
                                    <div class="box_r"></div>
                                        <uc1:DropDownTree runat="server" ID="drpUnit" Width="260px" MultiSelect="false" Url="../ajaxhandler/tree_query.ashx?action=unit" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_co white" style="text-align:right">
                                        缺省标志：
                                    </td>
                                    <td>
                                    <div class="box_r"></div>
                                        <input type="radio" runat="server" style="width:25px; border:0;" id="rd1" name="default" checked="true" /><label for="rd1">是</label><input
                                            type="radio" runat="server" style="width:25px; border:0" id="rd2" name="default" /><label  for="rd2">否</label>
                                    </td>
                                </tr>
                                <tr class="b_bg" style=" text-align:left">
                                <td colspan="2" style="padding-left:360px">                                                <input type="button" <%=ViewState["strDo"].ToString()!="Visible"?"":"disabled=\"disabled\""  %>
                                                id="btnAddPost" class=" input_c" value="添加" onclick="addPost();" />
                                                </td>

                                </tr>
                            </table>
                    </div>
                    <div class="tit_con" id="title1">
                        <span>职务信息列表</span>
                    </div>
                    <div class="ss_bg">
                        <div style="width:auto; height:auto; overflow-y:auto;">
                            <table id="tbRoleInfo" cellpadding="0" cellspacing="1" border="0" class=" ss_jg" style="width:500px;">
                                <tr class="b_c3">
                                    <th scope="col" width="90" align="center">
                                        角色信息
                                    </th>
                                    <th scope="col" width="200" align="center">
                                        单位
                                    </th>
                                    <th width="80" align="center" scope="col">
                                        缺省标识
                                    </th>
                                    <th scope="col" align="center" width="60">
                                        操作
                                    </th>
                                </tr>
                            </table>
                            </div>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="picInfo" runat="server">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;">图片上传</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <div class=" bf" style="text-align: center">
        <asp:Button runat="server" ID="btnSave" Text="保存" OnClientClick="if(!checkData()) return false;"
            CssClass=" input_bc" OnClick="btnSave_Click" />
        <asp:Button runat="server" ID="btnCancle" Text="返回" OnClick="btnCancleClick" CssClass=" input_fh" />
        <input type="hidden" runat="server" id="hidRoleInfo" value="[]"/>
    </div>
</asp:Content>
