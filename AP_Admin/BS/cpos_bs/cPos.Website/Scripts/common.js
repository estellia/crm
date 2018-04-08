//check gridview all check box
//require jquery
function checkAll(event) {
    var e = event ? event : window.event;
    var obj = e.target || e.srcElement;
    $(obj).parents('table:first')
              .find(':checkbox').filter(':enabled')
              .attr("checked", $(obj).attr("checked"));

}


function checkColAll(event) {
    var e = event ? event : window.event;
    var obj = e.target || e.srcElement;
    var list = $(obj).parents('table:first')
              .find('.check').filter(':enabled');
    $(obj).parents('table:first')
              .find('.check').find(':checkbox').filter(':enabled')
              .attr("checked", $(obj).attr("checked"));

}

//open a window 
function openWindow(url, width, height, NewWin) {

    var popUpWin = 0;
    var left = 200;
    var top = 200;
    if (screen.width >= width) {
        left = Math.floor((screen.width - width) / 2);
    }
    if (screen.height >= height) {
        top = Math.floor((screen.height - height) / 2);
    }

    var from = window.open(url, NewWin, 'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=1,resizable=yes,copyhistory=yes,Width=' + width + ',Height=' + height + ',dialogLeft=' + left + ', dialogTop=' + top + ',screenX=' + left + ',screenY=' + top + '' + "edge: Raised; center: Yes; help: Yes; resizable: Yes; status: Yes;");

}

function showModelWindow(url, width, height) {
    var from = window.showModalDialog(url, null, 'dialogHeight=' + height + ';dialogWidth=' + width);
    return from;
}


/*消息提示筐*/
var infobox = {};
//方法可被重写
infobox.showPop = function (text, className) {
    alert(text);
}

//弹出 bill_action 页面，返回 bool ,为 true 时表达保存操作。
function show_bill_action(bill_kind_code, have_flow, action_flag_type, action_flag_value, bill_id) {
    var url = "../bill/bill_action.aspx?bill_kind_code=" + bill_kind_code + "&have_flow=" + have_flow + "&action_flag_type=" + action_flag_type + "&action_flag_value=" + action_flag_value + "&bill_id=" + bill_id;
    var rtnVal = window.showModalDialog(url, "bill_action", "dialogHeight=250px;dialogWidth=400px;");
    return rtnVal;
}
//弹出inventory/cc_bill_show_item或adjust_bill_show_item页面，返回string,不为""时表示保存操作。
function show_bill_item(urlname, array) {
    var url = "../inventory/" + urlname + ".aspx?data="+array;
    var rtnVal = window.showModalDialog(url,"", "dialogHeight=600px;dialogWidth=800px;");
    return rtnVal;
}



//getTop 在有多个frame也就是iframe时,得到最顶级的frame.
//return:window
var getTop
= function (win) {
    if (win) {
        return win.parent && win.parent != win ? getTop(win.parent) : win;
    }
    else
        return getTop(window);
}

