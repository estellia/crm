/*
注意autocomplete.showlist 的使用 
autocomplete.search()，autocomplete.liOnClick()，autocomplete.showUL()都可以重写
autocomplete.search 默认情况下读取JSON 对象 rows（对象数组） 属性，根据返回的数据自己处理成autocomplete.showlist 对象数组  
autocomplete.liOnClick 默认情况给搜索框赋值 以及 autocomplete.selectValue 赋值，根据需求自己处理
autocomplete.showUL 默认情况显示autocomplete.showlist中对象的showtxt属性 可以autocomplete.showlist中对象的更多属性
作者：郝磊
版本：v0.4.8 
兼容性：IE 6,7,8,Chrome,Opera,FF
最后修改日期 2012-2-15
*/

//配置项
var autocomplete = {
    whatermarkin: function () { }, //文本框失去焦点
    whatermarkout: function () { }, //文本框得到焦点 
    whatermarkstring: "", //水印内容
    selectValue: null,                           //点击或者回车后所选中的值 
    searchboxid: "Tbx_SearchBar",                //搜索框
    showboxid: "ShowBox",                        //显示UL
    searchUrl: "InfoSearch.aspx",                //查询地址  
    searchpara: null,                              //动态查询条件应为function类型
    finish: function () { },                     //点击或者回车后，处理selectValue 的方法
    licolor: "#ffffff",                          //li背景色
    lihover: "#D5E2FF",                          //li选中要页面
    Ishighlight: false,                          //允许高亮
    highcolor: "blue",                           //高亮文字颜色
    IsFade: true,                                //展开/渐变       
    showlist: [],                                //showlist是JSON对象集合，查询返回集合 如果是XML或者字符串+分隔符数据请重写 Search ，
    //json 数组第一个属性为要显示内容， 其他属性自己定义，可以在展开列表显示更丰富的内容.
    //例如：autocomplete.showlist = [{ showtxt: "abc1",id:"1" }, { showtxt: "abc2",id:"2" }, { showtxt: "abc3",id:"3" }, { showtxt: "abc4", id: "4" } ];
    //第一个属性是要显示属性，如果需要显示更丰富的内容请重写BuildLiStr 方法
    //----------------------------------------------------------------------
    //以下为单页面多文本框autocomplete支持 配置 数组和方法要都要对应
    ismulittbx: false,      //flase 单文本框，true 多文本框
    mulit_tbxlist: [],      //["Tbx_SearchBar", "Tbx_SearchBar1", "Tbx_SearchBar2"]
    mulit_urllist: [],      //["InfoSearch.aspx", "InfoSearch.aspx", "InfoSearch.aspx"]
    mulit_finish: [],       //方法类别不同 方法处理selectValue的值[function () { }, function () { }, function () { } ]
    mulit_watermarkstring: [], //多文本框水印
    activesearchbox: 1,
    scrollitemcount: 10
};
//showbox 样式初始化
autocomplete.SetShowBoxCss = function () {
    var searchBarPosition = autocomplete.searchBarObj.position();
    autocomplete.showBoxObj = $("#" + autocomplete.showboxid);
    autocomplete.showBoxObj.css("display", "none")
    autocomplete.showBoxObj.css("font-size", "14px");
    autocomplete.showBoxObj.css("padding", "0");
    autocomplete.showBoxObj.css("list-style", "none");
    autocomplete.showBoxObj.css("position", "absolute");
    autocomplete.showBoxObj.css("margin", "0");
    autocomplete.showBoxObj.css("overflowX", "hidden");
    autocomplete.showBoxObj.css("display", "none");
    autocomplete.showBoxObj.css("border", "1px solid silver");
    autocomplete.showBoxObj.css("top", (searchBarPosition.top + autocomplete.searchBarObj.height() + 1 + parseInt(autocomplete.searchBarObj.css("padding-top").replace(/px/g, "")) + parseInt(autocomplete.searchBarObj.css("padding-bottom").replace(/px/g, "")) + parseInt(autocomplete.searchBarObj.css("margin-top").replace(/px/g, "").replace(/auto/, "0"))));
    autocomplete.showBoxObj.css("left", searchBarPosition.left);
    autocomplete.showBoxObj.css("min-width", (autocomplete.searchBarObj.width() + parseInt(autocomplete.searchBarObj.css("padding-left").replace(/px/g, "")) + parseInt(autocomplete.searchBarObj.css("padding-right").replace(/px/g, ""))));
    autocomplete.showBoxObj.css("margin-left", autocomplete.searchBarObj.css("margin-left"));
    autocomplete.showBoxObj.css("margin-right", autocomplete.searchBarObj.css("margin-right"));
    autocomplete.showBoxObj.css("z-index", "9999");
    autocomplete.showBoxObj.mouseover(function () { autocomplete.IsMouse = true });
    autocomplete.showBoxObj.mouseout(function () { autocomplete.IsMouse = false });
}
//li 变色
autocomplete.UL_hover = function (index, Istxtshow) {
    var list_li = autocomplete.showBoxObj.children("li");

    list_li.css("background", autocomplete.licolor);
    for (var i = 0; i < list_li.length; i++) {
        if (i == index) {
            list_li.eq(index).css("background", autocomplete.lihover);
            if (list_li.length > 10 && Istxtshow)
                autocomplete.showBoxObj.scrollTop(20 * index);
            if (Istxtshow) {
                for (var key in autocomplete.showlist[i]) { autocomplete.currentValue = autocomplete.showlist[i][key]; break; }
                autocomplete.searchBarObj.val(autocomplete.currentValue);
            }
            else { autocomplete.showboxselectindex = index; }
        }
    }
}
//创建列表  加入高亮文章
autocomplete.showUL = function () {
    if (autocomplete.showlist.length != 0) {
        autocomplete.showBoxObj.html(autocomplete.BuildLiStr());
        autocomplete.UL_LiInitial();
        if (autocomplete.IsFade) {
            if (autocomplete.IsFade && autocomplete.showBoxObj.css("display") == "none")
                autocomplete.showBoxObj.slideDown("normal");
            autocomplete.showBoxObj.hide();
            autocomplete.showBoxObj.fadeIn("slow");
        }
        //autocomplete.showBoxObj.css("display", "block");
    }
    else autocomplete.showBoxObj.hide();

}
//Builder Li标记
autocomplete.BuildLiStr = function () {
    var stringBuffer = [];
    for (var i = 0; i < autocomplete.showlist.length; i++) {
        var maintxt;
        for (var key in autocomplete.showlist[i]) { maintxt = autocomplete.showlist[i][key]; break; }
        var reg = new RegExp(autocomplete.searchValue, "gim");
        if (autocomplete.Ishighlight) maintxt = autocomplete.RegChange(maintxt, reg);
        var str = String.Format("<li flag='{0}' >{1}</li>", i, maintxt);
        stringBuffer.push(str);
    }
    return stringBuffer.join("");
}
//高亮正则替换
autocomplete.RegChange = function (inputtext, checkreg) {
    var findtext; var findexlist = []; var replacelist = []; var replacetext;
    replacetext = inputtext;
    findtext = checkreg.exec(inputtext);
    while (findtext != null) {
        if (findexlist.join("").indexOf(findtext[0]) < 0) {
            findexlist.push(findtext[0]);
            var replacereg = new RegExp(findtext[0], "gm");
            replacelist.push("<span class='maintxt'>" + findtext[0] + "</span>");
            replacetext = replacetext.replace(replacereg, "{" + (replacelist.length - 1) + "}");
        }
        findtext = checkreg.exec(inputtext);
    }
    return String.Format(replacetext, replacelist);
}


