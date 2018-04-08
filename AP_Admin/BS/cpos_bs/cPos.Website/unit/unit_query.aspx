<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="unit_query.aspx.cs" Inherits="unit_unit_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<%@ Register Src="../controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function reconfirm(sender) {
            if (sender == "停用") {
                return confirm("是否要启用？");
            }
            else {
                return confirm("是否要停用？");
            }
        }
        //返回 from=url
        function getFrom() {
            return "from=" + escape(document.getElementById("_from").value); 
        }

        //查看  
        function showpic(unit_id) {
            this.location.href = "unit_show.aspx?strDo=Visible&unit_id=" + unit_id + "&" + getFrom();
        }

        var unit_id = '<%=this.Request.QueryString["unit_id"]%>';

        //新建
        function create() {
            this.location.href = "unit_show.aspx?strDo=Create&unit_id=" + unit_id
             + "&" + getFrom();
        }

        //编辑
        function modify(unit_id) {
            this.location.href = "unit_show.aspx?strDo=Modify&unit_id=" + unit_id
        + "&" + getFrom();
        }

        //停用
        function stop_use(unit_id) {
            if (show_bill_action('unit', 0, 6, 2, menu_id)) {
                this.location.href = document.getElementById("_from").value;
            }
        }

        //启用
        function enable_use(unit_id) {
            if (show_bill_action('unit', 0, 6, 1, unit_id)) {
                this.location.href = document.getElementById("_from").value;
            }
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

        $(function () {
            $("#<%=DropDownTree1.ClientID %>")[0].onselect = function (item) {
                return item.id && item.id.length >= 6;
            }
        });

        function show(status) {
            if (status == "正常") {
                return "true";
            }
            else {
                return "false";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj" >
        <tr>
            <td width="150" align="right" class="tit_c">
                门店编码:
            </td>
            <td class="td_lp">
                <asp:TextBox MaxLength="30"  ID="tbUnitCode" runat="server"></asp:TextBox>
            </td>
            <td width="150" align="right" class="tit_c">
                门店名称:
            </td>
            <td class="td_lp">
                <asp:TextBox MaxLength="25"  ID="tbUnitName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="150" align="right" class="tit_c">
                门店类型:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="ddlUnitType" Height="22px"  runat="server" DataTextField="Type_Name"
                    DataValueField="Type_Id">
                </asp:DropDownList>
            </td>
            <td width="150" align="right" class="tit_c">
                电&nbsp &nbsp 话:
            </td>
            <td class="td_lp">
                <asp:TextBox MaxLength="30" ID="tbUnitTel" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="tit_c" width="150">
                城&nbsp &nbsp 市:
            </td>
            <td class="td_lp">
                <uc2:DropDownTree ID="DropDownTree1" runat="server"  Url="../ajaxhandler/tree_query.ashx?action=city" />
            </td>
            <td align="right" class="tit_c" width="150">
                门店状态:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="selUnitStatus" runat="server"  Height="22px" DataTextField="Description" DataValueField="Status">
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="display:none">
            <td class="td_lp" align="right">
                <asp:Button runat="server" CssClass="input_c" ID="btnHighSearch" Text="高级查询" Width="80px"
                    Enabled="false" />
            </td>
            <td class="tit_c" align="right" colspan="3">
                <%--高级查询：--%>
            </td>
        </tr>
    </table>
    <div class="b_bg">
        <asp:Button ID="btnSearch" CssClass="input_c" Text="查询" runat="server" OnClick="btnSearch_Click" />
         <input type="button"  class="input_c" value="重置"  onclick="location.href='<%=this.Request.Path %>'" />
        <input type="button" value="新建" onclick="create()" class="input_c" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span></div>
    <div class="ss_bg">
        <asp:GridView ID="gvUnit" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
            AutoGenerateColumns="False" OnRowCommand="gvUnit_RowCommand" GridLines="None"
            ShowHeaderWhenEmpty="True">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="gv-checkbox" Visible="false" HeaderText="<input type='checkbox' ID='power' onclick='selectAll(this);' />">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" Visible="false" class="chk_row" onclick="check()" runat="server" customerId='<%#Eval("Id") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="序号" ItemStyle-CssClass="gv-no">
                    <ItemTemplate>
                        <%# SplitPageControl1.PageSize * SplitPageControl1.PageIndex + Container.DataItemIndex + 1%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="门店编码">
                    <ItemTemplate>
                        <a href='unit_show.aspx?strDo=Visible&unit_id=<%#Eval("Id") %>&from=<%= Server.UrlEncode(this.Request.Url.PathAndQuery) %>'>
                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Code") %>' /></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="门店名称" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CityName" HeaderText="城市" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Contact" HeaderText="联系人" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Telephone" ItemStyle-HorizontalAlign="Center" HeaderText="电话"
                    ItemStyle-Width="100px" />
                <asp:BoundField DataField="TypeName" HeaderText="门店类型" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Status_desc" HeaderText="状态" ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ItemStyle-Width="70px" HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a onclick='modify("<%# Eval("Id")%>")' href="javascript:void(0);">
                            <img src="../img/modify.png" alt="" title="修改" /></a>
                        <asp:ImageButton runat="server" OnClientClick="confirm('是否要启用/停用？') return false;"
                            ID="imgControl" CommandName='<%#Eval("Status_desc") %>' CommandArgument='<%#Eval("Id") %>'
                            ImageUrl='<%#getImgUrl(Eval("Status_desc")) %>' ToolTip='<%#getStatusText(Eval("Status_desc")) %>'  />
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
