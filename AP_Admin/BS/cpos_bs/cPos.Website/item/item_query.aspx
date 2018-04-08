<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="item_query.aspx.cs" Inherits="item_item_query" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="cc1" %>
<%@ Register Src="~/controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<style>
    #MainContent_tvTree{width:120px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;}
</style>
    <script type="text/javascript">
        function go_Create() {
            location.href = "item_show.aspx?item_id=&strDo=Create&from=" + getForm();
        }
        //查看
        function go_View(id) {
            location.href = "item_show.aspx?item_id=" + id + "&strDo=Visible&from=" + getForm();
        }
        //编辑
        function go_Edit(id) {
            location.href = "item_show.aspx?item_id=" + id + "&strDo=Modify&from=" + getForm();
        }
        //属性查看页
        function go_Propery(id) {
            location.href = "item_property.aspx?item_id=" + id + "&from=" + getForm();
        }
        //价格查看页
        function go_Price(id) {
            location.href = "item_price.aspx?item_id=" + id + "&from=" + getForm();
        }
        //Sku查看页
        function go_Sku(id) {
            location.href = "item_sku_show.aspx?item_id=" + id + "&from=" + getForm();
        }
        //图片查看页
        function go_Pic(id) {

        }
        //更改用户状态
        function go_Status(id, status) {
            var msg = status == "1" ? "停用" : "启用";
            var newSatus = status == "1" ? "-1" : "1";
            if (confirm("确认要" + msg + "此商品么？", "询问")) {
                $("#<%=hid_Item_Status.ClientID %>").val(newSatus);
                $("#<%=hid_Item_Id.ClientID %>").val(id);
                $("#<%=btnChangStatus.ClientID %>").trigger("click");
            }
        }
        function getForm() {
            return escape(document.getElementById("_from").value);
        }
    </script>
    <style type="text/css">
        #MainContent_tvTree
        {
            overflow: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table border="0" cellpadding="0" cellspacing="4" width="100%">
        <tr>
            <td style="vertical-align: top; border: 1px solid #ccc; overflow: hidden;">
                <asp:TreeView Style="overflow: hidden;" runat="server" Width="200px" ID="tvTree"
                    SelectedNodeStyle-BackColor="Blue" SelectedNodeStyle-ForeColor="White" OnSelectedNodeChanged="tvTree_SelectedNodeChanged">
                </asp:TreeView>
            </td>
            <td style="vertical-align: top; height: 100%">
                <div class="tit_con">
                    <span>搜索条件</span>
                </div>
                <table border="0" cellspacing="0" cellpadding="0" class="ss_tj" >
                    <tr>
                        <td class="tit_c">
                            商品编码:
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbCode" MaxLength="30" runat="server"></asp:TextBox>
                        </td>
                        <td class="tit_c">
                            商品名称:
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbName" MaxLength="25" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tit_c">
                            商品类别:
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbItemCategory" MaxLength="30" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                        <td class="tit_c">
                            商品状态:
                        </td>
                        <td class="td_lp">
                            <asp:DropDownList ID="selStatus" runat="server">
                                <asp:ListItem Value="" Selected="True" Text="全部"></asp:ListItem>
                                <asp:ListItem Value="1" Text="正常"></asp:ListItem>
                                <asp:ListItem Value="-1" Text="停用"></asp:ListItem>
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
                    <asp:GridView ID="gvItem" runat="server" CellPadding="0" Width="98%" CellSpacing="1"
                        CssClass="ss_jg" AutoGenerateColumns="False" AllowPaging="False" DataKeyNames="Item_Id"
                        GridLines="None" ShowHeaderWhenEmpty="True">
                        <RowStyle CssClass="b_c4" />
                        <Columns>
                            <asp:TemplateField ItemStyle-CssClass="gv-no" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%# SplitPageControl1.PageSize * SplitPageControl1.PageIndex + Container.DataItemIndex + 1%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Item_Category_Name" HeaderText="商品类型" ItemStyle-Width="100px"
                                ControlStyle-CssClass="overflow-text" ItemStyle-HorizontalAlign="center" />
                            <asp:TemplateField ItemStyle-CssClass="gv-code" ItemStyle-HorizontalAlign="Center"
                                HeaderText="商品编码">
                                <ItemTemplate>
                                    <a href="#" onclick="go_View('<%#Eval("Item_Id") %>')">
                                        <%#Eval("Item_Code")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Item_Name" HeaderText="商品名称"  ItemStyle-HorizontalAlign="center" />
                            <asp:BoundField DataField="Item_Name_Short" HeaderText="商品简称"  ItemStyle-Width="120px"
                                ItemStyle-HorizontalAlign="center" />
                            <asp:BoundField DataField="Pyzjm" HeaderText="商品助记码" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="center" />
                            <asp:TemplateField HeaderText="状态" ItemStyle-CssClass="gv-status" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <%#Convert.ToInt32(Eval("Status"))==1?"正常":"停用" %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作" ItemStyle-Width="150px" ItemStyle-CssClass=" gv-5icon"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <a href="#" onclick="go_Edit('<%#Eval("Item_id") %>')">
                                        <img src="../img/modify.png" title="修改" alt="修改" /></a> <a href="#" onclick="go_Status('<%#Eval("Item_Id") %>','<%#Eval("Status") %>')">
                                            <img src="../img/<%#Eval("Status").ToString()=="1"?"disable.png":"enable.png" %>"  title= "<%#Eval("Status").ToString()=="1"?"停用":"启用"%>" /></a>
                                    <a href="#" onclick="go_Propery('<%#Eval("Item_Id") %>')"><img src="../img/property.png" alt="" title="属性" /></a> 
                                    <a href="#" onclick="go_Price('<%#Eval("Item_Id") %>')">
                                        <img src="../img/price.png" alt="" title="价格" /></a> 
                                        <a href="#" onclick="go_Sku('<%#Eval("Item_Id") %>')"><img src="../img/sku.png" alt="" title="SKU" /></a> 
                                        <a href="#"
                                            onclick="go_Pic('<%#Eval("Item_Id") %>')">
                                            <img src="../img/image.png" alt="" title="图片" /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="b_c3" />
                        <AlternatingRowStyle CssClass="b_c5" />
                    </asp:GridView>
                    <cc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass=" pag" PageSize="10"
                        OnRequireUpdate="SplitPageControl1_RequireUpdate" />
                </div>
            </td>
        </tr>
    </table>
    <input type="button" runat="server" id="btnChangStatus" style="display: none" onserverclick="btnChangStatusClick" />
    <input type="hidden" runat="server" id="hid_Item_Id" value="" />
    <input type="hidden" runat="server" id="hid_Item_Status" />
    <input type="hidden" runat="server" id="hid_ItemCategoryID" value="" />
</asp:Content>
