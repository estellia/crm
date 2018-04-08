<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="item_show.aspx.cs" Inherits="item_item_show" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .hidden
        {
            display:none;
        }
        #tbTableSku td{ text-align:center;}
        #tbTablePrice td{ text-align:center;}
    </style>
    <script type="text/javascript">
        var itemPriceData = <%= this.hidItemPrice.Value %>; //priceData
        var itemPropData = <%= this.hidItemProp.Value %>;
        var itemSkuData = <%= this.hidItemSku.Value %>; //skuData 
    </script>
    <script type="text/javascript">
        //删除
        function go_Del(sender, type) {
            $(sender).parent().parent().remove();
            switch (type) {
                case "itemPrice": {getTableShowOrHide("tbTablePrice","title1"); deleteItemPrice($(sender).parent().parent().attr("item_price_type_id")); } break;
                case "itemSku": { getTableShowOrHide("tbTableSku","title2");deleteSku(sender); } break;
                default: break;
            }
        }
        //添加价格信息
        function addItemPrice(){
            var itemPriceType = $("#<%=selItemPriceType.ClientID %>");
            var itemPriceTypeText = $("#<%=selItemPriceType.ClientID %>").find("option:selected").text();
            var itemPrice = $("#<%=tbPrice.ClientID %>");
            if (!validatePrice(itemPrice.val())) {
                this.infobox.showPop("价格数据格式不正确!");
                itemPrice.focus();
                return;
            }
            for (var i = 0; i < itemPriceData.length; i++) {
                if (itemPriceType.val() == itemPriceData[i].item_price_type_id) {
                    this.infobox.showPop("" + itemPriceTypeText + " 价格信息已存在!");
                    return;
                }
            }
            var para = {};
            para.item_price_id = null;
            para.item_price_type_id = itemPriceType.val();
            para.item_price_type_name = itemPriceTypeText;
            para.item_price = itemPrice.val();
            itemPriceData.push(para);
            buildPriceTable();
        }
        function buildPriceTable(){
            $("#tbTablePrice tr[item_price_type_id]").remove();
            for(var i=0;i<itemPriceData.length;i++){
                buildPriceTr(itemPriceData[i]);
            }
        }
        function buildPriceTr(data) {
            var length = $("#tbTablePrice tr").length;
            var _class = "";
            if (length % 2 == 0) {
                _class = "b_c5";
            } else {
                _class = "b_c4";
            }
            var show = "";
            if('<%=ViewState["action"] %>'=='Visible'){
                show = "none";
            }
            var tr = "<tr class=\""+_class+"\" item_price_type_id=\"" + data.item_price_type_id + "\"><td>" + data.item_price_type_name + "</td><td>" + parseFloat(data.item_price).toFixed(4) + "</td><td><a href=\"#\" onclick=\"go_Del(this,'itemPrice');\" style=\"display:"+show+"\"><img src=\"../img/delete.png\" alt=\"\" title=\"删除\" /></a></td></tr>";
            $("#tbTablePrice").append(tr);
            getTableShowOrHide("tbTablePrice","title1");
        }
        //验证输入的价格数据是否合法
        function validatePrice(price) {
            var priceReg = /^\-?\d{1,10}$|^\-?\d{1,10}\.?\d{1,4}$/;
            if (!priceReg.test(price)) {
                return false;
            }
            return true;
        }
        //删除价格信息
        function deleteItemPrice(data) {
            for (var i = 0; i < itemPriceData.length; i++) {
                if (itemPriceData[i].item_price_type_id == data) {
                    itemPriceData.removeValue(itemPriceData[i]);
                }
            }
        }
        var test = "[ ,\\`,\\~,\\!,\\@,\#,\\$,\\%,\\^,\\+,\\*,\\&,\\\\,\\/,\\?,\\|,\\:,\\.,\\<,\\>,\\{,\\},\\(,\\),\\',\\;,\\=,\"]";
            var reg_is_char = new RegExp(test);
            var reg_is_cn = /^[\u0391-\uFFE5]+$/;
            //英文
            var reg_is_en = /^[\s,a-z,A-Z]*$/;
        //添加sku信息
        function addSkuPost() {
            if(reg_is_char.test($("#<%=tbBarcode.ClientID %>").val())|reg_is_cn.test($("#<%=tbBarcode.ClientID %>").val())){
                infobox.showPop("barcode不允许输入汉字或特殊字符!");
                $("#<%=tbBarcode.ClientID %>").focus();
                return;
            }
            var index = parseInt($("#tbTableSku tbody tr").last().attr("index") == null ? "0" : $("#tbTableSku tbody tr").last().attr("index")) + 1;
            var skuData = getSkuInfo(index);
            if(!skuData){
                return;
            }
            if(checkBarcodeExists(skuData)){
                alert("Barcode已存在!");
                $("#<%=tbBarcode.ClientID %>").focus();
                return;
            }
            if (checkSkuExists(skuData)) {
                alert("Sku信息已存在！");
                return;
            }
            if (!(skuData == undefined || skuData == null)) {
                itemSkuData.push(skuData);
                buildSkuTable();
            }
        }
        //添加Sku列表
        function buildSkuTable(){
            $("#tbTableSku tr[index]").remove();
            for(var i=0;i<itemSkuData.length;i++){
                addSkuTr(itemSkuData[i],i+1);
            }
        }
        function addSkuTr(data,index) {
            var length = $("#tbTableSku tr").length;
            var _class = "";
            if (index % 2 == 0) {
                _class = "b_c4";
            } else {
                _class = "b_c5";
            }
            var show = "";
            if('<%=ViewState["action"] %>'=='Visible'){
                show = "none";
            }
            var skuTr = "<tr class=\""+_class+"\" sku_id=\""+data.sku_id+"\" index='" + index + "'>";
            skuTr += "<td class='hidden'>"+(data.prop_1_detail_name==null?"&nbsp;":data.prop_1_detail_name)+"</td>";
            skuTr += "<td class='hidden'>"+(data.prop_2_detail_name==null?"&nbsp;":data.prop_2_detail_name)+"</td>";
            skuTr += "<td class='hidden'>"+(data.prop_3_detail_name==null?"&nbsp;":data.prop_3_detail_name)+"</td>";
            skuTr += "<td class='hidden'>"+(data.prop_4_detail_name==null?"&nbsp;":data.prop_4_detail_name)+"</td>";
            skuTr += "<td class='hidden'>"+(data.prop_5_detail_name==null?"&nbsp;":data.prop_5_detail_name)+"</td>";
            skuTr += "<td>" + data.barcode + "</td>";
            skuTr += "<td><a href=\"javascript:void(0);\" onclick=\"go_Del(this,'itemSku');\" style=\"display:"+show+"\"><img title='删除' alt='' src=\"../img/delete.png\"/></a></td>";
            skuTr += "</tr>";
            $("#tbTableSku").append(skuTr);
            getTableShowOrHide("tbTableSku","title2");
            $(".itemSku").each(function () {
                var display_index = $(this).attr("columnIndex");
                $($("#tbTableSku tr[index=" + index + "]")[0].cells[display_index - 1]).removeClass("hidden");
            });
        }
        //获取Sku对象
        function getSkuInfo(index) {
            var sku_flag = true;
            var para = "{";
            $(".itemSku").each(function () {
                if ($(this).val() == "--" | $(this).val() == undefined|$(this).val()=="") {
                    sku_flag = false;
                    $(this).focus();
                    var input_flag = $(this).attr("input_flag");
                    var prop_name = $(this).attr("prop_name");
                    if (input_flag == "text") {
                        alert(prop_name + "不能为空");
                        return false;
                    } else if (input_flag == "select") {
                        alert("必须选择" + prop_name);
                        return false;
                    }
                }
                var dispaly_index = $(this).attr("columnIndex");
                var prop_id = $(this).attr("id");
                for (var i = 1; i <= 5; i++) {
                    if (i == dispaly_index) {
                        var prop_name ="";
                        if($(this).attr("type")=="text"){
                            prop_name = $(this).val();
                        }else if($(this).attr("type")=="radio"){
                            prop_name= $(this).attr("prop_name");
                        }
                        else{
                            prop_name = $(this).find("option[selected=true]").text();
                        }
                        para += "\"prop_" + i + "_id\":\"" + prop_id + "\",\"prop_" + dispaly_index + "_detail_id\":\"" + $(this).val() + "\",\"prop_"+i+"_detail_name\":\""+prop_name+"\",";
                        break;
                    }
                }
            });
            if (sku_flag) {
                if ($("#<%=tbBarcode.ClientID %>").val() == "") {
                    this.infobox.showPop("barcode不能为空");
                    $("#<%=tbBarcode.ClientID %>").focus();
                    sku_flag = false;
                }
            }
            para += "\"barcode\":\"" + $("#<%=tbBarcode.ClientID %>").val() + "\",";
            para += "\"sku_id\":\"\",";
            para += "\"index\":\"" + index + "\"";
            para += "}";
            return sku_flag ? JSON.parse(para) : null;
        }
        //检查Barcode是否存在
        function checkBarcodeExists(data){
            if(data==null){
                return false;
            }
            var is_Exists = false;
            $(itemSkuData).each(function(){
                if(data.barcode==this.barcode){
                    is_Exists = true;
                    return false;
                }
            });
            return is_Exists;
        }
        //检查Sku是否存在
        function checkSkuExists(data) {
            if(!data){
                return false;
            }
            var is_Exists = false;
            for (var i = 0; i < itemSkuData.length; i++) {
                if (data.prop_1_id == (itemSkuData[i].prop_1_id == null ? undefined : itemSkuData[i].prop_1_id) &
                    data.prop_1_detail_id == (itemSkuData[i].prop_1_detail_id == null ? undefined : itemSkuData[i].prop_1_detail_id) &
                    data.prop_2_id == (itemSkuData[i].prop_2_id == null ? undefined : itemSkuData[i].prop_2_id) &
                    data.prop_2_detail_id == (itemSkuData[i].prop_2_detail_id == null ? undefined : itemSkuData[i].prop_2_detail_id) &
                    data.prop_3_id == (itemSkuData[i].prop_3_id == null ? undefined : itemSkuData[i].prop_3_id) &
                    data.prop_3_detail_id == (itemSkuData[i].prop_3_detail_id == null ? undefined : itemSkuData[i].prop_3_detail_id) &
                    data.prop_4_id == (itemSkuData[i].prop_4_id == null ? undefined : itemSkuData[i].prop_4_id) &
                    data.prop_4_detail_id == (itemSkuData[i].prop_4_detail_id == null ? undefined : itemSkuData[i].prop_4_detail_id) & data.prop_5_id == (itemSkuData[i].prop_5_id == null ? undefined : itemSkuData[i].prop_5_id) &
                    data.prop_5_detail_id == (itemSkuData[i].prop_5_detail_id == null ? undefined : itemSkuData[i].prop_5_detail_id)) {
                    is_Exists = true;
                    break;
                }
            }
            return is_Exists;
        }
        //删除sku信息
        function deleteSku(sender) {
            var parentId = $(sender).parent().parent();
            var index = parentId.attr("index");
            var sku_id = parentId.attr("sku_id");
            $(itemSkuData).each(function(){
                if(this.index){
                    if(index==this.index){
                        itemSkuData.removeValue(this);
                        return false;
                    }
                }else{
                    if(sku_id==this.sku_id){
                        itemSkuData.removeValue(this);
                        return false;
                    }
                }
            });
        }
        //获取价格列表
        function getItemPrice() {
            return JSON.stringify(itemPriceData);
        }
        //获取Sku列表
        function getItemSku() {
            return JSON.stringify(itemSkuData);
        }
        //获得属性列表
        function getItemProp() {
            var item_prop_data = savePropData("ITEM");
            return JSON.stringify(item_prop_data);
        }
        //加载属性数据
        window.onload = function () {
            if('<%=!IsPostBack %>'=='True'){
                loadPropData(itemPropData, "ITEM");
            }
        }
        function checkData() {

            var itemcode = $("#<%=tbItemCode.ClientID %>");
            var itemname = $("#<%=tbItemName.ClientID %>");
            var Pyzjm = $("#<%=tbPyzjm.ClientID %>");
            var ItemCategory = $("#<%=selItemCategory.ClientID %>")[0];
            var ItemPriceType = $("#<%=selItemPriceType.ClientID %>");
           // var itemPriceTypeText = $("#<%=selItemPriceType.ClientID %>").find("option:selected");
            if (!ItemCategory.values()[0]) {
                this.infobox.showPop("必须选择商品类别!");
                //商品价格单击事件
                $("#basicinfo").click();
                setTimeout(function () { ItemCategory.focus(); }, 100);
                return false;
            }
             var test = "[ ,\\`,\\~,\\!,\\@,\#,\\$,\\%,\\^,\\+,\\*,\\&,\\\\,\\/,\\?,\\|,\\:,\\.,\\<,\\>,\\{,\\},\\(,\\),\\',\\;,\\=,\"]";
            var reg_is_char = new RegExp(test);
            var reg_is_cn = /^[\u0391-\uFFE5]+$/;
           if (itemcode.val() == "") {
                this.infobox.showPop("商品编码不能为空!");
                $("#basicinfo").click();
                setTimeout(function () { itemcode.focus(), 100 });
                return false;
            }
            if(reg_is_char.test(itemcode.val())|reg_is_cn.test(itemcode.val())){
                this.infobox.showPop("商品编码不允许输入中文或特殊字符!");
               $("#basicinfo").click();
                setTimeout(function () { itemcode.focus(), 100 });
                return false;
            }
            if (itemname.val() == "") {
                this.infobox.showPop("商品名称不能为空!");
                $("#basicinfo").click();
                setTimeout(function () { itemname.focus(); }, 100);
                return false;
            }
             var tbItemNameEn = $("#<%=tbItemNameEn.ClientID %>");
             var reg_is_en = /^[\s,a-z,A-Z]*$/;
            if(tbItemNameEn.val()!=""){
                if(!reg_is_en.test(tbItemNameEn.val())){
                    infobox.showPop("英文名请录入英文!");
                    $("#basicinfo").click();
                setTimeout(function () { tbItemNameEn.focus(); }, 100);
                    return false;
                }
            }
            if (Pyzjm.val() == "") {
                this.infobox.showPop("拼音助记码不能为空!");
                $("#basicinfo").click();
                setTimeout(function () { Pyzjm.focus(); }, 100);
                return false;
            }
            if(Pyzjm.val()!=""){
                if(!reg_is_en.test(Pyzjm.val())){
                    alert("拼音助记码请录入拼音!");
                   $("#basicinfo").click();
                setTimeout(function () { Pyzjm.focus(); }, 100);
                    return false;
                }
            }
            if(itemPriceData.length==0){
                infobox.showPop("必须输入至少一项价格信息!");
                $("#price").click();
                return false;
            }
            if(itemSkuData.length==0){
                infobox.showPop("必须输入至少一项sku信息!");
                $("#sku").click();
                return false;
            }
            $("#<%=hidItemPrice.ClientID %>").val(getItemPrice()); //保存商品价格信息
            $("#<%=hidItemProp.ClientID %>").val(getItemProp()); //保存商品属性信息
            $("#<%=hidItemSku.ClientID %>").val(getItemSku()); //保存商品Sku信息
            return true;
        }
         $(function(){
            if(<%=btnSave.Visible?"false":"true" %>)
            {
                disableCtrls("#<%=tabContainer1.ClientID %>");
            }
            buildPriceTable();
            buildSkuTable();
            getTableShowOrHide("tbTablePrice","title1");
            getTableShowOrHide("tbTableSku","title2");
            $("#<%=selItemCategory.ClientID %>")[0].onselect = function (item) {
                return item&&!item.hasChildren;
            }
        });

    </script>
    <style type="text/css">
       .hidden
        {
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <cc1:TabContainer runat="server" ID="tabContainer1">
        <cc1:TabPanel runat="server" ID="tabBaseicInfo">
            <HeaderTemplate>
                <div class="tab_head" id="basicinfo">
                    <span style="font-size: 12px;">基本信息</span></div>
            
</HeaderTemplate>
            
<ContentTemplate>
             <div style="background-color: #f2f1ef;">
                 <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
                     <tr>
                         <td   class="td_co">
                             商品类别：
                         </td>
                         <td>
                             <div class="box_r"></div>
                             <uc1:DropDownTree ID="selItemCategory"  runat="server"  Url="../ajaxhandler/tree_query.ashx?action=category"/>








                         </td>
                     </tr>
                     <tr>
                         <td  class="td_co">
                             商品编码：
                         </td>
                         <td>
                             <div class="box_r"></div>
                             <asp:TextBox  ID="tbItemCode" MaxLength="30" Width="123px" runat="server"></asp:TextBox>








                         </td>
                     </tr>
                     <tr>
                         <td   class="td_co">
                             商品名称：
                         </td>
                         <td>
                             <div class="box_r"></div>
                             <asp:TextBox  ID="tbItemName" MaxLength="25" runat="server"></asp:TextBox>








                         </td>
                     </tr>
                     <tr>
                         <td   class="td_co">
                             英文名：
                         </td>
                         <td class="td_lp">
                             <asp:TextBox ID="tbItemNameEn" MaxLength="50" runat="server"></asp:TextBox>








                         </td>
                     </tr>
                     <tr>
                         <td   class="td_co">
                             简称：
                         </td>
                         <td class="td_lp">
                             <asp:TextBox ID="tbItemNameShort" MaxLength="15" runat="server"></asp:TextBox>








                         </td>
                     </tr>
                     <tr>
                         <td   class="td_co">
                             拼音助记码：
                         </td>
                         <td>
                             <div class="box_r"></div>
                             <asp:TextBox  ID="tbPyzjm" Width="123px" MaxLength="10" runat="server"></asp:TextBox>








                         </td>
                     </tr>
                     <tr>
                        <td class="td_co">是否赠品：</td>
                        <td>
                            <div class="box_r"></div>
                            <asp:DropDownList runat="server" ID="ifgifts" Width="125px"><asp:ListItem Text="是" Value="1"></asp:ListItem>
<asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
</asp:DropDownList>






                        </td>
                     </tr>
                     <tr>
                        <td class="td_co">是否常用商品：</td>
                        <td>
                            <div class="box_r"></div>
                            <asp:DropDownList runat="server" ID="ifoften" Width="125px"><asp:ListItem Text="是" Value="1"></asp:ListItem>
<asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
</asp:DropDownList>






                        </td>
                     </tr>
                     <tr>
                        <td class="td_co">是否服务型商品：</td>
                        <td>
                            <div class="box_r"></div>
                            <asp:DropDownList runat="server" ID="ifservice" Width="125px">
                                <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                     </tr>

                     <tr>
                        <td class="td_co">是否散货：</td>
                        <td>
                            <div class="box_r"></div>
                            <asp:DropDownList runat="server" ID="ddIsGB" Width="125px">
                                <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="否" Value="0" ></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                     </tr>
                     <tr>
                        <td class="td_co">散货排序：</td>
                        
                         <td class="td_lp">
                             <asp:TextBox ID="tbDisplayIndex" MaxLength="5" runat="server"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td   class="td_co">
                             备注：
                         </td>
                         <td class="td_lp">
                             <asp:TextBox ID="tbRemark" Width="431px" Height="50px" TextMode="MultiLine" MaxLength="180" runat="server"></asp:TextBox>

                         </td>
                     </tr>
                 </table>
                 </div>
            
</ContentTemplate>
        







</cc1:TabPanel>
        <cc1:TabPanel runat="server" ID="tabItemProp">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;">商品属性</span></div>
            
</HeaderTemplate>
            







<ContentTemplate>
                <div style="background-color: #f2f1ef;">
                <%--生成商品属性代码--%>
                    <%= PropHelper.PropHelperSingleton.CreationPropGroup("ITEM") %>
                </div>
            
</ContentTemplate>
        







</cc1:TabPanel>
        <cc1:TabPanel runat="server" ID="tabItemPrice">
            <HeaderTemplate>
                <div class="tab_head" id="price">
                    <span style="font-size: 12px;">商品价格</span></div>
            
</HeaderTemplate>
<ContentTemplate>
                <div style="background-color: #f2f1ef;">
                <div class="tit_con">
                    <span>价格操作</span>
                </div>
                    <table cellpadding="0" cellspacing="0" border="0" class="con_tab">
                        <tr>
                            <td  class="td_co" >
                                价格类型：
                            </td>
                            <td>
                                <div class="box_r"></div>
                                <asp:DropDownList  runat="server" ID="selItemPriceType">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td  class="td_co" >
                                价格：
                            </td>
                            <td>
                                <div class="box_r"></div>
                                <asp:TextBox runat="server" MaxLength="15" ID="tbPrice"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                            <td colspan="2" class="b_bg" style="padding-left:345px; text-align:left;">
                                <input type="button" runat="server" id="btnItemPriceAdd" value="添加" class=" input_c" onclick="addItemPrice();" />
                            </td>
                    </table>
                    <div class="tit_con" id="title1">
                        <span>价格列表</span> 
                    </div>
                    <div class="ss_bg">
                    <div style="width:auto; height:auto; overflow-y:auto;">
                        <table id="tbTablePrice" cellpadding="0" cellspacing="1" border="0" class="ss_jg" style="width:500px">
                                    <tr class="b_c3">
                                        <th scope="col" width="100" align="center">价格类型</th>
                                        <th scope="col" width="90" align="center">价格</th>
                                        <th scope="col" width="90" align="center">操作</th>
                                    </tr>
                                </table></div>
                    </div>
                </div>
            
</ContentTemplate>
        







</cc1:TabPanel>
        <cc1:TabPanel runat="server" ID="tabItemSku">
            <HeaderTemplate>
                <div class="tab_head" id="sku">
                    <span style="font-size: 12px;">商品SKU</span></div>
            
</HeaderTemplate>
            







<ContentTemplate>
             <div style="background-color: #f2f1ef;">
             <div class="tit_con">
                <span>SKU操作</span>
             </div>
                        <table cellpadding="0" cellspacing="0" border="0" class=" con_tab">
                           <%=PropHelper.PropHelperSingleton.CreationSkuProp() %>
                            <tr>
                                <td  class="td_co" >
                                    barcode：
                                </td>
                                <td class="td_lp">
                                    <asp:TextBox runat="server"  MaxLength="30" ID="tbBarcode"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="b_bg" colspan="2" style=" padding-left:345px; text-align:left">
                                    <input id="btnSkuAdd" type="button" runat="server" value="添加" class=" input_c" onclick="addSkuPost();" />
                                </td>
                            </tr>
                        </table>
                        <div class="tit_con" id="title2">
                            <span>SKU列表</span>
                        </div>
                 <div class="ss_bg">
                     <div style="width: auto; height: auto; overflow-y: auto;">
                         <table id="tbTableSku" cellpadding="0" cellspacing="1" border="0" class=" ss_jg"
                             style="width: 500px;">
                             <tr class="b_c3">
                                 <% for (int i = 1; i <= 5; i++)
                                    {%>
                                 <%if (this.SkuProInfos != null && this.SkuProInfos.Count != 0)
                                   {
                                       var item = this.SkuProInfos.FirstOrDefault(obj => obj.display_index == i);
                                       if (item != null)
                                       {
                                 %>
                                 <th width="90" scope="col" align='center'>
                                     <%=item.prop_name%>
                                 </th>
                                 <%} %>
                                 <%}
                                                    }%>
                                 <th scope="col" align="center" width="100">
                                     barcode </td>
                                     <td scope="col" align="center" width="100">
                                         操作
                                     </td>
                             </tr>
                         </table>
                     </div>
                 </div>
                </div>
            
</ContentTemplate>
        







</cc1:TabPanel>
    </cc1:TabContainer>
    <div class=" bf">
        <asp:Button runat="server" ID="btnSave" Text="保存" OnClientClick="if(!checkData()) return false;" CssClass=" input_bc"
            OnClick="btnSave_Click" />
        <asp:Button runat="server" ID="btnCancle" Text="返回" CssClass= " input_fh" OnClick="btnCancle_Click" />
    <input type="hidden" runat="server" id="hidItemPrice" value="[]"/>
    <input type="hidden" runat="server" id="hidItemProp"  value="[]"/>
    <input type="hidden" runat="server" id="hidItemSku"  value="[]"/>
    </div>
   
</asp:Content>
