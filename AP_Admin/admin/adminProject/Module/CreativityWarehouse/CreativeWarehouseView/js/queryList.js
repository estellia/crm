define(['jquery', 'bdTemplate', 'tools', 'easyui', 'artDialog', 'touchslider'], function ($) {
    var page = {
        elems: {
        },
        init: function () {
            this.initEvent();
            this.loadPageData();
        },
        initEvent: function () {
            var that = this,
				$notShow = $('.nextNotShow span');
			$notShow.on('click',function(){
				if($notShow.hasClass('on')){
					$notShow.removeClass('on');
				}else{
					$notShow.addClass('on');
				}
			})
            
			$(".releasebtn").on("click", function () {
			    $.messager.alert("提示","发布成功！");
			    that.SetLEventTemplateReleaseStatus(function () {
			        that.loadPageData();
			    });
			});

            //查看全年计划
			$(".Annualplanbtn").on("click", function () {


			    $("#Annualplanlayer").show();

			    var h = $(".Annualplan").height();
			    var w = $(".Annualplan").width();
			    $(".planlayer").css("margin-top", -(h / 2));
			    $(".planlayer").css("margin-left", -(w / 2));
            });

            $("#Annualplanlayer").on("click", function () {

                $("#Annualplanlayer").hide();
            });

            $(".close").on("click", function () {

                $("#Annualplanlayer").hide();
            });
            //查看全年计划end


            $(".Theme").on("click", function () {
                $(".NextSeasonList").hide();
                $("." + $(this).data("showclass")).show();
                $(".Theme img").each(function () {
                    var imgsrc = $(this).data("img");
                    $(this).attr("src", imgsrc);
                });
                debugger;
                var onimgsrc = $(this).find("img").data("onimg");
                $(this).find("img").attr("src", onimgsrc);
            });

           
            $('#win').delegate(".saveBtn", "click", function (e) {
                $('#win').window('close');
            });
           
            
        },




        //设置查询条件   取得动态的表单查询参数
        setCondition:function(){
            var that=this;
            var fileds=$("#seach").serializeArray();
            $.each(fileds,function(i,filed){
                //filed.value=filed.value=="0"?"":filed.value;
                //that.loadData.seach[filed.name]=filed.value;
                that.loadData.seach.form[filed.name]=filed.value;
            });
        },
		
        //加载页面的数据请求
        loadPageData: function (e) {
            debugger;
            var that = this;

            //获取全年计划图片
            that.GetHomePageCommon(function (data) {
                $(".Annualplan").attr("src", data.ImageUrl);
               
            });

            that.GetPreview(function (data) {
                if (data)
                {
                    if (data.BannerPreviewList) {
                        $(".InSeasonList").append(bd.template("tpl_InSeasonList", data));
                        //赋值导航start
                        var navhtml = "<span class='thisseasontouchslider-prev'></span>";
                        for (i = 0; i < data.BannerPreviewList.length; i++) {
                            if (i == 0)
                                navhtml += " <span class='thisseasontouchslider-nav-item thisseasontouchslider-nav-item-current'></span>";
                            else
                                navhtml += " <span class='thisseasontouchslider-nav-item '></span>";
                        }
                        navhtml += "<span class='thisseasontouchslider-next'></span>";

                        $(".thisseasontouchslider-nav").html(navhtml);
                        //赋值导航end

                        $(".touchsliderthisseason").touchSlider({
                            container: this,
                            duration: 350, // 动画速度
                            delay: 3000, // 动画时间间隔
                            margin: 5,
                            mouseTouch: true,
                            namespace: "touchslider",
                            next: ".thisseasontouchslider-next", // next 样式指定
                            pagination: ".thisseasontouchslider-nav-item",
                            currentClass: "thisseasontouchslider-nav-item-current", // current 样式指定
                            prev: ".thisseasontouchslider-prev", // prev 样式指定
                            autoplay: true, // 自动播放
                            viewport: ".thisseasontouchslider-viewport"
                        });
                    }

                    //即将上线
                    if (data.SeasonPlanPreviewList)
                    {
                        $(".seasonlist_ul").html(bd.template("tpl_seasonlist", data));
                    }
                    
                    //节假日主题营销
                    if (data.HolidaySysMarketingGroupTypePreview)
                    {
                        data.activityList = data.HolidaySysMarketingGroupTypePreview.HolidayLEventTemplatePreview;
                        $(".HolidayLEventTemplatePreview").html(bd.template("tpl_NextSeasonList", data));
                    }
                    //爆款商品
                    if (data.ProductSysMarketingGroupTypePreview) {
                        data.activityList = data.ProductSysMarketingGroupTypePreview.ProductLEventTemplatePreview;
                        $(".ProductLEventTemplatePreview").html(bd.template("tpl_NextSeasonList", data));
                    }
                   //门店活动
                    if (data.UnitSysMarketingGroupTypePreview) {
                        data.activityList = data.UnitSysMarketingGroupTypePreview.UnitLEventTemplatePreview;
                        $(".UnitLEventTemplatePreview").html(bd.template("tpl_NextSeasonList", data));
                    }
                }
            });

           
        },
		
		
        //加载更多的资讯或者活动
        loadMoreData: function (currentPage) {
            var that = this;
            
           
        },
        GetPreview: function ( callback) {//获取创意预览
                var that = this;
                $.util.ajax({
                    url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                    data: {
                        action: 'GetPreview'
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
        GetHomePageCommon: function (callback) {//获取全年计划图片
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
        SetLEventTemplateReleaseStatus: function (callback) {//发布
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/CreativityWarehouse/CreativityWarehouseHandle.ashx",
                data: {
                    action: 'SetLEventTemplateReleaseStatus'
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

       
        //下季活动列表
        NextSeasonList: function (callback) {
            debugger;
            var that = this;
            $.util.ajax({
                url: "/ApplicationInterface/Gateway.ashx",
                data: {
                    action: 'CreativityWarehouse.Theme.NextSeasonList'
                },
                success: function (data) {
                    if (data.IsSuccess && data.ResultCode == 0) {
                        var result = data.Data;
                        
                        if (result) {
                            if (callback) {
                                callback(result);
                            }
                        }

                    } else {
                        debugger;
                        alert(data.Message);

                    }
                },
                complete: function () {
                    that.elems.submitstate = false;
                }
            });
        },
       
       
        loadData: {
            args: {
                bat_id:"1",
                PageIndex: 1,
                PageSize: 10,
                SearchColumns:{},    //查询的动态表单配置
                OrderBy:"",           //排序字段
                SortType: 'DESC',    //如果有提供OrderBy，SortType默认为'ASC'
                Status:-1,
                page:1,
                start:0,
                limit:15
            },
            tag:{
                VipId:"",
                orderID:''
            },
            seach:{
                item_category_id:null,
                SalesPromotion_id:null,
                form:{
                    item_code:"",
                    item_name:"",
                    item_startTime:'',
                    item_endTime:''
                }
            }
			 
        }

    };
    page.init();
});


