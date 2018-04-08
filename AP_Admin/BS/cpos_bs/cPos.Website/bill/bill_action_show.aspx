<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" CodeFile="bill_action_show.aspx.cs" Inherits="bill_bill_action_show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script type="text/javascript">
    function go_back() {
        this.location.href = '<%=this.Request.QueryString["from"] ?? "bill_action_query.aspx"%>';
    }

     $(function(){
            if(<%=btnSave.Visible?"false":"true" %>)
            {
                disableCtrls("#tabBill_action_query");
            }
        });

        function checkInput() {
            if ($("#<%=cbBillKind.ClientID %>").val() == "-1") {
                this.infobox.showPop("请选择表单类型！");
                $("#<%= cbBillKind.ClientID%>").focus();
                return false;
            }
            if ($("#<%=tbCode.ClientID %>").val() == "") {
                this.infobox.showPop("编码不能为空");
                $("#<%=tbCode.ClientID %>").focus();
                return false;
            }
            if ($("#<%=tbDescription.ClientID %>").val() == "") {
                this.infobox.showPop("描述不能为空");
                $("#<%=tbDescription.ClientID %>").focus();
                return false;
            }
            var all_false = false;
            $(".test").each(function(){
                if($(this).val()=="1"){
                    all_false = true;
                    return false;
                }
            });
            if(!all_false){
                alert("标志不能全为否!");
                return false;
            }
            return true;
        }
        $(function(){
            $(".test").change(changeOther);
        });
        function changeOther(){
            var target = event.target;
            if(!target){
                target = event.srcElement;
            }
            if($(target).val()=="1"){
                $(".test").each(function(){
                    if(this!=target){
                        $(this).val("0");
                    }
                });
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div style="background-color: #f2f1ef;">
        <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabBill_action_query">
            <tr>
                <td align="right" class="td_co">
                    表单类型：
                </td>
                <td >
                <div class="box_r">
                </div>
                    <asp:DropDownList  ID="cbBillKind" runat="server" DataTextField="Type_Name" DataValueField="Type_Id" Width="240px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="150" align="right" class="td_co">
                    编码：
                </td>
                <td >
                <div class="box_r">
                </div>
                    <asp:TextBox runat="server" Width="236px" MaxLength="30"
                        ID="tbCode"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="150" align="right" class="td_co">
                    描述：
                </td>
                <td>
                <div class="box_r">
                </div>
                    <asp:TextBox runat="server" Width="236px" MaxLength="50"
                        ID="tbDescription"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="td_co">
                    新建标志：
                </td>
                <td class="td_lp">
                    <asp:DropDownList class="test" ID="cbCreateFlag" runat="server" Width="110px">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="150" align="right" class="td_co">
                    修改标志：
                </td>
                <td class="td_lp">
                    <asp:DropDownList class="test" ID="cbModifyFlag" runat="server" Width="110px">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="150" align="right" class="td_co">
                    审核标志：
                </td>
                <td class="td_lp">
                    <asp:DropDownList class="test" ID="cbApproveFlag" runat="server"  Width="110px">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
                        <tr>
                <td width="150" align="right" class="td_co">
                    退回标志：
                </td>
                <td class="td_lp">
                    <asp:DropDownList class="test" ID="cbRejectFlag" runat="server"  Width="110px">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
                        <tr>
                <td width="150" align="right" class="td_co">
                    删除标志：
                </td>
                <td class="td_lp">
                    <asp:DropDownList class="test" ID="cbCancelFlag" runat="server" Width="110px">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td width="150" align="right" class="td_co">
                    排序：
                </td>
                <td class="td_lp">
                    <asp:TextBox runat="server" ID="txtDisplayIndex"></asp:TextBox>

                </td>
            </tr>
        </table>
    </div>
    <div class="bf">
        <asp:Button ID="btnSave" runat="server" Text="保存" class="input_bc" OnClick="btnSave_Click"  OnClientClick="if(!checkInput()){return false;}" />
        <input type="button" onclick="go_back()" value="返回" class="input_fh" />
    </div>

</asp:Content>

