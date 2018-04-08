<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="announce_query.aspx.cs" Inherits="exchange_announce_query" %>

<%@ Register Src="~/controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        //返回 from=url
        function getFrom() {
            return escape($("#_from").val());
        }

        //修改
        function go_Modify(sender) {
            this.location.href = "announce_show.aspx?strDo=3&announce_id=" + sender + "&from=" + getFrom();
        }
        //查看
        function go_Visible(sender) {
            this.location.href = "announce_show.aspx?strDo=1&announce_id=" + sender + "&from=" + getFrom();
        }
        //新建
        function go_Create() {
            this.location.href = "announce_show.aspx?strDo=2" + "&from=" + getFrom();
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
            $(".pop").css("width", $("#MainContent_tabContainerAnnounce_tabBasic_tvUnit").css("width"));
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj" >
        <tr>
            <td class="tit_c" >
                主题：
            </td>
            <td class="td_lp" colspan="3" >
                <asp:TextBox  runat="server" ID="tbTitle" MaxLength="25"></asp:TextBox>
            </td>
            <td class="tit_c" >
                通告期限：
            </td>
            <td class="td_lp" align="left">
                <input type="text" runat="server"  readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_tbBeginDate','MainContent_tbInsuraceDateEnd');"
                    title="双击清除时间" id="tbBeginDate" class="selectdate" />
            </td>
            <td align="center">
                至
            </td>
            <td class="td_lp" align="right">
                <input type="text" runat="server"  readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_tbEndDate');" title="双击清除时间" id="tbEndDate" class="selectdate" />
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                通告单位：
            </td>
            <td class="td_lp" colspan="3">
                <uc1:DropDownTree ID="tvUnit" runat="server" MultiSelect="false" Url="../ajaxhandler/tree_query.ashx?action=unit" />
            </td>
            <td class="tit_c">
                允许下发：
            </td>
            <td class="td_lp" colspan="3" >
                <asp:DropDownList runat="server" ID="cbAllowDownload">
                    <asp:ListItem>全部</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                    <asp:ListItem Value="1">是</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
        <input type="button" class="input_c" value="重置" onclick="location.href='<%=this.Request.Path %>'" />
        <input type="button" runat="server" id="btnCreate" value="新建" onclick="go_Create();"
            class="input_c" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg" style="padding-right: 14px;">
        <asp:GridView runat="server" AllowPaging="true" DataKeyNames="ID" ID="gvAnnounce"
            CellPadding="0" CellSpacing="1" CssClass="ss_jg" AutoGenerateColumns="False"
            GridLines="None" ShowHeaderWhenEmpty="True" OnRowCommand="gvAnnounce_RowCommand">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# SplitPageControl1.PageSize * SplitPageControl1.PageIndex + Container.DataItemIndex + 1%></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="No" ItemStyle-Width="150px" HeaderText="编号" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="主题" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="javascript:void(0);" onclick="go_Visible('<%#Eval("ID") %>');">
                            <%#Eval("Title")%></a></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="通知单位" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Unit.DisplayName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Publisher" ItemStyle-Width="100px" HeaderText="发布人" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="AllowDownloadDescription" ItemStyle-Width="100px" HeaderText="允许下发"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="操作" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a id="imgModify" onclick='go_Modify("<%#Eval("ID") %>")' style="cursor: pointer">
                            <img src="../img/modify.png" title="修改" alt="修改" /></a>
                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/img/delete.png" title="删除" runat="server"
                            CommandArgument='<%#Eval("ID") %>' OnClientClick="if(!confirm('确认要删除该条数据吗？')) return false;"
                            CommandName="Operate-Delete" />
                        <asp:ImageButton runat="server" ID="imgPublish" CommandName="Publish" CommandArgument='<%#Eval("ID") %>'
                            ImageUrl="../img/publish.png" title="下发" alt="下发" />
                        <asp:ImageButton runat="server" ID="imgStopPublish" CommandName="StopPublish" CommandArgument='<%#Eval("ID") %>'
                            ImageUrl="../img/stop_publish.png" title="停止下发" alt="停止下发" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pag" />
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        <uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pagebar" PageSize="1"
            OnRequireUpdate="SplitPageControl1_RequireUpdate" />
    </div>
</asp:Content>
