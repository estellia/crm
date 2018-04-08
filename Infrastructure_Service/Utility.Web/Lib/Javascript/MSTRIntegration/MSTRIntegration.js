/*
MSTR集成
@/// <param name="postFormID" type="String">用于将MSTR请求参数POST到MSTR服务器的中转form的ID,如果为空或null,则按自己的规则生成</param>
*/
function MSTRIntegration(postFormID) {
    this.postFormID = postFormID;
    this.postForm = null;
}
MSTRIntegration.prototype = {
    /*
    @private    获取用于中转MSTR请求参数的form
    */
    getPOSTForm: function () {
        if (this.postForm == null) {
            if (this.postFormID == null) {
                this.postFormID = "___mstrIntegration_post_transit_form";
            }
            var form = document.getElementById(this.postFormID);
            if (form == null) {
                form = document.createElement("FORM");
                form.setAttribute("id", this.postFormID);
                form.method = "POST";
                document.body.appendChild(form);
            }
            this.postForm = form;
        }
        //
        return this.postForm;
    }
    /*
    @private    从URL的QueryString中获取参数列表对象
    @/// <param name="url" type="String">url</param>
    */
    , getParamsInQueryString: function (url) {
        var result = {};
        if (url != null) {
            var start = url.indexOf('?');
            var queryString = url.substring(start);
            result = Ext.Object.fromQueryString(queryString);
            result.___domain = url.substring(0, start);
        }
        //
        return result;
    }
    /*
    @public     采用POST的方式,将MSTR报表呈现在指定的iframe下
    @/// <param name="reportUrl" type="String">带有集成信息&提示答案等参数的MSTR报表的URL</param>
    @/// <param name="iframeID" type="String">用于呈现MSTR报表的iframe容器的ID</param>
    */
    , RenderReport: function (reportUrl, iframeID) {
        if (reportUrl != null && reportUrl.length > 0) {
            //通过id获取iframe对象的name,如果没有名称则设置与id同名的name
            var reportContainerFrame = document.getElementById(iframeID);
            if (reportContainerFrame == null) {
                Ext.Error.raise("未找到ID为" + iframeID + "报表容器iframe.");
            }
            var frameName = iframeID;
            if (reportContainerFrame.name == null || reportContainerFrame.name == "") {
                Ext.Error.raise("报表容器[ID=" + iframeID + "]未设置name属性,请设置name属性.");
            } else {
                frameName = reportContainerFrame.name;
            }
            //解析参数
            var params = this.getParamsInQueryString(reportUrl);
            //提交MSTR报表请求
            var form = this.getPOSTForm();
            form.action = params.___domain;
            delete params.___domain;
            form.target = frameName;
            //将所有的QueryString中的参数添加进form中
            //先移除
            while (form.firstChild) {
                form.removeChild(form.firstChild);
            }
            //然后添加
            for (var key in params) {
                if (params.hasOwnProperty(key)) {
                    var item = document.createElement("input");
                    item.setAttribute("type", "hidden");
                    item.setAttribute("name", key);
                    item.setAttribute("value", params[key]);
                    //
                    form.appendChild(item);
                }
            }
            //
            form.submit();
        }
    }
};
//创建一个实例
var MSTRIntegrationUtils = new MSTRIntegration();