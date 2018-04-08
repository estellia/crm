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


            that.elems.sectionPage.delegate(".btnText","click", function (e) {
                var imgCode=$(this).data("imgcode");
                $(".phonePanel").find("img[data-imgcode="+imgCode+"]").parent().find("img").hide();
                $(".phonePanel").find("img[data-imgcode="+imgCode+"]").show();
            }).delegate("#getBack","click",function(){
                $.util.toNewUrlPath("queryList.aspx");
            });
            that.elems.sectionPage.delegate("#subMitBtn","click", function (e) {
                if ($('#optionForm').form('validate')) {

                    var fields = $('#optionForm').serializeArray(); //自动序列化表单元素为JSON对象
                   var issubMit=true,ImageIdList=[],messageType=$("#picText").combobox("getValue");//1是文字 2是图片
                    $("[data-imgcode].jsUploadBtn").each(function(){
                        var dom=$(this),name=$(this).data("imgcode"),value="",msg=$(this).data("msg");
                        var obj=$(this).data("imgObj");
                        if(!(messageType==1&&name=="RuleImageUrl")) {
                            if (obj && obj.ImageID) {
                                ImageIdList.push(obj.ImageID)
                            }else{
                                $.messager.alert("提示",msg+"未上传");
                                issubMit=false;
                                return false;
                            }
                        }

                    });

                  debugger;
                     if(issubMit) {
                         fields.push({name:"ImageIdList",value:ImageIdList});
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
            $("#picText").combobox({
                onSelect: function (record) {
                    debugger;
                    if (record.label == "1") {
                        $(".picTextPanel .lineText").hide(0);
                        $(".picTextPanel textarea").show(0);
                        $(".picTextPanel textarea").validatebox({
                            novalidate:false
                        })
                    } else if (record.label == "2") {
                        $(".picTextPanel .lineText").show(0);
                        $(".picTextPanel textarea").hide(0);
                        $(".picTextPanel textarea").validatebox({
                            novalidate:true
                        })
                    }
                }
            })


        },

        //加载页面的数据请求
        loadPageData: function (e) {
            function imgLoad(me,obj){
                if(me.is("img")){
                    me.attr("src",obj.imageUrl)
                }else if(me.hasClass("jsUploadBtn")){
                    me.data("imgObj",obj);
                } else if(!me.hasClass("btnText")){
                    me.css({"backgroundImage":'url('+obj.imageUrl+')' });
                }
            }
            this.loadData.operation("","getInfo",function(data){
                $("#optionForm").form("load",data.Data.LEventsData);

                var imgListData=data.Data,url="",imageId="";
                var obj={};//logo
                debugger;
                $('[data-imgcode="LogoImageUrl"]').each(function(){
                    obj={imageUrl:imgListData.LogoImageUrl,ImageID:imgListData.LogoImageId};//logo
                    var me=$(this);
                    imgLoad ($(this),obj)
                });
                $('[data-imgcode="BackGroundImageUrl"]').each(function(){
                    obj={imageUrl:imgListData.BackGroundImageUrl,ImageID:imgListData.BackGroundImageId};//背景
                    var me=$(this);
                    imgLoad ($(this),obj)
                });
                $('[data-imgcode="NotReceiveImageUrl"]').each(function(){
                    var me=$(this);
                    obj={imageUrl:imgListData.NotReceiveImageUrl,ImageID:imgListData.NotReceiveImageId};//未领取图片
                    imgLoad ($(this),obj)
                });
                $('[data-imgcode="ReceiveImageUrl"]').each(function(){
                    obj={imageUrl:imgListData.ReceiveImageUrl,ImageID:imgListData.ReceiveImageId};//领取图片
                    var me=$(this);
                    imgLoad ($(this),obj)
                });
                if( data.Data.LEventsData){
                    $("#picText").combobox("setValue","1")
                }else{
                    $("#picText").combobox("setValue","2")
                    $('[data-imgcode="RuleImageUrl"]').each(function(){
                        obj={imageUrl:imgListData.RuleImageUrl,ImageID:imgListData.RuleImageId};//规则图片图片
                        var me=$(this);
                        imgLoad ($(this),obj)
                    });
                }

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
                    var fileds = [{name: "imageUrl", value: data.url}];

                    var obj = $(ele).data("imgObj");
                if(obj) {
                    $.each(obj, function (name, value) {
                        if (name != "imageUrl") {
                            fileds.push({name: name, value: value});
                        }

                    });
                }
                    var dom= $(".spreadPanel").find("[data-imgcode="+imgCode+"]");
                    if(dom.is("img")){
                        $(".spreadPanel").find("[data-imgcode="+imgCode+"]").attr("src",data.url);
                    } else{
                        $(".spreadPanel").find("[data-imgcode="+imgCode+"]").css({"backgroundImage":'url('+data.url+')' });
                    }


                    that.loadData.args.bat_id = BatId;
                    that.loadData.operation(fileds, "setImg", function (data) {
                        debugger;
                        if(!obj){obj={}}
                        $.each(fileds, function (index, filed) {
                            if (filed.name != "imageUrl") {
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
                        prams.data.action = "SetLEvents";         //保存红包和编辑红包
                        prams.data["EventCode"]="HB";//红包
                        break;
                    case  "setImg":                              //设置图片id
                        prams.data.action = "SetObjectImages";
                        prams.data["BatId"] = this.args.bat_id;
                        break;
                    case "getInfo":
                        prams.data.action="GetLEvents";

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

