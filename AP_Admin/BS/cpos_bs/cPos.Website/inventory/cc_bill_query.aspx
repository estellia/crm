<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="cc_bill_query.aspx.cs" Inherits="inventory_cc_bill_query" %>
<%@ Register src="../controls/DropDownTree.ascx" tagname="DropDownTree" tagprefix="uc2" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
    //获取from字段
    function getfrom() {
        return escape($("#_from").val());
    }
    //转到后台修改页面 传递当前ID参数
    function go_Modify(sender) {
        order_id = $(sender).attr("value");
        this.location.href = "cc_bill_show.aspx?strDo=Modify&order_id=" + order_id + "&from=" + getfrom();
    }
    //转到后台查看页面 传递当前ID参数
    function go_Visible(sender) {
        this.location.href = "cc_bill_show.aspx?strDo=Visible&order_id="+sender+"&from="+getfrom();
    }
    //转到添加页面
    function go_Add() {
        this.location.href = "cc_bill_show.aspx?strDo=Create&from="+getfrom();
    }
    $(function () {
        $("#<%=selUnit.ClientID %>")[0].onchanged = function () {
            $("#<%=Button1.ClientID %>").click();
        };
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <td  class="tit_c" >
                盘点单单号：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox ID="tbOrderNo" MaxLength="30" runat="server"></asp:TextBox>
            </td>
            <td class="tit_c"  >
                状态：
            </td>
            <td class="td_lp" colspan="3">
                <asp:DropDownList runat="server" ID="selStatus"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                盘点单位：
            </td>
            <td colspan="3" align="left" class="td_lp" style="text-align:left">
             <uc2:DropDownTree ID="selUnit" runat="server" MultiSelect="false" Url="../ajaxhandler/tree_query.ashx?action=unit" />
            </td>
            <td class="tit_c" >
                仓库：
            </td>
            <td class="td_lp" colspan="3">
                <asp:DropDownList runat="server" ID="selWarehouse"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                单据日期：
            </td>
            <td class="td_lp" align="left">
                <input type="text" runat="server" readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_selOrderDateBegin','MainContent_selOrderDateEnd');"
                    title="双击清除时间" id="selOrderDateBegin" class="selectdate" />
            </td>
            <td align="center">
                至
            </td>
            <td class="td_lp" align="right" >
                <input type="text" runat="server" readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_selOrderDateEnd');" title="双击清除时间" id="selOrderDateEnd" class="selectdate" />
            </td>
            <td class="tit_c" >
                完成日期：
            </td>
            <td class="td_lp" align="left">
                <input type="text" runat="server" readonly="readonly" ondblclick="this.value=''" onclick="Calendar('MainContent_selCompleteDateBegin','MainContent_selCompleteDateEnd');"
                    title="双击清除时间" id="selCompleteDateBegin" class="selectdate" />
            </td>
            <td>
                至
            </td>
            <td class="td_lp" align="right" >
                <input type="text" runat="server"  readonly="readonly" ondblclick="this.value=''" onclick="Calendar('MainContent_selCompleteDateEnd');"
                    title="双击清除时间" id="selCompleteDateEnd" class="selectdate" />
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                数据来源：
            </td>
            <td class="td_lp" colspan="3">
                <asp:DropDownList runat="server" ID="SelectDataFrom"></asp:DropDownList>
            </td>
            <td class="tit_c" >
            </td>
            <td class="td_lp" colspan="3">
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
        <input type="button"  class="input_c" value="重置"  onclick="location.href='<%=this.Request.Path %>'" />
        <input type="button" runat="server" id="btnAdd" value="新建" onclick="go_Add();" class="input_c" />
    </div>
    <div style="display: none">
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg">
        <asp:GridView ID="gvCcBill" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
            AutoGenerateColumns="False" GridLines="None" ShowHeaderWhenEmpty="True" 
            onrowcommand="gvCcBill_RowCommand" onrowdatabound="gvCcBill_RowDataBound">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="gv-no" HeaderText="序号" ItemStyle-Width="30"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <%# SplitPageControl1.PageSize * SplitPageControl1.PageIndex + Container.DataItemIndex + 1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="盘点单单号" ItemStyle-CssClass="gv-4word">
                    <ItemTemplate>
                        <a id="a" onclick='go_Visible("<%#Eval("order_id ") %>")' style="cursor: pointer">
                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("order_no") %>' /></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="unit_name" HeaderText="盘点单位" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="100px" />
                <asp:BoundField DataField="data_from_name" HeaderText="数据来源" ItemStyle-Width="80px"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="warehouse_name" HeaderText="仓库" ItemStyle-CssClass="gv-tel"
                    ItemStyle-Width="80px" />
                <asp:BoundField DataField="order_date" HeaderText="单据日期" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="complete_date" HeaderText="完成日期" ItemStyle-Width="60px"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="total_qty" HeaderText="盘点单总数" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="status_desc" HeaderText="状态" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="操作" ItemStyle-CssClass=" gv-5icon" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="modify" value='<%#Eval("order_id") %>' OnClientClick="go_Modify(this);return false;"><img src="../img/modify.png" title="修改" alt="修改" /></asp:LinkButton>
                        <asp:LinkButton OnClientClick="return confirm('确认要审批该盘点单？','询问');" runat="server"
                            ID="check" CommandArgument='<%#Eval("order_id") %>' CommandName="Check"><img src="../img/approve.png" title="审批" alt="审批" /></asp:LinkButton>
                        <asp:LinkButton OnClientClick="return confirm('确认要回退该盘点单？','询问');" runat="server"
                            ID="back" CommandArgument='<%#Eval("order_id") %>' CommandName="Back"><img src="../img/reject.png" title="回退" alt="回退" /></asp:LinkButton>
                        <asp:LinkButton OnClientClick="return confirm('确实要删除该盘点单？','询问');" runat="server"
                            ID="delete" CommandArgument='<%#Eval("order_id") %>' CommandName="cDelete"><img src="../img/delete.png" title="删除" alt="删除" /></asp:LinkButton>
                        <asp:LinkButton ID="createAj" OnClientClick="return confirm('确实要生成调整单？','询问');" runat="server" CommandArgument='<%#Eval("order_id") %>' CommandName="cCreateAj"><img src="../img/create.png" alt="生成调整单" title="生成调整单"/></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pag" />
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
    <uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pag" PageSize="10"
        OnRequireUpdate="SplitPageControl1_RequireUpdate" />
    </div>
</asp:Content>
