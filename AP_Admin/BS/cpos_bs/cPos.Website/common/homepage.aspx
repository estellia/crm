<%@ Page Title="店铺管理系统" Language="C#" AutoEventWireup="true" 
    CodeFile="homepage.aspx.cs" Inherits="common_homepage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/tree.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="../Scripts/menu.js"></script>
    <script type="text/javascript" src="../Scripts/region.js"></script>
    <script src="../Scripts/String_EX.js" type="text/javascript"></script>
    <script src="../Scripts/AutoComplete.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/common.js"></script>
    <script src="../Scripts/calendar.js" type="text/javascript"></script>
    <script src="../Scripts/JSON2.js" type="text/javascript"></script>
    <script src="../Scripts/Plugins/jquery.tree.js" type="text/javascript"></script>
    <script type="text/javascript">
        checkFrame_IsTop(); 

        infobox.timeout = null;
        infobox.items = [];
        infobox.showPop = function (text, className) {
            infobox.items.push({ text: text, className: className });
        }

        function do_popinfo_thing() {
            infobox.showPop = showPop;
            for (var i = 0; i < infobox.items.length; i++) {
                showPop(infobox.items[i].text, infobox.items[i].className);
            }
        }
        function showPop(text, className) {
            alert(text);
        }
        $(function () {
            //页面加载延迟弹出消息框
            setTimeout(do_popinfo_thing, 300);
        }); 
    </script>
     <script type="text/javascript">
         $(function () {
             function autoSize() {
                 try {
                     setHeight('mainframe');
                     var width = $(window).width() - $(".cPos_a .zd").width() - 2;
                     if ($(".cPos_a .left").is(":visible")) {
                         width = width - $(".cPos_a .left").width();
                     }
                     $("#mainframe").parent().width(Math.max(width, 0));

                     var height = $("#sysMenus").height();
                     height = Math.max(height, $("#mainframe").height() + 6);
                     height = Math.max(height, document.documentElement.clientHeight - 166);
                     height = Math.max(height, 0);

                     $(".cPos_a .left").height(height);
                     $("#mainframe").height(Math.max(height - 6, 0));
                     var $btn = $(".cPos_a .zd").height(height).find("a");
                     $btn.css("margin-top", Math.max(0, height - $btn.height()) / 2 + 'px');
                 }
                 catch (ex) { }
                 setTimeout(autoSize, 100);
             }
             autoSize();
         });

         //设置 iframe 高度为内容高度 [yf-new]
         var setHeight = function (frameId) {
            try{ 
                var height = window.frames[frameId]&&window.frames[frameId].document&&window.frames[frameId].document.body&&window.frames[frameId].document.body.scrollHeight;
                if(height >=0)
                {
                    $("#" + frameId).height(height);
                }
                //$(window.frames[frameId].document).height()); 
             }
             catch(ex){}
         }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //loadSysMenus();
            BuilderMenu();
        });

        function Layer1MenuCilck(menu) {
            $(menu).closest("ul").find(".lab_d").hide();
            $(menu).parent().next().show();
            return false;
        }

        function Layer2MenuCilck(menu) {
            $(menu).parent().next().toggle(); 
            return false;
        }

        function Layer3MenuCilck(menu) {
            if (menu.href && menu.href != "#") {
                $("#mainframe")[0].src = menu.href;
            }
            return false;
        } 

        function getUrl(menu) {
            return(menu.URLPathWithID == null ||
                menu.URLPathWithID == undefined) ? "#" : menu.URLPathWithID;
        }

        function goMenuClick(menu) {
            
        }

        function BuilderMenu() {
            $("#sysMenus").html("");
            var data = eval($("#<%=hid_menu_data.ClientID%>").val()); 
            $(data).each(function (index , item1) {
                var $1 = $("<li><a onclick='return Layer1MenuCilck(this)' target='mainframe'></a></li><div class='lab_d' style='display:none'></div>");
                $1.find("a").text(item1.Item.Menu_Name).attr("href", getUrl(item1.Item)); 
                $(item1.SubItems).each(function (index, item2) {
                    var $2 = $("<div class='l_1'><a onclick='return Layer2MenuCilck(this)' target='mainframe'></a></div><div style='display:none'></div>");
                    $2.find("a").text(item2.Item.Menu_Name).attr("href", getUrl(item2.Item));
                    $(item2.SubItems).each(function (index, item3) {
                        var $3 = $("<div class='l_3'><a target='mainframe' onclick='return Layer3MenuCilck(this)'></a></div>");
                        $3.find("a").text(item3.Item.Menu_Name).attr("href", getUrl(item3.Item));
                        $3.appendTo($2.eq(1));
                    });
                    $2.appendTo($1.eq(1));
                });
                $("#sysMenus").append($1);
            });
        }

    </script>
    <style type="text/css"> 
        #pop_container{position:absolute; top:65px;display:block;height:16px;line-height:16px;text-align:center;width:100%;}
        #pop_container .item{display:inline-block;padding:2px 10px; margin-left:2px;}
        #pop_container .info{background:#68af02;*border-bottom:2px solid #68af02;color:White;}
        #pop_container .error{background:#ef8f00;*border-bottom:2px solid #ef8f00;color:White;} 
    </style> 
</head>
<body>
    <form id="Form1" runat="server" >
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True">
        <Services>
            <asp:ServiceReference Path="~/WebService/MenuService.asmx" />
        </Services>
    </asp:ScriptManager>
    <div id="error"></div>
    <div class="rmt">
        <p class="top fl">
            <span>
                <img src="../img/logo_t.png"  alt=""/></span></p>
    </div>
    <div class="titna"><asp:Label ID="lblCurrentCustomer" runat="server" Text=""></asp:Label></div>
    <div class="y_tibg">
        <span class="l_hi">你好！<asp:Label ID="lblUsername" runat="server" Text="系统管理员" /></span><span
            class="r_mz">
            <a target='mainframe' href="../right/modify_user_pwd.aspx">修改密码</a>
            &nbsp;|&nbsp;<asp:LinkButton
                ID="lbtnLogout" runat="server" Text="注销" OnClick="lbtnLogout_Click" /></span></div>
    <div class="cPos_a">
        <div class="left" id="divMenu">
            <ul id="sysMenus"> 
            </ul>
        </div>
        <div class="zd">
            <a href="#" style="margin:auto;padding:auto;display:block;">
                <img id="imgShowMenu" src="../img/dot_f.png" onclick="showMenu()" alt=""/>
            </a>
       </div>
        <div class="right" style="background:none;">
            <iframe id="mainframe" class="mainframe" name="mainframe"" src="" frameborder="0" style="background-color:transparent;width:100%;"></iframe>
        </div>
    </div>
    <div class="foot">
        <div>
            <a href="#">
                
            </a><span>Copyright 2012, 版权所有 JITmarketing.CN </span> 
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hid_menu_data" Value="[]" />
    </form>
</body>
</html>
