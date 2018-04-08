define(['jquery', 'tools','easyui', 'kkpager', 'artDialog'], function ($) {
    var page = {
        elems: {
            sectionPage:$("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel:$("#gridTable"),                   //表格body部分
            tabelWrap:$('#tableWrap'),
            thead:$("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation:$('#opt,#Tooltip'),              //弹出框操作部分
            vipSourceId:'',
            click:true,
            dataMessage:  $("#pageContianer").find(".dataMessage"),
            panlH:116                           // 下来框统一高度
        },
        detailDate:{},
        ValueCard:'',//储值卡号
        select:{
            ToolList:[
                {
                    "InteractionType": 2,
                    "DrawMethodName": "团购",
                    "DrawMethodCode": "TG"
                },
                /*{

                    "InteractionType": 1,
                    "DrawMethodName": "问卷",
                    "DrawMethodCode": "QN"
                },*/
                {
                    "InteractionType": 1,
                    "DrawMethodName": "红包",
                    "DrawMethodCode": "HB"
                },
              /*  {

                    "InteractionType": 2,
                    "DrawMethodName": "热销",
                    "DrawMethodCode": "RX"
                },*/
                {

                    "InteractionType": 2,
                    "DrawMethodName": "抢购",
                    "DrawMethodCode": "QG"
                },
               /* {
                    "InteractionType": 1,
                    "DrawMethodName": "大转盘",
                    "DrawMethodCode": "DZP"
                }*/
            ]
        }
        ,
        init: function () {
            this.initEvent();
            this.loadPageData();

        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
           //


            that.elems.sectionPage.delegate(".queryBtn","click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中

                //查询数据
                var fileds=$("#seach").serializeArray();
                that.loadData.args.pageIndex=0;
                that.loadData.operation(fileds,"select",function(data){
                    //写死的数据
                    //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                    //渲染table

                    that.renderTable(data);


                });
                $.util.stopBubble(e);

            });
            that.elems.operation.delegate(".commonBtn","click",function(e){
               /* var  selectList= that.elems.tabel.datagrid("getSelections");*/
                var  type= $(this).data("flag");
                if(type=="add"){
                    that.selectTool();
                }
            });



            /**************** -------------------弹出easyui 控件 start****************/
            var  wd=200,H=30;
            var list=that.select.ToolList;
            list.push({
                "DrawMethodName": "全部",
                "DrawMethodCode": "-1",
                "selected":true
            });
            $("#EventCode").combobox({
                valueField: 'DrawMethodCode',
                textField: 'DrawMethodName',
                data: list,
            });
            /**************** -------------------弹出easyui 控件  End****************/


            /**************** -------------------弹出窗口初始化 start****************/
            $('#win').window({
                modal:true,
                shadow:false,
                collapsible:false,
                minimizable:false,
                maximizable:false,
                closed:true,
                closable:true
            });
            $('#panlconent').layout({
                fit:true
            });
            $('#win').delegate(".listType span","click",function(e){
                     var code=$(this).data("code");
                switch(code){
                    case "HB":
                        $.util.toNewUrlPath("redPacket.aspx");
                        break;
                    case "TG":
                    case "QG":
                        $.util.toNewUrlPath("sales.aspx?code="+code);
                        break;

                }



            });
            /**************** -------------------弹出窗口初始化 end****************/

            /**************** -------------------列表操作事件用例 start****************/
            that.elems.tabelWrap.delegate(".opt","click",function(e){
                var rowIndex=$(this).data("index");
                var optType=$(this).data("flag");
                that.elems.tabel.datagrid('selectRow', rowIndex);
                var row = that.elems.tabel.datagrid('getSelected');
                if(optType=="exit"){
                    switch(row.EventCode){
                        case "HB":
                            $.util.toNewUrlPath("redPacket.aspx?EventId="+row.EventId);
                            break;
                        case "TG":
                        case "QG":
                            $.util.toNewUrlPath("sales.aspx?code="+row.EventCode+"&EventId="+row.EventId);
                            break;

                    }

                } else {
                    that.loadData.operation(row, optType, function (data) {
                        alert("操作成功");
                        that.loadPageData(e);
                    });
                }


            }) ;
            /**************** -------------------列表操作事件用例 End****************/
        },





        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            $.util.stopBubble(e);
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that=this;
            if(!data.Data.PanicbuyingEventList){
                data.Data["PanicbuyingEventList"]=[];
                return;
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({
                method : 'post',
                iconCls : 'icon-list', //图标
                singleSelect : false, //多选
                // height : 332, //高度
                fitColumns : true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped : true, //奇偶行颜色不同
                collapsible : true,//可折叠
                //数据来源
                data:data.Data.PanicbuyingEventList,
              /*  sortName : 'brandCode', //排序的列
                /!*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*!/
                idField : 'Item_Id', //主键字段
                /!*  pageNumber:1,*!/*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/
                columns : [[

                    {field : 'EventName',title : '活动名称',width:125,align:'left',resizable:false,
                        formatter:function(value ,row,index){
                            var long=56;
                            if(value&&value.length>long){
                                return '<div class="rowText" title="'+value+'">'+value.substring(0,long)+'...</div>'
                            }else{
                                return '<div class="rowText">'+value+'</div>'
                            }
                        }
                    },
                    {field : 'EventCode',title : '活动工具',width:80,align:'center',resizable:false,
                        formatter:function(value ,row,index){
                            $.each(that.select.ToolList,function(index,filed){
                                debugger;
                                if(filed.DrawMethodCode==value){
                                    value= filed.DrawMethodName;
                                }
                            });
                            return value;
                        }
                    } ,



                    {field: 'EventId', title: '操作', width: 30, align: 'left', resizable: false,
                        formatter: function (value, row, index) {
                            var str = "<div class='operation'>";

                            str += "<div data-index=" + index + " data-flag='exit' class='exit  opt' title='编辑'> </div>";
                            str += "<div data-index=" + index + " data-flag='delete' class='delete opt' title='删除'></div></div>";
                            return str
                        }
                    }

                ]],



            });



            //分页
            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex?that.loadData.args.PageIndex+1:1,
                mode: 'click', //设置为click模式
                //总页码
                total: data.Data.TotalPageCount,
                totalRecords:data.Data.TotalCount,
                isShowTotalPage: true,
                isShowTotalRecords: true,
                //点击页码、页码输入框跳转、以及首页、下一页等按钮都会调用click
                //适用于不刷新页面，比如ajax
                click: function (n) {
                    //这里可以做自已的处理
                    //...
                    //处理完后可以手动条用selectPage进行页码选中切换
                    this.selectPage(n);
                    //让  tbody的内容变成加载中的图标
                    //var table = $('table.dataTable');//that.tableMap[that.status];
                    //var length = table.find("thead th").length;
                    //table.find("tbody").html('<tr ><td style="height: 150px;text-align: center;vertical-align: middle;" colspan="' + (length + 1) + '" align="center"> <span><img src="../static/images/loading.gif"></span></td></tr>');

                    that.loadMoreData(n-1);
                },
                //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                getHref: function (n) {
                    return '#';
                }

            }, true);



        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            that.loadData.args.PageIndex = currentPage;
            var fileds=$("#seach").serializeArray();
            that.loadData.operation(fileds,"select",function(data){
                //写死的数据
                //data={"ResultCode":0,"Message":null,"IsSuccess":true,"Data":{"DicColNames":{"UserName":"姓名","Phone":"手机","Email":"邮箱","Col9":"人数","Col8":"职位","Col7":"公司","Col3":"性别"},"SignUpList":[{"SignUpID":"60828091-F8F4-4C97-8F6C-6AC9E627DF97","EventID":"16856b2950892b62473798f3a88ee3e3","UserName":"王孟孟","Phone":"18621865591","Email":"mengmeng.wang@jitmarketing.cn","Col9":"1","Col8":"研发总监","Col7":"上海杰亦特有限公司","Col3":"男"}],"TotalCountUn":1,"TotalCountYet":9,"TotalPage":1}};
                //渲染table

                that.renderTable(data);


            });
        },


        //选择工具
      selectTool:function(data){
            var that=this;
            that.elems.optionType="cancel";
          var top=$(document).scrollTop()+130;
            $('#win').window({title:"选择活动工具",width:360,height:280,left:($(window).width()-360)*0.5,top:top});
            //改变弹框内容，调用百度模板显示不同内容
            /*$('#panlconent').layout('remove','center');
            var html=bd.template('tpl_OrderCancel');
            var options = {
                region: 'center',
                content:html
            };
            $('#panlconent').layout('add',options);
            this.loadData.tag.orderID=data.OrderID;*/
            $('#win').window('open');
        },
        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 0,
                PageSize:10,
                SearchColumns:[],    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1,
                page:1,
                start:0,
                limit:15
            },
            operation:function(pram,operationType,callback){
                debugger;
                var prams={data:{action:""}};
                prams.url="/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx";
                //根据不同的操作 设置不懂请求路径和 方法


                prams.data["EventId"]=pram["EventId"];

                switch(operationType){
                    case "select":prams.data.action="GetMarketingList";  //上架
                        $.each(pram, function (index, field) {


                            if(field.value!="-1") {
                                prams.data[field.name] = field.value;
                            }
                        });
                        prams.data["PageIndex"]= this.args.PageIndex;
                        prams.data["PageSize"]= this.args.PageSize;
                        break;
                    case "delete":prams.data.action="DeleteLEvents"; break;
                }


                $.util.ajax({
                    url: prams.url,
                    data:prams.data,
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            if (callback) {
                                callback(data);
                            }

                        } else {
                            alert(data.Message);
                        }
                    }
                });
            }



        }

    };
    page.init();
});