//查验当前页面是否包含在顶级iframe中，否则将顶级iframe跳转到当前页的url地址
var checkFrame_IsTop
= function () {
    if (getTop() != window) {
        getTop().location.href = window.location.href;
        return false;
    }
    return true;
}
//获取数组指定元素的索引
Array.prototype.getValueIndex = function (value) {
    if (!this | this.length == 0) {
        return;
    }
    var index = -1;
    for (var i = 0; i < this.length; i++) {
        if (this[i] == value) {
            index = i;
            break;
        }
    }
    return index;
}
//移除指定索引处的元素
Array.prototype.remove = function (index) {
    if (!this | this.length == 0) {
        return;
    }
    if (index == -1) {
        throw "数组中未找到该元素";
    }
    for (var i = 0, n = 0; i < this.length; i++) {
        if (this[index] != this[i]) {
            this[n++] = this[i];
        }
    }
    this.length -= 1;
}
//移除数组中的元素
Array.prototype.removeValue = function (value) {
    var index = this.getValueIndex(value);
    this.remove(index);
}
var max_length = 180; //默认textarea文本长度
$(function () {
    $("textarea").each(function () {
        if (!$(this).hasClass("customer")) {
            $(this).keyup(validateMaxLength).blur(validateMaxLength);
        }
    });

    var _ddtree = $(".dropDownTree");
    if (_ddtree.length != 0) {
        var _width = $(_ddtree[0]).css("width");
        var _intWidth = parseInt(_width.substring(0, _width.length - 2)) - 4;
        $(".tvPanel").css("width", _intWidth + "px");
    }
});
function validateMaxLength() {
    var target = event.target;
    if (!target) {
        target=event.srcElement;
    }
    var _length = $(target).val().length;
    if (_length > max_length) {
        target.value = $(target).val().substring(0, max_length);
    }
}
//日期格式yyyy-MM
function getShortDate(sender) {
    var value = sender.value.split("-");
    if (value.length == 0) {
        return;
    }
    value.pop();
    var shortDate = value.join("-");
    sender.value = shortDate;
}
//保存属性数据
function savePropData(type) {
    var unit = [];
    switch (type) {
        case "UNIT": saveUnitData(); break;
        case "ITEM": saveItemData(); break;
        default: break;
    }
    function saveItemData() {
        $("._prop_detail").each(function () {
            var para = {};
            para.PropertyCodeId = $(this).attr("id");
            para.PropertyDetailId = $(this).attr("PropertyDetailId");
            para.PropertyCodeValue = this.value;
            unit.push(para);
        });
        $("._prop_detail_radio").each(function () {
            if ($(this).attr("checked")) {
                var para = {};
                para.PropertyCodeId = $(this).attr("PropertyDetailId");
                para.PropertyDetailId = $(this).attr("id");
                unit.push(para);
            }
        });
    }
    function saveUnitData() {
        $("._prop_detail").each(function () {
            var para = {};
            para.PropertyCodeId = $(this).attr("id");
            para.PropertyDetailId = $(this).attr("PropertyDetailId");
            para.PropertyDetailCode = this.value;
            unit.push(para);
        });
        $("._prop_detail_radio").each(function () {
            if ($(this).attr("checked")) {
                var para = {};
                para.PropertyCodeId = $(this).attr("PropertyDetailId");
                para.PropertyDetailCode = $(this).attr("id");
                unit.push(para);
            }
        });
    }
    return unit;
}

//加载属性数据
function loadPropData(data, type) {
    var $data = $(data);
    switch (type) {
        case "UNIT": loadUnitProp(data); break;
        case "ITEM": loadItemProp(data); break;
        default: break;
    }
    function loadItemProp(data) {
        $data.each(function (index, item) {
            var value = "";
            if ($("#" + item.PropertyCodeId).attr("type") == "text") {
                value = item.PropertyCodeValue;
            } else {
                value = item.PropertyDetailId;
            }
            $("#" + item.PropertyCodeId).val(value).attr("checked", "checked").attr("PropertyDetailId", item.PropertyDetailId);
            if ($("#" + item.PropertyCodeId).length == 0) {
                $("#" + item.PropertyDetailId).attr("checked", "checked");
            }
        });
    }
    function loadUnitProp(data) {
        $data.each(function (index, item) {
            var value = "";
            if ($("#" + item.PropertyCodeId).attr("type") == "text") {
                value = item.PropertyDetailCode;
            }
            else {
                value = item.PropertyDetailId;
            }
            $("#" + item.PropertyCodeId).val(value).attr("checked", "checked").attr("PropertyDetailId", item.PropertyDetailId);
            if ($("#" + item.PropertyCodeId).length == 0) {
                $("#" + item.PropertyDetailId).attr("checked", "checked");
            }
        });
    }
}
Date.prototype.isBigThanCurrentDate = function (date) {
    var _current = new Date().toLocaleDateString().replace("年", "-").replace("月", "-").replace("日", "-").split("-");
    var _currentDate = new Date(_current[0], parseInt(_current[1]) - 1, _current[2]);
    if (!date) {
        return false;
    }
    var _thisDate = getDateFromStr(date);
    if (_thisDate > _currentDate) {
        return true;
    }
    return false;
}
function getDateFromStr(str) {
    //var rel = str.replace(/^[-,\/,年,月,日]*$/g, ",");
    var _this = str.split("-");
    if (_this[1].substring(0, 1) == "0") {
        _this[1] = _this[1].replace(/[0]{1}/, "");
    }
    if (_this[2].substring(0, 1) == "0") {
        _this[2] = _this[2].replace(/[0]{1}/, "");
    }
    var _thisDate = new Date(_this[0], parseInt(_this[1]) - 1, parseInt(_this[2]));
    return _thisDate;
}
Date.prototype.isFirBigThanSec = function (date1, date2) {
    if (date1 == "" | date2 == "") {
        return false;
    }
    var _thisDate1 = getDateFromStr(date1);
    var _thisDate2 = getDateFromStr(date2);
    if (_thisDate1 > _thisDate2) {
        return true;
    }
    return false;
}
//禁用区域内所有控件
function disableCtrls(area) {
    $(area).find("input").attr("readonly", "readonly").attr("disabled", "disabled");
    $(area).find("select").attr("readonly", "readonly").attr("disabled", "disabled");
    $(area).find("textarea").attr("readonly", "readonly").attr("disabled", "disabled");
    $(area).find("dropDownTree").toggleClass("readonly", true);
}

