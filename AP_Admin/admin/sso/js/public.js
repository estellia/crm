$(function(){
	//ͷ������
	$('.inner_header li').hover(function(){
		$(this).addClass('current').siblings().removeClass('current');
	});
})