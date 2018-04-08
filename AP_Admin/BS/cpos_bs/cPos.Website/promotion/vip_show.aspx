<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true"
    CodeFile="vip_show.aspx.cs" Inherits="promotion_vip_show" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function go_Back() {
            location.href = '<%=this.Request.QueryString["from"] ?? "vip_query.aspx"%>'

        }

         $(function(){
            if(<%=btnSave.Visible?"false":"true" %>)
            {
                disableCtrls("#<%=tabContainer1.ClientID %>");
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <cc1:TabContainer  runat="server" ID="tabContainer1" 
        ActiveTabIndex="0">
        <cc1:TabPanel runat="server" ID="tabBasic">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;">基本信息</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="tabCustomer">
                    <tr>
                        <td  class="td_co" >
                            会员卡号：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <asp:TextBox runat="server"  ID="tbNo" MaxLength="25"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            类型：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <asp:DropDownList ID="cbType"  runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            姓名：
                        </td>
                        <td>
                            <div class="box_r">
                            </div>
                            <asp:TextBox ID="tbName"  runat="server"  MaxLength="25"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            性别：
                        </td>
                        <td class="td_lp">
                            <asp:DropDownList ID="cbGender" runat="server">
                                <asp:ListItem Value="男">男</asp:ListItem>
                                <asp:ListItem Value="女">女</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            身份证号：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbIdentityNo" runat="server"   MaxLength="25"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            英文名：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbEnglishName" runat="server"   MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            生日：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbBirthday" runat="server"   MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            地址：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbAddress" runat="server"   MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            邮编：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbPostcode" runat="server"   MaxLength="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            手机：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbCell" runat="server"   MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            邮箱：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbEmail" runat="server"   MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            QQ：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbQQ" runat="server"   MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            MSN：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbMSN" runat="server"   MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            微博：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbWeibo" runat="server"   MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            当前积分：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbPoints" runat="server" CssClass="ban_in_oper_time" 
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            状态：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbStatus" runat="server" CssClass="ban_in_oper_time" 
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            办卡时间：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbActivateTime" runat="server" CssClass="ban_in_oper_time" 
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            办卡门店：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbActivateUnitName" runat="server" CssClass="ban_in_oper_time" 
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            有效期限：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbExpiredDate" runat="server"   MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            备注：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbRemark" runat="server"  Width="430px" Height="50px" MaxLength="180"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" ID="tabCard">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;">卡信息</span></div>
            </HeaderTemplate>
            <ContentTemplate>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" ID="tabOper">
            <HeaderTemplate>
                <div class="tab_head">
                    <span style="font-size: 12px;">操作信息</span></div>
            </HeaderTemplate>
            <ContentTemplate>
                <table border="0" cellspacing="0" cellpadding="0" class="con_tab" id="Table1">
                    <tr>
                        <td  class="td_co">
                            创建人：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox runat="server" CssClass="ban_in_oper_time" ID="tbCreater" 
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            创建时间：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbCreateTime" runat="server" CssClass="ban_in_oper_time" 
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            最后修改人：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbEditor" runat="server" CssClass="ban_in_oper_time" 
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            最后修改时间：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbEditTime" runat="server" CssClass="ban_in_oper_time" 
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  class="td_co">
                            系统最后修改时间：
                        </td>
                        <td class="td_lp">
                            <asp:TextBox ID="tbSysModifyTime" runat="server" CssClass="ban_in_oper_time" 
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        
    </cc1:TabContainer>
    <div   class="bf"style="clear: both; text-align:center"  >
            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="input_fh" OnClick="btnSave_Click" />&nbsp
            <input type="button" id="btnReturn" onclick="go_Back()" value="返回" class="input_fh" />
        </div>
</asp:Content>
