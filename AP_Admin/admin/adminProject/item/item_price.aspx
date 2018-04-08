<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="item_item_price" Codebehind="item_price.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
    .td
    {
        background:#ccc;
        border:1px solid Black;
        border-left:0;
        text-align:center;
    }
    .reptable
    {
        margin-left:auto;
        margin-right:auto;
    }
    .reptable td
    {
        text-align:center;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <div>
<div class="ss_bg">
    <table class="ss_jg" cellpadding="0" cellspacing="1" border="0">
        <tr>
            <td colspan="2" class="tit_con"><span>商品价格</span></td>
        </tr>
            <tr class="b_c3">
                <th scope="col" width="150" align="center">价格类型</th>
                <th scope="col" width="150" align="center">价格</th>
            </tr>
<asp:Repeater runat="server" ID="repTable">
    <ItemTemplate><tr class="b_c4"><td align="center"><%#Eval("item_price_type_name")%></td><td align="center"><%#Eval("item_price")%></td></tr></ItemTemplate>
    <AlternatingItemTemplate>
        <tr class="b_c5"><td align="center"><%#Eval("item_price_type_name")%></td><td align="center"><%#Eval("item_price")%></td></tr>
    </AlternatingItemTemplate>
</asp:Repeater>
    </table>
</div>
<div class="bf">
<input id="btnClose" type="button" runat="server" class=" input_bc" onserverclick="btnCloseClick" value="返回" />
</div>
</div>
</asp:Content>

