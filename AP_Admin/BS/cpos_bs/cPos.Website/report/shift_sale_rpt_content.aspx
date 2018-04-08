<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" CodeFile="shift_sale_rpt_content.aspx.cs" Inherits="report_shift_sale_rpt_content" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<%@ Register Src="../controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
        function go_view(shiftid) {
            var chiled = window.parent.document.getElementById("chiledIframe");
            //$(chiled).css("height","470px");
            setTimeout(function(){$(window.parent.document.getElementById("detailList")).trigger("click");},100);
            chiled.src = "shift_sale_rpt_iframe.aspx?shiftId=" + shiftid;
        }
        function downloadExcel() {
            var condition = { type: "shift", user_name: '<%=this.Request["user_name"] %>', order_date_begin: '<%=this.Request["order_date_begin"] %>', order_date_end: '<%=this.Request["order_date_end"] %>', unit_id: '<%=this.Request["unit_id"] %>' };
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div style="background-color: #f2f1ef; display:none;" id="reportContent">
        <div class="b_bg" style="width: 100%; height: auto">
            <input type="button" value="导出" id="btnDownLoad" class="input_c" onclick="downloadExcel();return false;" />
        </div>
        <div class="ss_bg">
            <table class="ss_jg" cellspacing="1" cellpadding="0">
                <tr class="b_c3">
                    <th scope="col" style="width:150px;">
                      收银员
                    </th>
                    <th scope="col" style="width:150px;">
                      门店名称
                    </th>
                    <th scope="col" style="width:150px;">
                       开班时间
                    </th>
                    <th scope="col" style="width:150px;">
                        交班时间
                    </th>
                    <th scope="col" style="width:150px;">
                      销售笔数
                    </th>
                    <th scope="col" style="width:150px">
                        准备金
                    </th>
                    <th scope="col" style="width:150px;">
                        销售金额
                    </th>
                    <th scope="col" style="width:150px;">
                        退款金额
                    </th>
                    <th scope="col" style="width:150px">
                        总金额
                    </th>
                    <th scope="col" style="width:150px;">
                        操作
                    </th>
                </tr>
                <%if (ShiftInfos != null && ShiftInfos.Count > 0)
                  {
                      decimal total_sales_qty = 0;
                      decimal total_total_amount = 0;
                      for (int i = 0; i < ShiftInfos.Count; i++)
                      {
                         
                %>
                <% if (i % 2 == 0)
                   { %>
                <tr class="b_c4">
                    <td align="center">
                        <%=ShiftInfos[i].sales_user%>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].unit_name%>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].open_time%>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].close_time%>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].sales_qty%>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].deposit_amount %>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].sale_amount%>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].return_amount %>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].sales_total_amount %>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0);" onclick='go_view("<%=ShiftInfos[i].shift_id %>")'>
                            <img src="../img/view.png" title="明细" alt="明细" /></a>
                    </td>
                </tr>
                <%}
                   else
                   {
                %>
                <tr class="b_c5">
                     <td align="center">
                        <%=ShiftInfos[i].sales_user%>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].unit_name%>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].open_time%>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].close_time%>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].sales_qty%>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].deposit_amount %>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].sale_amount%>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].return_amount %>
                    </td>
                    <td align="center">
                        <%=ShiftInfos[i].sales_total_amount %>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0);" onclick='go_view("<%=ShiftInfos[i].shift_id %>")'>
                            <img src="../img/view.png" title="明细" alt="明细" /></a>
                    </td>
                </tr>
                <%} %>
                <%
                          
                    total_sales_qty += ShiftInfos[i].sales_qty;
                    total_total_amount += ShiftInfos[i].sales_total_amount;
                      } %>
                 <tr class="b_c5">
                 <td align="center">总计</td>
                 <td align="center">&nbsp;</td>
                 <td align="center">&nbsp;</td>
                 <td align="center">&nbsp;</td>
                 <td align="center"><%=this.Info.sales_total_qty %></td>
                 <td align="center"><%=this.Info.total_deposit_amount %></td>
                 <td align="center"><%=this.Info.total_sale_amount %></td>
                 <td align="center"><%=this.Info.total_return_amount %></td>
                 <td align="center"><%=this.Info.sales_total_total_amount %></td>
                 <td align="center"></td>

                 </tr>
                <%
       
                  } %>
            </table>
            <uc1:SplitPageControl ID="SplitPageControl1" runat="server" CssClass="pagebar" PageSize="10"
                OnRequireUpdate="SplitPageControl1_RequireUpdate" />
        </div>
    </div>
</asp:Content>

