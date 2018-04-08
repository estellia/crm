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
            panlH: 116,                           // 下来框统一高度
            last_Index: -1,
            isinserted:false
        },
        init: function () {
            this.initEvent();
            this.loadPageData();

        },
        initEvent: function () {
            var that = this;
            
            $("#addbtn").on("click", function () {
                if (!that.elems.isinserted) {
                    that.elems.isinserted = true;
                    that.elems.tabel.datagrid('endEdit', that.elems.last_Index);
                    that.elems.tabel.datagrid('appendRow', {
                        PlanDate: '',
                        PlanName: '',
                        SeasonPlanId: ""
                    });
                    that.elems.last_Index = that.elems.tabel.datagrid('getRows').length - 1;
                    that.elems.tabel.datagrid('selectRow', that.elems.last_Index);
                    that.elems.tabel.datagrid('beginEdit', that.elems.last_Index);
                } else {
                    $.messager.alert("提示", "新增的数据还有未提交！");
                }
            });
           
            //数据编辑
            that.elems.tabelWrap.on("click", ".editbtn", function () {
                if (!that.elems.isinserted) {
                    var rowIndex = $(this).data("index");
                    if (that.elems.last_Index != rowIndex) {
                        that.elems.tabel.datagrid('endEdit', that.elems.last_Index);
                        that.elems.tabel.datagrid('beginEdit', rowIndex);
                        that.elems.tabel.datagrid('selectRow', rowIndex);
                        $(this).parents("._opt").find(".editopt").hide();
                        $(this).parents("._opt").find(".submitopt").show();

                    }
                    that.elems.last_Index = rowIndex;
                } else {
                    $.messager.alert("提示", "新增的数据还有未提交！");
                }
            });

            //数据删除
            that.elems.tabelWrap.on("click", ".deletebtn", function () {

                var rowIndex = $(this).data("index");
                var SeasonPlanId = $(this).data("id");
                var date = $(this).data("date");
                $.messager.confirm('是否删除', '确认删除' + date + '?', function (r) {
                    if (r) {
                        that.DeleteSeasonPlan(SeasonPlanId, function () {
                            that.elems.tabel.datagrid('deleteRow', rowIndex);

                            that.loadPageData();

                        });
                    }
                });
                
                
            });


            //数据取消
            that.elems.tabelWrap.on("click", ".cancelbtn", function () {

                if (that.elems.isinserted) {
                    that.elems.isinserted = false;
                }
                var rowIndex = $(this).data("index");
                if (rowIndex == null) {
                    that.elems.tabel.datagrid('rejectChanges');
                } else {
                    that.elems.tabel.datagrid('cancelEdit', rowIndex);
                    that.elems.last_Index = -1;
                    $(this).parents("._opt").find(".editopt").show();
                    $(this).parents("._opt").find(".submitopt").hide();
                }
            });

            //数据提交
            that.elems.tabelWrap.on("click", ".submitbtn", function () {
                debugger;
                var rowIndex = $(this).data("index");
                var data = null;
                if (rowIndex!=null) {
                    that.elems.tabel.datagrid('endEdit', rowIndex);
                    data = that.elems.tabel.datagrid('getChanges', "updated");
                } else {

                    that.elems.tabel.datagrid('endEdit', that.elems.last_Index);
                    data = that.elems.tabel.datagrid('getChanges', "inserted");
                }
                if (data.length > 0) {
                    if (data[0].PlanDate == "" || data[0].PlanName == "") {
                        return 
                    } 

                    that.SetSeasonPlan(data[0].PlanDate, data[0].PlanName, data[0].SeasonPlanId, function () {
                        
                        that.loadPageData();
                    });
                    
                }
                that.elems.last_Index= -1;
            });

            //上传按钮初始化
            $("#section").find(".jsUploadBtn").each(function (i, e) {
                that.addUploadImgEvent(e);
            });

        },




        //设置查询条件   取得动态的表单查询参数
        setCondition: function () {
            debugger;
            var that = this;
            //查询每次都是从第一页开始
            that.loadData.args.start = 0;
            var fileds = $("#seach").serializeArray();
           
        },

        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;
            that.loadData.GetSeasonPlanList(function (data) {
                that.renderTable(data);
            });

            that.GetHomePageCommon(function (data) {
                if (data.HomePageCommon) {
                    $("#BannerImageId").val(data.HomePageCommon.ImageId)
                }
            });
            $.util.stopBubble(e);
        },

        //渲染tabel
        renderTable: function (data) {
            debugger;
            var that = this;
            var lastIndex;
            debugger;
            if (!data.SeasonPlanList) {

                data.SeasonPlanList = [];
            }
            //jQuery easy datagrid  表格处理
            that.elems.tabel.datagrid({

                method: 'post',
                iconCls: 'icon-list', //图标
                singleSelect: false, //多选
                width: 500,
                // height : 332, //高度
                fitColumns: false, //自动调整各列，用了这个属性，下面各列的宽度值就只是一个比例。
                striped: true, //奇偶行颜色不同
                collapsible: true,//可折叠
                //数据来源
                data: data.SeasonPlanList,
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
                        field: 'PlanDate', title: '活动日期', width: 200, editor: {
                            type: 'datebox', options: {required:true}
                        }, align: 'left', resizable: false
                    },
                    {
                        field: 'PlanName', title: '活动名称', width: 200, editor:{
                            type: 'validatebox', options: {required:true}
                        },  align: 'left', resizable: false
                    },
                    {
                        field: 'SeasonPlanId', title: '', width: 100, align: 'center', resizable: false,
                        formatter: function (value, row, index) {
                            if (value == "") {//新增
                                var str = "<a href='javascript:void(0)'  class='editbtn editopt' data-id=" + value + " data-index=" + index + "  style='display:none;' > 编辑</a>";
                                str += "<a href='javascript:void(0)' class='deletebtn editopt' data-id=" + value + " data-index=" + index + "  style='display:none;'> 删除</a>";
                                str += "<a href='javascript:void(0)'  class='submitbtn submitopt' data-id=" + value + " data-index=" + index + "  > 提交</a>";
                                str += "<a href='javascript:void(0)'  class='cancelbtn submitopt' data-id=" + value + " data-index=" + index + "  > 取消</a>";
                            } else {//编辑
                                var str = "<a href='javascript:void(0)' class='editbtn editopt' data-id=" + value + " data-index=" + index + "  > 编辑</a>";
                                str += "<a href='javascript:void(0)' class='deletebtn editopt' data-date=" + row.PlanDate + " data-id=" + value + " data-index=" + index + "  > 删除</a>";
                                str += "<a href='javascript:void(0)'  class='submitbtn submitopt' data-id=" + value + " data-index=" + index + " style='display:none;'  > 提交</a>";
                                str += "<a href='javascript:void(0)'  class='cancelbtn submitopt' data-id=" + value + " data-index=" + index + " style='display:none;' > 取消</a>";
                            }
                           
                            str = "<span class='_opt'>" + str + "</span>";
                            return str;
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

           
        },
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            this.loadData.args.start = (currentPage - 1) * 15;
            that.loadData.GetSeasonPlanList(function (data) {
                that.renderTable(data);
            });
        },
        addUploadImgEvent: function (e) {//图片上传处理
            var that = this;
            this.uploadImg(e, function (ele, data) {
                $("#ImageURL").val(data.url);
                var id = $("#BannerImageId").val();
                that.SetObjectImages(id, data.url, function (_data) {
                    $.messager.alert("提示","图片上传成功！");
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

        DeleteSeasonPlan: function (SeasonPlanId, callback) {//删除计划
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                data: {
                    action: 'DeleteSeasonPlan',
                    SeasonPlanId: SeasonPlanId
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

        SetSeasonPlan: function (PlanDate, PlanName, SeasonPlanId, callback) {//编辑计划
            var that = this;
            if (SeasonPlanId == "") {
                that.elems.isinserted = false;
            }
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                data: {
                    action: 'SetSeasonPlan',
                    PlanDate: PlanDate,
                    PlanName: PlanName,
                    SeasonPlanId: SeasonPlanId
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

        SetObjectImages: function (ImageId, ImageURL, callback) {//修改全年计划图片
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                data: {
                    action: 'SetObjectImages',
                    ImageId: ImageId,
                    ImageURL: ImageURL,
                    IsHomePage: 1
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

        GetHomePageCommon: function ( callback) {//获取计划图片
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                data: {
                    action: 'GetHomePageCommon'
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
                limit: 15
            },

            GetSeasonPlanList: function (callback) {
                var that = this;
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                    data: {
                        action: 'GetSeasonPlanList',
                        PageIndex: that.args.PageIndex,
                        PageSize: that.args.PageSize
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
            }


        }

    };
    page.init();
});