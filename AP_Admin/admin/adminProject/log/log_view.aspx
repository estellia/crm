<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="log_log_view" Codebehind="log_view.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .tbLogBody
        {
            padding: 4px;
            font-size: 15px;
            height: 400px;
        }
        .con_tab
        {
           border-collapse:collapse;
        }
        .con_tab td
        {
            border: 1px solid #c0c9d2;
        }
         .logContent
        {
            padding:4px;
            font-size:15px;
        }
        .con_tab tr{ height:24px;
                      font-size:15;
                      line-height:24px;
                      word-wrap:break-word;
                     }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
<div class="tit_con">
    <span>日志内容</span>
</div>
    <div style="background-color: #f2f1ef;">
        <table border="0" cellspacing="0" cellpadding="0" class="con_tab" style="text-align: center; width:98%; table-layout:fixed;">
            <tr>
                <td class="td_co" >
                    平台：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblAppType">
                    </asp:Label>
                </td>
                <td class="td_co" >
                    接口代码：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblIfCode"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td_co" >
                    日志类型：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblLogType">
                    </asp:Label>
                </td>
                <td class="td_co" >
                    日志代码（错误码）：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblLogCode"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td_co" >
                    日志关键字：
                </td>
                <td class="td_lp" colspan="3"  >
                    <asp:Label  runat="server" ID="lblBizCode"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td_co" >
                    业务ID：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblBizId"></asp:Label>
                </td>
                <td class="td_co" >
                    日志ID：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblLogId"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td_co" >
                    创建时间：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblCreateTime"></asp:Label>
                </td>
                <td class="td_co" >
                    修改时间：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblModifyTime"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td_co" >
                    创建人ID：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblCreateUserId"></asp:Label>
                </td>
                <td class="td_co" >
                    修改人ID：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblModifyUserId"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td_co" >
                    客户代码：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblCustomerCode"></asp:Label>
                </td>
                <td class="td_co" >
                    客户ID：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblCustomerId"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td_co" >
                    门店代码：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblUnitCode"></asp:Label>
                </td>
                <td class="td_co" >
                    门店ID：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblUnitId"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td_co" >
                    用户代码：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblUserCode"></asp:Label>
                </td>
                <td class="td_co" >
                    用户ID：
                </td>
                <td class="td_lp" >
                    <asp:Label  runat="server" ID="lblUserId"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td_co"  style="margin-top: 0px; vertical-align: top;">
                    <div>
                        日志内容：</div>
                </td>
                <td colspan="3">
                <textarea runat="server" id="tbLogBody" readonly="readonly" style=" height:100%; width:99%;" class="logContent"  rows="30" ></textarea>
               
                </td>
            </tr>
        </table>
    </div>
    <div class="bf" style="width: 100%; height: auto;padding-bottom:10px; ">
        <input type="button" value="导出" runat="server" id="btnExport" class=" input_bc" onserverclick="btnExportClick" />
        <input type="button" id="btnClose" class=" input_fh" value="关闭" onclick="window.close();"/>
        <%--<asp:Button ID="btnClose" CssClass="input_c" Text="关闭" runat="server" OnClientClick="window.close();" />--%>
    </div>
</asp:Content>
