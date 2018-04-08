<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="user_query.aspx.cs" Inherits="right_user_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="cc1" %>
<%@ Register Src="../Controls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .chk_row input{ margin-top:7px;}
    </style>
    <script type="text/javascript">
        //查看
        function go_View(user_id) {
            location.href = "user_show.aspx?user_id=" + user_id + "&strDo=Visible&from=" + getFrom();
        }
        //新建
        function go_Create() {
            location.href = "user_show.aspx?user_id=&strDo=Create&from=" + getFrom();
        }
        //修改
        function go_Edit(user_id) {
            location.href = "user_show.aspx?user_id=" + user_id + "&strDo=Modify&from=" + getFrom();
        }
        //修改密码
        function go_ModifyPwd(user_id, user_name) {
            location.href = "user_reset_pwd.aspx?user_name=" + user_name + "&user_id=" + user_id + "&from=" + getFrom();
        }
        //上传图片
        function go_Pic(user_id) {
            location.href = "user_upload_picture.aspx?user_id=" + user_id + "&from=" + getFrom();
        }
        //更改用户状态
        function go_Status(id, status) {
            var msg = status == "1" ? "停用" : "启用";
            var newSatus = status == "1" ? "-1" : "1";
            if (confirm("确认要" + msg + "该用户么？", "询问")) {
                $("#<%=hid_User_Status.ClientID %>").val(newSatus);
                $("#<%=hid_User_Id.ClientID %>").val(id);
                $("#<%=btnChangStatus.ClientID %>").trigger("click");
            }
        }
        function getFrom() {
            return escape(document.getElementById("_from").value);
        }
        function selectAll(sender) {
            if ($(sender).attr("checked")) {
                $("input:checkbox").each(function () { $(this).attr("checked", "checked"); });
            }
            else {
                $("input:checkbox").each(function () { $(this).attr("checked", ""); });
            }
        }
        function check() {
            var boxs = $(".select").find("input:checkbox").length;
            var checkedboxs = $(".select").find("input:checked").length;
            if (boxs == checkedboxs) {
                $("#power").attr("checked", "checked");
            }
            else {
                $("#power").removeAttr("checked");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span>
    </div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj" >
        <tr>
            <td  class="tit_c">
                工号:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbUserCode" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td  class="tit_c">
                姓名:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbUserName" runat="server" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
                手机:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCellPhone" runat="server"></asp:TextBox>
            </td>
            <td class="tit_c">
                状态:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="cbUserStatus" runat="server">               
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div class="b_bg">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
        <input type="button"  class="input_c" value="重置"  onclick="location.href='<%=this.Request.Path %>'" />
        <input type="button" runat="server" id="btnCreate" value="新建" onclick="go_Create();"
            class="input_c" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg">
        <asp:GridView ID="gvUser" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
            AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="User_Id" GridLines="None"
            ShowHeaderWhenEmpty="True">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="gv-checkbox" Visible="false" HeaderText="<input type='checkbox' style='margin-top:7px' ID='power' onclick='selectAll(this);'/>">
                    <ItemTemplate>
                        <asp:CheckBox Visible="false" ID="chkSelect" CssClass="chk_row" onclick="check()" runat="server" customerId='<%#Eval("User_Id") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="序号" ItemStyle-CssClass="gv-no">
                    <ItemTemplate>
                        <%#SplitPageControl1.PageSize * SplitPageControl1.PageIndex + Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="工号" ItemStyle-Width="200px">
                    <ItemTemplate>
                        <a href="#" onclick="go_View('<%#Eval("User_Id") %>')">
                            <%#Eval("User_Code") %></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="User_Name" HeaderText="姓名" ItemStyle-CssClass="gv-person" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="User_Gender" HeaderText="性别" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="User_CellPhone" ItemStyle-HorizontalAlign="Center" HeaderText="手机"
                    ItemStyle-CssClass="gv-tel"/>
                <asp:BoundField DataField="QQ" HeaderText="QQ" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="User_Status_Desc" HeaderText="状态" ItemStyle-CssClass="gv-status"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ItemStyle-CssClass="gv-4word" ItemStyle-Width="150px" HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="#" onclick="go_Edit('<%#Eval("User_Id") %>')"> <img src="../img/modify.png" alt="" title="修改" /></a>
                         <a href="#" onclick="go_ModifyPwd('<%#Eval("User_Id") %>','<%#Eval("User_Name") %>')">
                            <img src="../img/reset_pwd.png" alt="" title="密码重置" /></a> 
                            <a href="#" onclick="go_Status('<%#Eval("User_Id") %>','<%#Eval("User_Status") %>');">
                                <img src="../img/<%#Eval("User_Status").ToString()=="1"?"disable.png":"enable.png" %>" title= "<%#Eval("User_Status").ToString()=="1"?"停用":"启用"%>"></a> 
                                <a href="#" onclick="go_Pic('<%#Eval("User_Id") %>')">
                                    <img src="../img/image.png" alt="" title="图片" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        <input type="button" runat="server" id="btnChangStatus" style="display: none" onserverclick="btnChangStatusClick" />
        <input type="hidden" runat="server" id="hid_User_Id" value="" />
        <input type="hidden" runat="server" id="hid_User_Status" />
        <div class="fr">
            <cc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pagebar" PageSize="10"
                OnRequireUpdate="SplitPageControl1_RequireUpdate" />
        </div>
    </div>
</asp:Content>
