<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/common/Site.master" Inherits="InfoBoxPage" Codebehind="InfoBoxPage.aspx.cs" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .infobox{width:420px;text-align:left;margin:auto;margin-top:40px;}
        .infobox-header{line-height:28px;height:28px;background-color:#4F81BD;border:1px solid #ccc; font-weight:bold;color:White;} 
        .infobox-body{border:1px solid #ccc;border-top:none;background-color:White;}
        .info{padding:20px 4px;}
        .desc{padding:4px;font-size:12px;color:#ccc; border-top:1px solid #ccc;margin:4px;line-height:14px;}
    </style>
    <script type="text/javascript">
        function goto() {
            try {
                this.location.href = '<%=this.Request["go"] %>';
            } catch (ex) {
                alert(ex);
            }
        } 
        //setTimeout(goto, 3000);
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent"> 
    <div style="background-color:White;">
           <div class="infobox">
            <div class="infobox-header">
                <div runat="server" id="div_title" style="padding-left:4px"></div>
            </div>
            <div class="infobox-body"> 
                <div runat="server" id="div_content" class="info"></div>
                <div class="desc b_bg" style="background-color:White"> 
                    <%--说明：3 秒钟后自动返回--%>     
                    <input type="button" id="btnEnable" value="确定" class="input_c" onclick="goto()"/>
                </div>
            </div> 
         </div>
 </div>
</asp:Content>
 