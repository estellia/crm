<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="item_price_adjust_query.aspx.cs" Inherits="item_item_price_adjust_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        //查看
        function go_View(user_id) {
            location.href = "item_price_adjust_show.aspx?order_id=" + user_id + "&strDo=Visible&from=" + getFrom();
        }
        //新建
        function go_Create() {
            location.href = "item_price_adjust_show.aspx?order_id=&strDo=Create&from=" + getFrom();
        }
        //修改
        function go_Edit(sender) {
            var user_id = $(sender).attr("order_id");
            location.href = "item_price_adjust_show.aspx?order_id=" + user_id + "&strDo=Modify&from=" + getFrom();
        }
        function getFrom() {
            return escape(document.getElementById("_from").value);
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
                调价单单号:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbOrderNo" MaxLength="30" runat="server"></asp:TextBox>
            </td>
            <td  class="tit_c">
                单据日期:
            </td>
            <td class="td_lp">
                <input type="text" runat="server" id="selOrderDate" readonly="readonly" onclick="Calendar('MainContent_selOrderDate');"
                    ondblclick="this.value=''" title="双击清除日期" />
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
                调价开始日期:
            </td>
            <td class="td_lp">
                <input type="text" runat="server" id="selBeginDate" readonly="readonly" onclick="Calendar('MainContent_selBeginDate','MainContent_selEndDate');"
                    ondblclick="this.value=''" title="双击清除日期" />
            </td>
            <td  class="tit_c">
                调价结束日期:
            </td>
            <td class="td_lp">
                <input type="text" runat="server" id="selEndDate" readonly="readonly" onclick="Calendar('MainContent_selEndDate');"
                    ondblclick="this.value=''" title="双击清除日期" />
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
                价格类型:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="selItemPriceType" runat="server">
                </asp:DropDownList>
            </td>
            <td  class="tit_c">
                状态:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="selStatus" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div class="b_bg">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
        <input type="button" class="input_c" value="重置" onclick="location.href='<%=this.Request.Path %>'" />
        <input type="button" runat="server" id="btnCreate" value="新建" onclick="go_Create();"
            class="input_c" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg">
        <asp:GridView ID="gvPriceAdjust" runat="server" CellPadding="0" Width="98%" CellSpacing="1"
            CssClass="ss_jg" AutoGenerateColumns="False" AllowPaging="False" DataKeyNames="order_id"
            GridLines="None" ShowHeaderWhenEmpty="True" OnRowCommand="gvPriceAdjust_RowCommand"
            OnRowDataBound="gvPriceAdjust_RowDataBound">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="gv-no" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#this.SplitPageControl1.PageSize*this.SplitPageControl1.PageIndex+Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="调价单单号">
                    <ItemTemplate>
                        <a href="#" onclick="go_View('<%#Eval("order_id") %>');"><%#Eval("order_no")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-CssClass="gv-date" ItemStyle-Width="120px" DataField="order_date" HeaderText="单据日期"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-CssClass="gv-date" ItemStyle-Width="120px" DataField="begin_date" HeaderText="调价开始日期"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-CssClass="gv-date" ItemStyle-Width="100px" DataField="end_date" HeaderText="调价结束日期"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="110px" DataField="item_price_type_name" HeaderText="价格类型"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-CssClass=" gv-status" ItemStyle-Width="100px" DataField="status_desc"  HeaderText="状态"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-CssClass="gv-person" ItemStyle-Width="110px" DataField="create_user_name" HeaderText="创建人"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="创建时间" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("create_time")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="gv-4word" ItemStyle-Width="180px"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                         <asp:HyperLink runat="server" ID="modify" NavigateUrl="#" order_id='<%#Eval("order_id") %>' onclick='go_Edit(this);return false;'><img src="../img/modify.png" title="修改" alt="修改" /></asp:HyperLink>
                        <asp:LinkButton OnClientClick="return confirm('确实要删除该调价单？','询问');" runat="server"
                            ID="delete" CommandArgument='<%#Eval("order_id") %>' CommandName="Del"><img src="../img/delete.png" title="删除" alt="删除" /></asp:LinkButton>
                        <asp:LinkButton OnClientClick="return confirm('确认要审批该调价单？','询问');" runat="server"
                            ID="check" CommandArgument='<%#Eval("order_id") %>' CommandName="Check"><img src="../img/approve.png" title="审批" alt="审批" /></asp:LinkButton>
                        <asp:LinkButton OnClientClick="return confirm('确认要回退该调价单？','询问');" runat="server"
                            ID="back" CommandArgument='<%#Eval("order_id") %>' CommandName="Back"><img src="../img/reject.png" title="回退" alt="回退" /></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        <cc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pag" PageSize="10"
            OnRequireUpdate="SplitPageControl1_RequireUpdate" />
    </div>
</asp:Content>
