define(['jquery', 'tools','easyui','kindeditor', 'artDialog'], function ($) {
    //上传图片
    KE = KindEditor;
    var page = {
        elems: {
            sectionPage: $("#section"),
            panlH: 116                           // 下来框统一高度
        },
        detailDate: {},
        ValueCard: '',//储值卡号
        select: {
            isSelectAllPage: false,                 //是否是选择所有页面
            tagType: [],                             //标签类型
            tagList: []                              //标签列表
        },
        init: function () {
            if($.util.getUrlParam("code")=="TG"){
                $('[data-tipname="EvnetName"]').html("团购名称：");
                $('[data-tipname="ImageUrl"]').html("团购kv图片：");
            }else{
                $('[data-tipname="EvnetName"]').html("抢购名称：");
                $('[data-tipname="ImageUrl"]').html("抢购kv图片：");
                $('[data-tipname="ImageUrl"]').html("抢购kv图片：");
            }
            this.initEvent();
            debugger;
            var eventId= $.util.getUrlParam("EventId");
            if(eventId){
                this.loadData.args.EventId=eventId;
                this.loadPageData()
            }


        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询


            that.elems.sectionPage.delegate("#getBack","click",function(){
                $.util.toNewUrlPath("queryList.aspx");
            });
            that.elems.sectionPage.delegate("#subMitBtn","click", function (e) {
                if ($('#optionForm').form('validate')) {

                    var fields = $('#optionForm').serializeArray(); //自动序列化表单元素为JSON对象
                   var issubMit=true,ImageIdList=[];//
                    $("[data-imgcode].jsUploadBtn").each(function(){
                        var dom=$(this),name=$(this).data("imgcode"),value="",msg=$(this).data("msg");
                        var obj=$(this).data("imgObj");

                            if (obj && obj.ImageID) {
                                ImageIdList.push(obj.ImageID);//多张的 图片处理 一张图片去第一项
                            }else{
                                $.messager.alert("提示",msg+"未上传");
                                issubMit=false;
                                return false;
                            }


                    });

                  debugger;
                     if(issubMit) {
                         fields.push({name:"ImageId",value:ImageIdList[0]});
                         that.loadData.operation(fields, "add", function (data) {
                            alert("操作成功");
                             $.util.toNewUrlPath("queryList.aspx");

                         });
                     }
                }
            });

            that.elems.sectionPage.find(".jsUploadBtn").each(function (i, e) {
                that.addUploadImgEvent(e);

            });



        },

        //加载页面的数据请求
        loadPageData: function (e) {
            this.loadData.operation("","getInfo",function(data){
                var img=data.Data.PanicbuyingEventInfoData.ImageUrl;
                $(".spreadPanel").find("img[data-imgcode]").attr("src",img);
                var obj={ImageID:data.Data.PanicbuyingEventInfoData.ImageId,ImageUrl:data.Data.PanicbuyingEventInfoData.ImageUrl};
                $('[data-imgcode].jsUploadBtn').data("imgObj",obj);
                $("#optionForm").form("load",data.Data.PanicbuyingEventInfoData);
            });
        },
        uploadImg: function (btn, callback) {
            setTimeout(function () {
                var uploadBtn = KE.uploadbutton({
                    width: "100%",
                    button: btn,
                    //上传的文件类型
                    fieldName: 'imgFile',
                    //注意后面的参数，dir表示文件类型，width表示缩略图的宽，height表示高
                    url: '/ApplicationInterface/kindeditor/asp.net/upload_homepage_json.ashx?dir=image',
                    afterUpload: function (data) {
                        if (data.error === 0) {
                            if (callback) {
                                callback(btn, data);
                            }
                            //取返回值,注意后台设置的key,如果要取原值
                            //取缩略图地址
                            //var thumUrl = KE.formatUrl(data.thumUrl, 'absolute');

                            //取原图地址
                            //var url = KE.formatUrl(data.url, 'absolute');
                        } else {
                            alert(data.message);
                        }
                    },
                    afterError: function (str) {
                        alert('自定义错误信息: ' + str);
                    }
                });
                uploadBtn.fileBox.change(function (e) {
                    uploadBtn.submit();
                });
            }, 10);

        },
        addUploadImgEvent: function (e) {
            var that = this;
            this.uploadImg(e, function (ele, data) {
                //上传成功后回写数据

                    debugger;
                    that.loadData.args["ImageUrl"] = data.url;
                    var imgCode = $(ele).data("imgcode"), BatId = $(ele).data("batid");
                    // $(ele).data("imgObj").html('<img src="' + data.url + '"  style="max-width:100%;max-height:100%;" />');  //.data("value", data.thumUrl);
                    var fileds = [{name: "ImageUrl", value: data.url}];

                    var obj = $(ele).data("imgObj");
                if(obj) {
                    $.each(obj, function (name, value) {
                        if (name != "ImageUrl") {
                            fileds.push({name: name, value: value});
                        }

                    });
                }
                    var dom= $(".spreadPanel").find("[data-imgcode="+imgCode+"]");
                    if(dom.is("img")){
                        $(".spreadPanel").find("[data-imgcode="+imgCode+"]").attr("src",data.url);
                    } else if(!dom.hasClass("jsUploadBtn")){
                        $(".spreadPanel").find("[data-imgcode="+imgCode+"]").css({"backgroundImage":'url('+data.url+')' });
                    }


                    that.loadData.args.bat_id = BatId;
                    that.loadData.operation(fileds, "setImg", function (data) {
                        debugger;
                        if(!obj){obj={}}
                        $.each(fileds, function (index, filed) {
                            if (filed.name != "ImageUrl") {
                                obj[filed.name] = filed.value;
                            }
                        });
                        obj["ImageID"] = data.Data.ImageId;
                        $(ele).data("imgObj", obj);
                    });



            });
        },



        loadData: {
            args: {
                bat_id: "",
                EventId:'',
                PageIndex: 0,
                PageSize: 10,
                SearchColumns: {}    //查询的动态表单配置

            },
            operation: function (pram, operationType, callback) {
                debugger;
                var prams = {data: {action: ""}};
                prams.url = "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx";
                //根据不同的操作 设置不懂请求路径和 方法


                prams.data.EventId =this.args.EventId;
                $.each(pram, function (index, field) {
                    if (field.value !== "" || field.value != "-1") {
                        prams.data[field.name] = field.value;
                    }
                });
                switch (operationType) {
                    case  "add":
                        prams.data.action = "SetPanicbuyingEvent";         //保存红包和编辑红包
                        prams.data["EventCode"]= $.util.getUrlParam("code");//团购，秒杀
                        break;
                    case  "setImg":                              //设置图片id
                        prams.data.action = "SetObjectImages";
                        break;
                    case "getInfo":
                        prams.data.action="GetPanicbuyingEvent";

                        break;
                }


                $.util.ajax({
                    url: prams.url,
                    data: prams.data,
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

