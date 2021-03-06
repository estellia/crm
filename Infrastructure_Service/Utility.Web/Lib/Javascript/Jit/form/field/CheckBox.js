﻿Ext.define('Jit.form.field.Checkbox', {
    extend: 'Ext.form.field.Checkbox'
    , alias: 'widget.jitcheckbox'
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
            , margin: '0 10 10 10'
            , width: 183
            , labelWidth: 73
            , height: 22
        }

        //合并配置项
        cfg = Ext.applyIf(cfg, defaultConfig);
        //初始化配置项
        this.initConfig(cfg);
        //调用父类进行初始化
        this.callParent(arguments);
    },
    jitGetValue: function () {
        return this.getValue();
    },
    jitSetValue: function (value) {
        this.setValue(value);
    }
});