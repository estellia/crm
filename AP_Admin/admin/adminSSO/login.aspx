 <%@ Page Language="C#" AutoEventWireup="true" Inherits="Login" CodeBehind="Login.aspx.cs" %>

<!doctype html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="format-detection" content="telephone=no">
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport">

    <title>连锁掌柜-商户登录</title>
    <link href="css/style.css?v3" rel="stylesheet" type="text/css" />
    <script src="js/jquery.js" type="text/javascript"></script>


</head>
<body>
    <form id="form1" runat="server">
        <img id="loginBgImg" src="images/bg-login.jpg" alt="登录背景" />
        <div class="loginArea">
            <h2 class="title">欢迎登录连锁掌柜</h2>
            <p style="color: red; text-align: center; height: 33px; line-height: 33px;">
                <asp:Label ID="lblInfor" runat="server"></asp:Label>
            </p>
            <div class="formBox" id="queryTable">
                <p class="inputBox">
                    <input type="text" runat="server" class="LoginText" value="xgx" runat="server" id="txtCustomerCode" placeholder="请输入商户编码"  />
                </p>
                <p class="inputBox">
                    <input type="text" runat="server" class="LoginText" value="admin" id="txtUsername" runat="server" placeholder="请输入用户名" />
                </p>
                <p class="inputBox pwBox">
                    <input type="password" name="password" runat="server" class="LoginText" id="txtPassword" runat="server" placeholder="请输入密码"  value="123456"/><%--<span class="forgetPw">忘记密码</span>--%>
                </p>
                <p class="loginBtnBox">
                    <asp:LinkButton ID="btnLogin" style='text-decoration:none;' runat="server" class="loginBtn" OnClick="btnLogin_Click">登录</asp:LinkButton>
                </p>
                <p class="recordPwBox"><a href="javascript:;" style='text-decoration:none;' class="recordPw" id="Select" >记住密码</a></p>
                <input id="chkRemember" type="checkbox" name="checkbox2" value="checkbox" hidden="hidden" runat="server" />
            </div>
        </div>


        <!-- header -->
        <%-- 
	    <div class="header-top">
    	    <div class="header-btnWrap">
        	    <a href="javascript:;" class="loginBtn on">登录<em></em></a>
                <a href="register.html" class="">注册试用<em></em></a>
            </div>
        </div>
       
        <div class="form-login-area">
		<div class="erwm">
                     <img src="images/erwm.png" width="116" height="116">
                     <p><img src="images/erwmzi.png"></p>

         </div>
		
            <div style=" font-weight:100; text-align:left; color:red;">
	            <b><asp:Label ID="lblInfor" runat="server" style="color:red"></asp:Label></b>
	        </div>
    	    <div class="inputBox commercialBox">
        	    <em></em>
                <p>
             <input type="text" runat="server" class="LoginText" value="" runat="server" id="txtCustomerCode" placeholder="输入公司编码"  />
                </p>
            </div>
            <div class="inputBox userBox">
        	    <em></em>
                <p>
                    <input type="text" runat="server" class="LoginText" value="" id="txtUsername" runat="server"   placeholder="用户名"/>
                </p>
            </div>
            <div class="inputBox pwBox">
        	    <em></em>
                <p>
                    <input type="password" runat="server" class="LoginText" id="txtPassword" runat="server" placeholder="密码" />
                </p>
            </div>
            <div class="skin saveLoginStateNo" rel="0" onClick="SaveLogin(this)" >
	            
	        </div>
            <div class="pwStatus">
        	    <a class="forgetPw" href="javascript:;">忘记密码？</a>
                <p class="keepStatus saveLoginStateNo"  onClick="SaveLogin(this)">保持登录状态</p>
                <input type="checkbox" id="chkRemember" runat="server" style="filter:alpha(opacity=0);opacity:0;"  />
            </div>
        
            <asp:ImageButton id="btnLogin" runat="server" class="loginBtn"  OnClick="btnLogin_Click" src="images/btnLogin.png" />
        
        </div>
        --%>






        <div class="footer" style="display: none;">
            <div class="common" style="overflow: hidden">

                <div class="footerDiv1 fldisplay" style="margin-right: 0; width: 256px;">
                    <h2 class="footerDivTitle3 skin">contact us</h2>
                    <div style="padding-top: 20px;">
                        <div class="inputDiv skin">
                            <span class="tip" style="left: 5px;">姓名</span>
                            <input type="text" id="PUserName" runat="server" class="inputText" onkeypress="InputKeyDown(this)" onblur="InputBlur(this)" value="" />
                        </div>
                        <div class="blank1" style="height: 10px;"></div>
                        <div class="inputDiv skin">
                            <span class="tip" style="left: 5px;">公司名称</span>
                            <input type="text" id="PCompany" runat="server" class="inputText" onkeypress="InputKeyDown(this)" onblur="InputBlur(this)" value="" />
                        </div>
                        <div class="blank1" style="height: 10px;"></div>
                        <div class="inputDiv skin">
                            <span class="tip" style="left: 5px;">公司邮箱</span>
                            <input type="text" id="PEmail" runat="server" class="inputText" onkeypress="InputKeyDown(this)" onblur="InputBlur(this)" value="" />
                        </div>
                        <div class="blank1" style="height: 10px;"></div>
                        <div class="inputDiv skin">
                            <span class="tip" style="left: 5px;">公司总机+分级</span>
                            <input type="text" id="PTel" runat="server" class="inputText" onkeypress="InputKeyDown(this)" onblur="InputBlur(this)" value="" />
                        </div>
                        <div class="blank1" style="height: 10px;"></div>
                        <div class="inputDiv skin">
                            <span class="tip" style="left: 5px;">手机号</span>
                            <input type="text" id="PPhone" runat="server" class="inputText" onkeypress="InputKeyDown(this)" onblur="InputBlur(this)" value="" />
                        </div>
                        <div class="blank1" style="height: 10px;"></div>
                        <div class="inputDiv skin">
                            <span class="tip" style="left: 5px;">行业</span>
                            <input type="text" id="PIndustry" runat="server" class="inputText" onkeypress="InputKeyDown(this)" onblur="InputBlur(this)" value="" />
                        </div>

                        <div class="inputBtn" style="text-align: right;">
                            <div style="float: right;">
                                <%--    <asp:ImageButton id="btnSend" runat="server" class="skin BtnSend" style="clear:both" ImageUrl="images/send.png" OnClick="btnSend_Click" />
                                --%>
                                <input type="hidden" id="hMsg" value="" runat="server" />
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" id="hdPwd" runat="server" />
    </form>
    <script type="text/javascript">
        (function () {
            var $recordPw = $('.recordPw');
            $recordPw.on('click', function () {
                var $this = $(this);
                if ($this.hasClass('on')) {
                    $this.removeClass('on');
                    document.getElementById("chkRemember").checked = false;
                } else {
                    $this.addClass('on');
                    document.getElementById("chkRemember").checked = true;
                }
            })

            function Show() {
                var $check = $('#chkRemember');
                if ($check.val() == 'true') {
                    $recordPw.addClass('on');
                } else {
                    $recordPw.removeClass('on');
                }
            }

            window.onload = Show();

        }());

        $(window).bind("keydown", function (e) {
            // 兼容FF和IE和Opera    
            var theEvent = e || window.event;
            var code = theEvent.keyCode || theEvent.which || theEvent.charCode;
            if (code == 13) {
                //回车执行查询
                document.getElementById("btnLogin").click();
            }
        });
    </script>
    <script type="text/javascript">
        function InputGetFocus(id) {
            $("#" + id).focus();
        }
        function InputKeyDown(o) {
            var getValue = $(o).val();

            if (getValue.length >= 0) {
                $(o).siblings(".tip").hide();

            }
        }
        function InputBlur(o) {
            var getValue = $(o).val();
            if (getValue.length == 0) {
                $(o).siblings(".tip").show();
            }
        }
        function SaveLogin(o) {
            if ($(o).hasClass("saveLoginStateNo")) {
                $(o).removeClass("saveLoginStateNo").addClass("on").attr("rel", "1");
                $("#chkRemember").attr("checked", true);

            } else {
                $(o).removeClass("on").addClass("saveLoginStateNo").attr("rel", "0");
                $("#chkRemember").attr("checked", false);
            }
        }
        $(function () {
            if ($("#txtCustomerCode").val() != "") {
                $("#txtCustomerCode").siblings(".tip").hide();
            }
            if ($("#txtUsername").val() != "") {
                $("#txtUsername").siblings(".tip").hide();
            }
            if ($("#txtPassword").val() != "") {
                $("#txtPassword").siblings(".tip").hide();
            }
        })

        function InputKeyDownAll(o) {
            if (document.getElementById("PUserName").value.length > 0)
                InputKeyDown(document.getElementById("PUserName"));
            if (document.getElementById("PCompany").value.length > 0)
                InputKeyDown(document.getElementById("PCompany"));
            if (document.getElementById("PEmail").value.length > 0)
                InputKeyDown(document.getElementById("PEmail"));
            if (document.getElementById("PTel").value.length > 0)
                InputKeyDown(document.getElementById("PTel"));
            if (document.getElementById("PPhone").value.length > 0)
                InputKeyDown(document.getElementById("PPhone"));
            if (document.getElementById("PIndustry").value.length > 0)
                InputKeyDown(document.getElementById("PIndustry"));
        }
    </script>
    <div style="display: none;">

        <script>
            var _hmt = _hmt || [];
            (function () {
                var hm = document.createElement("script");
                hm.src = "//hm.baidu.com/hm.js?ffcf7959dd7df4ebbec8d064c18d8ec4";
                var s = document.getElementsByTagName("script")[0];
                s.parentNode.insertBefore(hm, s);
            })();
        </script>
    </div>
</body>
</html>
