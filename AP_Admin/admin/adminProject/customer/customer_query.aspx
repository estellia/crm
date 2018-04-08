<%@ Page Title="客户查询" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="customer_customer_query" Codebehind="customer_query.aspx.cs" %>

<%@ Register Src="../Controls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
       function selectAll(sender) {
           if ($(sender).attr("checked")) {
               $("input:checkbox").each(function () { $(this).attr("checked", "checked"); });
           }
           else {
               $("input:checkbox").each(function () { $(this).attr("checked",""); });
           }
       }
       //获取所有选择项的ID
       function getSelectedIds() {
           var arry = [];
           $("input:checked").each(function () {
               if ($(this).attr("id") != "power") {
                   arry.push($(this).parent().attr("customerId"));
               }
           });
           return arry.join(",");
       }
       //启用
       function enable_use() {
           if (show_bill_action('customer', 0, 6, 1, getSelectedIds())) {
               $("#<%=btnRefresh.ClientID %>")[0].click();
               //this.location.href = document.getElementById("_from").value;
           }
       }
       //停用 
       function stop_use() {
           if (show_bill_action('customer', 0, 6, 2, getSelectedIds())) {
               $("#<%=btnRefresh.ClientID %>")[0].click();
               //this.location.href = document.getElementById("_from").value;
           }
        }
        //新建
        function go_Create() {
            window.location.href = "customer_show.aspx?oper_type=2&from=" + escape(document.getElementById("_from").value);
            //location.href = "customer_show.aspx?oper_type=2&from=" + document.getElementById("_from").value;
        }
   </script>
   <script type="text/javascript">
       function check() {
           var boxs = $(".select").find("input:checkbox").length;
           var checkedboxs = $(".select").find("input:checked").length;
           if (boxs == checkedboxs) {
               $("#power").attr("checked", "checked");
           }
           else {
               $("#power").removeAttr("checked");
           }
       }
   </script>
   <script type="text/javascript">
       function CheckSelected(sender) {
           var checkboxs = $("input:checked");
           if (checkboxs.length == 0) {
               alert("必须选择至少一个客户");
               return false
           }
           return true;
       }
       fnShowLoading = function(msg) {
           var lblInit = document.getElementById("lblInit");
           var lblClose = document.getElementById("lblClose");
           lblInit.style.display = "";
           lblClose.style.display = "";
           lblInit.innerHTML = "正在初始化，可能需要几分钟...";
           return true;
       }
       fnHideLoading = function(msg) {
           if (msg == "") msg = "初始化完成";
           var lblInit = document.getElementById("lblInit");
           var lblClose = document.getElementById("lblClose");
           lblInit.innerHTML = msg;
           lblInit.style.display = "";
           lblClose.style.display = "";
       }
       fnClose = function(msg) {
           var lblInit = document.getElementById("lblInit");
           var lblClose = document.getElementById("lblClose");
           lblInit.style.display = "none";
           lblClose.style.display = "none";
       }
   </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="lblInit" style="position:absolute; left:300px; top: 120px; padding:4px; background:#eee; border:1px solid #333; height:50px; width: 300px; line-height:50px; display:none; "></div>
    <div id="lblClose" style="position:absolute; left:580px; top: 125px; height:20px; width:30px; color:#333; cursor:pointer;display:none;" onclick="fnClose()">关闭</div>
    <div class="tit_con">
        <span>搜索条件</span></div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <td class="tit_c">
                编码:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbCode"  runat="server" MaxLength="30" ></asp:TextBox>
            </td>
            <td   class="tit_c">
                名称:
            </td>
            <td class="td_lp" >
                <asp:TextBox ID="tbName" runat="server"  MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
                联系人:
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbContacter" runat="server"  MaxLength="25" ></asp:TextBox>
            </td>
            <td class="tit_c">
                状态:
            </td>
            <td class="td_lp" >
                <asp:DropDownList ID="cbStatus" runat="server" Width="196px" >
                    <asp:ListItem Value="0">全部</asp:ListItem>
                    <asp:ListItem Value="1">正常</asp:ListItem>
                    <asp:ListItem Value="-1">停用</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
           <tr id="tr_unit" runat="server">
            <td  class="tit_c">
                运营商:
            </td>
            <td class="td_lp">
                <asp:DropDownList ID="ddl_Unit" runat="server" Width="196px" >
                   
                </asp:DropDownList>
            </td>
            <td class="tit_c">
               
            </td>
            <td class="td_lp" >
              
            </td>
        </tr>
    </table>
    <div class="b_bg">
        <div style="display:none">
            <asp:Button ID="btnRefresh" CssClass="input_c" Text="刷新" runat="server" OnClick="btnRefresh_Click" />
        </div>
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" /> 
        <input type="button" runat="server" id="btnCreate" value="新建" onclick="go_Create();" class="input_c" />
        <input type="button" id="btnDisable" class="input_c" value="停用"  onclick="if(!CheckSelected(this)){ return false;} stop_use()" />
        <input type="button" id="btnEnable" class="input_c" value="启用"  onclick="if(!CheckSelected(this)){ return false;} enable_use()" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span>
    </div>
    <div class="ss_bg">
        <asp:GridView ID="gvCustomer" runat="server" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
            AutoGenerateColumns="False" DataSourceID="odsCustomer" AllowPaging="True" DataKeyNames="ID"
            OnRowCommand="gvCustomer_RowCommand" GridLines="None" ShowHeaderWhenEmpty="True">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="gv-checkbox" HeaderText="<input type='checkbox' ID='power' onclick='selectAll(this);'/>">
                    <ItemTemplate>
                        <asp:CheckBox ID="select" CssClass="select" onclick="check()"  runat="server" customerId='<%#Eval("ID") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编码"  ItemStyle-CssClass="gv-code" ItemStyle-Width="120px">
                    <ItemTemplate >
                        <a href='customer_show.aspx?oper_type=1&customer_id=<%#Eval("ID") %>'>
                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Code") %>' /></a>
                    </ItemTemplate>
                    <ItemStyle  HorizontalAlign="Center"/>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="客户标识" />
                <asp:BoundField DataField="Name" HeaderText="客户名称" />
                 <asp:BoundField DataField="unit_name" HeaderText="运营商名称" />
                <asp:BoundField DataField="Address" HeaderText="地址" />
                <asp:BoundField DataField="Contacter" HeaderText="联系人" ItemStyle-CssClass="gv-person" ItemStyle-Width="100px"/>
                <asp:BoundField DataField="Tel" ItemStyle-HorizontalAlign="Center" HeaderText="电话" ItemStyle-CssClass="gv-tel"/>
                <asp:BoundField DataField="Units_zh" ItemStyle-HorizontalAlign="Center" HeaderText="门店数" ItemStyle-Width="50px"/>
                <asp:BoundField DataField="StatusDescription" HeaderText="状态" ItemStyle-CssClass="gv-status" />
                <asp:BoundField DataField="IsALD" HeaderText="是否同步到阿拉丁" ItemStyle-CssClass="gv-status" />
                <asp:TemplateField ItemStyle-Width="150px" HeaderText="操作" ItemStyle-CssClass="gv-5icon">
                    <ItemTemplate>
                        <a href='../customer/customer_show.aspx?oper_type=3&customer_id=<%# Eval("ID")%>'>
                            <img src="../img/edit.png" alt="" title="编辑" />
                        </a>
                        &nbsp;
                        <a style="display:none;" href='../customer/customer_show.aspx?oper_type=3&customer_id=<%# Eval("ID")%>'>
                            <asp:ImageButton ID="imgSubmit" runat="server" ImageUrl="../img/exchange.png" alt=""
                                CommandArgument='<%#Eval("ID") %>' CommandName="Submit" AlternateText="提交" title="提交" />
                        </a>
                        <a href='../customer/shop_query.aspx?customer_id=<%# Eval("ID")%>&customer_name=<%#Eval("Name") %>'>
                            <img src="../img/shop.png" alt="" title="门店" />
                        </a>
                        <a style="display:none;" href='../customer/terminal_query.aspx?customer_id=<%# Eval("ID")%>&customer_name=<%#Eval("Name") %>'>
                            <img src="../img/terminal.png" alt="" title="终端" />
                        </a>
                        <a style="display:none;" href='../customer/user_query.aspx?customer_id=<%# Eval("ID")%>&customer_name=<%#Eval("Name") %>'>
                            <img src="../img/user.png" alt="" title="用户" />
                        </a>
                        &nbsp;
                        <a href='../customer/customer_show.aspx?oper_type=3&customer_id=<%# Eval("ID")%>'>
                            <asp:ImageButton ID="imgSubmitInit" runat="server" ImageUrl="../img/exchange.png" alt="" OnClientClick="fnShowLoading(this);"
                                CommandArgument='<%#Eval("ID") %>' CommandName="SubmitInit" AlternateText="客户初始化" title="客户初始化" />
                        </a>
                        <%-- href='http://dev.o2omarketing.cn:9004/External/PageConfig/applyToCustomer.aspx?customerId=<%# Eval("ID")%>' --%>
                        <a
                        href='<%=CustomerVocationUrl+"?customerId="%><%# Eval("ID")%>'
                        target="_blank">
                            <img title="设置行业版本" src="../img/approve.png" alt="设置行业版本" />      
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
                <uc1:Pager ID="Pager1" runat="server" RecordCount="0" />
            </PagerTemplate>
            <PagerStyle CssClass="pag" />
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="odsCustomer" runat="server" EnablePaging="True" MaximumRowsParameterName="maxRowCount"
        OnObjectCreating="odsCustomer_ObjectCreating" OnSelecting="odsCustomer_Selecting"
        SelectCountMethod="SelectCustomerListCount" SelectMethod="SelectCustomerList"
        StartRowIndexParameterName="startRowIndex" TypeName="cPos.Admin.Service.Interfaces.ICustomerService">
    </asp:ObjectDataSource>
</asp:Content>
