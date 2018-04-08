<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="terminal_query.aspx.cs" Inherits="customer_terminal_query" %>

<%@ Register Src="../Controls/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
    //新建
    function go_Create() {
        this.location.href = "terminal_show.aspx?oper_type=2&customer_id=<%=this.Request.QueryString["customer_id"] %>&from=" + getForm();
    }
    //查看
    function go_View(ct_id){
        this.location.href="terminal_show.aspx?ct_id="+ct_id+"&oper_type=1&from="+getForm();
    }
    //编辑
    function go_Edit(ct_id){
        this.location.href="terminal_show.aspx?ct_id="+ct_id+"&oper_type=3&from="+getForm();
    }
    function getForm() {
        return escape(document.getElementById("_from").value);
    }
    </script>
    <div class="tit_con">
        <span>搜索条件</span>
    </div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <td  class="tit_c" >
                客户编码/名称：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  ID="tbCustomerName"  runat="server" MaxLength="25"></asp:TextBox>
            </td>
            <td class="tit_c"  >
                终端持有方式：
            </td>
            <td class="td_lp" colspan="3">
                <asp:DropDownList  runat="server" ID="cbHoldType">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                终端类型：
            </td>
            <td class="td_lp" colspan="3">
                <asp:DropDownList  runat="server" ID="cbType">
                </asp:DropDownList>
            </td>
            <td class="tit_c" >
                终端编码：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbCode" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                终端序列号：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbSn" MaxLength="25"></asp:TextBox>
            </td>
            <td class="tit_c" >
                终端软件版本：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbSoftwareVersion" MaxLength="25"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                终端购买日期：
            </td>
            <td class="td_lp" align="left">
                <input type="text" runat="server"  readonly="readonly" class="selectdate" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_tbPurchaseDateBegin','MainContent_tbPurchaseDateEnd');"
                    title="双击清除时间" id="tbPurchaseDateBegin" />
            </td>
            <td align="center">
                至
            </td>
            <td class="td_lp" align="right">
                <input type="text" runat="server" class="selectdate" readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_tbPurchaseDateEnd');" title="双击清除时间" id="tbPurchaseDateEnd" />
            </td>
            <td class="tit_c" >
                终端出保日期：
            </td>
            <td class="td_lp" align="left">
                <input type="text" runat="server" class="selectdate" readonly="readonly" onclick="Calendar('MainContent_tbInsuraceDateBegin','MainContent_tbInsuraceDateEnd');"
                    title="双击清除时间" id="tbInsuraceDateBegin"  ondblclick="this.value=''"/>
            </td>
            <td>
                至
            </td>
            <td class="td_lp" align="right">
                <input type="text" runat="server" class="selectdate" readonly="readonly" onclick="Calendar('MainContent_tbInsuraceDateEnd');"
                    title="双击清除时间" id="tbInsuraceDateEnd" ondblclick="this.value=''" />
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto">
        <asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />
        <input type="button" value="新建" runat="server" id="btnCreate" class="input_c" onclick="go_Create();" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span></div>
    <div class="ss_bg" style="padding-right: 14px;">
        <asp:GridView runat="server" AllowPaging="true" DataKeyNames="ID" ID="gvTerminal"
            CellPadding="0" CellSpacing="1" CssClass="ss_jg" AutoGenerateColumns="False"
            GridLines="None" ShowHeaderWhenEmpty="True" DataSourceID="osdTerminal">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField ItemStyle-Width="30px" HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#this.gvTerminal.PageIndex * this.gvTerminal.PageSize + Container.DataItemIndex + 1%></ItemTemplate>
                </asp:TemplateField>                 
                <asp:TemplateField HeaderText="客户编码" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetCustomerCode(Eval("Customer"),"code")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="客户名称" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetCustomerCode(Eval("Customer"),"name") %></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="HoldTypeDescription" ItemStyle-Width="100px" HeaderText="终端持有方式"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="TypeDescription" ItemStyle-Width="100px" HeaderText="终端类型"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Code" ItemStyle-Width="150px" HeaderText="终端编码" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="终端序列号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="javascript:void(0);" onclick="go_View('<%#Eval("ID") %>');">
                            <%#Eval("SN") %></a></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SoftwareVersion" ItemStyle-Width="100px" HeaderText="终端软件版本" />
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a onclick="go_Edit('<%#Eval("ID") %>');" href="javascript:void(0);">
                            <img alt="" src="../img/edit.png" /></a></ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
                <uc1:Pager ID="Pager1" runat="server" RecordCount="0" />
            </PagerTemplate>
            <PagerStyle CssClass="pag" />
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        <asp:ObjectDataSource ID="osdTerminal" runat="server" EnablePaging="True" MaximumRowsParameterName="maxRowCount"
            OnObjectCreating="osdTerminal_ObjectCreating" OnSelecting="osdTerminal_Selecting"
            SelectCountMethod="SelectTerminalListCount" SelectMethod="SelectTerminalList"
            StartRowIndexParameterName="startRowIndex" TypeName="cPos.Admin.Service.Interfaces.ICustomerService">
        </asp:ObjectDataSource>
    </div>
</asp:Content>
