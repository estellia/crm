<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="right_menu_version_query" CodeBehind="menu_version_query.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function go_Confirm(sender) {
            var status = $(sender).attr("desc");
            var value = status == "-1" ? "启用" : "停用";
            return confirm("是否要" + value + "?");
        }
        //确认是否设置菜单可操作权限
        function go_IsCanAccessConfirm(sender) {
            var status = $(sender).attr("desc");
            var value = status == "0" ? "启用" : "停用";
            return confirm("是否要" + value + "?");
        }

        //返回 from=url
        function getFrom() {
            return "from=" + escape(document.getElementById("_from").value);
        }

        //查看  
        function show(menu_id) {
            this.location.href = "menu_show.aspx?oper_type=1&menu_id=" + menu_id + "&" + getFrom();
        }

        var cur_menu_id = '<%=this.Request.QueryString["cur_menu_id"]%>';

        //新建
        function create() {
        <%-- if(<%=tvMenu.SelectedNode.Depth %> < 3)
         { --%>
         this.location.href = "menu_show.aspx?oper_type=2&app_id=" + '<%=cbApp.SelectedValue %>'
             + "&parent_menu_id=" + '<%=tvMenu.SelectedNode.Value %>'
                 + "&cur_menu_id=" + cur_menu_id
                 + "&" + getFrom();
         //}else{alert("最多只能添加三级菜单");}
     }

     //编辑
     function edit(menu_id) {
         this.location.href = "menu_show.aspx?oper_type=3&menu_id=" + menu_id
             + "&cur_menu_id=" + cur_menu_id
             + "&" + getFrom();
     }

     //停用
     function stop_use(menu_id) {
         if (show_bill_action('menu', 0, 6, 2, menu_id)) {
             this.location.href = document.getElementById("_from").value;
         }
     }

     //启用
     function enable_use(menu_id) {
         if (show_bill_action('menu', 0, 6, 1, menu_id)) {
             this.location.href = document.getElementById("_from").value;
         }
     }

     $(function () {
         //全选事件
         $("#chk_all").click(
            function () {
                if ($(this).attr("checked")) {
                    $(".chk_mi").attr("checked", "checked");
                }
                else {
                    $(".chk_mi").removeAttr("checked");
                }
            });
         //停用按钮
         $("#btnDisable").click(
                function () {
                    var ids = getCheckedIds();
                    if (ids.length == 0) {
                        infobox.showPop("请选择菜单", "error");
                        return;
                    }
                    stop_use(ids.join(','));
                }
             );

         //启用按钮
         $("#btnEnable").click(
                function () {
                    var ids = getCheckedIds();
                    if (ids.length == 0) {
                        infobox.showPop("请选择菜单", "error");
                        return;
                    }
                    enable_use(ids.join(','));
                }
             );
     });

     function getCheckedIds() {
         var rult = [];
         $(".chk_mi:checked").each(function () {
             rult.push($(this).val());
         });
         return rult;
     }
    </script>
    <style type="text/css">
        .ss_jg {
            margin: 0px;
        }

        #MainContent_tvMenu {
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            -o-text-overflow: ellipsis;
            width: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="tit_con"><span>菜单查询</span></div>
    <div>
        <table cellpadding="0" cellspacing="0" class="td_lp ss_tj">
            <tr>
                <td class="tit_c">行业版本：</td>
                <td>
                    <asp:DropDownList runat="server" ID="cbVersion" ViewStateMode="Enabled"></asp:DropDownList></td>
                <td class="tit_c"><span>应用系统：</span></td>
                <td>
                    <asp:DropDownList runat="server" ID="cbApp" ViewStateMode="Enabled"></asp:DropDownList>
                </td>
                <td class="tit_c">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr style="display: none">
                <td class="tit_c">状态：</td>
                <td>
                    <asp:DropDownList ID="cbStatus" runat="server" Width="196px">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="1">正常</asp:ListItem>
                        <asp:ListItem Value="-1">停用</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tit_c"></td>
                <td>&nbsp;</td>
                <td class="tit_c">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <div class="b_bg">
        <%--<asp:Button ID="btnQuery" CssClass="input_c" Text="查询" runat="server" OnClick="btnQuery_Click" />--%>
        <asp:Button ID="btnSync" CssClass="input_c" Text="同步" runat="server" OnClick="btnSync_Click"
            OnClientClick="this.value='同步中...';this.disabled=true" UseSubmitBehavior="false" />
        <input type="button" id="btnDisable" value="停用" class="input_c hidden" />
        <input type="button" id="btnEnable" value="启用" class="input_c hidden" />
    </div>
    <div class="ss_bg">
        <table cellpadding="0" cellspacing="4" style="width: 100%; height: 100px">
            <tr>
                <td style="vertical-align: top; border: 1px solid #ccc;">
                    <asp:TreeView ID="tvMenu" runat="server" Width="200" SelectedNodeStyle-BackColor="Blue" SelectedNodeStyle-ForeColor="White">
                    </asp:TreeView>
                </td>
                <td style="vertical-align: top; width: 100%">
                    <asp:GridView ID="gvMenu" runat="server" CellPadding="0"
                        CellSpacing="1" CssClass="ss_jg"
                        AutoGenerateColumns="False" DataKeyNames="ID" OnRowCommand="gvMenu_RowCommand"
                        GridLines="None"
                        ShowHeaderWhenEmpty="True" OnRowDataBound="gvMenu_RowDataBound">
                        <RowStyle CssClass="b_c4" />
                        <Columns>
                            <%--                        <asp:TemplateField ItemStyle-CssClass="gv-checkbox hidden" HeaderText="<input type='checkbox' id='chk_all'/>">
                            <ItemTemplate>
                                <input type="checkbox" class="chk_mi" value='<%#Eval("ID") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="编码" ItemStyle-CssClass="gv-code" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <a onclick='show("<%#Eval("ID") %>")' href="javascript:">
                                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Code") %>' /></a>
                                </ItemTemplate>

                                <ItemStyle CssClass="gv-code"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="名称" ItemStyle-Width="100px">
                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="EnglishName" HeaderText="英文名称" ItemStyle-Width="100px" Visible="False">
                                <ItemStyle Width="100px"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="URLPath" HeaderText="访问路径" />
                            <asp:BoundField DataField="StatusDescription" HeaderText="状态" ItemStyle-CssClass="gv-status">
                                <ItemStyle CssClass="gv-status"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="备注" ItemStyle-CssClass="gv-4word" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="remark" runat="server" ToolTip='<%#Eval("remark") %>' Text='<%#Eval("remark")==null?"":Eval("remark").ToString().Length>=10?Eval("remark").ToString().Substring(0,10)+"...":Eval("remark").ToString() %>' />
                                </ItemTemplate>

                                <ItemStyle CssClass="gv-4word" Width="138px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DisplayIndex" HeaderText="显示顺序" ItemStyle-CssClass="gv-4word">
                                <ItemStyle CssClass="gv-4word"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CustomerVisibleDescription" HeaderText="客户可见" ItemStyle-CssClass="gv-4word" Visible="False">
                                <ItemStyle CssClass="gv-4word"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="gv-2icon">
                                <ItemTemplate>
                                    <a onclick='edit("<%# Eval("ID")%>")' style="display: none; cursor: pointer">
                                        <img src="../img/edit.png" alt="" title="编辑" />
                                    </a>
                                    <asp:ImageButton runat="server" ID="lbControl" CommandName='<%#Eval("Status") %>' desc='<%#Eval("Status") %>' CommandArgument='<%#Eval("ID") %>' OnClientClick="if(!go_Confirm(this)) return false;" ToolTip='<%#Eval("Status").ToString()=="-1"?"启用" : "停用"%>' />
                                    <asp:ImageButton runat="server" ID="lbIsCanAccess" CommandName='SetIsCanAccess' desc='<%#Eval("IsCanAccess") %>'  CommandArgument='<%#Eval("vvMappingMenuId") %>' OnClientClick="if(!go_IsCanAccessConfirm(this)) return false;" ToolTip='<%#Eval("IsCanAccess").ToString()=="0"?"启用操作权限" : "停用操作权限"%>' />
                                </ItemTemplate>

                                <ItemStyle Width="100px" CssClass="gv-2icon"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="b_c3" />
                        <AlternatingRowStyle CssClass="b_c5" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

