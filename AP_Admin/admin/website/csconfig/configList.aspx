<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" CodeFile="configList.aspx.cs" Inherits="csconfig_configList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="../css/style3.css" rel="stylesheet" type="text/css" />
    <link href="../css/pagination.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div style="width:100%;height:100%;">
        <iframe src="http://www.o2omarketing.cn:9004/External/PageConfig/configList.aspx" style="min-height:800px;width:100%;"></iframe>
 
       <%--开发服务器上的:  http://localhost:9004/External/PageConfig/configList.aspx--%>
    </div>
    
</asp:Content>
