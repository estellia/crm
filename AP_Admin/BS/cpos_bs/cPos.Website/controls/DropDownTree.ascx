<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DropDownTree.ascx.cs" Inherits="controls_DropDownTree" %> 
    <table id="<%=this.ClientID%>" class="dropDownTree <%=this.ReadOnly?"readonly":"" %>"   cellpadding="0" cellspacing="0" style="height:<%=this.Height%>;width:<%=this.Width%>"  >
        <tr style="line-height:10px;height:<%=this.Height%>"><td td style="width:auto; height:<%=this.Height%>"><input type="text" id="tbItemText" runat="server" style="border:none;background-color:transparent;height:16px;" readonly="readonly"/></td><td style="width:17px;height:<%=this.Height%>"><asp:Image ID="HyperLink1" runat="server" CssClass="dropbar" ImageUrl="~/img/dropdown.png" Width="17px" style="Height:<%=this.Height%>"  ></asp:Image></td></tr>
        <tr style="height:0px"><td colspan="2" style="height:0px;">
                <div class="pop" style="position:absolute;">
                    <table style="padding:0;margin:0" cellpadding="0" cellspacing="0">
                        <tr><td style="vertical-align:top;" ><div style="overflow-y:auto;overflow-x:hidden;" class="tvPanel"><asp:Panel runat="server" ID="tvItems"></asp:Panel></div></td></tr> 
                        <%if (this.MultiSelect)
                          { %>
                        <tr><td style="height:4px"><div style="border-top:1px solid #ccc;"></div></td></tr>
                        <tr><td style="text-align:right;height:10px;"><input type="button" value="选择" id="btnSelect" runat="server" class="button"/><input type="button" value="取消" id="btnCancel" runat="server"  class="button" /></td></tr>
                        <%} %>
                    </table> 
                </div>
        </td></tr>
    </table>
    <div style="display:none">
        <asp:HiddenField runat="server" ID="tbSelectTexts" />
        <asp:HiddenField runat="server" ID="tbSelectValues" />
    </div>  
    <script type="text/javascript">
        //js 闭包初始化树下拉选择控件   ▽
        (
        function () {
            var $tbSelectValues = $('#<%=tbSelectValues.ClientID %>');
            var $tbSelectTexts = $('#<%=tbSelectTexts.ClientID %>');
            var $tbItemText = $('#<%=tbItemText.ClientID %>');

            var vari = document.getElementById('<%=this.ClientID %>');
            var $tree = $('#<%=tvItems.ClientID %>');
            
            //数据源
            var val = $("#<%=this.ClientID%>"+"_"+"data" ).val();
            if(val == null || val == "")
            {
                val = "[]";
            }
            vari.rootdata = eval(val);

            //获取或设置选择的值
            vari.values = function () {
                if (arguments.length > 0) {
                    $tbSelectValues.val(toCodeString(arguments[0]));
                }
                else {
                    return toArray($tbSelectValues.val())
                }
            }

            //获取或设置选择的文本
            vari.texts = function () {
                if (arguments.length > 0) {
                    $tbSelectTexts.val(toCodeString(arguments[0]));
                    vari.title = arguments[0].join(',');
                }
                else {
                    return toArray($tbSelectTexts.val());
                }
            }
            //将字符串数组转换成字符串
            function toCodeString(arr) {
                var rult = [];
                $(arr).each(function (index, e) {
                    rult.push(encodeURIComponent(e));
                });
                return rult.join('|');
            }
            //将字符串组转换成字符串
            function toArray(str) {
                var rult = [];
                if(str != "" && str != null)
                {
                    $(str.split('|')).each(function (index, e) {
                        rult.push(decodeURIComponent(e));
                    });
                }
                return rult;
            }

            //获取或设置文本
            vari.text = function () {
                if (arguments.length > 0) {
                    $tbItemText.val(arguments[0]);
                    vari.title = arguments[0];
                }
                else {
                    return $tbItemText.val();
                }
            }
            //显示下拉项
            vari.showPop = function () {
                vari.init();
                vari.isShowPop = true;
                $(vari).find(".pop").slideDown("fast");
            }

            vari.hidePop = function () {
                vari.isShowPop = false;
                $(vari).find(".pop").slideUp("fast");
            }

            //选择事件，前台 js 可能注册此方法以判断用户是否可以选择项。
            vari.onselect=function (item)
            {    
                return true;
            }

            //选择改变事件 
            vari.onchanged=function()
            {    
                return true;
            }

            //初始化树结点
            vari.init = function(){
                if(!$tree.initflg)
                {
                    $tree.treeview(getTreeCfgObj());
                    $tree.initflg = true;
                }
            }

            //返回 jsonTree 参数对象
            function getTreeCfgObj()
            {
                var o = { 
                    url:'<%=this.Url%>'
                    ,showcheck: <%=this.MultiSelect?"true":"false" %>
                    ,onnodeclick:function(item){
                        var is_single_select = <%=this.MultiSelect?"false":"true" %>;

                        //设置选择项-单选模式
                        if(is_single_select && (!vari.onselect || vari.onselect && vari.onselect(item)))
                        {
                            vari.text(item.text);
                            var ischanged = vari.values().join(',') != [item.value].join(',');
                            vari.values([item.value]);
                            vari.texts([item.text]);
                            vari.hidePop();
                            
                            if(ischanged && vari.onchanged) 
                            {
                                vari.onchanged();
                            } 
                        }
                    }
                };
                o.data = vari.rootdata;
                o.cbiconpath ='<%=this.ResolveUrl("~") %>css/images/icons/';
                return o;
            }
            /*====== 界面元素事件、初始化 ======*/
            //确定
            $("#<%=btnSelect.ClientID %>").click(
                function () {
                    var selval = $tree.getCheckedNodes();
                    var val = "";
                    if (selval != null) {
                        val = selval.join(",");
                    }
                    
                    var tsc = $tree.getTSNs();
                    var text = [];
                    $(tsc).each(function (index, e) {
                        text.push(e.text);
                    });
                    if (text.length > 0) {
                        vari.text(text[0]);
                    }
                    else {
                        vari.text("");
                    }
                    vari.texts(text);
                    var ischanged = vari.values().join(',') != selval.join(',');
                    vari.values(selval); 
                    vari.hidePop();
                    if(ischanged && vari.onchanged)
                    {
                        vari.onchanged();
                    }
                }
            );
            //取消
            $("#<%=btnCancel.ClientID %>").click(
                function () {
                    vari.hidePop();
                }
            );
           
            //显示/隐藏下拉列表
            $("#<%=HyperLink1.ClientID %>").click(
                function () {
                    <%if(!this.ReadOnly){ %>
                    if (vari.isShowPop) {
                        vari.hidePop();
                    }
                    else {
                        vari.showPop();
                    }
                    <%} %>
                }
            );

            vari.title = vari.texts().join(',');

            $tbItemText.css("width","100%");

            $(vari).find(".pop .tvPanel").css("width", '<%=this.DropdownWidth %>');
            $(vari).find(".pop .tvPanel").css("height", '<%=this.DropdownHeight %>'); 

            //鼠标在其它区域按下将出起dropdown
            $(document).mousedown(
                function(e){
                    if(vari.isShowPop)
                    {
                        if($(e.target).parents('#'+vari.id).length==0)
                        {
                            vari.hidePop();
                        }
                    }
                }
            );
            
            vari.focus = function(){
                $tbItemText.focus();
            }

        }) (); 
           
    </script> 