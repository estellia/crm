<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" CodeFile="unit_sale_rpt_content.aspx.cs" Inherits="report_unit_sale_rpt_content" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<%@ Register Src="../controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
   
        function go_view(unit_id, order_date) {
            var _parent = window.parent;
            var chiled =_parent.document.getElementById("chiledIframe");
            //$(chiled).css("height","470px");
            setTimeout(function(){$(_parent.document.getElementById("detailList")).trigger("click");},100);
            chiled.src = "unit_sale_rpt_iframe.aspx?unit=" + unit_id + "&orderdate=" + order_date;
        }
    function downloadExcel() {
        var condition = { type: "saleMain", order_no: '<%=this.Request["order_no"] %>', order_date_begin: '<%=this.Request["order_date_begin"] %>', order_date_end: '<%=this.Request["order_date_end"] %>', unit_id: '<%=this.Request["unit_id"] %>' };
        $.ajax({
            url:'../ajaxhandler/ajax_handler_toExecl.ashx',
            type:'post',
            data:condition,
            dataType:'text',
            success:function(data){
                if (data) {
                    window.parent.location.href = data;
                }
            },
            error:function(data){
                alert(data.responseText);
            }
        });
    }
    $(function () { 
        var length = <%=this.ICount %>;
        if(length==0){
            $("#reportContent").css("display","none");
        }else{
            $("#reportContent").css("display","");
        }
        var height = document.body.offsetHeight;
        $(window.parent.document.getElementById("mainIframe")).css("height",height+"px");
        if(height>470){
            $(window.parent.document.getElementById("MainContent_tabContainer1_result")).css("height",height+30+"px");
            $(window.parent.document.getElementById("MainContent_tabContainer1_result")).parent().css("height",height+50+"px");
        }
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="background-color: #f2f1ef; display:none;" id="reportContent">
        <div class="b_bg" style="width: 100%; height: auto">
            <input type="button" value="导出" id="btnDownLoad" class="input_c" onclick="downloadExcel();return false;" />
        </div>
        <div class="ss_bg">
            <table class="ss_jg" cellspacing="1" cellpadding="0">
                <tr class="b_c3">
                    <th scope="col" style="width:168px;">
                        日期
                    </th>
                    <th scope="col" style="width:168px;">
                        门店名称
                    </th>
                    <th scope="col" style="width:168px;">
                        销售笔数
                    </th>
                    <th scope="col" style="width:168px;">
                        日销售金额
                    </th>
                    <th scope="col" style="width:168px;">
                        操作
                    </th>
                </tr>
                <%if (ReportInfoList != null && ReportInfoList.SalesReportList.Count > 0)
                  {
                      var count = 0;
                      for (int i = 0; i < ReportInfoList.SalesReportList.Count; i++)
                      {
                          count = i;
                %>
                <% if (i % 2 == 0)
                   { %>
                <tr class="b_c4">
                    <td align="center">
                        <%=ReportInfoList.SalesReportList[i].order_date%>
                    </td>
                    <td align="center">
                        <%=ReportInfoList.SalesReportList[i].unit_name%>
                    </td>
                    <td align="center">
                        <%=ReportInfoList.SalesReportList[i].sales_qty%>
                    </td>
                    <td align="center">
                        <%=ReportInfoList.SalesReportList[i].sales_amount%>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0);" onclick="go_view('<%=ReportInfoList.SalesReportList[i].unit_id %>','<%=ReportInfoList.SalesReportList[i].order_date %>')">
                            <img src="../img/view.png" title="明细" alt="明细" /></a>
                    </td>
                </tr>
                <%}
                   else
                   {
                %>
                <tr class="b_c5">
                    <td align="center">
                        <%=ReportInfoList.SalesReportList[i].order_date%>
                    </td>
                    <td align="center">
                        <%=ReportInfoList.SalesReportList[i].unit_name%>
                    </td>
                    <td align="center">
                        <%=ReportInfoList.SalesReportList[i].sales_qty%>
                    </td>
                    <td align="center">
                        <%=ReportInfoList.SalesReportList[i].sales_amount%>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0);" onclick="go_view('<%=ReportInfoList.SalesReportList[i].unit_id %>','<%=ReportInfoList.SalesReportList[i].order_date %>')">
                           <img src="../img/view.png" title="明细" alt="明细" /></a>
                    </td>
                </tr>
                <%} %>
                <%
                          
                          
                      } %>
                <tr class="<%=count%2==0?"b_c5":"b_c4" %>">
                    <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
                </tr>
                <tr class="<%=(count+1)%2==0?"b_c5":"b_c4" %>">
                    <td align="center">总计</td>
                    <td>&nbsp;</td>
                    <td align="center"><%=ReportInfoList.sales_total_qty %></td>
                    <td align="center"><%=ReportInfoList.sales_total_amount %></td>
                    <td>&nbsp;</td>
                </tr>
                <%
                 
                  } %>
            </table>
            <uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pagebar" PageSize="10"
                OnRequireUpdate="SplitPageControl1_RequireUpdate" />
        </div>
    </div>
</asp:Content>

