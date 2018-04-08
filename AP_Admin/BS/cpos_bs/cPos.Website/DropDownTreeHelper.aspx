<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DropDownTreeHelper.aspx.cs" Inherits="DropDownTreeHelper" %>
<%@ Register src="controls/DropDownTree.ascx" tagname="DropDownTree" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/tree.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/Plugins/jquery.tree.js" type="text/javascript"></script>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/tree.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
         function getTree() { 
            return document.getElementById("DropDownTree1");
         }

         $(function () {
             //得到选中项的值
             $("#btn_getvals").click(
                function () {
                    var vals = getTree().values();
                    alert(vals.join(","));
                }
            );

             //得到选中项的文本text
             $("#btn_gettexts").click(
                function () {
                    var vals = getTree().texts();
                    alert(vals.join(","));
                }
            );

             //得到选择树控件的 text
             $("#btn_gettext").click(
                function () {
                    var val = getTree().text();
                    alert(val);
                }
            );
             //             //只读
             //             $("#btn_notreadonly").click(
             //                function () {
             //                    val.
             //                }
             //             );
             getTree().onchanged = function () {
                 alert("change");
             }
         });
         
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>请选择树结点：</td>
                <td><uc1:DropDownTree ID="DropDownTree1" runat="server" Width="200px" DropdownHeight="100px" MultiSelect="true" ReadOnly="false"  Url="ajaxhandler/tree_query.ashx?action=test"   /></td> 
                <td><div runat="server" id="div_rult"></div></td>
            </tr>
            <tr>
                <td> </td>
                <td><input type="text" style="width:200px" /></td>
                <td></td>
            </tr> 
            <tr>
                <td> </td>
                <td><table cellpadding=0; cellspacing=0 style="width:200px;background-color:Red;padding:0px;margin:0px;"><tr><td >ds</td></tr></table></td>
                <td></td>
            </tr>
            <tr>
                <td>后台提交</td>
                <td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" /></td>
                <td></td>
            </tr>
            <tr>
                <td>前台操作</td>
                <td><input type="button" value ="得到选中项的values" id="btn_getvals" /></td>
                <td></td>
            </tr>
            <tr>
                <td>前台操作</td>
                <td><input type="button" value ="得到选中项的texts" id="btn_gettexts" /></td>
                <td></td>
            </tr>
            <tr>
                <td>前台操作</td>
                <td><input type="button" value ="得到选择树控件的 text" id="btn_gettext" /></td>
                <td></td>
            </tr>
     <%--       <tr>
                <td>前台操作</td>
                <td><input type="button" value ="只读" id="btn_readonly" /></td>
                <td></td>
            </tr>
            <tr>
                <td>前台操作</td>
                <td><input type="button" value ="非只读" id="btn_notreadonly" /></td>
                <td></td>
            </tr>--%>
        </table> 
    </div>
    </form>
</body>
</html>

