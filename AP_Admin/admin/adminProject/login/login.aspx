<%@ Page Language="C#" AutoEventWireup="true" Inherits="login_login" Codebehind="login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
<meta name="format-detection" content="telephone=no" />
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
	<title>正念连锁云掌柜管理后台</title>
    <link rel="icon" href="/favicon.ico" type="image/x-icon" /><link rel="shortcut icon" href="/favicon.ico" type="image/x-icon" />
	<link href="styles/reset-pc.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

.bannerBox{height:548px;margin-bottom:55px;background:url(images/login_banner_bg.png) no-repeat center center;}
.bannerBox_bg{width:1010px;height:548px;margin:0 auto;}
.bannerBox_bg .login_box{float:right;width:326px;height:263px;margin:192px 2px 0 0;padding:60px 0 0 60px;background:url(images/login_box_bg.png) no-repeat center center;}
.bannerBox_bg .input_box{width:266px;height:42px;margin-bottom:20px;border:1px solid #e9e9e9;}
.bannerBox_bg .input_box em{float:left;width:42px;height:42px;}
.bannerBox_bg .input_box .input_label{margin-left:46px;}
.bannerBox_bg .input_box input{height:42px;}
.bannerBox_bg .input_box .name_icon{background:#ccc url(images/home_icon.png) no-repeat center center;}
.bannerBox_bg .input_box .user_icon{background:#ccc url(images/user_icon.png) no-repeat center center;}
.bannerBox_bg .input_box .password_icon{background:#ccc url(images/password_icon.png) no-repeat center center;}
.bannerBox_bg .loginBtn{display:block;width:268px;height:50px;margin-bottom:10px;text-align:center;font:18px/50px "黑体";border-radius:3px;background:#81bf24;color:#fff;}
.bannerBox_bg .login_status{display:block;text-indent:30px;font:16px/30px "黑体";background:url(images/checkBox.png) no-repeat left center;color:#666;cursor:pointer;}
.bannerBox_bg .login_status.on{background:url(images/checkBox_on.png) no-repeat left top;}

.server_highlights{width:1010px;margin:0 auto 58px;}
.server_highlights .item_box{float:left;width:25%;}
.server_highlights .item_icon{width:104px;height:139px;margin:0 auto 12px;}
.server_highlights .server01{background:url(images/server01.png) no-repeat 0 0;}
.server_highlights .server02{background:url(images/server02.png) no-repeat 0 0;}
.server_highlights .server03{background:url(images/server03.png) no-repeat 0 0;}
.server_highlights .server04{background:url(images/server04.png) no-repeat 0 0;}
.server_highlights .text_box{width:200px;margin:0 auto;font:14px/18px "Microsoft YaHei";color:#666;}

.footer_info{height:375px;background:#3b3c3a;color:#ccc;}
.footer_info_1010{width:1010px;margin:0 auto;line-height:20px;font-size:13px;}
.footer_info_1010 .item_box{float:left;width:265px;margin-right:100px;}
.footer_info_1010 .item_box.item_box_last{margin-right:0;}
.footer_info_1010 .tit{margin:35px 0 15px 0;font-size:14px;}
.colorO{color:#f47900;}
</style>

	<script type="text/javascript" src="../js/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" src="../js/public.js"></script>
	<!--[if lt IE 7]> 
	<script type="text/javascript" src="../js/DD_belatedPNG_0.0.8a-min.js"></script>
	<script type="text/javascript" src="../js/ie6.js"></script> 
	<script type="text/javascript"> 
		// 设定需要渲染的DOM对象 
		DD_belatedPNG.fix('.png_bg'); 
	</script> 
	<![endif]-->

	<script type="text/javascript">
	function go_login() {
		if (event.KeyCode == 13) {
			$("#<%=btnLogin.ClientID %>").trigger("click");
		}
	}
	</script>
</head>
<body onkeydown="go_login();" onkeypress="go_login();" class="index_wrapper">
	<form id="form1" runat="server" defaultbutton="btnLogin">
		
	<div class="loginPage">
    
    <div class="bannerBox">
    	<div class="bannerBox_bg">
    		<div class="login_box">
            <span class="login_attention colorO"><b><asp:Label ID="lblInfor" runat="server"></asp:Label></b>
					<%--	<b class="none">密码长度必须控制在6-32位</b>--%></span>
                <div class="input_box">
                	<em class="user_icon"></em>
                    <p class="input_label"><input type="text" placeholder="用户名" id="txtUsername" runat="server" /></p>
                </div>
                
                <div class="input_box">
                	<em class="password_icon"></em>
                    <p class="input_label"><input type="password" placeholder="密码" id="txtPassword" runat="server" /></p>
                </div>
                
               <%-- <a id="btnLogin2" runat="server" class="loginBtn" href="javascript:;" onclick="">登录</a>--%>
                <asp:Button id="btnLogin" runat="server" class="loginBtn"  Text="登录"
								OnClick="btnLogin_Click" />
                
               <label style="display:block;"><input type="checkbox" id="chkRemember" runat="server" style="width:20px;height:20px;margin-right:8px;" />保持登录状态</label></div>
            </div>
        </div>
    </div>
    
    <div class="server_highlights">
    	<div class="clearfix">
        	<div class="item_box">
            	<p class="item_icon server01"></p>
                <p class="text_box">利用移动技术实现创新的购物体验和消费者互动。</p>
            </div>
            
            <div class="item_box">
            	<p class="item_icon server02"></p>
                <p class="text_box">发挥线下门店优势，借助微信把线下人流导引到品牌会员中心。</p>
            </div>
            
            <div class="item_box">
            	<p class="item_icon server03"></p>
                <p class="text_box">利用移动互联网技术实现品牌企业的线上、线下融合。</p>
            </div>
            
            <div class="item_box">
            	<p class="item_icon server04"></p>
                <p class="text_box">通过专业的会员数据分析实现精准会员营销。</p>
            </div>
        </div>
    </div>
    
    <div class="footer_info" style="height:100px;">
    	<div class="footer_info_1010 clearfix">
        	<!--<div class="item_box">
            	<p class="tit">关于我们</p>
            	<p>深圳市阿拉丁移动生活科技有限公司是逸马集团旗下的控股公司。总部位于深圳，下辖上海、北京、长沙、青岛、成都等十多家运营分支机构。平台聚集了数百名移动互联网技术专家，再借助逸马集团多年来为联想、苏宁、创维、松下电工等近三万家连锁企业服务的实战经验，为连锁产业提供“全网连锁”O2O辅导方案和平台共享联盟服务。致力于打造一个本地化移动生活社区和亿业连锁商家的聚合平台，为推动连锁商业文明的发展，帮助传统连锁企业实现全网连锁的梦想而“生命不息，战斗不止”!</p><br />
				<p>copyrights©深圳阿拉丁移动科技有限公司</p>
            </div>
            
            <div class="item_box">
            	<p class="tit">深圳</p>
            	<p><img src="images/map01.png" alt="阿拉丁深圳分公司" title="阿拉丁深圳分公司" /></p><br />
				<p>总部电话：400-0323-200<br /> 
	       				   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;0755-66825558<br />
				地  址：深圳市龙岗区龙岗大道（布吉段）3007号（立信路与龙岗大道交叉口）永昌大厦逸马连锁产业基地<br />
                </p>
            </div>
            
            <div class="item_box item_box_last">
            	<p class="tit">上海</p>
            	<p><img src="images/map02.png" alt="阿拉丁上海分公司" title="阿拉丁上海分公司" /></p><br />
				<p>电       话：021- 31083862<br /> 
				   地       址：上海市静安区延平路121号三和大厦6A座<br />
                </p>
            </div>
            -->

            
        </div>
    </div>
    
    
</div>
		
		<input type="hidden" id="hdPwd" runat="server" />
	</form>
</body>
	
<script type="text/javascript">
    $(function () {
        //模拟radio
        //var radio = $('input:checkbox');
        radio_checkbox($('.checkboxBlock input:checkbox'))
        function radio_checkbox(obj) {
            obj.each(function () {
                if (this.checked) {
                    $(this).parent().addClass(this.type + '_check');
                }
            });
            obj.live('click', function () {
                var cur_span = $(this).blur().parent();
                var type = this.type;
                if (this.checked) {
                    cur_span.addClass(type + '_check');
                    if (type == 'radio') {
                        cur_span.parents('.' + type + '_box').find('span').not(cur_span).removeClass(type + '_check');
                    }
                } else {
                    cur_span.removeClass(type + '_check');
                }
            });
        }

        /*----- 表单默认文字的显隐 -----*/
        inputWordShowHide();
        function inputWordShowHide() {
            var type;
            $("input[type='text'],input[type='password']").focus(function () {
                var txtValue = $(this).val();
                type = $(this).attr('type');
                if (type == 'text') {
                    $(this).addClass("inputOn");
                    if (txtValue == "公司编码" || txtValue == "用户名") {
                        $(this).val("");
                        return;
                    }
                } else {
                    $(this).addClass("inputOn");
                    $(this).siblings().addClass('none');
                }
            }).blur(function () {
                var txtValue = $(this).val();
                if (type == 'text') {
                    if (txtValue == "") {
                        $(this).val(this.defaultValue).removeClass("inputOn");
                    }
                } else {
                    if (txtValue == "") {
                        $(this).removeClass("inputOn");
                        $(this).siblings().removeClass('none');
                    }
                }
            });
            if ($("#hdPwd").val() != "") {
                $("#txtCustomerCode").addClass("inputOn");
                $("#txtUsername").addClass("inputOn");
                $("#txtPassword").val($("#hdPwd").val());
                $("#txtPassword").addClass("inputOn");
                $("#txtPassword").siblings().addClass('none');
            }
        }
    })
    function SaveLogin1() {
        if ($('.login_status').hasClass('on')) {
            $('.login_status').removeClass('on').attr('rel', '0');
            $('#chkRemember').attr("checked", false);
        } else {
            $('.login_status').addClass('on').attr('rel', '1');
            $('#chkRemember').attr("checked", true);

        }
        return false;
    }
</script>
</html>