//验证规则
var is_reg_cn = /[\u0391-\uFFE5]/; //中文
var is_reg_en = /^[\s,a-z,A-Z]*$/; //英文
var is_reg_spec = new RegExp("[ ,\\`,\\~,\\!,\\@,\#,\\$,\\%,\\^,\\+,\\*,\\&,\\\\,\\/,\\?,\\|,\\:,\\.,\\<,\\>,\\{,\\},\\(,\\),\\',\\;,\\=,\"]"); //特殊字符
var is_reg_empty = /^\s*$/; //空
var is_reg_url = /^[a-z,A-Z,0-9,\.\-\=_\?\/]*$/; //url
var is_reg_mobile = /^0{0,1}(13[0-9]|15[0-9]|18[1-9]|14[1-9])[0-9]{8}$/; //移动电话
var is_reg_phone = /^0\d{2,3}(\-)?\d{7,8}$|^\d{7,8}$/; //固话
var is_reg_email = /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/; //邮箱
var is_reg_postcode = /^[1-9]\d{5}$/; //邮编

function validateSpecChar(element, msg, allowEmpty, tab) {
    if (!initBegin(element, msg, allowEmpty, tab)) {
        return false;
    }
    var value = $(element).val();
    if (allowEmpty) {
        if (is_reg_empty.test(value)) {
            return true;
        }
    }
    if (is_reg_spec.test(value)) {
        initEnd(element,msg+ "不允许输入特殊字符!", tab);
        return false;
    }
    return true;
}

function validatePostCode(element, msg, allowEmpty, tab) {
    if (!initBegin(element, msg, allowEmpty, tab)) {
        return false;
    }
    var value = $(element).val();
    if (allowEmpty) {
        if (is_reg_empty.test(value)) {
            return true;
        }
    }
    if (!is_reg_postcode.test(value)) {
        initEnd(element, "邮编格式不正确!", tab);
        return false;
    }
    return true;
}
function validateEmail(element, msg, allowEmpty, tab) {
    if (!initBegin(element, msg, allowEmpty, tab)) {
        return false;
    }
    var value = $(element).val();
    if (allowEmpty) {
        if (is_reg_empty.test(value)) {
            return true;
        }
    }
    if (!is_reg_email.test(value)) {
        initEnd(element, "邮箱格式不正确!", tab);
        return false;
    }
    return true;
}
function validatePhone(element, msg, allowEmpty, tab) {
    if (!initBegin(element, msg, allowEmpty, tab)) {
        return false;
    }
    var value = $(element).val();
    if (allowEmpty) {
        if (is_reg_empty.test(value)) {
            return true;
        }
    }
    if (!is_reg_phone.test(value)) {
        initEnd(element, msg+"号码格式不正确!", tab);
        return false;
    }
    return true;
}

