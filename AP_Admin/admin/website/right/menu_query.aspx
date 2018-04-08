<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" CodeFile="menu_query.aspx.cs" Inherits="right_menu_query" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server"> 
    <script type="text/javascript">

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
         if(<%=tvMenu.SelectedNode.Depth %> < 3)
         { 
             this.location.href = "menu_show.aspx?oper_type=2&app_id=" + '<%=cbApp.SelectedValue %>'
                 + "&parent_menu_id=" + '<%=tvMenu.SelectedNode.Value %>'
                 + "&cur_menu_id=" + cur_menu_id
                 + "&" + getFrom();
         }else{alert("最多只能添加三级菜单");}
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
        .ss_jg{margin:0px;}
        #MainContent_tvMenu{overflow:hidden;text-overflow:ellipsis;white-space:nowrap;-o-text-overflow:ellipsis; width:200px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="tit_con"><span>菜单查询</span></div>
    <div>
         <table cellpadding="0" cellspacing = "0" class="td_lp ss_tj" >
                        <tr>
                            <td class="tit_c"><span >应用系统</span></td>
                            <td>
                                <asp:DropDownList runat="server" ID="cbApp" ViewStateMode="Enabled"></asp:DropDownList>
                            </td> 
                            <td class="tit_c">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr> 
                    </table>
    </div> 
    <div class="b_bg">  
        <input type="button" id="btnNew" value="新建" class="input_c" onclick="create()" />
        <input type="button" id="btnDisable" value="停用" class="input_c hidden"/>
        <input type="button" id="btnEnable" value="启用" class="input_c hidden"/>
    </div> 
    <div class="ss_bg">
        <table cellpadding="0" cellspacing = "4" style="width:100%;height:100px"> 
            <tr> 
                <td style="vertical-align:top;border:1px solid #ccc;">
                    <asp:TreeView ID="tvMenu" runat="server" width="200" SelectedNodeStyle-BackColor="Blue" SelectedNodeStyle-ForeColor="White" >
                    </asp:TreeView>
                </td> 
                <td style="vertical-align:top;width:100%">
                    <asp:GridView ID="gvMenu" runat="server" cellpadding="0" 
                    cellspacing="1" CssClass="ss_jg"  
                    AutoGenerateColumns="False"
                    AllowPaging="False" DataKeyNames="ID" OnRowCommand="gvMenu_RowCommand"
                    GridLines="None" 
                    ShowHeaderWhenEmpty="True" onrowdatabound="gvMenu_RowDataBound">
                    <RowStyle CssClass="b_c4" />
                    <Columns>
<%--                        <asp:TemplateField ItemStyle-CssClass="gv-checkbox hidden" HeaderText="<input type='checkbox' id='chk_all'/>">
                            <ItemTemplate>
                                <input type="checkbox" class="chk_mi" value='<%#Eval("ID") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="编码" ItemStyle-CssClass="gv-code">
                            <ItemTemplate>
                                <a onclick='show("<%#Eval("ID") %>")' href="javascript:">
                                <asp:Label ID="lblCode"  runat="server" Text='<%#Eval("Code") %>'/></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="名称" ItemStyle-Width="100px"/>
                        <asp:BoundField DataField="EnglishName" HeaderText="英文名称" ItemStyle-Width="100px" />
                        <asp:BoundField DataField="URLPath" HeaderText="访问路径" /> 
                        <asp:BoundField DataField="StatusDescription" HeaderText="状态"  ItemStyle-CssClass="gv-status" />
                        <asp:BoundField DataField="DisplayIndex" HeaderText="显示顺序" ItemStyle-CssClass="gv-4word"/>
                        <asp:BoundField DataField="CustomerVisibleDescription" HeaderText="客户可见" ItemStyle-CssClass="gv-4word"/>
                        <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="gv-2icon">
                            <ItemTemplate> 
                                <a onclick='edit("<%# Eval("ID")%>")' style=" cursor:pointer">
                                    <img src="../img/edit.png" alt="" title="编辑" />
                                </a>  
                                <asp:LinkButton CommandArgument='<%# Eval("ID")%>' ID="linkStop" runat="server" OnClick="Stop_Click"><img src="../img/disable.png" alt="" title="停用"/></asp:LinkButton>
                                <a onclick='enable_use("<%# Eval("ID")%>")' <%# 1.Equals(Eval("Status"))?"style='display:none'":"" %> style=" cursor:pointer">
                                    <img src="../img/enable.png" alt="" title="启用"/>
                                </a> 
                            </ItemTemplate>
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

