var appConfig = {
    staticUrl:'/Module/static/js/',
    cache: document.body.hasAttribute("cache"),
    version: document.body.hasAttribute("cache") && document.body.hasAttribute("version") ? document.body.getAttribute("version") : Math.floor(new Date().getTime() / (1000 * 60 * 60 * 12))
    }

// 路径定义
require.config({
    urlArgs: "v=" + (appConfig.cache ? appConfig.version : (new Date()).getTime()),
    baseUrl:'',   //window.staticUrl ? window.staticUrl:'http://static.uat.chainclouds.com'
    shim: {
        'touchslider': {
            exports: 'touchslider'
        },
        'pagination': {
            deps: ['jquery'],
            exports: 'pagination'
        },
        'highcharts': {
            deps: ['jquery'],
            exports: 'highcharts'
        },
        'tools': {
            deps: ['jquery'],
            exports: 'tools'
        },
        'mobileScroll': {
            deps: ['jquery'],
            exports: 'mobileScroll'
        },
        'highchartsMore':{
            deps: ['highcharts'],
            exports: 'highchartsMore'
        },
        'highchartsExporting':{
            deps: ['highcharts','highchartsMore'],
            exports: 'highchartsExporting'
        },
        'easyui':{
            deps: ['jquery'],
            exports: 'easyui'
        }


    },
   
    paths: {

        // lib
        jquery:appConfig.staticUrl+'lib/jquery-1.8.3.min',
        tools:appConfig.staticUrl+'lib/tools-lib',
        easyui:appConfig.staticUrl+'lib/jquery.easyui.min',
        artDialog: appConfig.staticUrl+'plugin/artDialog',
        // plugin
        kkpager:appConfig.staticUrl+'plugin/kkpager',
        jsonp:appConfig.staticUrl+'plugin/jquery.jsonp',
        mustache:appConfig.staticUrl+'plugin/mustache',
        touchslider: appConfig.staticUrl+'plugin/touchslider',//轮播工具
        template: appConfig.staticUrl+'plugin/template', //artTemplate,模板工具  微官网开发有应用，可参考，好处是完全面向对象，js方法完全外部处理，方便维护开发。
        bdTemplate:appConfig.staticUrl+'plugin/bdTemplate',//百度模板，暂时保留，不要使用
        kindeditor: appConfig.staticUrl+'plugin/kindeditor',//百度编辑器
        pagination: appConfig.staticUrl+'plugin/jquery.jqpagination',//jquery  ui分页插件，暂时保留，不要使用。
        highcharts: appConfig.staticUrl+'plugin/highcharts',//线性图表开发。
        highchartsMore: appConfig.staticUrl+'plugin/highcharts-more', //线性图表开发。
        highchartsExporting: appConfig.staticUrl+'plugin/highcharts-exporting' //线性图表开发。
    }
});

define(['jquery'], function($) {
    var pageJs = $("#section").data("js"),
        pageJsPrefix = '';
    if (pageJs.length) {
        var arr = pageJs.split(" ");
        for (var i = 0; i < arr.length; i++) {
            arr[i] = pageJsPrefix + arr[i];
        }
        require([arr.join(",")], function () { });
    }

});


