<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="bill_role_action_show.aspx.cs" Inherits="bill_bill_role_action_show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function go_back() {
            this.location.href = '<%=this.Request.QueryString["from"] ?? "bill_role_action_query.aspx"%>';
        }

        function ValidateInput() {

            return true;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="background-color: #f2f1ef;">
        <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
            <tr>
                <td width="150" align="right" class="td_co">
                    角色：
                </td>
                <td >
                <div class="box_r">
                </div>
                    <asp:DropDownList ID="cbRole" runat="server" DataTextField="Type_Name" DataValueField="Type_Id" Width="236px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="td_co">
                    表单类型：
                </td>
                <td>
                <div class="box_r">
                </div>
                    <asp:DropDownList ID="cbBillKind" runat="server" DataTextField="Type_Name" DataValueField="Type_Id"
                        OnSelectedIndexChanged="cbBillKind_SelectedIndexChanged" AutoPostBack="true" Width="236px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="td_co">
                    表单操作：
                </td>
                <td >
                <div class="box_r">
                </div>
                    <asp:DropDownList ID="cbBillAction" runat="server" DataTextField="Type_Name" DataValueField="Type_Id" Width="236px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="150" align="right" class="td_co">
                    前置状态：
                </td>
                <td >
                <div class="box_r">
                </div>
                    <asp:DropDownList ID="cbPrevBillStatus" runat="server" DataTextField="Type_Name"
                        DataValueField="Type_Id" Width="236px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="150" align="right" class="td_co">
                    当前状态：
                </td>
                <td >
                <div class="box_r">
                </div>
                    <asp:DropDownList ID="cbCurrBillStatus" runat="server" DataTextField="Type_Name"
                        DataValueField="Type_Id" Width="236px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="150" align="right" class="td_co">
                    最小金额：
                </td>
                <td class="td_lp">
                    <asp:TextBox runat="server" Width="120px" MaxLength="10"
                        ID="tbMinMoney"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="td_co">
                    最大金额：
                </td>
                <td class="td_lp">
                    <asp:TextBox runat="server" Width="120px" MaxLength="10"
                        ID="tbMaxMoney"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="bf">
        <asp:Button ID="btnSave" runat="server" Text="保存" class="input_bc" OnClick="btnSave_Click"  OnClientClick="if(!ValidateInput()){return false;}"/>
        <input type="button" onclick="go_back()" value="返回" class="input_fh" />
    </div>
</asp:Content>
