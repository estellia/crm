Ext.define('Jit.form.field.Display', {
    extend: 'Ext.form.field.Display'
    , alias: 'widget.jitdisplayfield'
    , config: {
        /*
        @size 可选的值有small,big
        */
        jitSize: null
    }
    , constructor: function (cfg) {
        //定义默认配置
        var defaultConfig = {
            labelSeparator: ''
            , labelPad: 10
            , labelAlign: 'right'
            , margin: '0 10 10 10'
            , labelWidth: 73
            , cls: 'Displayfield'
            , height: 22
        };
        //自己的配置项处理
        var cfg = Ext.applyIf(cfg, {
            jitSize: 'small'
        });
        if (cfg.jitSize) {
            var size = cfg.jitSize.toString().toLowerCase();
            switch (size) {
                case 'small':
                    {
                        defaultConfig.width = 183;
                    }
                    break;
                case 'big':
                    {
                        defaultConfig.width = 233;
                    }
                    break;
            }
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