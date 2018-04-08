<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="inout_pos_receipt_show" Codebehind="pos_receipt_show.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .header
        {
            background-color: #ccc;
        }
        #item_detail td
        {
            text-align: center;
        }
            .hidden,.cPos_a .right .ss_bg .b_c4 .hidden
        {
            width: 0px;
            overflow:hidden; 
            padding:0;
            margin:0; 
            font-size:0px;
        }
    </style>
     <script type="text/javascript">
     $(function(){
            if(<%=btnSave.Visible?"false":"true" %>)
            {
                disableCtrls("#tab");
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
        <span>pos小票明细</span>
    </div>
    <div>
        <table class="ss_tj" cellpadding="0" cellspacing="0" border="0" style="margin: 10px" id="tab">
            <tr>
                <td align="right" class="tit_c">
                    单据编号:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="order_no" MaxLength="30" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    日期:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="order_date" MaxLength="10" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    状态:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="status_desc" MaxLength="10" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="tit_c">
                    门店:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="create_unit_name" MaxLength="50" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    业务员:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="pos_name" MaxLength="30" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    数量:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="tital_qty" MaxLength="15" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="tit_c">
                    折扣:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="dicount_rate" MaxLength="15" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    金额:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="total_amoount" MaxLength="15" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    找零:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="keep_the_change" MaxLength="15" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="tit_c">
                    抹零:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="wiping_zero" MaxLength="15" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    会员名:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="vip_no" MaxLength="50" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    &nbsp;
                </td>
                <td class="td_lp">
                    &nbsp;
                </td>
            </tr><%--
            <tr>
                <td align="right" class="tit_c">
                    制单人:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="cretae_user_name" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    发出人:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="send_user_name" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    验收人:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="accepet_user_name" runat="server"></asp:TextBox>
                </td>
            </tr>--%><%--
            <tr>
                <td align="right" class="tit_c">
                    制单时间:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="create_time" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    发出时间:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="send_time" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    验收时间:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="accpect_time" runat="server"></asp:TextBox>
                </td>
            </tr>--%>
            <tr>
                <td align="right" class="tit_c">
                    收银员:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="approve_user_name" MaxLength="50" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    收银时间:
                </td>
                <td class="td_lp">
                    <asp:TextBox ID="approve_time" MaxLength="10" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    &nbsp;
                </td>
                <td class="td_lp">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" class="tit_c">
                    备注:
                </td>
                <td class="td_lp" colspan="3">
                    <asp:TextBox ID="remark" TextMode="MultiLine" Width="100%" runat="server" MaxLength="180"></asp:TextBox>
                </td>
                <td align="right" class="tit_c">
                    &nbsp;
                </td>
                <td class="td_lp">
                    &nbsp;
                </td>
            </tr>
        </table>
        <div class="tit_con">
            <span>商品明细</span>
        </div>
        <div class="ss_bg">
            <table id="item_detail" style="margin-left: auto; margin-right: auto;" cellpadding="0"
                cellspacing="1" class="ss_jg" border="0">
                <thead>
                    <tr class="b_c3">
                        <th scope="col" class="gv-no">
                            序号
                        </th>
                        <th scope="col" class="gv-code">
                            商品代码
                        </th>
                        <th scope="col" class="gv-4word">
                            商品名称
                        </th>
                        <% for (int i = 1; i <= 5; i++)
                           {%>
                        <%var item = this.SkuProInfos.FirstOrDefault(obj => obj.display_index == i);
                          if (item != null)
                          {
                        %>
                        <th scope="col" width="80" columnindex="<%= item.display_index %>">
                            <%=item.prop_name %>
                        </th>
                        <%} %>
                        <%} %>
                        <th scope="col" width="80">
                            数量
                        </th>
                        <th scope="col" width="80">
                            单价
                        </th>
                        <th scope="col" width="80">
                            折扣
                        </th>
                        <th scope="col" width="80">
                            标准价
                        </th>
                        <th scope="col" width="80">
                            计划售价
                        </th>
                        <th scope="col" width="80">
                            总金额
                        </th>
                        <th scope="col" width="100">
                            备注
                        </th>
                    </tr>
                </thead>
                <% int sque = 1; if (this.InoutDetailInfos != null && this.InoutDetailInfos.Count != 0) foreach (var item in this.InoutDetailInfos)
                       {
                %>
                <tr class="<%=sque%2==0?"b_c4":"b_c5" %>">
                    <td>
                        <%= sque%>
                    </td>
                    <td>
                        <%= item.item_code??"&nbsp;" %>
                    </td>
                    <td>
                        <%= item.item_name??"&nbsp;" %>
                    </td>
                    <%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index ==1) == null ? "" : "<td>" + item.prop_1_detail_name + "</td>"%>
                    <%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 2) == null ? "" : "<td>" + item.prop_2_detail_name + "</td>"%>
                    <%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 3) == null ? "" : "<td>" + item.prop_3_detail_name + "</td>"%>
                    <%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 4) == null ? "" : "<td>" + item.prop_4_detail_name + "</td>"%>
                    <%= this.SkuProInfos.FirstOrDefault(obj => obj.display_index == 5) == null ? "" : "<td>" + item.prop_5_detail_name + "</td>"%>
                    <td>
                        <%= item.enter_qty.ToString() %>
                    </td>
                    <td>
                        <%= item.enter_price.ToString() %>
                    </td>
                    <td>
                        <%= item.discount_rate.ToString() %>
                    </td>
                    <td>
                        <%= item.std_price.ToString() %>
                    </td>
                    <td>
                        <%= item.plan_price.ToString() %>
                    </td>
                    <td>
                        <%= item.retail_amount.ToString()%>
                    </td>
                    <td>
                        <%= item.remark??"&nbsp;" %>
                    </td>
                </tr>
                <% sque++;
                   } %>
            </table>
        </div>
        <div class="bf">
         <asp:Button ID="btnSave" runat="server" Text="保存" class="input_bc"/>
            <input type="button" value="返回" class="input_fh" onclick="location.href='<%=this.Request.QueryString["from"] %>';" />
        </div>
    </div>
</asp:Content>
