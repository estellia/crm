
var csconfig = csconfig || {};
var csconfig.main = (function () {
    
    var _page_id;

    this.init = function(pid){
    	_page_id = pid || "";
    	pageInit();
    	eventInit();
    };
    function pageInit(){
    	var pcf = {
    		"configList":function(){
                Paginate.init({
                   {    "url":""
                       ,"action":""
                       ,"size":11
                       ,"success":function(data){
                            if(data.code == 200){
                                var tpl = '<tr><td><a href="javascript:;" class="editText">编辑</a></td>{{info}}</tr>';
                                var str ="";
                                var tmp_key = ["a","b","c","d"],tmp;

                                for(var name in data.content){
                                    
                                    tmp = "";
                                    for(var i=0;i<4;i++){
                                       tmp += "<td>"+data.content[name][tmp_key[i]]+"</td>";
                                    }
                                    str += tpl.replace("{{info}}",tmp);
                                        
                                     
                                    }
                                }
                            }
                       }
                    }
                });
    		},
    		"configAdd":function(){

    		}
    	};
    	pcf[_page_id] && pcf[_page_id]();
    }
    function eventInit(){
    	var pcf = {
    		"configList":function(){
    			//pagebar 


    		},
    		"configAdd":function(){
    			$(".commonOutArea .submitBtn").on("click", function () {
				    window.location = "configList.aspx";
				});
    		}
    	};
    	pcf[_page_id] && pcf[_page_id]();
    }

    return this;

})();

var csconfig.getdata = (function(){

    return this;
})();

$(function(){

	csconfig.main.init($(".page")[0].id);
});
	

