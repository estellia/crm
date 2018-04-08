
var Ajax = {
	
	buildAjaxpInfo:function(param){
			
			var _param = {
				type: "post",
				dataType: "json",
				url: "",
				data: null,
				beforeSend: function () {
					//UI.Loading('SHOW');
				},
				success: null,
				error: function (XMLHttpRequest, textStatus, errorThrown){
					//UI.Loading("CLOSE");
				}
			};
			
			$.extend(_param,param);

			//var baseInfo = this.getBaseAjaxParam();
			var baseInfo = {};
			
			var _data = {
				'action':param.data.action,
				'ReqContent':JSON.stringify({
					'common':(param.data.common?$.extend(baseInfo,param.data.common):baseInfo),
					'special':(param.data.special?param.data.special:param.data)
				})
			};
			
			_param.data = _data;
			
			return _param;
		},
		ajax:function(param){

			var _param;

			_param = this.buildAjaxParams(param);
			
			$.ajax(_param);
		}
};

var Paginate = (function(params){  //params:{"url":"","action":"","size":11,"success":function(){}}

	var page_curr = 0;
	var page_total = 0;
	var pInfo;

	this.init = function(params){

		pInfo = params || {};
		this.pageRefresh();
		eventInit();
	}	
	this.pageRefresh = function(){  //参数
		Ajax.ajax({
			url:pInfo["url"],
			data:{
				'action':pInfo["action"],
				'page_index': page_curr,
				'page_size': pInfo["size"]
			},
			success:function(data){
				
				params["success"](data);
			}
		});
	}	
	function eventInit(){
		$(".pageBarWrap a").on("click",function(){
			if($(this).hasClass("disable")){
				return false;
			}
			if($(this).hasClass("pageUp")){
				// pageUp
				if(page_curr){
					page_curr--;
					this.pageRefresh();
				}

				page_curr && $(this).addClass("disable");
			}else{
				// pageDown
				if(page_curr-page_total<0){
				 	page_curr++;
				 	this.pageRefresh();
				}
				(page_curr-page_total<0) && $(this).addClass("disable");

			}
		})
	}

})();