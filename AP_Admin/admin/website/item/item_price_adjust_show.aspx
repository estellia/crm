<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="item_price_adjust_show.aspx.cs" Inherits="item_item_price_adjust_show" %>
<%@ Register src="~/controls/DropDownTree.ascx" tagname="DropDownTree" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
             .hidden
        {
            display:none;
        }
        #skuPropList tbody td{ text-align:center;}
        #tbItemPriceInfo td{ text-align:center;}
        .b_c4 td{ text-align:center;}
        .b_c5 td{ text-align:center;}
    </style>
    <script type="text/javascript">
        var adjustPriceData=<%=this.hidItemPrice.Value %>;
        var adjustSkuListData = <%=this.hidItemSku.Value %>;
    </script>
    <script type="text/javascript">
        function go_Del(sender, type) {
            switch (type) {
                case "price": deletePriceTr(sender); break;
                case "sku":deleteAdjustSkuTr(sender);break;
                default: break;
            }
        }
        $(function () {
            autocomplete.ismulittbx = "true";
            autocomplete.mulit_tbxlist = ["<%= selItem.ClientID %>", "<%= selSkuItem.ClientID %>"];
            autocomplete.mulit_urllist = ["itemSearch.aspx", "itemSearch.aspx"];
            autocomplete.mulit_finish =
                [
                  function () {
                      if (autocomplete.selectValue["showName"] != null)
                          $("#<%= selItem.ClientID %>").attr("item_id", autocomplete.selectValue["showId"]).attr("item_code",autocomplete.selectValue["showNo"]);
                      else
                          $("#<%= selItem.ClientID %>").attr("item_Id", "").attr("item_code","");
                  },
                  function () {
                      if (autocomplete.selectValue["showName"] != null)
                          loadSkuByItemId(autocomplete.selectValue["showId"]);
                      else
                          $("#<%= selSkuItem.ClientID %>").attr("item_Id", "");
                  }
                 ];
            autocomplete.showboxid = "ShowBox";
            autocomplete.Ishighlight = true;
            autocomplete.Initial();
        });
        var priceReg = /^\-?\d{1,10}$|^\-?\d{1,10}\.?\d{1,4}$/;
        //添加调价商品价格信息
        function addjustPriceData() {
            var item_id = $("#<%=selItem.ClientID %>").attr("item_id");
            var item_name = $("#<%=selItem.ClientID %>").val();
            if(item_name==""){
                alert("商品不能为空!");
                $("#<%=selItem.ClientID %>").focus();
                return false;
            }
            if($("#<%=tbPrice.ClientID %>").val()==""){
                alert("价格信息不能为空!");
                $("#<%=tbPrice.ClientID %>").focus();
                return false;
            }
            if(!priceReg.test($("#<%=tbPrice.ClientID %>").val())){
                alert("价格数据格式不正确!");
                $("#<%=tbPrice.ClientID %>").focus();
                return false;
            }
            var exists = false;
            $(adjustPriceData).each(function () {
                if (this.item_id == item_id) {
                    alert("商品'" + item_name + "'价格信息已存在!");
                    exists = true;
                    return false;
                }
            });
            if (exists)
                return exists;
            var para = {};
            para.item_id = item_id;
            para.order_id = '<%=this.Request.QueryString["order_id"] %>';
            para.item_name = $("#<%=selItem.ClientID %>").val()+"-"+$("#<%=selItem.ClientID %>").attr("item_code");
            para.order_detail_item_id = null;
            para.price = parseFloat($("#<%=tbPrice.ClientID %>").val()).toFixed(4);
            adjustPriceData.push(para);
            buildPriceTable();
        }
        //添加一行价格信息
        function buildPriceTable(){
            $("#tbItemPriceInfo tr[item_id]").remove();
            for(var i=0;i<adjustPriceData.length;i++){
               buildPriceTr(adjustPriceData[i]);
            }
        }
        function buildPriceTr(data) {
            var length = $("#tbItemPriceInfo tr").length;
            var _class = "b_c4";
            if(length%2==0){
                _class="b_c5";
            }
            var show=""
            if('<%=ViewState["strDo"] %>'=='Visible'){
                show = "none";
            }
            var tr = "<tr class=\""+_class+"\" item_id='" + data.item_id + "'>";
            tr += "<td>" + data.item_name + "</td><td>" + data.price + "</td>";
            tr += "<td><a href=\"javascript:void(0);\" onclick=\"go_Del(this,'price')\" style=\"display:"+show+"\"><img src=\"../img/delete.png\"  alt='' title=\"删除\"/></a></td>";
            $("#tbItemPriceInfo").append(tr);
            getTableShowOrHide("tbItemPriceInfo","title1");
        }
        //删除一行价格信息
        function deletePriceTr(sender) {
            var parent = $(sender).parent().parent();
            $(sender).parent().parent().remove();
            getTableShowOrHide("tbItemPriceInfo","title1");
            $(adjustPriceData).each(function () {
                if (parent.attr("item_id") == this.item_id) {
                    adjustPriceData.removeValue(this);
                }
            });
        }
        //根据商品Id加载sku列表
        function loadSkuByItemId(item_id) {
            $.post("itemSearch.aspx", { "action": "getSkuList", "item_id": item_id }, function (data) {
                if (data) {
                    rebuildSkuTbody(data);
                }
            }, "json");
        }
        //更新SkuList列表数据
        function rebuildSkuTbody(data) {
            $("#skuPropList tbody tr").remove();
            if (data.length == 0){
                getTableShowOrHide("skuPropList","title2");
                return;
            }
            for (var i = 0; i < data.length; i++) {
                buildSkuTr(data[i]);
            }
        }
        //创建一行sku信息
        function buildSkuTr(data) {
            var length = $("#skuPropList tbody tr").length;
            var _class = "b_c5";
            if(length%2==0){
                _class="b_c4";
            }
            var tr = "<tr class=\""+_class+"\" sku_id='"+data.sku_id+"'>";
            tr += "<td class=\"hidden\">" + (data.prop_1_detail_name==null?"&nbsp;":data.prop_1_detail_name) + "</td>";
            tr += "<td class=\"hidden\">" + (data.prop_2_detail_name==null?"&nbsp;":data.prop_2_detail_name) + "</td>";
            tr += "<td class=\"hidden\">" + (data.prop_3_detail_name==null?"&nbsp;":data.prop_3_detail_name) + "</td>";
            tr += "<td class=\"hidden\">" + (data.prop_4_detail_name==null?"&nbsp;":data.prop_4_detail_name) + "</td>";
            tr += "<td class=\"hidden\">" + (data.prop_5_detail_name==null?"&nbsp;":data.prop_5_detail_name) + "</td>";
            tr += "<td><input sku_input='price' maxlength=\"15\" type=\"text\" class=\"addInput0\"/></td>";
            tr += "<td><a href=\"javascript:void(0);\" onclick=\"addSkuPost(this);\" >添加</a></td>";
            tr += "</tr>";
            $("#skuPropList").append(tr);
            getTableShowOrHide("skuPropList","title2");
            var cells = $("#skuPropList tbody tr").last().find("td");
            $("#skuPropList th").each(function (index, item) {
                $(cells[parseInt($(item).attr("columnIndex")) - 1]).removeClass("hidden").attr("columnIndex", $(item).attr("columnIndex"));
            });
        }
        //验证数据价格是否合法
        function validataPrice(value) {
            if(!priceReg.test(value)){
                this.infobox.showPop("价格数据格式不正确!");
                 $("#<%=tbPrice.ClientID %>").focus();
                return false;
            }
            return true;
        }
        function addSkuPost(sender) {
            var index = parseInt($("#skuList tbody tr").last().attr("index") == null ? "0" : $("#skuList tbody tr").last().attr("index")) + 1;
            var parent = $(sender).parent().parent();
            var input = parent.find("[sku_input=price]");
            if (!validataPrice(input.val())) {
                input.focus();
                return;
            }
            var data = getSkuInfo(parent, input,index);
            if(checkSkuExists(data)){
                this.infobox.showPop("sku信息已存在！");
                return;
            }
            adjustSkuListData.push(data);
            buildAdjustSkuTable();
        }
        function checkSkuExists(data) {
            var flag = false;
            $(adjustSkuListData).each(function () {
                if (data.item_name == this.item_name & 
                    data.prop_1_detail_name == (this.prop_1_detail_name == null ? undefined : this.prop_1_detail_name) &
                    data.prop_2_detail_name == (this.prop_2_detail_name == null ? undefined : this.prop_2_detail_name) &
                    data.prop_3_detail_name == (this.prop_3_detail_name == null ? undefined : this.prop_3_detail_name) &
                    data.prop_4_detail_name == (this.prop_4_detail_name == null ? undefined : this.prop_4_detail_name) &
                    data.prop_5_detail_name == (this.prop_5_detail_name == null ? undefined : this.prop_5_detail_name)
                ) {
                    flag = true;
                    return false;
                }
            });
            return flag;
        }
        //添加skuInfo
        function getSkuInfo(parent,input,index) {
            var para = "{";
            para += "\"item_name\":\"" + $("#<%=selSkuItem.ClientID %>").val() + "\",";
            para += "\"order_detail_sku_id\":\"\",";
            para += "\"order_id\":\"<%=this.Request.QueryString["order_id"]??"" %>\",";
            para += "\"price\":\"" + parseFloat(input.val()).toFixed(4) + "\",";
            parent.find("[columnIndex]").each(function () {
                para += "\"prop_" + $(this).attr("columnIndex") + "_detail_name\":\"" + $(this).text() + "\",";
            });
            para += "\"sku_id\":\"" + parent.attr("sku_id") + "\",";
            para += "\"index\":\"" + index + "\"";
            para += "}";
            return JSON.parse(para);
        }
        function buildAdjustSkuTable(){
            $("#skuList tbody tr").remove();
            for(var i=0;i<adjustSkuListData.length;i++){
                buildAdjustSkuTr(adjustSkuListData[i],i+1);
            }
        }
        //创建一行调价单信息
        function buildAdjustSkuTr(data, index) {
            var length = $("#skuList tbody tr").length;
            var _class="b_c5";
            if(length%2==0){
                _class="b_c4";
            }
            var show=""
            if('<%=ViewState["strDo"] %>'=='Visible'){
                show = "none";
            }
            var tr = "<tr class=\""+_class+"\" index='"+index+"'>";
            tr += "<td>" + data.item_name + "</td>";
            tr += "<td class='hidden'>" + (data.prop_1_detail_name==null?"&nbsp;":data.prop_1_detail_name) + "</td>";
            tr += "<td class='hidden'>" + (data.prop_2_detail_name==null?"&nbsp;":data.prop_2_detail_name) + "</td>";
            tr += "<td class='hidden'>" + (data.prop_3_detail_name==null?"&nbsp;":data.prop_3_detail_name) + "</td>";
            tr += "<td class='hidden'>" + (data.prop_4_detail_name==null?"&nbsp;":data.prop_4_detail_name) + "</td>";
            tr += "<td class='hidden'>" + (data.prop_5_detail_name==null?"&nbsp;":data.prop_5_detail_name) + "</td>";
            tr += "<td>" + data.price + "</td>";
            tr += "<td><a href=\"javascript:void(0)\" onclick=\"go_Del(this,'sku')\" style=\"display:"+show+"\"><img src=\"../img/delete.png\" alt='' title=\"删除\"/></a></td>";
            $("#skuList").append(tr);
            getTableShowOrHide("skuList","title3");
            var tds = $("#skuList tbody tr").last().find("td");
            $("#skuPropList th").each(function (index, item) {
                $(tds[$(item).attr("columnIndex")]).removeClass("hidden").attr("columnIndex", $(item).attr("columnIndex"));
            });
        }
        //从数据源中删除sku记录
        function deleteAdjustSkuTr(sender) {
            var index = $(sender).parent().parent().attr("index");
            $(sender).parent().parent().remove();
            getTableShowOrHide("skuList","title3");
            var order_detail_sku_id = $(sender).parent().parent().attr("order_detail_sku_id");
            $(adjustSkuListData).each(function(){
                if(this.index){
                    if(this.index==index){
                        adjustSkuListData.removeValue(this);
                        return false;
                    }
                }else{
                    if(this.order_detail_sku_id==order_detail_sku_id){
                        adjustSkuListData.removeValue(this);
                        return false;
                    }
                }
            });
        }
        function checkData(){
          //todo:此处添加数据验证逻辑
            //调价结束日期大于调价开始日期
            //调价开始日期大于系统日期,大于单据日期
            var beginDate = $("#<%=selBeginDate.ClientID %>").val();
            var endDate = $("#<%=selEndDate.ClientID %>").val();
            var orderDate = $("#<%=tbOrderDate.ClientID %>").val();
              var orderNo = $("#<%=tbOrderNo.ClientID %>").val();
            if(orderNo==""){
                this.infobox.showPop("调价单单号不能为空!");
               setTimeout(function(){ $("#<%=tbOrderNo.ClientID %>").focus();},100);
                $("#dvBsInfo").click();
                return false;
            }
            if(orderDate==""){
                alert("单据日期不能为空!");
                $("#dvBsInfo").click();
                setTimeout(function(){$("#<%=tbOrderDate.ClientID %>").focus();},100);
                return false;
            }
            if(beginDate==""){
                alert("调价开始日期不能为空!");
                 $("#dvBsInfo").click();
                setTimeout(function(){$("#<%=selBeginDate.ClientID %>").focus();},100);
                return false;
            }
            if(endDate==""){
                alert("调价结束日期不能为空!");
                 $("#dvBsInfo").click();
                 setTimeout(function(){$("#<%=selEndDate.ClientID %>").focus();},100);
                 return false;
            }
            if(!Date.prototype.isFirBigThanSec(endDate,beginDate)){
                this.infobox.showPop("调价结束日期必须大于调价开始日期!");
                $("#dvBsInfo").click();
                setTimeout(function(){$("#<%=selBeginDate.ClientID %>").focus();},100);
                return false;
            }
            if(!Date.prototype.isBigThanCurrentDate(beginDate)||!Date.prototype.isFirBigThanSec(beginDate,orderDate)){
                this.infobox.showPop("调价开始日期必须大于系统日期,大于单据日期!");
                $("#dvBsInfo").click();
                setTimeout(function(){$("#<%=selBeginDate.ClientID %>").focus();},100);
                return false;
            }
            //调价单单号不能为空
          
            if(adjustPriceData.length==0&&adjustSkuListData.length==0){
                $("#_item_info").click();
                this.infobox.showPop("商品信息与sku信息必须至少有一个!");
                return false;
            }
            if($("#<%=unitTree.ClientID %> :checkbox[checked=true]").length==0){
                $("#unitinfo").click();
                this.infobox.showPop("必须选择至少一项组织信息!");
                return false;
            }
              $("#<%=hidItemPrice.ClientID %>").val(JSON.stringify(adjustPriceData));
            $("#<%=hidItemSku.ClientID %>").val(JSON.stringify(adjustSkuListData));
            return true;
        }
         $(function(){
            if(<%=btnSave.Visible?"false":"true" %>)
            {
                disableCtrls("#<%=tabContainer1.ClientID %>");
            }
        });

        $(function () {
            $("#<%=unitTree.ClientID %> :checkbox").click(
            function () {
                if ($(this).is(":checked")) {
                    $(this).parentsUntil("#org_div").filter("div").prev("table").find(":checkbox").attr("checked", "checked");
                }else{
                    unsel_notice(this);
                } 
                 function unsel_notice(chkbox)
                    {
                        if(!$(chkbox).closest("table").siblings().find(":checkbox").is(":checked"))
                        {
                               var $check = $(chkbox).closest("div").prev("table").find(":checkbox"); 
                               if($check.length>0 && $("div.tree-container").has($check).length>0)
                               {
                                    $check.removeAttr("checked");
                                    unsel_notice($check);
                               }
                        } 
                    }
            }
        );
            buildPriceTable();
            buildAdjustSkuTable();
            getTableShowOrHide("tbItemPriceInfo","title1");
            getTableShowOrHide("skuPropList","title2");
            getTableShowOrHide("skuList","title3");
        }); 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <cc1:TabContainer runat="server" ID="tabContainer1">
    <cc1:TabPanel runat="server" ID="basicInfo">
        <HeaderTemplate>
            <div class="tab_head">
                    <span style="font-size: 12px;" id="dvBsInfo">基础信息</span></div>
        </HeaderTemplate>
        <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" class="con_tab">
                            <tr>
                                <td  class="td_co" >
                                    调价单单号：
                                </td>
                                <td>
                                    <div class="box_r"></div>
                                    <asp:TextBox ID="tbOrderNo" MaxLength="30" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td  class="td_co" >
                                    单据日期：
                                </td>
                                <td>
                                    <div class="box_r"></div>
                                    <asp:TextBox ID="tbOrderDate"   onclick="Calendar('MainContent_tabContainer1_basicInfo_tbOrderDate');" title="双击清除日期" MaxLength="10" ondblclick="this.value='';" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td  class="td_co" >
                                    价格类型：
                                </td>
                                <td class="td_lp">
                                    <asp:DropDownList runat="server" ID="selItemPriceType">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td  class="td_co" >
                                    调价开始日期：
                                </td>
                                <td>
                                    <div class="box_r"></div>
                                    <asp:TextBox ID="selBeginDate"  onclick="Calendar('MainContent_tabContainer1_basicInfo_selBeginDate');" title="双击清除日期" ondblclick="this.value='';" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td  class="td_co" >
                                    调价结束日期：
                                </td>
                                <td>
                                    <div class="box_r"></div>
                                    <asp:TextBox ID="selEndDate"  onclick="Calendar('MainContent_tabContainer1_basicInfo_selEndDate');" title="双击清除日期" ondblclick="this.value='';" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td  class="td_co" >
                                    备注：
                                </td>
                                <td class="td_lp">
                                    <asp:TextBox ID="tbRemark" Width="431px" Height="50px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
        </ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel runat="server" ID="itemInfo">
        <HeaderTemplate>
            <div class="tab_head">
                    <span style="font-size: 12px;" id="_item_info">商品信息</span></div>
        </HeaderTemplate>
        <ContentTemplate>
            <div style="background-color: #f2f1ef;">
                <div class="tit_con">
                    <span>商品信息</span>
                </div>
                <table cellpadding="0" cellspacing="0" border="0" class="con_tab">
                    <tr>
                        <td  class="td_co" >
                            商品：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <asp:TextBox   runat="server" ID="selItem"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co" >
                            价格：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <asp:TextBox runat="server" MaxLength="15" ID="tbPrice"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="b_bg" style="text-align:left; padding-left:345px">
                            <input type="button" id="btnAddItem" <%=ViewState["strDo"].ToString()=="Visible"?"disabled":"" %>
                                onclick="addjustPriceData();" value="添加" class="input_c"/>
                        </td>
                    </tr>
                </table>
                <div class="tit_con" id="title1">
                    <span>商品价格列表</span>
                </div>
                <div class="ss_bg">
                    <div style="width:auto; height:auto; overflow-y:auto;">
                        <table id="tbItemPriceInfo" cellpadding="0" cellspacing="1" border="0" class="ss_jg" style="width:500px">
                                <tr class="b_c3">
                                    <th width="200" scope="col" align="center">
                                        名称-号码
                                    </th>
                                    <th width="90" scope="col" align="center">
                                        价格
                                    </td>
                                    <th width="60" scope="col" align="center">
                                        操作
                                    </td>
                                </tr>
                            </table>
                            </div>
                </div>
            </div>
        </ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel ID="skuInfo" runat="server">
        <HeaderTemplate>
            <div class="tab_head">
                    <span style="font-size: 12px;">SKU信息</span></div>
        </HeaderTemplate>
        <ContentTemplate>
            <div style="background-color: #f2f1ef;">
            <div class="tit_con">
                <span>商品信息</span>
            </div>
                    <table cellpadding="0" cellspacing="0" class="con_tab">
                    <tr>
                        <td  class="td_co" >
                            商品：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <asp:TextBox runat="server" ID="selSkuItem"></asp:TextBox>
                        </td>
                    </tr>
                </table>
             <div class="tit_con" id="title2">
                <span>商品列表</span>
             </div>
            <div class="ss_bg">
                <div style="width:auto; height:auto; overflow-y:auto;">
                    <table id="skuPropList" style="width:500px" cellpadding="0" cellspacing="1" border="0" class="ss_jg">
                                <thead>
                                    <tr class="b_c3">
                                        <% for (int i = 1; i <= 5; i++)
                                           {%>
                                        <%
                                            if (this.SkuProInfos != null && this.SkuProInfos.Count != 0)
                                            {
                                                var item = this.SkuProInfos.FirstOrDefault(obj => obj.display_index == i);
                                                if (item != null)
                                                {
                                        %>
                                        <th scope="col" align="center" width="100" columnindex="<%= item.display_index %>">
                                            <%=item.prop_name%>
                                        </th>
                                        <%}
                            else
                            {%>
                                        <th scope="col" align="center" class="hidden">
                                        </th>
                                        <%} %>
                                        <%}
                       } %>
                                        <th scope="col" align="center" width="100">
                                            价格
                                        </th>
                                        <th scope="col" align="center" width="100">
                                            操作
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                            </div>
            </div>
            <div class="tit_con" id="title3">
                <span>商品SKU价格列表</span>
            </div>
              <div class="ss_bg">
                <div style="width:auto; height:auto; overflow-y:auto;">
                    <table id="skuList" style="width:500px" cellpadding="0" cellspacing="1" border="0" class="ss_jg">
                                <thead>
                                    <tr class="b_c3">
                                        <th scope="col" align="center" width="100">
                                            商品名称
                                        </th>
                                        <% for (int i = 1; i <= 5; i++)
                                           {%>
                                        <%
                                            if (this.SkuProInfos != null && this.SkuProInfos.Count != 0)
                                            {
                                                var item = this.SkuProInfos.FirstOrDefault(obj => obj.display_index == i);
                                                if (item != null)
                                                {
                                        %>
                                        <th width="100" scope="col" align="center">
                                            <%=item.prop_name %>
                                        </th>
                                        <%}
                          else
                          {%>
                                        <td class="hidden">
                                        </td>
                                        <%} %>
                                        <%}
                           }%>
                                        <th scope="col" align="center" width="100">
                                            价格
                                        </th>
                                        <th scope="col" align="center" width="100">
                                            操作
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                </div>
              </div>
            </div>
        </ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel ID="unitInfo" runat="server">
        <HeaderTemplate>
            <div class="tab_head">
                    <span style="font-size: 12px;" id="unitinfo">组织信息</span></div>
        </HeaderTemplate>
        <ContentTemplate>
        <div style="background-color: #f2f1ef;">
        <div class="tit_con"><span>组织信息</span></div>
        <table cellpadding="0" cellspacing="0" border="0" class="con_tab">
            <tr>
                <td class="td_co" valign="top">
                    组织信息：
                </td>
                <td style="vertical-align: top; line-height:0px; ">
                    <div style="width:auto; height:500px; overflow-y:scroll;">
                    <asp:TreeView ID="unitTree" runat="server" Style="width: auto" SelectedNodeStyle-BackColor="Blue"
                        SelectedNodeStyle-ForeColor="White">
                    </asp:TreeView>
                    </div>
                </td>
            </tr>
        </table>
        </div>
            
        </ContentTemplate>
    </cc1:TabPanel>
</cc1:TabContainer>
<div class="bf">
    <asp:Button  runat="server" ID="btnSave" CssClass=" input_bc" Text="保存" OnClientClick="if(!checkData()) return false;" onclick="btnSave_Click"/>&nbsp;&nbsp;<asp:Button 
        runat="server" ID="btnCancle" Text="返回" CssClass=" input_fh" onclick="btnCancle_Click"/>
</div>
<input type="hidden" runat="server" id="hidItemPrice" value="[]"/>
<input type="hidden" runat="server" id="hidItemSku" value="[]"/>
</asp:Content>
