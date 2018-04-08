<%@ Page Language="C#" AutoEventWireup="true" CodeFile="authcodeimage.aspx.cs" Inherits="login_authcodeimage" %>

<% Response.Buffer = true ;%>
<% Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);%>
<% Response.Expires = 0 ;%>
<% Response.CacheControl = "no-cache" ; %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
