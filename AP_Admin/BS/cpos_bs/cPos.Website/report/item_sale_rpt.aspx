<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="item_sale_rpt.aspx.cs" Inherits="report_item_sale_rpt" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<%@ Register Src="../controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
    function resetQueryParameter() {
        this.location.href = '<%=this.Request.Path %>';
     
    }
    function getTree() {
        return document.getElementById("<%=selUnit.ClientID %>");
    }
    function downloadExcel() {
        var condition = { type: "item", item_code: $("#<%=tbItemCode.ClientID %>").val(), item_name: $("#<%=tbItemName.ClientID %>").val(),barcode:$("#<%=tbBarcode.ClientID %>").val(), order_date_begin: $("#<%=selOrderDateBegin.ClientID %>").val(), order_date_end: $("#<%=selOrderDateEnd.ClientID %>").val(), unit_id: $("#<%=selUnit.ClientID %>")[0].values() };
        $.ajax({
            url: '../ajaxhandler/ajax_handler_toExecl.ashx',
            type: 'post',
            data: condition,
            dataType: 'text',
            success: function (data) {
                if (data) {
                    location.href = data;
                }
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <td  class="tit_c">
                商品编码：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbItemCode" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td  class="tit_c">
                商品名称：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbItemName" runat="server" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
                商品条形码：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbBarcode" runat="server" MaxLength="30"></asp:TextBox>
            </td>
            <td  class="tit_c">
                门店：
            </td>
            <td class="td_lp">
            <uc2:DropDownTree runat="server" ID="selUnit"  MultiSelect="True" Url="../ajaxhandler/tree_query.ashx?action=unit&showCheck=true" />
                
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
               销售开始日期：
            </td>
            <td class="td_lp">
                <input type="text" runat="server"  readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_selOrderDateBegin','MainContent_selOrderDateEnd');"
                    title="双击清除时间" id="selOrderDateBegin" />
            </td>
            <td  class="tit_c">
               销售结束日期：
            </td>
            <td class="td_lp">
                <input type="text" runat="server"  readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_selOrderDateEnd');" title="双击清除时间" id="selOrderDateEnd" />
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" 
            onclick="btnQuery_Click"/>
        <input type="button" id="btnReset" class="input_c" value="重置" onclick="resetQueryParameter()" />
        <input type="button" value="导出" id="btnDownLoad" class="input_c" onclick="downloadExcel();return false;" />
    </div>
    <div style="background-color: #f2f1ef;">
        <div class="tit_con">
            <span>搜索结果</span>
        </div>
       
        <div class="ss_bg">
        <% if (IsPostBack)
           { %>
            <table class="ss_jg" cellspacing="1" cellpadding="0">
                <tr class="b_c3">
                    <th scope="col" style="width: 150px;">
                       商品编码
                    </th>
                    <th scope="col" style="width: 150px;">
                      商品名称
                    </th>
                     <th scope="col" style="width: 150px;">
                        商品条形码
                    </th>
                    <th scope="col" style="width: 150px;">
                        销售单价
                    </th>
                    <th scope="col" style="width: 150px;">
                       销售数量
                    </th>
                    <th scope="col" style="width:150px;">
                    销售金额
                    </th>
                </tr>
                <%if (itemSalesReportInfoList != null && itemSalesReportInfoList.Count > 0)
                  {
                      for (int i = 0; i < itemSalesReportInfoList.Count; i++)
                      {
                         
                %>
                <% if (i % 2 == 0)
                   { %>
                <tr class="b_c4">
                    <td align="center">
                        <%=itemSalesReportInfoList[i].item_code%>
                    </td>
                    <td align="center">
                        <%=itemSalesReportInfoList[i].item_name%>
                    </td>
                    <td align="center">
                        <%=itemSalesReportInfoList[i].barcode%>
                    </td>
                    <td align="center">
                        <%=itemSalesReportInfoList[i].std_price%>
                    </td>
                  <td align="center">
                        <%=itemSalesReportInfoList[i].enter_qty%>
                      
                    </td>
                    <td align="center">
                        <%=itemSalesReportInfoList[i].enter_amount%>
                      
                    </td>
                    
                </tr>
                <%}
                   else
                   {
                %>
                <tr class="b_c5">
                    <td align="center">
                        <%=itemSalesReportInfoList[i].item_code%>
                    </td>
                    <td align="center">
                        <%=itemSalesReportInfoList[i].item_name%>
                    </td>
                    <td align="center">
                        <%=itemSalesReportInfoList[i].barcode%>
                    </td>
                    <td align="center">
                        <%=itemSalesReportInfoList[i].std_price%>
                    </td>
                  <td align="center">
                        <%=itemSalesReportInfoList[i].enter_qty%>
                      
                    </td>
                    <td align="center">
                        <%=itemSalesReportInfoList[i].enter_amount%>
                      
                    </td>
                    
                </tr>
                <%} %>
                <%
                          
                          
            } %>
                <%
       
            } %>
            </table>
            <uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pag" PageSize="10"
                OnRequireUpdate="SplitPageControl1_RequireUpdate" />
                <%} %>
        </div>
    </div>
</asp:Content>