//Li 初始化（背景，设置高亮颜色,事件）
autocomplete.UL_LiInitial = function () {
    var list_li = autocomplete.showBoxObj.children();
    list_li.mouseenter(function () {
        autocomplete.UL_hover($(this).attr("flag"), false);
    });
    if (list_li.length > autocomplete.scrollitemcount) {
        autocomplete.showBoxObj.css("height", "200px");
        if ($.browser.msie) { autocomplete.showBoxObj.css("overflowY", "auto"); }
        else { autocomplete.showBoxObj.css("overflow", "auto"); }
    } else {
        autocomplete.showBoxObj.css("height", "auto");
        if ($.browser.msie) { autocomplete.showBoxObj.css("overflowY", "hidden"); }
        else { autocomplete.showBoxObj.css("overflow", "hidden"); }
    }
    list_li.css("background", autocomplete.licolor);
    list_li.css("width", "auto");
    list_li.css("white-space", "nowrap");
    list_li.css("height", "auto");
    list_li.css("line-height", "20px");
    list_li.css("color", "Black");
    list_li.css("cursor", "pointer");
    list_li.mousedown(function () {
        if (!autocomplete.ismulittbx) autocomplete.liOnClick(autocomplete.showboxselectindex, autocomplete.finish);
        else autocomplete.liOnClick(autocomplete.showboxselectindex, autocomplete.mulit_finish[autocomplete.activesearchbox]);
    })
    if (autocomplete.Ishighlight) { for (var i = 0; i < list_li.length; i++) { var b_list = list_li.eq(i).children(".maintxt"); b_list.css("color", autocomplete.highcolor); b_list.css("font-weight", "bold"); } }
}
autocomplete.hidshowbox = function () { autocomplete.showBoxObj.html(""); autocomplete.showBoxObj.css("display", "none"); }
//上|下|回车
autocomplete.dealKeycode = function (event) {
    var list_li = autocomplete.showBoxObj.children();
    if (list_li.length > 0) {
        var myEvent = event || window.event;
        var keycode = myEvent.keyCode;
        if (keycode == 38 || keycode == 40) {
            if (keycode == 38) { autocomplete.showboxselectindex = --autocomplete.showboxselectindex; } //上                
            if (keycode == 40) { autocomplete.showboxselectindex = ++autocomplete.showboxselectindex; } //下
            if (autocomplete.showboxselectindex > list_li.length - 1) { autocomplete.showboxselectindex = -1; }
            if (autocomplete.showboxselectindex < -1) { autocomplete.showboxselectindex = list_li.length - 1; }
            if (autocomplete.showboxselectindex == -1) {
                autocomplete.currentValue = autocomplete.searchValue;
                autocomplete.searchBarObj.val(autocomplete.searchValue);
                autocomplete.searchBarObj.focus();
                autocomplete.UL_hover(autocomplete.showboxselectindex, true);
            }
            else { autocomplete.UL_hover(autocomplete.showboxselectindex, true); }
        }
        //回车
        if (keycode == 13) {
            if (!autocomplete.ismulittbx) {
                if (autocomplete.showboxselectindex >= 0) autocomplete.liOnClick(autocomplete.showboxselectindex, autocomplete.finish);
                else if (typeof (autocomplete.finish) == "function") autocomplete.finish();
            }
            else {
                if (autocomplete.showboxselectindex >= 0) autocomplete.liOnClick(autocomplete.showboxselectindex, autocomplete.mulit_finish[autocomplete.activesearchbox]);
                else if (typeof (autocomplete.mulit_finish[autocomplete.activesearchbox]) == "function") autocomplete.mulit_finish[autocomplete.activesearchbox]();
            }
            event.originalEvent.cancelBubble = true;
        }
    }
}
//点击事件  触发finishfunction 方法  autocomplete.selectValue  autocomplete.showlist[为选择索引位置] 的对象
autocomplete.liOnClick = function (index, finishfunction) {
    var obj = autocomplete.showlist[index];
    if (obj == null || obj == undefined) {
        return;
    }
    var maintxt;
    for (var key in obj) { maintxt = obj[key]; break; }
    autocomplete.searchBarObj.val(maintxt);
    autocomplete.currentValue = maintxt;
    autocomplete.selectValue = obj;
    try { var range = autocomplete.searchBarObj[0].createTextRange(); range.move('character', range.text.length); range.select(); } catch (e) { }
    autocomplete.searchBarObj.focus();
    autocomplete.hidshowbox();
    if (typeof (finishfunction) == "function")
        finishfunction();
}
//查询方法返回json类型{ rows:[{ showtxt: "abc1",id:"1" }, { showtxt: "abc2",id:"2" }, { showtxt: "abc3",id:"3" }, { showtxt: "abc4", id: "4" } ]}
//或者直接返回json数组[{ 字段1: "abc1",id:"1" }, { 字段1: "abc2",id:"2" }, { 字段1: "abc3",id:"3" }, { 字段1: "abc4", id: "4" } ]
autocomplete.search = function () {
    if (autocomplete.IsFocus && autocomplete.searchBarObj != null) {
        if (autocomplete.currentValue == autocomplete.searchBarObj.val())
            return;
        autocomplete.showboxselectindex = -1;
        autocomplete.selectValue = null;
        autocomplete.showlist = [];
        autocomplete.currentValue = autocomplete.searchBarObj.val();
        if (autocomplete.currentValue.trim() == "") { autocomplete.hidshowbox(); return; }
        autocomplete.searchValue = autocomplete.currentValue;
        //post|get 自己修改
        var paras = { key: autocomplete.currentValue };
        if (autocomplete.searchpara != null) {
            paras = autocomplete.searchpara();
            paras.key = autocomplete.currentValue
        }
        $.post(autocomplete.searchUrl, paras, function (data) {
            if (data.toString() != "") {
                autocomplete.showboxselectindex = -1;
                try {
                    var obj = eval('(' + data.toString() + ')');
                }
                catch (e) { return alert("返回数据结构错误，请参考查询方法注释和showlist注释"); }
                if (obj instanceof Array) {
                    autocomplete.showlist = obj;
                }
                else {
                    for (var key in obj) { autocomplete.showlist = obj[key]; break; }

                }
                if (autocomplete.showlist.length > 0) autocomplete.showUL();
                else alert("无法解析返回的数据结构，请参考autocomplete.search方法注释和autocomplete.showlist方法注释");
            }
            else autocomplete.hidshowbox();
        })
    }
}

