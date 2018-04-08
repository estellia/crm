<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="unit_sale_rpt.aspx.cs" Inherits="report_unit_sale_rpt" %>

<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<%@ Register Src="../controls/DropDownTree.ascx" TagName="DropDownTree" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<style>
    .right{ background:White;}
    #MainContent_tabContainer1_body{ border:0;}
    #MainContent_tabContainer1_detail{ height: auto; min-height:470px;}
    #MainContent_tabContainer1_result{ height:auto; min-height:470px;}
</style>
<script type="text/javascript">
//    window.onbeforeunload = function () {
//        if (event.clientX > document.body.clientWidth && event.clientY < 0 || event.altKey) {
//            $.ajax({
//                url: '../ajaxhandler/ajax_handler_toExecl.ashx',
//                type: 'post',
//                data: { type: "clear" },
//                error: function (data) {
//                    alert(data.responseText);
//                }
//            });
//        }
//    }
    window.onunload = function () {
        $.ajax({
            url: '../ajaxhandler/ajax_handler_toExecl.ashx',
            type: 'post',
            data: { type: "clear" },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
    function query() {
        $.ajax({
            url: '../ajaxhandler/ajax_handler_toExecl.ashx',
            type: 'post',
            data: { type: "clear" },
            error: function (data) {
                alert(data.responseText);
            }
        });
        $("#<%=tabContainer1.ClientID %>").css("display", "block");
        var childFrame = document.getElementById("chiledIframe");
        childFrame.src = "unit_sale_rpt_iframe.aspx";
        var mainFrame = document.getElementById("mainIframe");
        mainFrame.src = "unit_sale_rpt_content.aspx" + getCondtion();
    }
    function getCondtion() {
        var condition = { order_no: $("#<%=tbOrderNo.ClientID %>").val(), order_date_begin: $("#<%=selOrderDateBegin.ClientID %>").val(), order_date_end: $("#<%=selOrderDateEnd.ClientID %>").val(), unit_id: $("#<%=selUnit.ClientID %>")[0].values() };
      return "?order_no=" + condition.order_no + "&order_date_begin=" + condition.order_date_begin + "&order_date_end=" + condition.order_date_end + "&unit_id=" + condition.unit_id;
  }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<div class="tit_con">
    <span>搜索条件</span>
</div>
<table border="0" cellspacing="0" cellpadding="0" class="ss_tj"  >
        <tr>
            <td  class="tit_c">
                门店：
            </td>
            <td class="td_lp">
                <uc2:DropDownTree runat="server" ID="selUnit" MultiSelect="True" Url="../ajaxhandler/tree_query.ashx?action=unit&showCheck=true" />
            </td>
            <td  class="tit_c">
                单据号：
            </td>
            <td class="td_lp">
                <asp:TextBox ID="tbOrderNo" runat="server" MaxLength="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  class="tit_c">
                单据开始日期：
            </td>
            <td class="td_lp">
                <input type="text" runat="server"  readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_selOrderDateBegin','MainContent_selOrderDateEnd');"
                    title="双击清除时间" id="selOrderDateBegin" />
            </td>
            <td  class="tit_c">
                单据结束日期：
            </td>
            <td class="td_lp">
                <input type="text" runat="server"  readonly="readonly" ondblclick="this.value=''"
                    onclick="Calendar('MainContent_selOrderDateEnd');" title="双击清除时间" id="selOrderDateEnd" />
            </td>
        </tr>
    </table>
    <div class="b_bg" style="width: 100%; height: auto">
        <input type="button" class="input_c" id="btnQuery" value="查询" onclick="query();" />
        <input type="button" class="input_c" id="btnReset" runat="server" value="重置" onserverclick="btnResetClick" />
        
    </div>
    <div class="tit_con">
            <span>搜索结果</span>
        </div>
<cc1:TabContainer ID="tabContainer1" runat="server" style="display:none; height:auto" 
        ActiveTabIndex="0">
    <cc1:TabPanel ID="result" runat="server">
        <HeaderTemplate><div class="tab_head" id="searchResult"><span style="font-size: 12px;">汇总</span></div></HeaderTemplate>
        <ContentTemplate><iframe scrolling="auto" frameborder="0" src="unit_sale_rpt_content.aspx" id="mainIframe" style="width:100%; height:auto; min-height:470px;"></iframe></ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel ID="detail" runat="server">
        <HeaderTemplate><div class="tab_head" id="detailList"><span style="font-size: 12px;">明细</span></div></HeaderTemplate>
        <ContentTemplate><iframe scrolling="auto" frameborder="0" src="unit_sale_rpt_iframe.aspx" id="chiledIframe" style="width:100%; height:auto; min-height:470px"></iframe></ContentTemplate>
    </cc1:TabPanel>
</cc1:TabContainer>
</asp:Content>
