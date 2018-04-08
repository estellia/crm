<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="terminal_query.aspx.cs" Inherits="terminal_terminal_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        //新建
        function go_Create() {
            this.location.href = "terminal_show.aspx?oper_type=2&from=" + getForm();
        }
        //查看
        function go_View(pos_id) {
            this.location.href = "terminal_show.aspx?pos_id=" + pos_id + "&oper_type=1&from=" + getForm();
        }
        //查看门店
        function go_show(pos_id) {
            this.location.href = "terminal_unit_query.aspx?pos_id=" + pos_id + "&from=" + getForm();
        }
        //编辑
        function go_Edit(pos_id) {
            this.location.href = "terminal_show.aspx?pos_id=" + pos_id + "&oper_type=3&from=" + getForm();
        }
        function getForm() {
            return escape(document.getElementById("_from").value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj" >
        <tr>
            <td class="tit_c"  >
                终端持有方式：
            </td>
            <td class="td_lp" colspan="3">
                <asp:DropDownList  runat="server" ID="cbHoldType">
                    <asp:ListItem Value="0">全部</asp:ListItem>
                    <asp:ListItem Value="2">自有</asp:ListItem>
                    <asp:ListItem Value="1">租赁</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tit_c"  >
                终端类型：
            </td>
            <td class="td_lp" colspan="3">
                <asp:DropDownList  runat="server" ID="cbType">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tit_c"  >
                终端编码：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbCode" MaxLength="30"></asp:TextBox>
            </td>
            <td class="tit_c"  >
                终端序列号：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbSn" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tit_c"  >
                终端购买日期：
            </td>
            <td class="td_lp" align="left">
                <input type="text" runat="server"  readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_tbPurchaseDateBegin','MainContent_tbPurchaseDateEnd');"
                    title="双击清除时间" id="tbPurchaseDateBegin" class="selectdate"/>
            </td>
            <td align="center">
                至
            </td>
            <td class="td_lp" align="right">
                <input type="text" runat="server"  readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_tbPurchaseDateEnd');" title="双击清除时间" id="tbPurchaseDateEnd" class="selectdate"/>
            </td>
            <td class="tit_c"  >
                终端出保日期：
            </td>
            <td class="td_lp" align="left">
                <input type="text" runat="server"  readonly="readonly" ondblclick="this.value=''"  onclick="Calendar('MainContent_tbInsuraceDateBegin','MainContent_tbInsuraceDateEnd');"
                    title="双击清除时间" id="tbInsuraceDateBegin" class="selectdate"/>
            </td>
            <td align='center'>
                至
            </td>
            <td class="td_lp" align="right">
                <input type="text" runat="server"  readonly="readonly" ondblclick="this.value=''" onclick="Calendar('MainContent_tbInsuraceDateEnd');"
                    title="双击清除时间" id="tbInsuraceDateEnd" class="selectdate"/>
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
        <input type="button" value="重置" id="btnReset" class="input_c" onclick="location.href= '<%=this.Request.Path %>'" />
        <input type="button" value="新建" runat="server" id="btnCreate" class="input_c" onclick="go_Create();" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span></div>
    <div class="ss_bg" style="padding-right: 14px;">
        <asp:GridView runat="server" AllowPaging="false" DataKeyNames="ID" ID="gvTerminal"
            CellPadding="0" CellSpacing="1" CssClass="ss_jg" AutoGenerateColumns="False"
            GridLines="None" ShowHeaderWhenEmpty="True">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# SplitPageControl1.PageSize * SplitPageControl1.PageIndex + Container.DataItemIndex + 1%></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="HoldTypeDescription" ItemStyle-Width="100px" HeaderText="持有方式"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TypeDescription" ItemStyle-Width="100px" HeaderText="类型"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Code" ItemStyle-Width="150px" HeaderText="编码" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="序列号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="javascript:void(0);" onclick="go_View('<%#Eval("ID") %>');">
                            <%#Eval("SN") %></a></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PurchaseDate" ItemStyle-CssClass="gv-date" HeaderText="购买日期" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="InsuranceDate" ItemStyle-CssClass="gv-date" HeaderText="出保日期" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:BoundField DataField="SoftwareVersion" ItemStyle-CssClass="gv-5icon" HeaderText="程序版本" />
                <asp:BoundField DataField="DBVersion" HeaderText="数据库版本" ItemStyle-CssClass="gv-4icon" />
                <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="gv-3icon">
                    <ItemTemplate>
                        <a onclick="go_Edit('<%#Eval("ID") %>');" href="javascript:void(0);">
                            <img alt="修改" title="修改" id="imgedit" src="../img/edit.png" /></a> <a onclick="go_show('<%#Eval("ID") %>');"
                                href="javascript:void(0);">
                                <img alt="门店" title="门店" id="imgshop" src="../img/shop.png" /></a></ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        <uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pag" PageSize="10"
            OnRequireUpdate="SplitPageControl1_RequireUpdate" />
    </div>
</asp:Content>
