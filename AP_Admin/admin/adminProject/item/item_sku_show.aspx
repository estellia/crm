<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="item_item_sku_show" Codebehind="item_sku_show.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<style type="text/css">
     .tdHeader
    {
        background:#ccc;
        border-left:1px solid Black;
        border-right:1px solid Black;
        text-align:center;
    }
   .hidden
        {
            display:none;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<div class=" ss_bg">
                    <table border="0" cellpadding="0" cellspacing="1" class="ss_jg">
                        <tr class="tit_con">
                            <td colspan="6" align="left">
                                <span>商品SKU属性</span>
                            </td>
                        </tr>
                        <tr class="b_c3">
                            <% for (int i = 1; i <= 5; i++)
                           {%>
                        <%var item = this.SkuProInfos.FirstOrDefault(obj => obj.display_index == i);
                          if (item != null)
                          {
                        %>
                        <th scope="col" width="150">
                            <%=item.prop_name %>
                        </th>
                        <%}
                      else
                      {%>
                        <th class="hidden">
                        </th>
                        <%} %>
                        <%} %>
                     <th scope="col" width="100">
                                Barcode
                       </th>
                        </tr>
            <asp:Repeater runat="server" ID="repTable">
                <ItemTemplate>
                    <tr class="b_c4">
                        <td align="center" class="<%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 1)==null?"hidden":"" %>">
                            <%#Eval("prop_1_detail_name")%>
                        </td>
                        <td align="center" class="<%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 2)==null?"hidden":"" %>">
                            <%#Eval("prop_2_detail_name")%>
                        </td>
                        <td align="center" class="<%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 3)==null?"hidden":"" %>">
                            <%#Eval("prop_3_detail_name")%>
                        </td>
                        <td align="center" class="<%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 4)==null?"hidden":"" %>">
                            <%#Eval("prop_4_detail_name")%>
                        </td>
                        <td align="center" class="<%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 5)==null?"hidden":"" %>">
                            <%#Eval("prop_5_detail_name")%>
                        </td>
                        <td align="center">
                            <%#Eval("barcode")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="b_c5">
                        <td align="center" class="<%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 1)==null?"hidden":"" %>">
                            <%#Eval("prop_1_detail_name")%>
                        </td>
                        <td align="center" class="<%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 2)==null?"hidden":"" %>">
                            <%#Eval("prop_2_detail_name")%>
                        </td>
                        <td align="center" class="<%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 3)==null?"hidden":"" %>">
                            <%#Eval("prop_3_detail_name")%>
                        </td>
                        <td align="center" class="<%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 4)==null?"hidden":"" %>">
                            <%#Eval("prop_4_detail_name")%>
                        </td>
                        <td align="center" class="<%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 5)==null?"hidden":"" %>">
                            <%#Eval("prop_5_detail_name")%>
                        </td>
                        <td align="center">
                            <%#Eval("barcode")%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
                    </table>
    </div>
            <div class="bf">
            <input id="btnClose" type="button" runat="server" class=" input_bc"
                onserverclick="btnCloseClick" value="返回" />
        </div>
</asp:Content>

