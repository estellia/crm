<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="category_query.aspx.cs" Inherits="item_category_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function go_Confirm(sender) {
            var status = $(sender).attr("desc");
            var value = status == "-1" ? "启用" : "停用";
           return confirm("是否要" + value + "?") ;
        }
        function getfrom() {
            return "&from=" + escape($("#_from").val());
        }
//        function () {
//            this.$("#<%=tbCode.ClientID %>").focus();
//        }
        function go_Create() {
            this.location.href = "category_show.aspx?strDo=Create" + getfrom();
        }
        function go_Modify(category_id) {
            this.location.href = "category_show.aspx?strDo=Modify&item_category_id=" + category_id + getfrom();
        }
        function go_Visible(category_id) {
            this.location.href = "category_show.aspx?strDo=Visible&item_category_id=" + category_id + getfrom();
        }
        function ValidateInput() {
            if ($("#<%=tbStatus.ClientID %>").val() == "") {
                alert("至少输入一个查询条件");
                $("#<%=tbStatus.ClientID %>").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj" >
        <tr>
            <td  class="tit_c">
                商品类型编码:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCode" MaxLength="30" runat="server"></asp:TextBox>
            </td>
            <td  class="tit_c">
                商品类型名称:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbName" MaxLength="25" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
                拼音助记码:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbPYZJM" MaxLength="25" runat="server"></asp:TextBox>
            </td>
            <td  class="tit_c">
                商品类型状态:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="tbStatus" runat="server">
                    <asp:ListItem Value="">全部</asp:ListItem>
                    <asp:ListItem Value="1">正常</asp:ListItem>
                    <asp:ListItem Value="-1">停用</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
                <input type="button"  class="input_c" value="重置"  onclick="location.href='<%=this.Request.Path %>'" />
        <input type="button" runat="server" id="btnCreate" value="新建" onclick="go_Create();"
            class="input_c" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg">
        <asp:GridView ID="gvCategory" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
            AutoGenerateColumns="False" OnRowCommand="gvCategory_RowCommand" GridLines="None"
            ShowHeaderWhenEmpty="True" OnRowDataBound="gvCategory_RowDataBound">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField  HeaderText="序号" ItemStyle-CssClass="gv-no">
                    <ItemTemplate>
                        <%#SplitPageControl1.PageSize * SplitPageControl1.PageIndex + Container.DataItemIndex + 1%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品类型编码"  ItemStyle-CssClass="gv-5icon" >
                    <ItemTemplate> 
                        <a id="Visible" onclick="go_Visible('<%#Eval("Item_Category_Id") %>')" style="cursor: pointer">
                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Item_Category_Code") %>' /></a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="Item_Category_Name" HeaderText="商品类型名称" 
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Pyzjm" HeaderText="拼音助记码" ItemStyle-HorizontalAlign="Center"  ItemStyle-CssClass="gv-5icon"/>
                <asp:BoundField DataField="Parent_Name" HeaderText="上级商品名称" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Status_desc" HeaderText="状态" ItemStyle-CssClass="gv-3icon" />
                <asp:TemplateField  HeaderText="操作"  ItemStyle-CssClass="gv-5icon">
                    <ItemTemplate>
                        <a href="javascript:" onclick="go_Modify('<%#Eval("Item_Category_Id") %>')"> <img src="../img/edit.png" alt="" title="修改" /></a>
                        <asp:ImageButton runat="server" ID="lbControl" CommandName='<%#Eval("Status_desc") %>' desc='<%#Eval("Status") %>' CommandArgument='<%#Eval("Item_Category_Id") %>' OnClientClick="if(!go_Confirm(this)) return false;"  ToolTip='<%#Eval("Status").ToString()=="-1"?"启用" : "停用"%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pag" />
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        <uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pagebar" PageSize="10"
            OnRequireUpdate="SplitPageControl1_RequireUpdate" />
    </div>
</asp:Content>