autocomplete.CheckShowId = function () {
    return autocomplete.showboxid == "" || autocomplete.showboxid == null || autocomplete.showboxid == undefined || $("#" + autocomplete.showboxid)[0] == null;
}
//初始化
autocomplete.Initial = function () {
    if (autocomplete.CheckShowId()) { autocomplete.showboxid = "ShowBox"; if ($("#" + autocomplete.showboxid)[0] == null) $("body").append("<ul id='ShowBox' class='ShowBox'></ul>"); }
    if (!autocomplete.ismulittbx) {
        autocomplete.searchBarObj = $("#" + autocomplete.searchboxid);
        if (autocomplete.whatermarkstring.trim() != "") {
            autocomplete.searchBarObj.val(autocomplete.whatermarkstring);
            autocomplete.searchBarObj.css("color", "Silver");
        }
        autocomplete.currentValue = autocomplete.searchBarObj.val();
        autocomplete.IsFocus = true;
        autocomplete.searchBarObj.focusin(function () { autocomplete.IsFocus = true; if (typeof (autocomplete.whatermarkin) == 'function') autocomplete.whatermarkin(); }); //得到焦点
        autocomplete.searchBarObj.focusout(function () { autocomplete.IsFocus = false; if (!autocomplete.IsMouse) { autocomplete.hidshowbox(); if (typeof (autocomplete.whatermarkout) == 'function') autocomplete.whatermarkout(); } }); //失去焦点
        autocomplete.SetShowBoxCss();
        autocomplete.searchBarObj.keydown(function (event) { autocomplete.dealKeycode(event); });
    }
    else {
        if (typeof (autocomplete.mulit_tbxlist) == "object" && autocomplete.mulit_tbxlist.length > 0) {
            if (autocomplete.mulit_tbxlist.length == autocomplete.mulit_urllist.length && autocomplete.mulit_urllist.length == autocomplete.mulit_finish.length) {
                for (var i = 0; i < autocomplete.mulit_tbxlist.length; i++) {

                    $("#" + autocomplete.mulit_tbxlist[i]).focusin(function () {
                        autocomplete.IsFocus = true;
                        for (var i = 0; i < autocomplete.mulit_tbxlist.length; i++) {
                            if ($(this).attr("id") == autocomplete.mulit_tbxlist[i]) {
                                autocomplete.activesearchbox = i;
                                autocomplete.searchBarObj = $("#" + autocomplete.mulit_tbxlist[autocomplete.activesearchbox]);
                                autocomplete.SetShowBoxCss();
                                autocomplete.currentValue = autocomplete.searchBarObj.val();
                                autocomplete.searchUrl = autocomplete.mulit_urllist[autocomplete.activesearchbox];
                                autocomplete.whatermarkstring = autocomplete.mulit_watermarkstring[autocomplete.activesearchbox];
                                autocomplete.searchBarObj.unbind("keydown");
                                autocomplete.searchBarObj.keydown(function (event) { autocomplete.dealKeycode(event); });
                            }
                        }
                    }); //得到焦点
                    $("#" + autocomplete.mulit_tbxlist[i]).focusout(function () { autocomplete.IsFocus = false; if (!autocomplete.IsMouse) autocomplete.hidshowbox(); }); //失去焦点
                }
                autocomplete.IsFocus = true;
            }
            else {
                alert("设置了错误的多文本框参数,请确认文本框链接数,触发出发方法数是否相同");
            }
        }
        else alert("设置了错误的多文本框参数");
    }

    if (typeof (autocomplete.search) == "function") autocomplete.currentInterval = setInterval("autocomplete.search()", 500);
}