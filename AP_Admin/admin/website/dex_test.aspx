<%@ Page Title="test" Language="C#" MasterPageFile="~/common/Site.master" 
    AutoEventWireup="true" CodeFile="dex_test.aspx.cs" Inherits="dex_test" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Button ID="btnGetLogTypes" Text="GetLogTypes" runat="server" OnClick="btnGetLogTypes_Click" />
    <asp:Button ID="btnGetAppList" Text="GetAppList" runat="server" OnClick="btnGetAppList_Click" />
    <asp:Button ID="btnGetLogs" Text="GetLogs" runat="server" OnClick="btnGetLogs_Click" />
    <asp:Button ID="btnGetLogsCount" Text="GetLogsCount" runat="server" OnClick="btnGetLogsCount_Click" />
    <asp:Button ID="btnGetLog" Text="GetLog" runat="server" OnClick="btnGetLog_Click" />
    <div></div>
    <asp:TextBox ID="tbResult" runat="server" Rows="30" Width="800" TextMode="MultiLine" />
</asp:Content>

