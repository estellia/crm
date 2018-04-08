$(function(){   
    var mainHeight = $(window).height()-46;
    $('.main').height(mainHeight+'px');
	
	$(window).resize(function(){
		var mainHeight = $(window).height()-46;
		$('.main').height(mainHeight+'px');					  
	});
})