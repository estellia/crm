<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="bill_status_show.aspx.cs" Inherits="bill_bill_status_show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function go_back() {
            this.location.href = '<%=this.Request.QueryString["from"] ?? "bill_status_query.aspx"%>';
        }

        function checkInput() {
            if ($("#<%=cbBillKind.ClientID %>").val() == "-1") {
                this.infobox.showPop("请选择表单类型！");
                $("#<%= cbBillKind.ClientID%>").focus();
                return false;
            }
            if ($("#<%=tbStatus.ClientID %>").val() == "") {
                this.infobox.showPop("状态不能为空");
                $("#<%=tbStatus.ClientID %>").focus();
                return false;
            }
            if ($("#<%=tbDescription.ClientID %>").val() == "") {
                this.infobox.showPop("描述不能为空");
                $("#<%=tbDescription.ClientID %>").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="background-color: #f2f1ef;">
        <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
            <tr>
                <td class="td_co">
                    表单类型：
                </td>
                <td >
                <div class="box_r">
                </div>
                    <asp:DropDownList ID="cbBillKind" runat="server" DataTextField="Type_Name" DataValueField="Type_Id" >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td   class="td_co">
                    状态：
                </td>
                <td >
                <div class="box_r">
                </div>
                    <asp:TextBox runat="server"  MaxLength="25"  Width="245px"
                        ID="tbStatus"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td   class="td_co">
                    描述：
                </td>
                <td >
                <div class="box_r">
                </div>
                    <asp:TextBox runat="server"  MaxLength="50" Width="245px"
                        ID="tbDescription"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td  class="td_co">
                    起始标志：
                </td>
                <td >
                 <div class="box_r">
                </div>
                    <asp:DropDownList ID="cbBeginFlag" runat="server"  Width="110px">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  class="td_co" >
                    结束标志：
                </td>
                <td >
                 <div class="box_r">
                </div>
                    <asp:DropDownList ID="cbEndFlag" runat="server" Width="110px" >
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  class="td_co">
                    删除标志：
                </td>
                <td >
                <div class="box_r">
                </div>
                    <asp:DropDownList ID="cbDeleteFlag" runat="server" Width="110px">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  class="td_co">
                    自定义标志：
                </td>
                <td class="td_lp">
                    <asp:TextBox runat="server" MaxLength="10" Text="0"
                        ID="tbCustomerFlag" Width="105px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>

    <div class="bf">
        <asp:Button ID="btnSave" runat="server" Text="保存" class="input_bc" OnClick="btnSave_Click" OnClientClick="if(!checkInput()){return false;}"  />
        <input type="button" onclick="go_back()" value="返回" class="input_fh" />
    </div>
</asp:Content>
