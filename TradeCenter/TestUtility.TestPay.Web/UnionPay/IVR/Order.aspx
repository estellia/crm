<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="JIT.TestUtility.TestPay.Web.UnionPay.IVR.Order" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <h1>下订单</h1>
            第一步：手机号：<asp:TextBox ID="txtMobileNO" runat="server"></asp:TextBox> <asp:Button runat="server" ID="btnOrder" Text="下单" />
            <br />
            <br />
            第二步：<asp:Literal ID="ltGotoPay" runat="server" Text="等待预订单完成"></asp:Literal>
        </div>
        
        <div>
            <h1>查询订单</h1>
            第一步：<asp:Button runat="server" ID="btnQueryOrder" Text="查询订单" />
            <br />
            <br />
            查询结果：
            <br />
            <asp:TextBox runat="server" ID="txtOrderInfo" TextMode="MultiLine" Height="300" Width="400"></asp:TextBox>
        </div>
    </div>
    </form>
</body>
</html>
