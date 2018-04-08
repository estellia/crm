<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoSso.aspx.cs" Inherits="GoSso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
    <script src="Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        if (checkFrame_IsTop()) {
            this.location.href='<%=this.ResolveUrl(System.Configuration.ConfigurationManager.AppSettings["sso_url"].ToString()) %>';
        }
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
