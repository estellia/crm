<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="inout_bill_in_query.aspx.cs" Inherits="inventory_inout_bill_in_query" %>
<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<%@ Register Src="~/controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        //获取from字段
        function getfrom() {
            return escape($("#_from").val());
        }
        //转到后台修改页面 传递当前ID参数
        function go_Modify(sender) {
            var order_id = $(sender).attr("value");
            this.location.href = "inout_bill_in_show.aspx?strDo=Modify&order_id=" + order_id + "&from=" + getfrom();
        }
        //转到后台查看页面 传递当前ID参数
        function go_Visible(sender) {
            this.location.href = "inout_bill_in_show.aspx?strDo=Visible&order_id=" + sender + "&from=" + getfrom();
        }
        //转到添加页面
        function go_Add() {
            this.location.href = "inout_bill_in_show.aspx?strDo=Create&from=" + getfrom();
        }
        //重置搜索条件
        function ResetAll() {
            $("#<%=tbOrderNo.ClientID %>").val("");
            $("#<%=selOrderType.ClientID %>").val("");
            $("#<%=selStatus.ClientID%>").val("");
            $("#<%=selSalesUnit.ClientID%>")[0].text("");
            $("#<%=selSalesUnit.ClientID%>")[0].values([]);
            $("#<%=selSalesUnit.ClientID%>")[0].texts([]);
            $("#<%=selWarehouse.ClientID%>").val("");
            $("#<%=selPurchaseUnit.ClientID%>")[0].text("");
            $("#<%=selPurchaseUnit.ClientID%>")[0].values([]);
            $("#<%=selPurchaseUnit.ClientID%>")[0].texts([]);
            $("#<%=selOrderDateBegin.ClientID%>").val("");
            $("#<%=selOrderDateEnd.ClientID%>").val("");
            $("#<%=selCompleteDateBegin.ClientID %>").val("");
            $("#<%=selCompleteDateEnd.ClientID %>").val("");
            $("#<%=selDataFrom.ClientID%>").val("");
            $("#<%=tbRefOrderNo.ClientID%>").val("");
        }

        $(function () {
            $("#<%=selSalesUnit.ClientID %>")[0].onchanged = function () {
                $("#<%=Button1.ClientID %>").click();
            };
            $("#<%=selPurchaseUnit.ClientID %>")[0].onchanged = function () {
                $("#<%=Button1.ClientID %>").click();
            };
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj" >
        <tr>
            <td  class="tit_c" >
                入库单单号：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  ID="tbOrderNo" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td class="tit_c"  >
                入库单类型：
            </td>
            <td class="td_lp" colspan="3">
                <asp:DropDownList  runat="server" ID="selOrderType">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                销售单位：
            </td>
            <td colspan="3" align="left" class="td_lp" style="text-align: left">
                <uc2:dropdowntree id="selSalesUnit" runat="server" multiselect="false" Url="../ajaxhandler/tree_query.ashx?action=unit"/>
            </td>
            <td class="tit_c" >
                仓库：
            </td>
            <td class="td_lp" colspan="3">
                <asp:DropDownList  runat="server" ID="selWarehouse">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                采购单位：
            </td>
            <td colspan="3" align="left" class="td_lp" style="text-align: left">
                <uc2:dropdowntree id="selPurchaseUnit" runat="server" multiselect="false" Url="../ajaxhandler/tree_query.ashx?action=unit"/>
            </td>
            <td class="tit_c" >
                入库单状态：
            </td>
            <td class="td_lp" colspan="3">
                <asp:DropDownList  runat="server" ID="selStatus">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                单据日期：
            </td>
            <td class="td_lp" align="left">
                <input type="text" runat="server" class="selectdate"  readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_selOrderDateBegin','MainContent_selOrderDateEnd');"
                    title="双击清除时间" id="selOrderDateBegin" />
            </td>
            <td align="center">
                至
            </td>
            <td class="td_lp" align="right">
                <input type="text" runat="server" class="selectdate" readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_selOrderDateEnd');" title="双击清除时间" id="selOrderDateEnd" />
            </td>
            <td class="tit_c" >
                入库日期：
            </td>
            <td class="td_lp" align="left">
                <input type="text" runat="server" class="selectdate" readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_selCompleteDateBegin','MainContent_selCompleteDateEnd');"
                    title="双击清除时间" id="selCompleteDateBegin" />
            </td>
            <td align="center">
                至
            </td>
            <td class="td_lp" align="right">
                <input type="text" runat="server" class="selectdate" readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_selCompleteDateEnd');" title="双击清除时间" id="selCompleteDateEnd" />
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                数据来源：
            </td>
            <td class="td_lp" colspan="3">
                <asp:DropDownList  runat="server" ID="selDataFrom">
                </asp:DropDownList>
            </td>
            <td class="tit_c" >
                来源单据号:
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  ID="tbRefOrderNo" MaxLength="30" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
        <input type="button" runat="server" id="btnReset" value="重置" onserverclick="btnResetClick"
            class="input_c" />
        <input type="button" runat="server" id="btnAdd" value="新建" onclick="go_Add();" class="input_c" />
    </div>
    <div style="display: none">
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg">
        <asp:GridView ID="gvInoutBill" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
            AutoGenerateColumns="False" GridLines="None" ShowHeaderWhenEmpty="True" OnRowCommand="gvInoutBill_RowCommand"
            OnRowDataBound="gvInoutBill_RowDataBound">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="gv-no" HeaderText="序号" ItemStyle-Width="30"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#SplitPageControl1.PageIndex*SplitPageControl1.PageSize +Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="入库单单号" ItemStyle-CssClass="gv-4word">
                    <ItemTemplate>
                        <a id="a" onclick='go_Visible("<%#Eval("order_id ") %>")' style="cursor: pointer">
                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("order_no") %>' /></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="order_reason_name" HeaderText="入库单类型" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="100px" />
                <asp:BoundField DataField="data_from_name" HeaderText="数据来源" ItemStyle-Width="80px"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="sales_unit_name" HeaderText="销售单位" ItemStyle-CssClass="gv-tel"
                    ItemStyle-Width="80px" />
                <asp:BoundField DataField="purchase_unit_name" HeaderText="采购单位" ItemStyle-CssClass="gv-tel"
                    ItemStyle-Width="80px" />
                <asp:BoundField DataField="warehouse_name" HeaderText="仓库" ItemStyle-CssClass="gv-tel"
                    ItemStyle-Width="80px" />
                <asp:BoundField DataField="order_date" HeaderText="单据日期" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="complete_date" HeaderText="入库日期" ItemStyle-Width="60px"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="status_desc" HeaderText="状态" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="gv-2icon" ItemStyle-Width="80px">
                    <ItemTemplate>
                    <a runat="server" id="modify" value='<%#Eval("order_id") %>' onclick='go_Modify(this);return false;' style="cursor: pointer" >
                            <img src="../img/modify.png" title="修改" alt="修改" /></a>
                        <asp:LinkButton OnClientClick="return confirm('确认要审批该入库单？','询问');" runat="server"
                            ID="check" CommandArgument='<%#Eval("order_id") %>' CommandName="Check"><img src="../img/approve.png" title="审批" alt="审批" /></asp:LinkButton>
                        <asp:LinkButton OnClientClick="return confirm('确认要回退该入库单？','询问');" runat="server"
                            ID="back" CommandArgument='<%#Eval("order_id") %>' CommandName="Back"><img src="../img/reject.png" title="回退" alt="回退" /></asp:LinkButton>
                            <asp:LinkButton OnClientClick="return confirm('确实要删除该入库单？','询问');" runat="server"
                            ID="delete" CommandArgument='<%#Eval("order_id") %>' CommandName="inDelete"><img src="../img/delete.png" title="删除" alt="删除" /></asp:LinkButton>
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
