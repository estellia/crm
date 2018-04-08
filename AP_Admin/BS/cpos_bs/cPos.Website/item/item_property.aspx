<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" CodeFile="item_property.aspx.cs" Inherits="item_item_property" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<style type="text/css">
    .tdHeaer
    {
        background:#ccc;
        border-left:1px solid Black;
        border-right:1px solid Black;
        text-align:center;
    }
    .tdHeaderNormal
    {
        background:#ccc;
        border-right:1px solid Black;
        text-align:center;
    }
    .center
    {
        text-align:center;
    }
    .tdFirst
    {
        border-left:1px solid black;
        border-right:1px solid black;
        border-bottom:1px solid black;
    }
    .tdNrmal
    {
        border-right:1px solid black;
        border-bottom:1px solid black;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="ss_bg">
        
            <asp:Repeater runat="server" ID="repTable">
                <HeaderTemplate>
                    <table border="0" cellpadding="0" cellspacing="1" class="ss_jg">
                        <tr>
                            <td colspan="3" class="tit_con" style="text-align:left">
                                <div>
           <span>商品属性</span> 
        </div>
                            </td>
                        </tr>
                        <tr class="b_c3">
                            <th scope="col" width="150" align="center">属性组</th>
                            <th scope="col" width="150" align="center">属性</th>
                            <th scope="col" width="150" align="center">属性值</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class=" b_c4">
                        <td align="center">
                            <%#Eval("PropertyCodeGroupName")%>
                        </td>
                        <td align="center">
                            <%#Eval("PropertyCodeName")%>
                        </td>
                        <td align="center">
                            <%#Eval("PropertyCodeValue")%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class=" b_c5">
                        <td align="center">
                            <%#Eval("PropertyCodeGroupName")%>
                        </td>
                        <td align="center">
                            <%#Eval("PropertyCodeName")%>
                        </td>
                        <td align="center">
                            <%#Eval("PropertyCodeValue")%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table></FooterTemplate>
            </asp:Repeater>
    </div>
            <div class="bf">
            <input id="btnClose"  type="button" runat="server" class=" input_bc"
                onserverclick="btnCloseClick" value="返回" />
        </div>
</asp:Content>

