define(['jquery', 'tools', 'easyui', 'kkpager', 'artDialog', 'kindeditor'], function ($) {
    KE = KindEditor;
    var page = {
        elems: {
            sectionPage: $("#section"),
            simpleQueryDiv: $("#simpleQuery"),     //简单查询条件的层dom
            allQueryDiv: $("#allQuery"),             //所有的查询条件层dom
            uiMask: $("#ui-mask"),
            tabel: $("#gridTable"),                   //表格body部分
            tabelWrap: $('#tableWrap'),
            thead: $("#thead"),                    //表格head部分
            showDetail: $('#showDetail'),         //弹出框查看详情部分
            operation: $('#opt,#Tooltip'),              //弹出框操作部分
            vipSourceId: '',
            dataMessage: $("#pageContianer").find(".dataMessage"),
            panlH: 116                           // 下来框统一高度
        },
        init: function () {
            this.initEvent();
            this.loadPageData();

        },
        initEvent: function () {
            var that = this;
            //点击查询按钮进行数据查询
            that.elems.sectionPage.delegate(".queryBtn", "click", function (e) {
                //调用设置参数方法   将查询内容  放置在this.loadData.args对象中
                that.setCondition();
                //查询数据
                that.loadData.GetBannerList( function (data) {
                    that.renderTable(data);
                });
                $.util.stopBubble(e);

            });

            //上传按钮初始化
            $("#win").find(".jsUploadBtn").each(function (i, e) {
                debugger;
                that.addUploadImgEvent(e);
            });


            $("#addKVBtn").on("click", function () {
                that.HideLink();
                $('#addkvForm').form('load', { AdId: "", ActivityGroupId: "", TemplateId: "", BannerImageId: null, BannerUrl: "", BannerName: "", DisplayIndex: "", "ActivityGroupName": "" });
                $("#ImageURL").attr("src", "");
                $(".logo").html('<img src="../../styles/images/newYear/imgDefault.png">');

                $('#win').window({ title: "KV新增" });
                $('#win').window('open');
            });

            //保存
            $(".saveBtn").on("click", function () {
                if ($('#addkvForm').form('validate')) {

                    var BannerImage = $("#BannerImageId").val();
                    if (BannerImage != "") {


                        var prams = { action: 'SetBanner' },
                            fields = $('#addkvForm').serializeArray();
                        for (var i = 0; i < fields.length; i++) {
                            var obj = fields[i];
                            prams[obj['name']] = obj['value'];
                        }


                        that.SetBanner(prams, function () {
                            $.messager.alert("提示", "保存成功！");
                            that.loadPageData();
                            $('#win').window('close');
                        });


                    } else {
                        $.messager.alert("提示", "图片为必填！");
                    }


                }
            })

            //上架、下架
            that.elems.tabelWrap.on( "click",".upperlowerbtn", function () {
                that.SetBannerStatus($(this).data("adid"), $(this).data("status"), function () {
                    that.loadPageData();
                });
            });

            //编辑
            that.elems.tabelWrap.on("click", ".editbtn", function () {
                var rowIndex = $(this).data("index");
                var data= $("#gridTable").datagrid('getRows');
                debugger;
                if (data[rowIndex]) {
                    debugger;
                    if (data[rowIndex].ActivityGroupName == null) {
                        that.ShowLink();
                    } else {
                        that.HideLink();
                    }
                    $("#ImageURL").attr("src", data[rowIndex].ImageUrl);
                    $(".logo").html('<img src="' + data[rowIndex].ImageUrl + '"  style="max-width:100%;max-height:100%;" />');
                }
                debugger;
                $('#addkvForm').form('load', data[rowIndex]);

                if (data[rowIndex].ActivityGroupName == null)
                {
                    $('#ActivityGroup').combobox("setValue",null);
                }
                $('#win').window({ title: "KV编辑" });
                $('#win').window('open');
            });

            //删除
            that.elems.tabelWrap.on("click", ".deletebtn", function () {
                that.DeleteBanner($(this).data("adid"), function () {
                    that.loadPageData();
                });
            });
           
        },

        SetObjectImages: function (ImageId, ImageURL, callback) {//修改全年计划图片
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                data: {
                    action: 'SetObjectImages',
                    ImageId: ImageId,
                    ImageURL: ImageURL,
                    IsHomePage: 0
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        $.messager.alert("提示",data.Message);

                    }
                }
            });
        },




        //设置查询条件   取得动态的表单查询参数
        setCondition: function () {
            debugger;
            var that = this;
            //查询每次都是从第一页开始
            that.loadData.args.start = 0;
            var fileds = $("#seach").serializeArray();
            $.each(fileds, function (i, filed) {
                that.loadData.seach[filed.name] = filed.value;
            });
        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            var wd = 200, H = 32;
            //搜索状态
            $('#status').combobox({
                width: wd,
                height: H,
                panelHeight: that.elems.panlH,
                valueField: 'key',
                textField: 'value',
                data: [
                    {
                        "key": "0",
                        "value": "全部"
                    }, {
                        "key": "10",
                        "value": "待上架"
                    }, {
                        "key": "20",
                        "value": "待发布"
                    }, {
                        "key": "30",
                        "value": "已发布"
                    }, {
                        "key": "40",
                        "value": "已下架"
                    }
                ]
            });

            //主题类型
            that.loadData.GetSysMarketingGroupTypeList(function (Data) {
                if (Data.SysMarketingGroupTypeDropDownList != null) {
                    Data.SysMarketingGroupTypeDropDownList.push({ ActivityGroupId: null, Name: "广告" });
                    //主题类型
                    $('#ActivityGroup').combobox({
                        width: wd,
                        height: H,
                        panelHeight: that.elems.panlH,
                        valueField: 'ActivityGroupId',
                        textField: 'Name',
                        data: Data.SysMarketingGroupTypeDropDownList,
                        onSelect: function (param) {
                            if (param) {
                                $("#kvname").val('');
                                if (param.ActivityGroupId == null) {
                                    that.ShowLink();

                                } else {
                                    that.HideLink();
                                    
                                }
                            }
                        }
                    });
                }
            });
            

            that.GetLEventTemplateDropDownList(function (data) {

                var wd = 200, H = 32;
                $('#Activity').combobox({
                    width: wd,
                    height: H,
                    panelHeight: that.elems.panlH,
                    valueField: 'TemplateId',
                    textField: 'TemplateName',
                    data: data.LEventTemplateDropDownList,
                    onSelect: function (param) {
                        if (param) {
                            $("#kvname").val(param.TemplateName);
                        }
                    }
                });
            });


            $(that.elems.sectionPage.find(".queryBtn").get(0)).trigger("click");
            $.util.stopBubble(e);
        },
        //显示广告地址
        ShowLink: function () {
            $(".linkaddress").show();
            $(".kvActivity").hide();
            $("#linkaddress").validatebox({ required: true });
            $("#Activity").combobox({ required: false });
        },
        //隐藏广告地址
        HideLink: function () {
            $("#linkaddress").validatebox({ required: false });
            $("#Activity").combobox({ required: true });
            $(".linkaddress").hide();
            $(".kvActivity").show();
        },
        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that = this;
            if (!data.BannerInfoList) {

                data.BannerInfoList = [];
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({

                method: 'post',
                iconCls: 'icon-list', //图标
                singleSelect: false, //多选
                // height : 332, //高度
                fitColumns: true, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped: true, //奇偶行颜色不同
                collapsible: true,//可折叠
                //数据来源
                data: data.BannerInfoList,
                sortName: 'brandCode', //排序的列
                /*sortOrder : 'desc', //倒序
                 remoteSort : true, // 服务器排序*/
                idField: '', //主键字段
                /*  pageNumber:1,*/
                /* frozenColumns : [ [ {
                 field : 'brandLevelId',
                 checkbox : true
                 } //显示复选框
                 ] ],*/
                frozenColumns: [],
                columns: [[
                    {
                        field: 'BannerName', title: 'KV名称', width: 100, align: 'left', resizable: false
                       

                    },
                    {
                        field: 'ActivityGroupName', title: '主题类型', width: 125, align: 'left', resizable: false,
                        formatter: function (value, row, index) {
                            if (value ==null) {
                                return "广告";
                            } else {
                                return value;
                            }
                        }
                    },
                    { field: 'DisplayIndex', title: '显示顺序', width: 80, resizable: false, align: 'center' },
                    
                    {
                        field: 'Status', title: 'KV状态', width: 80, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                         
                            var staus;
                            switch (value) {
                                case 10: staus = "待上架"; break;
                                case 20: staus = "待发布"; break;
                                case 30: staus = "已发布"; break;
                                case 40: staus = "已下架"; break;
                                default: staus = "未上架"; break;
                            }
                            return staus;
                        }
                    },
                    {
                        field: 'AdId', title: '操作', width: 80, align: 'left', resizable: false,
                        formatter: function (value, row, index) {
                      
                            var staus;
                            switch (row.Status) {
                                case 10: staus = "<a href='javascript:void(0)' data-adid=" + value + " data-status='20' class='upperlowerbtn'> 上架</a>"; break;

                                case 20: staus = "<a href='javascript:void(0)'  data-adid=" + value + "  data-status='40'  class='upperlowerbtn'> 下架</a>"; break;
                                case 30: staus = "<a href='javascript:void(0)'  data-adid=" + value + "  data-status='40'  class='upperlowerbtn'> 下架</a>"; break;
                                case 40: staus = "<a href='javascript:void(0)'  data-adid=" + value + "  data-status='20'  class='upperlowerbtn'> 上架</a>"; break;
                                default: staus = "<a href='javascript:void(0)' data-adid=" + value + "  data-status='20'  class='upperlowerbtn'> 上架</a>"; break;
                            }
                            return staus + "<a href='javascript:void(0)' data-adid=" + value + " data-index=" + index + " class='editbtn'> 编辑</a><a href='javascript:void(0)' data-adid=" + value + " class='deletebtn'> 删除</a>";
                        }
                    }


                ]],

                onLoadSuccess: function (data) {
                    debugger;
                    that.elems.tabel.datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题
                    if (data.rows.length > 0) {
                        that.elems.dataMessage.hide();
                    } else {
                        that.elems.dataMessage.show();
                    }
                }
               

            });



            //分页
            //data.Data = {};
            //data.Data.TotalPageCount = data.TotalCount % that.loadData.args.limit == 0 ? data.TotalCount / that.loadData.args.limit : data.TotalCount / that.loadData.args.limit + 1;
            kkpager.generPageHtml({
                pno: that.loadData.args.PageIndex?that.loadData.args.PageIndex+1:1,
                mode: 'click', //设置为click模式
                //总页码
                total: data.TotalPageCount,
                totalRecords: data.TotalCount,
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

                    that.loadMoreData(n);
                },
                //getHref是在click模式下链接算法，一般不需要配置，默认代码如下
                getHref: function (n) {
                    return '#';
                }

            }, true);


           
        },
        SetBannerStatus: function (AdId, Status, callback) {//修改kv状态
            var that = this;
            if (!AdId) {
                return;
            }
            $.util.ajax({
                url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                data: {
                    action: 'SetBannerStatus',
                    AdId: AdId,
                    Status: Status
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;
                        alert(data.Message);

                    }
                }
            });
        },
        DeleteBanner: function (AdId, callback) {//删除kv数据
            var that = this;
            if (!AdId) {
                return;
            }
            $.util.ajax({
                url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                data: {
                    action: 'DeleteBanner',
                    AdId: AdId
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;

                        $.messager.alert("提示", data.Message); 

                    }
                }
            });
        },
        addUploadImgEvent: function (e) {//图片上传处理
            var that = this;
            this.uploadImg(e, function (ele, data) {
                //上传成功后回写数据
                if ($(ele).parent().parent().siblings("div.logo").length) {
                    $(ele).parent().parent().siblings("div.logo").html('<img src="' + data.url + '"  style="max-width:100%;max-height:100%;" />');  //.data("value", data.thumUrl);
                }

                $("#ImageURL").val(data.url);
                var id = $("#BannerImageId").val();
                that.SetObjectImages(id, data.url, function (_data) {
                    if (id == "") {
                        $("#BannerImageId").val(_data.ImageId);
                    }
                });

            });
        },
        uploadImg: function (btn, callback) {//上传图片
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

                            $.messager.alert("提示", data.Message);
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
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;

            that.loadData.args.PageIndex = currentPage-1;
            that.loadData.GetBannerList( function (data) {
                that.renderTable(data);
            });
        },

        GetLEventTemplateDropDownList: function (callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                data: {
                    action: 'GetLEventTemplateDropDownList',
                    TemplateName:"",
                    ActivityGroupId:"",
                    TemplateStatus: 0,
                    PageIndex: 0,
                    PageSize: 1000
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;

                        $.messager.alert("提示", data.Message);

                    }
                }
            });
        },

        SetBanner: function (param, callback) {
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                data: param,
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        if (callback) {
                            callback(result);
                        }
                    } else {
                        debugger;

                        $.messager.alert("提示", data.Message);

                    }
                }
            });
        },
        loadData: {
            args: {
                bat_id: "1",
                PageIndex: 0,
                PageSize: 10,
                SearchColumns: {},    //查询的动态表单配置
                OrderBy: "",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status: -1,
                page: 1,
                start: 0,
                limit: 10
            },
            seach: {
                name: "",
                status: 0,

            },

            GetBannerList: function (callback ) {
                var that = this;

                if (that.seach.status == "")
                {
                    that.seach.status = 0;
                }
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                    data: {
                        action: 'GetBannerList',
                        BannerName: that.seach.name,
                        Status: that.seach.status,
                        PageIndex: that.args.PageIndex,
                        PageSize: that.args.PageSize
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            var result = data.Data;
                            if (callback)
                            {
                                callback(result);
                            }
                        } else {
                            debugger;

                            $.messager.alert("提示", data.Message);

                        }
                    }
                });
            },

            GetSysMarketingGroupTypeList: function (callback) {
                var that = this;

                if (that.seach.status == "") {
                    that.seach.status = 0;
                }
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                    data: {
                        action: 'GetSysMarketingGroupTypeList'
                    },
                    success: function (data) {
                        if (data.IsSuccess && data.ResultCode == 0) {
                            var result = data.Data;
                            if (callback) {
                                callback(result);
                            }
                        } else {

                            $.messager.alert("提示", data.Message);

                        }
                    }
                });
            }


        }

    };
    page.init();
});