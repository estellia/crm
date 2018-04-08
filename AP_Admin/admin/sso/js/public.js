$(function(){
	//Í·²¿µ¼º½
	$('.inner_header li').hover(function(){
		$(this).addClass('current').siblings().removeClass('current');
	});
})