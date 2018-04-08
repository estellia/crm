define(['jquery', 'util'], function ($) {
    var page = {
        ele: {
            section: $("#section"),
            author: $("#author"),
            version: $("#version"),
            pageJson: $("#pageJson")

        },
        init: function () {
            this.pageId = $.util.getUrlParam("id");
            //debugger;
            //this.pageId = !this.pageId || "9AFF5764-AB2C-4DD2-8FD0-5D158BB3719A";
            this.loadData();
            this.initEvent();
        },
        loadData: function () {
            if (this.pageId) {
                this.loadPageInfo();
            }
        },
        initEvent: function () {
            this.ele.section.delegate("#submitBtn", "click", function () {
                var author, version, pageJson;
                author = self.ele.author.val();
                version = self.ele.version.val();
                pageJson = self.ele.pageJson.val();

                if (author == "") {
                    alert("请输入作者");
                    return false;
                }
                if (version == "") {
                    alert("请输入版本号");
                    return false;
                }
                if (pageJson == "") {
                    alert("请编辑pageJson");
                    return false;
                } else {
                    var json;
                    try {
                        json = JSON.parse(pageJson)
                    } catch (error) {
                        alert("json format error");
                        return false;
                    }
                }
                var obj = {};
                obj.Author = author;
                obj.Version = version;
                obj.PageJson = pageJson;
                if (self.pageId) {
                    obj.PageId = self.pageId;
                }

                self.submit(obj);
            }).delegate("#cancelBtn", "click", function () {
                window.history.go(-1);
            });
        },
        submit: function (param) {
            $.util.ajax({
                url: "http://112.124.68.147:9004/ApplicationInterface/Gateway.ashx",
                action: "WX.SysPage.SetSysPage",
                dataType: "jsonp",
                data: param,
                success: function (data) {
                    if (data.IsSuccess) {
                        alert("提交成功，即将跳往列表页！");
                        window.location = "configList.aspx";

                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        loadPageInfo: function (callback) {
            $.util.ajax({
                url: "http://112.124.68.147:9004/ApplicationInterface/Gateway.ashx",
                action: "WX.SysPage.GetSysPageDetail",
                dataType: "jsonp",
                data: {
                    PageId: this.pageId
                },
                success: function (data) {
                    if (data.IsSuccess) {
                        if (callback) {
                            callback(data.Data);
                        } else {
                            var idata = data.Data.PageInfo[0];
                            self.ele.author.val(idata.Author); //.attr("disabled", "disabled");
                            self.ele.version.val(idata.Version); //.attr("disabled", "disabled");
                            self.ele.pageJson[0].innerHTML = idata.PageJson;
                        }

                    } else {
                        alert(data.Message);
                    }
                }
            });
        },
        render: function (temp, data) {
            var render = template.compile(temp);
            return render(data || {});
        }
    };

    self = page;

    page.init();
});