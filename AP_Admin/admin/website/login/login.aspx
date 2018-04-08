<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>登录</title>
	<link rel="stylesheet" type="text/css" href="../css/basic.css" />
	<link rel="stylesheet" type="text/css" href="../css/common.css" />
	<link rel="stylesheet" type="text/css" href="../css/theme.css" />
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
		
	<!--main start-->
	<div class="main index_main clearfix">
		<!--inner_main end-->
		<div class="inner_main">
			<p class="clearfix pt10"><a href="#" title="回到首页"><img src="../images/logo.png" class="png_bg fl" /></a></p>
			<img src="../images/ad.png" class="png_bg fl index_ad" />
			<!--登录框 start-->
			<div class="login_wrap png_bg fr">
				<ul>
					<li class="login_attention colorO">
						<b><asp:Label ID="lblInfor" runat="server"></asp:Label></b>
						<b class="none">密码长度必须控制在6-32位</b>
					</li>
					<li class="username"><input type="text" id="txtUsername" runat="server" value="用户名" /></li>
					<li class="password"><input type="password" id="txtPassword" runat="server" /> <span>密码</span></li>
					<li class="remember_password">
						<span class="checkboxBlock"><input type="checkbox" id="chkRemember" runat="server" /></span>
						<span class="agree">保持登录状态</span>
					</li>
					<li>
						<a href="#">
							<asp:ImageButton id="btnLogin" runat="server" ImageUrl="../images/btn-login.jpg"
								OnClick="btnLogin_Click" />
						</a>
					</li>
				</ul>
			</div>
			<!--登录框 end-->
			
			<!--列表 start-->
			<div class="index_item_list clearfix cb">
				<dl>
					<dt>销售管理</dt>
					<dd>
						第一时间内快速实时地采集终端的销售和市场基础数据....
						<p><img src="../images/img/index-img-1.jpg" /></p>
					</dd>
				</dl>
				<dl>
					<dt>促销管理</dt>
					<dd>
						全面掌握促销员的排班，上下班考勤工作，监控促销员... 
						<p><img src="../images/img/index-img-2.jpg" /></p>
					</dd>
				</dl>
				<dl>
					<dt>终审考核</dt>
					<dd>
						实时监测终端品牌分销，货架占有率，促销活动等信息... 
						<p><img src="../images/img/index-img-3.jpg" /></p>
					</dd>
				</dl>
				<dl>
					<dt>商务智能</dt>
					<dd>
						信息完美呈现，表格，图表，地图以及仪表盘... 
						<p><img src="../images/img/index-img-4.jpg" /></p>
					</dd>
				</dl>
			</div>
			<!--列表 end-->
		</div>
		<!--inner_main end-->
	</div>
	<!--main end-->
	
	<!--footer start-->
		<div class="footer tc">上海杰亦特信息科技有限公司版权所有</div>
	<!--footer end--> 
		
		<input type="hidden" id="hdPwd" runat="server" />
	</form>
</body>
	
<script type="text/javascript">
$(function(){ 
	//模拟radio
	//var radio = $('input:checkbox');
	radio_checkbox($('.checkboxBlock input:checkbox'))
	function radio_checkbox(obj){
		obj.each(function(){
			if(this.checked){
				$(this).parent().addClass(this.type+'_check');
			}	
		});
		obj.live('click',function(){
			var cur_span = $(this).blur().parent();
			var type = this.type;
			if(this.checked){
				cur_span.addClass(type+'_check');
				if(type=='radio'){
					cur_span.parents('.'+type+'_box').find('span').not(cur_span).removeClass(type+'_check');	
				}
			}else{
				cur_span.removeClass(type+'_check');
			}
		});
	}
	
	/*----- 表单默认文字的显隐 -----*/
	inputWordShowHide();
	function inputWordShowHide(){
		var type;
		$("input[type='text'],input[type='password']").focus(function(){
			var txtValue = $(this).val();
			type = $(this).attr('type');
			if(type == 'text'){
				$(this).addClass("inputOn");
				if (txtValue == "公司编码" || txtValue == "用户名") {
					$(this).val("");
					return;
				}
			}else{
					$(this).addClass("inputOn");
					$(this).siblings().addClass('none');	
				}
		}).blur(function(){
				var txtValue = $(this).val();
				if(type == 'text'){
					if(txtValue == ""){
						$(this).val(this.defaultValue).removeClass("inputOn");
					}
				}else{
						if(txtValue == ""){
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
</script>
</html>
