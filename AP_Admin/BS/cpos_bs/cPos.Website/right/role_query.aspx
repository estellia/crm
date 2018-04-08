<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="role_query.aspx.cs" Inherits="right_role_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        //返回 from=url
        function getFrom() {
            return "from=" + escape($("#_from").val());
        }

        //修改
        function go_Modify(sender) {
            this.location.href = "role_show.aspx?strDo=Modify&Role_Id=" + sender + "&" + getFrom();
        }
        //查看
        function go_Visible(sender) {
            this.location.href = "role_show.aspx?strDo=Visible&Role_Id=" + sender + "&" + getFrom();
        }
        //新建
        function go_Create() {
            this.location.href = "role_show.aspx?Role_Id=&strDo=Create&appId=" + $("#<%=tbAppSys.ClientID%>").val() + "&" + getFrom();
        }
        // document ready
        $(function () {
            //全选事件
            $("#chk_all").click(
            function () {
                if ($(this).attr("checked")) {
                    $(".chk_row").attr("checked", "checked");
                }
                else {
                    $(".chk_row").removeAttr("checked");
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <td class="tit_c">
                应用系统:
            </td>
            <td class="td_lp">
                <asp:DropDownList runat="server" ID="tbAppSys" ViewStateMode="Enabled">
                </asp:DropDownList>
            </td>
            <td class="tit_c">
                &nbsp;
            </td>
            <td class="td_lp">
                &nbsp;
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
        <input type="button"  class="input_c" value="重置"  onclick="location.href='<%=this.Request.Path %>'" />
        <input type="button" runat="server" id="btnCreate" value="新建" onclick="go_Create();"
            class="input_c" />
    </div>
    <!-- gridview-->
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg">
        <asp:GridView ID="gv_role" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg" 
            AuoGenerateColumns="False" GridLines="None" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False"
            OnRowCommand="gv_role_RowCommand">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="gv-checkbox" Visible="false" HeaderText="<input type='checkbox' style='margin-top:7px; display:none' id='chk_all'/>">
                    <ItemTemplate>
                        <input type="checkbox" style="margin-top:7px; display:none" class="chk_row" value='<%#Eval("Role_Id") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="序号" ItemStyle-CssClass="gv-no">
                    <ItemTemplate>
                        <%# SplitPageControl1.PageSize * SplitPageControl1.PageIndex + Container.DataItemIndex + 1%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="角色编码" ItemStyle-CssClass="gv-code">
                    <ItemTemplate>
                        <a onclick='go_Visible("<%#Eval("Role_Id") %>")' style="cursor: pointer">
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Role_Code") %>' /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Role_Name" HeaderText="角色名称"   ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Role_Eng_Name" HeaderText="英文名" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Default_Flag_Desc" HeaderText="系统保留" ItemStyle-Width="60"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Create_User_Name" HeaderText="添加人" ItemStyle-CssClass="gv-person" />
                <asp:BoundField DataField="Create_Time" HeaderText="添加时间" ItemStyle-CssClass="gv-datetime" 
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Modify_User_Name" HeaderText="修改人" ItemStyle-CssClass="gv-person"  />
               <%-- <asp:TemplateField HeaderText="修改时间" ItemStyle-CssClass="gv-datetime">
                </asp:TemplateField>--%>
                <asp:BoundField DataField="Modify_Time" HeaderText="修改时间" ItemStyle-CssClass="gv-datetime"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="gv-2icon">
                    <ItemTemplate>
                        <a onclick="go_Modify('<%#Eval("Role_Id") %>')" href="javascript:void(0)">
                            <img src="../img/modify.png" title="修改" alt="修改" />
                        </a>
                        <asp:ImageButton ID="imgDel" ImageUrl="~/img/delete.png" title="删除" runat="server"
                            CommandArgument='<%#Eval("Role_Id") %>' OnClientClick="if(!confirm('确认要删除该条数据吗？')) return false;"
                            CommandName="Operate-Delete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pag" />
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        <uc1:splitpagecontrol id="SplitPageControl1" runat="server" cssclass="pag" pagesize="10"
            onrequireupdate="SplitPageControl1_RequireUpdate" />
    </div>
</asp:Content>
