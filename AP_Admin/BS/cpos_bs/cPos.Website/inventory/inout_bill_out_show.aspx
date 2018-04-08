<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" CodeFile="inout_bill_out_show.aspx.cs" Inherits="inventory_inout_bill_out_show" %>

<%@ Register Src="~/controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
         .hidden
        {
             display:none;
            }
    #tb_body td{ text-align:center;}
</style>
<script type="text/javascript">
    var data = <%=this.InoutDetail %>;
</script>
<script type="text/javascript">
    function buildItem() {
        var array = [];
        data.sort(function(x,y){
                return x.item_name.localeCompare(y.item_name);
            });
        for (var i = 0; i < data.length; i++) {
            array.push(buildItemTr(data[i], i));
        }
        $("#tb_body tr").remove();
        $(array).each(function () {
            $("#tb_body").append($(this));
        });
            getInvTableShowOrHide("tb_body","title1");
    }
    function buildItemTr(item, index) {
        var show = "";
        if ('<%=this.ViewState["action"] %>' == "Visible") {
            show = "class=\"hidden\"";
        } else {
            show = "";
        } 
        var _class = "";
        if (parseInt(index) % 2 == 0) {
            _class = "b_c4";
        } else {
            _class = "b_c5";
        }
        var tr = $("<tr class=\""+_class+"\"><td>" + (item.item_code==null?"&nbsp;":item.item_code) + "</td><td>" + (item.item_name==null?"&nbsp;":item.item_name) + "</td><td " + getClass(1) + ">" + (item.prop_1_detail_name==null?"&nbsp;":item.prop_1_detail_name) + "</td><td " + getClass(2) + ">" + (item.prop_2_detail_name==null?"&nbsp;":item.prop_2_detail_name) + "</td><td " + getClass(3) + ">" + (item.prop_3_detail_name==null?"&nbsp;":item.prop_3_detail_name) + "</td><td " + getClass(4) + ">" + (item.prop_4_detail_name==null?"&nbsp;":item.prop_4_detail_name) + "</td><td " + getClass(5) + ">" + (item.prop_5_detail_name==null?"&nbsp;":item.prop_5_detail_name) + "</td><td>" + item.order_qty + "</td><td>" + item.enter_qty + "</td><td>" + item.std_price + "</td><td>" + item.order_discount_rate + "</td><td>" + item.enter_price + "</td><td>" + item.discount_rate + "</td><td>" + item.retail_price + "</td><td>" + item.retail_amount + "</td><td><a href='#' "+show+" detail_id=\"" + item.order_detail_id + "\" index=\"" + index + "\" onclick=\"removeThis(this);\"><img src=\"../img/delete.png\" alt=\"\" title=\"删除\" /></a></td></tr>");
        return tr;
    }
    function getClass(prop) {
        var isShow = false;
        $("th[index]").each(function () {
            if ($(this).attr("index") == prop) {
                isShow = true;
                return false;
            }
        });
        if (isShow) {
            return "class=\"\"";
        } else {
            return "class=\"hidden\"";
        }
    }
    //检查商品是否存在
    function checkDetailExisits(value) {
        var is_exi = false;
        $(data).each(function () {
            if (this.sku_id == value.sku_id) {
                is_exi = true;
                return false;
            }
        });
        return is_exi;
    }
    //移除
    function removeThis(sender) {
        $(sender).parent().parent().remove();
            getInvTableShowOrHide("tb_body","title1");
        var index = $(sender).attr("index");
        var detail_id = $(sender).attr("detail_id");
        $(data).each(function () {
            if (this.index|this.index==0) {
                if (this.index == index) {
                    data.removeValue(this);
                    return false;
                }
            } else {
                if (this.order_detail_id == detail_id) {
                    data.removeValue(this);
                    return false;
                }
            }
        });
        recountTotal();
    }
    function showItem() {
        var value = openWindow("inout_bill_out_show_item.aspx", 420, 720, '');
        if (value) {
            if (checkDetailExisits(value)) {
                alert("sku信息已存在");
                return;
            }
            value.index = data.length;
            data.push(value);
            recountTotal();
            buildItem(data);
        }
    }
    //重新计算总金额
    function recountTotal() {
        var total = 0;
        $(data).each(function (index, item) {
            total += parseFloat(item.retail_amount ? item.retail_amount : 0);
        });
        $("#<%=tbTotalAmount.ClientID %>").val(total);
        $("#<%=hidTotalAmount.ClientID %>").val(total);
    }
    //page_load
    $(function () {
        buildItem();
        $("#<%=selSalesUnit.ClientID %>")[0].onchanged = unitChange;
    });
    function unitChange() {
        $("#<%=hidInout.ClientID %>").val(JSON.stringify(data));
        $("#<%=hidUnit.ClientID %>").trigger("click");
    }
    //open a window 
    function openWindow(url, width, height, NewWin) {

        var popUpWin = 0;
        var left = 200;
        var top = 200;
        if (screen.width >= width) {
            left = Math.floor((screen.width - width) / 2);
        }
        if (screen.height >= height) {
            top = Math.floor((screen.height - height) / 2);
        }

        var from = window.showModalDialog(url, $("#<%=tbDiscountRate.ClientID %>").val(), "dialogHeight=" + width + "px;dialogWidth=" + height + "px;dialogTop=" + top + "px;dialogLeft=" + left + "px;help=no;scroll=no;");
        return from;
    }
    function checkInput() {
        if ($("#<%=tbOrderNo.ClientID %>").val() == "") {
            alert("单号不能为空!");
            $("#<%=tbOrderNo.ClientID  %>").focus();
            return false;
        }
        if ($("#<%=selReasonType.ClientID %>").val() == "-1") {
            $("#<%=selReasonType.ClientID %>").focus();
            alert("必须选择出库类型!");
            return false;
        }
        if (!$("#<%=selSalesUnit.ClientID %>")[0].values()[0]) {
            $("#<%=selSalesUnit.ClientID %>").focus();
            alert("必须选择销售单位!");
            return false;
        }
        if ($("#<%=selWarehouse.ClientID %>").val() == "-1") {
            $("#<%=selWarehouse.ClientID %>").focus();
            alert("必须选择仓库!");
            return false;
        }
        if (!$("#<%=selPurchaseUnit.ClientID %>")[0].values()[0]) {
            $("#<%=selPurchaseUnit.ClientID %>").focus();
            alert("必须选择采购单位!");
            return false;
        }
        if ($("#<%=selOrderDate.ClientID %>").val() == "") {
            alert("单据日期不能为空!");
            $("#<%=selOrderDate.ClientID %>").focus();
            return false;
        }
        if ($("#<%=selCompleteDate.ClientID %>").val() == "") {
            alert("出库日期不能为空!");
            $("#<%=selCompleteDate.ClientID %>").focus();
            return false;
        }
        if(data.length==0){
            alert("必须添加至少一条商品明细信息!");
            return false;
        }
        return true;
    }
    function dosave() {
        if (!checkInput()) {
            return false;
        }
        $("#<%=hidInout.ClientID %>").val(JSON.stringify(data));
        return true;
    }

     $(function(){
            if(<%=btnSave.Visible?"false":"true" %>)
            {
                disableCtrls("#tab,#btns");
            }
            getInvTableShowOrHide("tb_body","title1");
        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con">
    <span>出库单明细</span>
</div>
    <table border="0" cellspacing="0" cellpadding="0" class="con_tab" style="margin-left:auto; margin-right:auto" id="tab">
        <tr>
            <td  class="td_co">
                出库单单号：
            </td>
            <td width="320">
                <div class="box_r">
                </div>
                <asp:TextBox ID="tbOrderNo" runat="server" ReadOnly="true" MaxLength="30" ></asp:TextBox>
            </td>
            <td  class="td_co">
                出库单类型：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:DropDownList runat="server" ID="selReasonType" >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                销售单位：
            </td>
            <td>
                <div class="box_r">
                </div>
                <uc1:DropDownTree runat="server" MultiSelect="false" ID="selSalesUnit"  Url="../ajaxhandler/tree_query.ashx?action=unit"/>
            </td>
            <td  class="td_co">
                仓库：
            </td>
            <td>
                <div class="box_r">
                </div>
                <asp:DropDownList runat="server" ID="selWarehouse" >
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                采购单位：
            </td>
            <td>
                <div class="box_r">
                </div>
                <uc1:DropDownTree runat="server" MultiSelect="false" ID="selPurchaseUnit"  Url="../ajaxhandler/tree_query.ashx?action=unit"/>
            </td>
            <td  class="td_co" >
                折扣：
            </td>
            <td>
                <div class="box_r"></div>
                <asp:TextBox ID="tbDiscountRate"  Text="1" runat="server" MaxLength="15"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                单据日期：
            </td>
            <td>
                <div class="box_r">
                </div>
                <input type="text" readonly="readonly" id="selOrderDate" title="双击清除时间" ondblclick="this.value=''" onchange="" onclick="Calendar('MainContent_selOrderDate');" runat="server" />
            </td>
            <td  class="td_co">
                出库日期：
            </td>
            <td>
                <div class="box_r">
                </div>
                <input type="text" readonly="readonly" id="selCompleteDate" title="双击清除时间" ondblclick="this.value=''" onchange="" onclick="Calendar('MainContent_selCompleteDate');" runat="server" />
            </td>
        </tr>
        <tr>
            <td  class="td_co" >
                来源单据号：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbRefOrderNo"  runat="server" MaxLength="30"></asp:TextBox>
            </td>
             <td class="td_co" >
                总金额：
            </td>
            <td class=" td_lp">
                <input type="hidden" runat="server" id="hidTotalAmount" />
                <asp:TextBox runat="server" ID="tbTotalAmount" ReadOnly="true" CssClass="ban_in_oper_time" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="td_co">
                备注：
            </td>
            <td class="td_lp" colspan="3">
                <div style="width:717px;*width:857px;_width:857px">
                <asp:TextBox TextMode="MultiLine" ID="tbRemark" Height="50px" runat="server" Width="100%" MaxLength="180"></asp:TextBox>
                </div>
            </td>
        </tr>
    </table>
    <div class="b_bg" id="btns">
        <input type="button" class="input_c" value="添加商品" id="btnAdd" <%=this.ViewState["action"].ToString()=="Visible"?"disabled":"" %> onclick="showItem();"/>
    </div>
    <div class="tit_con" id="title1">
        <span>商品明细</span></div>
    <div class="ss_bg">
        <div style="width:auto; height:auto; overflow-y:auto;">
        <table cellpadding="0" cellspacing="1" class="ss_jg" border="0">
            <thead>
                <tr class="b_c3">
                    <th scope="col" width="100">
                        商品编码
                    </th>
                    <th scope="col" width="100">
                        商品名称
                    </th>
                    <% for (int i = 1; i <= 5; i++)
                       {
                           var item = SkuPropInfos.FirstOrDefault(obj => obj.display_index == i);
                           if (item != null)
                           {%>
                    <th scope="col" width="80" index="<%= item.display_index %>">
                        <%=item.prop_name%>
                    </th>
                    <%}
                        else
                        {%>
                    <th class="hidden">
                    </th>
                    <%}
                    }%>
                    <th scope="col" width="80">
                        预订数
                    </th>
                    <th scope="col" width="80">
                        出库数
                    </th>
                    <th scope="col" width="80">
                        建议零售价
                    </th>
                    <th scope="col" width="80">
                        合同折扣
                    </th>
                    <th scope="col" width="80">
                        合同折扣价
                    </th>
                    <th scope="col" width="80">
                        折上折
                    </th>
                    <th scope="col" width="80">
                        最终零售价
                    </th>
                    <th scope="col" width="80">
                        总金额
                    </th>
                    <th scope="col" width="80">
                        操作
                    </th>
                </tr>
            </thead>
            <tbody id="tb_body">
            </tbody>
        </table>
        </div>
    </div>
    <div class="bf">
        <input type="button" runat="server" class=" input_bc" value="保存" id="btnSave" onclick="if(!dosave()) return false;" onserverclick="btnSaveClick"/>
        <input type="button" class=" input_fh" value="返回" id="btnReturn" onclick="location.href='<%=this.Request.QueryString["from"]??"stock_query.aspx" %>'"/>
    </div>
    <input type="hidden" runat="server" id="hidInout" />
    <input type="button" runat="server" onserverclick="btnUnitClick" id="hidUnit" style="display:none"/>
</asp:Content>

