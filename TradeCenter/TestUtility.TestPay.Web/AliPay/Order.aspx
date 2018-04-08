<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="JIT.TestUtility.TestPay.Web.AliPay.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        测试网站不支持自己输金额。每次0.01元,可以传参数金额：total_fee=**和物品名称:subject=**<br />
        如total_fee=0.5&subject=金条<br />
        <asp:Button ID="Button1" runat="server" Text="提交订单" onclick="Button1_Click" 
            Height="44px" Width="112px" />
        <br />
        URL:<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="Button2" runat="server" Text="付款" onclick="Button2_Click" 
            Height="48px" Width="69px" />
    </div>
    </form>
</body>
</html>
