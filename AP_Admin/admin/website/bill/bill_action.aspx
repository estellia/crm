<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bill_action.aspx.cs" Inherits="bill_bill_action" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>表单操作</title>
    <base target="_self" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/common.js" type="text/javascript"></script>

    <script type="text/javascript">
        function CheckRemark() {
            if (document.getElementById("tbRemark").value == "") {
                alert("请输入内容");
                return false;
            }
            return true;
        }
        
    </script>
</head>
<body style="overflow:hidden"> 
    <form id="form1" runat="server">
    <div class="mpos_a" >
    <div class="right">
        <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabOper" style="margin:auto;width:400px;height:250px;">
            <tr>
                <%--<td width="50px" align="right" class="td_lp"></td>--%>
                <td width="300px" class="td_lp" style="height:14px">备注:</td>
            </tr>
            <tr>
                <%--<td class="td_co"></td>--%>
                <td class="td_lp" style="height:10px">
                    <asp:TextBox ID="tbRemark" runat="server"  Height="160px" 
                        MaxLength="200" TextMode="MultiLine" Width="376px"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td colspan="2">
                   <div class="bf" style="background-color:transparent;padding:0;padding-top:6px;" >
                       <asp:Button ID="btnOK" runat="server" Text="确定" class="input_bc" OnClientClick="if(!CheckRemark()) return false;window.returnValue=true" OnClick="btnOK_Click"  UseSubmitBehavior="false" style="padding:0; margin-right:15px"/>
                      <%-- <asp:Button ID="btnReturn" runat="server" Text="返回" class="input_fh" OnClientClick="window.returnValue=false;" OnClick="btnReturn_Click" width="75px"  UseSubmitBehavior="false"/>--%>
                       <input type="button"  value="关闭" class="input_fh"onclick="window.returnValue=false;window.close();" id="btnReturn"/>
                    </div>
            </td>
            </tr>
        </table> 
    </div>
    </div>
    </form>
</body>
</html>
