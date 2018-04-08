<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" Inherits="log_log_query" Codebehind="log_query.aspx.cs" %>


<%@ Register Assembly="WebComponent" Namespace="WebComponent" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .btnMoreCss
        {
             
             margin-right:auto;
            overflow: auto;
            background-color: #F1F0EC;
            border: 0px solid #ccc;
        }
       
    </style>

    <script type="text/javascript">
        function toggle(targetid,sender) {
            if (document.getElementById) {
                target = document.getElementById(targetid);
                if (target.style.display == "block") {
                    target.style.display = "none";
                    $(sender).text("▼");
                    
                } else {
                    target.style.display = "block";
                    $(sender).text("▲");
                }
            }
        }
        //获取所有选择项的ID
        function getSelectedIds() {
            var arry = [];
            $("input:checked").each(function () {
                if ($(this).attr("id") != "power") {
                    arry.push($(this).attr("log_id"));
                }
            });
            if(arry.length==0){
                infobox.showPop("必须选择至少一条日志");
                return null;
            }
            return arry.join(",");
        }
        function getfrom() {
            return escape($("#_from").val());
        }
//        function go_Modify(log_id) {
//            this.location.href = "log_view.aspx.aspx?oper_type=3&log_id=" + log_id + "&from=" + getfrom();
//        }
//        function go_Visible(log_id) {
//            this.location.href = "log_view.aspx?oper_type=1&log_id=" + log_id + "&from=" + getfrom();
//        }

        function openWindow(url,width, height, NewWin) {

            var popUpWin = 0;
            var left = 200;
            var top = 200;
            if (screen.width >= width) {
                left = Math.floor((screen.width - width) / 2);
            }
            if (screen.height >= height) {
                top = Math.floor((screen.height - height) / 2);
            }

            var from = window.showModalDialog(url, null, "dialogHeight=" + width + "px;dialogWidth=" + height + "px;dialogTop=" + top + "px;dialogLeft=" + left + "px;help=no;scroll=yes;");
            return from;
        }
        function go_Export() {
            var ids=getSelectedIds();
            if(ids==null){
                return false;
            }
            $("#<%=hidLogIds.ClientID %>").val(getSelectedIds());
            return true;
        }
        function go_Visible(id) {
            location.href = "log_view.aspx?oper_type=1&log_id="+ id + "&from="+getfrom();
            //var value = openWindow("log_view.aspx?oper_type=1&log_id=" + id, 800, 900, '');
        }
        function checkDateFormat(){
            var flag = true;
            $(".selectdate").each(function(){
                var value = $(this).val();
                if(value!=""){
                if(value.split("-").length!=3){
                    alert("日期格式不正确!");
                    $(this).focus();
                    flag=  false;
                    return false;
                }
                }
            });
            return flag;
        }
        $(function(){
            $(".chk_row").click(checkPower);
        });
        function checkPower(){
            if($(".gv-checkbox").find(":checked").length==($(".ss_jg tr").length-1)){
                $("#power").attr("checked","checked");
            }else{
                $("#power").removeAttr("checked");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="tit_con">
        <span>搜索条件</span>
    </div>
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <td class="tit_c" >
                平台：
            </td>
            <td class="td_lp" >
                <asp:DropDownList  runat="server" ID="cbAppType">
                </asp:DropDownList>
            </td>
            <td class="tit_c" >
                接口代码：
            </td>
            <td class="td_lp" colspan="2">
                <asp:TextBox  runat="server" ID="tbIfCode"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                日志类型：
            </td>
            <td class="td_lp" >
                <asp:DropDownList  runat="server" ID="cbLogType">
                </asp:DropDownList>
            </td>
            <td class="tit_c" >
                日志代码（错误码）：
            </td>
            <td class="td_lp" >
                <asp:TextBox  runat="server" ID="tbLogCode" ></asp:TextBox>
            </td>
            <td class="td_lp" style="padding-left:20px;"><span style="font-size:smaller">更多查询条件</span><a href="#" class="btnMoreCss" onclick="toggle('More',this)">▼</a></td>
        </tr>
            </table>
<%--    <asp:LinkButton ID="btnMore" runat="server"  CssClass="btnMoreCss" OnClientClick="toggle('More')" >更多查询条件</asp:LinkButton>--%>
    <div  class="ss_tj" id ="More" style="display:none">
    <table border="0" cellspacing="0" cellpadding="0" class="ss_tj">
        <tr>
            <td class="tit_c" >
                日志关键字：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbBizCode"></asp:TextBox>
            </td>
            <td class="tit_c" >
                日志内容：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbLogBody"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                业务ID：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbBizId"></asp:TextBox>
            </td>
            <td class="tit_c" >
                日志ID：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbLogId"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                创建日期：
            </td>
            <td class="td_lp" align="left">
                <input type="text" runat="server" ondblclick="this.value=''" onclick="Calendar('MainContent_tbCreateTimeBegin','MainContent_tbCreateTimeEnd');"
                    title="双击清除时间" id="tbCreateTimeBegin"  class="selectdate"/>
            </td>
            <td align="center">
                至
            </td>
            <td class="td_lp" align="right" >
                <input type="text" runat="server" ondblclick="this.value=''" onclick="Calendar('MainContent_tbCreateTimeEnd');"
                    title="双击清除时间" id="tbCreateTimeEnd"  class="selectdate" />
            </td>
             <td class="tit_c" >
                修改日期：
            </td>
            <td class="td_lp" align="left">
                <input type="text" runat="server" ondblclick="this.value=''" onclick="Calendar('MainContent_tbModifyTimeBegin','MainContent_tbModifyTimeEnd');"
                    title="双击清除时间" id="tbModifyTimeBegin" class="selectdate" />
            </td>
            <td  align="center">
                至
            </td>
            <td class="td_lp" align="right" >
                <input type="text" runat="server" ondblclick="this.value=''" onclick="Calendar('MainContent_tbModifyTimeEnd');"
                    title="双击清除时间" id="tbModifyTimeEnd" class="selectdate" />
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                创建人ID：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbCreateUserId"></asp:TextBox>
            </td>
            <td class="tit_c" >
                修改人ID：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbModifyUserId"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                客户代码：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbCustomerCode"></asp:TextBox>
            </td>
            <td class="tit_c" >
                客户ID：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbCustomerId"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                门店代码：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbUnitCode"></asp:TextBox>
            </td>
            <td class="tit_c" >
                门店ID：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbUnitId"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tit_c" >
                用户代码：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbUserCode"></asp:TextBox>
            </td>
            <td class="tit_c" >
                用户ID：
            </td>
            <td class="td_lp" colspan="3">
                <asp:TextBox  runat="server" ID="tbUserId"></asp:TextBox>
            </td>
        </tr>
            </table>
    </div>

    <div class="b_bg" style="width: 100%; height: auto">
     <asp:Button ID="btnQuery" CssClass="input_c" OnClientClick="if(!checkDateFormat()) reutrn false;" Text="查询" runat="server" 
            onclick="btnQuery_Click"  />
    <input type="button" value="导出" runat="server" id="btnExport" class="input_c" onclick="if(!go_Export()) return false;" onserverclick="btnExportClick"/>
    <asp:Button text="重置" runat="server" id="btnReset" CssClass="input_c" 
            onclick="btnReset_Click" />
    </div>
    <div class="tit_con">
        <span>搜索结果</span></div>
    <div class="ss_bg" style="padding-right: 14px;">
        <asp:GridView ID="gvLog" runat="server" DataKeyNames="log_id" GridLines="None" ShowHeaderWhenEmpty="True" CellPadding="0" CellSpacing="1" CssClass="ss_jg"
            AutoGenerateColumns="False" >
            <RowStyle CssClass="b_c4" />
            <Columns>
                <asp:TemplateField  ItemStyle-CssClass="gv-checkbox" HeaderText="<input id='power' type='checkbox' onclick='checkAll(event)'/>">
                    <ItemTemplate>
                       <%-- <asp:CheckBox ID="chkSelect" log_id='<%#Eval("log_id") %>' runat="server"  CssClass="chk_row"/>--%>
                        <input type="checkbox" class="chk_row" log_id='<%#Eval("log_id") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center" HeaderText="平台">
                    <ItemTemplate>
                        <asp:Label ID="lblAppCode"  runat="server" Text='<%#Eval("app_code") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="日志类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="lblLogType"  runat="server" Text='<%#Eval("log_type_code") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="接口代码" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                    <ItemTemplate>
                        <asp:Label ID="lblIfCode"  runat="server" Text='<%#Eval("if_code") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="日志关键字" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                    <ItemTemplate>
                        <asp:Label ID="lblBizCode"  runat="server" Text='<%#Eval("biz_name") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="日志代码（错误码）" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="90px">
                    <ItemTemplate>
                        <asp:Label ID="lblLogCode"  runat="server" Text='<%#Eval("log_code") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建时间" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="140px">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateTime"  runat="server" Text='<%#Eval("create_time") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="日志内容" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                    <ItemTemplate>
                       <a id="a" href="log_view.aspx?oper_type=1&log_id=<%#Eval("log_id") %>" target="_blank" style="cursor: pointer">
                            <img src="../img/view.png" alt="" title="查看" /></a>
                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pag" />
            <HeaderStyle CssClass="b_c3" />
            <AlternatingRowStyle CssClass="b_c5" />
        </asp:GridView>
         <uc1:splitpagecontrol ID="SplitPageControl1" runat="server" 
        CssClass="pag" PageSize="10"
        OnRequireUpdate="SplitPageControl1_RequireUpdate" />
        </div>
        <input type="hidden" runat="server" id="hidLogIds" />
</asp:Content>

