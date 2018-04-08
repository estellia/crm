function __fnGetImg(m, type, id, value) {

    pObjectValue = eval(decodeURIComponent(value));
    var n = parseInt(document.getElementById("__sp_" + id).innerHTML) - 1;

    if (type == 1) {
        n = n - 1;
    }
    else {
        n = n + 1;
    }
    if (n >= m) {
        n = 0;
    }
    if (n < 0) {
        n = m - 1;
    }
    document.getElementById("__img" + id).src = '/File/MobileDevices/Photo/' + __clientid + '/' + pObjectValue[n].ClientUserID + '/'
             + pObjectValue[n].FileName;
    document.getElementById("__sp_" + id).innerHTML = (n + 1);
}
Ext.define('Jit.window.PhotoWindow', {
    alias: 'widget.jitphotowindow',

    constructor: function (args) {
        var me = this;
        var defaultConfig = {
            id: '__PhotoWindowID'   //panel 的id 默认为mapSelect          
            , renderTo: null //panel的renderTo          
            , photoTitle: '照片查看'
            , pClientID: 0
            , pClientUserID: 0
            , pObjectValue: ''
        }
        args = Ext.applyIf(args, defaultConfig);
        if (__clientid != null) {
            args.pClientID = __clientid;
        }
        if (args.value != null && args.value != '') {
            me.pObjectValue = eval(decodeURIComponent(args.value));
        }
        //照片窗体控件
        //照片panel
        me.photoPanelImg = Ext.create('Ext.panel.Panel', {
            width: 490,
            height: 325,
            columnWidth: 1,
            html: "<div style='width:488px;height:295px; text-align:center;padding-top:5px'>" +
            "<img id='__img" + args.id + "' style='max-width:480px;max-height:280px' src='" +
             "/File/MobileDevices/Photo/" + args.pClientID + "/" + me.pObjectValue[0].ClientUserID + "/"
             + me.pObjectValue[0].FileName + "'></div><div  style='width:488px;height:20px; text-align:center;'>" +
             "<a href='javascript:void(0)' onclick='__fnGetImg(" + me.pObjectValue.length + ",1,\"" + args.id + "\",\"" + args.value + "\")' >上一张</a>  <span id='__sp_" + args.id + "'>1</span>/" + me.pObjectValue.length + "  <a href='javascript:void(0)' onclick='__fnGetImg(" + me.pObjectValue.length + ",2,\"" + args.id + "\",\"" + args.value + "\")'>下一张</a></div>",
            layout: 'column',
            border: 0
        });

        if (Ext.getCmp(args.id) != null) {
            Ext.getCmp(args.id).destroy();
        }
        //上传图片的window
        var instance = Ext.create('Jit.window.Window', {
            id: args.id,
            title: args.photoTitle,
            items: [me.photoPanelImg],
            width: 500,
            height: 345,
            jitSize: "custom",
            constrain: true,
            modal: true
        });
    }
});