function validateMobile(element, msg, allowEmpty, tab) {
    if (!initBegin(element, msg, allowEmpty, tab)) {
        return false;
    }
    var value = $(element).val();
    if (allowEmpty) {
        if (is_reg_empty.test(value)) {
            return true;
        }
    }
    if (!is_reg_mobile.test(value)) {
        initEnd(element, msg+"号码格式不正确!", tab);
        return false;
    }
    return true;
}
function validateUrl(element, msg, allowEmpty, tab) {
    if (!initBegin(element, msg, allowEmpty, tab)) {
        return false;
    }
    var value = $(element).val();
    if (allowEmpty) {
        if (is_reg_empty.test(value)) {
            return true;
        }
    }
    if (!is_reg_url.test(value)) {
        initEnd(element, "请录入正确的Url地址!", tab);
        return false;
    }
    return true;
}
function validateCode(element, msg, allowEmpty, tab) {
    if (!initBegin(element, msg, allowEmpty, tab)) {
        return false;
    }
    var value = $(element).val();
    if (allowEmpty) {
        if (is_reg_empty.test(value)) {
            return true;
        }
    }
    if (is_reg_cn.test(value) | is_reg_spec.test(value)) {
        initEnd(element, msg+"不允许输入中文或特殊字符!", tab);
        return false;
    }
    return true;
}
function validateEnName(element, msg, allowEmpty, tab) {
    if (!initBegin(element, msg, allowEmpty, tab)) {
        return false;
    }
    var value = $(element).val();
    if (allowEmpty) {
        if (is_reg_empty.test(value)) {
            return true;
        }
    }
    if (!is_reg_en.test(value)) {
        initEnd(element, "请录入英文!", tab);
        return false;
    }
    return true;
}
function validateNotAllowCn(element, msg, allowEmpty, tab) {
    if (!initBegin(element, msg, allowEmpty, tab)) {
        return false;
    }
    var value = $(element).val();
    if (allowEmpty) {
        if (is_reg_empty.test(value)) {
            return true;
        }
    }
    if (is_reg_cn.test(value)) {
        initEnd(element, msg + "不允许输入中文!", tab);
        return false;
    }
    return true;
}
function validateNotEmpty(element, msg, tab) {
    var value = $(element).val();
    if (is_reg_empty.test(value)) {
        if (msg) {
            alert(msg + "不能为空!");
        }
        if (tab) {
            $(tab).click();
            setTimeout(function () { $(element).focus(); }, 100);
            return false;
        }
        $(element).focus();
        return false;
    }
    return true;
}
function initBegin(element, msg, allowEmpty, tab) {
    if (!element | element.length == 0) {
        return false;
    }
    var value = $(element).val();
    if (allowEmpty == undefined) {
        allowEmpty = true;
    }
    if (!allowEmpty) {
        if (!validateNotEmpty(element, msg, tab)) {
            $(element).focus();
            return false;
        }
    }
    return true;
}
function initEnd(element, msg, tab) {
    if (msg) {
        alert(msg);
    }
    if (tab) {
        $(tab).click();
        setTimeout(function () { $(element).focus(); }, 100);
        return false;
    }
    $(element).focus()
}

function getTableShowOrHide(table_id, div_id) {
    var length = $("#" + table_id + " tr").length;
    if (length == 1) {
        $("#" + table_id).parent().css("display", "none");
        $("#" + div_id).css("display", "none");
    } else {
        $("#" + table_id).parent().css("display", "");
        $("#" + div_id).css("display", "");
    }
}
function getInvTableShowOrHide(tbody_id, div_id) {
    var length = $("#" + tbody_id + " tr").length;
    if (length == 0) {
        $("#" + tbody_id).parent().parent().css("display", "none");
        $("#" + div_id).css("display", "none");
    } else {
        $("#" + tbody_id).parent().parent().css("display", "");
        $("#" + div_id).css("display", "");
    }
}