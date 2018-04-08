<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="unit_sale_rpt_iframe.aspx.cs" Inherits="report_unit_sale_rpt_iframe" %>
<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<script type="text/javascript">
    function go_delete(order_id) {
        $("#<%=hidOrderId.ClientID %>").val(order_id);
        $("#<%=hidDelete.ClientID %>").trigger("click");
    }
    $(function () {
        window.frameElement.style.height = $(window.document).height();
    });

    function downloadExcel() {
        var that = this;
        var condition = { type: "saleDetail"};
        $.ajax({
            url:'../ajaxhandler/ajax_handler_toExecl.ashx',
            type:'post',
            data:condition,
            dataType:'text',
            success:function(data){
                if (data) {
                    that.parent.location.href = data;
                }
            },
            error:function(data){
                alert(data.responseText);
            }
        });
    }
    $(function () { 
        var length= <%=ICount %>;
        if(length==0){
            $("#detail").css("display","none");
        }else{
            $("#detail").css("display","");
        }
        var height = document.body.offsetHeight;
        $(window.parent.document.getElementById("chiledIframe")).css("height",height+"px");
        if(height>470){
            $(window.parent.document.getElementById("MainContent_tabContainer1_detail")).css("height",height+30+"px");
            $(window.parent.document.getElementById("MainContent_tabContainer1_detail")).parent().css("height",height+50+"px");
        }
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="background-color: #f2f1ef;display:none;" id="detail">
        <div class="b_bg" style="width: 100%; height: auto">
            <input type="button" value="导出" id="btnDownLoad" class="input_c" onclick="downloadExcel();return false;" />
            <asp:Button ID="btnResetDetail" runat="server" CssClass="input_c" Text="清空" 
                onclick="btnResetDetail_Click" />
        </div>
        <div class="ss_bg">
            <table class="ss_jg" cellspacing="1" cellpadding="0">
                <tr class="b_c3">
                    <th scope="col" style="width: 120px;">
                        单据时间
                    </th>
                    <th scope="col" style="width:120px">
                        销售员
                    </th>
                    <th scope="col" style="width: 120px;">
                        收银员
                    </th>
                    <th scope="col" style="width: 120px;">
                        单据单号
                    </th>
                    <th scope="col" style="width: 120px;">
                        会员姓名
                    </th>
                    <th scope="col" style="width: 120px;">
                        支付方式
                    </th>
                    <th scope="col" style="width: 120px;">
                        单笔销售
                    </th>
                    <th scope="col" style="width: 120px;">
                        操作
                    </th>
                </tr>
                <% if (this.Source != null&&this.Source.Count()!=0)
                   {
                       var source = this.Source;
                       var i = 0;
                       decimal total = 0;
                       foreach (var item in source)
                       {
                %>
                <tr class="<%=i%2==0?"b_c4":"b_c5" %>">
                    <td align="center">
                        <%=item.create_time%>
                    </td>
                    <td align="center">
                        <%=item.sales_user %>
                    </td>
                    <td align="center">
                        <%=item.create_user_name%>
                    </td>
                    <td align="center">
                        <%=item.order_no%>
                    </td>
                    <td align="center">
                        <%=item.vip_no%>
                    </td>
                    <td align="center">
                        <%=item.payment_name%>
                    </td>
                    <td align="center">
                        <%=item.total_amount%>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0);" onclick="go_delete('<%=item.order_id %>')">
                            <img src="../img/delete.png" title="删除" alt="删除" /></a>
                    </td>
                </tr>
                <%
i++;
total += item.total_amount;
                       }
                %>
                <tr class="<%=i%2==0?"b_c4":"b_c5" %>">
                    <td align="center">
                        总计
                    </td>
                    
                    <td>
                        &nbsp;
                    </td>
                    <td>
                    &nbsp;
                    </td>
                    <td>
                    &nbsp;
                    </td>
                    <td>
                    &nbsp;
                    </td>
                    <td>&nbsp;</td>
                    <td align="center" id="Td1">
                        <%=total %>
                    </td>
                    <td>
                    </td>
                </tr>
                <%
                    }%>
            </table>
       <uc1:SplitPageControl runat="server" ID="SplitPageControl1" PageSize="10" CssClass="pag" OnRequireUpdate="SplitPageControl1_RequireUpdate"/>
        </div>
    </div>
    <input type="button" runat="server" id="hidDelete" onserverclick="btnDeleteClick" style="display:none"/>
    <input type="hidden" runat="server" id="hidOrderId" />
</asp:Content>
