<%@ Page Language="C#" AutoEventWireup="true" Inherits="bill_bill_action_log" Codebehind="bill_action_log.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>表单操作历史</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
<div class="tit_con">
        <span>搜索结果</span>
        </div>
<div class="ss_bg">
        <asp:GridView ID="gvBillActionLog" runat="server" cellpadding="0" 
            cellspacing="1" CssClass="ss_jg" 
        AutoGenerateColumns="False"
            AllowPaging="False" 
            GridLines="None" 
            ShowHeaderWhenEmpty="True">
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:BoundField DataField="ActionTime" HeaderText="操作时间" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="ActionUserName" HeaderText="操作人" ItemStyle-Width="100px"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="BillActionDescription" HeaderText="操作" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="PreBillStatusDescription" HeaderText="前置状态" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="CurBillStatusDescription" HeaderText="当前状态" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="ActionComment" HeaderText="备注" ItemStyle-HorizontalAlign="Left"/>
            </Columns>
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
        </div>
    </form>
</body>
</html>
